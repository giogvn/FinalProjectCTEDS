using DatabasePopulator.Interfaces;
using DatabasePopulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabasePopulator.Repositories
{
    public class CategoryRepository : IRecord<Category>
    {
        static string ConnectionString = "Server = DESKTOP-AIPLP16; Initial Catalog = SaveFirst ;integrated security=true;";
        public void Delete(int RecordId)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Category WHERE id = @Id";

                using (SqlCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(Category newRecord)
        {
            string queryInsert = $"INSERT INTO Category (id, saver_id, name) VALUES (@Id, @SaverId, @Name)";

            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<Category> FindAllFromSaver(string saverId)
        {
            List<Category> list = new();
            string querySelect = "SELECT * FROM Category WHERE saver_id = @SaverId";
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", saverId);
                    try
                    {
                        rdr = cmd.ExecuteReader();
                        rdr.Read();                   
                        Category record = new Category()
                        {
                            Id = rdr["id"].ToString(),
                            SaverId = rdr["saver_id"].ToString(),
                            Name = rdr["name"].ToString()
                        };
                        list.Add(record);
                        return list;
                    }
                    catch (Exception e)
                    {
                        return list;
                    }
                }
            }
        }

        public List<Category> getExpenseCategory(string expenseId)
        {
        string queryFind = $"SELECT * FROM ExpenseCategory JOIN Category ON expense_id = id WHERE expense_id = @expenseId;";
        List<Category> categories = new();
        using (SqlConnection con = new(ConnectionString))
        {
            con.Open();
            SqlDataReader rdr;
            using (SqlCommand cmd = new SqlCommand(queryFind, con))
            {
                cmd.Parameters.AddWithValue("@expenseId", expenseId);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {                       
                    Category record = new Category()
                    {
                        Id = rdr["id"].ToString(),
                        SaverId = rdr["saver_id"].ToString(),
                        Name = rdr["name"].ToString()
                    };
                    categories.Add(record);
                }
            }
        }
        return categories;
    }
        public void Update(Category record) => throw new NotImplementedException();
        public List<Category> ReadAll() => throw new NotImplementedException();
    }
}