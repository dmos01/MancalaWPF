using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mancala.Game;

namespace Mancala.Menu
{
    /// <summary>
    /// Interaction logic for PageMenuBoard.xaml
    /// </summary>
    public partial class PageMenuBoard : Page
    {
        readonly WindowMain parentWindow;
        Enums.Player whoStartsNextAlternating;
        int randomNumber;

        /// <summary>
        /// Setter also navigates to that page.
        /// </summary>
        Page CurrentMenuPage
        {
            get => _currentMenuPage;
            set
            {
                _currentMenuPage = value;
                frameMenu.Navigate(_currentMenuPage);
            }
        }
        Page _currentMenuPage;

        private PageMenuBoard()
        {
            InitializeComponent();
            whoStartsNextAlternating = Enums.Player.Player1;
            CurrentMenuPage = new PageNewGameSettings(whoStartsNextAlternating);
        }

        public PageMenuBoard(WindowMain parentWindow, BoardSize boardSize) : this()
        {
            this.parentWindow = parentWindow;
            ChangeSize(boardSize);
        }

        void btnStartNewGame_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            parentWindow.NewGame(FindWhoStarts());
            btnReturnToGame.IsEnabled = true;
            textBlockReturnToGame.Foreground = Cup.HighlightedColor;
        }

        Enums.Player FindWhoStarts()
        {
            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player1Name)
                return Enums.Player.Player1;
            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player2Name)
                return Enums.Player.Player2;

            switch (UserSettings.Default.WhoStarts)
            {
                case "Player 1":
                case "Player1":
                    return Enums.Player.Player1;
                case "Player 2":
                case "Player2":
                case "Computer":
                    return Enums.Player.Player2;
                case "Alternating":
                    return whoStartsNextAlternating;
                case "Random":
                    Random rand = new Random();
                    randomNumber = rand.Next(2);
                    return randomNumber == 0 ? Enums.Player.Player1 : Enums.Player.Player2;
                default:
                    UserSettings.Default.WhoStarts = UserSettings.Default.Player1Name;
                    UserSettings.Default.Save();
                    return Enums.Player.Player1;
            }
        }

        void btnResetSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you want to reset all settings to factory defaults?", "Reset Settings?",
                    MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                switch (CurrentMenuPage)
                {
                    case PageNewGameSettings ngs:
                        ngs.FocusOnFirstControl();
                        break;
                    case PagePlayerSettings ps:
                        ps.FocusOnFirstControl();
                        break;
                    case PageRules r:
                        r.FocusOnFirstControl();
                        break;
                    case PageSizes s:
                        s.FocusOnFirstControl();
                        break;
                }
                return;
            }

            UserSettings.Default.Reset();
            UserSettings.Default.Save();

            parentWindow.ResetSize();

            //Removing Lost Focus Event Handlers prevents them triggering and overwriting a now-reset value.
            switch (CurrentMenuPage)
            {
                case PageNewGameSettings ngs:
                    ngs.RemoveAllLostFocusHandlers();
                    CurrentMenuPage = new PageNewGameSettings(whoStartsNextAlternating);
                    break;
                case PagePlayerSettings ps:
                    ps.RemoveAllLostFocusHandlers();
                    CurrentMenuPage = new PagePlayerSettings();
                    break;
                case PageSizes _:
                    CurrentMenuPage = new PageSizes(parentWindow);
                    break;
            }
        }

        void BtnNewGameSettings_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            CurrentMenuPage = new PageNewGameSettings(whoStartsNextAlternating);
        }

        void BtnPlayerSettings_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            CurrentMenuPage = new PagePlayerSettings();
        }

        void BtnSizes_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            CurrentMenuPage = new PageSizes(parentWindow);
        }

        void BtnRules_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            CurrentMenuPage = new PageRules();
        }

        void BtnReturnToGame_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TextboxesAreValid() == false)
                return;

            parentWindow.Resume();
        }

        bool TextboxesAreValid()
        {
            switch (CurrentMenuPage)
            {
                case PageNewGameSettings ngs:
                    if (ngs.IfAnyTextboxIsEmptyThenFocus())
                        return false;
                    ngs.SaveTextboxSettings();
                    break;
                case PagePlayerSettings ps:
                    if (ps.IfAnyTextboxIsEmptyThenFocus())
                        return false;
                    ps.SaveTextboxSettings();
                    break;
            }

            return true;
        }

        public void ShowRules() => CurrentMenuPage = new PageRules();

        public void GameFinished()
        {
            whoStartsNextAlternating = FindWhoStartedAndGiveOtherPlayer();
            CurrentMenuPage = new PageNewGameSettings(whoStartsNextAlternating);
            btnReturnToGame.IsEnabled = false;
            textBlockReturnToGame.Foreground = Cup.UnhighlightedColor;
        }

        Enums.Player FindWhoStartedAndGiveOtherPlayer()
        {
            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player1Name)
                return Enums.Player.Player2;
            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player2Name)
                return Enums.Player.Player1;

            switch (UserSettings.Default.WhoStarts)
            {
                case "Player 1":
                case "Player1":
                    return Enums.Player.Player2;
                case "Player 2":
                case "Player2":
                case "Computer":
                    return Enums.Player.Player1;
                case "Alternating":
                    return whoStartsNextAlternating == Enums.Player.Player1 ? Enums.Player.Player2 : Enums.Player.Player1;
                case "Random":
                    return randomNumber == 0 ? Enums.Player.Player2 : Enums.Player.Player1;
                default:
                    UserSettings.Default.WhoStarts = UserSettings.Default.Player1Name;
                    UserSettings.Default.Save();
                    return Enums.Player.Player1;
            }
        }

        public void ChangeSize(BoardSize boardSize)
        {
            btnPlayer1Mancala.Width = boardSize.CupSizeAndMancalaWidth;
            btnPlayer2Mancala.Width = boardSize.CupSizeAndMancalaWidth;

            Label[] cups = MenuCups;
            Array.ForEach(cups, x => x.Width = boardSize.CupSizeAndMancalaWidth);
            Array.ForEach(cups, x => x.Height = boardSize.CupSizeAndMancalaWidth);

            dockPanelExclMancalas.Width = boardSize.PlayerSideWidth;
            frameMenu.Margin = boardSize.CentreFrameMargin;
            btnPlayer1Mancala.Margin = boardSize.RightMargin;
            dockPanelExclMancalas.Margin = boardSize.RightMargin;

        }

        Label[] MenuCups => new[]
        {
            btnNewGameSettings,
            btnPlayerSettings,
            btnPlayer2Cup2,
            btnSizes,
            btnRules,
            btnReturnToGame,

            btnPlayer1Cup0,
            btnPlayer1Cup1,
            btnStartNewGame,
            btnResetSettings,
            btnPlayer1Cup4,
            btnPlayer1Cup5
        };
    }
}
