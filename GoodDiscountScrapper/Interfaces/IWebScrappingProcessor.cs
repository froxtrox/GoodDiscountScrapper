using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodDiscountScrapper.Services
{
    public interface IWebScrappingProcessor
    {
        Task<IEnumerable<DiscountInfo>> Process(string url);
        void Save();
    }
}
