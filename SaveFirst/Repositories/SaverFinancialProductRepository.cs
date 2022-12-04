using Microsoft.Data.Sqlite;
using SaveFirst.Interfaces;
using SaveFirst.Repositories;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveFirst.Repositories
{
    public class SaverFinancialProductRepository : IRecord<SaverFinancialProduct>
    {
        static string ConnectionString = "Data source = SaverFinancialProduct.db";
        public void Delete(int RecordId)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryDelete = "DELETE FROM SaverFinancialProduct WHERE id = @Id";

                using (SqliteCommand cmd = new(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@Id", RecordId);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Create(SaverFinancialProduct newRecord)
        {
            string queryInsert = $"INSERT INTO SaverFinancialProduct (id ,saver_id, financial_product_id,  recurrence,  reason,  purchase_date,  number_of_shares) VALUES (@Id, @SaverId, @FinancialProductId, @Recurrence, @Reason, @PurchaseDate, @NumberOfShares )";
            using (SqliteConnection con = new(ConnectionString))
            {
                using (SqliteCommand cmd = new SqliteCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@SaverId", newRecord.SaverId);
                    cmd.Parameters.AddWithValue("@FinancialProductId", newRecord.FinancialProductName);
                    cmd.Parameters.AddWithValue("@Recurrence", newRecord.Recurrence);
                    cmd.Parameters.AddWithValue("@Reason", newRecord.Reason);
                    cmd.Parameters.AddWithValue("@PurchaseDate", newRecord.PurchaseDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@NumberOfShares", newRecord.NumberOfShares);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        static List<SaverFinancialProduct> FindAllFromSaver(string queryFind)
        {
            SaverFinancialProduct record = null;
            List<SaverFinancialProduct> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string queryFind = $"IF EXISTS(SELECT * FROM UserCredentials WHERE Email = {email})\r\n BEGIN\r\n   SELECT * FROM UserCredentials WHERE Email = {email}\r\n\r\n END";
                //string queryFind = $"SELECT * FROM SaverFinancialProduct WHERE saver_id = '{SaverId}'";
                using (SqliteCommand cmd = new SqliteCommand(queryFind, con))
                {
                    con.Open();

                    try
                    {
                        SqliteDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            string[] nums = rdr["purchase_date"].ToString().Split("-");
                            int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                            record = new SaverFinancialProduct()
                            {
                                Id = (int)rdr["id"],
                                SaverId = (int)rdr["saver_id"],
                                FinancialProductName = rdr["financial_product_name"].ToString(),
                                Recurrence = rdr["recurrence"].ToString(),
                                Reason = rdr["reason"].ToString(),
                                PurchaseDate = new DateOnly(num[0], num[1], num[2]),
                                NumberOfShares = (int)rdr["number_of_shares"]
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

        public List<SaverFinancialProduct> ReadAll(string querySelect)
        {
            List<SaverFinancialProduct> list = new();
            using (SqliteConnection con = new(ConnectionString))
            {
                //string querySelect = $"SELECT * FROM SaverFinancialProduct";
                con.Open();

                SqliteDataReader rdr;

                using (SqliteCommand cmd = new SqliteCommand(querySelect, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        string[] nums = rdr["purchase_date"].ToString().Split("-");
                        int[] num = { int.Parse(nums[0]), int.Parse(nums[1]), int.Parse(nums[2]) };
                        SaverFinancialProduct record = new SaverFinancialProduct()
                        {
                            Id = (int)rdr["id"],
                            SaverId = (int)rdr["saver_id"],
                            FinancialProductName = rdr["financial_product_name"].ToString(),
                            Recurrence = rdr["recurrence"].ToString(),
                            Reason = rdr["reason"].ToString(),
                            PurchaseDate = new DateOnly(num[0], num[1], num[2]),
                            NumberOfShares = (int)rdr["number_of_shares"]
                        };
                        list.Add(record);
                    }
                }
            }
            return list;
        }

        public void Update(SaverFinancialProduct record)
        {
            using (SqliteConnection con = new(ConnectionString))
            {
                string queryUpdateBody = "UPDATE SaverFinancialProduct SET id = @Id , saver_id = @SaverId, financial_product_id = @FinancialProductId,  recurrence = @Recurrence,  reason = @Reason,  purchase_date = @PurchaseDate,  number_of_shares = @NumberOfShares WHERE id = @Id";

                using (SqliteCommand cmd = new(queryUpdateBody, con))
                {
                    cmd.Parameters.AddWithValue("@Id", record.Id);
                    cmd.Parameters.AddWithValue("@SaverId", record.SaverId);
                    cmd.Parameters.AddWithValue("@FinancialProductId", record.FinancialProductName);
                    cmd.Parameters.AddWithValue("@Recurrence", record.Recurrence);
                    cmd.Parameters.AddWithValue("@Reason", record.Reason);
                    cmd.Parameters.AddWithValue("@PurchaseDate", record.PurchaseDate.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@NumberOfShares", record.NumberOfShares);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        
    }
}
