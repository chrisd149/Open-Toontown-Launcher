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
            currentPopulation();
            LocalGlobals.jsonFileLoc = $"{System.IO.Directory.GetCurrentDirectory()}\\quicklogin.json";
            refreshList();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/chrisd149/Open-Toontown-Launcher");

        }

        private void currentPopulation()
        {
            // Starts timer
            Timer myTimer = new Timer();
            populationLabel.Text = WebRequest.getPopulation() + " toons";

            var timer = new Timer { Interval = LocalGlobals.GETInterval };
            timer.Tick += (o, args) =>
            {
                populationLabel.Text = WebRequest.getPopulation() + " toons";
            };
            timer.Start();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            WebRequest.Main(username, password);
        }

        private void instructionsButton_Click(object sender, EventArgs e)
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

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Label1_Click_2(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click_3(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            string text_body = $"This will create a QuickLogin account for {username}, which allows you to quickly login without entering any login credentials. " +
                Environment.NewLine + Environment.NewLine +
                $"Your login information for this account will be stored locally, with your password encrypted. " +
                $" However, a risk is still present whenever personal account data is stored locally, and your login info can still be susceptible to bad actors. " +
                Environment.NewLine + Environment.NewLine +
                $"Do you still want to create a QuickLogin account for {username}?";
            DialogResult dialogResult = MessageBox.Show(text_body, "Confirm Quick Account", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                WebRequest.createQuickAccount(username, password);
                refreshList();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String usr = comboBox1.Text;
            WebRequest.quickLogin(usr);
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public void refreshList()
        {
            comboBox1.DataSource = WebRequest.returnAccounts();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            String usr = comboBox1.Text;
            WebRequest.removeUser(usr);
            refreshList();
        }

        private void Button5_Click(object sender, EventArgs e)
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
    }
}
