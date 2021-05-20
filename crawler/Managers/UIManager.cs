using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace Crawler.Managers
{
    public static class UiManager
    {
        public static readonly MainWindow MainWindow = (Crawler.MainWindow)Application.Current.MainWindow;
        public static readonly FolderBrowserDialog FolderBrowser = new FolderBrowserDialog();

        public static void DisableUi()
        {
            MainWindow.Search.IsEnabled = false;
            MainWindow.DownloadButton.Visibility = Visibility.Hidden;
            MainWindow.StopButton.Visibility = Visibility.Visible;
        }
        public static void EnableUi()
        {
            MainWindow.Search.IsEnabled = true;
            MainWindow.DownloadButton.Visibility = Visibility.Visible;
            MainWindow.StopButton.Visibility = Visibility.Hidden;
            MainWindow.DownloadProgress.Visibility = Visibility.Hidden;
        }
    }
}
