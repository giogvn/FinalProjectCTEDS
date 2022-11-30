﻿using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace SaveFirst.Repositories
{
    public class CategoryRepository : IRecord<Category>
    {
        static string ConnectionString = "Data source = Saver.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM Category WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(Category newRecord)
        {
            string queryInsert = $"INSERT INTO Category (name) VALUES (@SaverId, @Name)";

            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        static List<Category> FindAllFromSaver(string queryFind)
        {
            Category record = null;
            List<Category> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"SELECT * FROM Category WHERE saver_id = '{Id}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();
                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {

                            record = new Category()
                            {
                                Id = (int)rdr["id"],
                                SaverId = (int)rdr["saver_id"],
                                Name = rdr["name"].ToString()
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

        public List<Category> ReadAll(string query) => throw new NotImplementedException();

        public void Update(Category record) => throw new NotImplementedException();
    }
}