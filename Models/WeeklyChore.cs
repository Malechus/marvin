using System;
using System.Collections.Generic;

namespace marvin.Models;

public partial class WeeklyChore
{
    public int Id { get; set; }

    public string ChoreName { get; set; } = null!;

    public string? WeekOne { get; set; }

    public string? WeekTwo { get; set; }

    public string? WeekThree { get; set; }

    public string? WeekFour { get; set; }

    public ulong Active { get; set; }

    public string? Notes { get; set; }
}
