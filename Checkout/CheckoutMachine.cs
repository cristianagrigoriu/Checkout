using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class CheckoutMachine
    {
        List<Item> items;

        public CheckoutMachine(string priceConfiguration)
        {
            items = priceConfiguration
                .Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(itemLine => new Item(itemLine))
                .ToList();
        }

        public void Scan(string item) => items.Find(i => i.ItemName == item).ScannedQuantity++;

        public int Total => items.Sum(item => item.GetToTalPrice());
    }
}
