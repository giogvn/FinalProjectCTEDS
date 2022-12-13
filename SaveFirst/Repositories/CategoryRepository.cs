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
        
        public List<Saver> FindAllFromSaver(int Id)
        {
            Saver record = null;
            List<Saver> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                string queryFind = $"SELECT * FROM Saver WHERE payer_id = '{Id}'";
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    con.Open();

                    try
                    {
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {

                            string[] nums = rdr["birthdate"].ToString().Split("-");
                            int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                            record = new Saver()
                            {
                                Id = rdr["id"].ToString(),
                                Type = rdr["user_type"].ToString(),
                                PayerId = rdr["payer_id"].ToString(),
                                Birthday = new DateTime(num[0], num[1], num[2]),
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

        public List<Category> getExpenseCategory(string expenseId)
        {
        string queryFind = $"SELECT * FROM Category WHERE id = (SELECT category_id FROM ExpenseCategory WHERE expense_id = @ExpenseId);";
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
