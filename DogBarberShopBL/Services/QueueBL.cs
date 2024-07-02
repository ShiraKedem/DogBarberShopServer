using AutoMapper;
using DogBarberShopBL.Interfaces;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using DogBarberShopDB.Services;
using DogBarberShopEntites;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace DogBarberShopBL.Services
{
    public class QueueBL : IQueueBL
    {
        private readonly IUserDB _userDB;
        private readonly IQueueDB _QueueDB;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueueBL(IQueueDB queueDB, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserDB userDB)
        {
            _QueueDB = queueDB;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userDB = userDB;
        }

        public int GetUserFromToken()
        {
            byte[] byteArray;
            if (_httpContextAccessor.HttpContext.Session.TryGetValue("userId", out byteArray))
            {
                if (byteArray != null && byteArray.Length == sizeof(int))
                {
                    return BitConverter.ToInt32(byteArray, 0);
                }
            }

            throw new Exception("User ID not found in session");
        }

        public void addQueue(QueueAddQueueDTO queueDTO)
        {
            Queue queue = _mapper.Map<Queue>(queueDTO);
            queue.UserId = GetUserFromToken();
            queue.ProductionDate = DateTime.Now;
            _QueueDB.addQueue(queue);
        }
        
        public List<Queue> GetAllQueues()
        {
            return _QueueDB.GetAllQueues();
        }

        public void UpdateQueue(int queueId, QueueUpdateDTO queueUpdateDTO)
        {
            Queue queueFromDb = _QueueDB.GetQueueById(queueId);

            if (queueFromDb == null )
            {
                throw new Exception("Queue not found");
            }

           if(queueUpdateDTO.Date!=null)
            queueFromDb.Date = queueUpdateDTO.Date;
          if (queueUpdateDTO.Hour!=null)
            queueFromDb.Hour = queueUpdateDTO.Hour;

            _QueueDB.UpdateQueue(queueFromDb);
        }

        public void DeleteQueue(int id)
        {
            _QueueDB.DeleteQueue(id);

        }
        public int? GetUserById(int queueId)
        {
            Queue queue = _QueueDB.GetQueueById(queueId);
            if (queue == null)
            {
                throw new Exception("Queue not found");
            }

            return queue.UserId;
        }

    }
}
