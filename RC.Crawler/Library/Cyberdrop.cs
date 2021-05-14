using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RC.Crawler
{
    public class Cyberdrop
    {
        readonly Random random = new Random();

        public async Task Run(string url)
        {
            HttpResponseMessage response = await AppManager.httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to connect to the cyberdrop. please check the url.", "Connection Error!");
                return;
            }

            var imageList = Regex.Matches(response.Content.ReadAsStringAsync().Result, @"<a class=""image"" href=""(.*?)"" target=""_blank""");
            
            DownloadManager.InitDownload(imageList.Count);

            foreach (Match image in imageList)
            {
                string fileUrl = image.Groups[1].Value;
                string fileName = random.Next().ToString("X");
                string fileExt = Regex.Match(fileUrl, @"(\.mp4|.mov|\.m4v|\.ts|\.mkv|\.avi|\.wmv|\.webm|\.vob|\.gifv|\.mpg|\.mpeg|\.mp3|\.flac|\.wav|\.png|\.jpeg|\.jpg|\.gif|\.bmp|\.webp|\.heif|\.tiff|\.svf|\.svg|\.ico|\.psd|\.ai|\.pdf|\.txt|\.log|\.csv|\.xml|\.cbr|\.zip|\.rar|\.7z|\.tar|\.gz|\.iso|\.torrent|\.kdbx)").Groups[1].Value;
                string fileDirectory = AppManager.folderBrowser.SelectedPath + "\\" + fileName + fileExt;
                await DownloadManager.DownloadFile(fileUrl, fileDirectory);
                DownloadManager.ChangeProgress();

            }
        }
        
    }
}
