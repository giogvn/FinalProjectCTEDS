using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SaveFirst.Repositories
{
    public class PaymentMethodRepository : IRecord<PaymentMethod>
    {
        static string ConnectionString = "Server=labsoft.pcs.usp.br; Initial Catalog=db_7; User id=''; pwd='';";
        public void Delete(int RecordId)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM PaymentMethod WHERE id = @Id";

                using (SqlCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(PaymentMethod newRecord)
        {
            string queryInsert = $"INSERT INTO PaymentMethod (id, saver_id, name, bank, limit, invoice_due_date, invoice_closing_date) " +
                $"VALUES (@Id, @SaverId, @Name, @Bank, @Limit, @InvoiceDueDate, @InvoiceClosingDate)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Bank", newRecord.Bank);
                    cmd.Parameters.AddWithValue("@Limit", newRecord.Limit);
                    if (newRecord.InvoiceDueDate != null)
                    {
                        cmd.Parameters.AddWithValue("@InvoiceDueDate", newRecord.InvoiceDueDate);
                        cmd.Parameters.AddWithValue("@InvoiceClosingDate", newRecord.InvoiceClosingDate);
                    }

                    else
                    {
                        cmd.Parameters.AddWithValue("@InvoiceDueDate", DBNull.Value);
                        cmd.Parameters.AddWithValue("@InvoiceClosingDate", DBNull.Value);
                    }

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<PaymentMethod> FindAllFromSaver(string saverId)
        {
            PaymentMethod record = null;
            List<PaymentMethod> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                string queryFind = $"SELECT * FROM PaymentMethod WHERE saver_id = @SaverId";
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", saverId);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
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


                }
            }
            return list;
        }

        public List<PaymentMethod> ReadAll() => throw new NotImplementedException();

        public void Update(PaymentMethod record)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE CreditCard SET name = @Name , bank = @Bank, expiration_date = CONVERT(datetime,@ExpirationDate,103), invoice_due_date = @InvoiceDueDate, invoice_closing_date = @InvoiceClosingDate, WHERE id = @Id";

                using (SqlCommand cmd = new(queryUpdateBody, con))
                {
                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Bank", record.Bank);
                    cmd.Parameters.AddWithValue("@InvoiceDueDate", record.InvoiceDueDate.ToString());
                    cmd.Parameters.AddWithValue("@InvoiceClosingDate", record.InvoiceClosingDate);
                    cmd.Parameters.AddWithValue("@ExpirationDate", record.ExpirationDate);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public double ExpensesFromPaymentMethod(string PaymentMethodId)
        {
            List<Expense> expenses = ExpenseRepository.GetExpensesFromPaymentMethod(PaymentMethodId);

            double total = 0;
            foreach(Expense expense in expenses)
            {
                total += expense.InstallmentValue;
            }
            return total;
        }
    } 
}
