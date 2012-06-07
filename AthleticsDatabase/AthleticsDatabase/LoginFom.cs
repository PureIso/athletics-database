using System;
using System.Windows.Forms;

namespace AthleticsDatabase
{
    /// <summary>Login Form</summary>
    public partial class LoginFom : Form
    {
        private int _tries;
        /// <summary>Login Form</summary>
        public LoginFom()
        {
            _tries = 0;
            InitializeComponent();
        }

        private void LogInButtonClick(object sender, EventArgs e)
        {
            try
            {
                var database = MainForm.Connection();
                if (database.ValidateUser(userNameTextBox.Text, passwordTextBox.Text))
                {
                    MessageBox.Show(this, @"Log In Successful.", @"Athletics Database", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MainForm.EnabledMenu = true;
                    MainForm.LoginText = @"&Log-Out";
                    Close();
                }
                else
                {
                    _tries++;
                    MessageBox.Show(this, @"Invalid Log In.", @"Athletics Database", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    errorlabel.Text = (3 - _tries) + " left.";
                }
                if (_tries == 3) Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message, @"Athletics Database",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void LogInKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar != (char)13) return;//Carriage Return ASCII value
                var database = MainForm.Connection();
                if (database.ValidateUser(userNameTextBox.Text, passwordTextBox.Text))
                {
                    MessageBox.Show(this, @"Log In Successful.", @"Athletics Database", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    MainForm.EnabledMenu = true;
                    MainForm.LoginText = @"&Log-Out";
                    Close();
                }
                else
                {
                    _tries++;
                    MessageBox.Show(this, @"Invalid Log In.", @"Athletics Database", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    errorlabel.Text = (3 - _tries) + " left.";
                }
                if (_tries == 3) Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, @"Athletics Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
