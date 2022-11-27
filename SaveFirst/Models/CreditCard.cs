using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class CreditCard
    {

        public int Id { get; set; } 
        public int SaverId { get; set; }

        public string? Name { get; set; }

        public string? Bank { get; set; }

        public DateOnly ExpirationDate { get; set; }

        public DateOnly InvoiceDueDate { get; set; }

        public DateOnly InvoiceClosingDate { get; set; }

    }
}
