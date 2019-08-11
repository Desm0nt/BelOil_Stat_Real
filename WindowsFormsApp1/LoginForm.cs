using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBO;
using WindowsFormsApp1.DataTables;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                bool checkRes = dbOps.ExistCheck(textBox1.Text); // проверка есть ли пользователь
                if (checkRes == false)
                    throw new Exception("Пользователь с таким именем не найден.");
                bool loginRes = dbOps.Login(textBox1.Text, textBox2.Text);  // попытка входа с заданным логином и паролем
                if (loginRes == false)
                    MessageBox.Show("Не верный пароль.");
                else
                {
                    ReportsForm reportsForm = new ReportsForm();
                    reportsForm.FormClosed += new FormClosedEventHandler(reportsForm_FormClosed);
                    reportsForm.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // метод, выполняемый при закрытии основной формы
        void reportsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Program.relog == false)
            {
                Application.Exit();
            }
            else
            {
                this.Show();
                Program.relog = false;
            }
        }
    }
}
