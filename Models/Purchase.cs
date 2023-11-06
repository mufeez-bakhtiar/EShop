using System;
using System.Collections.Generic;

namespace EShop.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? VendorName { get; set; }
        public DateTime? DateOfPurchase { get; set; }
    }
}
