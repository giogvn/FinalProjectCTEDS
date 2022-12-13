using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveFirst.Models;
using SaveFirst.Interfaces;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Data.SqlClient;

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
            using (SqlConnection con = new SqlConnection(this.ConnectionString))
            {
                // SQL Injection
                //string queryInsert = $"INSERT INTO Products (IdProduct, Name, Description, Price) VALUES ('{newProduct.IdProduct}', '{newProduct.Name}', '{newProduct.Description}', {newProduct.Price})";

                string queryInsert = $"INSERT INTO {DatabaseName} ({Labels[0]}, {Labels[1]}, saver_id) VALUES (@ForeignKey1, @ForeignKey2, @SaverId)";

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
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

        public List<IntermediateModel> FindAllFromSaver(int saverId)
        {
            IntermediateModel record = null;
            List<IntermediateModel> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                //string queryFind = $"IF EXISTS(SELECT * FROM UserCredentials WHERE Email = {email})\r\n BEGIN\r\n   SELECT * FROM UserCredentials WHERE Email = {email}\r\n\r\n END";
                string queryFind = $"SELECT * FROM {DatabaseName} WHERE saver_id = '@SaverId'";
                using (SqlCommand cmd = new SqlCommand(queryFind, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@SaverId", saverId);

                    try
                    {
                        SqlDataReader  rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            record = new IntermediateModel()
                            {
                                ForeignKey1 = rdr[$"{Labels[0]}"].ToString(),
                                ForeignKey2 = rdr[$"{Labels[1]}"].ToString(),
                                SaverId = rdr["saver_id"].ToString()
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

        public List<IntermediateModel> ReadAll()
        {
            List<IntermediateModel> list = new();
            using (SqlConnection con = new(ConnectionString))
            {
                string querySelect = $"SELECT * FROM {DatabaseName}";
                con.Open();

                SqlDataReader  rdr;

                using (SqlCommand cmd = new SqlCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        IntermediateModel record = new()
                        {
                            ForeignKey1 = rdr[$"{Labels[0]}"].ToString(),
                            ForeignKey2 = rdr[$"{Labels[1]}"].ToString(),
                            SaverId = rdr["saver_id"].ToString()
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
