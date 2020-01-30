using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Mancala.Menu
{
    /// <summary>
    /// Interaction logic for PageRules.xaml
    /// </summary>
    public partial class PageRules : Page
    {
        public PageRules()
        {
            InitializeComponent();
            lblVersionNumber.Content = "Version " + AboutInfo.ThreePartVersionNumber + " ";

            txtReadOnlyRules.Text = string.Join("\r\n",
                "Mancala is played on a board with two rows of 6 cups and a larger cup, called a Mancala, on each side. Player 1 owns the bottom row and left Mancala; player 2 owns the top row and right Mancala. The aim of the game is to have the most stones in your Mancala when either player has no more stones in any of their cups.",
                "",
                "On your turn, click one of your cups. The stones in that cup will be 'sowed' ⁠— the first stone will be placed into the cup (or Mancala) on its left, and so on. Sowing will continue clockwise until there are no more stones to sow.* Sowing will include your Mancala but exclude your opponent's. If the last stone is sowed into your Mancala, you gain an extra turn.",
                "",
                "When either player has no more stones in any of their cups, the game ends. Any stones still in play will be moved to that player's Mancala (or their opponent's, depending on the setting chosen under New Game Settings). After that, the player with the most stones in their Mancala wins.",
                "",
                "",
                "Capturing: If the last stone is sowed into one of your empty cups and the cup vertically opposite is not empty, all of the stones in both cups will be moved to your Mancala. When that happens, you will not gain an extra turn.",
                "",
                "Multiple Laps: If the last stone is sowed into a cup that is not empty (on either player's side), all of the stones in that cup will be sowed as normal. The turn only ends when the last stone is sowed into an empty cup. Extra turns (by sowing into your Mancala) apply, even if the cup currently being sowed is not the first cup of the turn.",
                "",
                "",
                "*Cups with 13 or more stones will end up sowing into themselves, which is why some cups will not be empty once sowing is complete."
            );

            txtReadOnlyRules.Focus();
        }

        public void FocusOnFirstControl() => txtReadOnlyRules.Focus();

        void lblVersionNumber_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(
                string.Join("\r\n",
                    "Mancala " + AboutInfo.ThreePartVersionNumber + ", released " + AboutInfo.ReleaseDate + ".",

                    AboutInfo.Creator,
                    AboutInfo.Licenses), "About Mancala 4 (WPF)");
        }
    }
}