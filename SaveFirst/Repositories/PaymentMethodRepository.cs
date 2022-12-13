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
            string queryInsert = $"INSERT INTO PaymentMethod (id, saver_id, name, bank, limit, invoice_due_date, invoice_closing_date, registration_date) " +
                $"VALUES (@Id, @SaverId, @Name, @Bank, @Limit, @InvoiceDueDate, @InvoiceClosingDate, @RegistrationDate)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Bank", newRecord.Bank);
                    cmd.Parameters.AddWithValue("@Limit", newRecord.Limit);
                    cmd.Parameters.AddWithValue("@RegistrationDate", newRecord.RegistrationDate.ToString());
                    cmd.Parameters.AddWithValue("@InvoiceDueDate", newRecord.InvoiceDueDate);
                    cmd.Parameters.AddWithValue("@InvoiceClosingDate", newRecord.InvoiceClosingDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<PaymentMethod> FindAllFromSaver(string Id)
        {
            PaymentMethod record = null;
            List<PaymentMethod> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                string queryFind = $"SELECT * FROM CreditCard WHERE saver_id = '{Id}'";
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
                                Id = rdr["id"].ToString(),
                                SaverId = rdr["saver_id"].ToString(),
                                Name = rdr["name"].ToString(),
                                Bank = rdr["bank"].ToString(),
                                Limit = (float) rdr["limit"],
                                ExpirationDate = new DateTime(expDate[0], expDate[1], expDate[2]),
                                InvoiceDueDate = (int)rdr["invoice_due_date"],
                                InvoiceClosingDate = (int)rdr["invoice_closing_date"]
                            };
                            list.Add(record);

                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Not found");
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

        public float ExpensesFromPaymentMethod(string PaymentMethodId)
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