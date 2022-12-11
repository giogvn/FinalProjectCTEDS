using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class PaymentMethod
    {

        public int Id { get; set; } 
        public int SaverId { get; set; }

        public string? Name { get; set; }

        public string? Bank { get; set; }

        public float Limit {get; set; }

        public DateOnly ExpirationDate { get; set; }

        public DateOnly InvoiceDueDate { get; set; }

        public DateOnly InvoiceClosingDate { get; set; }

        public DateOnly RegistrationDate { get; set; }

        public DateOnly CancelDate { get; set;}

    }
}
