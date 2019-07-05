using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DownloaderLibrary;



namespace StreamingDownlaoder
{
    class Program
    {
        public static void Main(string[] args)
        {

        }
    }
}
/*
namespace StreamingDownloader
{
    class Program
    {


        static void Main(string[] args)
        {
            BlockingCollection<KeyValuePair<string, string>> urlPathPairCollection = new BlockingCollection<KeyValuePair<string, string>>();


            Task.Run(() =>
            {

            })
            //This task adds urls to the urlList 
            Task.Run(() =>
            {
                //Adding items to the blocking collection
                //Not all items are added to the collection at once
                //This simulates the case where new data is being added to the list continously with delays
                for (int i = 0; i < 5; i++)
                {
                    string path = "D:/ demo - " + i + ".png";
                    urlPathPairCollection.TryAdd(new KeyValuePair<string, string>(@"http://lorempixel.com/500/500", path));
                    Thread.Sleep(new Random().Next(3000, 10000)); // wait for anywhere between 3 to 10 seconds to simulate data coming in continously after some delay
                }

                // Indicating that the task has completed
                urlPathPairCollection.CompleteAdding();
            });

            Downloader downloader = new Downloader();

            // This task uses the urls and the paths from the streaming list and downloads files
            Task consumer = Task.Run(() =>
            {


                //Continue till the addition of elements is not complete and the collection is not empty
                while (!urlPathPairCollection.IsCompleted)
                {
                    bool elementInCollection = false;
                    elementInCollection = urlPathPairCollection.TryTake(out KeyValuePair<string, string> pair);
                    // next steps should be done only if we were able to extract an element from the collection
                    if (elementInCollection)
                    {
                        string url = pair.Key;
                        string path = pair.Value;
                        byte[] downloadedFile = null;
                        bool downloadSuccess = true;

                        try
                        {
                            downloadedFile = downloader.Download(url);
                            Console.WriteLine("Successfully downloaded file from url: " + url);
                            Thread.Sleep(5000);
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
                                Console.WriteLine("Successfully saved file to " + path);
                            }
                            catch (IOException fileWriteException)
                            {
                                Console.WriteLine("Error: Unable to save the downloaded file");
                                Console.WriteLine(fileWriteException);
                            }
                        }
                    }

                }
            });


            consumer.Wait();
            Console.WriteLine("Enter any key to exit");
            Console.ReadKey();
        }
    }
}
*/
