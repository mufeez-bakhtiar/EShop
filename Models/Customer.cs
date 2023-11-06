using System;
using System.Collections.Generic;

namespace EShop.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Cnic { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Purchased { get; set; }
    }
}
