using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Review
{
    public int ReviewId { get; set; }

    public int UserId { get; set; }

    public string ReviewType { get; set; } = null!;

    public int TargetId { get; set; }

    public int RatingValue { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
