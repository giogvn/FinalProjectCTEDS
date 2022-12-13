using System;
using DatabasePopulator.Models;
using DatabasePopulator.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;

namespace DatabasePopulator.Repositories
{
    internal class CalculatorRepository
    {
        public DateTime CalculateDueDate(DateTime purchaseDate, int numberOfInstallments ,int invoiceDueDate)
        {
            DateTime dueD = purchaseDate.AddMonths(numberOfInstallments);
            Console.WriteLine(dueD.ToString());
            int dueMonth = dueD.Month;
            int dueYear = dueD.Year;
            return new DateTime(dueYear, dueMonth, invoiceDueDate);
        }


        public int CalculateInstallmentsLeft(DateTime dueDate, int invoiceDueDate)
        {

            DateTime today = DateTime.Today;
            DateTime currDueDate = new DateTime(today.Year, today.Month, invoiceDueDate);

            if (today > dueDate) { return 0; }

            return (((currDueDate.Year - dueDate.Year) * 12) + currDueDate.Month - dueDate.Month) *-1;
        }

        public float CalculateInstallmentValue(int numberOfInstallments, float expenseValue)
        {
            return expenseValue / numberOfInstallments;
        }


    }
}
