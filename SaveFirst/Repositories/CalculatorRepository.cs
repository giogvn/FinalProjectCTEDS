using System;
using SaveFirst.Models;
using SaveFirst.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Documents;

namespace SaveFirst.Repositories
{
    internal class CalculatorRepository
    {
        internal List<Expense> GetExpensesFromIncomeResource(int IncomeResourceId , string status = "active")
        {
            string queryFind = $"SELECT * FROM Expense WHERE id = (SELECT expense_id FROM " +
                $"ExpensePaymentMethod NATURAL JOIN PaymentMethodIncomeResource WHERE income_resource_id = {IncomeResourceId})" +
                $"AND status = {status};";

            return ExpenseRepository.FindAllFromSaver(queryFind);
        }

        public float MoneyLeftFromIncomeResource(int IncomeResourceId)
        {
            string queryFind = $"SELECT * FROM IncomeResource WHERE id = {IncomeResourceId}";
            List<Expense> expenses = this.GetExpensesFromIncomeResource(IncomeResourceId);
            float moneyLeft = IncomeResourceRepository.FindAllFromSaver(queryFind)[0].Value;

            foreach(Expense expense in expenses) 
            {
                moneyLeft -= expense.InstallmentValue;
            }

            return moneyLeft;
        }

        public DateOnly CalculateDueDate(DateOnly purchaseDate, int numberOfInstallments ,int invoiceDueDate)
        {
            DateOnly dueD = purchaseDate.AddMonths(numberOfInstallments);
            int dueMonth = dueD.Month;
            int dueYear = dueD.Year;
            return new DateOnly(dueYear, dueMonth, invoiceDueDate);
        }

        public int CalculateInstallmentsLeft(DateOnly dueDate, int invoiceDueDate)
        {
            DateTime td = DateTime.Today;
            DateOnly today = DateOnly.FromDateTime(td);
            DateOnly currDueDate = new DateOnly(today.Year, today.Month, invoiceDueDate);

            if (today > dueDate) { return 0; }

            return ((currDueDate.Year - dueDate.Year) * 12) + currDueDate.Month - dueDate.Month;
        }
    }
}
