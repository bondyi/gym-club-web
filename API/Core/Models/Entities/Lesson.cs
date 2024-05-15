using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Lesson
{
    public int LessonId { get; set; }

    public string LessonName { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
