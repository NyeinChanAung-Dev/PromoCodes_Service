﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PromoCodes_Service.Models.ViewModel.ResponseModels
{
    public sealed class Error
    {
        public string Code { get; }
        public string Message { get; }

        public Error(string code, string description)
        {
            Code = code;
            Message = description;
        }
    }
}
