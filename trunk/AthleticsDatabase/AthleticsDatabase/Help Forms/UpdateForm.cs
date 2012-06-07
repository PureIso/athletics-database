using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace AthleticsDatabase
{
    /// <summary>Update application</summary>
    public partial class UpdateForm : Form
    {
        /// <summary>Update application</summary>
        public UpdateForm()
        {
            InitializeComponent();
        }
        private void CloseForm()
        {
            if (InvokeRequired) Invoke((MethodInvoker)(CloseForm));
            else
                Close();
        }
        private void UpdateFormLoad(object sender, EventArgs e)
        {
            var oThread = new Thread(CheckUpdateThread);
            oThread.Start();
        }
        private void CheckUpdateThread()
        {
            try
            {
                //progress bar continues progress
                SetProgressBarMarqueeAnimationSpeed(30);
                //xml location
                var xmlReader = XmlReader.Create("http://www.olaegbe.com/athleticsDatabase.xml");
                //read and stop at lateVersion element
                xmlReader.Read();
                xmlReader.ReadToFollowing("latestVersion");
                //Read the contents of latest version to xmlVersion string
                var xmlVersion = xmlReader.ReadString();
                xmlReader.ReadToFollowing("latestDownload");
                var xmlDownloadLink = xmlReader.ReadString();
                xmlReader.Close();

                //comparing version
                if (String.CompareOrdinal(xmlVersion, Application.ProductVersion) > 0)
                {
                    if (ShowMessageBoxReturn(@"Update found! " + Environment.NewLine + @"New Version: " + xmlVersion +
                        Environment.NewLine + @"Would you like to download?", @"Update", MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        var webclient = new WebClient();
                        //=========================================
                        //Rename the current exe to temp
                        var newPath = Path.Combine(Application.StartupPath, "oldFileAD");
                        if (File.Exists(newPath)) File.Delete(newPath);
                        File.Move(Application.ExecutablePath, newPath);
                        //==========================================
                        //Download file
                        webclient.DownloadFileAsync(new Uri(xmlDownloadLink), "AthleticsDatabase.exe");
                        webclient.DownloadProgressChanged += (s, ev) =>
                        {
                            SetUpdatingLabelText("Updating... " + ev.ProgressPercentage + "% Complete");
                        };
                        webclient.DownloadFileCompleted += (s, ev) =>
                        {
                            webclient.Dispose();
                            ShowMessageBox(
                            @"File Downloaded!", @"Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Create a new process containing the information for our updated version.
                            var proccess = new System.Diagnostics.Process
                            {
                                //Start the new updated proccess
                                StartInfo = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath)
                            };
                            //We then start the process.
                            proccess.Start();
                            //And kill the current.
                            System.Diagnostics.Process.GetCurrentProcess().Kill(); 
                        };
                    }
                }
                else
                {
                    ShowMessageBox(@"No Update Found", @"Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CloseForm();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CloseForm();
            }
        }
        private void SetProgressBarMarqueeAnimationSpeed(int value)
        {
            if (progressBar.InvokeRequired)
                progressBar.Invoke((MethodInvoker)
                    (() => SetProgressBarMarqueeAnimationSpeed(value)));
            else
            {
                progressBar.MarqueeAnimationSpeed = value;
            }
        }
        private void SetUpdatingLabelText(string value)
        {
            if (updatingLabel.InvokeRequired)
                updatingLabel.Invoke((MethodInvoker)
                    (() => SetUpdatingLabelText(value)));
            else
            {
                updatingLabel.Text = value;
            }
        }
        private void ShowMessageBox(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            if (InvokeRequired)
                Invoke((MethodInvoker)
                    (() => ShowMessageBox(text, caption, buttons, icon)));
            else
            {
                MessageBox.Show(this, text, caption, buttons, icon);
            }
        }
        private DialogResult ShowMessageBoxReturn(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            var returnVal = DialogResult.None;
            if (InvokeRequired)
                Invoke((MethodInvoker)
                    (() => returnVal = ShowMessageBoxReturn(text, caption, buttons, icon)));
            else
            {
                return MessageBox.Show(this, text, caption, buttons, icon);
            }
            return returnVal;
        }
    }
}
