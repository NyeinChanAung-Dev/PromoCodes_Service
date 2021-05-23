using PromoCodes_Service.Models.ViewModel.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromoCodes_Service.Models.ViewModel.ResponseModels
{
    public class GeneratePromoCodeResponse:ResponseBase
    {
        public bool PromoCodeGenerated { get; set; }
    }
}
