using System;
using System.Collections.Generic;

namespace EShop.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }
        public string? Sku { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Price { get; set; }
        public string? CategoryId { get; set; }
    }
}
