using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class Expense
    {
        public int Id { get; set; }

        public int SaverId { get; set; }

        public DateOnly Date { get; set; }

        public  DateOnly DueDate { get; set; }

        public float Value { get; set; }

        public string? Type { get; set; } //recorrente - esporádico

        public string? Description { get; set; }

        public string? Status { get; set; } // active 

        public int NumberOfInstallments { get; set; }

        public float InstallmentValue { get; set; }

        public int InstallmentsLeft { get; set; }

    }
}
