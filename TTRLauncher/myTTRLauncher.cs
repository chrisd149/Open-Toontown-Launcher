using System;
using System.Windows.Forms;


namespace myTTRLauncher
{
    public partial class myTTRLauncher : Form
    {
        public myTTRLauncher()
        {
            InitializeComponent();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/chrisd149/myTTRLauncher/tree/master/TTRLauncher");

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            HTTPClient.WebRequest.Main(username, password);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Label1_Click_2(object sender, EventArgs e)
        {

        }
    }
}
