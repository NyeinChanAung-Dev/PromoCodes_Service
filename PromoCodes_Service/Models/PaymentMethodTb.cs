using System;
using System.Collections.Generic;

#nullable disable

namespace PromoCodes_Service.Models
{
    public partial class PaymentMethodTb
    {
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public string Description { get; set; }
        public bool IsDiscount { get; set; }
        public int DiscountPercentage { get; set; }
        public short Status { get; set; }
    }
}
