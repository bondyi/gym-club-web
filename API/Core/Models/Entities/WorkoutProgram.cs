using System;
using System.Collections.Generic;

namespace API.Core.Models.Entities;

public partial class WorkoutProgram
{
    public int WorkoutProgramId { get; set; }

    public string WorkoutProgramType { get; set; } = null!;

    public string WorkoutProgramName { get; set; } = null!;

    public bool Location { get; set; }

    public bool Gender { get; set; }

    public string TrainingMethod { get; set; } = null!;

    public int WorkoutsPerWeek { get; set; }

    public int Difficulty { get; set; }

    public int DurationInWeeks { get; set; }

    public string? Description { get; set; }
}
