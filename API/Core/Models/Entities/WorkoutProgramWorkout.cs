using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class WorkoutProgramWorkout
{
    public int WorkoutProgramId { get; set; }

    public int ExerciseWorkoutId { get; set; }

    public int Order { get; set; }

    public virtual ExerciseWorkout ExerciseWorkout { get; set; } = null!;

    public virtual WorkoutProgram WorkoutProgram { get; set; } = null!;
}
