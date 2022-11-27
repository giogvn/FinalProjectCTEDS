using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Repositories
{
    public class CheckingAccountRepository : IRecord<CheckingAccount>
    {
        private string ConnectionString = "Data source = CheckingAccount.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM CheckingAccount WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(CheckingAccount newRecord)
        {
            string queryInsert = $"INSERT INTO CheckingAccount (saver_id, name, bank, registration_date, cancel_date) " +
                $"VALUES (@SaverId, @Name, @Bank, @RegistrationDate, @CancelDate)";

            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Bank", newRecord.Bank);
                    cmd.Parameters.AddWithValue("@RegistrationDate", newRecord.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CancelDate", newRecord.CancelDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<CheckingAccount> FindAllFromSaver(int Id)
        {
            CheckingAccount record = null;
            List<CheckingAccount> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryFind = $"SELECT * FROM CheckingAccount WHERE saver_id = '{Id}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();
                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] regD = rdr["registration_date"].ToString().Split("-");
                            int[] regDate = { int.Parse(regD[0]), int.Parse(regD[1]), int.Parse(regD[2]) };


                            string[] cancelD = rdr["invoice_due_date"].ToString().Split("-");
                            int[] cancelDate = { int.Parse(cancelD[0]), int.Parse(cancelD[1]), int.Parse(cancelD[2]) };

                     
                            record = new CheckingAccount()
                            {
                                Id = (int)rdr["id"],
                                SaverId = (int)rdr["saver_id"],
                                Name = rdr["name"].ToString(),
                                Bank = rdr["bank"].ToString(),
                                RegistrationDate = new DateOnly(regDate[0], regDate[1], regDate[2]),
                                CancelDate = new DateOnly(cancelDate[0], cancelDate[1], cancelDate[2]),
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

        public List<CheckingAccount> ReadAll() => throw new NotImplementedException();

        public void Update(CheckingAccount record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE CheckingAccount SET name = @Name , bank = @Bank, registration_date = @RegistrationDate, cancel_date = @CancelDate WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
                {

                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Bank", record.Bank);
                    cmd.Parameters.AddWithValue("@RegistrationDate", record.RegistrationDate);
                    cmd.Parameters.AddWithValue("@CancelDate", record.CancelDate);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}