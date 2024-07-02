using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;

namespace DogBarberShopBL.Interfaces
{
    public interface IUserBL
    {
        void addUserName(UserAddUserNameDTO userDTO);
     
         List<User> GetAllUsers();
   string GetUserNameById(int id);
  
}
    }