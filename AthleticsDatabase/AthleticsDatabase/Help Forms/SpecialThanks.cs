using System.Diagnostics;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>special thanks</summary>
    public partial class SpecialThanks : Form
    {
        /// <summary>special thanks</summary>
        public SpecialThanks()
        {
            InitializeComponent();
        }

        private void AaiLinkLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.athleticsireland.ie/content/");
        }
        private void AthleticleinsterLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.athleticsleinster.org/");
        }
        private void AthleticsmunsterLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.munsterathletics.com/");
        }

        private void OkButtonClick(object sender, System.EventArgs e)
        {
            Close();
        }
    }
}
