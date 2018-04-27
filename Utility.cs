/*
 * @author: Justin Benge 
 * 
 * Date: 2 october, 2017
 * 
 * FileName: Utility.cs
 * 
 * Compiler: compiled using mdtool for linux in monodevelop
 * 
 * Description: Just a utillty to hold the values of fields such as the Api verison value.... Has setter and getter
 * methods to set the respective  fields. Also has a tool to write a status log to a file aptly named status log.
 * Along with one other tool to check if a directory exitsts. if it doesnt then it will create one.
 * 
 * (would it be easier to use a constructor and make the values immutable by function calls once already set?)
 */ 


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D2L.Extensibility.AuthSdk;

namespace StudentSuccess
{
    class Utility
    {

        //API Version
        private static string apiVersion = string.Empty;
        //Valence application ID
        private static string appIdValence = string.Empty;
        //Valence application Key
        private static string appKeyValence = string.Empty;
        //Schema
        private static string schemaValence = string.Empty;
        //Host
        private static string hostValence = string.Empty;
        //Port
        private static int portValence = 0;
        //UserId
        private static string userIdValence = string.Empty;
        //UserKey
        private static string userKeyValence = string.Empty;
		//DownloadDataPath
		private static string downloadDataPathValence = string.Empty;
		//SkipFileName
		private static string skipFileNameValence = string.Empty;
		//LogPath valence
		private static string logPathValence = String.Empty;



		/*
		 * Set the valence application ID
		 * 
		 * @param appIdValenceStr
		 * 		the value to set the Valence ID to
		 */
        public static void SetAppIdValence(string appIdValenceStr)
        {
            appIdValence = appIdValenceStr;
        }

		/*
		 * Get Valence app ID
		 * 
		 * @return
		 * 		the valence applicatio id
		 */
        public static string GetAppIdValence()
        {
            return appIdValence;
        }
        /*
         * Set Valence application Key
         *
         * @param appKeyValenceStr
         * 		the value to set the valence app key too
         */
        public static void SetAppKeyValence(string appKeyValenceStr)
        {
            appKeyValence = appKeyValenceStr;
        }

        /* 
         *Get Valence application Key
         *
         * @return
         * 		Valence application Key
         */
        public static string GetAppKeyValence()
        {
            return appKeyValence;
        }

        /*
         * Set Schema
         * 
         * @param schemaValenceStr
         * 		the value to set the Schemea to
         */
        public static void SetSchemaValence(string schemaValenceStr)
        {
            schemaValence = schemaValenceStr;
        }

        /*
         * Get Schema
         *  
         * @return 
         * 		the schema
         */
        public static string GetSchemaValence()
        {
            return schemaValence;
        }
        /* 
         * Set Host
         * 
         * @param hostValencStr
         * 		the value to set the host to 
         */
        public static void SetHostValence(string hostValenceStr)
        {
            hostValence = hostValenceStr;
        }

        /*
         * Get Host
         *
         * @return 
         * 		the Host
         */
        public static string GetHostValence()
        {
           return hostValence;
        }

        /* 
         * Set Port
         *
         * @param portValenceInt
         * 		the value to set the port number to
         */
        public static void SetPortValence(int portValenceInt)
        {
            portValence = portValenceInt;
        }

     
		/*
		 * Get the port
		 * 
		 * @return
		 * 		the port number
		 */
        public static int GetPortValence()
        {
            return portValence;
        }
       

		/*
		 * Set the userID
		 * 
		 * @param userIdValenceStr
		 * 		the string to set the userIdValence to
		 */
        public static void SetUserIdValence(string userIdValenceStr)
        {
            userIdValence = userIdValenceStr;
        }

        
		/*
		 * Get the userID
		 * 
		 * @return
		 * 		The user's id as a string
		 */ 
        public static string GetUserIdValence()
        {
            return userIdValence;
        }


       	/*
		 * Set the userKey
		 * 
		 * @pararm userKeyValenceStr
		 * 		The string to set the userkey to
		 */
        public static void SetUserKeyValence(string userKeyValenceStr)
        {
            userKeyValence = userKeyValenceStr;
        }

        
		/*
		 * Get the user key we just set
		 * 
		 * @return
		 * 		the users key as a string
		 */
        public static string GetUserKeyValence()
        {
            return userKeyValence;
        }


		/*
		 * Set the API version
		 * 
		 * @pararm apiVerstionStr
		 * 		the string to set the api Version to
		 */
        public static void SetApiVersion(string apiVersionStr)
		{
			apiVersion = apiVersionStr;
		}


		/*
		 * Get the api version we just set
		 * 
		 * @return
		 * 		the api version as a string
		 */
        public static string GetApiVersion()
        {
            return apiVersion;
        }


		/*
		 * Set the log path valence for use of logging errors
		 * 
		 * @param temp
		 * 		The destination to where we want the file
		 */
		public static void SetLogPathValence(string temp){
			logPathValence = temp;
		}


		/*
		 * Get the destination path to the log file
		 * 
		 * @return
		 * 		the path to the log file
		 */
		public static string GetLogPathValence(){
			return logPathValence;
		}		


		/*
		 * Set the download path 
		 * 
		 * @param temp
		 * 		The path in which to download from
		 */
		public static void SetDownloadDataPathValence(string temp){
			downloadDataPathValence = temp;
		}


		/*
		 * Get the path to the donwloaddata
		 * 
		 * @return
		 * 		The path where we should be downloading the data from
		 */
		public static string GetDownloadDataPathValence(){
			return downloadDataPathValence;
		}


		/*
		 * Create a string of names seperated by commas. These names are the names
		 * that should be skipped when downloading the actual data
		 * 
		 * @pararm temp
		 * 		The names to skip
		 */
		public static void SetSkipFileNameValence(string temp){
			skipFileNameValence = temp;
		}


		/*
		 * Get that list of names to skip as a string of names seperated by commas
		 * 
		 * @return
		 * 		The string of names seperated by commas
		 */ 
		public static string GetSkipFileName(){
			return skipFileNameValence;
		}


		/*
		 * Looks for a directory in the specified path. If it existst then do nothing, otherwise
		 * create a directory with the specified path. This is a void method
		 * 
		 * @param path
		 * 		the path to the directory in question
		 */
		public static void CreateOrCheckDirectory(string path){
			if (!Directory.Exists (path)) {
				using (File.Create (path)) {
				}
			}
		}


		/*
		 * Used to log errors,or lack there of to a specified. Mainly used with the log path. This
		 * is a void method
		 * 
		 * @param path
		 * 		The path of the file to write to
		 * @param name
		 * 		The skipped name, or a status of "All dloads complete", "Some dloads complete", or "All failed"
		 * @param errText
		 * 		The status of the download
		 * @param optErr
		 * 		an optional error displaying the Exceptions message or the time it took to donwload
		 * 		(this parameter is defaulted to the empty string, for when we log the skipped names)
		 */
		public static void WriteToFile(string path, string name, string errText, string optErr = ""){
			using (var tw = new StreamWriter (path, true)) {
				tw.WriteLine (DateTime.Now + "\t" + errText + "\t" + name + "\t" + optErr);
				tw.Close ();
			}
		}

    }
}
