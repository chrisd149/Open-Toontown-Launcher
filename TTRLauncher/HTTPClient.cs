using System;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using LocalGlobals;
using myTTRLauncher;


namespace HTTPClient
{
    public static class WebRequest
    {
        public static void Main(string usr, string pws, string responseStatus = "none")
        {
            //Set username and password as globals
            Globals.usr = usr;
            Globals.pws = pws;

            string url = "https://www.toontownrewritten.com/api/login?format=json";

            // Checks for username and password
            if (string.IsNullOrEmpty(usr))
            {
                Console.WriteLine("No username defined!");
            }
            if (string.IsNullOrEmpty(pws))
            {
                Console.WriteLine("No password defined!");
            }
            else
            {
                using (var wb = new WebClient())
                {
                    var data = new NameValueCollection();

                    if (responseStatus == "delayed")
                    {
                        data["queueToken"] = Globals.queueToken;
                    }
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
        public static void HTTPStatus(string response)
        {
            // JObject is used to get values from the response
            dynamic status = JObject.Parse(response);
            if (status.success == "delayed")
            {
                // Adds 1 second to delay
                Globals.timeToWait += 1000;
                Task.Delay(Globals.timeToWait).Wait();

                // Sends another POST request with the queueToken only
                Globals.queueToken = status.queueToken;
                Main(Globals.usr, Globals.pws, Convert.ToString(status.success));
            }
            if (status.success == "partial")
            {
                
            }
            if (status.success == "true")
            {
                Globals.timeToWait = 0;

                //Logins to game using credientials
                Console.WriteLine("SUCCESS: Logging you in to the Tooniverse...");
                Environment.SetEnvironmentVariable("TTR_GAMESERVER", Convert.ToString(status.gameserver));
                Environment.SetEnvironmentVariable("TTR_PLAYCOOKIE", Convert.ToString(status.cookie));
                // TODO: make this dynamic
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


