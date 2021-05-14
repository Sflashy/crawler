using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RC.Crawler
{
    public class _4Chan
    {
        public async Task Run(string url)
        {
            string htmlBody = await RequestManager.Request(url);
            if (htmlBody == null) return;
            MatchCollection imageList = Regex.Matches(htmlBody, @"class=""fileThumb"" href=""(.*?)"" target");
            DownloadManager.InitDownload(imageList.Count);
            foreach (Match image in imageList)
            {
                string fileUrl = "https:" + image.Groups[1].Value;
                string fileName = AppManager.GetRanmdomFileName();
                string fileExt = AppManager.GetFileExt(fileUrl);
                string fileDirectory = AppManager.folderBrowser.SelectedPath + "/" + fileName + fileExt;
                await DownloadManager.DownloadFile(fileUrl, fileDirectory);
                DownloadManager.ChangeProgress();
            }
        }
    }
}
