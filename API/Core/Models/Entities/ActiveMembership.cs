using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class ActiveMembership
{
    public int ActiveMembershipId { get; set; }

    public int UserId { get; set; }

    public int? VisitsLeft { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual User User { get; set; } = null!;
}
