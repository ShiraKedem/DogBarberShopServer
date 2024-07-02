using DogBarberShopDB.EF.Contexts;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using DogBarberShopEntites;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Services
{
    public class UsersDB:IUsersDB

    {
        private readonly ILogger<UsersDB> _logger;

        private readonly DogBarberShopContext _dogBarberShopContext;
        public UsersDB(DogBarberShopContext dogBarberShopContext, ILogger<UsersDB> logger)
        {
            _logger = logger;
            _dogBarberShopContext = dogBarberShopContext;

        }

        public User Login(User userLogin)
        {
         return
                _dogBarberShopContext
                .Users
                .FirstOrDefault(u => u.UserName == userLogin.UserName && u.Password == userLogin.Password);


    }
}
    }

