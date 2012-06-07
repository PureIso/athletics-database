using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>Add clubs into the database</summary>
    public partial class AddClub : Form
    {
        /// <summary>Add club form component initializer</summary>
        public AddClub()
        {
            InitializeComponent();
        }
        private void EnterButtonClick(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(clubListRegionComboBox.Text))
            {
                ShowMessageBox(@"Select a Region!", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clubListView.Items.Clear();
            var oThread = new Thread(LoadClubs);
            oThread.Start();
        }
        private void ClearButtonClick(object sender, EventArgs e)
        {
            addClubNameTextBox.Clear();
        }
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            var oThread = new Thread(SubmitClub);
            oThread.Start();
        }

        #region Threads & Thread Safe
        private void SubmitClub()
        {
            if (string.IsNullOrEmpty(GetAddClubNameTextBoxText()))
            {
                ShowMessageBox(@"Invalid Club Name", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var messageString = @"Club Region: " + GetAddClubRegionComboBoxText() + Environment.NewLine
                + "Club Name: " + GetAddClubNameTextBoxText() + Environment.NewLine;

            if (ShowMessageBoxReturn(messageString, @"Athletics Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;

            try
            {
                var databaseAccess = MainForm.Connection();
                databaseAccess.AddClub(GetAddClubNameTextBoxText(), GetAddClubRegionComboBoxText());
                ShowMessageBox(@"Added Successfully.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void LoadClubs()
        {
            try
            {
                //Get selected region ID
                var databaseAccess = MainForm.Connection();
                var regionID = databaseAccess.GetRegionId(GetClubListRegionComboBoxText());
                var listOfClubs = databaseAccess.ListAllClubs(regionID);
                if (listOfClubs == null)
                {
                    ShowMessageBox(@"No Results Found", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (var club in listOfClubs)
                {
                    ClubListViewUpdate(club);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }
        private void ClubListViewUpdate(IList<string> data)
        {
            if (clubListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                clubListView.Invoke((MethodInvoker)(() => ClubListViewUpdate(data)));
            else
            {
                string[] listColumnArray = {data[0], data[1]};
                clubListView.Items.Add(new ListViewItem(listColumnArray));
            }
        }
        private string GetClubListRegionComboBoxText()
        {
            var returnVal = "";
            if (clubListRegionComboBox.InvokeRequired)
                clubListRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetClubListRegionComboBoxText(); });
            else
            {
                return clubListRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetAddClubRegionComboBoxText()
        {
            var returnVal = "";
            if (addClubRegionComboBox.InvokeRequired)
                addClubRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddClubRegionComboBoxText(); });
            else
            {
                return addClubRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetAddClubNameTextBoxText()
        {
            var returnVal = "";
            if (addClubNameTextBox.InvokeRequired)
                addClubNameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddClubNameTextBoxText(); });
            else
            {
                return addClubNameTextBox.Text;
            }
            return returnVal;
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
        #endregion
    }
}
