using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class Workout
{
    public int WorkoutId { get; set; }

    public virtual ICollection<ExerciseWorkout> ExerciseWorkouts { get; set; } = new List<ExerciseWorkout>();
}
