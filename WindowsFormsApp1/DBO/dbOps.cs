using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DataTables;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1.DBO
{
    class dbOps
    {
        // строка подключения
        static string cnStr = ConfigurationManager.AppSettings["NewTerDB"];

        /// <summary>
        /// Проверка на существование пользователя
        /// </summary>
        /// <param name="uName">Имя пользователя</param>
        /// <returns>Сообщение о существовании</returns>
        public static bool ExistCheck(string uName)
        {
            bool status = false;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewUsers where login = @login";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@login", uName);

                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result == 1)
                    status = true;

                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка проверки наличия пользователя: " + Ex.Message);
            }
            return status;
        }

        /// <summary>
        /// Вход пользователя в систему
        /// </summary>
        /// <param name="Login">Имя пользователя</param>
        /// <param name="Password">Пароль</param>
        /// <returns>Сообщение об успешности входа</returns>
        public static bool Login(string login, string password)
        {
            bool status = false;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                //проверка на корректность пароля
                string query = "SELECT * FROM NewUsers WHERE login = @login AND password = @pass";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pass", password);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        status = true;
                        CurrentData.UserData = new UserTable { Id = Int32.Parse(dr["id"].ToString()), Login = dr["login"].ToString(), Password = dr["password"].ToString(), Name = dr["name"].ToString(), Id_org = Int32.Parse(dr["id_org"].ToString()), Post = dr["post"].ToString(), Email = dr["email"].ToString(), Phone = dr["phone"].ToString() };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка логина: " + Ex.Message);
            }
            return status;
        }

        /// <summary>
        /// Получение данных об организации
        /// </summary>
        /// <param name="id_org">ID организации</param>
        public static string GetCompanyName(int id_org)
        {
            string name = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT name FROM [NewOrg] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id_org);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        name = dr["name"].ToString();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return name;
        }

        public static string GetProdUnit(int id_prod)
        {
            string unit = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT unit FROM [NewProduct] where id = @id_prod";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_prod", id_prod);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        unit = dr["unit"].ToString();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return unit;
        }

        public static List<NormTable> GetNormList(int id_org, int id_rep)
        {
            List<NormTable> NormList = new List<NormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewNormData] WHERE [id_norm] = @id_norm AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_norm", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                string[] rowopt = !String.IsNullOrWhiteSpace(dr["row_options"].ToString())?dr["row_options"].ToString().Split(','): new string[] { };
                                NormList.Add(new NormTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_prod = Int32.Parse(dr["id_prod"].ToString()), Code = Int32.Parse(dr["code"].ToString()), name = dr["name"].ToString(), fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0, type = Int32.Parse(dr["type"].ToString()), row_options = rowopt, val_plan = float.Parse(dr2["value_plan"].ToString()), val_fact = float.Parse(dr2["value_fact"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return NormList;
        }

        public static List<RecievedTable> GetRecievedList(int id_owner, int id_rep)
        {
            List<RecievedTable> RecievedList = new List<RecievedTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewRecievedOrgList] where id_owner = @id_owner";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewRecievedOrg] WHERE [id_recieved] = @id_recieved AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_recieved", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {

                                //тут баги
                                RecievedList.Add(new RecievedTable { Id = Int32.Parse(dr["id"].ToString()), Id_owner = Int32.Parse(dr["id_owner"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())), res_type = Int32.Parse(dr["res_type"].ToString()), value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return RecievedList;
        }

        public static List<SendedTable> GetSendedList(int id_owner, int id_rep)
        {
            List<SendedTable> SendedList = new List<SendedTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewSendedOrgList] where id_owner = @id_owner";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewSendedOrg] WHERE [id_sended] = @id_sended AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_sended", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {

                                //тут баги
                                SendedList.Add(new SendedTable { Id = Int32.Parse(dr["id"].ToString()), Id_owner = Int32.Parse(dr["id_owner"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())), res_type = Int32.Parse(dr["res_type"].ToString()), value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return SendedList;
        }

        public static List<SourceTable> GetSourceList(int id_org, int id_rep)
        {
            List<SourceTable> SourceList = new List<SourceTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgSoucesList] where id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewOrgSouces] WHERE [id_src] = @id_src AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_src", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                int fuel_id = Int32.Parse(dr["id_fuel"].ToString());
                                int fuel_type = 0;
                                if (fuel_id > 5000)
                                    fuel_type = 5000;
                                else if (fuel_id > 4000 && fuel_id < 5000)
                                    fuel_type = 4000;
                                else if (fuel_id > 3100 && fuel_id < 4000)
                                    fuel_type = 3100;
                                else if (fuel_id > 2200 && fuel_id < 3100)
                                    fuel_type = 2200;
                                else if (fuel_id > 2100 && fuel_id < 2200)
                                    fuel_type = 2100;
                                else if (fuel_id > 1200 && fuel_id < 2100)
                                    fuel_type = 1200;
                                else
                                    fuel_type = 1100;
                                //тут баги
                                SourceList.Add(new SourceTable { Id = Int32.Parse(dr["id"].ToString()), Id_object = Int32.Parse(dr["id_object"].ToString()), Id_fuel = Int32.Parse(dr["id_fuel"].ToString()), Fuel_group = fuel_type, Res_type = Int32.Parse(dr["res_type"].ToString()), Value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return SourceList;
        }


        public static NormTable GetOneNorm(int id_org, int id_rep, int id_norm)
        {
            NormTable Norm = new NormTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id_org = @id_org AND id=@id_norm";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_norm", id_norm);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewNormData] WHERE [id_rep] = @id_rep AND id_norm=@id_norm";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_norm", id_norm);
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                Norm=new NormTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_prod = Int32.Parse(dr["id_prod"].ToString()), Code = Int32.Parse(dr["code"].ToString()), name = dr["name"].ToString(), fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0, type = Int32.Parse(dr["type"].ToString()), val_plan = float.Parse(dr2["value_plan"].ToString()), val_fact = float.Parse(dr2["value_fact"].ToString()) };
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return Norm;
        }

        public static FuelTable GetFuelData(int? fuel_id)
        {
            FuelTable Fuel = new FuelTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewFuels] where fuel_id = @fuel_id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@fuel_id", fuel_id);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Fuel = new FuelTable {fuel_id = Int32.Parse(dr["fuel_id"].ToString()), fuel_group = Int32.Parse(dr["group_id"].ToString()), name = dr["name"].ToString(), Qn=Int32.Parse(dr["Qn"].ToString()), B_y= float.Parse(dr["B_y"].ToString()), unit = dr["unit"].ToString() };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return Fuel;
        }

        public static FactorTable GetFactorData(int type)
        {
            FactorTable Factor = new FactorTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewFactors] where type = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@type", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Factor = new FactorTable { type = Int32.Parse(dr["type"].ToString()), value = float.Parse(dr["value"].ToString()) };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return Factor;
        }

        public static int GetReportId(int id_org, int year, int month)
        {
            int repid = 0;
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT id FROM [NewReport_1Per] WHERE [id_org] = @id_org AND [year] = @year AND [month] = @month";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    repid = Int32.Parse(dr["id"].ToString());
                }
            }
            myConnection.Close();
            return repid;
        }

        public static int GetNormId(int id_org, int id_prod)
        {
            int normid = 0;
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT id FROM [NewNorm] WHERE [id_org] = @id_org AND [id_prod] = @id_prod";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);
            command.Parameters.AddWithValue("@id_prod", id_prod);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    normid = Int32.Parse(dr["id"].ToString());
                }
            }
            myConnection.Close();
            return normid;
        }

    }
}
