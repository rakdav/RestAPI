using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Inscompany
{
    public int Id { get; set; }

    public string Address { get; set; } = null!;

    public string Inn { get; set; } = null!;

    public string Rs { get; set; } = null!;

    public string Bik { get; set; } = null!;

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
