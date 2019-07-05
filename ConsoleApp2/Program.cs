using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Service;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamingDownloader downloader = new TCPServer().Connect();

            //for (int i = 0; i < 100; i++)
            //{
            //    if (downloader.GetResults() != null)
            //    {
            //        Console.WriteLine(downloader.GetResults().Count());
            //    }
            //    else
            //    {
            //        Console.WriteLine(0);
            //    }
            //}

            for (int i = 0; i < 100; i++)
            {
                downloader.AddUrl("http://www.mocky.io/v2/5d1f03823100001f55ebe9f0?mocky-delay=1000ms");
                Console.WriteLine("DownloadedCount" + downloader.DownloadedCount());
                foreach (var content in downloader.GetResults().ToList())
                    Console.WriteLine("urls" + downloader.PendingDownloadsCount());
                if (i == 11)
                {
                    ;
                }
                if (i == 10)
                {
                    downloader.AddUrl("http://www.mocky.io/v2/5d1f03823100001f55ebe9f0?mocky-delay=3000ms");
                    downloader.AddUrl("http://www.mocky.io/v2/5d1f03823100001f55ebe9f0?mocky-delay=3000ms");
                    //Thread.Sleep(10000);
                    //Console.WriteLine("Sleeping for 10 second");
                }
            }

            while (downloader.PendingDownloadsCount() != 0)
            {
                Console.WriteLine("Downloaded " + downloader.DownloadedCount() + " files");
                Console.WriteLine("Pending " + downloader.PendingDownloadsCount() + "files");
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
