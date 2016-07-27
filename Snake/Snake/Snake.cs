using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Snake
{
    class Snake
    {
        List<Point> snake = new List<Point>();
        string dir = "u";
        int score;
        SolidBrush brush;
        //needs direction, list of points,
        //needs to
            //add a new point
            //remove the old point
            //draw self
            //grow on apple
            //collision

        //return list function?

       //returns a point at [i]

        public Snake(Color c)
        {
            brush = new SolidBrush(c);
        }

        public void addScore()
        {
            score++;
        }

        public int returnScore()
        {
            return score;
        }

        public Point snakePoint(int i)
        {
            return snake[i];
        }

        public List<Point> getList()
        {
            return snake;
        }

        public Boolean checkCollision(Point current, Snake otherSnake)
        {
            //checks if snake leaves walls
            if (current.X < 0 || current.X >= 400 || current.Y < 0 || current.Y >= 400)
            {
                return true;
            }

            //checks collision with itself
            for (int i = 0; i < snake.Count() - 2; i++)
            {
                if (current.X == snake[i].X && current.Y == snake[i].Y)
                {
                    return true;
                }
                //if currenthead.x == p.x and head.y == y
            }

            List<Point> other = otherSnake.getList();

            for (int i = 0; i < other.Count(); i++)
            {
                if (current.X == other[i].X && current.Y == other[i].Y)
                {
                    return true;
                }
            }

            return false;

        }

        public void setDir(String d)
        {
            //changes direction
            dir = d;
        }

        public String getDir()
        {
            return dir;
        }

        public void move(Graphics g)
        {
            Point currentHead = snake[snake.Count - 1];
            if (dir == "u")
            {
                snake.Add(new Point(currentHead.X, currentHead.Y - 10));
            }

            if (dir == "d")
            {
                snake.Add(new Point(currentHead.X, currentHead.Y + 10));
            }

            if (dir == "l")
            {
                snake.Add(new Point(currentHead.X - 10, currentHead.Y));
            }

            if (dir == "r")
            {
                snake.Add(new Point(currentHead.X + 10, currentHead.Y));
            }
        }

        internal void Clear()
        {
            snake.Clear();
        }

        internal void Add(Point point)
        {
            snake.Add(point);
        }

        internal int Count()
        {
            return snake.Count();
        }

        internal void RemoveAt(int p)
        {
            snake.RemoveAt(p);
        }

        internal void draw(Graphics g)
        {
            foreach (Point p in snake)
            {
                g.FillRectangle(brush, p.X, p.Y, 10, 10);
            }
        }
    }
}
