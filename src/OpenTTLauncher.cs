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
            populationLabel.Text = HTTPClient.WebRequest.getPopulation() + " toons";

            var timer = new Timer { Interval = LocalGlobals.GETInterval };
            timer.Tick += (o, args) =>
            {
                populationLabel.Text = HTTPClient.WebRequest.getPopulation() + " toons";
            };
            timer.Start();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordTextBox.Text;
            HTTPClient.WebRequest.Main(username, password);
        }

        private void instructionsButton_Click(object sender, EventArgs e)
        {
            string message = "This launcher uses your login credentials to login to Toontown Rewritten.  You must have an account " +
                "registered with Toontown Rewritten, which you can do at toontownrewritten.com.  You must also have Toontown Rewritten" +
                "installed on your system, with it already updated (i.e. the offical launcher has been launched at least once.) " +
                Environment.NewLine + Environment.NewLine +
                "Enter your username and password in the fields on the launcher, and click the play button. The game should begin logging in if " +
                "your credentials are correct.  You may also be asked to enter a token if you have ToonGuard or Two-factor authorization enabled. " +
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
            HTTPClient.WebRequest.createQuickAccount(username, password);
            refreshList();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String usr = comboBox1.Text;
            HTTPClient.WebRequest.quickLogin(usr);
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        public void refreshList()
        {
            comboBox1.DataSource = HTTPClient.WebRequest.returnAccounts();
        }
    }
}
