using System;

namespace WindowsFormsApp8.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        public User ()
        {
        }

        public User(string email, string username, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }

        public User(int id, string email, string username, string password, DateTime createdAt, DateTime updatedAt)
        {
            this.Id = id;
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.CreatedAt = createdAt;
            this.UpdatedAt = updatedAt;
        }
    }
}
