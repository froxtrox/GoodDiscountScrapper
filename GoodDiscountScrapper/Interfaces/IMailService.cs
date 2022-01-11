using GoodDiscountScrapper.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoodDiscountScrapper.Interfaces
{
    public interface IMailService
    {
        Task<bool> Send(List<DiscountInfo> dataItems);
    }
}
