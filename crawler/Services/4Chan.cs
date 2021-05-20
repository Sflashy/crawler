using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crawler.Interfaces;
using Crawler.Managers;

namespace Crawler.Services
{
    public class _4Chan : ICrawler
    {
        public async Task Run(string url)
        {
            var htmlBody = await RequestManager.Request(url);
            if (htmlBody == null) return;
            var imageList = Regex.Matches(htmlBody, @"class=""fileThumb"" href=""(.*?)"" target");
            DownloadManager.InitDownload(imageList.Count);
            foreach (Match image in imageList)
            {
                var fileUrl = "https:" + image.Groups[1].Value;
                var fileExt = CrawlerManager.GetFileExt(fileUrl);
                var fileName = Regex.Match(fileUrl, @"http.*\/\/i\.4cdn\.org\/.*\/(.*)\.").Groups[1].Value;
                var filePath = UiManager.FolderBrowser.SelectedPath + "/" + fileName + fileExt;
                if (DownloadManager.IsFileAlreadyDownloaded(filePath)) continue;
                await DownloadManager.DownloadFile(fileUrl, filePath);
                DownloadManager.ChangeProgress();
            }
        }
    }
}