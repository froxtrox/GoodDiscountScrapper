using GoodDiscountScrapper.Services;
using GoodDiscountScrapper.Test.Helper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodDiscountScrapper.Test.Services
{
    [TestFixture]
    public class Process
    {



        private DependencyResolverHelper _serviceProvider;
        private readonly IWebScrappingProcessor sut;

        public Process()
        {

            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelper(webHost);
            sut = _serviceProvider.GetService<IWebScrappingProcessor>();
        }

        [Test]
        public void sut_Should_Get_Resolved()
        {



            //Assert
            Assert.IsNotNull(sut);
        }


    }
}
