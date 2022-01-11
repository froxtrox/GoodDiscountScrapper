using GoodDiscountScrapper.Interfaces;
using GoodDiscountScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodDiscountScrapper.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebScrappingProcessor _processor;
        private readonly IMailService _mailService;



        public IndexModel(ILogger<IndexModel> logger, IWebScrappingProcessor processor)
        {
            _logger = logger;
            _processor = processor;

        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var discountUrl = "https://www.footlocker.co.uk/en/category/sale/men/shoes.html?query=sale%3Arelevance%3AstyleDiscountPercent%3ASALE%3Agender%3AMen%3Aproducttype%3AShoes%3Abrand%3AJordan";
                var data = await _processor.Process(discountUrl);
                return new OkObjectResult(data);
            }
            catch (InvalidCastException iceEx)
            {
 
                return BadRequest("Invalid cast exception." + iceEx.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
                throw;
            }

        }

    }
}
