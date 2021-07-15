using System;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Globals;
using System.Collections.Generic;
using System.Web.Script.Serialization;


namespace HTTPClient
{
    public static class WebRequest
    {
        public static void Main(string usr, string pws, string responseStatus = "none")
        {
            //Set username and password as globals
            LocalGlobals.usr = usr;
            LocalGlobals.pws = pws;


            // Checks for username and password
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pws))
            {
                MessageBox.Show("No username or password entered!", "Incorrect Login Information");
                return;
            }
            else
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    // Enters login queue
                    LocalGlobals.url = "https://www.toontownrewritten.com/api/login?format=json";
                    wb.Headers.Set("Content-type", "application/x-www-form-urlencoded");
                    switch (responseStatus)
                    {
                        case "delayed":
                            data["queueToken"] = LocalGlobals.queueToken;
                            break;
                        // Incorrect login details
                        case "false":
                            MessageBox.Show(LocalGlobals.banner, "Yipes!");
                            return;
                        // Allows user to enter 2FA or ToonGuard token to login
                        case "partial":
                            data["appToken"] = Interaction.InputBox(LocalGlobals.banner, "Authorization Token Needed", "");
                            data["authToken"] = LocalGlobals.responseToken;

                            // Exit login sequence if auth pop up is closed
                            if (string.IsNullOrWhiteSpace(data["appToken"]))
                            {
                                return;
                            }
                            break;
                        case "none":
                            data["username"] = usr;
                            data["password"] = pws;
                            break;

                    }
                    // Sends POST response
                    // TEMP Ill remove this in the next commit lol
                    LocalGlobals.game = "TTR";
                    try
                    {
                        var response = wb.UploadValues(LocalGlobals.url, "POST", data);
                        string responseInString = System.Text.Encoding.UTF8.GetString(response);
                        Console.WriteLine(response);
                        HTTPStatus(responseInString);
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message, "Error!");
                    }
                }
            }
        }
    
        public static string getPopulation()
        {
            string population_url = "https://www.toontownrewritten.com/api/population";
            using (var client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.Headers.Add("User-Agent", "Project: Open Toontown Launcher, Author: Christian Diaz, Email: christianmigueldiaz@gmail.com");
                var raw_data = client.DownloadString(population_url);
                dynamic data = JObject.Parse(raw_data);
                string population = data.totalPopulation;
                Console.WriteLine(population);

                // Updates population counter
                return population;
            }
        }

        public static void createQuickAccount(string usr, string pws)
        {
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pws))
            {
                MessageBox.Show("No username or password entered!", "Incorrect Login Information");
                return;
            }

            string json;
            if (File.Exists(@".\quicklogin.json")){
                string jsonFile = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(jsonFile);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        MessageBox.Show($"User {usr} is already a QuickLogin user!", "Account Already Added");
                        return;
                    }
                }
                json_Dictionary.Add(usr, pws);
                json = JsonConvert.SerializeObject(json_Dictionary);
            }
            else
            {
                var columns = new Dictionary<string, string>
                {
                    {usr, pws},
                };
                json = JsonConvert.SerializeObject(columns);
            }
            
            //write string to file
            System.IO.File.WriteAllText(@".\quicklogin.json", json);
            MessageBox.Show($"Added {usr} to QuickLogin!", "Account Added");
        }
        public static void quickLogin(string usr)
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                string json = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        var pws = json_Dictionary[usr];
                        Main(Convert.ToString(usr), Convert.ToString(pws));
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show($"User {usr} has not been added to QuickLogin yet.", "Account Not Added");
                return;
            }
        }
        public static List<string> returnAccounts()
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                string json = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                List<string> accounts = new List<string>(json_Dictionary.Keys);
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public static void removeUser(string usr)
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                string json = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                json_Dictionary.Remove(usr);

                json = JsonConvert.SerializeObject(json_Dictionary);
                System.IO.File.WriteAllText(@".\quicklogin.json", json);
                MessageBox.Show($"Removed {usr} from QuickLogin!", "Account Removed");
            }
        }
        public static void HTTPStatus(string response)
        {
            // JObject is used to get values from the response
            dynamic json = JObject.Parse(response);
            switch (Convert.ToString(json.success))
            {
                case "false":
                    LocalGlobals.banner = json.banner;
                    Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "delayed":
                    // Adds 1 second to delay
                    LocalGlobals.timeToWait += 1000;
                    Task.Delay(LocalGlobals.timeToWait).Wait();

                    // Sends another POST request with the queueToken only
                    LocalGlobals.queueToken = json.queueToken;
                    Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "partial":
                    LocalGlobals.banner = json.banner;
                    // Adds 1 second to delay
                    LocalGlobals.timeToWait += 1000;
                    Task.Delay(LocalGlobals.timeToWait).Wait();

                    // Sends another POST request with the authToken and responseToken only
                    LocalGlobals.responseToken = json.responseToken;
                    Main(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "true":
                    LocalGlobals.timeToWait = 0;
                    LocalGlobals.GETInterval = 150000;

                    // Logins to game using credientials
                    Console.WriteLine("SUCCESS: Logging you in to the Tooniverse...");
                    MessageBox.Show("Logging you in to the Tooniverse...", "Successful Login");
                    Environment.SetEnvironmentVariable("TTR_GAMESERVER", Convert.ToString(json.gameserver));
                    Environment.SetEnvironmentVariable("TTR_PLAYCOOKIE", Convert.ToString(json.cookie));

                    Directory.SetCurrentDirectory(@"C:\Program Files (x86)\Toontown Rewritten");

                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.FileName = "TTREngine.exe";
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

                    // Starts game
                    Process.Start(startInfo);
                    break;
            }
            
        }
    }
}


