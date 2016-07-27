using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//Made by Alex McKirdy
//PP590
//on 21 June 2013

//This is Snake

namespace Snake
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //sets up all lists, brushes and variables with a global scope
        //List<Point> snake = new List<Point>();
        //string move;
        //SolidBrush snakeBrush = new SolidBrush(Color.Black);
        //int length;
        //int score;

        //player 1, Up Down Left Right
        Snake mysnake = new Snake(Color.Black);
        //Player 2, W S A D
        Snake mysnake2 = new Snake(Color.Red);
         

        Color clearColor = Color.OliveDrab;
        SolidBrush clearBrush = new SolidBrush(Color.OliveDrab);
        SolidBrush appleBrush = new SolidBrush(Color.Red);
        



        List<Point> apples = new List<Point>();

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            clearBrush.Color = clearColor;
            pictureBox1.CreateGraphics().Clear(clearColor);
            //draws rectangle around board
            pictureBox1.CreateGraphics().DrawRectangle(new Pen(Color.Black), 0, 0, 400, 400);

            //for startpoint
            mysnake.Clear();
            mysnake.Add(new Point(300, 200));
            mysnake.Add(new Point(310, 200));
            mysnake.Add(new Point(320, 200));
            mysnake.setDir("u");
            //mysnake.move();

            mysnake2.Clear();
            mysnake2.Add(new Point(100, 200));
            mysnake2.Add(new Point(110, 200));
            mysnake2.Add(new Point(120, 200));
            mysnake2.setDir("u");

            //score = 0;
            //length = (score + 4) * 3;
            apples.Clear();
            spawnApple();

            snakeTimer.Start();
            appleTimer.Start();



        }

        //for key movement
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                mysnake.setDir("u");
            }
            if (e.KeyCode == Keys.Down)
            {
                mysnake.setDir("d");
            }
            if (e.KeyCode == Keys.Left)
            {
                mysnake.setDir("l");
            }
            if (e.KeyCode == Keys.Right)
            {
                mysnake.setDir("r");
            }

            //snake2
            if (e.KeyCode == Keys.W)
            {
                mysnake2.setDir("u");
            }
            if (e.KeyCode == Keys.S)
            {
                mysnake2.setDir("d");
            }
            if (e.KeyCode == Keys.A)
            {
                mysnake2.setDir("l");
            }
            if (e.KeyCode == Keys.D)
            {
                mysnake2.setDir("r");
            }
        }


        private void snakeTimer_Tick(object sender, EventArgs e)
        {

            Graphics g = pictureBox1.CreateGraphics();

            Point currentHead = mysnake.snakePoint(mysnake.Count() - 1);
            Point currentHead2 = mysnake2.snakePoint(mysnake2.Count() - 1);
            //places next point dependant on the direction it is 


            //draw snake

            mysnake.move(g);
            mysnake.draw(g);
            mysnake2.move(g);
            mysnake2.draw(g);
            //apple collision
            if (checkApple(currentHead))
            {
                mysnake.addScore();
                spawnApple();
            }

            if (checkApple(currentHead2))
            {
                mysnake2.addScore();
                spawnApple();
            }

            //running into itself or leaving the border
            if (mysnake.checkCollision(currentHead, mysnake2))
            {
                //dead snake!
                snakeTimer.Stop();
                appleTimer.Stop();
                MessageBox.Show("Game Over", "Player 2 Wins");

            }

            if (mysnake2.checkCollision(currentHead2, mysnake))
            {
                //dead snake!
                snakeTimer.Stop();
                appleTimer.Stop();
                MessageBox.Show("Game Over", "Player 1 Wins");

            }

            //changes the length - this makes it dependent on the score
            //length = (score + 4) * 3;

            //clears behind the snake
            if (!checkApple(mysnake.snakePoint(mysnake.Count() - 1)))
            {
                Point snakeEnd = mysnake.snakePoint(0);
                mysnake.RemoveAt(0);
                g.FillRectangle(clearBrush, snakeEnd.X, snakeEnd.Y, 10, 10);
            }

            if (!checkApple(mysnake2.snakePoint(mysnake2.Count() - 1)))
            {
                Point snakeEnd2 = mysnake2.snakePoint(0);
                mysnake2.RemoveAt(0);
                g.FillRectangle(clearBrush, snakeEnd2.X, snakeEnd2.Y, 10, 10);
            }


            //draws apples
            foreach (Point p in apples)
            {
                g.FillEllipse(appleBrush, p.X, p.Y, 10, 10);
            }

            //shows score in corner
            labelScore1.Text = "Snake 1 Score = " + mysnake.returnScore().ToString();
            labelScore2.Text = "Snake 2 Score = " + mysnake2.returnScore().ToString();
        }

        

        //public Boolean checkCollision(Point current)
        //{
        //    //checks if snake leaves walls
        //    if (current.X < 0 || current.X >= 400 || current.Y < 0 || current.Y >= 400)
        //    {
        //        return true;
        //    }

        //    //checks collision with itself
        //    for (int i = 0; i < mysnake.Count() - 2; i++)
        //    {
        //        if (current.X == mysnake.snakePoint(i).X && current.Y == mysnake.snakePoint(i).Y)
        //        {
        //            return true;
        //        }
        //        //if currenthead.x == p.x and head.y == y
        //    }

        //        return false;

        //}
        

        
        public Boolean checkApple(Point current)
        {
            //for every apple (usually one though)
            for (int i = 0; i < apples.Count(); i++)
            {
                if (current.X == apples[i].X && current.Y == apples[i].Y)
                {
                    apples.RemoveAt(i);
                    spawnApple();
                    return true;

                }
            }
            return false;
        }


        public void spawnApple()
        {
            //places an apple in a random location
            Random rand = new Random();
            //makes num multiple of 10 - so in grid
            int appleX = rand.Next(0, 40) * 10;
            int appleY = rand.Next(0, 40) * 10;
            apples.Add(new Point(appleX, appleY));
        }

        private void appleTimer_Tick(object sender, EventArgs e)
        {
            //spawnApple();
        }
        private void Form1_Load(object sender, EventArgs e)
        { }


    }
}
