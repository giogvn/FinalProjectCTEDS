using SaveFirst.Interfaces;
using SaveFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using SaveFirst.Builders;

namespace SaveFirst.Repositories
{
    public class SaverRepository : IRecord<Saver>
    {
        private static ModelBuilder modelBuilder = new();
        static string ConnectionString = "Server=labsoft.pcs.usp.br; Initial Catalog=db_7; User id=''; pwd='';";
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
            string queryInsert = $"INSERT INTO Saver (id , email, type, payer_id,  name,  birthdate, password) VALUES (@Id, @Email, @Type, @PayerId, @Name, CONVERT(datetime,@Birthday,103), @Password)";
            using (SqlConnection con = new(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@Id", newRecord.Id);
                    cmd.Parameters.AddWithValue("@Password", newRecord.Password);
                    cmd.Parameters.AddWithValue("@Email", newRecord.Email);
                    cmd.Parameters.AddWithValue("@Type", newRecord.Type);
                    cmd.Parameters.AddWithValue("@PayerId", newRecord.PayerId);
                    cmd.Parameters.AddWithValue("@Name", newRecord.Name);
                    cmd.Parameters.AddWithValue("@Birthday", newRecord.Birthday.ToString());

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Saver> FindAllFromSaver(int Id)  => throw new NotImplementedException();

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
                    rdr = cmd.ExecuteReader();
                    return modelBuilder.Build(rdr, "saver");
                }
            }
        }
        public List<Saver> ReadAll() => throw new NotImplementedException();

        public void Update(Saver record) => throw new NotImplementedException();
    }
}
