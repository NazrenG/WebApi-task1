﻿namespace WebApi_task1.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Discount {  get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}
