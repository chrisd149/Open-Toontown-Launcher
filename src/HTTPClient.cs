using System;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Globals;
using myTTRLauncher;


namespace HTTPClient
{
    public static class WebRequest
    {
        public static void Main(string usr, string pws, string responseStatus = "none")
        {
            //Set username and password as globals
            LocalGlobals.usr = usr;
            LocalGlobals.pws = pws;

            string url = "https://www.toontownrewritten.com/api/login?format=json";

            // Checks for username and password
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pws))
            {
                MessageBox.Show("No username or password entered!", "Incorrect Login Information");
            }
            else
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    // Enters login queue
                    if (responseStatus == "delayed")
                    {
                        data["queueToken"] = LocalGlobals.queueToken;
                    }

                    // Incorrect login details
                    if (responseStatus == "false")
                    {
                        MessageBox.Show(LocalGlobals.banner, "Incorrect Login Information");
                        return;
                    }

                    // Allows user to enter 2FA token to login
                    if (responseStatus == "partial")
                    {
                        data["appToken"] = Interaction.InputBox("Input an auth token here", "Authorization Token", "");
                        data["authToken"] = LocalGlobals.responseToken;

                        // Exit login sequence if auth pop up is closed
                        if (string.IsNullOrWhiteSpace(data["appToken"]))
                        {
                            return;
                        }
                    }

                    // Logins
                    else
                    {
                        data["username"] = usr;
                        data["password"] = pws;
                    }

                    wb.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    // Sends POST response
                    var response = wb.UploadValues(url, "POST", data);
                    string responseInString = System.Text.Encoding.UTF8.GetString(response);
 
                    Console.WriteLine(responseInString);
                    HTTPStatus(responseInString);
                }
            }
        }
        public static string getPopulation()
        {
            string population_url = "https://www.toontownrewritten.com/api/population";
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("User-Agent", "Project: Open TTR Launcher, Author: Christian Diaz, Email: christianmigueldiaz@gmail.com");
                var raw_data = client.DownloadString(population_url);
                dynamic data = JObject.Parse(raw_data);
                string population = data.totalPopulation;
                Console.WriteLine(population);

                // Updates population counter
                return population;
            }
        }
        public static void HTTPStatus(string response)
        {
            // JObject is used to get values from the response
            dynamic json = JObject.Parse(response);
            if (json.success == "false")
            {
                LocalGlobals.banner = json.banner;
                Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
            }
            if (json.success == "delayed")
            {
                // Adds 1 second to delay
                LocalGlobals.timeToWait += 1000;
                Task.Delay(LocalGlobals.timeToWait).Wait();

                // Sends another POST request with the queueToken only
                LocalGlobals.queueToken = json.queueToken;
                Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
            }
            if (json.success == "partial")
            {
                // Adds 1 second to delay
                LocalGlobals.timeToWait += 1000;
                Task.Delay(LocalGlobals.timeToWait).Wait();

                // Sends another POST request with the authToken and responseToken only
                LocalGlobals.responseToken = json.responseToken;
                Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
            }
            if (json.success == "true")
            {

                LocalGlobals.timeToWait = 0;
                LocalGlobals.GETInterval = 150000;

                // Logins to game using credientials
                Console.WriteLine("SUCCESS: Logging you in to the Tooniverse...");
                Environment.SetEnvironmentVariable("TTR_GAMESERVER", Convert.ToString(json.gameserver));
                Environment.SetEnvironmentVariable("TTR_PLAYCOOKIE", Convert.ToString(json.cookie));
                    
                Directory.SetCurrentDirectory(@"C:\Program Files (x86)\Toontown Rewritten");

                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = "TTREngine.exe";
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                // Starts game
                Process.Start(startInfo);
            }
        }
    }
}


