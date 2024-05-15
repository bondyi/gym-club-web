using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int UserId { get; set; }

    public DateTime PurchaseTime { get; set; }

    public decimal Amount { get; set; }

    public virtual PurchaseProduct? PurchaseProduct { get; set; }

    public virtual User User { get; set; } = null!;
}
