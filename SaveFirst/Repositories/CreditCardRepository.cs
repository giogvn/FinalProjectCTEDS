using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SaveFirst.Repositories
{
    public class CreditCardRepository : IRecord<CreditCard>
    {
        static string ConnectionString = "Data source = CreditCard.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM CreditCard WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(CreditCard newRecord)
        {
            string queryInsert = $"INSERT INTO CreditCard (saver_id, name, bank, expiration_date, invoice_due_date, invoice_closing_date) " +
                $"VALUES (@SaverId, @Name, @Bank, @ExpirationDate, @InvoiceDueDate, @InvoiceClosingDate)";

            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
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

        static List<CreditCard> FindAllFromSaver(string queryFind)
        {
            CreditCard record = null;
            List<CreditCard> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"SELECT * FROM CreditCard WHERE saver_id = '{Id}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();
                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] expD = rdr["expiration_date"].ToString().Split("-");
                            int[] expDate = { int.Parse(expD[0]), int.Parse(expD[1]), int.Parse(expD[2]) };


                            string[] invDueD = rdr["invoice_due_date"].ToString().Split("-");
                            int[] invDate = { int.Parse(invDueD[0]), int.Parse(invDueD[1]), int.Parse(invDueD[2]) };

                            string[] invClosingD = rdr["invoice_closing_date"].ToString().Split("-");
                            int[] invClosingDate = { int.Parse(invClosingD[0]), int.Parse(invClosingD[1]), int.Parse(invClosingD[2]) };


                            record = new CreditCard()
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

        public List<CreditCard> ReadAll(string query) => throw new NotImplementedException();

        public void Update(CreditCard record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE CreditCard SET name = @Name , bank = @Bank, expiration_date = @ExpirationDate, invoice_due_date = @InvoiceDueDate, invoice_closing_date = @InvoiceClosingDate, WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
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
    } 
}