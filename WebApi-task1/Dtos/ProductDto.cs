﻿namespace WebApi_task1.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public decimal Price { get; set; }
        public int Discount { get; set; }
    }
}
