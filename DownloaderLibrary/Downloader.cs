using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace DownloaderLibrary
{
    public class Downloader
    {
        /*
         Downloads a file from the url provided to the specified path on the local system.

         Args:
            url: URL of the file to be downloaded
            path: Destination of the file on the local system.

          Returns:
            No return value. Exceptions are passed on to be handled by the caller
        */
        public byte[] Download(string url)
        {
            //Cannot download, null url
            if (url == null)
            {
                throw new UriFormatException("URL cannot be null");
            }

            WebClient webClient = new WebClient();
            byte[] fileContents;
            try
            {
                // Might return a WebException that is passed on to the caller
                fileContents = webClient.DownloadData(url);
            }
            finally
            {
                webClient.Dispose();
            }
            return fileContents;

        }


        /*
         * Overloaded function to download multiple files
         */
        public List<byte[]> Download(List<string> urlList)
        {
            List<byte[]> downloadResults = new List<byte[]>();
            for (int i = 0; i < urlList.Count; i++)
            {
                // This throws an WebException when one of the downloads fail
                // This exception is passed on to the caller
                // This assumes that downloading the other files does not make sense if one of the files fail
                downloadResults.Add(Download(urlList[i]));
            }
            return downloadResults;
        }


        /*
          * Streaming download using BlockingCollection
          */
        public List<byte[]> Download(BlockingCollection<KeyValuePair<string, string>> urlPathPairCollection)
        {
            List<byte[]> downloadResults = new List<byte[]>();


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







            for (int i = 0; i < urlPathPairCollection.Count; i++)
            {
                // This throws an WebException when one of the downloads fail
                // This exception is passed on to the caller
                // This assumes that downloading the other files does not make sense if one of the files fail
                downloadResults.Add(Download(urlPathPairCollection[i]));
            }
            return downloadResults;
        }
    }
}
