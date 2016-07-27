using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using System.Xml.Serialization;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pushing_Your_Buttons
{
    public sealed partial class MainPage : Page
    {
        /* Game Description
        Pushing Your Buttons
        Alex McKirdy
        2015, made for Microsoft Student Accelerator

            Premise of the game - press the button as many times as possible, with as few errors as possible

            Modes:
                0 - Zen - Untimed, just keep pressing the button
                1 - Timed - 60 seconds, many presses
                2 - Challenge - Game over at 60 seconds, or when a red one is hit

            
                TODO - Store High score
                DONE - Allow user to select the mode
                DONE - Timers
                TODO - Add sound
                TODO (low priority) - Change the amount of rows and columns dynamically (for different difficulty)
           */



        //////////////////////////////////////////////////////////////////////////////////
        #region Variables
        // resharper adds "private" to these variables, no point in arguing with it
        // static needed for settingpage.xaml.cs to work


        public static ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings; //chur (Jay's) MSA weather app
        
        // http://stackoverflow.com/questions/20407410/proper-way-to-limit-possible-string-argument-values-and-see-them-in-intellisense
        // enum most appropriate data type for game mode

        public enum ValidGameMode
        {       
            Zen,
            Timed,
            Challenge
        }

        private static ValidGameMode selected_game_mode = ValidGameMode.Challenge;   // game mode must be chosen from enum above - but it defaults to first one
        // saves having to retrieve it from localsettings unless needed

        private Random rand = new Random();         // RNG, used throughout

        private TimeSpan defaultTime = TimeSpan.FromSeconds(45);               // To set duration of a new game - typically 45 seconds
        private TimeSpan remainingTime;             // decremented when game is running, must be set first

        private bool gameStarted;                   // so button does something different on first click of game
        private bool game_set_up;                   // first tap after the end of a game will set up the next game - not go straight to a new one (set to true at end of newGame())

        private bool currentButtonIsRed = false;    // needed for challenge mode
        private bool enableRotation = true;         // used for rotation (will probably keep true, as it makes the app more entertaining)
        private bool soundEnabled = true;

        private int rows;                           // these two likely to remain the same throughout,
        private int columns;                        // but allow extra / fewer rows with little modification
        private int score;                          // current score

        //timers
        private DispatcherTimer redButtonTimer = new DispatcherTimer(); //Time until a red square disappears (800ms?) 
        private DispatcherTimer Timer_100ms = new DispatcherTimer();
        //For updating time display (100ms) and calculating remaining time

        //TODO - Add highscores
        #endregion
        //////////////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////////////
        #region Event Handlers
        public MainPage()
        {
            this.InitializeComponent();

            // set initial timers
            redButtonTimer.Interval = TimeSpan.FromMilliseconds(800);
            Timer_100ms.Interval = TimeSpan.FromMilliseconds(100);

            //and do stuff when they are successfully ticked
            redButtonTimer.Tick += new EventHandler<object>(TimerForRedButtonChallenge);
            Timer_100ms.Tick += new EventHandler<object>(Timer_100ms_Tick);

            newGame();
        }
        private void TimerForRedButtonChallenge(object sender, object e)
        {
            //should only occur when challenge makes a red button show
            //doesn't tick on start
            challenge_redButtonFinished();
        }

        private void command_ber_btn_settings_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SettingsPage));
            //TODO - need to find out how to pass the game mode back and forth
        }

        private void command_bar_btn_newgame_Click(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        private void GameButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonHit();
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////

        public static ValidGameMode GetGameModeFromLocalSettings()
        {
            // this shouldn't be necessary, but going straight from the enum was causing grief
            // static needed for settingpage.xaml.cs to work

            var tmpValue = localSettings.Values["selectedGameMode"];
            ValidGameMode gm;
            if (tmpValue == null)
            {
                // set a default value (will happen on first run)
                localSettings.Values["selectedGameMode"] = 0;
                selected_game_mode = ValidGameMode.Zen;
                return ValidGameMode.Zen;
            }
            switch ((int) tmpValue) 
            {
                case 0:
                    gm = ValidGameMode.Zen;
                    break;
                case 1:
                    gm = ValidGameMode.Timed;
                    break;
                case 2:
                    gm = ValidGameMode.Challenge;
                    break;
                default:
                    gm = ValidGameMode.Zen;
                    break;
            }

            return gm;
        }

        public static void updateGameMode(ValidGameMode gm)
        {
            // set the private variable first
            selected_game_mode = gm;

            //then update the localSettings
            switch (gm)
            {
                case ValidGameMode.Zen:
                    localSettings.Values["selectedGameMode"] = 0;
                    break;
                case ValidGameMode.Timed:
                    localSettings.Values["selectedGameMode"] = 1;
                    break;
                case ValidGameMode.Challenge:
                    localSettings.Values["selectedGameMode"] = 2;
                    break;
                default:
                    throw new Exception("Game mode not implemented");
            }
        }
        private void newGame()
        {
            selected_game_mode = GetGameModeFromLocalSettings();
            //reset in prep for new game
            score = 0;
            remainingTime = defaultTime;
            currentButtonIsRed = false;

            //Start timer on first button press
            Timer_100ms.Stop();
            gameStarted = false;

            //while these are likely to remain the same, this allows extra / fewer rows later on
            rows = GameGrid.RowDefinitions.Count - 1; //bottom row is to hide command bar, top bar holds timer
            columns = GameGrid.ColumnDefinitions.Count;

            //Makes the button appear in the same beginnning point each game
            Grid.SetRow(GameButton, 3);
            Grid.SetColumn(GameButton, 1);
            GameButton.Margin = new Thickness(0);

            GameButton.Foreground = new SolidColorBrush(Colors.Black); //text colour
            GameButtonRotation.Rotation = 0;
            GameButton.Content = "";
            // set colour depending on game type:
            switch (selected_game_mode)
            {
                case ValidGameMode.Zen:
                    GameButton.Background = new SolidColorBrush(Colors.Cyan);
                    resetTimeLabel(showTime: false);
                    break;
                case ValidGameMode.Timed:
                    GameButton.Background = new SolidColorBrush(Colors.GreenYellow);
                    //GameButton.Content = "60.0";
                    resetTimeLabel();
                    break;
                case ValidGameMode.Challenge:
                    GameButton.Background = new SolidColorBrush(Colors.White);
                    //GameButton.Content = "60.0";
                    resetTimeLabel();
                    break;
                default:
                    throw new Exception("Game mode not set - can not create a new game");
            }

            // Game has been set up correctly - allow the user to play
            game_set_up = true;
        }

        private async void gameOver()
        {
            //stop timers
            Timer_100ms.Stop();
            redButtonTimer.Stop();
            game_set_up = false;

            var dialog = new MessageDialog("You scored {}".Replace("{}", score.ToString()));
            if (currentButtonIsRed)
            {
                //if the game ended because the player hit red, be meaner to them
                dialog.Title = "Game Over";
            }
            else
            {
                dialog.Title = "Congratulations";
            }
            await dialog.ShowAsync();
        }

        private void resetTimeLabel(bool showTime = true)
        {
            LabelSeconds.Text = (defaultTime.TotalMilliseconds / 1000).ToString("f1") + "s";
            if (showTime)  {LabelSeconds.Visibility = Visibility.Visible;}
            else           {LabelSeconds.Visibility = Visibility.Collapsed;}
        }

        private void Timer_100ms_Tick(object sender, object e)
        {
            if (remainingTime == TimeSpan.FromSeconds(0))
            {
                // if no time remaining, end game
                GameButton.Content = score.ToString();
                //LabelSeconds.Text = "0.00s";
                gameOver(); //stops timers inside the function
            }
            else
            {
                //Reduce Timer
                remainingTime -= TimeSpan.FromMilliseconds(100);
            } 


            //update time display
            if (selected_game_mode != ValidGameMode.Zen)
            {
                LabelSeconds.Text = (remainingTime.TotalMilliseconds/1000).ToString("f1") + "s";
            }
        }


        private void moveButton()
        {
            //place the button in a random position
            Grid.SetRow(GameButton, rand.Next(1, rows)); //as top row is used for timer display
            Grid.SetColumn(GameButton, rand.Next(0, columns));
            if (enableRotation) { GameButtonRotation.Rotation = rand.Next(-45, 45); }
        }

        private void randomButtonColour()
        {
            //randomise colour - colours less likely to be reddish
            Color randomColor = Color.FromArgb(255, Convert.ToByte(rand.Next(0, 255)),
                Convert.ToByte(rand.Next(100, 255)), Convert.ToByte(rand.Next(100, 255)));
            GameButton.Background = new SolidColorBrush(randomColor);
        }

        //////////////////////////////////////////////////////////////////////////////////
        #region Gameplay specific to each game mode
        private void challenge_game()
        {

            // if the user presses a red button during challenge mode
            // currentButtonIsRed is set to false once the red timer has finished
            if (currentButtonIsRed)
            { 
                gameOver();
                return;
            }

            //20 percent chance of red
            if (rand.Next(0, 10) < 2)
            {
                Timer_100ms.Stop();
                moveButton();
                GameButton.Background = new SolidColorBrush(Colors.Red);
                GameButton.Content = "Wait!";
                currentButtonIsRed = true;
                redButtonTimer.Start();
            }
            moveButton();
            score++;
        }

        private void challenge_redButtonFinished()
        {
            redButtonTimer.Stop();
            moveButton();
            GameButton.Background = new SolidColorBrush(Colors.White);
            GameButton.Content = "";
            currentButtonIsRed = false;
            Timer_100ms.Start();
        }

        private void timed_game()
        {
            moveButton();
            score++;
            randomButtonColour();
        }

        private void zen_game()
        {
            moveButton();
            score++;                                    //add to count
            randomButtonColour();
            GameButton.Content = score.ToString();      //place the current score on the button
        }
        #endregion
        //////////////////////////////////////////////////////////////////////////////////
        
        private void ButtonHit()
        {
            // Check that the button is set up for a new game
            if (!game_set_up)
            {
                newGame();
                return;
            }

            // after game is set up, start the timer on first press (except in a zen game)
            if (selected_game_mode != ValidGameMode.Zen && !gameStarted)
            {
                Timer_100ms.Start();
            }
            gameStarted = true;


            //Choose the appropriate game mode on button hit
            switch (selected_game_mode)
            {
                case ValidGameMode.Zen:         zen_game();             break;
                case ValidGameMode.Challenge:   challenge_game();       break;
                case ValidGameMode.Timed:       timed_game();           break;
                default: throw new Exception("Game mode invalid, or not set");
                // default only if mode not set, (zen should be set as game mode on start)
            }
        }


    }
}
