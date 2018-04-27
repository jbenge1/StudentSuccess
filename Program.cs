/*
 * @author: Justin Benge 
 * 
 * Date: 2 october, 2017
 * 
 * FileName: Program.cs
 * 
 * Compiler: Compiled using mdtool for linux in monodevelop
 * 
 * Description: main function sets up all configurations then creates a new ID2LUserContext and 
 * donwloads files
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
    class Program
    {
        static void Main(string[] args)
        {
            ConfigManager.SetAppConfigurationValue();
            //Create a user context to make Valence API call
            ID2LUserContext userContext = APIUtility.GetD2LUserContext();
            //Download data zip file and extract it.
            APIUtility.DownloadFile(userContext);

        }
    }
}
