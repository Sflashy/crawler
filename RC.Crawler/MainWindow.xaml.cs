
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RC.Crawler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Drag(object sender, MouseButtonEventArgs e) => this.DragMove();
        private void MinimizeApplication(object sender, MouseButtonEventArgs e) => this.WindowState = WindowState.Minimized;
        private void ExitApplication(object sender, MouseButtonEventArgs e) => Application.Current.Shutdown();
        private void ExitAppMouseEnter(object sender, MouseEventArgs e) => ExitApp.Background = new SolidColorBrush(Color.FromRgb(200, 10, 10));
        private void ExitAppMouseLeave(object sender, MouseEventArgs e) => ExitApp.Background = Brushes.Transparent;

        private async void Download(object sender, RoutedEventArgs e)
        {
            if (AppManager.IsDownloading)
            {
                DownloadManager.CancelDownload();
                AppManager.IsDownloading = false;
                return;
            }
                
            string searchType = Regex.Match(AppManager.mainWindow.Search.Text, @"http.*\/\/(.*?)\.").Groups[1].Value;
            if (AppManager.folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            AppManager.DisableControls();
            switch (searchType)
            {
                case "imgur":
                    await AppManager.imgur.Run(Search.Text);
                    break;
                case "cyberdrop":
                    await AppManager.cyberdrop.Run(Search.Text);
                    break;
            }

            AppManager.EnableControls();
        }

        private void DownloadMouseEnter(object sender, MouseEventArgs e) => DownloadButton.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
        private void DownloadMouseLeave(object sender, MouseEventArgs e) => DownloadButton.Background = Brushes.Transparent;
    }
}
