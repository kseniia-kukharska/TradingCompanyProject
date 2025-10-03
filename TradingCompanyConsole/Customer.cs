using System;
using System.Collections.Generic;

namespace TradingCompanyDal;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
