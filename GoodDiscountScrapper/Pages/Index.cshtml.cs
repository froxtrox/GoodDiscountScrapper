using GoodDiscountScrapper.Interfaces;
using GoodDiscountScrapper.Options;
using GoodDiscountScrapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace GoodDiscountScrapper.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IWebScrappingProcessor _processor;
        private readonly TwilioAccountOptions _optionsDelegate;




        public IndexModel(ILogger<IndexModel> logger, IWebScrappingProcessor processor, IOptionsMonitor<TwilioAccountOptions> optionsDelegate)
        {
            _logger = logger;
            _processor = processor;
            _optionsDelegate = optionsDelegate.CurrentValue;

        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var discountUrl = "https://www.footlocker.co.uk/en/category/sale/men/shoes.html?query=sale%3Arelevance%3AstyleDiscountPercent%3ASALE%3Agender%3AMen%3Aproducttype%3AShoes%3Abrand%3AJordan";
                var data = await _processor.Process(discountUrl);


                var sb = new StringBuilder("Top J Discount ");

                foreach (var item in data)
                {
                    sb.Append(item.ToString());
                }

              

                // Find your Account SID and Auth Token at twilio.com/console
                // and set the environment variables. See http://twil.io/secure
                string accountSid = _optionsDelegate.AccountSid;
                string authToken = _optionsDelegate.AuthToken;

                TwilioClient.Init(accountSid, authToken);


                var message = MessageResource.Create(
                    body: sb.ToString(),
                    to: new Twilio.Types.PhoneNumber(""),
                    from: new Twilio.Types.PhoneNumber("")
                );

                return new OkObjectResult(message.Sid);



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
