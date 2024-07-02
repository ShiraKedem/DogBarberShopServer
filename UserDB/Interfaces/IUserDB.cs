using DogBarberShopDB.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Interfaces
{
    public interface IUserDB
    {
        void addUserName(User user);
        List<User> GetAllUsers();
        string GetUserNameById(int id);
    }
}
