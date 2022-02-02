using HtmlAgilityPack;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GoodDiscountScrapper.Services
{
    public class HTMLScrapper : IScrapper
    {
        private readonly IConfiguration _configuration;
        public HTMLScrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<HtmlDocument> LoadHTMLAsync(string url)
        {

            HtmlDocument response;
            if ((bool)_configuration.GetValue(typeof(bool), "UseDummyRequests") == true)
            {
                response = await GetDummyResponse();
            }
            else
            {
                HttpClient client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
                client.DefaultRequestHeaders.Accept.Clear();
                HtmlWeb web = new HtmlWeb();
                response = web.Load(url);
                //var node = response.DocumentNode.SelectSingleNode("//head/title");
            }
            return response;
        }

        private async Task<HtmlDocument> GetDummyResponse()
        {
            string content = await File.ReadAllTextAsync("dummyFLResponse.txt");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            return doc;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
