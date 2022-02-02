using HtmlAgilityPack;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;
using GoodDiscountScrapper.Extensions;
using GoodDiscountScrapper.Interfaces;
using Microsoft.Extensions.Options;
using GoodDiscountScrapper.Options;
 
namespace GoodDiscountScrapper.Services
{
    public class DealDiscountProcessor : IWebScrappingProcessor
    {
        public const string TargetBaseUrl = "https://www.footlocker.co.uk/";
        private readonly IScrapper _scrapper;
        private readonly IMailService _emailService;
        private readonly IOptionsMonitor<SearchInfoOptions> _optionsDelegate;

        public DealDiscountProcessor(IScrapper scrapper, IMailService emailService, IOptionsMonitor<SearchInfoOptions> optionsDelegate)
        {
            _scrapper = scrapper;
            _emailService = emailService;
            _optionsDelegate = optionsDelegate;
        }

        public async Task<IEnumerable<DiscountInfo>> Process(string url)
        {
            var htmlDoc = await _scrapper.LoadHTMLAsync(url);
            var SearchResultsNode = htmlDoc.DocumentNode.Descendants(0).First(n => n.HasClass("SearchResults"));
            var listNode = SearchResultsNode.SelectNodes("//ul/li/div/a");
            var list = new List<DiscountInfo>();
            foreach (var node in listNode)
            {
                list.Add(GetDiscountInfoFromNode(node));
            }

            //Get top 3 discounts
            list = list.OrderByDescending(x => x.Discount).Take(3).ToList();

            //await _emailService.Send(list);
 
            return list;
        }
 

        public void Save()
        {
            throw new System.NotImplementedException();
        }


        private DiscountInfo GetDiscountInfoFromNode(HtmlNode node)
        {
            {

                var nameNode = node.Descendants(0).First(x => x.HasClass("ProductName-primary")).InnerText;
                var finalPrice = node.Descendants(0).First(n => n.HasClass("ProductPrice-final")).InnerText;
                var originaPrice = node.Descendants(0).First(n => n.HasClass("ProductPrice-original")).InnerText;
                var link = node.GetAttributeValue("href", "");
                //en/product/jordan-delta-menshoes/314103478104.html
                return new DiscountInfo()
                {
                    Name = nameNode,
                    FinalPrice = finalPrice,
                    OriginalPrice = originaPrice,
                    Link = link.GetFullUrl(TargetBaseUrl),
                    Brand = _optionsDelegate.CurrentValue.Brand,
                    Category = _optionsDelegate.CurrentValue.Category,
                    Discount = MoneyCalculationExtensions.GetDiscountPercentage(originaPrice, finalPrice)
                };
            }
        }
    }
}