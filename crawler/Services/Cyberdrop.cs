using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Crawler.Interfaces;
using Crawler.Managers;

namespace Crawler.Services
{
    public class Cyberdrop: ICrawler
    {
        public async Task Run(string url)
        {
            string htmlBody = await RequestManager.Request(url);
            if (string.IsNullOrEmpty(htmlBody)) return;
            var imageList = Regex.Matches(htmlBody, @"<a class=""image"" href=""(.*?)"" target=""_blank""");
            DownloadManager.InitDownload(imageList.Count);

            foreach (Match image in imageList)
            {
                string fileUrl = image.Groups[1].Value;
                string fileName = fileUrl.GetHashCode().ToString("X");
                string fileExt = CrawlerManager.GetFileExt(fileUrl);
                string filePath = UiManager.FolderBrowser.SelectedPath + "\\" + fileName + fileExt;
                await DownloadManager.DownloadFile(fileUrl, filePath);
                DownloadManager.ChangeProgress();
            }
        }
        
    }
}
