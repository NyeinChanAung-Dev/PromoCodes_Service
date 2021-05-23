﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PromoCodes_Service.Models
{
    public partial class PurchaseOrderTb
    {
        public string PurchaseOrderNo { get; set; }
        public string VoucherNo { get; set; }
        public string ImagePath { get; set; }
        public string BuyerName { get; set; }
        public string BuyerPhone { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BuyType { get; set; }
        public string PaymentMethod { get; set; }
        public decimal VoncherAmount { get; set; }
        public decimal SellingPrice { get; set; }
        public int SellingDiscount { get; set; }
        public int Quantity { get; set; }
        public decimal TotalSellingAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public bool VoucherGenerated { get; set; }
        public short Status { get; set; }
        public int Id { get; set; }
    }
}
