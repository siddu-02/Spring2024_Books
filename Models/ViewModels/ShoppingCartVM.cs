﻿namespace Spring2024_Books.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<Cart> CartItems { get; set; }

        public Order Order { get; set; }

    }
}
