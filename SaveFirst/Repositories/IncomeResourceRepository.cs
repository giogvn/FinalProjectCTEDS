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
    public class IncomeResourceRepository : IRecord<IncomeResource>
    { 
        static string ConnectionString = "Data source = IncomeResource.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM IncomeResource WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(IncomeResource newRecord)
        {
            string queryInsert = $"INSERT INTO IncomeResource (saver_id, name, value, payday, start_date, end_date, recurrence) " +
                $"VALUES (@SaverId, @Name, @Value, @Payday, @StartDate, @EndDate, @Recurrence)";

            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Value", newRecord.Value);
                    cmd.Parameters.AddWithValue("@Payday", newRecord.PayDay);
                    cmd.Parameters.AddWithValue("@StartDate", newRecord.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", newRecord.EndDate);
                    cmd.Parameters.AddWithValue("@Recurrence", newRecord.Recurrence);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static public List<IncomeResource> FindAllFromSaver(string queryFind)
        {
            IncomeResource record = null;
            List<IncomeResource> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"SELECT * FROM IncomeResource WHERE saver_id = '{Id}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();
                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] startD = rdr["start_date"].ToString().Split("-");
                            int[] startDate = { int.Parse(startD[0]), int.Parse(startD[1]), int.Parse(startD[2]) };


                            string[] endD = rdr["end_date"].ToString().Split("-");
                            int[] endDate = { int.Parse(endD[0]), int.Parse(endD[1]), int.Parse(endD[2]) };
                      
                            record = new IncomeResource()
                            {
                                Id = (int)rdr["id"],
                                SaverId = (int)rdr["saver_id"],
                                Name = rdr["name"].ToString(),
                                Value = (float)rdr["value"],
                                PayDay = (int)rdr["payday"],
                                StartDate = new DateOnly(startDate[0], startDate[1], startDate[2]),
                                EndDate = new DateOnly(endDate[0], endDate[1], endDate[2])                               
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

        public List<IncomeResource> ReadAll(string querySelect) => throw new NotImplementedException();

        public void Update(IncomeResource record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE IncomeResource SET name = @Name , value = @Value, payday = @Payday, recurrence = @Recurrence, WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
                {

                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Value", record.Value);
                    cmd.Parameters.AddWithValue("@Payday", record.PayDay);
                    cmd.Parameters.AddWithValue("@Recurrence", record.Recurrence);        

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public float MoneyLeftFromIncomeResource(int IncomeResourceId)
        {
            string queryFind = $"SELECT * FROM IncomeResource WHERE id = {IncomeResourceId}";
            List<Expense> expenses = ExpenseRepository.GetExpensesFromIncomeResource(IncomeResourceId);
            float moneyLeft = FindAllFromSaver(queryFind)[0].Value;

            foreach (Expense expense in expenses)
            {
                moneyLeft -= expense.InstallmentValue;
            }

            return moneyLeft;
        }
    } 
}