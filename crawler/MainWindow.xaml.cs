
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Crawler.Managers;
using Crawler.Services;

namespace Crawler
{
    public partial class MainWindow
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

        private void Stop(object sender, RoutedEventArgs e)
        {
            DownloadManager.AbortDownload();
        }
        
        private async void Start(object sender, RoutedEventArgs e)
        {
            if (UiManager.FolderBrowser.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) return;

            UiManager.DisableUi();

            if(Search.Text.Contains("imgur.com"))
            {
                await CrawlerManager.Run(new Imgur(), Search.Text);
            }
            else if (Search.Text.Contains("4chan.org"))
            {
                await CrawlerManager.Run(new _4Chan(), Search.Text);
            }
            else if (Search.Text.Contains("cyberdrop.me"))
            {
                await CrawlerManager.Run(new Cyberdrop(), Search.Text);
            }
            UiManager.EnableUi();
        }
        private void Search_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            DownloadButton.IsEnabled = !string.IsNullOrEmpty(Search.Text);
        }
    }
}
