using System;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace RC.Crawler
{
    public static class DownloadManager
    {
        public static int thread;
        public static int threadLimit;
        public static bool abortDownload;

        public static void InitDownload(double maxValue)
        {
            thread = 0;
            threadLimit = 10;
            AppManager.mainWindow.DownloadProgress.Value = 0;
            AppManager.mainWindow.DownloadProgress.Maximum = maxValue;
            AppManager.mainWindow.DownloadProgress.Visibility = Visibility.Visible;
            abortDownload = false;

        }

        public static async Task DownloadFile(string fileUrl, string fileDirectory)
        {
            while (!abortDownload)
            {
                if (thread < threadLimit)
                {
                    WebClient wb = new WebClient();
                    wb.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadProgressCompleteEvent);
                    wb.DownloadFileAsync(new Uri(fileUrl), fileDirectory);
                    thread++;
                    break;
                }
                await Task.Delay(100);
            }
        }

        public static void ChangeProgress() => AppManager.mainWindow.DownloadProgress.Value++;
        public static void DownloadProgressCompleteEvent(object sender, AsyncCompletedEventArgs e) => thread--;
        public static void CancelDownload() => abortDownload = true;
    }
}
