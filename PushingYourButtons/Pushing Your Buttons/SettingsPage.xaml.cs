using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Pushing_Your_Buttons
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    /// 
    public sealed partial class SettingsPage : Page
    {


        // Probably can retrieve this from mainpage.xaml.cs instead
       
        private MainPage.ValidGameMode tempGameModeStore ;

        public SettingsPage()
        {
            this.InitializeComponent();
            updateDisplayedBox();

        }

        private void updateDisplayedBox()
        {

            tempGameModeStore = MainPage.GetGameModeFromLocalSettings();

            switch (tempGameModeStore)
            {
                case MainPage.ValidGameMode.Challenge:
                    RadioButton_Challenge.IsChecked = true;
                    break;
                case MainPage.ValidGameMode.Zen:
                    RadioButton_Zen.IsChecked = true;
                    break;
                case MainPage.ValidGameMode.Timed:
                    RadioButton_Timed.IsChecked = true;
                    break;
                default:
                    throw new Exception("Huh? Game mode should have been set before this.");
            }
        }

        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (MainPage));

        }

        private void gameModeChange(MainPage.ValidGameMode mode)
        {
            tempGameModeStore = mode;
            MainPage.updateGameMode(mode);
        }

        private void RadioButton_Zen_Checked(object sender, RoutedEventArgs e)
        {
            gameModeChange(MainPage.ValidGameMode.Zen);
        }

        private void RadioButton_Timed_Checked(object sender, RoutedEventArgs e)
        {
            gameModeChange(MainPage.ValidGameMode.Timed);
        }

        private void RadioButton_Challenge_Checked(object sender, RoutedEventArgs e)
        {
            gameModeChange(MainPage.ValidGameMode.Challenge);
        }
    }
}
