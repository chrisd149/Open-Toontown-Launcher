using System;
using System.Windows.Forms;
using System.Threading.Tasks;
using Globals;


namespace OpenTTRLauncher
{
    public partial class OpenTTRLauncher : Form
    {
        public OpenTTRLauncher()
        {
            InitializeComponent();
            currentPopulation();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/chrisd149/OpenTTRLauncher/tree/master/TTRLauncher");

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

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
