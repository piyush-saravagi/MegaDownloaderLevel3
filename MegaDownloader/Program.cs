using System;
using System.IO;
using System.Net;
using DownloaderLibrary;

namespace MegaDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = args[0];
            string path = args[1];
            bool replace = false;
            if (args.Length > 2 && args[2] == "r")
                replace = true;

            if (replace || !File.Exists(path))
            {
                // Download only if
                // 1. file replacement is allowed
                // 2. file is not present
                Downloader downloader = new Downloader();
                bool downloadSuccess = true;
                byte[] downloadedFile = null;
                try
                {
                    downloadedFile = downloader.Download(url);
                }
                catch (WebException webException)
                {
                    // Application specific handling
                    Console.WriteLine("Error: Unable to download file from the url");
                    Console.WriteLine(webException);    // Provide more details
                    downloadSuccess = false;
                }


                // Save file to the path
                if (downloadSuccess)
                {
                    // Save to file
                    try
                    {
                        File.WriteAllBytes(path, downloadedFile);
                    }
                    catch (IOException fileWriteException)
                    {
                        Console.WriteLine("Error: Unable to save the downloaded file");
                        Console.WriteLine(fileWriteException);
                    }
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
