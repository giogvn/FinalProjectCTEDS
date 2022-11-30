using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveFirst.Models;
using SaveFirst.Interfaces;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Data.Sqlite;

namespace SaveFirst.Repositories
{
    public class IntermediateRepository : IRecord<IntermediateModel>
    {
        private string ConnectionString { get; set; } = string.Empty;
        private string DatabaseName { get; set; } = string.Empty;
        private string[] Labels { get; set; }

        public IntermediateRepository(string connectionString, string databaseName, string[] labels)
        {
            if (connectionString == string.Empty || databaseName == string.Empty)
                throw new ArgumentNullException("Received empty strings");
            else if (labels.Length != 2)
                throw new ArgumentException("Invalid number of labels for Intermediate DB");

            ConnectionString = connectionString;
            DatabaseName = databaseName;
            Labels = labels;    

        }

        public void Create(IntermediateModel newRecord)
        {
            using (SqliteConnection con = new SqliteConnection(ConnectionString))
            {
                // SQL Injection
                //string queryInsert = $"INSERT INTO Products (IdProduct, Name, Description, Price) VALUES ('{newProduct.IdProduct}', '{newProduct.Name}', '{newProduct.Description}', {newProduct.Price})";

                string queryInsert = $"INSERT INTO {DatabaseName} ({Labels[0]}, {Labels[1]}, saver_id) VALUES (@ForeignKey1, @ForeignKey2, @SaverId)";

                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@ForeignKey1", newRecord.ForeignKey1);
                    cmd.Parameters.AddWithValue("@ForeignKey2", newRecord.ForeignKey2);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    
                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int RecordId)
        {
            //Intermediate delete shall be handled directly through sql
            throw new NotImplementedException();
        }

        static List<IntermediateModel> FindAllFromSaver(string queryFind)
        {
            IntermediateModel record = null;
            List<IntermediateModel> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"IF EXISTS(SELECT * FROM UserCredentials WHERE Email = {email})\r\n BEGIN\r\n   SELECT * FROM UserCredentials WHERE Email = {email}\r\n\r\n END";
                string queryFind = $"SELECT * FROM {DatabaseName} WHERE saver_id = '{SaverId}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();

                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            record = new IntermediateModel()
                            {
                                ForeignKey1 = (int)rdr[$"{Labels[0]}"],
                                ForeignKey2 = (int)rdr[$"{Labels[1]}"],
                                SaverId = (int)rdr["saver_id"]
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

        public List<IntermediateModel> ReadAll(string querySelect)
        {
            List<IntermediateModel> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string querySelect = $"SELECT * FROM {DatabaseName}";
                con.Open();

                SqliteDataReader rdr;

                using (SqliteCommand cmd = new SqliteCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        IntermediateModel record = new()
                        {
                            ForeignKey1 = (int) rdr[$"{Labels[0]}"],
                            ForeignKey2 = (int) rdr[$"{Labels[1]}"],
                            SaverId = (int) rdr["saver_id"]
                        };

                        list.Add(record);
                    }
                }
            }
            return list;
        }

        public void Update(IntermediateModel record)
        {
            throw new NotImplementedException();
        }
    }
}
