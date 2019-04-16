using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DBO;
using WindowsFormsApp1.DataTables;

namespace WindowsFormsApp1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        /// 
        public static bool relog = false;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            string uName = Environment.UserName;
            bool result = dbOps.ExistCheck(uName);
            if (result == true)
                Application.Run(new LoginForm());
            else
                Application.Run(new LoginForm());
                MessageBox.Show("Пользователь не найден\nЗаменить на форму регистрации");
        }
    }
}
