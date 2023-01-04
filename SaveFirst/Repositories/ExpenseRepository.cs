using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using SaveFirst.Builders;

namespace SaveFirst.Repositories
{
    public class ExpenseRepository : IRecord<Expense>
    {

        private static ModelBuilder modelBuilder = new();
        static string ConnectionString = "Server=labsoft.pcs.usp.br; Initial Catalog=db_7; User id=''; pwd='';";
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
                $"number_of_installments, installment_value, installments_left) VALUES (@Id, @SaverId,  CONVERT(datetime,@Date,103), CONVERT(datetime,@DueDate,103), @Value, @Type, " +
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
                    return modelBuilder.Build(rdr, "expense");
                }
            }
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
                    return modelBuilder.Build(rdr, "expense");
                }
            }
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
                    return modelBuilder.Build(rdr, "expense");
                }
            }
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
                    return modelBuilder.Build(rdr, "expense");
                }
            }
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
                    return modelBuilder.Build(rdr, "expense");
                }
            }
        }
    }
}

