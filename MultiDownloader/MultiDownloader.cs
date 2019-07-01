using System;
using System.IO;
using DownloaderLibrary;
namespace MultiDownloader
{
    class MultiDownloader
    {
        static void Main(string[] args)
        {
            /*  
             * The following argument parsing is specific to THIS console application. 
             * Other applications can skip these steps if they have a list of URLs to download files from
             * 
             * The follwing arguments are expected
             * argument 0: Number of files to be downloaded (n)
             * next n arguments represent the urls of the files to be downloaded
             * next n arguments are the respective paths where the files need to be saved
             * this is follwed by an optional argument to indicate whether file replacement should take place
             */

            int numOfDownloads = Convert.ToInt32(args[0]);
            string[] urlList = new string[numOfDownloads];
            string[] pathList = new string[numOfDownloads];
            for (int i = 0; i < numOfDownloads; i++)
            {
                urlList[i] = args[i + 1];
                pathList[i] = args[i + 1 + numOfDownloads];
            }

            bool replace = false;
            if (args.Length > 1 + numOfDownloads + numOfDownloads && args[args.Length - 1] == "r")
            {
                //optional argument present for file replacement 
                replace = true;
            }

            /*
              * Any application (console, web, whatever) needs to just create an object of the Downloader class 
              * and call the Download function passing in the parameters
              */
            Downloader downloader = new Downloader();
            byte[][] downloadedFiles = downloader.Download(urlList);

            int j = 0;
            foreach (var file in downloadedFiles)
            {
                if (!File.Exists(pathList[j]) || replace == true)
                {
                    try
                    {
                        File.WriteAllBytes(pathList[j], file);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("Error: Unable to write file.");
                        Console.WriteLine(e);
                    }
                }
                j++;
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
