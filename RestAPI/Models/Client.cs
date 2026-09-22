using System;
using System.Collections.Generic;

namespace RestAPI.Models;

public partial class Client
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public string? Middlename { get; set; }

    public DateOnly Birthday { get; set; }

    public string Passport { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Number { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int Company { get; set; }

    public string Photo { get; set; } = null!;

    public virtual Inscompany CompanyNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
