﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PromoCodes_Service.Models.ViewModel.Constant;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodes_Service.Helper
{
    public class JwtValidationHandler : JwtSecurityTokenHandler, ISecurityTokenValidator
    {
        private IConfiguration config;
        public JwtValidationHandler(IConfiguration _config)
        {
            config = _config;
        }

        public override ClaimsPrincipal ValidateToken(string token, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            JwtSecurityToken incomingToken = ReadJwtToken(token);
            string UserIdStr = incomingToken.Claims.First(claim => claim.Type == "UserID").Value;
            string UserName = incomingToken.Claims.First(claim => claim.Type == "UserName").Value;
            RSACryptoServiceProvider privateKey = new RSACryptoServiceProvider();
            privateKey.FromXmlString(config["RsaPrivateKey"]);
            var tmp = config["Issuer"] + config["Audience"];

            validationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = config["Issuer"],
                ValidAudience = config["Audience"],
                IssuerSigningKey = new RsaSecurityKey(privateKey),
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(0) //0 minute tolerance for the expiration date
            };
            Int32.TryParse(UserIdStr, out int UserId);

            LoginInformation.UserID = UserId;
            LoginInformation.UserName = UserName;
           
            return base.ValidateToken(token, validationParameters, out validatedToken);
        }
    }
}
