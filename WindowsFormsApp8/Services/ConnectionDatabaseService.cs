using System;
using MySql.Data.MySqlClient;

namespace WindowsFormsApp8.Services
{
    public class ConnectionDatabaseService
    {
        private MySqlConnection connection = null;
        public ConnectionDatabaseService(
            string databaseName, 
            string username, 
            string password, 
            string server = "localhost", 
            int port = 3306) {

            string connectionString = $"server={server};port={port};database={databaseName};user={username};password={password};";

            this.connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection getConnection()
        {
            return this.connection;
        }
    }
}
