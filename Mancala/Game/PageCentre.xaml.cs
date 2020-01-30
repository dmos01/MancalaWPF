using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mancala.Game
{
    /// <summary>
    /// Interaction logic for PageCentre.xaml
    /// </summary>
    public partial class PageCentre : Page
    {
        string player1TurnText;
        string player2TurnText;
        readonly WindowMain parentWindow;

        private PageCentre() => InitializeComponent();

        public PageCentre(WindowMain mainWindow, Enums.PlayerType player2Type) : this()
        {
            parentWindow = mainWindow;
            SetText();
            SetAnimation();
            SetPlayerNames(player2Type);
        }

        void SetText()
        {
            string remainingStonesText = "When one player's side is empty,\r\n";
            remainingStonesText += UserSettings.Default.PlayerWithEmptySideGetsRemainingStones
                ? "they get the remaining stones."
                : "the other gets the remaining stones.";
            textBlockWhoGetsRemainingStones.Text = remainingStonesText;

            if (UserSettings.Default.Capturing && UserSettings.Default.MultipleLaps)
            {
                lblAdditionalRulesCheck.Visibility = Visibility.Visible;
                lblAdditionalRules.Visibility = Visibility.Visible;
                lblAdditionalRules.Content = "Capturing and multiple laps.";
            }
            else if (UserSettings.Default.Capturing)
            {
                lblAdditionalRulesCheck.Visibility = Visibility.Visible;
                lblAdditionalRules.Visibility = Visibility.Visible;
                lblAdditionalRules.Content = "Capturing.";
            }
            else if (UserSettings.Default.MultipleLaps)
            {
                lblAdditionalRulesCheck.Visibility = Visibility.Visible;
                lblAdditionalRules.Visibility = Visibility.Visible;
                lblAdditionalRules.Content = "Multiple laps.";
            }
            else
            {
                lblAdditionalRulesCheck.Visibility = Visibility.Hidden;
                lblAdditionalRules.Visibility = Visibility.Hidden;
            }
        }


        //In PageCentre because PageCentre is the lowest page to be created.
        void SetAnimation()
        {
            Animation.HumanTimeSpan = UserSettings.Default.HumanAnimationSpeed;
            Animation.ComputerTimeSpan = UserSettings.Default.ComputerAnimationSpeed;
            Animation.ComputerTimeSpanTwice = Animation.ComputerTimeSpan.Add(Animation.ComputerTimeSpan);

            sliderHumanAnimationSpeed.Value = GetAnimationSliderValueFromTimeSpan(Animation.HumanTimeSpan);
            sliderComputerAnimationSpeed.Value = GetAnimationSliderValueFromTimeSpan(Animation.ComputerTimeSpan);

            sliderHumanAnimationSpeed.ValueChanged += SliderHumanAnimationSpeed_ValueChanged;
            sliderComputerAnimationSpeed.ValueChanged += SliderComputerAnimationSpeed_ValueChanged;
        }

        void SetPlayerNames(Enums.PlayerType player2Type)
        {
            string player1Name = UserSettings.Default.Player1Name;
            player1TurnText = player1Name.EndsWith("s")
                ? player1Name + "' turn."
                : player1Name + "'s turn.";

            string player2Name = UserSettings.Default.Player2Name;
            player2TurnText = player2Name.EndsWith("s")
                  ? player2Name + "' turn."
                  : player2Name + "'s turn.";

            string player2NamePlusDescription;
            switch (player2Type)
            {
                case Enums.PlayerType.Human:
                    player2NamePlusDescription = player2Name + ".";
                    break;
                case Enums.PlayerType.VeryHard:
                    player2NamePlusDescription = player2Name + " (Very Hard).";
                    break;
                default:
                    player2NamePlusDescription = player2Name + " (" + player2Type + ").";
                    break;
            }

            textBlockPlayerNames.Text = player1Name + " vs. " + player2NamePlusDescription;
        }

        void BtnOpenMenu_OnClick(object sender, RoutedEventArgs e)
        {
            //parentWindow.IncreaseSize();
            parentWindow.Pause();
        }

        void LblEmptyPlayerGetsRemainingStones_OnMouseDown(object sender, MouseButtonEventArgs e) =>
            parentWindow.Pause();

        void LblAdditionalRules_OnMouseDown(object sender, MouseButtonEventArgs e) => parentWindow.Pause();

        void SliderHumanAnimationSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan speed = GetAnimationTimeSpanFromSliderValue((byte)sliderHumanAnimationSpeed.Value);
            UserSettings.Default.HumanAnimationSpeed = speed;
            UserSettings.Default.Save();
            Animation.HumanTimeSpan = speed;
        }

        void SliderComputerAnimationSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            TimeSpan speed = GetAnimationTimeSpanFromSliderValue((byte)sliderComputerAnimationSpeed.Value);
            UserSettings.Default.ComputerAnimationSpeed = speed;
            UserSettings.Default.Save();
            Animation.ComputerTimeSpan = speed;
            Animation.ComputerTimeSpanTwice = speed.Add(speed);
        }

        TimeSpan GetAnimationTimeSpanFromSliderValue(byte value)
        {
            switch (value)
            {
                case 0:
                    return new TimeSpan(0, 0, 0, 0, 450);
                case 1:
                    return new TimeSpan(0, 0, 0, 0, 350);
                case 2:
                    return new TimeSpan(0, 0, 0, 0, 250);
                case 3:
                    return new TimeSpan(0, 0, 0, 0, 150);
                case 4:
                    return TimeSpan.Zero;
                default:
                    return new TimeSpan(0, 0, 0, 0, 250);
            }
        }

        byte GetAnimationSliderValueFromTimeSpan(TimeSpan timeSpan)
        {
            switch (timeSpan.Milliseconds)
            {
                case 450:
                    return 0;
                case 350:
                    return 1;
                case 250:
                    return 2;
                case 150:
                    return 3;
                case 0:
                    return 4;
                default:
                    return 2;
            }
        }

        public void ChangePlayerTurnText(Enums.Player whoseTurn) =>
            lblWhoseTurn.Content = whoseTurn == Enums.Player.Player1 ? player1TurnText : player2TurnText;

        public void EnableControls()
        {
            sliderHumanAnimationSpeed.IsEnabled = true;
            sliderComputerAnimationSpeed.IsEnabled = true;
            btnOpenMenu.Focus();
        }

        public void DisableControls()
        {
            sliderHumanAnimationSpeed.IsEnabled = false;
            sliderComputerAnimationSpeed.IsEnabled = false;
        }
    }
}
