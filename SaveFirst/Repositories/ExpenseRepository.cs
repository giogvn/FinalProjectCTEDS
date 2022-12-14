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
    public class ExpenseRepository : IRecord<Expense>
    {
        static string ConnectionString = "Server = DESKTOP-AIPLP16; Initial Catalog = SaveFirst;integrated security=true;";
        public void Delete(int RecordId)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Expense WHERE id = @Id";

                using (SqlCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(Expense newRecord)
        {
            string queryInsert = $"INSERT INTO Expense (id, saver_id, expense_date, due_date, value, expense_type, description, status," +
                $"number_of_installments, installment_value, installments_left) VALUES (@Id, @SaverId, @Date, @DueDate, @Value, @Type, " +
                $"@Description, @Status, @NumberOfInstallments, @InstallmentsValue, @InstallmentsLeft)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {

                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Date", newRecord.Date.ToString());
                    cmd.Parameters.AddWithValue("@DueDate", newRecord.DueDate.ToString());
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

        public List<Expense> FindAllFromSaver(string saverId)
        {
            string queryFind = $"SELECT * FROM Expense WHERE status = @Status AND saver_id = @SaverId;";
            List<Expense> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "active");
                    cmd.Parameters.AddWithValue("@SaverId", saverId);
                    rdr = cmd.ExecuteReader();

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
                }
            }
            return list;
        }

        public List<Expense> ReadAll() => throw new NotImplementedException();

        public void Update(Expense record)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE Expense SET date = @Date , value = @Value, type = @Type, description = @Description, number_of_installments = @NumberOfInstallments WHERE id = @Id";

                using (SqlCommand cmd = new(queryUpdateBody, con))
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

        public static List<Expense> GetExpensesFromIncomeResource(string IncomeResourceId, string status = "active")
        {
            string queryFind = $"SELECT * FROM Expense WHERE id = (SELECT expense_id FROM " +
                $"ExpensePaymentMethod NATURAL JOIN PaymentMethodIncomeResource WHERE income_resource_id = @IncomeResourceId)" +
                $"AND status = @Status;";

            List<Expense> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@IncomeResource", IncomeResourceId);
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string[] nums = rdr["expense_date"].ToString().Split("-");
                        int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                        Expense record = new Expense()
                        {
                            Id = rdr["id"].ToString(),
                            SaverId = rdr["saver_id"].ToString(),
                            Date = new DateTime(num[0], num[1], num[2]),
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

        public static List<Expense> GetExpensesFromPaymentMethod(string PaymentMethodId, string status = "active")
        {
            string queryFind = $"SELECT * FROM Expense JOIN (SELECT expense_id FROM ExpensePaymentMethod WHERE payment_method_id = @PaymentMethodId) Temp ON id = Temp.expense_id;";

            List<Expense> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@PaymentMethodId", PaymentMethodId);
                    rdr = cmd.ExecuteReader();



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
                }
            }
            return list;
        }

        public double CalculateTotalExpenses(string saverId, DateTime limitDate)
        {
            string queryFind = $"SELECT * FROM Expense WHERE status = @Status AND saver_id = @SaverId";
            List<Expense> expenses = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "active");
                    cmd.Parameters.AddWithValue("@SaverId", saverId);
                    rdr = cmd.ExecuteReader();

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
                        expenses.Add(record);
                    }
                }
            }
            double total = 0;
            foreach (Expense expense in expenses)
            {
                if (expense.DueDate <= limitDate)
                {
                    total += expense.Value;
                }
            }
            return total;
        }

        public List<Expense> getCategoryExpenses(string categoryId)
        {
            string queryFind = $"SELECT * FROM Expense JOIN ExpenseCategory ON expense_id = id WHERE category_id = @CategoryId";
            List<Expense> expenses = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    rdr = cmd.ExecuteReader();

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
                        expenses.Add(record);
                    }
                }
            }
            return expenses;
        }
    }
}

