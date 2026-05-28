using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using WindowsFormsApp8.Entities;
using WindowsFormsApp8.Services;

namespace WindowsFormsApp8.Repo
{
    public class UserRepository
    {
        ConnectionDatabaseService connector = null;

        public UserRepository() {
            this.connector = new ConnectionDatabaseService("emolod15adv", "root", "root"); 
        }

        public bool create(User newUser)
        {
            string query = 
                "INSERT INTO `users`(`username`, `email`, `password`) " +
                "VALUES (@username, @email, @password)";

            using (MySqlConnection connection = this.connector.getConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@username", newUser.Username);
                command.Parameters.AddWithValue("@email", newUser.Email);
                command.Parameters.AddWithValue("@password", newUser.Password);

                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }

        public bool update(User user) {

            string query =
                    "UPDATE `users` " +
                    "SET `username`= @username,`email`= @email,`password`= @password " +
                    " WHERE id = @id";

            using (MySqlConnection connection = this.connector.getConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@id", user.Id);

                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }

        public User getOne(int id)
        {
            string query = "SELECT * FROM users WHERE id = @id";

            using (MySqlConnection connection = this.connector.getConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                MySqlDataReader reader =  command.ExecuteReader();

                if (reader.Read())
                {
                    User user = new User(
                        Convert.ToInt32(reader["id"]), 
                        reader["email"].ToString(), 
                        reader["username"].ToString(),
                        reader["password"].ToString(), 
                        Convert.ToDateTime(reader["createdAt"]), 
                        Convert.ToDateTime(reader["updatedAt"])
                    );

                    return user;
                }

                return null;
            }
        }

        public List<User> getAll(int limit = 20)
        {
            List<User> users = new List<User>();
            string query = "SELECT * FROM users LIMIT " + limit;

            using (MySqlConnection connection = this.connector.getConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    User user = new User(
                        Convert.ToInt32(reader["id"]),
                        reader["email"].ToString(),
                        reader["username"].ToString(),
                        "",
                        Convert.ToDateTime(reader["createdAt"]),
                        Convert.ToDateTime(reader["updatedAt"])
                    );

                    users.Add(user);
                }
            }

            return users;
        }

        public bool delete(int id)
        {
            string query = "DELETE FROM users WHERE id = @id";

            using (MySqlConnection connection = this.connector.getConnection())
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                if (command.ExecuteNonQuery() == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}
