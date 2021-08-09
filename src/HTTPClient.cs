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


namespace OpenTTLauncher
{
    public static class WebRequest
    {
        public static void Main(string usr, string pws, string responseStatus = "none")
        {
            //Set username and password as globals
            LocalGlobals.usr = usr;
            LocalGlobals.pws = EncodePasswordToBase64(pws);


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
        // Returns current population of Toontown to live counter on launcher
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
        // Adds new QuickLogin account
        {
            // No username and/or password entered
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pws))
            {
                MessageBox.Show("No username or password entered!", "Incorrect Login Information");
                return;
            }

            string json;
            // If the json already exists, we have to get the data from it, convert it to a dictionary, modify the dictionary, and then create the json.
            if (File.Exists(@".\quicklogin.json")){
                string jsonFile = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(jsonFile);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        // User is already in json file.
                        MessageBox.Show($"User {usr} is already a QuickLogin user!", "Account Already Added");
                        return;
                    }
                }
           
                // Adds user info to json
                json_Dictionary.Add(usr, EncodePasswordToBase64(pws));
                json = JsonConvert.SerializeObject(json_Dictionary);
            }
            else
            {
                var columns = new Dictionary<string, string>
                {
                    {usr, EncodePasswordToBase64(pws)},
                };
                json = JsonConvert.SerializeObject(columns);
            }
            // Check if user credentials are correct by sending a simple POST login request
            using (var wb = new WebClient())
            {
                var data = new NameValueCollection();

                // Enters login queue
                wb.Headers.Set("Content-type", "application/x-www-form-urlencoded");
                data["username"] = usr;
                data["password"] = pws;
                try
                {
                    var response = wb.UploadValues(LocalGlobals.url, "POST", data);
                    string responseInString = System.Text.Encoding.UTF8.GetString(response);
                    dynamic response_json = JObject.Parse(responseInString);
                    if (Convert.ToString(response_json.success) == "false")
                    {
                        MessageBox.Show(Convert.ToString(response_json.banner), "Yipes!");
                        return;
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error!");
                }
            }

            // Write json string to file
            System.IO.File.WriteAllText(@".\quicklogin.json", json);
            MessageBox.Show($"Added {usr} to QuickLogin!", "Account Added");
        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public static string DecodeFrom64(string encodedPassword)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedPassword);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public static void quickLogin(string usr)
        // QuickLogin function
        // Logins with the currently selected account.
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                string json = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        // Found the account, get it's password and login.
                        object pws = json_Dictionary[usr];
                        string decoded_pws = DecodeFrom64(Convert.ToString(pws));
                        Main(Convert.ToString(usr), decoded_pws);
                        return;
                    }
                }
            }
            else
            {
                // Account not in json
                MessageBox.Show($"User {usr} has not been added to QuickLogin yet.", "Account Not Added");
                return;
            }
        }
        public static List<string> returnAccounts()
        // Returns list of accounts in json.  Used to update dropdownlist of accounts.
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                string json = File.ReadAllText(@".\quicklogin.json");
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                // Convert keys (account usernames) to list.
                List<string> accounts = new List<string>(json_Dictionary.Keys);
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public static void removeUser(string usr)
        // Removes selected account from json.
        {
            if (File.Exists(@".\quicklogin.json"))
            {
                // Simular to adding a user, we must convert the file to dict and then back to json.
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
                    Environment.SetEnvironmentVariable("TTR_GAMESERVER", Convert.ToString(json.gameserver));
                    Environment.SetEnvironmentVariable("TTR_PLAYCOOKIE", Convert.ToString(json.cookie));
                    string dir = Convert.ToString(Properties.Settings.Default["GameDirectory"]);
                    Directory.SetCurrentDirectory(dir);

                    ProcessStartInfo startInfo = new ProcessStartInfo();

                    startInfo.FileName = "TTREngine.exe";
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    try
                    {
                        // Starts game
                        MessageBox.Show("Logging you in to the Tooniverse...", "Successful Login");
                        Process.Start(startInfo);
                    }
                    catch (Exception start_error)
                    {
                        MessageBox.Show(Convert.ToString(start_error), "Error!");
                    }
                    break;
            }
            
        }
    }
}


