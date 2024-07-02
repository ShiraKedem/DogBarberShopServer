using DogBarberShopDB.EF.Models;
using DogBarberShopEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBL.Interfaces
{
    public interface IQueueBL
    {
      
        void addQueue(QueueAddQueueDTO queueDTO);
        List<Queue> GetAllQueues();
        void UpdateQueue(int queueId, QueueUpdateDTO queueUpdateDTO);
        void DeleteQueue(int id);
        int? GetUserById(int queueId);

    }
}
