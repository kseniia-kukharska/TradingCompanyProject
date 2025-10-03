using System;
using System.Collections.Generic;

namespace TradingCompanyDal;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product OrderDetailNavigation { get; set; } = null!;
}
