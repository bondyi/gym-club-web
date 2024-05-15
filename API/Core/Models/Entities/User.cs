using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserRole { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string HashPassword { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateOnly? BirthDate { get; set; }

    public bool? Gender { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenCreatedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ActiveMembership> ActiveMemberships { get; set; } = new List<ActiveMembership>();

    public virtual ICollection<AttendenceMark> AttendenceMarks { get; set; } = new List<AttendenceMark>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
