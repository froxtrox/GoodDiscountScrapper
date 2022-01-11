using System;
using System.Globalization;

namespace GoodDiscountScrapper.Extensions
{
    public static class MoneyCalculationExtensions
    {
        public static string GetDiscountPercentage(string orig, string final)
        {
            var origlDecimal = decimal.Parse(orig, NumberStyles.Currency);
            var finalDecimal = decimal.Parse(final, NumberStyles.Currency);
            return String.Format("{0:n1}",  (1 - finalDecimal / origlDecimal) * 100) + " %";
        }
    }
}
