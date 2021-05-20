using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.Managers
{
    public static class DownloadManager
    {
        private static int _thread;
        private static int _threadLimit;
        private static bool _abortDownload;

        public static void InitDownload(double maxValue)
        {
            _thread = 0;
            _threadLimit = 10;
            UiManager.MainWindow.DownloadProgress.Value = 0;
            UiManager.MainWindow.DownloadProgress.Maximum = maxValue;
            UiManager.MainWindow.DownloadProgress.Visibility = Visibility.Visible;
            _abortDownload = false;
        }

        public static async Task DownloadFile(string fileUrl, string filePath)
        {
            while (!_abortDownload)
            {
                if (_thread < _threadLimit)
                {
                    using WebClient wb = new WebClient();
                    wb.DownloadFileCompleted += DownloadProgressCompleteEvent;
                    wb.DownloadProgressChanged += DownloadProgressChangeEvent;
                    wb.DownloadFileAsync(new Uri(fileUrl), filePath);
                    _thread++;
                    break;
                }
                await Task.Delay(100);
            }
        }

        private static void DownloadProgressChangeEvent(object sender, DownloadProgressChangedEventArgs e)
        {
            if (_abortDownload)
            {
                ((WebClient)sender).CancelAsync();
            }

        }
        public static bool IsFileAlreadyDownloaded(string filePath)
        {
            return File.Exists(filePath) && new FileInfo(filePath).Length != 0;
        }
        public static void ChangeProgress() => UiManager.MainWindow.DownloadProgress.Value++;
        public static void DownloadProgressCompleteEvent(object sender, AsyncCompletedEventArgs e) => _thread--;
        public static void AbortDownload() => _abortDownload = true;
    }
}
