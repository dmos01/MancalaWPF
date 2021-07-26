using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mancala.Menu
{
    /// <summary>
    ///     Interaction logic for PageMenuBoard.xaml
    /// </summary>
    public partial class PageMenuBoard
    {
        readonly WindowMain parentWindow;
        Page _currentMenuPage;
        Enums.Player playerToStart;
        int randomNumber;

        /// <summary>
        ///     Setter also navigates to that page.
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

        Label[] MenuCups => new[]
        {
            btnNewGameSettings,
            btnPlayerSettings,
            btnPlayer2Cup2,
            btnSizeSettings,
            btnRules,
            btnReturnToGame,

            btnPlayer1Cup0,
            btnPlayer1Cup1,
            btnStartNewGame,
            btnResetSettings,
            btnPlayer1Cup4,
            btnPlayer1Cup5
        };

        private PageMenuBoard()
        {
            InitializeComponent();
            playerToStart = Enums.Player.Player1;
            CurrentMenuPage = new PageNewGameSettings(playerToStart);

            SolidColorBrush background = ColorScheme.CurrentColorScheme.CupBackgroundColor;
            SolidColorBrush text = ColorScheme.CurrentColorScheme.EnabledStoneColor;
            SolidColorBrush border = ColorScheme.CurrentColorScheme.CupBorderColor;
            Thickness borderThickness = ColorScheme.CurrentColorScheme.CupBorderThickness;

            btnNewGameSettings.Background = background;
            textBlockNewGameSettings.Foreground = text;
            btnNewGameSettings.BorderBrush = border;
            btnNewGameSettings.BorderThickness = borderThickness;

            btnPlayerSettings.Background = background;
            textBlockPlayerSettings.Foreground = text;
            btnPlayerSettings.BorderBrush = border;
            btnPlayerSettings.BorderThickness = borderThickness;

            btnSizeSettings.Background = background;
            textBlockSizeSettings.Foreground = text;
            btnSizeSettings.BorderBrush = border;
            btnSizeSettings.BorderThickness = borderThickness;

            btnPlayer2Cup2.Background = background;
            btnPlayer2Cup2.BorderBrush = border;
            btnPlayer2Cup2.BorderThickness = borderThickness;

            btnRules.Background = background;
            textBlockRules.Foreground = text;
            btnRules.BorderBrush = border;
            btnRules.BorderThickness = borderThickness;

            btnReturnToGame.Background = background;
            textBlockReturnToGame.Foreground = ColorScheme.CurrentColorScheme.DisabledStoneColor;
            btnReturnToGame.BorderBrush = border;
            btnReturnToGame.BorderThickness = borderThickness;

            btnPlayer1Cup0.Background = background;
            btnPlayer1Cup0.BorderBrush = border;
            btnPlayer1Cup0.BorderThickness = borderThickness;

            btnPlayer1Cup1.Background = background;
            btnPlayer1Cup1.BorderBrush = border;
            btnPlayer1Cup1.BorderThickness = borderThickness;

            btnStartNewGame.Background = background;
            textBlockStartNewGame.Foreground = text;
            btnStartNewGame.BorderBrush = border;
            btnStartNewGame.BorderThickness = borderThickness;

            btnResetSettings.Background = background;
            textBlockResetSettings.Foreground = text;
            btnResetSettings.BorderBrush = border;
            btnResetSettings.BorderThickness = borderThickness;

            btnPlayer1Cup4.Background = background;
            btnPlayer1Cup4.BorderBrush = border;
            btnPlayer1Cup4.BorderThickness = borderThickness;

            btnPlayer1Cup5.Background = background;
            btnPlayer1Cup5.BorderBrush = border;
            btnPlayer1Cup5.BorderThickness = borderThickness;

            btnPlayer1Mancala.Background = background;
            btnPlayer1Mancala.BorderBrush = border;
            btnPlayer1Mancala.BorderThickness = borderThickness;

            btnPlayer2Mancala.Background = background;
            btnPlayer2Mancala.BorderBrush = border;
            btnPlayer2Mancala.BorderThickness = borderThickness;
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

            SetPlayerToStart();
            parentWindow.NewGame(playerToStart);
            btnReturnToGame.IsEnabled = true;
            textBlockReturnToGame.Foreground = ColorScheme.CurrentColorScheme.EnabledStoneColor;
        }

        void SetPlayerToStart()
        {
            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player1Name)
                playerToStart = Enums.Player.Player1;
            else if (UserSettings.Default.WhoStarts == UserSettings.Default.Player2Name)
                playerToStart = Enums.Player.Player2;

            else if (UserSettings.Default.WhoStarts == MancalaResources.Player2HumanDefaultName ||
                     UserSettings.Default.WhoStarts == MancalaResources.Player2ComputerDefaultName)
            {
                playerToStart = Enums.Player.Player2;
            }
            else if (UserSettings.Default.WhoStarts.Equals(MancalaResources.Alternating,
                StringComparison.OrdinalIgnoreCase)) { }
            else if (UserSettings.Default.WhoStarts.Equals(MancalaResources.Random,
                StringComparison.OrdinalIgnoreCase))
            {
                Random rand = new();
                randomNumber = rand.Next(2);
                playerToStart = randomNumber == 0 ? Enums.Player.Player1 : Enums.Player.Player2;
            }
            else
            {
                UserSettings.Default.WhoStarts = UserSettings.Default.Player1Name;
                UserSettings.Default.Save();
                playerToStart = Enums.Player.Player1;
            }
        }

        void btnResetSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show(MessageResources.ResetQuestionMessage, MessageResources.ResetQuestionTitle,
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
                    case PageRulesAndAbout r:
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
                    CurrentMenuPage = new PageNewGameSettings(playerToStart);
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

            CurrentMenuPage = new PageNewGameSettings(playerToStart);
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

            CurrentMenuPage = new PageRulesAndAbout();
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

        public void ShowRules() => CurrentMenuPage = new PageRulesAndAbout();

        public void GameFinished()
        {
            playerToStart = playerToStart == Enums.Player.Player1 ? Enums.Player.Player2 : Enums.Player.Player1;
            CurrentMenuPage = new PageNewGameSettings(playerToStart);
            btnReturnToGame.IsEnabled = false;
            textBlockReturnToGame.Foreground = ColorScheme.CurrentColorScheme.DisabledStoneColor;
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
    }
}