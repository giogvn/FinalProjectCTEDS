using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Models
{
    public class ExpenseCategory: IntermediateModel
    {
        public static string[] Labels = { "expense_id", "category_id" };
        //Add a string array conatining the name that the ForeignKeys take in the database
        //Possibly -> string[] Labels = { "name1" , "name2" }
    }
    public class ExpensePaymentMethod: IntermediateModel
    {
        public static string[] Labels = { "expense_id ", "payment_method_id" };
    }
    public class PaymentMethodIncomeResource: IntermediateModel
    {
        public static string[] Labels = { "payment_method_id", "income_resource_id" };
    }
}
