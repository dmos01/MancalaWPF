using System;
using System.Windows;

namespace Mancala.Menu
{
    /// <summary>
    ///     Interaction logic for PagePlayerSettings.xaml
    /// </summary>
    public partial class PagePlayerSettings
    {
        public const byte MaxNameLength = 25;

        public PagePlayerSettings()
        {
            InitializeComponent();
            lblPlayer2IsLabel.Width = lblPlayer1NameLabel.Width;

            txtPlayer1Name.Text = UserSettings.Default.Player1Name;
            txtPlayer2Name.Text = UserSettings.Default.Player2Name;

            if (Enum.TryParse(UserSettings.Default.Player2Type, out Enums.PlayerType player2) == false)
            {
                player2 = Enums.PlayerType.Normal;
                UserSettings.Default.Player2Type = player2.ToString();
                UserSettings.Default.Save();
            }

            sliderPlayer2Is.Value = (double) player2;
            sliderPlayer2Is.ValueChanged += SliderPlayer2Is_OnValueChanged;
            txtPlayer1Name.Focus();
        }

        public bool IfAnyTextboxIsEmptyThenFocus()
        {
            if (txtPlayer1Name.Text == "")
            {
                MessageBox.Show(MessageResources.Player1NameMissingMessage, MessageResources.PlayerNameMissingTitle);
                txtPlayer1Name.Focus();
                return true;
            }

            if (txtPlayer2Name.Text == "")
            {
                MessageBox.Show(MessageResources.Player2NameMissingMessage, MessageResources.PlayerNameMissingTitle);
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
            //Change name if currently a default name.

            if (sliderPlayer2Is.Value == 0) //Human
            {
                if (txtPlayer1Name.Text == MancalaResources.Player2ComputerDefaultName)
                {
                    txtPlayer2Name.Text = MancalaResources.Player2HumanDefaultName;
                    UserSettings.Default.Player2Name = MancalaResources.Player2HumanDefaultName;
                    UserSettings.Default.Save();
                }
            }
            else if (txtPlayer2Name.Text == MancalaResources.Player2HumanDefaultName)
            {
                txtPlayer2Name.Text = MancalaResources.Player2ComputerDefaultName;
                UserSettings.Default.Player2Name = MancalaResources.Player2ComputerDefaultName;
                UserSettings.Default.Save();
            }

            UserSettings.Default.Player2Type = ((Enums.PlayerType) sliderPlayer2Is.Value).ToString();
            UserSettings.Default.Save();
        }
    }
}