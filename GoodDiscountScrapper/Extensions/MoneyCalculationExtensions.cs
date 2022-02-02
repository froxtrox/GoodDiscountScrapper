using System;
using System.Globalization;

namespace GoodDiscountScrapper.Extensions
{
    public static class MoneyCalculationExtensions
    {
        public static decimal GetDiscountPercentage(string orig, string final)
        {
            var origlDecimal = decimal.Parse(orig, NumberStyles.Currency);
            var finalDecimal = decimal.Parse(final, NumberStyles.Currency);
            return (1 - finalDecimal / origlDecimal) * 100;
        }
    }
}
