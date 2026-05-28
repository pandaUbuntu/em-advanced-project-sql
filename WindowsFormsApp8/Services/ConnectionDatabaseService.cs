using System;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp8.Services
{
    public class ConnectionDatabaseService
    {
        private string connectionString = "";
        public ConnectionDatabaseService(
            string databaseName, 
            string username, 
            string password, 
            string server = "localhost", 
            int port = 3306) {

            this.connectionString = $"server={server};port={port};database={databaseName};user={username};password={password};";
        }

        public MySqlConnection getConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
