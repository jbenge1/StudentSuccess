/*
 * @author: Justin Benge
 * 
 * Date: 2 october, 2017
 * 
 * Filename: ConfigManager.cs
 * 
 * Compiler: Compiled using mdtool for linux in monodevelop
 * 
 * Description:Makes use of Microsoft's built in ConfigurationManager to grab values from the applications default
 * configuration....
 * 
 */
	
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSuccess
{
    class ConfigManager
    {
	   /*
		* Used to grab the necessary values to use in Utility.cs
		* 
		* @return
		* 		void return
		*/
        public static void SetAppConfigurationValue()
        {

            //Valence application ID
            string appIdValence = ConfigurationManager.AppSettings["AppId"].Trim(); 
            //Valence application Key
            string appKeyValence = ConfigurationManager.AppSettings["AppKey"].Trim();
            //Schema
            string schemaValence = ConfigurationManager.AppSettings["Scheme"].Trim();
            //Host
            string hostValence = ConfigurationManager.AppSettings["Host"].Trim();
			Console.WriteLine (hostValence);
            //Port
            int portValence = int.Parse(ConfigurationManager.AppSettings["Port"].Trim());
            //UserId
            string userIdValence = ConfigurationManager.AppSettings["UserId"].Trim();
            //UserKey
            string userKeyValence = ConfigurationManager.AppSettings["UserKey"].Trim();
            //API Version
            string apiVersion = ConfigurationManager.AppSettings["APIVersion"].Trim();
			//DonwloadDataPath
			string downloadDataPathValence = ConfigurationManager.AppSettings ["DownloadDataPath"].Trim ();
			Console.WriteLine (downloadDataPathValence);
			//LogPath
			string logPathValence = ConfigurationManager.AppSettings ["LogPath"].Trim ();
			//SkipFileName
			string skipFileNamevalence = ConfigurationManager.AppSettings ["skipFileName"].Trim ();

            //Set Valence application ID
            Utility.SetAppIdValence(appIdValence);
            //Set Valence application Key
            Utility.SetAppKeyValence(appKeyValence);
            // Set Schema
            Utility.SetSchemaValence(schemaValence);
            //Set Host
            Utility.SetHostValence(hostValence);
            //Set Port
            Utility.SetPortValence(portValence);
            //Set UserId
            Utility.SetUserIdValence(userIdValence);
            //Set UserKey
            Utility.SetUserKeyValence(userKeyValence);
            //Set API Version
            Utility.SetApiVersion(apiVersion);
			// Set DownloadDataPath
			Utility.SetDownloadDataPathValence (downloadDataPathValence);
			//Set LogPath
			Utility.SetLogPathValence (logPathValence);
			//Set SkipFileName
			Utility.SetSkipFileNameValence (skipFileNamevalence);
        }

    }
}
