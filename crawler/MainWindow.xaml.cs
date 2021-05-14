
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Crawler
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
                

            if (AppManager.folderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            AppManager.DisableControls();
            if(Search.Text.Contains("imgur.com"))
            {
                await AppManager.imgur.Run(Search.Text);
            }
            else if (Search.Text.Contains("4chan.org"))
            {
                await AppManager._4chan.Run(Search.Text);
            }
            else if (Search.Text.Contains("cyberdrop.me"))
            {
                await AppManager.cyberdrop.Run(Search.Text);
            }
            AppManager.EnableControls();
        }

        private void DownloadMouseEnter(object sender, MouseEventArgs e) => DownloadButton.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
        private void DownloadMouseLeave(object sender, MouseEventArgs e) => DownloadButton.Background = Brushes.Transparent;

        private void Search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Search.Text))
                DownloadButton.IsEnabled = false;
            else
                DownloadButton.IsEnabled = true;
        }
    }
}
