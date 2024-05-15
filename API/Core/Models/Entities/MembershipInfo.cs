using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class MembershipInfo
{
    public int ProductId { get; set; }

    public string MembershipType { get; set; } = null!;

    public int? Duration { get; set; }

    public int? VisitCount { get; set; }

    public virtual Product Product { get; set; } = null!;
}
