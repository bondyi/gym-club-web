using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class PurchaseProduct
{
    public int PurchaseId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Purchase Purchase { get; set; } = null!;
}
