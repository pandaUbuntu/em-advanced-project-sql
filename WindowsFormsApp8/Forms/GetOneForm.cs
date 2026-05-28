using Org.BouncyCastle.Asn1.X509;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WindowsFormsApp8.Entities;
using WindowsFormsApp8.Repo;
using WindowsFormsApp8.Services;

namespace WindowsFormsApp8.Forms
{
    public partial class GetOneForm : Form
    {
        UserRepository userRepository = null;
        MainForm mForm = null;
        User user = null;
        public GetOneForm(MainForm mForm, UserRepository userRepository)
        {
            InitializeComponent();
            this.userRepository = userRepository;
            this.mForm = mForm;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            int id = Convert.ToInt32(this.textBoxId.Text.Trim());

            if (id <= 0)
                throw new Exception("Недопустимий id");

            user = this.userRepository.getOne(id);

            this.textBoxEmail.Text = user.Email;
            this.textBoxUserName.Text = user.Username;
            this.textBoxPassword.Text = user.Password;
            this.labelCreatedAt.Text = user.CreatedAt.ToString("dd-MM-yyyy");
            this.labelUpdatedAt.Text = user.UpdatedAt.ToString("dd-MM-yyyy");

        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            try
            {
                string username = ValidateService.validateTextBox(this.textBoxUserName, 2, "name");
                string email = ValidateService.validateTextBox(this.textBoxEmail, 6, "email"); ;
                string password = ValidateService.validateTextBox(this.textBoxPassword, 8, "password");

                if (!Regex.IsMatch(email, "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"))
                    throw new Exception("Невалідний email!");

                if (!Regex.IsMatch(password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                    throw new Exception("Пароль має містити малі літери, великі, число та спецсимвол та мати довжину не менше 8 символів!");

                user.Email = email;
                user.Username = username;
                user.Password = password;

                if (this.userRepository.update(user))
                    MessageBox.Show("Об'єкт оновлений", "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Халепа, не вдалося оновити об'єкт!", "Результат!", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ПОМИЛКА!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            this.mForm.Visible = true;
            this.Dispose();
        }

        private void GetOneFormClosing(object sender, FormClosingEventArgs e)
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
                    Application.Exit();
                }

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
