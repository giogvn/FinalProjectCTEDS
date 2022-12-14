using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class PaymentMethod
    {

        public string? Id { get; set; } 
        public string? SaverId { get; set; }

        public string? Name { get; set; }

        public string? Bank { get; set; }

        public double Limit {get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? InvoiceDueDate { get; set; }

        public int? InvoiceClosingDate { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public DateTime? CancelDate { get; set;}

    }
}
