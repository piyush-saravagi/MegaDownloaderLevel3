using DownloaderLibrary;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Service
{
    public class StreamingDownloader
    {
        private BlockingCollection<string> urls = new BlockingCollection<string>();
        private List<dynamic> results = new List<dynamic>();
        private Downloader downloader = new Downloader();

        public StreamingDownloader()
        {
            // Start the Download asynchronously  
            // Forget and fire
            Task downloadTask = Task.Run(() =>
            {
                // fire and forget
                while (!urls.IsCompleted)
                {
                    foreach (var downloadedFile in downloader.Download(urls))
                    {
                        results.Add(downloadedFile);
                    }
                }
            });
        }

        public bool AddUrl(string url)
        {
            if (url.Equals("DONE"))
            {
                urls.CompleteAdding();
            }
            return urls.TryAdd(url);
        }

        public int DownloadedCount()
        {
            return results.Count();
        }

        public IEnumerable<dynamic> GetResults()
        {

            //foreach (var downloadedFile in downloader.Download(urls))
            //{
            //    results.Add(downloadedFile);
            //}
            return results; //can cause a NRE in the caller when no downloads 
        }

        public int PendingDownloadsCount()
        {
            return urls.Count;
        }

        public bool DownloadsComplete()
        {
            return urls.IsCompleted && PendingDownloadsCount() == 0;
        }


        public bool HasDownloadData()
        {
            if (results == null)
                return false;
            return results.Count() != 0;
        }
    }
}