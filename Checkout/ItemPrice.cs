using System;
using System.Linq;

namespace Checkout
{
    internal class ItemPrice
    {
        public int NormalPrice { get; set; }
        public int SpecialPrice { get; set; }
        public int SpecialQuantity { get; set; }

        public ItemPrice(string priceDescription)
        {
            var prices = priceDescription.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var hasSpecialPrice = prices.Count() > 1;

            NormalPrice = int.Parse(prices[0]);
            SpecialQuantity = hasSpecialPrice ? int.Parse(prices[1]) : 0;
            SpecialPrice = hasSpecialPrice ? int.Parse(prices[3]) : 0;
        }

        public int GetTotalPrice(int quantity)
        {
            var remaningQuantity = quantity;
            var specialPrice = 0;

            if (CanApplySpecialPrice())
            {
                var maxTimesTheSpecialPriceIsApplied = quantity / this.SpecialQuantity;
                specialPrice = SpecialPrice * maxTimesTheSpecialPriceIsApplied;
                remaningQuantity -= SpecialQuantity * maxTimesTheSpecialPriceIsApplied;
            }

            return specialPrice + this.NormalPrice * remaningQuantity;
        }

        private bool CanApplySpecialPrice()
        {
            return this.SpecialQuantity != 0;
        }
    }
}