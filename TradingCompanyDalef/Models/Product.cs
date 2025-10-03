using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TradingCompanyDalef.Models;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(9, 2)")]
    public decimal Price { get; set; }

    public int Amount { get; set; }

    [InverseProperty("OrderDetailNavigation")]
    public virtual OrderDetail? OrderDetail { get; set; }
}
