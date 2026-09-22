using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Orderservice
{
    public int Id { get; set; }

    public int Idorder { get; set; }

    public int Idservice { get; set; }

    public string Status { get; set; } = null!;

    public virtual Order IdorderNavigation { get; set; } = null!;

    public virtual Service IdserviceNavigation { get; set; } = null!;
}
