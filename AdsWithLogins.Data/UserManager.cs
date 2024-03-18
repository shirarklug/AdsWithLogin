using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdsWithLogins.Data
{
    public class UserManager
    {
        private string _connectionString;

        public UserManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Users (Name, Email, PasswordHash) " +
                "VALUES (@name, @email, @hash)";
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@hash", hash);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public User Login(string email, string password)
        {
            var user = GetByEmail(email);
            if (user == null)
            {
                return null;
            }

            var isMatch = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!isMatch)
            {
                return null;
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT TOP 1 * FROM Users WHERE Email = @email";
            cmd.Parameters.AddWithValue("@email", email);
            conn.Open();
            var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }

            return new User
            {
                Id = (int)reader["UserId"],
                Email = (string)reader["Email"],
                Name = (string)reader["Name"],
                PasswordHash = (string)reader["PasswordHash"]
            };
        }
    }
}
