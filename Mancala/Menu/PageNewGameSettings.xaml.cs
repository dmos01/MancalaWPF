using System.Windows;
using System.Windows.Controls;

namespace Mancala.Menu
{
    /// <summary>
    /// Interaction logic for PageNewGameSettings.xaml
    /// </summary>
    public partial class PageNewGameSettings : Page
    {
        private PageNewGameSettings() => InitializeComponent();

        public PageNewGameSettings(Enums.Player whoStartsNextAlternating) : this()
        {
            InitialiseWhoStartsCombobox(whoStartsNextAlternating);

            if (UserSettings.Default.Capturing)
                chkCapturing.IsChecked = true;

            if (UserSettings.Default.MultipleLaps)
                chkMultipleLaps.IsChecked = true;

            txtStartingStones.Text = UserSettings.Default.NumStartingStones.ToString();
            cboWhenSideEmpty.SelectedIndex = UserSettings.Default.PlayerWithEmptySideGetsRemainingStones ? 0 : 1;

            AddAllEventHandlers();
            cboWhoStarts.Focus();
        }

        void InitialiseWhoStartsCombobox(Enums.Player whoStartsNextAlternating)
        {
            cboWhoStarts.Items[0] = UserSettings.Default.Player1Name;
            cboWhoStarts.Items[1] = UserSettings.Default.Player2Name;

            cboWhoStarts.Items[2] = whoStartsNextAlternating == Enums.Player.Player1
                ? "Alternating (" + UserSettings.Default.Player1Name + " next)"
                : "Alternating (" + UserSettings.Default.Player2Name + " next)";

            cboWhoStarts.Items[3] = "Random";

            if (UserSettings.Default.WhoStarts == UserSettings.Default.Player1Name)
                cboWhoStarts.SelectedIndex = 0;
            else if (UserSettings.Default.WhoStarts == UserSettings.Default.Player2Name)
                cboWhoStarts.SelectedIndex = 1;
            else
            {
                switch (UserSettings.Default.WhoStarts)
                {
                    case "Player 1":
                        cboWhoStarts.SelectedIndex = 0;
                        break;
                    case "Computer":
                        cboWhoStarts.SelectedIndex = 1;
                        break;
                    case "Alternating":
                        cboWhoStarts.SelectedIndex = 2;
                        break;
                    case "Random":
                        cboWhoStarts.SelectedIndex = 3;
                        break;
                    default:
                        UserSettings.Default.WhoStarts = UserSettings.Default.Player1Name;
                        UserSettings.Default.Save();
                        cboWhoStarts.SelectedIndex = 0;
                        break;
                }
            }
        }

        void AddAllEventHandlers()
        {
            cboWhoStarts.SelectionChanged += cboWhoStarts_SelectionChanged;
            chkCapturing.Checked += chkCapturing_Checked;
            chkMultipleLaps.Checked += chkMultipleLaps_Checked;
            chkCapturing.Unchecked += chkCapturing_Unchecked;
            chkMultipleLaps.Unchecked += chkMultipleLaps_Unchecked;
            txtStartingStones.TextChanged += txtStartingStones_TextChanged;
            txtStartingStones.LostFocus += txtStartingStones_OnLostFocus;
            cboWhenSideEmpty.SelectionChanged += cboWhenSideEmpty_SelectionChanged;
        }

        void cboWhoStarts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserSettings.Default.WhoStarts = cboWhoStarts.SelectedIndex == 2
                ? "Alternating" : cboWhoStarts.SelectedItem.ToString();
            UserSettings.Default.Save();
        }

        void chkCapturing_Checked(object sender, RoutedEventArgs e)
        {
            chkMultipleLaps.IsChecked = false;
            UserSettings.Default.Capturing = true;
            UserSettings.Default.Save();
        }

        void chkMultipleLaps_Checked(object sender, RoutedEventArgs e)
        {
            chkCapturing.IsChecked = false;
            UserSettings.Default.MultipleLaps = true;
            UserSettings.Default.Save();
        }

        void chkCapturing_Unchecked(object sender, RoutedEventArgs e)
        {
            UserSettings.Default.Capturing = false;
            UserSettings.Default.Save();
        }

        void chkMultipleLaps_Unchecked(object sender, RoutedEventArgs e)
        {
            UserSettings.Default.MultipleLaps = false;
            UserSettings.Default.Save();
        }

        void txtStartingStones_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (byte.TryParse(txtStartingStones.Text, out byte number))
            {
                if (number < 1)
                    txtStartingStones.Text = "1";
                else if (number > 20)
                    txtStartingStones.Text = "20";
            }
            else
                txtStartingStones.Text = "";
        }

        void txtStartingStones_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSettings.Default.NumStartingStones =
                byte.TryParse(txtStartingStones.Text, out byte number) ? number : (byte)4;
            UserSettings.Default.Save();
        }

        public void SaveTextboxSettings() => txtStartingStones_OnLostFocus(this, null);

        public void RemoveAllLostFocusHandlers() => txtStartingStones.LostFocus -= txtStartingStones_OnLostFocus;

        public bool IfAnyTextboxIsEmptyThenFocus()
        {
            if (txtStartingStones.Text != "")
                return false;

            MessageBox.Show("Enter the number of starting stones per cup.", "Number of Starting Stones Missing");
            txtStartingStones.Focus();
            return true;
        }

        public void FocusOnFirstControl() => cboWhoStarts.Focus();

        void cboWhenSideEmpty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserSettings.Default.PlayerWithEmptySideGetsRemainingStones = cboWhenSideEmpty.SelectedIndex == 0;
            UserSettings.Default.Save();
        }
    }
}
