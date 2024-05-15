using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class ExerciseWorkout
{
    public int ExerciseWorkoutId { get; set; }

    public int WorkoutId { get; set; }

    public int ExerciseId { get; set; }

    public int Order { get; set; }

    public string? SupersetMark { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual ICollection<UserProgress> UserProgresses { get; set; } = new List<UserProgress>();

    public virtual Workout Workout { get; set; } = null!;
}
