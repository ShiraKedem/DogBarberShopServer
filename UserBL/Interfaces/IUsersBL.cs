using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBL.Interfaces
{
    public interface IUsersBL
    {
         bool Login(UserLoginDTO userLoginDTO);
    }
}
