namespace GoodDiscountScrapper.Extensions
{
    public static class UrlExtensions
    {
        public static string GetFullUrl(this string href, string baseUrl) => $"{baseUrl}{href}";

    }
}
