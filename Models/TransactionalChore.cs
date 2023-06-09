using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marvin.Models
{
    public class TransactionalChore
    {
        public int Id { get; set; }

        public DateTime? WeekOf { get; set; }

        public int? ChoreId { get; set; }

        public bool? Completed { get; set; }

        public DateTime? CompletedDateTime { get; set; }
    }
}