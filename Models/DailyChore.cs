using System;
using System.Collections.Generic;

namespace marvin.Models;

public partial class DailyChore
{
    public int Id { get; set; }

    public string ChoreName { get; set; } = null!;

    public string Monday { get; set; } = null!;

    public string Tuesday { get; set; } = null!;

    public string Wednesday { get; set; } = null!;

    public string Thursday { get; set; } = null!;

    public string Friday { get; set; } = null!;

    public string Saturday { get; set; } = null!;

    public string Sunday { get; set; } = null!;

    public string? Notes { get; set; }

    public ulong Active { get; set; }
}
