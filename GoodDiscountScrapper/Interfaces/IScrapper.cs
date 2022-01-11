using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodDiscountScrapper.Services
{
    public interface IScrapper
    {
        Task<HtmlDocument> LoadHTMLAsync(string url);
    }
}
