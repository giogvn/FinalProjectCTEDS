using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Models;
using System.Data.SqlClient;


namespace SaveFirst.Builders
{
  public class ModelBuilder
  {
    private List<T> BuildExpenses(SqlDataReader rdr)
    {
      List<Expense> list = new();
      while (rdr.Read())
      {
        Expense record = new Expense()
        {
          Id = rdr["id"].ToString(),
          SaverId = rdr["saver_id"].ToString(),
          Date = Convert.ToDateTime(rdr["expense_date"].ToString()),
          Value = (double)rdr["value"],
          Type = rdr["expense_type"].ToString(),
          Description = rdr["description"].ToString(),
          Status = rdr["status"].ToString(),
          NumberOfInstallments = (int)rdr["number_of_installments"],
          InstallmentValue = (double)rdr["installment_value"],
          InstallmentsLeft = (int)rdr["installments_left"]
        };
        list.Add(record);
      }
      return list;
    }

    private List<Category> BuildCategories(SqlDataReader rdr)
    {
      List<Category> list= new();
      while (rdr.Read())
      {
        Category record = new()
        {
          Id = rdr["id"].ToString(),
          SaverId = rdr["saver_id"].ToString(),
          Name = rdr["name"].ToString()
        };
        list.Add(record);
      }
      return list;
    }

    private List<Saver> BuildSavers(SqlDataReader rdr)
    {
      List<Saver> list = new();

       while (rdr.Read())
      {
        record = new Saver()
        {
          Id = rdr["id"].ToString(),
          Type = rdr["user_type"].ToString(),
          PayerId = rdr["payer_id"].ToString(),
          Birthday = Convert.ToDateTime(rdr["birthdate"].ToString())
        };
        list.Add(record);
      }
      return list;
    }

    private List<PaymentMethod> BuildPaymentMethod(SqlDataReader rdr)
    {
      List<PaymentMethod> list = new();
      while (rdr.Read())
      {
        record = new PaymentMethod()
        {
          Id = rdr["id"].ToString(),
          SaverId = rdr["saver_id"].ToString(),
          Name = rdr["name"].ToString(),
          Bank = rdr["bank"].ToString(),
          Limit = (double)rdr["limit"]
        };
        if (typeof(DBNull) == rdr["invoice_due_date"].GetType())
        {
          record.InvoiceDueDate = null;
          record.InvoiceClosingDate = null;
        }
        else
        {
          record.InvoiceDueDate = (int)rdr["invoice_due_date"];
          record.InvoiceClosingDate = (int)rdr["invoice_closing_date"];
        }
        list.Add(record);
      }
      return list;
    }

    public List<IntermediateModel> BuildIntermediateModel()

    public List<T> Build(SqlDataReader rdr, string modelName, string[] labels = null) 
    {
      switch (modelName)
      {
        case "expense":
          return BuildExpenses(rdr);

        case "category":
          return BuildCategories(rdr);
        
        case "saver":
          return BuildSavers(rdr);

        case "paymentMethod":
          return BuildPaymentMethod(rdr);
        
        case "intermediate":
          return BuildIntermediateModel(rdr, labels);
      }
    }
  }
}
