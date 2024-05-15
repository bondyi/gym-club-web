using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class AttendenceMark
{
    public int AttendenceMarkId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
