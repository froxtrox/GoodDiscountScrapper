namespace GoodDiscountScrapper.Services
{
    public class DiscountInfo
    {
        public string Name { get; set; }
        public string Link { get; set; }
        public string Category { get; set; } = "Shoe";

        public string Brand { get; set; } = "Jordan";

        public string OriginalPrice { get; set; }

        public string FinalPrice { get; set; }

        public string Discount { get; set; }

        public string Comment { get; set; }
    }
}