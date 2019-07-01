﻿using System;
using System.Net;
using System.IO;
using System.Collections.Generic;

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
    }
}
