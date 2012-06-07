using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>Add competitions into the database</summary>
    public partial class AddCompetition : Form
    {
        /// <summary>Add competition form component initializer</summary>
        public AddCompetition()
        {
            InitializeComponent();
        }
        private void CompetitionHistorySeasonComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(competitionHistorySeasonComboBox.Text)) return;
            competitionHistoryCountryComboBox.Enabled = true;
            competitionHistoryCountryComboBox.Items.Clear();
            var databaseAccess = MainForm.Connection();
            var countries = databaseAccess.GetCountries(competitionHistorySeasonComboBox.Text);

            foreach (var country in countries)
            {
                competitionHistoryCountryComboBox.Items.Add(country);
            }
            competitionHistoryRegionComboBox.Enabled = true;
        }
        private void ClearButtonClick(object sender, EventArgs e)
        {
            competitionNameTextBox.Clear();
            competitionCountryTextBox.Text = @"Ireland";
            competitionRegionComboBox.Text = @"All Ireland";
            competitionVenueTextBox.Clear();
            competitionDateTextBox.Text = @"DD/MM/YYYY";
        }
        private void AddCompetitionLoad(object sender, EventArgs e)
        {
            competitionRegionComboBox.Text = @"All Ireland";
            var databaseAccess = MainForm.Connection();
            var years = databaseAccess.GetCompetitionYear(Enumerations.CompetitionType.All);
            foreach (var year in years)
            {
                competitionHistorySeasonComboBox.Items.Add(year);
            }
            competitionHistorySeasonComboBox.Enabled = true;
        }
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            var oThread = new System.Threading.Thread(SubmitCompetition);
            oThread.Start();
        }
        private void EnterButtonClick(object sender, EventArgs e)
        {
            var oThread = new System.Threading.Thread(LoadCompetitionHistory);
            competitionHistoryListView.Items.Clear();
            oThread.Start();
        }

        #region Thread
        private void SubmitCompetition()
        {
            var competitionType = indoorRadioButton.Checked ? 
                Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;
            var competitionName = GetCompetitionNameTextBoxText();
            
            if (string.IsNullOrEmpty(competitionName))
            {
                ShowMessageBox(@"Empty Competition Name", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var country = GetCompetitionCountryTextBoxText();
            var region = GetCompetitionRegionComboBoxText();
            if (country == "Ireland" && region == "Non Irish")
            {
                ShowMessageBox(@"Country doesn't match Region.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var competitionVenue = GetCompetitionVenueTextBoxText();
            var competitionDate = GetCompetitionDateTextBoxText();
            if (!VariousFunctions.ValidateDate(competitionDate))
            {
                ShowMessageBox(@"Invalide Date", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var messageString = @"Competition Type: " + competitionType + Environment.NewLine +
                "Competition Name: " + competitionName + Environment.NewLine +
                "Country: " + country + Environment.NewLine +
                "Region: " + region + Environment.NewLine +
                "Venue: " + competitionVenue + Environment.NewLine +
                "Date: " + competitionDate + Environment.NewLine;
            if (ShowMessageBoxReturn(messageString,@"Athletics Database",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) != DialogResult.OK) return;

            try
            {
                var databaseAccess = MainForm.Connection();
                databaseAccess.AddCompetition(competitionName, country, region, competitionVenue, competitionDate,
                                              competitionType);
                ShowMessageBox(@"Added Successfully.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCompetitionHistory()
        {
            if (GetCompetitionHistoryRegionComboBoxText() == null)
            {
                ShowMessageBox(@"Invalid Action", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var country = GetCompetitionHistoryCountryComboBoxText();
            var region = GetCompetitionHistoryRegionComboBoxText();
            if (country == "Ireland" && region == "Non Irish")
            {
                ShowMessageBox(@"Country doesn't match Region.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var databaseAccess = MainForm.Connection();
                var datas = databaseAccess.ListAllCompetitions(GetCompetitionHistorySeasonComboBoxText(),
                                                               GetCompetitionHistoryCountryComboBoxText(),
                                                               GetCompetitionHistoryRegionComboBoxText());

                CompetitionsListViewUpdate(datas);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region ThreadSafe
        private string GetCompetitionNameTextBoxText()
        {
            var returnVal = "";
            if (competitionNameTextBox.InvokeRequired)
                competitionNameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionNameTextBoxText(); });
            else
            {
                return competitionNameTextBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionCountryTextBoxText()
        {
            var returnVal = "";
            if (competitionCountryTextBox.InvokeRequired)
                competitionCountryTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionCountryTextBoxText(); });
            else
            {
                return competitionCountryTextBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionRegionComboBoxText()
        {
            var returnVal = "";
            if (competitionRegionComboBox.InvokeRequired)
                competitionRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionRegionComboBoxText(); });
            else
            {
                return competitionRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionVenueTextBoxText()
        {
            var returnVal = "";
            if (competitionVenueTextBox.InvokeRequired)
                competitionVenueTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionVenueTextBoxText(); });
            else
            {
                return competitionVenueTextBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionDateTextBoxText()
        {
            var returnVal = "";
            if (competitionDateTextBox.InvokeRequired)
                competitionDateTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionDateTextBoxText(); });
            else
            {
                return competitionDateTextBox.Text;
            }
            return returnVal;
        }
        private void CompetitionsListViewUpdate(IEnumerable<string[]> datas)
        {
            if (datas == null) return;
            if (competitionHistoryListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                competitionHistoryListView.Invoke((MethodInvoker)(() => CompetitionsListViewUpdate(datas)));
            else
            {
                var listColumnArray = new string[7];
                foreach (var data in datas)
                {
                    listColumnArray[0] = data[0];
                    listColumnArray[1] = data[1];
                    listColumnArray[2] = data[2];
                    listColumnArray[3] = data[3];
                    listColumnArray[4] = data[4];
                    competitionHistoryListView.Items.Add(new ListViewItem(listColumnArray));
                }
            }
        }
        private string GetCompetitionHistorySeasonComboBoxText()
        {
            var returnVal = "";
            if (competitionHistorySeasonComboBox.InvokeRequired)
                competitionHistorySeasonComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionHistorySeasonComboBoxText(); });
            else
            {
                return competitionHistorySeasonComboBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionHistoryRegionComboBoxText()
        {
            var returnVal = "";
            if (competitionHistoryRegionComboBox.InvokeRequired)
                competitionHistoryRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionHistoryRegionComboBoxText(); });
            else
            {
                return competitionHistoryRegionComboBox.Enabled == false ? null : competitionHistoryRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetCompetitionHistoryCountryComboBoxText()
        {
            var returnVal = "";
            if (competitionHistoryCountryComboBox.InvokeRequired)
                competitionHistoryCountryComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetCompetitionHistoryCountryComboBoxText(); });
            else
            {
                return competitionHistoryCountryComboBox.Text;
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
