using DogBarberShopDB.EF.Contexts;
using DogBarberShopDB.EF.Models;
using DogBarberShopDB.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopDB.Services
{
    public class QueueDB:IQueueDB
    {
        private readonly ILogger<QueueDB> _logger;   

        private readonly DogBarberShopContext _dogBarberShopContext;
        public QueueDB(DogBarberShopContext dogBarberShopContext,ILogger<QueueDB>logger)
        {
            _logger = logger;
            _dogBarberShopContext = dogBarberShopContext;

        }

        public void addQueue(Queue queue)
        {
                _dogBarberShopContext.Add(queue);
                _dogBarberShopContext.SaveChanges();
            
        }

        public List<Queue> GetAllQueues() {
        return _dogBarberShopContext.Queues.ToList();
        }


      
        public Queue GetQueueById(int queueId)
        {
            return _dogBarberShopContext.Queues.SingleOrDefault(q => q.Id == queueId);
        }

        public void UpdateQueue(Queue queue)
        {
            _dogBarberShopContext.Queues.Update(queue);
            _dogBarberShopContext.SaveChanges();
        }
        public void DeleteQueue(int id)
        {
            Queue queueFromDb = _dogBarberShopContext
                    .Queues
                    .FirstOrDefault(x => x.Id == id);
            _dogBarberShopContext.Queues.Remove(queueFromDb);
            _dogBarberShopContext.SaveChanges();

        }
    }
}
