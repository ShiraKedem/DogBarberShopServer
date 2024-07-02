using AutoMapper;
using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB
{
    public class MapperManager:Profile
    {
        public MapperManager()
        {
            CreateMap<UserAddUserNameDTO, User>();
            //.ForMember(user => user.UserName,
            //options => options.MapFrom(userLoginDTO => userLoginDTO.Name));
            //.ForMember(x => x.UserName, options => options.Ignore());
            CreateMap<QueueAddQueueDTO, Queue>();
            CreateMap<UserLoginDTO, User>();
            CreateMap<QueueUpdateDTO, Queue>();



        }
    }
}
