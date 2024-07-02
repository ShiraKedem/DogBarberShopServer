using DogBarberShopBL.Interfaces;
using DogBarberShopDB.Interfaces;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Services;
using DogBarberShopEntites;
using AutoMapper;

namespace DogBarberShopBL.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserDB _userDB;
        private readonly IMapper _mapper;
        public UserBL(IUserDB userDB,IMapper mapper)
        {
            _userDB = userDB;
            _mapper = mapper;
        }

        public void addUserName(UserAddUserNameDTO userDTO)

        {
            User userMapped = _mapper.Map<User>(userDTO);
            _userDB.addUserName(userMapped);
          
        }
  

    public List<User> GetAllUsers()
    {
        return _userDB.GetAllUsers();
    }

        public string GetUserNameById(int id)
        {
            return _userDB.GetUserNameById(id);

        }
    }
  }