﻿namespace Spring2024_Books.Models.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
