namespace AthleticsDatabase
{
    partial class AddCompetition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddCompetition));
            this.addNewCompetitionGroupBox = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.competitionRegionComboBox = new System.Windows.Forms.ComboBox();
            this.competitionDateTextBox = new System.Windows.Forms.TextBox();
            this.competitionVenueTextBox = new System.Windows.Forms.TextBox();
            this.competitionCountryTextBox = new System.Windows.Forms.TextBox();
            this.competitionNameTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.addCompetitionTypeGroupBox = new System.Windows.Forms.GroupBox();
            this.outdoorRadioButton = new System.Windows.Forms.RadioButton();
            this.indoorRadioButton = new System.Windows.Forms.RadioButton();
            this.addCompetitionHistoryGroupBox = new System.Windows.Forms.GroupBox();
            this.helpLabel3 = new System.Windows.Forms.Label();
            this.competitionHistoryListView = new System.Windows.Forms.ListView();
            this.idColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.competitionNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.venueColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.dateColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.enterButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.competitionHistoryRegionComboBox = new System.Windows.Forms.ComboBox();
            this.regionlabel = new System.Windows.Forms.Label();
            this.competitionHistoryCountryComboBox = new System.Windows.Forms.ComboBox();
            this.seasonlabel = new System.Windows.Forms.Label();
            this.competitionHistorySeasonComboBox = new System.Windows.Forms.ComboBox();
            this.addACompetitionToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addNewCompetitionGroupBox.SuspendLayout();
            this.addCompetitionTypeGroupBox.SuspendLayout();
            this.addCompetitionHistoryGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // addNewCompetitionGroupBox
            // 
            this.addNewCompetitionGroupBox.Controls.Add(this.clearButton);
            this.addNewCompetitionGroupBox.Controls.Add(this.submitButton);
            this.addNewCompetitionGroupBox.Controls.Add(this.competitionRegionComboBox);
            this.addNewCompetitionGroupBox.Controls.Add(this.competitionDateTextBox);
            this.addNewCompetitionGroupBox.Controls.Add(this.competitionVenueTextBox);
            this.addNewCompetitionGroupBox.Controls.Add(this.competitionCountryTextBox);
            this.addNewCompetitionGroupBox.Controls.Add(this.competitionNameTextBox);
            this.addNewCompetitionGroupBox.Controls.Add(this.label5);
            this.addNewCompetitionGroupBox.Controls.Add(this.label4);
            this.addNewCompetitionGroupBox.Controls.Add(this.label3);
            this.addNewCompetitionGroupBox.Controls.Add(this.label2);
            this.addNewCompetitionGroupBox.Controls.Add(this.label1);
            this.addNewCompetitionGroupBox.Controls.Add(this.addCompetitionTypeGroupBox);
            this.addNewCompetitionGroupBox.Location = new System.Drawing.Point(2, 4);
            this.addNewCompetitionGroupBox.Name = "addNewCompetitionGroupBox";
            this.addNewCompetitionGroupBox.Size = new System.Drawing.Size(289, 262);
            this.addNewCompetitionGroupBox.TabIndex = 0;
            this.addNewCompetitionGroupBox.TabStop = false;
            this.addNewCompetitionGroupBox.Text = "Add a new competition";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(148, 226);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(58, 25);
            this.clearButton.TabIndex = 26;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(212, 226);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(57, 25);
            this.submitButton.TabIndex = 25;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.SubmitButtonClick);
            // 
            // competitionRegionComboBox
            // 
            this.competitionRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionRegionComboBox.FormattingEnabled = true;
            this.competitionRegionComboBox.Items.AddRange(new object[] {
            "Leinster",
            "Ulster",
            "Munster",
            "Connacht",
            "All Ireland",
            "Non Irish"});
            this.competitionRegionComboBox.Location = new System.Drawing.Point(129, 134);
            this.competitionRegionComboBox.Name = "competitionRegionComboBox";
            this.competitionRegionComboBox.Size = new System.Drawing.Size(140, 22);
            this.competitionRegionComboBox.TabIndex = 24;
            this.addACompetitionToolTip.SetToolTip(this.competitionRegionComboBox, "Any competition not taking place in Ireland are Non-Irish(region) \r\nand all Natio" +
                    "nal competitions are All-Ireland(region).");
            // 
            // competitionDateTextBox
            // 
            this.competitionDateTextBox.Location = new System.Drawing.Point(129, 200);
            this.competitionDateTextBox.MaxLength = 10;
            this.competitionDateTextBox.Name = "competitionDateTextBox";
            this.competitionDateTextBox.Size = new System.Drawing.Size(94, 20);
            this.competitionDateTextBox.TabIndex = 23;
            this.competitionDateTextBox.Text = "DD/MM/YYYY";
            this.addACompetitionToolTip.SetToolTip(this.competitionDateTextBox, "Please make sure you use the specified layout.\r\nDD/MM/YYYY e.g. 29/12/2011");
            // 
            // competitionVenueTextBox
            // 
            this.competitionVenueTextBox.Location = new System.Drawing.Point(129, 167);
            this.competitionVenueTextBox.Name = "competitionVenueTextBox";
            this.competitionVenueTextBox.Size = new System.Drawing.Size(140, 20);
            this.competitionVenueTextBox.TabIndex = 22;
            // 
            // competitionCountryTextBox
            // 
            this.competitionCountryTextBox.Location = new System.Drawing.Point(129, 100);
            this.competitionCountryTextBox.Name = "competitionCountryTextBox";
            this.competitionCountryTextBox.Size = new System.Drawing.Size(140, 20);
            this.competitionCountryTextBox.TabIndex = 21;
            this.competitionCountryTextBox.Text = "Ireland";
            // 
            // competitionNameTextBox
            // 
            this.competitionNameTextBox.Location = new System.Drawing.Point(129, 67);
            this.competitionNameTextBox.Name = "competitionNameTextBox";
            this.competitionNameTextBox.Size = new System.Drawing.Size(140, 20);
            this.competitionNameTextBox.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(80, 204);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "Date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 14);
            this.label4.TabIndex = 18;
            this.label4.Text = "Venue :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 14);
            this.label3.TabIndex = 17;
            this.label3.Text = "Region :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(65, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Country :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 14);
            this.label1.TabIndex = 15;
            this.label1.Text = "Competition Name :";
            // 
            // addCompetitionTypeGroupBox
            // 
            this.addCompetitionTypeGroupBox.Controls.Add(this.outdoorRadioButton);
            this.addCompetitionTypeGroupBox.Controls.Add(this.indoorRadioButton);
            this.addCompetitionTypeGroupBox.Location = new System.Drawing.Point(55, 20);
            this.addCompetitionTypeGroupBox.Name = "addCompetitionTypeGroupBox";
            this.addCompetitionTypeGroupBox.Size = new System.Drawing.Size(201, 38);
            this.addCompetitionTypeGroupBox.TabIndex = 14;
            this.addCompetitionTypeGroupBox.TabStop = false;
            this.addCompetitionTypeGroupBox.Text = "Competition Type";
            // 
            // outdoorRadioButton
            // 
            this.outdoorRadioButton.AutoSize = true;
            this.outdoorRadioButton.Location = new System.Drawing.Point(117, 13);
            this.outdoorRadioButton.Name = "outdoorRadioButton";
            this.outdoorRadioButton.Size = new System.Drawing.Size(70, 18);
            this.outdoorRadioButton.TabIndex = 3;
            this.outdoorRadioButton.Text = "Outdoor";
            this.outdoorRadioButton.UseVisualStyleBackColor = true;
            // 
            // indoorRadioButton
            // 
            this.indoorRadioButton.AutoSize = true;
            this.indoorRadioButton.Checked = true;
            this.indoorRadioButton.Location = new System.Drawing.Point(14, 13);
            this.indoorRadioButton.Name = "indoorRadioButton";
            this.indoorRadioButton.Size = new System.Drawing.Size(60, 18);
            this.indoorRadioButton.TabIndex = 2;
            this.indoorRadioButton.TabStop = true;
            this.indoorRadioButton.Text = "Indoor";
            this.indoorRadioButton.UseVisualStyleBackColor = true;
            // 
            // addCompetitionHistoryGroupBox
            // 
            this.addCompetitionHistoryGroupBox.Controls.Add(this.helpLabel3);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.competitionHistoryListView);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.enterButton);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.label6);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.competitionHistoryRegionComboBox);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.regionlabel);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.competitionHistoryCountryComboBox);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.seasonlabel);
            this.addCompetitionHistoryGroupBox.Controls.Add(this.competitionHistorySeasonComboBox);
            this.addCompetitionHistoryGroupBox.Location = new System.Drawing.Point(299, 4);
            this.addCompetitionHistoryGroupBox.Name = "addCompetitionHistoryGroupBox";
            this.addCompetitionHistoryGroupBox.Size = new System.Drawing.Size(507, 262);
            this.addCompetitionHistoryGroupBox.TabIndex = 1;
            this.addCompetitionHistoryGroupBox.TabStop = false;
            this.addCompetitionHistoryGroupBox.Text = "Competition History";
            // 
            // helpLabel3
            // 
            this.helpLabel3.ForeColor = System.Drawing.Color.DarkRed;
            this.helpLabel3.Location = new System.Drawing.Point(239, 12);
            this.helpLabel3.Name = "helpLabel3";
            this.helpLabel3.Size = new System.Drawing.Size(258, 57);
            this.helpLabel3.TabIndex = 19;
            this.helpLabel3.Text = "*Select a season, Country and Region followed by the Enter button.\r\nThis will dis" +
                "play all competitions during the season in that Country and region.\r\n";
            // 
            // competitionHistoryListView
            // 
            this.competitionHistoryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.competitionNameColumnHeader,
            this.typeColumnHeader,
            this.venueColumnHeader,
            this.dateColumnHeader});
            this.competitionHistoryListView.FullRowSelect = true;
            this.competitionHistoryListView.GridLines = true;
            this.competitionHistoryListView.Location = new System.Drawing.Point(7, 112);
            this.competitionHistoryListView.MultiSelect = false;
            this.competitionHistoryListView.Name = "competitionHistoryListView";
            this.competitionHistoryListView.Size = new System.Drawing.Size(493, 141);
            this.competitionHistoryListView.TabIndex = 15;
            this.competitionHistoryListView.UseCompatibleStateImageBehavior = false;
            this.competitionHistoryListView.View = System.Windows.Forms.View.Details;
            // 
            // idColumnHeader
            // 
            this.idColumnHeader.Text = "ID";
            this.idColumnHeader.Width = 28;
            // 
            // competitionNameColumnHeader
            // 
            this.competitionNameColumnHeader.Text = "Competition Name";
            this.competitionNameColumnHeader.Width = 129;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "Type";
            this.typeColumnHeader.Width = 88;
            // 
            // venueColumnHeader
            // 
            this.venueColumnHeader.Text = "Venue";
            this.venueColumnHeader.Width = 96;
            // 
            // dateColumnHeader
            // 
            this.dateColumnHeader.Text = "Date";
            this.dateColumnHeader.Width = 66;
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(273, 84);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(87, 25);
            this.enterButton.TabIndex = 14;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.EnterButtonClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 14);
            this.label6.TabIndex = 13;
            this.label6.Text = "Country :";
            // 
            // competitionHistoryRegionComboBox
            // 
            this.competitionHistoryRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionHistoryRegionComboBox.Enabled = false;
            this.competitionHistoryRegionComboBox.FormattingEnabled = true;
            this.competitionHistoryRegionComboBox.Items.AddRange(new object[] {
            "Leinster",
            "Ulster",
            "Munster",
            "Connacht",
            "All Ireland",
            "Non Irish"});
            this.competitionHistoryRegionComboBox.Location = new System.Drawing.Point(94, 83);
            this.competitionHistoryRegionComboBox.Name = "competitionHistoryRegionComboBox";
            this.competitionHistoryRegionComboBox.Size = new System.Drawing.Size(137, 22);
            this.competitionHistoryRegionComboBox.TabIndex = 12;
            // 
            // regionlabel
            // 
            this.regionlabel.AutoSize = true;
            this.regionlabel.Location = new System.Drawing.Point(24, 86);
            this.regionlabel.Name = "regionlabel";
            this.regionlabel.Size = new System.Drawing.Size(51, 14);
            this.regionlabel.TabIndex = 11;
            this.regionlabel.Text = "Region :";
            // 
            // competitionHistoryCountryComboBox
            // 
            this.competitionHistoryCountryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionHistoryCountryComboBox.Enabled = false;
            this.competitionHistoryCountryComboBox.FormattingEnabled = true;
            this.competitionHistoryCountryComboBox.Location = new System.Drawing.Point(94, 52);
            this.competitionHistoryCountryComboBox.Name = "competitionHistoryCountryComboBox";
            this.competitionHistoryCountryComboBox.Size = new System.Drawing.Size(137, 22);
            this.competitionHistoryCountryComboBox.TabIndex = 10;
            // 
            // seasonlabel
            // 
            this.seasonlabel.AutoSize = true;
            this.seasonlabel.Location = new System.Drawing.Point(22, 24);
            this.seasonlabel.Name = "seasonlabel";
            this.seasonlabel.Size = new System.Drawing.Size(53, 14);
            this.seasonlabel.TabIndex = 9;
            this.seasonlabel.Text = "Season :";
            // 
            // competitionHistorySeasonComboBox
            // 
            this.competitionHistorySeasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.competitionHistorySeasonComboBox.Enabled = false;
            this.competitionHistorySeasonComboBox.FormattingEnabled = true;
            this.competitionHistorySeasonComboBox.Location = new System.Drawing.Point(94, 20);
            this.competitionHistorySeasonComboBox.Name = "competitionHistorySeasonComboBox";
            this.competitionHistorySeasonComboBox.Size = new System.Drawing.Size(137, 22);
            this.competitionHistorySeasonComboBox.TabIndex = 8;
            this.competitionHistorySeasonComboBox.SelectedIndexChanged += new System.EventHandler(this.CompetitionHistorySeasonComboBoxSelectedIndexChanged);
            // 
            // addACompetitionToolTip
            // 
            this.addACompetitionToolTip.IsBalloon = true;
            this.addACompetitionToolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.addACompetitionToolTip.ToolTipTitle = "Add A Competition";
            // 
            // AddCompetition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(810, 272);
            this.Controls.Add(this.addCompetitionHistoryGroupBox);
            this.Controls.Add(this.addNewCompetitionGroupBox);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddCompetition";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Athletics Database - Add Competition";
            this.Load += new System.EventHandler(this.AddCompetitionLoad);
            this.addNewCompetitionGroupBox.ResumeLayout(false);
            this.addNewCompetitionGroupBox.PerformLayout();
            this.addCompetitionTypeGroupBox.ResumeLayout(false);
            this.addCompetitionTypeGroupBox.PerformLayout();
            this.addCompetitionHistoryGroupBox.ResumeLayout(false);
            this.addCompetitionHistoryGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox addNewCompetitionGroupBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.ComboBox competitionRegionComboBox;
        private System.Windows.Forms.TextBox competitionDateTextBox;
        private System.Windows.Forms.TextBox competitionVenueTextBox;
        private System.Windows.Forms.TextBox competitionCountryTextBox;
        private System.Windows.Forms.TextBox competitionNameTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox addCompetitionTypeGroupBox;
        private System.Windows.Forms.RadioButton outdoorRadioButton;
        private System.Windows.Forms.RadioButton indoorRadioButton;
        private System.Windows.Forms.GroupBox addCompetitionHistoryGroupBox;
        private System.Windows.Forms.Label seasonlabel;
        private System.Windows.Forms.ComboBox competitionHistorySeasonComboBox;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox competitionHistoryRegionComboBox;
        private System.Windows.Forms.Label regionlabel;
        private System.Windows.Forms.ComboBox competitionHistoryCountryComboBox;
        private System.Windows.Forms.ListView competitionHistoryListView;
        private System.Windows.Forms.ColumnHeader idColumnHeader;
        private System.Windows.Forms.ColumnHeader competitionNameColumnHeader;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.ColumnHeader venueColumnHeader;
        private System.Windows.Forms.ColumnHeader dateColumnHeader;
        private System.Windows.Forms.ToolTip addACompetitionToolTip;
        private System.Windows.Forms.Label helpLabel3;

    }
}