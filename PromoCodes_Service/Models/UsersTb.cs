﻿using System;
using System.Collections.Generic;

#nullable disable

namespace PromoCodes_Service.Models
{
    public partial class UsersTb
    {
        public int UserId { get; set; }
        public string LoginId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool MultiSessionEnable { get; set; }
        public short Status { get; set; }
    }
}
