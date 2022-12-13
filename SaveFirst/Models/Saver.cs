using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class Saver
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? PayerId { get; set; }
        public string Name { get; set; } = String.Empty;    
        public DateTime Birthday { get; set; }
        public string Password { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;


    }
}
