using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>Enter results into the database</summary>
    public partial class EnterResult : Form
    {
        /// <summary>enter results form component initializer</summary>
        public EnterResult()
        {
            InitializeComponent();
        }
        private void EnterResultLoad(object sender, EventArgs e)
        {
            ClearAll();
            var oThread = new Thread(LoadStartUpData);
            oThread.Start();
        }

        #region Enter Result
        private void EnterResultOutdoorRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            ClearAll();
            var oThread = new Thread(LoadStartUpData);
            oThread.Start();
        }
        private void EnterResultSeasonComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(enterResultSeasonComboBox.Text)) return;
            enterResultCountryComboBox.Items.Clear();
            enterResultCountryComboBox.Enabled = true;
            enterResultRegionComboBox.Items.Clear();
            enterResultRegionComboBox.Enabled = false;
            enterResultVenueComboBox.Items.Clear();
            enterResultVenueComboBox.Enabled = false;
            enterResultCompetitionComboBox.Items.Clear();
            enterResultCompetitionComboBox.Enabled = false;
            enterResultDateComboBox.Items.Clear();
            enterResultDateComboBox.Enabled = false;

            var database = MainForm.Connection();
            var countries = database.GetCountries(enterResultSeasonComboBox.Text);
            foreach (var country in countries)
            {
                enterResultCountryComboBox.Items.Add(country);
            }
        }
        private void EnterResultCountryComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(enterResultCountryComboBox.Text)) return;
            enterResultRegionComboBox.Items.Clear();
            enterResultRegionComboBox.Enabled = true;
            enterResultVenueComboBox.Items.Clear();
            enterResultVenueComboBox.Enabled = false;
            enterResultCompetitionComboBox.Items.Clear();
            enterResultCompetitionComboBox.Enabled = false;
            enterResultDateComboBox.Items.Clear();
            enterResultDateComboBox.Enabled = false;

            var competitionType = enterResultIndoorRadioButton.Checked
                             ? Enumerations.CompetitionType.Indoor
                             : Enumerations.CompetitionType.Outdoor;

            var database = MainForm.Connection();
            var regions = database.GetRegion(enterResultSeasonComboBox.Text, enterResultCountryComboBox.Text,competitionType);
            foreach (var region in regions)
            {
                enterResultRegionComboBox.Items.Add(region);
            }
        }
        private void EnterResultRegionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(enterResultRegionComboBox.Text)) return;
            enterResultVenueComboBox.Items.Clear();
            enterResultVenueComboBox.Enabled = true;
            enterResultCompetitionComboBox.Items.Clear();
            enterResultCompetitionComboBox.Enabled = false;
            enterResultDateComboBox.Items.Clear();
            enterResultDateComboBox.Enabled = false;

            var competitionType = enterResultIndoorRadioButton.Checked
                             ? Enumerations.CompetitionType.Indoor
                             : Enumerations.CompetitionType.Outdoor;

            var database = MainForm.Connection();
            var venues = database.GetVenues(enterResultSeasonComboBox.Text, enterResultCountryComboBox.Text, enterResultRegionComboBox.Text, competitionType);
            foreach (var venue in venues)
            {
                enterResultVenueComboBox.Items.Add(venue);
            }
        }
        private void EnterResultVenueComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(enterResultVenueComboBox.Text)) return;
            enterResultCompetitionComboBox.Items.Clear();
            enterResultCompetitionComboBox.Enabled = true;
            enterResultDateComboBox.Items.Clear();
            enterResultDateComboBox.Enabled = false;

            var competitionType = enterResultIndoorRadioButton.Checked
                             ? Enumerations.CompetitionType.Indoor
                             : Enumerations.CompetitionType.Outdoor;

            var database = MainForm.Connection();
            var competitions = database.GetCompetitionName(enterResultSeasonComboBox.Text, enterResultCountryComboBox.Text, enterResultRegionComboBox.Text, enterResultVenueComboBox.Text, competitionType);
            foreach (var competition in competitions)
            {
                enterResultCompetitionComboBox.Items.Add(competition);
            }
        }
        private void EnterResultCompetitionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(enterResultCompetitionComboBox.Text)) return;
            enterResultDateComboBox.Items.Clear();
            enterResultDateComboBox.Enabled = true;

            var competitionType = enterResultIndoorRadioButton.Checked
                             ? Enumerations.CompetitionType.Indoor
                             : Enumerations.CompetitionType.Outdoor;

            var database = MainForm.Connection();
            var dates = database.GetCompetitionDate(enterResultSeasonComboBox.Text, enterResultCountryComboBox.Text, enterResultRegionComboBox.Text, enterResultVenueComboBox.Text,enterResultCompetitionComboBox.Text, competitionType);
            foreach (var date in dates)
            {
                enterResultDateComboBox.Items.Add(date);
            }
        }
        private void ClearButtonClick(object sender, EventArgs e)
        {
            if (ShowMessageBoxReturn(@"Are you sure?", @"Athletics Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
            //===========================
            athleteIDOneTextBox.Clear();
            performanceOneTextBox.Clear();
            //===========================
            athleteIDTwoTextBox.Clear();
            performanceTwoTextBox.Clear();
            //===========================
            athleteIDThreeTextBox.Clear();
            performanceThreeTextBox.Clear();
            //===========================
            athleteIDFourTextBox.Clear();
            performanceFourTextBox.Clear();
            //===========================
            athleteIDFiveTextBox.Clear();
            performanceFiveTextBox.Clear();
            //===========================
            athleteIDSixTextBox.Clear();
            performanceSixTextBox.Clear();
            //===========================
            athleteIDSevenTextBox.Clear();
            performanceSevenTextBox.Clear();
            //===========================
            athleteIDEightTextBox.Clear();
            performanceEightTextBox.Clear();
            //===========================
            athleteIDNineTextBox.Clear();
            performanceNineTextBox.Clear();
            //===========================
            athleteIDTenTextBox.Clear();
            performanceTenTextBox.Clear();
            //===========================
            athleteIDElevenTextBox.Clear();
            performanceElevenTextBox.Clear();
            //===========================
            athleteIDTwelveTextBox.Clear();
            performanceTwelveTextBox.Clear();
            //===========================
            athleteIDThirteenTextBox.Clear();
            performanceThirteenTextBox.Clear();
            //===========================
            athleteIDFourteenTextBox.Clear();
            performanceFourteenTextBox.Clear();
            //===========================
        }
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            if (ShowMessageBoxReturn(@"Are you sure?", @"Athletics Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK) return;
            var database = MainForm.Connection();
            var competitionId = database.GetCompetitionId(enterResultDateComboBox.Text, enterResultCountryComboBox.Text,
                enterResultRegionComboBox.Text, enterResultVenueComboBox.Text, enterResultCompetitionComboBox.Text);
            var gender = enterResultMaleRadioButton.Checked
                ? Enumerations.Gender.Male : Enumerations.Gender.Female;
            var competitionType = enterResultIndoorRadioButton.Checked
                ? Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;
            try
            {
                submitButton.Enabled = false;
                clearButton.Enabled = false;
                for (var i = 0; i < 14; i++)
                {
                    toolStripProgressBar.Value += 7;
                    switch (i)
                    {
                        case 0:
                            if (!VariousFunctions.IsNumeric(athleteIDOneTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDOneTextBox.Text) ||
                                String.IsNullOrEmpty(performanceOneTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceOneTextBox.Text,Int32.Parse(athleteIDOneTextBox.Text),
                                competitionId,gender,competitionType,Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 1:
                            if (!VariousFunctions.IsNumeric(athleteIDTwoTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDTwoTextBox.Text) ||
                                String.IsNullOrEmpty(performanceTwoTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceTwoTextBox.Text, Int32.Parse(athleteIDTwoTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 2:
                            if (!VariousFunctions.IsNumeric(athleteIDThreeTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDThreeTextBox.Text) ||
                                String.IsNullOrEmpty(performanceThreeTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceThreeTextBox.Text, Int32.Parse(athleteIDThreeTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 3:
                            if (!VariousFunctions.IsNumeric(athleteIDFourTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDFourTextBox.Text) ||
                                String.IsNullOrEmpty(performanceFourTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceFourTextBox.Text, Int32.Parse(athleteIDFourTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 4:
                            if (!VariousFunctions.IsNumeric(athleteIDFiveTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDFiveTextBox.Text) ||
                                String.IsNullOrEmpty(performanceFiveTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceFiveTextBox.Text, Int32.Parse(athleteIDFiveTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 5:
                            if (!VariousFunctions.IsNumeric(athleteIDSixTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDSixTextBox.Text) ||
                                String.IsNullOrEmpty(performanceSixTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceSixTextBox.Text, Int32.Parse(athleteIDSixTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 6:
                            if (!VariousFunctions.IsNumeric(athleteIDSevenTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDSevenTextBox.Text) ||
                                String.IsNullOrEmpty(performanceSevenTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceSevenTextBox.Text, Int32.Parse(athleteIDSevenTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 7:
                            if (!VariousFunctions.IsNumeric(athleteIDEightTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDEightTextBox.Text) ||
                                String.IsNullOrEmpty(performanceEightTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceEightTextBox.Text, Int32.Parse(athleteIDEightTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 8:
                            if (!VariousFunctions.IsNumeric(athleteIDNineTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDNineTextBox.Text) ||
                                String.IsNullOrEmpty(performanceNineTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceNineTextBox.Text, Int32.Parse(athleteIDNineTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 9:
                            if (!VariousFunctions.IsNumeric(athleteIDTenTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDTenTextBox.Text) ||
                                String.IsNullOrEmpty(performanceTenTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceTenTextBox.Text, Int32.Parse(athleteIDTenTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 10:
                            if (!VariousFunctions.IsNumeric(athleteIDElevenTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDElevenTextBox.Text) ||
                                String.IsNullOrEmpty(performanceElevenTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceElevenTextBox.Text, Int32.Parse(athleteIDElevenTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 11:
                            if (!VariousFunctions.IsNumeric(athleteIDTwelveTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDTwelveTextBox.Text) ||
                                String.IsNullOrEmpty(performanceTwelveTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceTwelveTextBox.Text, Int32.Parse(athleteIDTwelveTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 12:
                            if (!VariousFunctions.IsNumeric(athleteIDThirteenTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDThirteenTextBox.Text) ||
                                String.IsNullOrEmpty(performanceThirteenTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceThirteenTextBox.Text, Int32.Parse(athleteIDThirteenTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                        case 13:
                            if (!VariousFunctions.IsNumeric(athleteIDFourteenTextBox.Text)) break;
                            if (String.IsNullOrEmpty(athleteIDFourteenTextBox.Text) ||
                                String.IsNullOrEmpty(performanceFourteenTextBox.Text)) break;
                            database.AddResults(enterResultEventComboBox.Text, performanceFourteenTextBox.Text, Int32.Parse(athleteIDFourteenTextBox.Text),
                                competitionId, gender, competitionType, Int32.Parse(enterResultSeasonComboBox.Text));
                            break;
                    }
                }
                ShowMessageBox(@"Results Entered Successfully.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                submitButton.Enabled = true;
                clearButton.Enabled = true;
                toolStripProgressBar.Value = 0;
            }
        }

        #region Thread
        private void LoadStartUpData()
        {
            try
            {
                //Get selected gender
                var competitionType = enterResultIndoorRadioButton.Checked ? 
                    Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;

                var database = MainForm.Connection();
                var years = database.GetCompetitionYear(competitionType);
                foreach (var year in years)
                {
                    SetEnterResultSeasonComboBoxItem(year);
                }
                var events = database.ListAllEvents(competitionType);
                foreach (var @event in events)
                {
                    SetEnterResultEventComboBoxItem(@event);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Function
        private void ClearAll()
        {
            enterResultEventComboBox.Items.Clear();
            enterResultSeasonComboBox.Items.Clear();
            enterResultCountryComboBox.Items.Clear();
            enterResultRegionComboBox.Items.Clear();
            enterResultVenueComboBox.Items.Clear();
            enterResultCompetitionComboBox.Items.Clear();
            enterResultDateComboBox.Items.Clear();
            //=============================================
            enterResultRegionComboBox.Enabled = false;
            enterResultCountryComboBox.Enabled = false;
            enterResultCompetitionComboBox.Enabled = false;
            enterResultDateComboBox.Enabled = false;
            enterResultVenueComboBox.Enabled = false;
        }
        #endregion

        #region Thread Safe
        private void SetEnterResultSeasonComboBoxItem(string value)
        {
            if (enterResultSeasonComboBox.InvokeRequired)
                enterResultSeasonComboBox.Invoke((MethodInvoker)
                    (() => SetEnterResultSeasonComboBoxItem(value)));
            else
            {
                enterResultSeasonComboBox.Items.Add(value);
            }
        }
        private void SetEnterResultEventComboBoxItem(string value)
        {
            if (enterResultEventComboBox.InvokeRequired)
                enterResultEventComboBox.Invoke((MethodInvoker)
                    (() => SetEnterResultEventComboBoxItem(value)));
            else
            {
                enterResultEventComboBox.Items.Add(value);
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
        #endregion

        #region Search
        #region Key Press
        private void EnterResultAthleteIdSearchTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (!VariousFunctions.IsNumeric(enterResultAthleteIDSearchTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                var oThread = new Thread(Search);
                enterResultAthleteIDTextBox.Text = enterResultAthleteIDSearchTextBox.Text;
                enterResultSearchFirstNameTextBox.Clear();
                enterResultSearchSurnameTextBox.Clear();
                oThread.Start();    
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EnterResultSearchFirstNameTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (VariousFunctions.IsNumeric(enterResultSearchFirstNameTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                enterResultSearchListView.Items.Clear();
                var oThread = new Thread(LoadEnterResultNamesToListView);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EnterResultSearchSurnameTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (VariousFunctions.IsNumeric(enterResultSearchSurnameTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                enterResultSearchListView.Items.Clear();
                var oThread = new Thread(LoadEnterResultNamesToListView);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Button Clicks
        private void EnterResultEnterAthletesIdButtonClick(object sender, EventArgs e)
        {
            if (!VariousFunctions.IsNumeric(enterResultAthleteIDSearchTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oThread = new Thread(Search);
            enterResultAthleteIDTextBox.Text = enterResultAthleteIDSearchTextBox.Text;
            enterResultSearchFirstNameTextBox.Clear();
            enterResultSearchSurnameTextBox.Clear();
            oThread.Start();
        }
        private void EnterResultSearchButtonClick(object sender, EventArgs e)
        {
            if (VariousFunctions.IsNumeric(enterResultSearchFirstNameTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (VariousFunctions.IsNumeric(enterResultSearchSurnameTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            enterResultSearchListView.Items.Clear();
            var oThread = new Thread(LoadEnterResultNamesToListView);
            oThread.Start();
        }
        private void EnterResultClearSearchResultsButtonClick(object sender, EventArgs e)
        {
            enterResultSearchListView.Items.Clear();
            enterResultSearchFirstNameTextBox.Clear();
            enterResultSearchSurnameTextBox.Clear();
        }
        private void EnterResultSearchListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            var oThread = new Thread(LoadEnterResultSearchFromListView);
            enterResultSearchFirstNameTextBox.Clear();
            enterResultSearchSurnameTextBox.Clear();
            oThread.Start();
        }
        private void EnterResultAddAthleteButtonClick(object sender, EventArgs e)
        {
            var newform = new AddAthlete();
            newform.ShowDialog(this);
        }
        #endregion

        #region Thread
        private void Search()
        {
            try
            {
                //Clearing
                DetailEnterResultClear();

                //Get selected gender
                var gender = enterResultSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var database = MainForm.Connection();
                var athleteInfo = database.GetAthlete(GetEnterResultAthleteIdSearchTextBoxText(), gender);
                if (athleteInfo == null) return;
                SetEnterResultAthleteIdTextBoxText(athleteInfo[0]);
                SetEnterResultNameTextBoxText(athleteInfo[1]);
                SetEnterResultSurnameTextBoxText(athleteInfo[2]);
                SetYearOfBirthTextBoxTextBoxText(athleteInfo[3]);
                SetEnterResultRegionTextBoxText(athleteInfo[4]);
                SetEnterResultClubTextBoxText(athleteInfo[5]);
                SetEnterResultGenderTextBoxText(enterResultSearchMaleRadioButton.Checked ? "Male" : "Female");
                SetEnterResultAthleteIdSearchTextBoxText("");
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadEnterResultNamesToListView()
        {
            try
            {
                //Get selected gender
                var gender = enterResultSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var database = MainForm.Connection();
                var athleteNames = database.SearchAthletes(GetEnterResultSearchFirstNameTextBoxText(),
                    GetEnterResultSearchSurnameTextBoxText(), gender);
                if (athleteNames == null) return;
                EnterResultSearchListViewUpdate(athleteNames);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadEnterResultSearchFromListView()
        {
            try
            {
                //Clearing
                DetailEnterResultClear();

                //Get selected gender
                var gender = enterResultSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var database = MainForm.Connection();
                var athleteInfo = database.GetAthlete(GetEnterResultSearchListViewSelectedIndex(), gender);
                if (athleteInfo == null) return;
                SetEnterResultAthleteIdTextBoxText(athleteInfo[0]);
                SetEnterResultNameTextBoxText(athleteInfo[1]);
                SetEnterResultSurnameTextBoxText(athleteInfo[2]);
                SetYearOfBirthTextBoxTextBoxText(athleteInfo[3]);
                SetEnterResultRegionTextBoxText(athleteInfo[4]);
                SetEnterResultClubTextBoxText(athleteInfo[5]);
                SetEnterResultGenderTextBoxText(enterResultSearchMaleRadioButton.Checked ? "Male" : "Female");
                SetEnterResultAthleteIdSearchTextBoxText("");
                ClearItemEnterResultSearchListView();
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region enterResult Functions
        private void DetailEnterResultClear()
        {
            //Clearing
            SetEnterResultAthleteIdTextBoxText("");
            SetEnterResultNameTextBoxText("");
            SetEnterResultSurnameTextBoxText("");
            SetYearOfBirthTextBoxTextBoxText("");
            SetEnterResultRegionTextBoxText("");
            SetEnterResultClubTextBoxText("");
            SetEnterResultGenderTextBoxText("");
        }
        #endregion

        #region enterResult Thread Safe Functions
        //==============================================
        //==============Setter==========================
        private void SetEnterResultNameTextBoxText(string value)
        {
            if (enterResultNameTextBox.InvokeRequired)
                enterResultNameTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultNameTextBoxText(value)));
            else
            {
                enterResultNameTextBox.Text = value;
            }
        }
        private void SetEnterResultSurnameTextBoxText(string value)
        {
            if (enterResultSurnameTextBox.InvokeRequired)
                enterResultSurnameTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultSurnameTextBoxText(value)));
            else
            {
                enterResultSurnameTextBox.Text = value;
            }
        }
        private void SetEnterResultRegionTextBoxText(string value)
        {
            if (enterResultRegionTextBox.InvokeRequired)
                enterResultRegionTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultRegionTextBoxText(value)));
            else
            {
                enterResultRegionTextBox.Text = value;
            }
        }
        private void SetEnterResultClubTextBoxText(string value)
        {
            if (enterResultClubTextBox.InvokeRequired)
                enterResultClubTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultClubTextBoxText(value)));
            else
            {
                enterResultClubTextBox.Text = value;
            }
        }
        private void SetYearOfBirthTextBoxTextBoxText(string value)
        {
            if (yearOfBirthTextBox.InvokeRequired)
                yearOfBirthTextBox.Invoke((MethodInvoker)
                    (() => SetYearOfBirthTextBoxTextBoxText(value)));
            else
            {
                yearOfBirthTextBox.Text = value;
            }

        }
        private void SetEnterResultAthleteIdTextBoxText(string value)
        {
            if (enterResultAthleteIDTextBox.InvokeRequired)
                enterResultAthleteIDTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultAthleteIdTextBoxText(value)));
            else
            {
                enterResultAthleteIDTextBox.Text = value;
            }
        }
        private void SetEnterResultAthleteIdSearchTextBoxText(string value)
        {
            if (enterResultAthleteIDSearchTextBox.InvokeRequired)
                enterResultAthleteIDSearchTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultAthleteIdSearchTextBoxText(value)));
            else
            {
                enterResultAthleteIDSearchTextBox.Text = value;
            }
        }
        private void SetEnterResultGenderTextBoxText(string value)
        {
            if (enterResultGenderTextBox.InvokeRequired)
                enterResultGenderTextBox.Invoke((MethodInvoker)
                    (() => SetEnterResultGenderTextBoxText(value)));
            else
            {
                enterResultGenderTextBox.Text = value;
            }
        }
        private void ClearItemEnterResultSearchListView()
        {
            if (enterResultSearchListView.InvokeRequired)
                enterResultSearchListView.Invoke((MethodInvoker)
                    (ClearItemEnterResultSearchListView));
            else
            {
                enterResultSearchListView.Items.Clear();
            }
        }
        //================================================
        //==================Getter========================
        private int GetEnterResultAthleteIdSearchTextBoxText()
        {
            var returnVal = 0;
            if (enterResultAthleteIDSearchTextBox.InvokeRequired)
                enterResultAthleteIDSearchTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetEnterResultAthleteIdSearchTextBoxText(); });
            else
            {
                return int.Parse(enterResultAthleteIDSearchTextBox.Text);
            }
            return returnVal;
        }
        private string GetEnterResultSearchFirstNameTextBoxText()
        {
            var returnVal = "";
            if (enterResultSearchFirstNameTextBox.InvokeRequired)
                enterResultSearchFirstNameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetEnterResultSearchFirstNameTextBoxText(); });
            else
            {
                return enterResultSearchFirstNameTextBox.Text;
            }
            return returnVal;
        }
        private string GetEnterResultSearchSurnameTextBoxText()
        {
            var returnVal = "";
            if (enterResultSearchSurnameTextBox.InvokeRequired)
                enterResultSearchSurnameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetEnterResultSearchSurnameTextBoxText(); });
            else
            {
                return enterResultSearchSurnameTextBox.Text;
            }
            return returnVal;
        }
        private int GetEnterResultSearchListViewSelectedIndex()
        {
            var returnVal = 0;
            if (enterResultSearchListView.InvokeRequired)
                enterResultSearchListView.Invoke((MethodInvoker)
                    delegate { returnVal = GetEnterResultSearchListViewSelectedIndex(); });
            else
            {
                int index;

                for (index = 0; index < enterResultSearchListView.Items.Count; index++)
                {
                    //If the index in Row index is true?
                    if (enterResultSearchListView.Items[index].Selected)
                    {
                        //Return the value in the 1 column - This is the ID
                        return int.Parse(enterResultSearchListView.Items[index].SubItems[0].Text);
                    }
                }
            }
            return returnVal;
        }
        //===================================================
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
        private void EnterResultSearchListViewUpdate(IEnumerable<string[]> datas)
        {
            if (datas == null) return;
            if (enterResultSearchListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                enterResultSearchListView.Invoke((MethodInvoker)
                    (() => EnterResultSearchListViewUpdate(datas)));
            else
            {
                var listColumnArray = new string[3];
                foreach (var data in datas)
                {
                    listColumnArray[0] = data[0];
                    listColumnArray[1] = data[1];
                    listColumnArray[2] = data[2];
                    enterResultSearchListView.Items.Add(new ListViewItem(listColumnArray));
                }
            }
        }
        #endregion
        #endregion  
    }
}
