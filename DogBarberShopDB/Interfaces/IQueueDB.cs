using DogBarberShopDB.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Interfaces
{
    public interface IQueueDB
    {
        void addQueue(Queue queue);
        List<Queue> GetAllQueues();
        //void UpdateQueue(Queue queue);
         void UpdateQueue(Queue queue);
         Queue GetQueueById(int queueId);
        void DeleteQueue(int id);


    }

}
