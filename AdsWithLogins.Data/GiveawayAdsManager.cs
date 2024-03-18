using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdsWithLogins.Data
{
    public class GiveawayAdsManager
    {
        public string _connectionString;

        public GiveawayAdsManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<GiveawayAd> GetGiveawayAds()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Giveaway";
            connection.Open();

            List<GiveawayAd> giveaways = new();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                giveaways.Add(new GiveawayAd
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"],
                    Date = (DateTime)reader["Date"],
                    Details = (string)reader["Details"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    UserId = (int)reader["UserId"]
                });
            }
            return giveaways;
        }

        public int AddGiveaway(string name, string details, string phoneNumber, int userId)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Giveaway(Name, Date, Details, PhoneNumber, UserId)
                                VALUES (@name, @date, @details, @phoneNumber, @userId)
                                SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.Parameters.AddWithValue("@details", details);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@userId", userId);
            connection.Open();
            int id = (int)(decimal)cmd.ExecuteScalar();
            return id;
        }

        public void Delete(int id)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Giveaway WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public int? GetUserIdByEmail(string email)
        {
            if (email == null)
            {
                return null;
            }
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Users WHERE Email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            connection.Open();
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
    }
}
