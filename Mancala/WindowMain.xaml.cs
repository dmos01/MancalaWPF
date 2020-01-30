using System.ComponentModel;
using System.Windows;
using Mancala.Game;
using Mancala.Menu;

namespace Mancala
{
    /// <summary>
    /// Interaction logic for WindowMain.xaml
    /// </summary>
    public partial class WindowMain : Window
    {
        readonly PageMenuBoard menuBoard;
        PageGameBoard gameBoard;
        bool inGame;
        BoardSize boardSize;

        public WindowMain()
        {
            InitializeComponent();
            boardSize = new BoardSize(UserSettings.Default.StoneSize);

            menuBoard = new PageMenuBoard(this, boardSize);
            Width = boardSize.AbsoluteWidth;
            Height = boardSize.AbsoluteHeight;
            gameBoard = null;
            inGame = false;
        }

        public void NewGame(Enums.Player whoStarts)
        {
            gameBoard = new PageGameBoard(this, whoStarts, boardSize);
            frameMain.Navigate(gameBoard);
            inGame = true;
        }

        public void Pause()
        {
            menuBoard.ShowRules();
            frameMain.Navigate(menuBoard);
            inGame = false;
        }

        public void Resume()
        {
            if (gameBoard is null)
                return;

            frameMain.Navigate(gameBoard);
            inGame = true;
        }

        public void EndGame()
        {
            menuBoard.GameFinished();
            frameMain.Navigate(menuBoard);
            gameBoard = null;
            inGame = false;
        }

        public void DecreaseSize(bool changeMainWindowSize = true)
        {
            double stoneSize = UserSettings.Default.StoneSize;
            if (stoneSize <= BoardSize.MinStoneSize)
                return;

            stoneSize--;
            boardSize = new BoardSize(stoneSize);
            gameBoard?.ChangeSize(boardSize);
            menuBoard.ChangeSize(boardSize);

            if (changeMainWindowSize)
            {
                Width = boardSize.AbsoluteWidth;
                Height = boardSize.AbsoluteHeight;
            }

            UserSettings.Default.StoneSize = stoneSize;
            UserSettings.Default.Save();
        }

        public void IncreaseSize(bool changeMainWindowSize = true)
        {
            double stoneSize = UserSettings.Default.StoneSize;
            if (stoneSize >= BoardSize.MaxStoneSize)
                return;

            stoneSize++;
            boardSize = new BoardSize(stoneSize);
            gameBoard?.ChangeSize(boardSize);
            menuBoard.ChangeSize(boardSize);

            if (changeMainWindowSize)
            {
                Width = boardSize.AbsoluteWidth;
                Height = boardSize.AbsoluteHeight;
            }

            UserSettings.Default.StoneSize = stoneSize;
            UserSettings.Default.Save();
        }

        public void ResetSize()
        {
            UserSettings.Default.StoneSize = 15;
            boardSize = new BoardSize(15);
            Width = boardSize.AbsoluteWidth;
            Height = boardSize.AbsoluteHeight;
            gameBoard?.ChangeSize(boardSize);
            menuBoard.ChangeSize(boardSize);
        }

        void WindowMain_OnLoaded(object sender, RoutedEventArgs e)
            => frameMain.Navigate(menuBoard);

        void WindowMain_OnClosing(object sender, CancelEventArgs e)
        {
            if (inGame)
            {
                e.Cancel = true;
                EndGame();
            }
        }
    }
}
