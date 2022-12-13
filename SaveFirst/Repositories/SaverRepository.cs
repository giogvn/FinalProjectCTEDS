using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace SaveFirst.Repositories
{
    public class SaverRepository : IRecord<Saver>
    {
        static string ConnectionString = "Server = DESKTOP-AIPLP16; Initial Catalog = SaveFirst;integrated security=true;";
        public void Delete(int RecordId)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Saver WHERE id = @Id";

                using (SqlCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(Saver newRecord)
        {
            string queryInsert = $"INSERT INTO Saver (id , email, saver_type, payer_id,  name,  birthdate) VALUES (@Id, @Email, @Type, @PayerId, @Name, @Birthday)";
            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@Email", newRecord.Email);
                    cmd.Parameters.AddWithValue("@Type", newRecord.Type);
                    cmd.Parameters.AddWithValue("@PayerId", newRecord.PayerId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Birthday", newRecord.Birthday);

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

        public List<Saver> findSaver(string email, string password)
        {
            List<Saver> list = new();
            string querySelect = "SELECT * FROM Saver WHERE email = @Email AND password = @Password";
            using (SqlConnection con = new(ConnectionString))
            {
                con.Open();
                SqlDataReader rdr;
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);                   
                    try
                    {
                        rdr = cmd.ExecuteReader();
                        rdr.Read();
                        string[] nums = rdr["birthdate"].ToString().Split("-");
                        int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                        Saver record = new Saver()
                        {
                            Id = rdr["id"].ToString(),
                            Type = rdr["type"].ToString(),
                            Name = rdr["name"].ToString(),
                            Birthday = new DateTime(num[0], num[1], num[2])
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

        public List<Saver> ReadAll()
        {
            List<Saver> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                string querySelect = $"SELECT * FROM Saver";
                con.Open();
                SqlDataReader  rdr;
                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {                    
                    try
                    {
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] nums = rdr["birthdate"].ToString().Split("-");
                            int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                            Saver record = new Saver()
                            {
                                Id =  rdr["id"].ToString(),
                                Type = rdr["type"].ToString(),
                                Name = rdr["name"].ToString(),
                                Birthday = new DateTime(num[0], num[1], num[2])
                            };
                            list.Add(record);
                        }
                    }
                    catch (SqlException)
                    {
                        return list;    
                    }                    
                }
            }
            return list;
        }

        public void Update(Saver record)
        {
            using (SqlConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE Saver SET name = @Name , birthdate = @Birthday WHERE id = @Id";

                using (SqlCommand cmd = new(queryUpdateBody, con))
                {

                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@Name", record.Name);
                    cmd.Parameters.AddWithValue("@Birthday", record.Birthday);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}