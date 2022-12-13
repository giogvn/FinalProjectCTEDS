using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabasePopulator.Models
{
    public class Expense
    {
        public string? Id { get; set; }

        public string? SaverId { get; set; }

        public DateTime Date { get; set; }

        public  DateTime DueDate { get; set; }

        public float Value { get; set; }

        public string? Type { get; set; } //recorrente - esporádico

        public string? Description { get; set; }

        public string? Status { get; set; } // active 

        public int NumberOfInstallments { get; set; }

        public float InstallmentValue { get; set; }

        public int InstallmentsLeft { get; set; }

    }
}
