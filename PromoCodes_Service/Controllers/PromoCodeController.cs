using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PromoCodes_Service.Manager;
using PromoCodes_Service.Models.ViewModel.RequestModels;
using PromoCodes_Service.Models.ViewModel.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromoCodes_Service.Controllers
{
    [ApiController]
    public class PromoCodeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private PromoCodeManager _pcodemgr;

        public PromoCodeController(IConfiguration configuration)
        {
            _configuration = configuration;
            _pcodemgr = new PromoCodeManager(_configuration);
        }

        [Route("api/PromoCode/GeneratePromoCode")]
        [HttpPost]
        public IActionResult GeneratePromoCode(GeneratePromoCodeRequest _request)
        {
            try
            {
                var response = _pcodemgr.GeneratePromoCode(_request);
                if (response.StatusCode == 200)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode(response.StatusCode, response.GetError());
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new Error("internal-error", e.Message));
            }
        }

    }
}
