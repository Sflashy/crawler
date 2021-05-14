using MaterialDesignThemes.Wpf;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RC.Crawler
{
    static class AppManager
    {
        private static readonly Random random = new Random();
        public static bool IsDownloading;
        public static MainWindow mainWindow = (RC.Crawler.MainWindow)App.Current.MainWindow;
        public static Imgur imgur = new Imgur();
        public static readonly WebClient webClient = new WebClient();
        public static FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
        public static PackIcon packIcon = new PackIcon();
        public static Cyberdrop cyberdrop = new Cyberdrop();
        public static _4Chan _4chan = new _4Chan();

        public static void DisableControls()
        {
            mainWindow.Search.IsEnabled = false;
            packIcon.Height = 25;
            packIcon.Width = 25;
            packIcon.Kind = PackIconKind.CancelCircle;
            mainWindow.DownloadButton.Content = packIcon;
            IsDownloading = true;
        }
        public static void EnableControls()
        {
            mainWindow.Search.IsEnabled = true;
            packIcon.Height = 25;
            packIcon.Width = 25;
            packIcon.Kind = PackIconKind.Download;
            mainWindow.DownloadButton.Content = packIcon;
            IsDownloading = false;
            mainWindow.DownloadProgress.Visibility = System.Windows.Visibility.Hidden;
        }
        public static string GetFileExt(string fileUrl)
        {
            return Regex.Match(fileUrl, @"(\.mp4|.mov|\.m4v|\.ts|\.mkv|\.avi|\.wmv|\.webm|\.vob|\.gifv|\.mpg|\.mpeg|\.mp3|\.flac|\.wav|\.png|\.jpeg|\.jpg|\.gif|\.bmp|\.webp|\.heif|\.tiff|\.svf|\.svg|\.ico|\.psd|\.ai|\.pdf|\.txt|\.log|\.csv|\.xml|\.cbr|\.zip|\.rar|\.7z|\.tar|\.gz|\.iso|\.torrent|\.kdbx)").Groups[1].Value;
        }

        public static string GetRanmdomFileName()
        {
            return random.Next().ToString("X");
        }
            
    }
}
