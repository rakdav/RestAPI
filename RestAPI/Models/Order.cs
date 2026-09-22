using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Order
{
    public int Id { get; set; }

    public DateOnly Datestart { get; set; }

    public string Status { get; set; } = null!;

    public int Period { get; set; }

    public string Login { get; set; } = null!;

    public virtual Client LoginNavigation { get; set; } = null!;

    public virtual ICollection<Orderservice> Orderservices { get; set; } = new List<Orderservice>();
}
