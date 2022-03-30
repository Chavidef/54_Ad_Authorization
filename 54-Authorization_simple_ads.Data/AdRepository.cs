using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _54_Authorization_simple_ads.Data
{
    public class AdRepository
    {
        private string _connectionString;

        public AdRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddAd(Ad ad)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Ads (PhoneNumber, Details, DateCreated, UserId) " +
                "VALUES (@phoneNumber, @details, @dateCreated, @userId)";
            cmd.Parameters.AddWithValue("@phoneNumber", ad.PhoneNumber);
            cmd.Parameters.AddWithValue("@details", ad.Details);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@userId", ad.UserId);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public List<Ad> GetAllAds()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ads a " +
                "JOIN Users u " +
                "ON a.UserId = u.Id";
            connection.Open();
            var reader = cmd.ExecuteReader();
            var ads = new List<Ad>();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Id = (int)reader["Id"],
                    UserId = (int)reader["UserId"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Details = (string)reader["Details"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    UserName = (string)reader["Name"]
                });

            }
            return ads;
        }

        public List<Ad> GetAdsById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Ads a " +
                "JOIN Users u " +
                "ON a.UserId = u.Id " +
                "WHERE u.Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            var reader = cmd.ExecuteReader();
            var ads = new List<Ad>();
            while (reader.Read())
            {
                ads.Add(new Ad
                {
                    Id = (int)reader["Id"],
                    UserId = (int)reader["UserId"],
                    PhoneNumber = (string)reader["PhoneNumber"],
                    Details = (string)reader["Details"],
                    DateCreated = (DateTime)reader["DateCreated"],
                    UserName = (string)reader["Name"]
                });

            }
            return ads;
        }
        public void DeleteAd(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM Ads WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
