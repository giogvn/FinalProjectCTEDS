using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Repositories
{
    public class SaverRepository : IRecord<Saver>
    {
        private string ConnectionString = "Data source = Saver.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Saver WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
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
            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
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

        public static List<Saver> FindAllFromSaver(string queryFind)
        {
            Saver record = null;
            List<Saver> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"SELECT * FROM Saver WHERE payer_id = '{Id}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();

                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {

                            string[] nums = rdr["birthdate"].ToString().Split("-");
                            int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                            record = new Saver()
                            {
                                Id = (int)rdr["id"],
                                Type = (int)rdr["user_type"],
                                PayerId = (int)rdr["payer_id"],
                                Birthday = new DateOnly(num[0], num[1], num[2]),                            
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

        public List<Saver> ReadAll(string querySelect)
        {
            List<Saver> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string querySelect = $"SELECT * FROM Saver WHERE payer_id = @PayerId";
                con.Open();

                SqliteDataReader rdr;

                using (SqliteCommand cmd = new SqliteCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string[] nums = rdr["birthdate"].ToString().Split("-");
                        int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                        Saver record = new Saver()
                        {
                            Id = (int)rdr["id"],
                            Type = (int)rdr["saver_id"],
                            Name = rdr["name"].ToString(),
                            Birthday = new DateOnly(num[0], num[1], num[2])
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }

        public void Update(Saver record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE Saver SET name = @Name , birthdate = @Birthday WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
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