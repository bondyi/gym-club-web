using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string ExerciseName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<ExerciseWorkout> ExerciseWorkouts { get; set; } = new List<ExerciseWorkout>();
}
