using System;

namespace GoodDiscountScrapper.Services
{
    public class DiscountInfo
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }

        public string Brand { get; set; }

        public string OriginalPrice { get; set; }

        public string FinalPrice { get; set; }

        public string DiscountString => String.Format("{0:n1}", Discount + " %");

        public decimal Discount { get; set; }
        public string Comment { get; set; }

        public override string ToString() => String.Format($"Name: {0} Discount: {1}  Link: {2}", Name, DiscountString, Link);

    }
}