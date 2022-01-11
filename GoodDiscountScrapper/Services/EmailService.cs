using GoodDiscountScrapper.Interfaces;
using System.Threading.Tasks;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using FluentEmail.Core;
using System;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Collections.Generic;

namespace GoodDiscountScrapper.Services
{
    public class EmailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly IFluentEmail _email;
        public EmailService(IConfiguration configuration, IFluentEmail email)
        {
            _configuration = configuration;
            _email = email;
        }
        public async Task<bool> Send(List<DiscountInfo> dataItems)
        {
            bool isSuccess = false;
            //var sender = new SmtpSender(() => new System.Net.Mail.SmtpClient("localhost")
            //{
            //    // FOR TESTING PURPOSE
            //    EnableSsl = !(bool)_configuration.GetValue(typeof(bool), "UseDummyRequests"),
            //    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
            //    Port = 25
            //});
            ////Email.DefaultSender = sender;


            StringBuilder template = new();
            template.AppendLine("Dear friend");
            template.AppendLine("<ul>");
            template.AppendLine(@"@foreach (var item in Model.Foos)
                                {
                                  <li>@item.Name</li>
                                }");
            template.AppendLine("</ul>");
            template.AppendLine("- Gongzhu");

            try
            {
                var email = await _email
                    .To("gongzhuli@live.com", "gongzhu")
                    .Body("Test test")
                    .UsingTemplate(template.ToString(), new { DataItems = dataItems })
                    .SendAsync();

                if(email.Successful) isSuccess = true; 
            }
            catch (Exception ex)
            {
                throw;
            }

            return isSuccess;
        }
    }
}
