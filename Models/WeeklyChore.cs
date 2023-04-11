using System;
using System.Collections.Generic;

namespace marvin.Models;

public partial class WeeklyChore
{
    public int Id { get; set; }

    public string ChoreName { get; set; } = null!;

    public string ChoreDay { get; set; } = null!;

    public string? Responsibility { get; set; }

    public ulong Active { get; set; }

    public string? Notes { get; set; }
}
