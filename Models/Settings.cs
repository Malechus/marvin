using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marvin.Models
{
    public sealed class Settings
    {
        public required string Token { get; set; }
        public required ulong AnnounceChannel { get; set; }
        public required ulong DailyChoresChannel { get; set; }
        public required ulong WeeklyChoresChannel { get; set; }
        public required ulong AdHocChoresChannel { get; set; }
        public required string NewsKey { get; set; }
        public required ulong ServerID { get; set; }
        public required string HouseholdConnection { get; set; }
    }
}