namespace AthleticsDatabase
{
    partial class SpecialThanks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpecialThanks));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.weblinkLabel = new System.Windows.Forms.Label();
            this.aaiLink = new System.Windows.Forms.LinkLabel();
            this.athleticleinsterLinkLabel = new System.Windows.Forms.LinkLabel();
            this.athleticsmunsterLinkLabel = new System.Windows.Forms.LinkLabel();
            this.okButton = new System.Windows.Forms.Button();
            this.specialThanksNamesTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 164);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // weblinkLabel
            // 
            this.weblinkLabel.AutoSize = true;
            this.weblinkLabel.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weblinkLabel.Location = new System.Drawing.Point(172, 96);
            this.weblinkLabel.Name = "weblinkLabel";
            this.weblinkLabel.Size = new System.Drawing.Size(67, 12);
            this.weblinkLabel.TabIndex = 3;
            this.weblinkLabel.Text = "Web Links:";
            // 
            // aaiLink
            // 
            this.aaiLink.AutoSize = true;
            this.aaiLink.Location = new System.Drawing.Point(171, 108);
            this.aaiLink.Name = "aaiLink";
            this.aaiLink.Size = new System.Drawing.Size(134, 14);
            this.aaiLink.TabIndex = 4;
            this.aaiLink.TabStop = true;
            this.aaiLink.Text = "www.athleticsireland.ie";
            this.aaiLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AaiLinkLinkClicked);
            // 
            // athleticleinsterLinkLabel
            // 
            this.athleticleinsterLinkLabel.AutoSize = true;
            this.athleticleinsterLinkLabel.Location = new System.Drawing.Point(171, 122);
            this.athleticleinsterLinkLabel.Name = "athleticleinsterLinkLabel";
            this.athleticleinsterLinkLabel.Size = new System.Drawing.Size(146, 14);
            this.athleticleinsterLinkLabel.TabIndex = 5;
            this.athleticleinsterLinkLabel.TabStop = true;
            this.athleticleinsterLinkLabel.Text = "www.athleticsleinster.org";
            this.athleticleinsterLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AthleticleinsterLinkLabelLinkClicked);
            // 
            // athleticsmunsterLinkLabel
            // 
            this.athleticsmunsterLinkLabel.AutoSize = true;
            this.athleticsmunsterLinkLabel.Location = new System.Drawing.Point(171, 136);
            this.athleticsmunsterLinkLabel.Name = "athleticsmunsterLinkLabel";
            this.athleticsmunsterLinkLabel.Size = new System.Drawing.Size(154, 14);
            this.athleticsmunsterLinkLabel.TabIndex = 6;
            this.athleticsmunsterLinkLabel.TabStop = true;
            this.athleticsmunsterLinkLabel.Text = "www.munsterathletics.com";
            this.athleticsmunsterLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.AthleticsmunsterLinkLabelLinkClicked);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.Location = new System.Drawing.Point(341, 154);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(48, 21);
            this.okButton.TabIndex = 25;
            this.okButton.Text = "&OK";
            this.okButton.Click += new System.EventHandler(this.OkButtonClick);
            // 
            // specialThanksNamesTextBox
            // 
            this.specialThanksNamesTextBox.Location = new System.Drawing.Point(171, 5);
            this.specialThanksNamesTextBox.Multiline = true;
            this.specialThanksNamesTextBox.Name = "specialThanksNamesTextBox";
            this.specialThanksNamesTextBox.ReadOnly = true;
            this.specialThanksNamesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.specialThanksNamesTextBox.Size = new System.Drawing.Size(218, 78);
            this.specialThanksNamesTextBox.TabIndex = 26;
            this.specialThanksNamesTextBox.Text = "Stiina Tsedreki\r\nMaria Gabriele\r\nDavid Aubert\r\nJason O\'Gorman";
            // 
            // SpecialThanks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(391, 181);
            this.Controls.Add(this.specialThanksNamesTextBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.athleticsmunsterLinkLabel);
            this.Controls.Add(this.athleticleinsterLinkLabel);
            this.Controls.Add(this.aaiLink);
            this.Controls.Add(this.weblinkLabel);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Lucida Sans", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpecialThanks";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Athletics Database - Special Thanks";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label weblinkLabel;
        private System.Windows.Forms.LinkLabel aaiLink;
        private System.Windows.Forms.LinkLabel athleticleinsterLinkLabel;
        private System.Windows.Forms.LinkLabel athleticsmunsterLinkLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox specialThanksNamesTextBox;
    }
}