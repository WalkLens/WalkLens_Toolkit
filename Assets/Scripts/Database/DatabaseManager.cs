using System;
using System.Data.SqlClient;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private string connectionString = "Server=tcp:your_server.database.windows.net,1433;Initial Catalog=your_database;Persist Security Info=False;User ID=your_username;Password=your_password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

    public void SaveUser(string name, string job, string hobby)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO Users (Name, Job, Hobby) VALUES (@Name, @Job, @Hobby)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Job", job);
                command.Parameters.AddWithValue("@Hobby", hobby);
                command.ExecuteNonQuery();
            }
        }
    }

    public void LoadUsers()
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string query = "SELECT * FROM Users";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string name = reader["Name"].ToString();
                        string job = reader["Job"].ToString();
                        string hobby = reader["Hobby"].ToString();
                        Debug.Log($"Name: {name}, Job: {job}, Hobby: {hobby}");
                    }
                }
            }
        }
    }
}
