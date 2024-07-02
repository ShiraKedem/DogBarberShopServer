using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogBarberShopBL.Interfaces
{
    public interface IHomeBL
    {
        string GetUserName();
      
        string GetHeader();
        int GetUserId();

    }
}
