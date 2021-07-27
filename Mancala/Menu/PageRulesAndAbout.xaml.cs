using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace Mancala.Menu
{
    /// <summary>
    ///     Interaction logic for PageRulesAndAbout.xaml
    /// </summary>
    public partial class PageRulesAndAbout
    {
        readonly Version version = Assembly.GetExecutingAssembly().GetName().Version;
        
        public PageRulesAndAbout()
        {
            InitializeComponent();
            lblVersionNumber.Content = MancalaResources.VersionLiteral + " " + version.Major + "." + version.Minor + "." + version.Build +
                                       MancalaResources.EndOfSentence;
            txtReadOnlyRules.Text = MancalaResources.Rules;
            txtReadOnlyRules.Focus();
        }

        public void FocusOnFirstControl() => txtReadOnlyRules.Focus();

        void lblVersionNumber_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(string.Join(Environment.NewLine,
                MancalaResources.MancalaLiteral + " " + version.Major + "." + version.Minor + "." + version.Build + MancalaResources.ReleaseDate,
                MancalaResources.CreatedBy, MessageResources.Licenses), MessageResources.AboutWindowTitle);
        }
    }
}