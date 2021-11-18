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
        public static void Login(string usr, string pws, string responseStatus = "none")
        {
            //Set username and password as globals
            LocalGlobals.usr = usr;
            LocalGlobals.pws = LauncherProgram.EncodePasswordToBase64(pws);

            // Checks for username and password
            if (string.IsNullOrEmpty(usr) || string.IsNullOrEmpty(pws))
            {
                MessageBox.Show("No username or password entered!", "Incorrect Login Information");
                return;
            }
            else
            {
                var data = new NameValueCollection();

                // Enters login queue
                switch (responseStatus)
                {
                    // Second POST before logging in.
                    case "delayed":
                        data["queueToken"] = LocalGlobals.queueToken;
                        break;
                    // User entered incorrect login details
                    case "false":
                        MessageBox.Show(LocalGlobals.banner, "Yipes!");
                        return;
                    // Prompts user to enter 2FA or ToonGuard token to login
                    case "partial":
                        data["appToken"] = Interaction.InputBox(LocalGlobals.banner, "Authorization Token Needed", "");
                        data["authToken"] = LocalGlobals.responseToken;

                        // Exit login sequence if auth pop up is closed
                        if (string.IsNullOrWhiteSpace(data["appToken"]))
                        {
                            return;
                        }
                        break;
                    // Intial POST to login
                    case "none":
                        data["username"] = usr;
                        data["password"] = pws;
                        break;
                }
                // Sends POST response
                var login_process = HTTPPostClient(data);
                if (login_process is String)
                {
                    // Good POST request, client now starts login process.
                    HTTPStatus(login_process);
                }
                else
                {
                    // HTTPPostClient has run into an error and has notfied the user.
                    return;
                }
            }
        }
        public static string HTTPPostClient(NameValueCollection data)
        {
            // Sends POST response
            using (var wb = new WebClient())
            {
                wb.Headers.Set("Content-type", "application/x-www-form-urlencoded");
                wb.Headers.Add("User-Agent", "Project: Open Toontown Launcher, Author: Christian Diaz, Email: christianmigueldiaz@gmail.com");
                try
                {
                    var response = wb.UploadValues(LocalGlobals.url, "POST", data);
                    string responseInString = System.Text.Encoding.UTF8.GetString(response);
                    Console.WriteLine(response);
                    return responseInString;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message, "Error!");
                    return null;
                }
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
                    Login(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "delayed":
                    // Adds 1 second to delay
                    LocalGlobals.timeToWait += 1000;
                    Task.Delay(LocalGlobals.timeToWait).Wait();

                    // Sends another POST request with the queueToken only
                    LocalGlobals.queueToken = json.queueToken;
                    Login(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "partial":
                    LocalGlobals.banner = json.banner;
                    // Adds 1 second to delay
                    LocalGlobals.timeToWait += 1000;
                    Task.Delay(LocalGlobals.timeToWait).Wait();

                    // Sends another POST request with the authToken and responseToken only
                    LocalGlobals.responseToken = json.responseToken;
                    Login(LocalGlobals.usr, LocalGlobals.pws, Convert.ToString(json.success));
                    break;
                case "true":
                    LocalGlobals.timeToWait = 0;
                    LocalGlobals.GETInterval = 150000;

                    // Logins to game using credientials
                    Console.WriteLine("SUCCESS: Logging you in to the Tooniverse...");
                    Environment.SetEnvironmentVariable("TTR_GAMESERVER", Convert.ToString(json.gameserver));
                    Console.WriteLine($"GAMESERVER: {Convert.ToString(json.gameserver)}");
                    Environment.SetEnvironmentVariable("TTR_PLAYCOOKIE", Convert.ToString(json.cookie));
                    string dir = Convert.ToString(Properties.Settings.Default["GameDirectory"]);


                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = "TTREngine.exe",
                        WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
                    };

                    
                    try
                    {
                        Directory.SetCurrentDirectory(dir);
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

        public static string GetPopulation()
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
    }

    public static class LauncherProgram
    {
        public static void CreateQuickAccount(string usr, string pws)
        // Adds new QuickLogin account
        {
            string json;
            // If the user config has data, we have to get the data from it, convert it to a dictionary, modify the dictionary, and then return the dictionary to string.
            if (!string.IsNullOrEmpty(Convert.ToString(Properties.Settings.Default["Users"])))
            {
                string jsonFile = Convert.ToString(Properties.Settings.Default["Users"]);
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(jsonFile);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        // User is already in user config.
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
            var data = new NameValueCollection
            {
                ["username"] = usr,
                ["password"] = pws
            };
            
            var login_process = WebRequest.HTTPPostClient(data);
            dynamic response_json = JObject.Parse(login_process);

            if (Convert.ToString(response_json.success) == "false")
            {
                MessageBox.Show(Convert.ToString(response_json.banner), "Yipes!");
                return;
            }

            // Write json string to file
            Convert.ToString(Properties.Settings.Default["Users"] = json);
            Properties.Settings.Default.Save();
            MessageBox.Show($"Added {usr} to QuickLogin!", "Account Added");
        }

        public static string EncodePasswordToBase64(string password)
        // Encodes passwords stored to hopefully reduce issues.
        //
        // This isn't very secure, but I haven't found a way of making a more
        // secure password encoder without frying my brain.
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

        public static string DecodeFrom64(string encodedPassword)
        // Decodes stored passwords to be used for login.
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

        public static void QuickLogin(string usr)
        // QuickLogin function.
        // Logins with the currently selected account.
        {
            if (usr.Length == 0)
            {
                // No account selected
                MessageBox.Show($"No user was selected for QuickLogin!", "No Account Selected");
                return;
            }
            if (!string.IsNullOrEmpty(Convert.ToString(Properties.Settings.Default["Users"])))
            {
                string json = Convert.ToString(Properties.Settings.Default["Users"]);
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        // Found the account, get it's password and login.
                        object pws = json_Dictionary[usr];
                        string decoded_pws = DecodeFrom64(Convert.ToString(pws));
                        WebRequest.Login(Convert.ToString(usr), decoded_pws);
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
        public static List<string> ReturnAccounts()
        // Returns list of accounts in user config.  Used to update dropdownlist of accounts.
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Properties.Settings.Default["Users"])))
            {
                string json = Convert.ToString(Properties.Settings.Default["Users"]);
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                // Convert keys (account usernames) to list.
                List<string> accounts = new List<string>(json_Dictionary.Keys);
                accounts.Insert(0, "");
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public static void RemoveUser(string usr)
        // Removes selected account from user config.
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Properties.Settings.Default["Users"])))
            {
                // Simular to adding a user, we must convert the file to dict and then back to user config.
                string json = Convert.ToString(Properties.Settings.Default["Users"]);
                Dictionary<string, object> json_Dictionary = (new JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(json);
                bool usr_in_list = false;
                foreach (var item in json_Dictionary.Keys)
                {
                    if (usr == Convert.ToString(item))
                    {
                        usr_in_list = true;
                    }
                }
                if (usr_in_list is false)
                {
                    // No QuickLogin account selected, so we can't remove anything.
                    MessageBox.Show($"Select a QuickLogin account to remove.", "Select an Account");
                    return;
                }
                else
                {
                    json_Dictionary.Remove(usr);
                    json = JsonConvert.SerializeObject(json_Dictionary);
                    Properties.Settings.Default["Users"] = json;
                    Properties.Settings.Default.Save();
                    MessageBox.Show($"Removed {usr} from QuickLogin!", "Account Removed");
                }
            }
        }
    }
}


