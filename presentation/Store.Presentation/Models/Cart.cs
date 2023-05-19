﻿namespace Store.Presentation.Models
{
    public class Cart
    {
        public int OrderId { get; set; }
        public int TotalCount { get; set; }
        public decimal TotalPrice { get; set; }

        public Cart(int orderId)
        {
            OrderId = orderId;
            TotalCount = 0;
            TotalPrice = 0m;
        }
    }
}
