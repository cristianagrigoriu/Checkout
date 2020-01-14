using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout
{
    public class CheckoutMachine
    {
        Dictionary<string, ItemPrice> itemPrices = new Dictionary<string, ItemPrice>();
        private List<string> scannedItems = new List<string>();

        public CheckoutMachine(string priceConfiguration)
        {
            itemPrices = priceConfiguration.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(line => line[0], line => GetItemPriceConfiguration(line[0], line[1]));
        }

        public void Scan(string item)
        {
            scannedItems.Add(item);
        }

        public int Total {
            get
            {
                Dictionary<string, int> orderedScannedItems = GetOrderedScannedItems();
                return orderedScannedItems.Sum(item => itemPrices[item.Key].GetTotalPrice(item.Value));
            }
        }

        private ItemPrice GetItemPriceConfiguration(string item, string line)
        {
            string[] lineElements = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            bool hasSpecialPrice = lineElements.Count() > 1;
            return new ItemPrice
            {
                Item = item,
                Price = int.Parse(lineElements[0]),
                SpecialQuantity = hasSpecialPrice ? int.Parse(lineElements[1]) : 0,
                SpecialPrice = hasSpecialPrice ? int.Parse(lineElements[3]) : 0
            };
        }

        private Dictionary<string, int> GetOrderedScannedItems()
        {
            return scannedItems.GroupBy(c => c)
                .ToDictionary(item => item.Key, item => item.Count());
        }
    }
}
