using System;
using System.Collections.Generic;

namespace DogBarberShopDB.EF.Models;

public partial class Queue
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public DateTime ProductionDate { get; set; }

    public DateTime Date { get; set; }

    public string Hour { get; set; } = null!;

    public virtual User? User { get; set; }
}
