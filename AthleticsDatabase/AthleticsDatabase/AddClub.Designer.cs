namespace AthleticsDatabase
{
    partial class AddClub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddClub));
            this.addANewClubGroupBox = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.addClubNameTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.addClubRegionComboBox = new System.Windows.Forms.ComboBox();
            this.regionlabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clubListView = new System.Windows.Forms.ListView();
            this.idColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.clubNameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.enterButton = new System.Windows.Forms.Button();
            this.clubListRegionComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.addANewClubGroupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addANewClubGroupBox
            // 
            this.addANewClubGroupBox.Controls.Add(this.clearButton);
            this.addANewClubGroupBox.Controls.Add(this.submitButton);
            this.addANewClubGroupBox.Controls.Add(this.addClubNameTextBox);
            this.addANewClubGroupBox.Controls.Add(this.label4);
            this.addANewClubGroupBox.Controls.Add(this.addClubRegionComboBox);
            this.addANewClubGroupBox.Controls.Add(this.regionlabel);
            this.addANewClubGroupBox.Location = new System.Drawing.Point(12, 62);
            this.addANewClubGroupBox.Name = "addANewClubGroupBox";
            this.addANewClubGroupBox.Size = new System.Drawing.Size(233, 117);
            this.addANewClubGroupBox.TabIndex = 0;
            this.addANewClubGroupBox.TabStop = false;
            this.addANewClubGroupBox.Text = "Add a new club";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(98, 84);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(61, 25);
            this.clearButton.TabIndex = 28;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(165, 84);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(55, 25);
            this.submitButton.TabIndex = 27;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.SubmitButtonClick);
            // 
            // addClubNameTextBox
            // 
            this.addClubNameTextBox.Location = new System.Drawing.Point(80, 47);
            this.addClubNameTextBox.Name = "addClubNameTextBox";
            this.addClubNameTextBox.Size = new System.Drawing.Size(140, 20);
            this.addClubNameTextBox.TabIndex = 24;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 14);
            this.label4.TabIndex = 23;
            this.label4.Text = "Club Name :";
            // 
            // addClubRegionComboBox
            // 
            this.addClubRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addClubRegionComboBox.FormattingEnabled = true;
            this.addClubRegionComboBox.Items.AddRange(new object[] {
            "Leinster",
            "Ulster",
            "Munster",
            "Connacht",
            "All Ireland"});
            this.addClubRegionComboBox.Location = new System.Drawing.Point(80, 19);
            this.addClubRegionComboBox.Name = "addClubRegionComboBox";
            this.addClubRegionComboBox.Size = new System.Drawing.Size(140, 22);
            this.addClubRegionComboBox.TabIndex = 14;
            // 
            // regionlabel
            // 
            this.regionlabel.AutoSize = true;
            this.regionlabel.Location = new System.Drawing.Point(26, 22);
            this.regionlabel.Name = "regionlabel";
            this.regionlabel.Size = new System.Drawing.Size(51, 14);
            this.regionlabel.TabIndex = 13;
            this.regionlabel.Text = "Region :";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.clubListView);
            this.groupBox1.Controls.Add(this.enterButton);
            this.groupBox1.Controls.Add(this.clubListRegionComboBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(251, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 244);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current club list";
            // 
            // clubListView
            // 
            this.clubListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.clubNameColumnHeader});
            this.clubListView.FullRowSelect = true;
            this.clubListView.GridLines = true;
            this.clubListView.Location = new System.Drawing.Point(6, 50);
            this.clubListView.MultiSelect = false;
            this.clubListView.Name = "clubListView";
            this.clubListView.Size = new System.Drawing.Size(274, 188);
            this.clubListView.TabIndex = 28;
            this.clubListView.UseCompatibleStateImageBehavior = false;
            this.clubListView.View = System.Windows.Forms.View.Details;
            // 
            // idColumnHeader
            // 
            this.idColumnHeader.Text = "ID";
            this.idColumnHeader.Width = 43;
            // 
            // clubNameColumnHeader
            // 
            this.clubNameColumnHeader.Text = "Club Name";
            this.clubNameColumnHeader.Width = 201;
            // 
            // enterButton
            // 
            this.enterButton.Location = new System.Drawing.Point(225, 17);
            this.enterButton.Name = "enterButton";
            this.enterButton.Size = new System.Drawing.Size(55, 25);
            this.enterButton.TabIndex = 27;
            this.enterButton.Text = "Enter";
            this.enterButton.UseVisualStyleBackColor = true;
            this.enterButton.Click += new System.EventHandler(this.EnterButtonClick);
            // 
            // clubListRegionComboBox
            // 
            this.clubListRegionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clubListRegionComboBox.FormattingEnabled = true;
            this.clubListRegionComboBox.Items.AddRange(new object[] {
            "Leinster",
            "Ulster",
            "Munster",
            "Connacht",
            "All Ireland"});
            this.clubListRegionComboBox.Location = new System.Drawing.Point(80, 19);
            this.clubListRegionComboBox.Name = "clubListRegionComboBox";
            this.clubListRegionComboBox.Size = new System.Drawing.Size(140, 22);
            this.clubListRegionComboBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 14);
            this.label2.TabIndex = 13;
            this.label2.Text = "Region :";
            // 
            // AddClub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(538, 260);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.addANewClubGroupBox);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AddClub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Athletics Database - Add Club";
            this.addANewClubGroupBox.ResumeLayout(false);
            this.addANewClubGroupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox addANewClubGroupBox;
        private System.Windows.Forms.ComboBox addClubRegionComboBox;
        private System.Windows.Forms.Label regionlabel;
        private System.Windows.Forms.TextBox addClubNameTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button enterButton;
        private System.Windows.Forms.ComboBox clubListRegionComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListView clubListView;
        private System.Windows.Forms.ColumnHeader idColumnHeader;
        private System.Windows.Forms.ColumnHeader clubNameColumnHeader;
    }
}