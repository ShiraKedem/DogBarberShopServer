using AutoMapper;
using DogBarberShopBL.Interfaces;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

    public class UsersBL:IUsersBL
    {
        private readonly IUsersDB _usersDB;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppSettings _appSettings;

        public UsersBL(IUsersDB usersDB, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> options)
        {
            _usersDB = usersDB;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _appSettings=options.Value;
        }
        public bool Login(UserLoginDTO userLoginDTO)
        {
            User userMapped = _mapper.Map<User>(userLoginDTO);
            User userFromDb = _usersDB.Login(userMapped);
            if (userFromDb is not null)
            {
                CreateUserToken(userFromDb.UserName, userFromDb.Id); // הוסף את ה-ID של המשתמש
                byte[] byteIseName = Encoding.ASCII.GetBytes(userFromDb.UserName);
                _httpContextAccessor.HttpContext.Session.Set("username", byteIseName);
                byte[] ByteId = BitConverter.GetBytes(userFromDb.Id); 
                _httpContextAccessor.HttpContext.Session.Set("userId", ByteId); 
                return true;
            }
            return false;
        }


        private void CreateUserToken(string userName, int id)
        {
            string newAccessToken = this.GenerateAccessToken(userName,id);
            CookieOptions cookieTokenOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes)

               
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append(CookiesKeys.AccessToken, newAccessToken, cookieTokenOptions);
        }


        private string GenerateAccessToken(string userName,  int id)
        {
            var jwtSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey));
            var credentials = new SigningCredentials(jwtSecurityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, userName),
        new Claim(ClaimTypes.NameIdentifier, id.ToString()) 
    };
            var token = new JwtSecurityToken(
                _appSettings.Jwt.Issuer,
                _appSettings.Jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(_appSettings.Jwt.ExpireMinutes),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
