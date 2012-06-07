using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;

namespace AthleticsDatabase
{
    /// <summary>Main Form</summary>
    public partial class MainForm : Form
    {
        //private static readonly MsAccessDatabase DatabaseAccess = new MsAccessDatabase("athleticsDatabase");
        private static SqLiteDatabase _databaseAccess;

        #region Main Form
        /// <summary>Main form</summary>
        public MainForm()
        {
            try
            {
                InitializeComponent();
                _databaseAccess = new SqLiteDatabase("athleticsDatabase");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MainFormLoad(object sender, EventArgs e)
        {
            try
            {
                //Special UseOnly
                //DatabaseAccess.UpdateDatabase(2);
                //=====================
                if (_databaseAccess == null) Close();
                var oThread = new Thread(LoadLeaderboardData);
                var oThreadTwo = new Thread(CheckInternet);
                var oThreadThree = new Thread(UpdateDatabase);
                oThread.Start();
                oThreadTwo.Start();
                oThreadThree.Start();
                versionStripLabel.Text = @"Version: " + Application.ProductVersion;
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CheckInternet()
        {
            networkStatusLabel.Text = NetworkAccess.IsInternetAvailable() ? @"Online" : @"Offline";
            if (networkStatusLabel.Text == "Offline") return;
            EnableMenuStripItems();
            Thread.CurrentThread.Abort();
        }
        private void EnableMenuStripItems()
        {
            if (menuStrip.InvokeRequired)
                menuStrip.Invoke((MethodInvoker)
                    EnableMenuStripItems);
            else
            {
                checkForUpdateToolStripMenuItem.Enabled = true;
                donateToolStripMenuItem.Enabled = true;
            }
        }
        private void UpdateDatabase()
        {
            while (networkStatusLabel.Text == "Checking Connection.."){}
            if (networkStatusLabel.Text == "Offline") return;
            try
            {
                //xml location
                var xmlReader = XmlReader.Create("http://www.olaegbe.com/athleticsDatabase.xml");
                //read and stop at lateVersion element
                xmlReader.Read();
                xmlReader.ReadToFollowing("latestDBVersion");
                //Read the contents of latest version to xmlVersion string
                var xmlVersion = xmlReader.ReadString();
                xmlReader.ReadToFollowing("latestDBDownload");
                var xmlDownloadLink = xmlReader.ReadString();
                xmlReader.Close();

                //comparing version
                var v = _databaseAccess.GetDatabase();
                if (Int32.Parse(xmlVersion) >  v)
                {
                    if (ShowMessageBoxReturn(@"Database Update found! " + Environment.NewLine + @"New Version: " + xmlVersion +
                        Environment.NewLine + @"Would you like to download?", @"Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        var webclient = new WebClient();
                        //=========================================
                        //Rename the current exe to temp
                        var newPath = Path.Combine(Application.StartupPath, "athleticsDatabase.db");
                        _databaseAccess.Close();
                        if (File.Exists(newPath)) File.Delete(newPath);
                        //==========================================
                        //Download file
                        webclient.DownloadFileAsync(new Uri(xmlDownloadLink), "athleticsDatabase.db");
                        _databaseAccess.Open();
                        ShowMessageBox(@"Database Updated.", @"Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Thread.CurrentThread.Abort();
            }
        }

        /*
        /// <summary>Get current MS Database</summary>
        public static MsAccessDatabase Connection()
        {
            return DatabaseAccess;
        }
         */
        /// <summary>Get current SQLite Database</summary>
        public static SqLiteDatabase Connection()
        {
            return _databaseAccess;
        }
        /// <summary>Enable Edit Options</summary>
        public static bool EnabledMenu
        {
            set
            {
                addAthleteToolStripMenuItem.Enabled = value;
                addClubToolStripMenuItem.Enabled = value;
                addCompetitionToolStripMenuItem.Enabled = value;
                enterResultsToolStripMenuItem.Enabled = value;
            }
        }
        /// <summary>Allowed authorised users to login</summary>
        public static string LoginText
        {
            set { logInToolStripMenuItem.Text = value; }
        }
        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();//Immidiable stop the process
        }
        #endregion

        #region KeyPress Events
        private void ProfileAthleteIdSearchTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (!VariousFunctions.IsNumeric(profileAthleteIDSearchTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DetailProfileClear();
                var oThread = new Thread(LoadProfileSearch);
                var oThreadTwo = new Thread(LoadCompetitionYears);

                profileAthleteIDTextBox.Text = profileAthleteIDSearchTextBox.Text;
                profileCompetitionHistoryListView.Items.Clear();
                profileSeasonComboBox.Items.Clear();
                profileEventComboBox.Items.Clear();
                profileSearchFirstNameTextBox.Clear();
                profileSearchSurnameTextBox.Clear();

                profileSeasonComboBox.Text = null;
                profileEventComboBox.Text = null;
                
                profileEditButton.Enabled = true;

                oThread.Start();
                oThreadTwo.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProfileSearchFirstNameTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (VariousFunctions.IsNumeric(profileSearchFirstNameTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                profileSearchListView.Items.Clear();
                var oThread = new Thread(LoadProfileNamesToListView);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ProfileSearchSurnameTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                if (VariousFunctions.IsNumeric(profileSearchSurnameTextBox.Text))
                {
                    ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                profileSearchListView.Items.Clear();
                var oThread = new Thread(LoadProfileNamesToListView);
                oThread.Start();
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Menustrip Click
        private void WeblinkStripStatusLabelClick(object sender, EventArgs e)
        {
            Process.Start("http://www.olaegbe.com/");
        }
        private void AboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newform = new AboutBox();
            newform.ShowDialog(this);
        }
        private void ToolStripMenuItem2Click(object sender, EventArgs e)
        {
            ShowMessageBox(string.Format("Not Implemented"), string.Format("Athletics Database"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }
        private void AddAthleteToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var newform = new AddAthlete();
            newform.ShowDialog(this);
        }
        private void AddClubToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newform = new AddClub();
            newform.ShowDialog(this);
        }
        private void AddCompetitionToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newform = new AddCompetition();
            newform.ShowDialog(this);
        }
        private void EnterResultsToolStripMenuItem1Click(object sender, EventArgs e)
        {
            var newform = new EnterResult();
            newform.ShowDialog(this);
        }
        private void CheckForUpdateToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newform = new UpdateForm();
            newform.ShowDialog(this);
        }
        private void SpecialThanksToolStripMenuItemClick(object sender, EventArgs e)
        {
            var newform = new SpecialThanks();
            newform.ShowDialog(this); // freeze the parent form while childmis enabled
        }
        private void LogInToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (logInToolStripMenuItem.Text == @"&Log-In")
            {
                var newform = new LoginFom();
                newform.ShowDialog(this);
            }
            else
            {
                EnabledMenu = false;
                logInToolStripMenuItem.Text = @"&Log-In";
            }
        }
        #endregion
        //=============================
        //  Leaderboard Tab Section //
        //==========================
        #region Leaderboard Tab
        #region Button Click Events
        private void LeaderboadrOutdoorRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            var oThread = new Thread(LoadLeaderboardData);
            LeaderboardClean();
            oThread.Start();
        }
        private void LeaderboardPopulateButtonClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(leaderboardSeasonComboBox.Text))
            {
                ShowMessageBox(@"Please select a season.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(leaderboardRegionComboBox.Text))
            {
                ShowMessageBox(@"Please select a region.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(leaderboardAgeGroupComboBox.Text))
            {
                ShowMessageBox(@"Please select an age group.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(leaderboardEventsComboBox.Text))
            {
                ShowMessageBox(@"Please select an event.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oThread = new Thread(LoadLeaderboardTable);
            leaderboardListView.Items.Clear();
            oThread.Start();
        }
        #endregion

        #region Thread Work
        private void LoadLeaderboardData()
        {
            try
            {
                //if Leaderboad tab indoor radio button checked set the competition type
                var competitionType = leaderboadrIndoorRadioButton.Checked ?
                    Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;

                //Get competition year function
                var years = _databaseAccess.GetCompetitionYear(competitionType);
                foreach (var year in years)
                {
                    //calling a thread safty function
                    LeaderboardAddItems(year, "leaderboardSeasonComboBox");
                }
                //list all the events functions
                var events = _databaseAccess.ListAllEvents(competitionType);
                foreach (var singleEvent in events)
                {
                    //calling a thread safty function
                    LeaderboardAddItems(singleEvent, "leaderboardEventsComboBox");
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLeaderboardTable()
        {
            try
            {
                //if Leaderboad tab indoor radio button checked set the competition type
                var competitionType = leaderboadrIndoorRadioButton.Checked ?
                    Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;
                //Get selected gender
                var gender = leaderboardMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                //Using thread safety to get and load data
                var leaderboardData = _databaseAccess.GetLeaderboard
                    (gender, LeaderboardSeasonComboboxAccess(), LeaderboardEventComboboxAccess(),
                    LeaderboardRegionComboboxAccess(), competitionType, LeaderboardAgeComboboxAccess());
                if (leaderboardData == null)
                {
                    ShowMessageBox(@"No Results Found", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                LeaderboardListViewUpdate(leaderboardData);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Leaderboard Thread Safe Functions
        private void LeaderboardAddItems(string item,string value)
        {
            switch (value)
            {
                case "leaderboardSeasonComboBox":
                    if (leaderboardSeasonComboBox.InvokeRequired)
                        //lambda expression empty delegate that calls a recursive function if InvokeRequired
                        leaderboardSeasonComboBox.Invoke((MethodInvoker)(() => LeaderboardAddItems(item, value)));
                    else leaderboardSeasonComboBox.Items.Add(item);
                    break;
                case "leaderboardEventsComboBox":
                    if (leaderboardEventsComboBox.InvokeRequired)
                        //lambda expression empty delegate that calls a recursive function if InvokeRequired
                        leaderboardEventsComboBox.Invoke((MethodInvoker)(() => LeaderboardAddItems(item, value)));
                    else leaderboardEventsComboBox.Items.Add(item);
                    break;
            }
        }
        private Enumerations.Region LeaderboardRegionComboboxAccess()
        {
            var returnVal = Enumerations.Region.Munster;
            if (leaderboardRegionComboBox.InvokeRequired)
                leaderboardRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = LeaderboardRegionComboboxAccess(); });
            else
            {
                if (leaderboardRegionComboBox.Text == @"Leinster")
                    return Enumerations.Region.Leinster;
                if (leaderboardRegionComboBox.Text == @"Ulster")
                    return Enumerations.Region.Ulster;
                if (leaderboardRegionComboBox.Text == @"Connacht")
                    return Enumerations.Region.Connacht;
                return leaderboardRegionComboBox.Text == @"Munster"
                           ? Enumerations.Region.Munster
                           : Enumerations.Region.AllIreland;
            }
            return returnVal;
        }
        private string LeaderboardSeasonComboboxAccess()
        {
            var returnVal = "";
            if (leaderboardSeasonComboBox.InvokeRequired)
                leaderboardSeasonComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = LeaderboardSeasonComboboxAccess(); });
            else
            {
                return leaderboardSeasonComboBox.Text;
            }
            return returnVal;
        }
        private string LeaderboardEventComboboxAccess()
        {
            var returnVal = "";
            if (leaderboardEventsComboBox.InvokeRequired)
                leaderboardEventsComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = LeaderboardEventComboboxAccess(); });
            else
            {
                return leaderboardEventsComboBox.Text;
            }
            return returnVal;
        }
        private string LeaderboardAgeComboboxAccess()
        {
            var returnVal = "";
            if (leaderboardAgeGroupComboBox.InvokeRequired)
                leaderboardAgeGroupComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = LeaderboardAgeComboboxAccess(); });
            else
            {
                return leaderboardAgeGroupComboBox.Text;
            }
            return returnVal;
        }
        private void LeaderboardListViewUpdate(IEnumerable<string[]> datas)
        {
            if (datas == null) return;
            if (leaderboardListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                leaderboardListView.Invoke((MethodInvoker)(() => LeaderboardListViewUpdate(datas)));
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
                    listColumnArray[5] = data[5];
                    listColumnArray[6] = data[6];
                    leaderboardListView.Items.Add(new ListViewItem(listColumnArray));
                }
            }
        }
        #endregion

        #region Leaderboard Clean / Reset Function
        private void LeaderboardClean()
        {
            leaderboardSeasonComboBox.Items.Clear();
            leaderboardEventsComboBox.Items.Clear();
            leaderboardAgeGroupComboBox.Text = null;
            leaderboardRegionComboBox.Text = null;
            leaderboardEventsComboBox.Text = null;
        }
        #endregion
        #endregion
        //=========================
        //  Profile Tab Section //
        //=======================
        #region Profile Tab
        #region Button Click Event
        private void ProfileClearButtonClick(object sender, EventArgs e)
        {
            profileAthleteIDTextBox.Clear();
            profileGenderTextBox.Clear();
            profileNameTextBox.Clear();
            profileSurnameTextBox.Clear();
            yearOfBirthTextBox.Clear();
            profileRegionComboBox.Text = null;
            profileClubComboBox.Text = null;
            profileClubComboBox.Items.Clear();
            profileRegionComboBox.Enabled = false;
            profileClubComboBox.Enabled = false;
            yearOfBirthTextBox.Enabled = false;
            profileSaveButton.Enabled = false;
            profileEditButton.Enabled = false;
            profileEventComboBox.Enabled = false;
            profileEnterButton.Enabled = false;
            profileCompetitionHistoryListView.Items.Clear();
            profileSeasonComboBox.Items.Clear();
            profileSeasonComboBox.Text = null;
            profileEventComboBox.Text = null;
            profileEventComboBox.Items.Clear();
        }
        private void ProfileEditButtonClick(object sender, EventArgs e)
        {
            profileClubComboBox.Items.Clear();
            var oThread = new Thread(LoadProfileClubs);
            oThread.Start();
            profileRegionComboBox.Enabled = true;
            profileClubComboBox.Enabled = true;
            yearOfBirthTextBox.Enabled = true;
            profileSaveButton.Enabled = true;
        }
        private void ProfileRegionComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(profileClubComboBox.Text)) return;
            profileClubComboBox.Items.Clear();
            profileClubComboBox.Text = null;
            var oThread = new Thread(LoadProfileClubs);
            oThread.Start();
        }
        private void ProfileSearchListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            var oThread = new Thread(LoadProfileSearchFromListView);
            var oThreadTwo = new Thread(LoadCompetitionYears);
            DetailProfileClear();
            profileEventComboBox.Enabled = false;
            profileEnterButton.Enabled = false;
            profileCompetitionHistoryListView.Items.Clear();
            profileSeasonComboBox.Items.Clear();
            profileSeasonComboBox.Text = null;
            profileEventComboBox.Text = null;
            profileEventComboBox.Items.Clear();
            profileEditButton.Enabled = true;
            profileSearchFirstNameTextBox.Clear();
            profileSearchSurnameTextBox.Clear();
            oThread.Start();
            oThreadTwo.Start();
        }
        private void ProfileSeasonComboBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(profileSeasonComboBox.Text)) return;

            //Get selected competition Type
            var competitionType = profileIndoorRadioButton.Checked ?
                Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;
            //Get selected gender
            var gender = profileGenderTextBox.Text == @"Male" ?
                Enumerations.Gender.Male : Enumerations.Gender.Female;

            var events = _databaseAccess.ListEventPerAthletePerSeason
                (competitionType, gender, int.Parse(profileAthleteIDTextBox.Text), profileSeasonComboBox.Text);

            if (events == null) return;
            profileEventComboBox.Text = null;
            profileEventComboBox.Items.Clear();
            foreach (var anEvent in events)
            {
                profileEventComboBox.Items.Add(anEvent);
            }
            profileEventComboBox.Enabled = true;
            profileEnterButton.Enabled = true;
        }
        private void ProfileIndoorRadioButtonCheckedChanged(object sender, EventArgs e)
        {
            var oThread = new Thread(LoadCompetitionYears);
            //If the Gender textbox is empty - we have no athlete selected
            if (string.IsNullOrEmpty(profileGenderTextBox.Text)) return;
            oThread.Start();
            profileSeasonComboBox.Text = null;
            profileSeasonComboBox.Items.Clear();
            profileEventComboBox.Text = null;
            profileEventComboBox.Enabled = false;
            profileEnterButton.Enabled = false;
        }
        private void ProfileEnterButtonClick(object sender, EventArgs e)
        {
            var oThread = new Thread(LoadProfileCompetitionHistoryListView);
            oThread.Start();
        }
        private void ProfileEnterAthletesIdButtonClick(object sender, EventArgs e)
        {
            if (!VariousFunctions.IsNumeric(profileAthleteIDSearchTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var oThread = new Thread(LoadProfileSearch);
            var oThreadTwo = new Thread(LoadCompetitionYears);
            DetailProfileClear();
            profileAthleteIDTextBox.Text = profileAthleteIDSearchTextBox.Text;
            profileEventComboBox.Enabled = false;
            profileEnterButton.Enabled = false;
            profileCompetitionHistoryListView.Items.Clear();
            profileSeasonComboBox.Items.Clear();
            profileSeasonComboBox.Text = null;
            profileEventComboBox.Text = null;
            profileEventComboBox.Items.Clear();
            profileEditButton.Enabled = true;
            profileSearchFirstNameTextBox.Clear();
            profileSearchSurnameTextBox.Clear();

            oThread.Start();
            oThreadTwo.Start();
        }
        private void ProfileSearchButtonClick(object sender, EventArgs e)
        {
            if (VariousFunctions.IsNumeric(profileSearchFirstNameTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (VariousFunctions.IsNumeric(profileSearchSurnameTextBox.Text))
            {
                ShowMessageBox(@"Invalid ID Value.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            profileSearchListView.Items.Clear();
            var oThread = new Thread(LoadProfileNamesToListView);
            oThread.Start();
        }
        private void ProfileClearSearchResultsButtonClick(object sender, EventArgs e)
        {
            profileSearchListView.Items.Clear();
            profileSearchFirstNameTextBox.Clear();
            profileSearchSurnameTextBox.Clear();
        }
        private void ProfileSaveButtonClick(object sender, EventArgs e)
        {
            var oThread = new Thread(EditAthlete);
            oThread.Start();
        }
        #endregion

        #region Thread Work
        private void LoadProfileSearch()
        {
            try
            {
                //Get selected gender
                var gender = profileSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;
                
                var athleteInfo = _databaseAccess.GetAthlete(GetProfileAthleteIdSearchTextBoxText(), gender);
                if (athleteInfo == null) return;

                SetProfileGenderTextBoxText(gender.ToString());
                SetProfileAthleteIdTextBoxText(athleteInfo[0]);
                SetProfileNameTextBoxText(athleteInfo[1]);
                SetProfileSurnameTextBoxText(athleteInfo[2]);
                SetYearOfBirthTextBoxText(athleteInfo[3]);
                SetProfileRegionComboBoxText(athleteInfo[4]);
                SetProfileClubComboBoxText(athleteInfo[5]);
                SetProfileAthleteIdSearchTextBoxText("");
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProfileNamesToListView()
        {
            try
            {
                //Get selected gender
                var gender = profileSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var athleteNames = _databaseAccess.SearchAthletes(GetProfileSearchFirstNameTextBoxText(),
                    GetProfileSearchSurnameTextBoxText(), gender);
                if (athleteNames == null) return;
                ProfileSearchListViewUpdate(athleteNames);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProfileCompetitionHistoryListView()
        {
            try
            {
                ClearItemProfileCompetitionHistoryListView();

                //Get selected competition Type
                var competitionType = profileIndoorRadioButton.Checked ?
                    Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;
                //Get selected gender
                var gender = profileGenderTextBox.Text == @"Male" ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var history = _databaseAccess.ListAthleteEventHistory
                    (competitionType, gender, int.Parse(GetProfileAthleteIdTextBoxText()),
                     GetProfileSeasonComboBoxText(), GetProfileEventComboBoxText());
                if (history == null) return;
                ProfileCompetitionHistoryListViewUpdate(history);
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProfileSearchFromListView()
        {
            try
            {
                //Get selected gender
                var gender = profileSearchMaleRadioButton.Checked ?
                    Enumerations.Gender.Male : Enumerations.Gender.Female;

                var athleteInfo = _databaseAccess.GetAthlete(GetProfileSearchListViewSelectedIndex(), gender);
                if (athleteInfo == null) return;
                SetProfileAthleteIdTextBoxText(athleteInfo[0]);
                SetProfileNameTextBoxText(athleteInfo[1]);
                SetProfileSurnameTextBoxText(athleteInfo[2]);
                SetYearOfBirthTextBoxText(athleteInfo[3]);
                SetProfileRegionComboBoxText(athleteInfo[4]);
                SetProfileClubComboBoxText(athleteInfo[5]);
                SetProfileGenderTextBoxText(profileSearchMaleRadioButton.Checked ? "Male" : "Female");
                SetProfileAthleteIdSearchTextBoxText("");
                ClearItemProfileSearchListView();
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadProfileClubs()
        {
            try
            {
                //Get selected region ID
                var regionId = Connection().GetRegionId(GetProfileRegionComboBoxText());
                var listOfClubs = _databaseAccess.ListAllClubs(regionId);

                foreach (var club in listOfClubs)
                {
                    SetProfileClubComboBoxItems(club[1]);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadCompetitionYears()
        {
            try
            {
                //if Leaderboad tab indoor radio button checked set the competition type
                var competitionType = profileIndoorRadioButton.Checked ?
                    Enumerations.CompetitionType.Indoor : Enumerations.CompetitionType.Outdoor;

                //Get competition year function
                var years = _databaseAccess.GetCompetitionYear(competitionType);
                foreach (var year in years)
                {
                    //calling a thread safty function
                    SetProfileSeasonComboBoxItems(year);
                }
            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void EditAthlete()
        {
            try
            {
                if (!VariousFunctions.IsNumeric(GetYearOfBirthTextBoxText()))
                {
                    ShowMessageBox(@"Numeric Values only Please.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (String.IsNullOrEmpty(GetProfileClubComboBoxText()))
                {
                    ShowMessageBox(@"Please select a club", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                var messageString = @"Region: " + GetProfileRegionComboBoxText ()+ Environment.NewLine +
                "Club Name: " + GetProfileClubComboBoxText() + Environment.NewLine +
                "Year of Birth: " + GetYearOfBirthTextBoxText() + Environment.NewLine;

                if (ShowMessageBoxReturn(messageString, @"Athletics Database", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK) return;
                _databaseAccess.UpdateAthleteInfo(Int32.Parse(GetProfileAthleteIdTextBoxText()),GetProfileGenderTextBoxText(), 
                    GetProfileRegionComboBoxText(), GetProfileClubComboBoxText(), Int32.Parse(GetYearOfBirthTextBoxText()));

                ShowMessageBox(@"Edited Successfully.", @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception e)
            {
                ShowMessageBox(e.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Profile Functions
        private void DetailProfileClear()
        {
            //Clearing
            profileAthleteIDTextBox.Text = null;
            profileNameTextBox.Text = null;
            profileSurnameTextBox.Text = null;
            yearOfBirthTextBox.Text = null;
            profileRegionComboBox.Text = null;
            profileClubComboBox.Text = null;
            profileGenderTextBox.Text = null;
        }
        #endregion

        #region Profile Thread Safe Functions
        //==============================================
        //==============Setter==========================
        private void SetProfileNameTextBoxText(string value)
        {
            if (profileNameTextBox.InvokeRequired)
                profileNameTextBox.Invoke((MethodInvoker)
                    delegate{ SetProfileNameTextBoxText(value);});
            else
            {
                profileNameTextBox.Text = value;
                profileNameTextBox.Invalidate();
            }
        }
        private void SetProfileSurnameTextBoxText(string value)
        {
            if (profileSurnameTextBox.InvokeRequired)
                profileSurnameTextBox.Invoke((MethodInvoker)
                    delegate { SetProfileSurnameTextBoxText(value);});
            else
            {
                profileSurnameTextBox.Text = value;
                profileSurnameTextBox.Invalidate();
            }
        }
        private void SetProfileRegionComboBoxText(string value)
        {
            if (profileRegionComboBox.InvokeRequired)
                profileRegionComboBox.Invoke((MethodInvoker)
                    delegate { SetProfileRegionComboBoxText(value); });
            else
            {
                profileRegionComboBox.Text = value;
                profileRegionComboBox.Invalidate();
            }
        }
        private void SetProfileClubComboBoxText(string value)
        {
            if (profileClubComboBox.InvokeRequired)
                profileClubComboBox.Invoke((MethodInvoker)
                    delegate { SetProfileClubComboBoxText(value); });
            else
            {
                profileClubComboBox.Text = value;
            }
        }
        private void SetProfileClubComboBoxItems(string value)
        {
            if (profileClubComboBox.InvokeRequired)
                profileClubComboBox.Invoke((MethodInvoker)
                    delegate { SetProfileClubComboBoxItems(value); });
            else
            {
                profileClubComboBox.Items.Add(value);
            }
        }
        private void SetProfileSeasonComboBoxItems(string value)
        {
            if (profileSeasonComboBox.InvokeRequired)
                profileSeasonComboBox.Invoke((MethodInvoker)
                    delegate { SetProfileSeasonComboBoxItems(value); });
            else
            {
                profileSeasonComboBox.Items.Add(value);
            }
        }
        private void SetYearOfBirthTextBoxText(string value)
        {
            if (yearOfBirthTextBox.InvokeRequired)
                yearOfBirthTextBox.Invoke((MethodInvoker)
                    delegate { SetYearOfBirthTextBoxText(value); });
            else
            {
                yearOfBirthTextBox.Text = value;
                yearOfBirthTextBox.Invalidate();
            }
        }
        private void SetProfileAthleteIdTextBoxText(string value)
        {
            if (profileAthleteIDTextBox.InvokeRequired)
                profileAthleteIDTextBox.Invoke((MethodInvoker)
                    delegate { SetProfileAthleteIdTextBoxText(value); });
            else
            {
                profileAthleteIDTextBox.Text = value;
                profileAthleteIDTextBox.Invalidate();
            }
        }
        private void SetProfileAthleteIdSearchTextBoxText(string value)
        {
            if (profileAthleteIDSearchTextBox.InvokeRequired)
                profileAthleteIDSearchTextBox.Invoke((MethodInvoker)
                    delegate { SetProfileAthleteIdSearchTextBoxText(value); });
            else
            {
                profileAthleteIDSearchTextBox.Text = value;
                profileAthleteIDSearchTextBox.Invalidate();
            }
        }
        private void SetProfileGenderTextBoxText(string value)
        {
            if (profileGenderTextBox.InvokeRequired)
                profileGenderTextBox.Invoke((MethodInvoker)
                    delegate { SetProfileGenderTextBoxText(value); });
            else
            {
                profileGenderTextBox.Text = value;
                profileGenderTextBox.Invalidate();
            }
        }
        private void ClearItemProfileSearchListView()
        {
            if (profileSearchListView.InvokeRequired)
                profileSearchListView.Invoke((MethodInvoker)
                    ClearItemProfileSearchListView);
            else
            {
                profileSearchListView.Items.Clear();
            }
        }
        private void ClearItemProfileCompetitionHistoryListView()
        {
            if (profileCompetitionHistoryListView.InvokeRequired)
                profileCompetitionHistoryListView.Invoke((MethodInvoker)
                    ClearItemProfileCompetitionHistoryListView);
            else
            {
                profileCompetitionHistoryListView.Items.Clear();
            }
        }
        //================================================
        //==================Getter========================
        private int GetProfileAthleteIdSearchTextBoxText()
        {
            var returnVal = 0;
            if (profileAthleteIDSearchTextBox.InvokeRequired)
                profileAthleteIDSearchTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileAthleteIdSearchTextBoxText(); });
            else
            {
                return int.Parse(profileAthleteIDSearchTextBox.Text);
            }
            return returnVal;
        }
        private string GetProfileSeasonComboBoxText()
        {
            var returnVal = "";
            if (profileSeasonComboBox.InvokeRequired)
                profileSeasonComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileSeasonComboBoxText(); });
            else
            {
                return profileSeasonComboBox.Text;
            }
            return returnVal;
        }
        private string GetProfileEventComboBoxText()
        {
            var returnVal = "";
            if (profileEventComboBox.InvokeRequired)
                profileEventComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileEventComboBoxText(); });
            else
            {
                return profileEventComboBox.Text;
            }
            return returnVal;
        }
        private string GetProfileRegionComboBoxText()
        {
            var returnVal = "";
            if (profileRegionComboBox.InvokeRequired)
                profileRegionComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileRegionComboBoxText(); });
            else
            {
                return profileRegionComboBox.Text;
            }
            return returnVal;
        }
        private string GetProfileClubComboBoxText()
        {
            var returnVal = "";
            if (profileClubComboBox.InvokeRequired)
                profileClubComboBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileClubComboBoxText(); });
            else
            {
                return profileClubComboBox.Text;
            }
            return returnVal;
        }
        private string GetYearOfBirthTextBoxText()
        {
            var returnVal = "";
            if (yearOfBirthTextBox.InvokeRequired)
                yearOfBirthTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetYearOfBirthTextBoxText(); });
            else
            {
                return yearOfBirthTextBox.Text;
            }
            return returnVal;
        }
        private string GetProfileAthleteIdTextBoxText()
        {
            var returnVal = "";
            if (profileAthleteIDTextBox.InvokeRequired)
                profileAthleteIDTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileAthleteIdTextBoxText(); });
            else
            {
                return profileAthleteIDTextBox.Text;
            }
            return returnVal;
        }
        private string GetProfileGenderTextBoxText()
        {
            var returnVal = "";
            if (profileGenderTextBox.InvokeRequired)
                profileGenderTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileGenderTextBoxText(); });
            else
            {
                return profileGenderTextBox.Text;
            }
            return returnVal;
        }
        private string GetProfileSearchFirstNameTextBoxText()
        {
            var returnVal = "";
            if (profileSearchFirstNameTextBox.InvokeRequired)
                profileSearchFirstNameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileSearchFirstNameTextBoxText(); });
            else
            {
                return profileSearchFirstNameTextBox.Text;
            }
            return returnVal;
        }
        private string GetProfileSearchSurnameTextBoxText()
        {
            var returnVal = "";
            if (profileSearchSurnameTextBox.InvokeRequired)
                profileSearchSurnameTextBox.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileSearchSurnameTextBoxText(); });
            else
            {
                return profileSearchSurnameTextBox.Text;
            }
            return returnVal;
        }
        private int GetProfileSearchListViewSelectedIndex()
        {
            var returnVal = 0;
            if (profileSearchListView.InvokeRequired)
                profileSearchListView.Invoke((MethodInvoker)
                    delegate { returnVal = GetProfileSearchListViewSelectedIndex(); });
            else
            {
                int index;
                
                for (index = 0; index < profileSearchListView.Items.Count; index++)
                {
                    //If the index in Row index is true?
                    if (profileSearchListView.Items[index].Selected)
                    {
                        //Return the value in the 1 column - This is the ID
                        return int.Parse(profileSearchListView.Items[index].SubItems[0].Text);
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
        private void ProfileSearchListViewUpdate(IEnumerable<string[]> datas)
        {
            if (datas == null) return;
            if (profileSearchListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                profileSearchListView.Invoke((MethodInvoker)
                    (() => ProfileSearchListViewUpdate(datas)));
            else
            {
                var listColumnArray = new string[3];
                foreach (var data in datas)
                {
                    listColumnArray[0] = data[0];
                    listColumnArray[1] = data[1];
                    listColumnArray[2] = data[2];
                    profileSearchListView.Items.Add(new ListViewItem(listColumnArray));
                }
            }
        }
        private void ProfileCompetitionHistoryListViewUpdate(IEnumerable<string[]> datas)
        {
            if (datas == null) return;
            if (profileCompetitionHistoryListView.InvokeRequired)
                //lambda expression empty delegate that calls a recursive function if InvokeRequired
                profileCompetitionHistoryListView.Invoke((MethodInvoker)
                    (() => ProfileCompetitionHistoryListViewUpdate(datas)));
            else
            {
                var listColumnArray = new string[4];
                foreach (var data in datas)
                {
                    listColumnArray[0] = data[0];
                    listColumnArray[1] = data[1];
                    listColumnArray[2] = data[2];
                    listColumnArray[3] = data[3];
                    profileCompetitionHistoryListView.Items.Add(new ListViewItem(listColumnArray));
                }
            }
        }
        #endregion
        #endregion
    }
}
