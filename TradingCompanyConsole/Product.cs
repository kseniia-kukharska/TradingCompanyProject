using System;
using System.Collections.Generic;

namespace TradingCompanyDal;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Amount { get; set; }

    public virtual OrderDetail? OrderDetail { get; set; }
}
