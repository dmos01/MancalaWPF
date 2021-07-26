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
        /// <summary>
        ///     Uses Assembly version. Not to be confused with Package Version or Assembly File Version.
        /// </summary>
        public static string ThreePartVersionNumber
        {
            get
            {
                Version v = Assembly.GetExecutingAssembly().GetName().Version;
                return v.Major + "." + v.Minor + "." + v.Build;
            }
        }

        public PageRulesAndAbout()
        {
            InitializeComponent();
            lblVersionNumber.Content = MancalaResources.VersionLiteral + " " + ThreePartVersionNumber +
                                       MancalaResources.EndOfSentence;
            txtReadOnlyRules.Text = MancalaResources.Rules;
            txtReadOnlyRules.Focus();
        }

        public void FocusOnFirstControl() => txtReadOnlyRules.Focus();

        void lblVersionNumber_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(string.Join(Environment.NewLine,
                MancalaResources.MancalaLiteral + " " + ThreePartVersionNumber + MancalaResources.ReleaseDate,
                MancalaResources.CreatedBy, MessageResources.Licenses), MessageResources.AboutWindowTitle);
        }
    }
}