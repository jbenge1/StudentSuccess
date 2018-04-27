/* 
 * @author: Justin Benge 
 * 
 * Date: 2 October, 2017
 * 
 * FileName: APIUtility.cs 
 * 
 * Compiler: Compiled using mdtool for linux in monodevelop
 * 
 * Description: A utility to that instantiates ID2LUserContext, and downloads files using D2L.Extesnsibility.AuthdSdk
 * package.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using D2L.Extensibility.AuthSdk;

namespace StudentSuccess
{
    class APIUtility
    {
		/*
		 * Creates a new ID2LUserContext object
		 * 
		 * @return
		 * 		an instance of a ID2LUserContext
		 */
        public static ID2LUserContext GetD2LUserContext()
        {
            D2LAppContextFactory factory = new D2LAppContextFactory();
            ID2LAppContext appContext = factory.Create(Utility.GetAppIdValence(), Utility.GetAppKeyValence());
            HostSpec hostInfo = new HostSpec(Utility.GetSchemaValence(), Utility.GetHostValence(), Utility.GetPortValence());
            ID2LUserContext userContext = appContext.CreateUserContext(Utility.GetUserIdValence(), Utility.GetUserKeyValence(), hostInfo);
            return userContext;
        }


		/*
		 * Extract a value by splitting a string
		 * 
		 * @pararm s
		 * 		the string containing the desired value
		 * @param arrVal
		 * 		the location of the desired value?
		 * @param strtTrim
		 * 		the starting index of the value
		 * @param
		 * 		the ending index of the value
		 * 
		 * @return
		 * 		The desired string value
		 */
		public static string ExtractStringValue(string s, int arrVal, int strtTrim, int endTrim){
			var s1 = s.Split (',') [arrVal].ToString ().Trim ();
			var s2 = s1.Substring (strtTrim, s1.Length - strtTrim);
			var strVal = s2.Substring (0, s2.Length - endTrim);
			return strVal;
		}

        /* 
         * Download zip file and extract
         *
         * @param userContext
         * 		an instance of ID2LUserContext to create verivied credentials
         * 
         * @return
         * 		nothing....
         */
        public static void DownloadFile(ID2LUserContext userContext)
        {
			// Data Path
			string dataPath = Utility.GetDownloadDataPathValence ();
			// Log Path
			string logPath = Utility.GetLogPathValence ();
			// Error Log Path
			string errorlogPath =  "/home/justin/temp/ErrorLog.txt";
			// Status Log Path
			string statusLogPath =  "/home/justin/temp/StatusLog.txt";


            //save zip file 
			string zipPath = @"" + dataPath + "D2LDataFile.zip";
            //File path for extract
			string extractPath = @"" + dataPath + "ExtractD2lDataFile";
            //API version
            string apiVersion = Utility.GetApiVersion();
            //File path for destination
			string destPath = @"" + dataPath + "CombinedD2LDataFile\\";

			// Check to see if directories are on the disk 
			// if not then create them
		//	Utility.CreateOrCheckDirectory (destPath);
			Utility.CreateOrCheckDirectory (errorlogPath);
			Utility.CreateOrCheckDirectory (statusLogPath);

            //Create a request
			string request = "/d2l/api/lp/" + apiVersion + "/dataExport/bds/list";

            //URI
            Uri getListUri = userContext.CreateAuthenticatedUri(request, "GET");
			try{
				//Start the clock so we know how long the download took
				var startTime = DateTime.Now;
				// Flag for any errors in downloading
				var flag = false;

				// strat a web client
           		using (WebClient wc = new WebClient())
           	 	{

					wc.Headers.Add("User-Agent:Other");
					// Get a list of PuginID from the web service
					byte[] response = wc.DownloadData(getListUri);
					// convert the list into a string
					string str = wc.Encoding.GetString(response);
					var strList = str.Split('{').Skip(1);

					// get a name of to ignore
					string skipFileName = Utility.GetSkipFileName();
					// create a list of the file names to skip
					var skipList = skipFileName.Split(',');

					// (Loop through the donwloaded data)
					for(int z=0;z<strList.Count();z++){
					//foreach(string s in strList){
			
						// This is all failry slow, is there any way we can speed it up?
						string s = strList.ElementAt(z);
						// I do not think that string ops is slowing it down
						// Extract the pluginID from the string 
						var pluginId = ExtractStringValue(s, 0,12,1);
						// Extract the fileName from the string
						var name = ExtractStringValue(s,1,8,1).Replace(" ","");
						// Extract the Download link from the string
						var dLink = ExtractStringValue(s,4,15,0);

						// Now check if download link is available and if the name does not belong to the 
						// list of names to skip....
						if(dLink.Length > 6 && !skipList.Any(name.Contains)){
							using(WebClient wc1 = new WebClient()){
								//Create a webrequest and URI to download the data
								string request1 = "/d2l/api/lp/" + apiVersion + "/dataExport/bds/download/" + pluginId;
								//Acturally create the URI
								Uri uri = userContext.CreateAuthenticatedUri(request1, "GET");
								//D Download the file and store it zip
								Console.WriteLine((z+1)+": Downloading: "+name);
								wc1.DownloadFile(uri, zipPath);

								// this is where it is slow (I think) 
								// Now extract the zip contents and store them
								//Print out a message to display what were downloading for debugging purposes
								Console.WriteLine((z+1)+": Extracting : "+name);

								System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, "/home/justin/temp/");
							}

							// Now copy the files to the specified folder and get rid of any unwanted directories
//							var st = Directory.GetFiles((extractPath), "*.csv");
//							File.Copy(st[0], Path.GetFileName(st[0]), true);
//							Directory.Delete((extractPath), true);
							//debugging purposes
						//	if(z == 2)
						//		break;
						}

						else{

							//Log any names that were skipped
							if(skipList.Any(name.Contains)){
								Utility.WriteToFile(errorlogPath, name, "The download skipped for file name");
							}
							else{
								//Log any names that were not downloaded but not meant to be skipped
								Utility.WriteToFile(errorlogPath, name, "The download failed for file name");
								flag = true;
							}
						}
					}
					
				//	string targetDest = "/home/justin/temp/";
				//	string cwd = "/home/justin/Downloads/StudentSuccess/StudentSuccess";	
				//	string[] files = Directory.GetFiles(cwd, "*.csv *.txt *.zip");
				//	foreach(string s in files){
				//		System.IO.File.Copy(s, targetDest,true);
				//	}
            	}

				//Donwload should be complete so stop the clock
				var endTime = DateTime.Now;

				if(flag == false){
					// All files were download succesfully, log this
					Utility.WriteToFile(statusLogPath, "All Files", "Download Completed", String.Format(" Download Time {0}", (endTime - startTime)));
				}
				else{
					// Not all files were donwloaded, log this
					Utility.WriteToFile(statusLogPath, "Some Files", "Download partially Completed", String.Format(" Download Time {0}", (endTime-startTime)));
				}
        	}
			catch(Exception e){
				/*
				 * if download fails for whatever reason 
				 * ie we were unable to connect, Log this into the 
				 * log file and print out the exception
				 */
				Utility.WriteToFile (statusLogPath, "All Files", "Download Failed", e.Message);
			}
		}
    }
}
