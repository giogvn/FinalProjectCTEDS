using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Repositories
{
    public class ExpenseRepository : IRecord<Expense>
    {
        private string ConnectionString = "Data source = Expense.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Expense WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(Expense newRecord)
        {
            string queryInsert = $"INSERT INTO Expense (saver_id, expense_date, due_date, value, expense_type, description, status," +
                $"number_of_installments, installments_value, installments_left) VALUES (@SaverId, @Date, @DueDate, @Value, @Type, " +
                $"@Description, @Status, @NumberOfInstallments, @InstallmentsValue, @InstallmentsLeft)";

            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Date", newRecord.Date);
                    cmd.Parameters.AddWithValue("@DueDate", newRecord.Date);
                    cmd.Parameters.AddWithValue("@Value", newRecord.Value);
                    cmd.Parameters.AddWithValue("@Type", newRecord.Type);
                    cmd.Parameters.AddWithValue("@Description", newRecord.Description);
                    cmd.Parameters.AddWithValue("@Status", newRecord.Status);
                    cmd.Parameters.AddWithValue("@NumberOfInstallments", newRecord.NumberOfInstallments);
                    cmd.Parameters.AddWithValue("@InstallmentsValue", newRecord.InstallmentValue);
                    cmd.Parameters.AddWithValue("@InstallmentsLeft", newRecord.InstallmentsLeft);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Expense> FindAllFromSaver(string queryFind)
        {
            List<Expense> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string querySelect = $"SELECT * FROM Expense";
                con.Open();

                SqliteDataReader rdr;

                using (SqliteCommand cmd = new SqliteCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string[] nums = rdr["expense_date"].ToString().Split("-");
                        int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                        Expense record = new Expense()
                        {
                            Id = (int)rdr["id"],
                            SaverId = (int)rdr["saver_id"],
                            Date = new DateOnly(num[0], num[1], num[2]),
                            Value = (float)rdr["value"],
                            Type = rdr["expense_type"].ToString(),
                            Description = rdr["description"].ToString(),
                            Status = rdr["status"].ToString(),
                            NumberOfInstallments = (int)rdr["number_of_installments"],
                            InstallmentValue = (float)rdr["installment_value"],
                            InstallmentsLeft = (int)rdr["installments_left"]
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }

        List<Expense> ReadAll (string querySelect) => throw new NotImplementedException();   

        public void Update(Expense record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE Expense SET date = @Date , value = @Value, type = @Type, description = @Description, number_of_installments = @NumberOfInstallments WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
                {

                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Date", record.Date);
                    cmd.Parameters.AddWithValue("@Value", record.Value);
                    cmd.Parameters.AddWithValue("@Type", record.Type);
                    cmd.Parameters.AddWithValue("@Description", record.Description);
                    cmd.Parameters.AddWithValue("@NumberOfInstallments", record.NumberOfInstallments);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Expense> ReadAll(string query) => throw new NotImplementedException();
    }
}