using System;
using System.Windows.Forms;
using Globals;


namespace OpenTTLauncher
{
    public partial class OpenTTLauncher : Form
    {
        public OpenTTLauncher()
        { 
            InitializeComponent();
            CurrentPopulation();
            LocalGlobals.jsonFileLoc = $"{System.IO.Directory.GetCurrentDirectory()}\\quicklogin.json";
            VersionLabel.Text = $"v{Application.ProductVersion}";
            RefreshList();
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/chrisd149/Open-Toontown-Launcher");

        }

        private void CurrentPopulation()
        {
            // Starts timer
            Timer myTimer = new Timer();
            PopCounterLabel.Text = WebRequest.GetPopulation() + " toons";

            var timer = new Timer { Interval = LocalGlobals.GETInterval };
            timer.Tick += (o, args) =>
            {
                PopCounterLabel.Text = WebRequest.GetPopulation() + " toons";
            };
            timer.Start();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = UserTextBox.Text;
            string password = PwsdTextBox.Text;
            WebRequest.Login(username, password);
        }

        private void InstructionsButton_Click(object sender, EventArgs e)
        {
            string message = "This launcher uses your login credentials to login to Toontown Rewritten.  You must have an account " +
                "registered with Toontown Rewritten, which you can do at toontownrewritten.com.  You must also have Toontown Rewritten " +
                "installed on your system, with it already updated (i.e. the offical launcher has been launched at least once.) " +
                Environment.NewLine + Environment.NewLine +
                "QuickLogin:" + Environment.NewLine +
                "After entering your username and password, you can setup a QuickLogin account by clicking the 'Save Account' button. " +
                "After doing so, you can select an account to login with on the dropdown box, and click the 'QuickLogin' button to login with that account. " +
                "The account is stored locally in the project directory, and can be deleted by selecting your account in the dropdown, and clicking the " +
                "'Remove Account' button." + 
                Environment.NewLine + Environment.NewLine +
                "Contact me if you have any issues or questions at christianmigueldiaz@gmail.com or on discord at chrisd149#7640";
            string title = "Instructions";
            MessageBox.Show(message, title);
        }
        private void SaveAcctButton_Click(object sender, EventArgs e)
        {
            string username = UserTextBox.Text;
            string password = PwsdTextBox.Text;
            // No username and/or password entered
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("No username and/or password entered!", "Incorrect Login Information");
                return;
            }
            else
            {
                string text_body = $"This will create a QuickLogin account for {username}, which allows you to quickly login without entering any login credentials. " +
                Environment.NewLine + Environment.NewLine +
                $"Your login information for this account will be stored locally, with your password encrypted. " +
                $" However, a risk is still present whenever personal account data is stored locally, and your login info can still be susceptible to bad actors. " +
                Environment.NewLine + Environment.NewLine +
                $"Do you still want to create a QuickLogin account for {username}?";
                DialogResult dialogResult = MessageBox.Show(text_body, "Confirm Quick Account", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    LauncherProgram.CreateQuickAccount(username, password);
                    RefreshList();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }
        }

        public void RefreshList()
        {
            QuickLoginComboBox.DataSource = LauncherProgram.ReturnAccounts();
        }

        private void RemoveAcctButton_Click(object sender, EventArgs e)
        {
            String usr = QuickLoginComboBox.Text;
            LauncherProgram.RemoveUser(usr);
            RefreshList();
        }

        private void GameDirButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderDlg.SelectedPath;
                Properties.Settings.Default["GameDirectory"] = folderName;
                Properties.Settings.Default.Save();
            }
        }

        private void QuickLoginButton_Click(object sender, EventArgs e)
        {
            String usr = QuickLoginComboBox.Text;
            LauncherProgram.QuickLogin(usr);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void AuthLabel_Click_2(object sender, EventArgs e)
        {

        }

        private void PopCounterLabel_Click(object sender, EventArgs e)
        {

        }
        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void PwsdLabel_Click_3(object sender, EventArgs e)
        {

        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }

        private void PopLabel_Click(object sender, EventArgs e)
        {

        }

        private void QuickLoginLabel_Click(object sender, EventArgs e)
        {

        }

        private void QuickLoginComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void VersionLabel_Click(object sender, EventArgs e)
        {
        }
    }
}
