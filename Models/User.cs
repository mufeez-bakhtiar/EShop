using System;
using System.Collections.Generic;

namespace EShop.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Cnic { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? SystemUserId { get; set; }
        public string? Address { get; set; }
        public string? Role { get; set; }
    }
}
