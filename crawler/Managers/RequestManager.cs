using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler.Managers
{
    public static class RequestManager
    {
        public static readonly HttpClientHandler HttpClientHandler = new HttpClientHandler();
        private static readonly HttpClient HttpClient = new HttpClient(HttpClientHandler);
        public static async Task<string> Request(string url)
        {
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            HttpResponseMessage httpResponse = await HttpClient.GetAsync(url);
            if(!httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to connect to the website. please check the url.", "Connection Error!");
                return null;
            }
            return httpResponse.Content.ReadAsStringAsync().Result;
        }
    }
}
