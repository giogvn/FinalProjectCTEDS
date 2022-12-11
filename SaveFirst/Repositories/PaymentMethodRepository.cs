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
        static string ConnectionString = "Server = DESKTOP-AIPLP16; Initial Catalog = SaveFirst;integrated security=true;";
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
            string queryInsert = $"INSERT INTO PaymentMethod (saver_id, name, bank, expiration_date, invoice_due_date, invoice_closing_date, registration_date, cancel_date) " +
                $"VALUES (@SaverId, @Name, @Bank, @ExpirationDate, @InvoiceDueDate, @InvoiceClosingDate, @RegistrationDate, @CancelDate)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Bank", newRecord.Bank);
                    cmd.Parameters.AddWithValue("@ExpirationDate", newRecord.ExpirationDate);
                    cmd.Parameters.AddWithValue("@InvoiceDueDate", newRecord.InvoiceDueDate);
                    cmd.Parameters.AddWithValue("@InvoiceClosingDate", newRecord.InvoiceClosingDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static List<PaymentMethod> FindAllFromSaver(string queryFind)
        {
            PaymentMethod record = null;
            List<PaymentMethod> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                //string queryFind = $"SELECT * FROM CreditCard WHERE saver_id = '{Id}'";
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    con.Open();
                    try
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] expD = rdr["expiration_date"].ToString().Split("-");
                            int[] expDate = { int.Parse(expD[0]), int.Parse(expD[1]), int.Parse(expD[2]) };


                            string[] invDueD = rdr["invoice_due_date"].ToString().Split("-");
                            int[] invDate = { int.Parse(invDueD[0]), int.Parse(invDueD[1]), int.Parse(invDueD[2]) };

                            string[] invClosingD = rdr["invoice_closing_date"].ToString().Split("-");
                            int[] invClosingDate = { int.Parse(invClosingD[0]), int.Parse(invClosingD[1]), int.Parse(invClosingD[2]) };


                            record = new PaymentMethod()
                            {
                                Id = (int)rdr["id"],
                                SaverId = (int)rdr["saver_id"],
                                Name = rdr["name"].ToString(),
                                Bank = rdr["bank"].ToString(),
                                ExpirationDate = new DateOnly(expDate[0], expDate[1], expDate[2]),
                                InvoiceDueDate = new DateOnly(invDate[0], invDate[1], invDate[2]),
                                InvoiceClosingDate = new DateOnly(invClosingDate[0], invClosingDate[1], invClosingDate[2])
                            };
                            list.Add(record);

                        }
                    }
                    catch (Microsoft.Data.Sqlite.SqliteException)
                    {
                        Console.WriteLine("Not found");
                    }
                }
            }
            return list;
        }

        public List<PaymentMethod> ReadAll(string query) => throw new NotImplementedException();

        public void Update(PaymentMethod record)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE CreditCard SET name = @Name , bank = @Bank, expiration_date = @ExpirationDate, invoice_due_date = @InvoiceDueDate, invoice_closing_date = @InvoiceClosingDate, WHERE id = @Id";

                using (SqlCommand cmd = new(queryUpdateBody, con))
                {

                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Bank", record.Bank);
                    cmd.Parameters.AddWithValue("@InvoiceDueDate", record.InvoiceDueDate);
                    cmd.Parameters.AddWithValue("@InvoiceClosingDate", record.InvoiceClosingDate);
                    cmd.Parameters.AddWithValue("@ExpirationDate", record.ExpirationDate);


                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public float ExpensesFromPaymentMethod(int PaymentMethodId)
        {
            List<Expense> expenses = ExpenseRepository.GetExpensesFromPaymentMethod(PaymentMethodId);

            float total = 0;
            foreach(Expense expense in expenses)
            {
                total += expense.InstallmentValue;
            }
            return total;
        }
    } 
}