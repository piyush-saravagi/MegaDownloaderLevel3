using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DownloaderLibrary;

namespace Service
{
    // The service
    public class TCPServer
    {
        //Keep listening for connection requests and on successful connections
        //call the connect method that returns the StreamingDownloader
        public StreamingDownloader Connect()
        {
            // Create a new streaming downloader object that will handle the streaming for the current 
            // Each client gets a new StreamingDownloader object
            // Which provides an interface to add/download from url
            return new StreamingDownloader();
        }
    }
}
