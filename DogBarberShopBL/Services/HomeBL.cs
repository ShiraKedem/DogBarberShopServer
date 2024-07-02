using AutoMapper;
using Azure.Core;
using DogBarberShopBL.Interfaces;
using DogBarberShopDB.Interfaces;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogBarberShopEntites;

namespace DogBarberShopBL.Services
{
    public class HomeBL:IHomeBL
    {
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeBL( IOptions<AppSettings> options, IHttpContextAccessor httpContextAccessor)
        {

            _appSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
        }
        public int GetUserId()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            string userId = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userId, out int id))
            {
                throw new Exception("Invalid user ID in token");
            }

            return id;
        }
        public string GetUserName()
        {
            var token = _httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadJwtToken(token);
            string userName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return userName;
        }

        public string GetHeader() { 
        var token =_httpContextAccessor.HttpContext.Request.Cookies[CookiesKeys.AccessToken];
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadJwtToken(token);
        string userName = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
            return userName;
    }
    
    }
}
