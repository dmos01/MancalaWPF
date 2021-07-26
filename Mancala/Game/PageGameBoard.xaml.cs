using System;
using System.Windows;

namespace Mancala.Game
{
    /// <summary>
    ///     Interaction logic for PageGameBoard.xaml
    /// </summary>
    public partial class PageGameBoard
    {
        readonly PageGameCentre gameCentrePageGame;
        readonly WindowMain parentWindow;

        readonly Enums.PlayerType player2Type;


        Enums.Player currentPlayer;

        bool capturing { get; } = UserSettings.Default.Capturing;

        bool multipleLaps { get; } = UserSettings.Default.MultipleLaps;

        bool emptyPlayerGetsRemainingStones { get; } = UserSettings.Default.PlayerWithEmptySideGetsRemainingStones;

        string player1Name { get; } = UserSettings.Default.Player1Name;

        string player2Name { get; } = UserSettings.Default.Player2Name;

        private PageGameBoard()
        {
            InitializeComponent();
            byte startingStones = UserSettings.Default.NumStartingStones;
            InitialiseCupsAndMancalas(startingStones);
        }

        public PageGameBoard(WindowMain mainWindow, Enums.Player whoStarts, BoardSize boardSize) : this()
        {
            if (Enum.TryParse(UserSettings.Default.Player2Type, out player2Type) == false)
            {
                UserSettings.Default.Player2Type = Enums.PlayerType.Normal.ToString();
                UserSettings.Default.Save();
                player2Type = Enums.PlayerType.Normal;
            }

            parentWindow = mainWindow;
            gameCentrePageGame = new PageGameCentre(mainWindow, player2Type);
            if (boardSize != null)
                ChangeSize(boardSize);
            currentPlayer = whoStarts;
            StartNewTurnAndUnhighlightPreviousChosenCupIfApplicable(false);
        }

        void PageBoard_OnLoaded(object sender, RoutedEventArgs e) => frameCentre.Navigate(gameCentrePageGame);
    }
}