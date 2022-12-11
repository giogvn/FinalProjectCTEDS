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
            string queryInsert = $"INSERT INTO Expense (saver_id, expense_date, due_date, value, expense_type, description, status," +
                $"number_of_installments, installments_value, installments_left) VALUES (@SaverId, @Date, @DueDate, @Value, @Type, " +
                $"@Description, @Status, @NumberOfInstallments, @InstallmentsValue, @InstallmentsLeft)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
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

        public static List<Expense> FindAllFromSaver(int saverId)
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

        public List<Expense> ReadAll (string querySelect) => throw new NotImplementedException();   

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

        public static List<Expense> GetExpensesFromIncomeResource(int IncomeResourceId, string status = "active")
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

        public static List<Expense> GetExpensesFromPaymentMethod(int PaymentMethodId, string status = "active")
        {
            string queryFind = $"SELECT * FROM Expense WHERE id = (SELECT expense_id FROM " +
                $"ExpensePaymentMethod NATURAL JOIN PaymentMethodIncomeResource WHERE payment_method_id = @PaymentMethodId)" +
                $"AND status = @Status;";

            List<Expense> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@PaymentMethodId", PaymentMethodId);
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
        public float CalculateTotalExpenses(int saverId, DateOnly limitDate)
        {
            string queryFind = $"SELECT * FROM Expense JOIN (SELECT ExpensePaymentMethod, PaymentMethod WHERE saver_id = @SaverId AND payment_method_id = id" +
                $"AND type = credit_card AND invoice_closing_date <= @Date ON id = expense_id";
            List<Expense> expenses = new();
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    cmd.Parameters.AddWithValue("@Status", "active");
                    cmd.Parameters.AddWithValue("@Date", limitDate.ToString("dd/mm/yyyy"));
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
                        expenses.Add(record);
                    }
                }
            }
            float total = 0;
            foreach(Expense expense in expenses)
            {
                total += expense.Value;
            }
            return total;
        }
    }

    public List<Expense> getCategoryExpenses(int categoryId)
    {
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
                        expenses.Add(record);
                    }
                }
            }
            return expenses;
        }        
    }
}