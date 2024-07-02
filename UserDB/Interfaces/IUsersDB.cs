using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Interfaces
{
    public interface IUsersDB
    {
         User Login(User userLogin);

    }
}
