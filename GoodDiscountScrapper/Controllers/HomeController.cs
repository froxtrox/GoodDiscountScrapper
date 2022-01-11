using GoodDiscountScrapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GoodDiscountScrapper
{
    [Route("[controller]")]
    [Controller]
    public class HomeController : ControllerBase
    {
        private readonly IScrapper _scrapper;
        public HomeController(IScrapper scrapper)
        {
            _scrapper = scrapper;
        }

        [HttpGet()]
        public async Task<IActionResult> Index()
        {
            var discountUrl = "https://www.footlocker.co.uk/en/category/sale/men/shoes.html?query=sale%3Arelevance%3AstyleDiscountPercent%3ASALE%3Agender%3AMen%3Aproducttype%3AShoes%3Abrand%3AJordan";
            var data = await _scrapper.LoadHTMLAsync(discountUrl);
            return Ok(data);
        }
    }
}
