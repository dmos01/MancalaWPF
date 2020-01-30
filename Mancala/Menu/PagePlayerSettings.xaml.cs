using System.Windows;
using System.Windows.Controls;

namespace Mancala.Menu
{
    /// <summary>
    /// Interaction logic for PagePlayerSettings.xaml
    /// </summary>
    public partial class PagePlayerSettings : Page
    {
        public const byte MaxNameLength = 25;

        public PagePlayerSettings()
        {
            InitializeComponent();
            lblPlayer2IsLabel.Width = lblPlayer1NameLabel.Width;

            txtPlayer1Name.Text = UserSettings.Default.Player1Name;
            txtPlayer2Name.Text = UserSettings.Default.Player2Name;

            if (System.Enum.TryParse(UserSettings.Default.Player2Type, out Enums.PlayerType player2) == false)
            {
                player2 = Enums.PlayerType.Normal;
                UserSettings.Default.Player2Type = player2.ToString();
                UserSettings.Default.Save();
            }

            sliderPlayer2Is.Value = (double)player2;
            sliderPlayer2Is.ValueChanged += SliderPlayer2Is_OnValueChanged;
            txtPlayer1Name.Focus();
        }

        public bool IfAnyTextboxIsEmptyThenFocus()
        {
            if (txtPlayer1Name.Text == "")
            {
                MessageBox.Show("Enter a name for player 1.", "Player 1 Name Missing");
                txtPlayer1Name.Focus();
                return true;
            }

            if (txtPlayer2Name.Text == "")
            {
                MessageBox.Show("Enter a name for player 2.", "Player 2 Name Missing");
                txtPlayer2Name.Focus();
                return true;
            }

            return false;
        }

        public void FocusOnFirstControl() => txtPlayer1Name.Focus();

        void txtPlayer1Name_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSettings.Default.Player1Name = txtPlayer1Name.Text;
            UserSettings.Default.Save();
        }

        void txtPlayer2Name_OnLostFocus(object sender, RoutedEventArgs e)
        {
            UserSettings.Default.Player2Name = txtPlayer2Name.Text;
            UserSettings.Default.Save();
        }

        public void SaveTextboxSettings()
        {
            UserSettings.Default.Player1Name = txtPlayer1Name.Text;
            UserSettings.Default.Player2Name = txtPlayer2Name.Text;
            UserSettings.Default.Save();
        }

        public void RemoveAllLostFocusHandlers()
        {
            txtPlayer1Name.LostFocus -= txtPlayer1Name_OnLostFocus;
            txtPlayer2Name.LostFocus -= txtPlayer2Name_OnLostFocus;
        }

        void SliderPlayer2Is_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderPlayer2Is.Value == 0)
            {
                if (txtPlayer2Name.Text == "Computer")
                {
                    txtPlayer2Name.Text = "Player 2";
                    UserSettings.Default.Player2Name = "Player 2";
                    UserSettings.Default.Save();
                }
            }
            else if (txtPlayer2Name.Text == "Player 2")
            {
                txtPlayer2Name.Text = "Computer";
                UserSettings.Default.Player2Name = "Computer";
                UserSettings.Default.Save();
            }

            UserSettings.Default.Player2Type = ((Enums.PlayerType)sliderPlayer2Is.Value).ToString();
            UserSettings.Default.Save();
        }
    }
}
