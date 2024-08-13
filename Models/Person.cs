using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marvin.Models
{
    public partial class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ChorePoints { get; set; }

        public int MealPoints { get; set; }

        public DateOnly Birthday { get; set; }
    }
}
