using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RC.Crawler
{
    public static class DownloadManager
    {
        public static int thread;
        public static int threadLimit;

        public static void InitDownload(double maxValue)
        {
            thread = 0;
            threadLimit = 10;
            AppManager.mainWindow.DownloadProgress.Value = 0;
            AppManager.mainWindow.DownloadProgress.Maximum = maxValue;
            AppManager.mainWindow.DownloadProgress.Visibility = Visibility.Visible;

        }

        public static async Task DownloadFile(string fileUrl, string directory)
        {
            while (true)
            {
                if (thread < threadLimit)
                {
                    WebClient wb = new WebClient();
                    wb.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadProgressCompleteEvent);
                    wb.DownloadFileAsync(new Uri(fileUrl), directory);
                    thread++;
                    break;
                }
                await Task.Delay(100);
            }
        }

        public static void ChangeProgress()
        {
            AppManager.mainWindow.DownloadProgress.Value++;
        }
        public static void DownloadProgressCompleteEvent(object sender, AsyncCompletedEventArgs e)
        {
            thread--;
            //AppManager.mainWindow.DownloadProgress.Visibility = Visibility.Hidden;
        }
        public static void CancelDownload()
        {
            if(AppManager.webClient != null)
                AppManager.webClient.CancelAsync();
        }
    }
}
