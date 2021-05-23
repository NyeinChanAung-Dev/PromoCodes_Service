﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PromoCodes_Service.Models
{
    public partial class PurchaseHistoryTb
    {
        public int PurchaseHistoryId { get; set; }
        public string PromoCode { get; set; }
        public string VoucherNo { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public short Status { get; set; }
    }
}
