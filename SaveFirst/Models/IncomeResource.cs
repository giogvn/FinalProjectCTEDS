using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class IncomeResource
    {
        public int Id { get; set; } 
        
        public string? Name { get; set; }

        public int SaverId { get; set; }

        public int PayDay { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public float Value { get; set; }
        public string? Recurrence { get; set; }

    }
}
