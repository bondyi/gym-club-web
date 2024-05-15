using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class UserProgress
{
    public int UserProgressId { get; set; }

    public int UserId { get; set; }

    public int ExerciseWorkoutId { get; set; }

    public DateTime CompletionTime { get; set; }

    public virtual ExerciseWorkout ExerciseWorkout { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
