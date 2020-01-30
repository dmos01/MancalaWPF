using System.Windows;
using System.Windows.Controls;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Mancala.Menu
{
    /// <summary>
    /// Interaction logic for PageSizes.xaml
    /// </summary>
    public partial class PageSizes : Page
    {
        readonly WindowMain parentWindow;

        private PageSizes() => InitializeComponent();

        public PageSizes(WindowMain mainWindow) : this()
        {
            parentWindow = mainWindow;

            double stoneSize = UserSettings.Default.StoneSize;
            if (stoneSize >= BoardSize.MaxStoneSize)
                btnIncreaseSize.IsEnabled = false;
            else if (stoneSize <= BoardSize.MinStoneSize)
                btnDecreaseSize.IsEnabled = false;

        }

        void BtnIncreaseSize_OnClick(object sender, RoutedEventArgs e)
        {
            double stoneSize = UserSettings.Default.StoneSize;
            if (stoneSize >= (BoardSize.MaxStoneSize - 1))
                btnIncreaseSize.IsEnabled = false;
            btnDecreaseSize.IsEnabled = true;
            parentWindow.IncreaseSize();
        }

        void BtnDecreaseSize_OnClick(object sender, RoutedEventArgs e)
        {
            double stoneSize = UserSettings.Default.StoneSize;
            if (stoneSize <= (BoardSize.MinStoneSize + 1))
                btnDecreaseSize.IsEnabled = false;
            btnIncreaseSize.IsEnabled = true;
            parentWindow.DecreaseSize();
        }

        public void FocusOnFirstControl()
        {
            if (btnIncreaseSize.IsEnabled)
                btnIncreaseSize.Focus();
            else
                btnDecreaseSize.Focus();
        }
    }
}
