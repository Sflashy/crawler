using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace Crawler
{
    public static class RequestManager
    {
        public static HttpClientHandler httpClientHandler = new HttpClientHandler();
        public static HttpClient httpClient = new HttpClient(httpClientHandler);
        public static async Task<string> Request(string url)
        {
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:88.0) Gecko/20100101 Firefox/88.0");
            HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
            if(!httpResponse.IsSuccessStatusCode)
            {
                MessageBox.Show("Failed to connect to the website. please check the url.", "Connection Error!");
                return null;
            }
            return httpResponse.Content.ReadAsStringAsync().Result;
        }
    }
}
