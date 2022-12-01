﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class Saver
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public int PayerId { get; set; }
        public string Name { get; set; } = String.Empty;    
        public DateOnly Birthday { get; set; }
        public string Password { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;


    }
}