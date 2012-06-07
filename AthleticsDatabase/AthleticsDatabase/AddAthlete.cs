using System;
using System.Threading;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    public partial class AddAthlete : Form
    {
        private bool _working;
        /// <summary>Add athlete form component initializer</summary>
        public AddAthlete()
        {
            InitializeComponent();
        }
        private void AddAthleteClearButtonClick(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void AddAthleteSubmitButtonClick(object sender, EventArgs e)
        {
            if (_working) return;
            if (string.IsNullOrEmpty(addAthleteNameTextBox.Text) || VariousFunctions.IsNumeric(addAthleteNameTextBox.Text))
            {
                ShowMessageBox(@"Invalid Athlete Name.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(addAthleteSurnameTextBox.Text) || VariousFunctions.IsNumeric(addAthleteSurnameTextBox.Text))
            {
                ShowMessageBox(@"Invalid Athlete Surname.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!VariousFunctions.IsNumeric(addAthleteYearOfBirthTextBox.Text))
            {
                ShowMessageBox(@"Invalid Year of birth.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(GetAddAthleteClubComboBoxText()) || GetAddAthleteClubComboBoxText() == @"Select a Club")
            {
                ShowMessageBox(@"Please Select a Club.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oThread = new Thread(AddNewAthlete);
            oThread.Start();
        }

        #region Thread
        private void AddNewAthlete()
        {
            try
            {
                _working = true;
                var gender = addAthleteMaleRadioButton.Checked ?
                Enumerations.Gender.Male : Enumerations.Gender.Female;

                var messageString = @"Name: " + GetAddAthleteNameTextBoxText() + Environment.NewLine
                + "Surname: " + GetAddAthleteSurnameTextBoxText() + Environment.NewLine
                + "Year of birth: " + GetAddAthleteYearOfBirthTextBoxText() + Environment.NewLine
                + "Region: " + GetAddAthleteRegionComboBoxText() + Environment.NewLine
                + "Club: " + GetAddAthleteClubComboBoxText() + Environment.NewLine;

                if (ShowMessageBoxReturn(messageString, @"Athletics Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
                var databaseAccess = MainForm.Connection();
                databaseAccess.AddAthlete(gender, GetAddAthleteNameTextBoxText(), GetAddAthleteSurnameTextBoxText(), Int32.Parse(GetAddAthleteYearOfBirthTextBoxText()), GetAddAthleteRegionComboBoxText(), GetAddAthleteClubComboBoxText());
                ShowMessageBox(@"Added Successfully.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearAll();
                _working = false;
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
                var regionID = databaseAccess.GetRegionId(GetAddAthleteRegionComboBoxText());
                var listOfClubs = databaseAccess.ListAllClubs(regionID);
                if (listOfClubs == null)
                {
                    ShowMessageBox(@"No Results Found", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                foreach (var club in listOfClubs)
                {
                    AddClubs(club[1]);
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
        #endregion

        #region Safe Thread
        private string GetAddAthleteNameTextBoxText()
        {
            var returnVal = "";
            if (addAthleteNameTextBox.InvokeRequired)
                addAthleteNameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddAthleteNameTextBoxText(); });
            else
            {
                return addAthleteNameTextBox.Text;
            }
            return returnVal;
        }
        private string GetAddAthleteSurnameTextBoxText()
        {
            var returnVal = "";
            if (addAthleteSurnameTextBox.InvokeRequired)
                addAthleteSurnameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddAthleteSurnameTextBoxText(); });
            else
            {
                return addAthleteSurnameTextBox.Text;
            }
            return returnVal;
        }
        private string GetAddAthleteYearOfBirthTextBoxText()
        {
            var returnVal = "";
            if (addAthleteYearOfBirthTextBox.InvokeRequired)
                addAthleteYearOfBirthTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddAthleteYearOfBirthTextBoxText(); });
            else
            {
                return addAthleteYearOfBirthTextBox.Text;
            }
            return returnVal;
        }
        private string GetAddAthleteRegionComboBoxText()
        {
            var returnVal = "";
            if (addAthleteRegionComboBox.InvokeRequired)
                addAthleteRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddAthleteRegionComboBoxText(); });
            else
            {
                return addAthleteRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetAddAthleteClubComboBoxText()
        {
            var returnVal = "";
            if (addAthleteClubComboBox.InvokeRequired)
                addAthleteClubComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetAddAthleteClubComboBoxText(); });
            else
            {
                return addAthleteClubComboBox.Text;
            }
            return returnVal;
        }
        private void ClearAll()
        {
            SetAddAthleteNameTextBoxText("");
            SetAddAthleteSurnameTextBoxText("");
            SetAddAthleteYearOfBirthTextBoxText("");
            SetAddAthleteRegionComboBoxText("");
            SetAddAthleteClubComboBoxText("");
            SetAddAthleteClubComboBoxClear();
        }
        private void SetAddAthleteNameTextBoxText(string value)
        {
            if (addAthleteNameTextBox.InvokeRequired)
                addAthleteNameTextBox.Invoke((MethodInvoker)
                    (() => SetAddAthleteNameTextBoxText(value)));
            else
            {
                addAthleteNameTextBox.Text = value;
            }
        }
        private void SetAddAthleteSurnameTextBoxText(string value)
        {
            if (addAthleteSurnameTextBox.InvokeRequired)
                addAthleteSurnameTextBox.Invoke((MethodInvoker)
                    (() => SetAddAthleteSurnameTextBoxText(value)));
            else
            {
                addAthleteSurnameTextBox.Text = value;
            }
        }
        private void SetAddAthleteYearOfBirthTextBoxText(string value)
        {
            if (addAthleteYearOfBirthTextBox.InvokeRequired)
                addAthleteYearOfBirthTextBox.Invoke((MethodInvoker)
                    (() => SetAddAthleteYearOfBirthTextBoxText(value)));
            else
            {
                addAthleteYearOfBirthTextBox.Text = value;
            }
        }
        private void SetAddAthleteRegionComboBoxText(string value)
        {
            if (addAthleteRegionComboBox.InvokeRequired)
                addAthleteRegionComboBox.Invoke((MethodInvoker)
                    (() => SetAddAthleteRegionComboBoxText(value)));
            else
            {
                addAthleteRegionComboBox.Text = value;
            }
        }
        private void SetAddAthleteClubComboBoxText(string value)
        {
            if (addAthleteClubComboBox.InvokeRequired)
                addAthleteClubComboBox.Invoke((MethodInvoker)
                    (() => SetAddAthleteClubComboBoxText(value)));
            else
            {
                addAthleteClubComboBox.Text = value;
            }
        }
        private void AddClubs(string value)
        {
            if (addAthleteClubComboBox.InvokeRequired)
                addAthleteClubComboBox.Invoke((MethodInvoker)
                    (() => AddClubs(value)));
            else
            {
                addAthleteClubComboBox.Items.Add(value);
            }
        }
        private void SetAddAthleteClubComboBoxClear()
        {
            if (addAthleteClubComboBox.InvokeRequired)
                addAthleteClubComboBox.Invoke((MethodInvoker)
                    SetAddAthleteClubComboBoxClear);
            else
            {
                addAthleteClubComboBox.Items.Clear();
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
        #endregion

        private void AddAthleteRegionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(GetAddAthleteRegionComboBoxText())) return;
            addAthleteClubComboBox.Items.Clear();
            addAthleteClubComboBox.Text = @"Select a Club";
            var oThread = new Thread(LoadClubs);
            oThread.Start();
        }
    }
}
