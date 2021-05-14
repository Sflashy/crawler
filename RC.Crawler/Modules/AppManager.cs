using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RC.Crawler
{
    static class AppManager
    {
        public static bool IsDownloading;
        public static MainWindow mainWindow = (RC.Crawler.MainWindow)App.Current.MainWindow;
        public static HttpClientHandler httpClientHandler = new HttpClientHandler();
        public static Imgur imgur = new Imgur();
        public static readonly WebClient webClient = new WebClient();
        public static HttpClient httpClient = new HttpClient(httpClientHandler);
        public static FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
        public static PackIcon packIcon = new PackIcon();
        public static Cyberdrop cyberdrop = new Cyberdrop();

        public static void DisableControls()
        {
            mainWindow.Search.IsEnabled = false;
            packIcon.Height = 25;
            packIcon.Width = 25;
            packIcon.Kind = PackIconKind.CancelCircle;
            AppManager.mainWindow.DownloadButton.Content = packIcon;
            IsDownloading = true;
        }
        public static void EnableControls()
        {
            mainWindow.Search.IsEnabled = true;
            packIcon.Height = 25;
            packIcon.Width = 25;
            packIcon.Kind = PackIconKind.Download;
            AppManager.mainWindow.DownloadButton.Content = packIcon;
            IsDownloading = false;
            mainWindow.DownloadProgress.Visibility = System.Windows.Visibility.Hidden;
        }
    }
}
