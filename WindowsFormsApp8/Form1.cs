using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp8.Repo;

namespace WindowsFormsApp8
{
    public partial class Form1 : Form
    {
        UserRepository userRepository = null;
        public Form1()
        {
            InitializeComponent();
            this.userRepository = new UserRepository();
        }

        private string validateTextBox(TextBox textBox, int minLength, string fieldName)
        {
            string text = textBox.Text.Trim();

            if (text.Length < minLength)
                throw new Exception($"Недостатня довжина тексту в полі {fieldName}!");
             
            return text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBoxUsername.Clear();
            this.textBoxEmail.Clear();
            this.textBoxPassword.Clear();
            this.textBoxConfirmPassword.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string username = validateTextBox(this.textBoxUsername, 2, "name");
                string email = validateTextBox(this.textBoxEmail, 6, "email"); ;
                string password = validateTextBox(this.textBoxPassword, 8, "password");
                string confirmPassword = validateTextBox(this.textBoxConfirmPassword, 8, "confirmPassword");

                if (!Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                    throw new Exception("Невалідний email!");

                if (!Regex.IsMatch(password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                    throw new Exception("Пароль має містити малі літери, великі, число та спецсимвол та мати довжину не менше 8 символів!");
            
                if(password != confirmPassword)
                    throw new Exception("Паролі не співпадають!");

                this.userRepository.create(new Entities.User(email, username, password));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ПОМИЛКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.checkBox1.Checked)
            {
                this.textBoxPassword.PasswordChar = '\0';
                this.textBoxConfirmPassword.PasswordChar = '\0';
            } else
            {
                this.textBoxPassword.PasswordChar = '*';
                this.textBoxConfirmPassword.PasswordChar = '*';
            }
        }
    }
}
