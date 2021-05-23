using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PromoCodes_Service.Helper;
using PromoCodes_Service.Models;
using PromoCodes_Service.Models.ViewModel.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoCodes_Service.Extension
{
    public static class ServiceConfiguration
    {
        //public static void ConfigureDbContex(this IServiceCollection services, IConfiguration config)
        //{
        //    services.AddDbContext<EVoucherSystemDBContext>(options => options.UseSqlServer(config.GetConnectionString("EVoucherSystemDBString")));
        //}

        public static void ConfigJwtAuthorization(this IServiceCollection services, IConfiguration config)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";

            }).AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.SecurityTokenValidators.Clear();
                jwtBearerOptions.SecurityTokenValidators.Add(new JwtValidationHandler(config));
                jwtBearerOptions.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        string errorStatus = "";
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            errorStatus = "Token is Expired.";
                        }
                        else
                        {
                            errorStatus = "Invalid Token!";
                        }
                        Error err = new Error("Unauthorized Request", errorStatus);

                        var message = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(err));
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json;";
                        context.Response.Body.WriteAsync(message, 0, message.Length);
                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void ConfigureValidateModel(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            });
            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

    }
}
