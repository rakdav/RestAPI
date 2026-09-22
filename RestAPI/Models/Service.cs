using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Service
{
    public string Name { get; set; } = null!;

    public double Cost { get; set; }

    public int Code { get; set; }

    public DateOnly Deadline { get; set; }

    public double Average { get; set; }

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
