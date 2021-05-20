using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crawler.Interfaces;
using Crawler.Managers;
using Newtonsoft.Json;

namespace Crawler.Services
{
    public class Imgur : ICrawler
    {
        public async Task Run(string url)
        {
            url = FixUrl(url);

            RequestManager.HttpClientHandler.CookieContainer.Add(new Uri(url), new Cookie("over18", "1"));
            dynamic jsonData = JsonConvert.DeserializeObject(Regex.Match(await RequestManager.Request(url), @"image\s+:(.*?),\s+group").Groups[1].Value);
            if (jsonData == null) return;
            DownloadManager.InitDownload((double)jsonData.album_images.count);

            foreach (var image in jsonData.album_images.images)
            {
                string fileUrl = "https://i.imgur.com/" + image.hash + image.ext;
                string filePath = UiManager.FolderBrowser.SelectedPath + "\\" + image.hash + image.ext;
                if(DownloadManager.IsFileAlreadyDownloaded(filePath)) continue;
                await DownloadManager.DownloadFile(fileUrl, filePath);
                DownloadManager.ChangeProgress();
            }
            
        }

        private static string FixUrl(string url)
        {
            if (!url.Contains("https")) url = url.Replace("http", "https");

            if (url.Contains("gallery")) url = url.Replace("gallery", "a");

            if (!url.Contains("/all")) url += "/all";

            return url;

        }
    
    }
}
