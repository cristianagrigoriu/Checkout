using System;

namespace Checkout
{
    internal class Item
    {
        public string ItemName { get; set; }
        public int ScannedQuantity { get; set; }
        public ItemPrice Price { get; set; }

        public Item(string itemDescription)
        {
            var itemNameAndPrice = itemDescription.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
            this.ItemName = itemNameAndPrice[0];
            this.Price = new ItemPrice(itemNameAndPrice[1]);
        }

        public int GetToTalPrice()
        {
            return Price.GetTotalPrice(ScannedQuantity);
        }
    }
}
