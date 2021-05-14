using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler
{
    public class Imgur
    {
        public async Task Run(string url)
        {
            url = FixUrl(url);

            RequestManager.httpClientHandler.CookieContainer.Add(new Uri(url), new Cookie("over18", "1"));
            dynamic jsonData = JsonConvert.DeserializeObject(Regex.Match(await RequestManager.Request(url), @"image\s+:(.*?),\s+group").Groups[1].Value);

            DownloadManager.InitDownload((double)jsonData.album_images.count);

            foreach (var image in jsonData.album_images.images)
            {
                string fileUrl = "https://i.imgur.com/" + image.hash + image.ext;
                string fileDirectory = AppManager.folderBrowser.SelectedPath + "\\" + image.hash + image.ext;
                await DownloadManager.DownloadFile(fileUrl, fileDirectory);
                DownloadManager.ChangeProgress();
            }
            
        }

        private string FixUrl(string url)
        {
            if (!url.Contains("https")) url = url.Replace("http", "https");

            if (url.Contains("gallery")) url = url.Replace("gallery", "a");

            if (!url.Contains("/all")) url += "/all";

            return url;

        }
    
    }
}
