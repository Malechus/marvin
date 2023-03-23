using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marvin.Entities
{
    public sealed class Settings
    {
        public required string Token { get; set; }
        public required ulong AnnounceChannel { get; set; }
    }
}