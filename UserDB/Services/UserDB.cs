using DogBarberShopDB.EF.Contexts;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Services
{
    public class UserDB:IUserDB
    {
        private readonly DogBarberShopContext _dogBarberShopContext;
        public UserDB(DogBarberShopContext dogBarberShopContext)
        {
            _dogBarberShopContext = dogBarberShopContext;
        }

        public void addUserName(User user)
        {
            try {
            _dogBarberShopContext.Add(user);
            _dogBarberShopContext.SaveChanges(); 
           }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while saving the entity changes.");
                Console.WriteLine(ex.InnerException?.Message);
            }
        }

        public List<User> GetAllUsers()
        {
            return _dogBarberShopContext
                .Users
                .ToList();
        }
        public string GetUserNameById(int id)
        {
            var user = _dogBarberShopContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return user.UserName;
            }
            else
            {
                return "לא נמצא שם משתמש"; // או תוכל להחזיר הודעה אחרת בהתאם לצורך שלך
            }
        }


    }
}
