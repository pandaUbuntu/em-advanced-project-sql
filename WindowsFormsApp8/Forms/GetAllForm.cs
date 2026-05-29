using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsApp8.Entities;
using WindowsFormsApp8.Repo;

namespace WindowsFormsApp8.Forms
{
    public partial class GetAllForm : Form
    {

        UserRepository userRepository = null;
        MainForm mForm = null;

        public GetAllForm(MainForm mForm, UserRepository userRepository)
        {
            InitializeComponent();

            this.userRepository = userRepository;
            this.mForm = mForm;


            createList();
        }

        private void createList()
        {
            List<User> users = this.userRepository.getAll();

            for (int i = 0; i < users.Count; i++)
            {
                createRow(users[i], new Point(12, 100 * i + 5));
            }
        }

        private void createRow(User user, Point point)
        {
            GroupBox groupBox1 = new System.Windows.Forms.GroupBox();
            Label label1 = new System.Windows.Forms.Label();
            Label label2 = new System.Windows.Forms.Label();
            Label label3 = new System.Windows.Forms.Label();
            Button button1 = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();

            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = point;
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(560, 100);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = user.Id.ToString();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 30);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(67, 27);
            label1.TabIndex = 0;
            label1.Text = user.Username;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 57);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(67, 27);
            label2.TabIndex = 1;
            label2.Text = user.Email;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(437, 66);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 27);
            label3.TabIndex = 2;
            label3.Text = user.CreatedAt.ToString("dd-MM-yyyy");
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(442, 20);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(112, 37);
            button1.TabIndex = 3;
            button1.Text = "Видалити";
            button1.UseVisualStyleBackColor = true;
            button1.Click += (object sender, EventArgs e) =>
            {
                this.userRepository.delete(user.Id);

                this.Controls.Clear();
                createList();
            };

            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();

            this.Controls.Add(groupBox1);
        }

        private void GetAllClosing(object sender, FormClosingEventArgs e)
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
