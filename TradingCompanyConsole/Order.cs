using System;
using System.Collections.Generic;

namespace TradingCompanyDal;

public partial class Order
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int StatusId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateOnly OrderDate { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Status Status { get; set; } = null!;
}
