using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductType { get; set; } = null!;

    public string ProductName { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<PurchaseProduct> PurchaseProducts { get; set; } = new List<PurchaseProduct>();
}
