using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class Category
    {
        public int Id { get; set; }
        public int SaverId { get; set; }
        public string? Name { get; set; }
    }
}
