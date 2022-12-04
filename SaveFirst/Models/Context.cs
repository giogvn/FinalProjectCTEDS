using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
