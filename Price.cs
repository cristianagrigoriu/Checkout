namespace Checkout
{
    internal class ItemPrice
    {
        public int Price { get; set; }
        public int SpecialPrice { get; set; }
        public int SpecialQuantity { get; set; }
        public string Item { get; internal set; }

        public int GetTotalPrice(int quantity)
        {
            var specialPrice = 0;
            var remainingQuantity = quantity;

            if (this.SpecialQuantity != 0 && quantity >= this.SpecialQuantity)
            {
                specialPrice = this.SpecialPrice * (quantity / this.SpecialQuantity);
                remainingQuantity -= this.SpecialQuantity * (quantity / this.SpecialQuantity);
            }

            return specialPrice + this.Price * remainingQuantity;
        }
    }
}