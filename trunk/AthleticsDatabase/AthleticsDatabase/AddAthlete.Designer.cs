namespace AthleticsDatabase
{
    /// <summary>Add athletes into the database</summary>
    partial class AddAthlete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddAthlete));
            this.addAthleteMaleRadioButton = new System.Windows.Forms.RadioButton();
            this.addAthleteFemaleRadioButton = new System.Windows.Forms.RadioButton();
            this.addAthleteNameTextBox = new System.Windows.Forms.TextBox();
            this.addAthleteSurnameTextBox = new System.Windows.Forms.TextBox();
            this.addAthleteRegionComboBox = new System.Windows.Forms.ComboBox();
            this.addAthleteClubComboBox = new System.Windows.Forms.ComboBox();
            this.addAthleteClearButton = new System.Windows.Forms.Button();
            this.addAthleteSubmitButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.addAthleteYearOfBirthTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // addAthleteMaleRadioButton
            // 
            this.addAthleteMaleRadioButton.AutoSize = true;
            this.addAthleteMaleRadioButton.Checked = true;
            this.addAthleteMaleRadioButton.Location = new System.Drawing.Point(135, 12);
            this.addAthleteMaleRadioButton.Name = "addAthleteMaleRadioButton";
            this.addAthleteMaleRadioButton.Size = new System.Drawing.Size(50, 18);
            this.addAthleteMaleRadioButton.TabIndex = 0;
            this.addAthleteMaleRadioButton.TabStop = true;
            this.addAthleteMaleRadioButton.Text = "Male";
            this.addAthleteMaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // addAthleteFemaleRadioButton
            // 
            this.addAthleteFemaleRadioButton.AutoSize = true;
            this.addAthleteFemaleRadioButton.Location = new System.Drawing.Point(237, 12);
            this.addAthleteFemaleRadioButton.Name = "addAthleteFemaleRadioButton";
            this.addAthleteFemaleRadioButton.Size = new System.Drawing.Size(63, 18);
            this.addAthleteFemaleRadioButton.TabIndex = 1;
            this.addAthleteFemaleRadioButton.Text = "Female";
            this.addAthleteFemaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // addAthleteNameTextBox
            // 
            this.addAthleteNameTextBox.Location = new System.Drawing.Point(85, 50);
            this.addAthleteNameTextBox.Name = "addAthleteNameTextBox";
            this.addAthleteNameTextBox.Size = new System.Drawing.Size(137, 20);
            this.addAthleteNameTextBox.TabIndex = 2;
            // 
            // addAthleteSurnameTextBox
            // 
            this.addAthleteSurnameTextBox.Location = new System.Drawing.Point(85, 77);
            this.addAthleteSurnameTextBox.Name = "addAthleteSurnameTextBox";
            this.addAthleteSurnameTextBox.Size = new System.Drawing.Size(137, 20);
            this.addAthleteSurnameTextBox.TabIndex = 3;
            // 
            // addAthleteRegionComboBox
            // 
            this.addAthleteRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addAthleteRegionComboBox.FormattingEnabled = true;
            this.addAthleteRegionComboBox.Items.AddRange(new object[] {
            "Leinster",
            "Ulster",
            "Munster",
            "Connacht",
            "All Ireland"});
            this.addAthleteRegionComboBox.Location = new System.Drawing.Point(310, 48);
            this.addAthleteRegionComboBox.Name = "addAthleteRegionComboBox";
            this.addAthleteRegionComboBox.Size = new System.Drawing.Size(121, 22);
            this.addAthleteRegionComboBox.TabIndex = 4;
            this.addAthleteRegionComboBox.SelectedIndexChanged += new System.EventHandler(this.AddAthleteRegionComboBoxSelectedIndexChanged);
            // 
            // addAthleteClubComboBox
            // 
            this.addAthleteClubComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addAthleteClubComboBox.FormattingEnabled = true;
            this.addAthleteClubComboBox.Location = new System.Drawing.Point(310, 75);
            this.addAthleteClubComboBox.Name = "addAthleteClubComboBox";
            this.addAthleteClubComboBox.Size = new System.Drawing.Size(121, 22);
            this.addAthleteClubComboBox.TabIndex = 5;
            // 
            // addAthleteClearButton
            // 
            this.addAthleteClearButton.Location = new System.Drawing.Point(309, 103);
            this.addAthleteClearButton.Name = "addAthleteClearButton";
            this.addAthleteClearButton.Size = new System.Drawing.Size(57, 23);
            this.addAthleteClearButton.TabIndex = 7;
            this.addAthleteClearButton.Text = "Clear";
            this.addAthleteClearButton.UseVisualStyleBackColor = true;
            this.addAthleteClearButton.Click += new System.EventHandler(this.AddAthleteClearButtonClick);
            // 
            // addAthleteSubmitButton
            // 
            this.addAthleteSubmitButton.Location = new System.Drawing.Point(372, 103);
            this.addAthleteSubmitButton.Name = "addAthleteSubmitButton";
            this.addAthleteSubmitButton.Size = new System.Drawing.Size(59, 23);
            this.addAthleteSubmitButton.TabIndex = 8;
            this.addAthleteSubmitButton.Text = "Submit";
            this.addAthleteSubmitButton.UseVisualStyleBackColor = true;
            this.addAthleteSubmitButton.Click += new System.EventHandler(this.AddAthleteSubmitButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 14);
            this.label1.TabIndex = 9;
            this.label1.Text = "First Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "Surname:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "Year of birth:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(252, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 14);
            this.label4.TabIndex = 12;
            this.label4.Text = "Region:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(265, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 14);
            this.label5.TabIndex = 13;
            this.label5.Text = "Club:";
            // 
            // addAthleteYearOfBirthTextBox
            // 
            this.addAthleteYearOfBirthTextBox.Location = new System.Drawing.Point(85, 104);
            this.addAthleteYearOfBirthTextBox.MaxLength = 4;
            this.addAthleteYearOfBirthTextBox.Name = "addAthleteYearOfBirthTextBox";
            this.addAthleteYearOfBirthTextBox.Size = new System.Drawing.Size(88, 20);
            this.addAthleteYearOfBirthTextBox.TabIndex = 6;
            // 
            // AddAthlete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(442, 135);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addAthleteSubmitButton);
            this.Controls.Add(this.addAthleteClearButton);
            this.Controls.Add(this.addAthleteYearOfBirthTextBox);
            this.Controls.Add(this.addAthleteClubComboBox);
            this.Controls.Add(this.addAthleteRegionComboBox);
            this.Controls.Add(this.addAthleteSurnameTextBox);
            this.Controls.Add(this.addAthleteNameTextBox);
            this.Controls.Add(this.addAthleteFemaleRadioButton);
            this.Controls.Add(this.addAthleteMaleRadioButton);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddAthlete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Athletics Database - Add Athlete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton addAthleteMaleRadioButton;
        private System.Windows.Forms.RadioButton addAthleteFemaleRadioButton;
        private System.Windows.Forms.TextBox addAthleteNameTextBox;
        private System.Windows.Forms.TextBox addAthleteSurnameTextBox;
        private System.Windows.Forms.ComboBox addAthleteRegionComboBox;
        private System.Windows.Forms.ComboBox addAthleteClubComboBox;
        private System.Windows.Forms.Button addAthleteClearButton;
        private System.Windows.Forms.Button addAthleteSubmitButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox addAthleteYearOfBirthTextBox;

    }
}