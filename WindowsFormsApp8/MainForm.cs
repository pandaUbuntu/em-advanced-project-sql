using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp8.Entities;
using WindowsFormsApp8.Forms;
using WindowsFormsApp8.Repo;
using WindowsFormsApp8.Services;

namespace WindowsFormsApp8
{
    public partial class MainForm : Form
    {
        UserRepository userRepository = null;
        public MainForm()
        {
            InitializeComponent();
            this.userRepository = new UserRepository();
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
                string username = ValidateService.validateTextBox(this.textBoxUsername, 2, "name");
                string email = ValidateService.validateTextBox(this.textBoxEmail, 6, "email"); ;
                string password = ValidateService.validateTextBox(this.textBoxPassword, 8, "password");
                string confirmPassword = ValidateService.validateTextBox(this.textBoxConfirmPassword, 8, "confirmPassword");

                if (!Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                    throw new Exception("Невалідний email!");

                if (!Regex.IsMatch(password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                    throw new Exception("Пароль має містити малі літери, великі, число та спецсимвол та мати довжину не менше 8 символів!");

                if (password != confirmPassword)
                    throw new Exception("Паролі не співпадають!");


                if (this.userRepository.create(new Entities.User(email, username, password)))
                    MessageBox.Show("Об'єкт створений успішно", "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Халепа, не вдалося створити об'єкт!", "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            GetOneForm getOneForm = new GetOneForm(this, this.userRepository);
            getOneForm.Show();
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show(
                    "Ви дійсно хочете закрити програму?",
                    "Підтвердження виходу",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
              
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
