using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopEntites
{
    public class QueueUpdateDTO
    {
        public DateTime Date { get; set; }

        public string Hour { get; set; } = null!;
    }
}
