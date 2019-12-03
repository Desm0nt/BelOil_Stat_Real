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
using ComponentFactory.Krypton.Toolkit;

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
                KryptonMessageBox.Show("Ошибка проверки наличия пользователя: " + Ex.Message);
            }
            return status;
        }

        /// <summary>
        /// Проверка существования отчета
        /// </summary>
        /// <param name="id_org"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static bool ExistReportCheck(int id_org, int year, int month)
        {
            bool status = false;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewReport_1Per where id_org = @id_org and year = @year and month = @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result == 1)
                    status = true;

                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка проверки наличия отчета: " + Ex.Message);
            }
            return status;
        }

        /// <summary>
        /// Добавление нового отчета
        /// </summary>
        /// <param name="id_org"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="id_user"></param>
        public static void AddNewReport(int id_org, int year, int month, int id_user, int prof_num)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewReport_1Per (id_org, month, year, id_user, prof_num) VALUES (@id_org, @month, @year, @id_user, @prof_num)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@id_user", id_user);
                command.Parameters.AddWithValue("@prof_num", prof_num);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка проверки наличия отчета: " + Ex.Message);
            }
        }

        #region Долучение списков
        public static List<NormIdTypeTable> GetNormIdTypeList(int id_org, int num)
        {
            List<NormIdTypeTable> NormList = new List<NormIdTypeTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT * FROM [NewNorm] where id_org = @id_org and num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        NormList.Add(new NormIdTypeTable { Id = Int32.Parse(dr["id"].ToString()), Name = dr["name"].ToString(), Type = Int32.Parse(dr["type"].ToString())});
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormIdTypeList: " + Ex.Message);
            }
            return NormList;
        }
        public static List<SourceIdTable> GetSoucreIdList(int id_org, int num)
        {
            List<SourceIdTable> SourceList = new List<SourceIdTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT * FROM [NewOrgSoucesList] where id_org = @id_org and num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SourceList.Add(new SourceIdTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_fuel = Int32.Parse(dr["id_fuel"].ToString()), Id_object = Int32.Parse(dr["id_object"].ToString()), Res_type = Int32.Parse(dr["res_type"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetSoucreIdList: " + Ex.Message);
            }
            return SourceList;
        }
        public static List<RecievedIdTable> GetRecievedIdList(int id_owner, int num)
        {
            List<RecievedIdTable> SourceList = new List<RecievedIdTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT * FROM [NewRecievedOrgList] where id_owner = @id_owner and num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@num", num);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SourceList.Add(new RecievedIdTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_owner = Int32.Parse(dr["id_owner"].ToString()), Res_type = Int32.Parse(dr["res_type"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetRecievedIdList: " + Ex.Message);
            }
            return SourceList;
        }
        public static List<SendedIdTable> GetSendedIdList(int id_owner, int num)
        {
            List<SendedIdTable> SourceList = new List<SendedIdTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT * FROM [NewSendedOrgList] where id_owner = @id_owner and num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@num", num);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SourceList.Add(new SendedIdTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_owner = Int32.Parse(dr["id_owner"].ToString()), Res_type = Int32.Parse(dr["res_type"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetSendedIdList: " + Ex.Message);
            }
            return SourceList;
        }
        #endregion

        public static List<int> GetFuelsTradeId(int id_org, int trade)
        {
            List<int> trades = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgFuels] where id_org = @id_org and trade = @trade";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@trade", trade);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        trades.Add(Int32.Parse(dr["id"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetFuelsTradeId:  " + Ex.Message);
            }
            return trades;
        }
        public static void AddFuelTrades(int id_fuel, int id_org, int id_rep, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewFuelsTrade (id_fuel, id_org, id_rep, value) VALUES (@id_fuel, @id_org, @id_rep, @value)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_fuel", id_fuel);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка в AddFuelTrades: " + Ex.Message);
            }
        }

        #region Обновление данных
        public static void UpdateFuelTrades(int id, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewFuelsTrade SET value = @value WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateFuelTrades: " + Ex.Message);
            }
        }
        public static void UpdateFuelNorm(int id, float val_plan, float val_fact)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewNormData SET value_plan = @val_plan, value_fact = @val_fact WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@val_plan", val_plan);
                command.Parameters.AddWithValue("@val_fact", val_fact);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateFuelNorm: " + Ex.Message);
            }
        }
        public static void UpdateSource(int id, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewOrgSouces SET value = @value WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateSource: " + Ex.Message);
            }
        }
        public static void UpdateRecieved(int id, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewRecievedOrg SET value = @value WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateRecieved: " + Ex.Message);
            }
        }
        public static void UpdateSended(int id, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewSendedOrg SET value = @value WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateSended: " + Ex.Message);
            }
        }
        #endregion

        public static void AddNormData(int id_norm, int id_rep, float value_plan, float value_fact)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewNormData (id_norm, id_rep, value_plan, value_fact) VALUES (@id_norm, @id_rep, @value_plan, @value_fact)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_norm", id_norm);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@value_plan", value_plan);
                command.Parameters.AddWithValue("@value_fact", value_fact);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNormData: " + Ex.Message);
            }
        }
        public static void AddSource(int id_src, int id_org, int id_rep, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewOrgSouces (id_src, id_org, id_rep, value) VALUES (@id_src, @id_org, @id_rep, @value)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_src", id_src);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddSource: " + Ex.Message);
            }
        }
        public static void AddRecieved(int id_recieved, int id_org, int id_rep, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewRecievedOrg (id_recieved, id_org, id_rep, value) VALUES (@id_recieved, @id_org, @id_rep, @value)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_recieved", id_recieved);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddRecieved: " + Ex.Message);
            }
        }
        public static void AddSended(int id_sended, int id_org, int id_rep, float value)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewSendedOrg (id_sended, id_org, id_rep, value) VALUES (@id_sended, @id_org, @id_rep, @value)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_sended", id_sended);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@value", value);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddSended: " + Ex.Message);
            }
        }
        public static string GetFuelNameById(int fuel_id, int year, int month)
        {
            string name = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT COUNT(*)  FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@fuel_id", fuel_id);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);

                int RecordExist = (int)command.ExecuteScalar();
                if (RecordExist > 0)
                {
                    query = "SELECT * FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                else
                {
                    query = "SELECT * FROM [NewFuels] WHERE fuel_id = @fuel_id AND time_id = (SELECT MAX(time_id) FROM [NewFuels])";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
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
                KryptonMessageBox.Show("Ошибка GetFuelNameById: " + Ex.Message);
            }
            return name;
        }

        public static bool Exist4NormQuaterCheck(int id_org, int year, int quater)
        {
            bool status = false;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewRepNormData where id_org = @id_org and year = @year and quater = @quater";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@quater", quater);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result > 0)
                    status = true;

                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Exist4NormQuaterCheck: " + Ex.Message);
            }
            return status;
        }
        public static void Add4Norm(int year, int quater, int id_org, int id_prod, long id_local, int id_norm, float value, float volume, float norm, int fuel, string fuelname, int id_obj)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewRepNormData (year, quater, id_org, id_prod, id_local, id_norm, value, volume, norm, fuel, fuelname, id_obj) VALUES (@year, @quater, @id_org, @id_prod, @id_local, @id_norm, @value, @volume, @norm, @fuel, @fuelname, @id_obj)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@quater", quater);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_prod", id_prod);
                command.Parameters.AddWithValue("@id_local", id_local);
                command.Parameters.AddWithValue("@id_norm", id_norm);
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@norm", norm);
                command.Parameters.AddWithValue("@fuel", fuel);
                command.Parameters.AddWithValue("@fuelname", fuelname);
                command.Parameters.AddWithValue("@id_obj", id_obj);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Add4Norm: " + Ex.Message);
            }
        }
        public static void Add4Norm(int year, int quater, int id_org, int id_prod, long id_local, int id_norm, float value, float volume, float norm, int id_obj)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewRepNormData (year, quater, id_org, id_prod, id_local, id_norm, value, volume, norm, id_obj) VALUES (@year, @quater, @id_org, @id_prod, @id_local, @id_norm, @value, @volume, @norm, @id_obj)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@quater", quater);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_prod", id_prod);
                command.Parameters.AddWithValue("@id_local", id_local);
                command.Parameters.AddWithValue("@id_norm", id_norm);
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@volume", volume);
                command.Parameters.AddWithValue("@norm", norm);
                command.Parameters.AddWithValue("@id_obj", id_obj);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Add4Norm: " + Ex.Message);
            }
        }

        public static void Update4Norm_norm(int id, float norm)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewRepNormData SET norm = @norm WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@norm", norm);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Update4Norm_norm: " + Ex.Message);
            }
        }
        public static void Update4Norm_value_volume(int id, float value, float volume)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewRepNormData SET value = @value, volume = @volume WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@value", value);
                command.Parameters.AddWithValue("@volume", volume);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Update4Norm_value_volume: " + Ex.Message);
            }
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
                KryptonMessageBox.Show("Ошибка логина: " + Ex.Message);
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
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return name;
        }
        public static int GetCompanyHead(int id_org)
        {
            int head = 0 ;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT head FROM [NewOrg] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id_org);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        head = Int32.Parse(dr["head"].ToString());
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetCompanyHead: " + Ex.Message);
            }
            return head;
        }

        public static List<CompanyListTable> GetCompanyList()
        {
            List<CompanyListTable> CompanyList = new List<CompanyListTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrg] where id > 100 and id < 300 and id != 200";
                SqlCommand command = new SqlCommand(query, myConnection);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CompanyList.Add(new CompanyListTable { Id = Int32.Parse(dr["id"].ToString()), Name = dr["name"].ToString() });

                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return CompanyList;
        }
        public static List<CompanyStructListTable> GetStructCompanyList(int pid)
        {
            List<CompanyStructListTable> CompanyList = new List<CompanyStructListTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrg] where pid = @pid";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@pid", pid);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CompanyList.Add(new CompanyStructListTable { Id = Int32.Parse(dr["id"].ToString()), Name = dr["name"].ToString(), Pid = Int32.Parse(dr["pid"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetStructCompanyList: " + Ex.Message);
            }
            return CompanyList;
        }
        public static List<int> GetCompanyIdList(int pid)
        {
            List<int> CompanyIdList = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrg] where pid = @pid";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@pid", pid);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CompanyIdList.Add(Int32.Parse(dr["id"].ToString()));

                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return CompanyIdList;
        }

        #region main

        /// <summary>
        /// продукты справочник
        /// </summary>
        /// <param name="id_prod"></param>
        /// <returns></returns>
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
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return unit;
        }
        public static List<ProductTable> GetProdList(string sample1, string sample2)
        {
            List<ProductTable> productList = new List<ProductTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewProduct] where pid > 0 and pid < 4 AND (name like @sample1 and code like @sample2)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(new ProductTable
                        { Id = Int32.Parse(dr["id"].ToString()) , Code = Int32.Parse(dr["code"].ToString()), Name = dr["name"].ToString(),
                            Unit = dr["unit"].ToString(), nUnit = dr["norm_unit"].ToString(), s111 = Boolean.Parse(dr["f111"].ToString()), s112 = Boolean.Parse(dr["f112"].ToString()),
                         type = Int32.Parse(dr["pid"].ToString())});
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdList: " + Ex.Message);
            }
            return productList;
        }
        public static List<ProductTable> GetTypedProdList(string sample1, string sample2, int type)
        {
            List<ProductTable> productList = new List<ProductTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewProduct] where pid = @pid AND (name like @sample1 and code like @sample2)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");
                command.Parameters.AddWithValue("@pid", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(new ProductTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Code = Int32.Parse(dr["code"].ToString()),
                            Name = dr["name"].ToString(),
                            Unit = dr["unit"].ToString(),
                            nUnit = dr["norm_unit"].ToString(),
                            s111 = Boolean.Parse(dr["f111"].ToString()),
                            s112 = Boolean.Parse(dr["f112"].ToString()),
                            type = Int32.Parse(dr["pid"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdList: " + Ex.Message);
            }
            return productList;
        }

        public static List<int> GetProdIdList(int type)
        {
            List<int> productList = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT id FROM [NewProduct] where pid = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@type", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(Int32.Parse(dr["id"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdIdList: " + Ex.Message);
            }
            return productList;
        }
        public static List<int> GetProdCodeList(int type)
        {
            List<int> productList = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT code FROM [NewProduct] where pid = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@type", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(Int32.Parse(dr["code"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdCodeList: " + Ex.Message);
            }
            return productList;
        }
        public static void DeleteFromProd(int id_prod)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "DELETE FROM [NewProduct] where id = @id_prod";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_prod", id_prod);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("ОшибкаDeleteFromProd: " + Ex.Message);
            }
        }
        public static void UpdateProdList(ProductTable productTable, int odlId)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewProduct SET id = @id, code=@code, pid=@pid, name=@name, unit=@unit, norm_unit=@norm_unit, f111=@f111, f112=@f112 WHERE id = @oldid";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@oldid", odlId);
                command.Parameters.AddWithValue("@id", productTable.Id);
                command.Parameters.AddWithValue("@code", productTable.Code);
                command.Parameters.AddWithValue("@pid", productTable.type);
                command.Parameters.AddWithValue("@name", productTable.Name);
                command.Parameters.AddWithValue("@unit", productTable.Unit);
                command.Parameters.AddWithValue("@norm_unit", productTable.nUnit);
                command.Parameters.AddWithValue("@f111", productTable.s111);
                command.Parameters.AddWithValue("@f112", productTable.s112);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateProdList: " + Ex.Message);
            }
        }

        /// <summary>
        /// Человеки справочник
        /// </summary>
        /// <returns></returns> 
        public static List<PersonTable> GetPersonList(string sample)
        {
            int hval = 0;
            int impval = 0;
            int orgpid = 0;
            string orgname = "";
            List<PersonTable> personList = new List<PersonTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewPersons] where surname like @sample";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@sample", "%" + sample + "%");

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        SqlConnection myConnection2 = new SqlConnection(cnStr);
                        myConnection2.Open();

                        string query2 = "SELECT * FROM [NewOrg] where id=@id_org";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_org", Int32.Parse(dr["id_org"].ToString()));
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                hval = Int32.Parse(dr2["head"].ToString());
                                impval = Int32.Parse(dr2["implementer"].ToString());
                                orgpid = Int32.Parse(dr2["pid"].ToString());
                                orgname = dr2["name"].ToString();
                            }
                        }
                        myConnection2.Close();
                        personList.Add(new PersonTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Name = dr["name"].ToString(),
                            Surname = dr["surname"].ToString(),
                            Otchestvo = dr["patronymic"].ToString(),
                            Type = GetType(hval, impval, Int32.Parse(dr["id"].ToString())),
                            Post = dr["post"].ToString(),
                            Phone = dr["phone"].ToString(),
                            WPhone = dr["phone_work"].ToString(),
                            Email = dr["email"].ToString(),
                            Head = "ПО \"Белоруснефть\"",
                            Subhead = (orgpid/100),
                            Orgs = orgname,
                            Id_org = Int32.Parse(dr["id_org"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetPersonList: " + Ex.Message);
            }
            return personList;
        }
        public static int GetPersonLastID()
        {
            int lid = 0;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT MAX(id) FROM [NewPersons]";
                SqlCommand command = new SqlCommand(query, myConnection);
                lid = Convert.ToInt32(command.ExecuteScalar()) + 1;
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetPersonLastID: " + Ex.Message);
            }
            return lid;
        }
        public static void DeleteFromPerson(int id_pers)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "DELETE FROM [NewPersons] where id = @id_pers";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_pers", id_pers);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("DeleteFromPerson: " + Ex.Message);
            }
        }
        public static void UpdatePersonList(PersonTable personTable)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewPersons SET id_org=@id_org, name=@name, surname=@surname, patronymic=@patronymic, post=@post, phone=@phone, phone_work=@phone_work, email=@email WHERE id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", personTable.Id);
                command.Parameters.AddWithValue("@name", personTable.Name);
                command.Parameters.AddWithValue("@surname", personTable.Surname);
                command.Parameters.AddWithValue("@patronymic", personTable.Otchestvo);
                command.Parameters.AddWithValue("@post", personTable.Post);
                command.Parameters.AddWithValue("@phone", personTable.Phone);
                command.Parameters.AddWithValue("@phone_work", personTable.WPhone);
                command.Parameters.AddWithValue("@email", personTable.Email);
                command.Parameters.AddWithValue("@id_org", personTable.Id_org);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateProdList: " + Ex.Message);
            }
        }
        public static void AddNewPerson(PersonTable personTable)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "INSERT INTO NewPersons (id, id_org, name, surname, patronymic, post, phone, phone_work, email) VALUES (@id, @id_org, @name, @surname, @patronymic, @post, @phone, @phone_work, @email)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", personTable.Id);
                command.Parameters.AddWithValue("@name", personTable.Name);
                command.Parameters.AddWithValue("@surname", personTable.Surname);
                command.Parameters.AddWithValue("@patronymic", personTable.Otchestvo);
                command.Parameters.AddWithValue("@post", personTable.Post);
                command.Parameters.AddWithValue("@phone", personTable.Phone);
                command.Parameters.AddWithValue("@phone_work", personTable.WPhone);
                command.Parameters.AddWithValue("@email", personTable.Email);
                command.Parameters.AddWithValue("@id_org", personTable.Id_org);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNewPerson: " + Ex.Message);
            }
        }

        /// <summary>
        /// Для выбора руководителя и исполнителя в профиле
        /// </summary>
        public static List<PersonTable> GetPersonList(int id_org)
        {
            int hval = 0;
            int impval = 0;
            int orgpid = 0;
            string orgname = "";
            List<PersonTable> personList = new List<PersonTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewPersons] where id_org like @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        SqlConnection myConnection2 = new SqlConnection(cnStr);
                        myConnection2.Open();

                        string query2 = "SELECT * FROM [NewOrg] where id=@id_org";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_org", Int32.Parse(dr["id_org"].ToString()));
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                hval = Int32.Parse(dr2["head"].ToString());
                                impval = Int32.Parse(dr2["implementer"].ToString());
                                orgpid = Int32.Parse(dr2["pid"].ToString());
                                orgname = dr2["name"].ToString();
                            }
                        }
                        myConnection2.Close();
                        personList.Add(new PersonTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Name = GetPersonFIO(Int32.Parse(dr["id"].ToString())),
                            Surname = dr["surname"].ToString(),
                            Otchestvo = dr["patronymic"].ToString(),
                            Type = GetType(hval, impval, Int32.Parse(dr["id"].ToString())),
                            Post = dr["post"].ToString(),
                            Phone = dr["phone"].ToString(),
                            WPhone = dr["phone_work"].ToString(),
                            Email = dr["email"].ToString(),
                            Head = "ПО \"Белоруснефть\"",
                            Subhead = (orgpid / 100),
                            Orgs = orgname,
                            Id_org = Int32.Parse(dr["id_org"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetPersonList: " + Ex.Message);
            }
            return personList;
        }

        /// <summary>
        /// нормы предприятия профиль
        /// </summary>
        /// <returns></returns>
        public static string GetNormUnit(int id_prod)
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
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return unit;
        }
        public static string GetNormNUnit(int id_prod)
        {
            string nunit = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT norm_unit FROM [NewProduct] where id = @id_prod";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_prod", id_prod);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        nunit = dr["norm_unit"].ToString();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormNUnit: " + Ex.Message);
            }
            return nunit;
        }
        public static List<ProfNormTable> GetProfNormList(int id_org, int num, int month, int year, string sample1, string sample2)
        {
            List<ProfNormTable> normtList = new List<ProfNormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewNorm where num = @num AND id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewNorm] where num = @num AND id_org = @id_org AND (name like @sample1 and code like @sample2)";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                    command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);                   
                }
                else
                {
                    query = "SELECT * FROM [NewNorm] where id_org = @id_org AND num = (SELECT MAX(num) FROM [NewNorm] where ((year = @year AND month <= @month) OR year< @year)) AND (name like @sample1 and code like @sample2)";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                    command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);                   
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        bool f111 = false;
                        bool f112 = false;
                        var array = dr["row_options"].ToString().Split(',');
                        if (array.Length == 2)
                        {
                            f111 = true;
                            f112 = true;
                        }
                        else if (array.Length == 1 && array[0] == "111")
                            f111 = true;
                        else if (array.Length == 1 && array[0] == "112")
                            f112 = true;
                        int fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0;
                        string fuel_name = "";
                        if (fuel != 0)
                        {
                            fuel_name = " (" + GetFuelNameById(fuel, DateTime.Now.Year, DateTime.Now.Month) + ")";
                        }
                        normtList.Add(new ProfNormTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                            Code = Int32.Parse(dr["code"].ToString()),
                            Name = dr["name"].ToString(),
                            Unit = GetNormUnit(Int32.Parse(dr["id_prod"].ToString())),
                            nUnit = GetNormNUnit(Int32.Parse(dr["id_prod"].ToString())),
                            id_obj = Int32.Parse(dr["id_obj"].ToString()),
                            s111 = f111,
                            s112 = f112,
                            type = Int32.Parse(dr["type"].ToString()),
                            Id_local = dr["id_local"].ToString(),
                            real_name = dr["real_name"].ToString(),
                            name_with_fuel = dr["name"].ToString() + fuel_name,
                            id_fuel = fuel
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormList: " + Ex.Message);
            }
            return normtList;
        }
        public static List<ProfNormTable> GetProfNormTypedList(int id_org, int num, int month, int year, string sample1, string sample2, int type)
        {
            List<ProfNormTable> normtList = new List<ProfNormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewNorm where num = @num AND id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewNorm] where num = @num AND id_org = @id_org AND (name like @sample1 and code like @sample2) AND type = @type";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                    command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@type", type);                    
                }
                else
                {
                    query = "SELECT * FROM [NewNorm] where id_org = @id_org AND num = (SELECT MAX(num) FROM [NewNorm] where ((year = @year AND month <= @month) OR year< @year)) AND (name like @sample1 and code like @sample2) AND type = @type";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                    command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                    command.Parameters.AddWithValue("@type", type);              
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        bool f111 = false;
                        bool f112 = false;
                        var array = dr["row_options"].ToString().Split(',');
                        if (array.Length == 2)
                        {
                            f111 = true;
                            f112 = true;
                        }
                        else if (array.Length == 1 && array[0] == "111")
                            f111 = true;
                        else if (array.Length == 1 && array[0] == "112")
                            f112 = true;
                        int fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0;
                        string fuel_name = "";
                        if (fuel != 0)
                        {
                            fuel_name =" (" + GetFuelNameById(fuel, DateTime.Now.Year, DateTime.Now.Month) +")";
                        }
                        normtList.Add(new ProfNormTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                            Code = Int32.Parse(dr["code"].ToString()),
                            Name = dr["name"].ToString(),
                            Unit = GetNormUnit(Int32.Parse(dr["id_prod"].ToString())),
                            nUnit = GetNormNUnit(Int32.Parse(dr["id_prod"].ToString())),
                            id_obj = Int32.Parse(dr["id_obj"].ToString()),
                            s111 = f111,
                            s112 = f112,
                            type = Int32.Parse(dr["type"].ToString()),
                            Id_local = dr["id_local"].ToString(),
                            real_name = dr["real_name"].ToString(),
                            name_with_fuel = dr["name"].ToString() + fuel_name,
                            id_fuel = fuel
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormList: " + Ex.Message);
            }
            return normtList;
        }
        public static FuelsTable GetFuelById(int fuel_id, int year, int month)
        {
            FuelsTable fuel = new FuelsTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT COUNT(*)  FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@fuel_id", fuel_id);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);

                int RecordExist = (int)command.ExecuteScalar();
                if (RecordExist > 0)
                {
                    query = "SELECT * FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                else
                {
                    query = "SELECT * FROM [NewFuels] WHERE fuel_id = @fuel_id AND time_id = (SELECT MAX(time_id) FROM [NewFuels])";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        fuel = new FuelsTable
                        {
                            id = Int32.Parse(dr["id"].ToString()),
                            fuel_id = Int32.Parse(dr["fuel_id"].ToString()),
                            name = dr["name"].ToString(),
                            Qn = Int32.Parse(dr["Qn"].ToString()),
                            B_y = float.Parse(dr["B_y"].ToString()),
                            unit = dr["unit"].ToString()
                        };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetFuelById: " + Ex.Message);
            }
            return fuel;
        }
        public static List<ProfFuelsTable> GetProfFuelsList(int id_org, int num, int month, int year)
        {
            List<ProfFuelsTable> fuelList = new List<ProfFuelsTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewOrgFuels where num = @num AND id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewOrgFuels] where num = @num AND id_org = @id_org";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);  
                }
                else
                {
                    query = "SELECT * FROM [NewOrgFuels] where id_org = @id_org AND num = (SELECT MAX(num) FROM [NewOrgFuels] where ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);                 
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var fuel = GetFuelById(Int32.Parse(dr["id_fuel"].ToString()), year, month);
                        fuelList.Add(new ProfFuelsTable
                        {
                            id = Int32.Parse(dr["id"].ToString()),
                            fuel_id = Int32.Parse(dr["id_fuel"].ToString()),
                            name = fuel.name,
                            Qn = fuel.Qn,
                            B_y = fuel.B_y,
                            unit = fuel.unit,
                            trade = Boolean.Parse(dr["trade"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfFuelsList: " + Ex.Message);
            }
            return fuelList;
        }
        public static List<ProfFactorTable> GetProfFactorList()
        {
            List<ProfFactorTable> Factor = new List<ProfFactorTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM NewFactors";
                SqlCommand command = new SqlCommand(query, myConnection);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Factor.Add(new ProfFactorTable { id = Int32.Parse(dr["id"].ToString()), month = Int32.Parse(dr["month"].ToString()), year = Int32.Parse(dr["year"].ToString()), gkal = float.Parse(dr["gkal"].ToString()), kVt = float.Parse(dr["kvch"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfFactorList: " + Ex.Message);
            }
            return Factor;
        }
        public static List<ProfGenTable> GetProfGenList(int id_org, int num, int month, int year)
        {
            List<ProfGenTable> GenList = new List<ProfGenTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewOrgSoucesList where num = @num AND id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewOrgSoucesList] where num = @num AND id_org = @id_org";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);
                }
                else
                {
                    query = "SELECT * FROM [NewOrgSoucesList] where id_org = @id_org AND num = (SELECT MAX(num) FROM [NewNorm] where ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        GenList.Add(new ProfGenTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Obj_id = Int32.Parse(dr["id_object"].ToString()),
                            Obj_name = GetObjName(Int32.Parse(dr["id_object"].ToString())),
                            Fuel_id = Int32.Parse(dr["id_fuel"].ToString()),
                            Fuel_name = GetFuelNameById(Int32.Parse(dr["id_fuel"].ToString()), DateTime.Now.Year, DateTime.Now.Month),
                            Type = Int32.Parse(dr["res_type"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfGenList: " + Ex.Message);
            }
            return GenList;
        }
        public static List<ProfSendRecTable> GetProfSendList(int id_org, int num, int month, int year)
        {
            List<ProfSendRecTable> GenList = new List<ProfSendRecTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewSendedOrgList where num = @num AND id_owner = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewSendedOrgList] where num = @num AND id_owner = @id_org";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);
                }
                else
                {
                    query = "SELECT * FROM [NewSendedOrgList] where id_owner = @id_org AND num = (SELECT MAX(num) FROM [NewNorm] where ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        GenList.Add(new ProfSendRecTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())),
                            Id_org = Int32.Parse(dr["id_org"].ToString()),
                            Head = GetCompanyHead(Int32.Parse(dr["id_org"].ToString())),
                            Type = Int32.Parse(dr["res_type"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfSendRecList: " + Ex.Message);
            }
            return GenList;
        }
        public static List<ProfSendRecTable> GetProfRecList(int id_org, int num, int month, int year)
        {
            List<ProfSendRecTable> GenList = new List<ProfSendRecTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM NewRecievedOrgList where num = @num AND id_owner = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_org", id_org);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM [NewRecievedOrgList] where num = @num AND id_owner = @id_org";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@num", num);
                    command.Parameters.AddWithValue("@id_org", id_org);
                }
                else
                {
                    query = "SELECT * FROM [NewRecievedOrgList] where id_owner = @id_org AND num = (SELECT MAX(num) FROM [NewNorm] where ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        GenList.Add(new ProfSendRecTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())),
                            Id_org = Int32.Parse(dr["id_org"].ToString()),
                            Head = GetCompanyHead(Int32.Parse(dr["id_org"].ToString())),
                            Type = Int32.Parse(dr["res_type"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfSendRecList: " + Ex.Message);
            }
            return GenList;
        }
        public static string GetPersonFIO(int id)
        {
            string FIO = "";
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewPersons] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var name = dr["name"].ToString();
                        var second_name = dr["patronymic"].ToString();
                        FIO = name.ToArray()[0] + "." + second_name.ToArray()[0] +". " + dr["surname"].ToString();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetPersonFIO: " + Ex.Message);
            }
            return FIO;
        }
        public static ProfCompTable GetProfCompList(int id)
        {
            ProfCompTable CompanyList = new ProfCompTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrg] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var head = "?.?.?";
                        var imp = "?.?.?";
                        bool dochern = false;
                        if (Int32.Parse(dr["head"].ToString()) != -1 && Int32.Parse(dr["head"].ToString()) != 0)
                            head = GetPersonFIO(Int32.Parse(dr["head"].ToString()));
                        if (Int32.Parse(dr["implementer"].ToString()) != -1 && Int32.Parse(dr["implementer"].ToString()) != 0)
                            imp = GetPersonFIO(Int32.Parse(dr["implementer"].ToString()));
                        if (Int32.Parse(dr["pid"].ToString()) == 200)
                            dochern = true;
                        CompanyList = new ProfCompTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Name = dr["name"].ToString(),
                            Head = head,
                            Impl = imp,
                            doch = dochern
                        };

                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfCompList: " + Ex.Message);
            }
            return CompanyList;
        }

        public static List<int> GetNormIdList(int type)
        {
            List<int> productList = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT id FROM [NewProduct] where pid = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@type", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(Int32.Parse(dr["id"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdIdList: " + Ex.Message);
            }
            return productList;
        }
        public static List<int> GetNormCodeList(int type)
        {
            List<int> productList = new List<int>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT code FROM [NewProduct] where pid = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@type", type);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        productList.Add(Int32.Parse(dr["code"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProdCodeList: " + Ex.Message);
            }
            return productList;
        }
        public static void DeleteFromNorm(int id_prod)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "DELETE FROM [NewProduct] where id = @id_prod";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_prod", id_prod);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("ОшибкаDeleteFromProd: " + Ex.Message);
            }
        }
        public static void UpdateNormList(ProductTable productTable, int odlId)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "UPDATE NewProduct SET id = @id, code=@code, pid=@pid, name=@name, unit=@unit, norm_unit=@norm_unit, f111=@f111, f112=@f112 WHERE id = @oldid";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@oldid", odlId);
                command.Parameters.AddWithValue("@id", productTable.Id);
                command.Parameters.AddWithValue("@code", productTable.Code);
                command.Parameters.AddWithValue("@pid", productTable.type);
                command.Parameters.AddWithValue("@name", productTable.Name);
                command.Parameters.AddWithValue("@unit", productTable.Unit);
                command.Parameters.AddWithValue("@norm_unit", productTable.nUnit);
                command.Parameters.AddWithValue("@f111", productTable.s111);
                command.Parameters.AddWithValue("@f112", productTable.s112);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateProdList: " + Ex.Message);
            }
        }
        public static void UpdateOrgPerson(int id_org, int id_person, bool person_type)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "";
                if (person_type)
                    query = "UPDATE NewOrg SET head = @id_person WHERE id = @id_org";
                else
                    query = "UPDATE NewOrg SET implementer = @id_person WHERE id = @id_org";

                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_person", id_person);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка UpdateOrgPerson: " + Ex.Message);
            }
        }
        public static void AddNewObject(int id_org, string name, string full_name)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewObjects (id_org, name, full_name) VALUES (@id_org, @name, @full_name)"; //пишем данные параметрическим запросом
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@name", name);
                command2.Parameters.AddWithValue("@full_name", full_name);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка AddNewObject: " + Ex.Message);
            }
        }
        public static void UpdateObject(int id_obj, int id_org, string name, string full_name)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "UPDATE NewObjects SET id_org = @id_org, name = @name, full_name = @full_name WHERE id = @id_obj"; //пишем данные параметрическим запросом
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@name", name);
                command2.Parameters.AddWithValue("@full_name", full_name);
                command2.Parameters.AddWithValue("@id_obj", id_obj);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка UpdateObject: " + Ex.Message);
            }
        }
        public static void AddNewFactor(int month, int year, float gkal, float kvch)
        {
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT COUNT(*) FROM NewFactors where month = @month AND year = @year";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@year", year);

            var result = Convert.ToInt32(command.ExecuteScalar());
            myConnection.Close();
            if (result < 1)
            {
                try
                {
                    SqlConnection myConnection2 = new SqlConnection(cnStr);
                    myConnection2.Open();
                    string query2 = "INSERT INTO NewFactors (month, year, gkal, kvch) VALUES (@month, @year, @gkal, @kvch)"; //пишем данные параметрическим запросом
                    SqlCommand command2 = new SqlCommand(query2, myConnection2);
                    command2.Parameters.AddWithValue("@month", month);
                    command2.Parameters.AddWithValue("@year", year);
                    command2.Parameters.AddWithValue("@gkal", gkal);
                    command2.Parameters.AddWithValue("@kvch", kvch);
                    command2.ExecuteNonQuery();
                    myConnection2.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Ошибка AddNewFactor: " + Ex.Message);
                }
            }
            else
                MessageBox.Show("Ошибка: Коэффициенты за данный период уже добавлены!");
        }
        public static void UpdateFactor(float gkal, float kvch, int id)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "UPDATE NewFactors SET gkal = ROUND(@gkal, 3), kvch = ROUND(@kvch, 3)  WHERE id = @id"; //пишем данные параметрическим запросом
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@gkal", gkal);
                command2.Parameters.AddWithValue("@kvch", kvch);
                command2.Parameters.AddWithValue("@id", id);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Ошибка UpdateFactor: " + Ex.Message);
            }
        }
        public static void DeleteFactor(int id)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "DELETE FROM [NewFactors] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка DeleteFactor: " + Ex.Message);
            }
        }

        /// <summary>
        /// Топливо справочник
        /// </summary>
        /// <returns></returns> 
        public static List<FuelsTable> GetFuelList(string sample1, string sample2)
        {
            List<FuelsTable> fuelList = new List<FuelsTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewFuels] WHERE time_id = (SELECT MAX(time_id) FROM [NewFuels])  AND (name like @sample1 and fuel_id like @sample2)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@sample1", "%" + sample1 + "%");
                command.Parameters.AddWithValue("@sample2", "%" + sample2 + "%");

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        fuelList.Add(new FuelsTable
                        {
                            id = Int32.Parse(dr["id"].ToString()),
                            fuel_id = Int32.Parse(dr["fuel_id"].ToString()),
                            name = dr["name"].ToString(),
                            Qn = Int32.Parse(dr["Qn"].ToString()),
                            B_y = float.Parse(dr["B_y"].ToString()),
                            unit = dr["unit"].ToString()
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetFuelList: " + Ex.Message);
            }
            return fuelList;
        }
  
        public static void DeleteFromFuel(int id)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "DELETE FROM [NewFuels] where id = @id";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("DeleteFromFuel: " + Ex.Message);
            }
        }

        private static int GetType(int hv, int imp, int id)
        {
            int type = 0;
            if (id == hv)
                type = 1;
            else if (id == imp)
                type = 2;
            else
                type = 0;
            return type;
        }

        public static List<TradeTable> GetTrades(int id_rep)
        {
            List<TradeTable> trades = new List<TradeTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewTrade] where id_rep = @id_rep";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_rep", id_rep);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        trades.Add(new TradeTable { type = Int32.Parse(dr["type"].ToString()), value = float.Parse(dr["value"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return trades;
        }

        public static List<NormTable> GetNormListPR(int id_org, int id_rep, int num, int month, int year)
        {
            List<NormTable> NormList = new List<NormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "exec dbo.GetNormListPR @id_org, @id_rep, @num, @month, @year";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] rowopt = !String.IsNullOrWhiteSpace(dr["row_options"].ToString()) ? dr["row_options"].ToString().Split(',') : new string[] { };
                        float coeff = 0;
                        int fuel = 0;
                        string fname = "";
                        switch (Int32.Parse(dr["type"].ToString()))
                        {
                            case 1:
                                coeff = float.Parse(dr["B_y"].ToString());
                                fname = dr["fuel_name"].ToString();
                                fuel = Int32.Parse(dr["fuel"].ToString());
                                break;
                            case 2:
                                coeff = float.Parse(dr["gkal"].ToString());
                                break;
                            case 3:
                                coeff = float.Parse(dr["kvch"].ToString());
                                break;
                            default:
                                ;
                                break;
                        }
                        NormList.Add(new NormTable
                        {
                            Id = Int32.Parse(dr["norm_id"].ToString()),
                            Id_org = Int32.Parse(dr["id_org"].ToString()),
                            Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                            Code = Int32.Parse(dr["code"].ToString()),
                            name = dr["name"].ToString(),
                            fuel = fuel,
                            fuel_name = " (" + fname + ")",
                            type = Int32.Parse(dr["type"].ToString()),
                            row_options = rowopt,
                            Unit = GetNormUnit(Int32.Parse(dr["id_prod"].ToString())),
                            nUnit = GetNormNUnit(Int32.Parse(dr["id_prod"].ToString())),
                            val_plan = float.Parse(dr["value_plan"].ToString()),
                            val_fact = float.Parse(dr["value_fact"].ToString()),
                            val_fact_ut = float.Parse(dr["value_fact"].ToString()) * coeff,
                            val_plan_ut = float.Parse(dr["value_fact"].ToString()) * coeff,
                            norm_code = Int32.Parse(dr["norm_code"].ToString()),
                            Id_local = Int64.Parse(dr["id_local"].ToString()),
                            editable = false,
                            id_obj = Int32.Parse(dr["id_obj"].ToString())

                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormListPR: " + Ex.Message);
            }
            return NormList;
        }

        public static List<NormTable> GetNormInputList(int id_org, int id_rep, int num, int type, int year, int month)
        {
            List<NormTable> NormList = new List<NormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "exec dbo.GetTypedNormListPR @id_org, @id_rep, @num, @month, @year, @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.Parameters.AddWithValue("@id_rep", id_rep);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@type", type);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        string[] rowopt = !String.IsNullOrWhiteSpace(dr["row_options"].ToString()) ? dr["row_options"].ToString().Split(',') : new string[] { };
                        float coeff = 0;
                        int fuel = 0;
                        string fname = "";
                        switch (Int32.Parse(dr["type"].ToString()))
                        {
                            case 1:
                                coeff = float.Parse(dr["B_y"].ToString());
                                fname = dr["fuel_name"].ToString();
                                fuel = Int32.Parse(dr["fuel"].ToString());
                                break;
                            case 2:
                                coeff = float.Parse(dr["gkal"].ToString());
                                break;
                            case 3:
                                coeff = float.Parse(dr["kvch"].ToString());
                                break;
                            default:
                                ;
                                break;
                        }
                        NormList.Add(new NormTable
                        {
                            Id = Int32.Parse(dr["norm_id"].ToString()),
                            Id_org = Int32.Parse(dr["id_org"].ToString()),
                            Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                            Code = Int32.Parse(dr["code"].ToString()),
                            name = dr["name"].ToString(),
                            fuel = fuel,
                            fuel_name = " (" + fname + ")",
                            type = Int32.Parse(dr["type"].ToString()),
                            row_options = rowopt,
                            Unit = GetNormUnit(Int32.Parse(dr["id_prod"].ToString())),
                            nUnit = GetNormNUnit(Int32.Parse(dr["id_prod"].ToString())),
                            val_plan = float.Parse(dr["value_plan"].ToString()),
                            val_fact = float.Parse(dr["value_fact"].ToString()),
                            val_fact_ut = float.Parse(dr["value_fact"].ToString()) * coeff,
                            val_plan_ut = float.Parse(dr["value_fact"].ToString()) * coeff,
                            norm_code = Int32.Parse(dr["norm_code"].ToString()),
                            Id_local = Int64.Parse(dr["id_local"].ToString()),
                            editable = false,
                            id_obj = Int32.Parse(dr["id_obj"].ToString())

                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormInputList: " + Ex.Message);
            }
            return NormList;
        }

        public static List<NormTable> GetNormInputListOld(int id_org, int id_rep, int type, int year, int month)
        {
            List<NormTable> NormList = new List<NormTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id_org = @id_org and type = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@type", type);
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
                                string[] rowopt = !String.IsNullOrWhiteSpace(dr["row_options"].ToString()) ? dr["row_options"].ToString().Split(',') : new string[] { };
                                if (!String.IsNullOrWhiteSpace(dr["fuel"].ToString()))
                                {
                                    var name = dbOps.GetFuelNameById(Int32.Parse(dr["fuel"].ToString()), year, month);
                                    //NormList.Add(new NormInputTable { Id = Int32.Parse(dr2["id"].ToString()), name = dr["name"].ToString() + " (" + name + ")", val_plan = float.Parse(dr2["value_plan"].ToString()), val_fact = float.Parse(dr2["value_fact"].ToString()) });
                                    NormList.Add(new NormTable
                                    {
                                        Id = Int32.Parse(dr2["id"].ToString()),
                                        Id_org = Int32.Parse(dr["id_org"].ToString()),
                                        Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                                        Code = Int32.Parse(dr["code"].ToString()),
                                        name = dr["name"].ToString() + " (" + name + ")",
                                        fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0,
                                        type = Int32.Parse(dr["type"].ToString()),
                                        row_options = rowopt,
                                        val_plan = float.Parse(dr2["value_plan"].ToString()),
                                        val_fact = float.Parse(dr2["value_fact"].ToString()),
                                        val_fact_ut = 0,
                                        val_plan_ut = 0
                                    });
                                }
                                else
                                    NormList.Add(new NormTable
                                    {
                                        Id = Int32.Parse(dr2["id"].ToString()),
                                        Id_org = Int32.Parse(dr["id_org"].ToString()),
                                        Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                                        Code = Int32.Parse(dr["code"].ToString()),
                                        name = dr["name"].ToString(),
                                        fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0,
                                        type = Int32.Parse(dr["type"].ToString()),
                                        row_options = rowopt,
                                        val_plan = float.Parse(dr2["value_plan"].ToString()),
                                        val_fact = float.Parse(dr2["value_fact"].ToString()),
                                        val_fact_ut = 0,
                                        val_plan_ut = 0
                                    });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetNormInputList: " + Ex.Message);
            }
            return NormList;
        }

        public static List<RecievedTable> GetRecievedList(int id_owner, int id_rep, int num)
        {
            List<RecievedTable> RecievedList = new List<RecievedTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewRecievedOrgList] where id_owner = @id_owner AND num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@num", num);
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
                                RecievedList.Add(new RecievedTable
                                {
                                    Id = Int32.Parse(dr["id"].ToString()),
                                    Id_owner = Int32.Parse(dr["id_owner"].ToString()),
                                    Id_org = Int32.Parse(dr["id_org"].ToString()),
                                    org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())),
                                    res_type = Int32.Parse(dr["res_type"].ToString()),
                                    value = float.Parse(dr2["value"].ToString())
                                });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return RecievedList;
        }
        public static List<RecievedInputTable> GetRecievedInputList(int id_owner, int id_rep, int res_type)
        {
            List<RecievedInputTable> RecievedList = new List<RecievedInputTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewRecievedOrgList] where id_owner = @id_owner and res_type= @res_type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@res_type", res_type);
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
                                RecievedList.Add(new RecievedInputTable { Id = Int32.Parse(dr2["id"].ToString()), org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())), value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetRecievedInputList: " + Ex.Message);
            }
            return RecievedList;
        }

        public static List<SendedTable> GetSendedList(int id_owner, int id_rep, int num)
        {
            List<SendedTable> SendedList = new List<SendedTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewSendedOrgList] where id_owner = @id_owner AND num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@num", num);
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
                                SendedList.Add(new SendedTable
                                {
                                    Id = Int32.Parse(dr["id"].ToString()),
                                    Id_owner = Int32.Parse(dr["id_owner"].ToString()),
                                    Id_org = Int32.Parse(dr["id_org"].ToString()),
                                    org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())),
                                    res_type = Int32.Parse(dr["res_type"].ToString()),
                                    value = float.Parse(dr2["value"].ToString())
                                });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return SendedList;
        }
        public static List<SendedInputTable> GetSendedInputList(int id_owner, int id_rep, int res_type)
        {
            List<SendedInputTable> SendedList = new List<SendedInputTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewSendedOrgList] where id_owner = @id_owner and res_type = @res_type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_owner", id_owner);
                command.Parameters.AddWithValue("@res_type", res_type);
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
                                SendedList.Add(new SendedInputTable { Id = Int32.Parse(dr["id"].ToString()), org_name = GetCompanyName(Int32.Parse(dr["id_org"].ToString())), value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetSendedInputList: " + Ex.Message);
            }
            return SendedList;
        }

        public static List<SourceTable> GetSourceList(int id_org, int id_rep, int num)
        {
            List<SourceTable> SourceList = new List<SourceTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgSoucesList] where id_org = @id_org AND num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
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
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return SourceList;
        }
        public static List<SourceInputTable> GetSourceInputList(int id_org, int id_rep, int res_type)
        {
            List<SourceInputTable> SourceList = new List<SourceInputTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgSoucesList] where id_org = @id_org and res_type=@res_type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@res_type", res_type);
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
                                //тут баги
                                SourceList.Add(new SourceInputTable { Id = Int32.Parse(dr2["id"].ToString()), ObjName = GetObjName(Int32.Parse(dr["id_object"].ToString())), Value = float.Parse(dr2["value"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetSourceInputList: " + Ex.Message);
            }
            return SourceList;
        }

        public static List<FTradeTable> GetFTradeList(int id_org, int id_rep, int num)
        {
            List<FTradeTable> FTradeList = new List<FTradeTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);

                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgFuels] where id_org = @id_org and trade = @trade";
                bool trade = true;
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@trade", trade);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewFuelsTrade] WHERE [id_fuel] = @id_tfuel AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_tfuel", Int32.Parse(dr["fuel_code"].ToString()));
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
                                FTradeList.Add(new FTradeTable
                                {
                                    Id = Int32.Parse(dr["id"].ToString()),
                                    Id_fuel = Int32.Parse(dr["id_fuel"].ToString()),
                                    Fuel_group = fuel_type,
                                    Id_org = Int32.Parse(dr["id_org"].ToString()),
                                    Value = float.Parse(dr2["value"].ToString()),
                                    fuel_code = Int32.Parse(dr["fuel_code"].ToString())
                                });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных торговли топливом: " + Ex.Message);
            }
            return FTradeList;
        }
        public static List<FTradeInputTable> GetFTradeInputList(int id_org, int id_rep, int year, int month)
        {
            List<FTradeInputTable> FTradeList = new List<FTradeInputTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewOrgFuels] where id_org = @id_org and trade = @trade";
                bool trade = true;
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@trade", trade);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewFuelsTrade] WHERE [id_fuel] = @id_tfuel AND [id_rep] = @id_rep";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_tfuel", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@id_rep", id_rep);
                        var a = Int32.Parse(dr["id"].ToString());
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                //тут баги
                                var Fuel = dbOps.GetFuelData(Int32.Parse(dr["id_fuel"].ToString()), year, month);
                                var Fsum = MakeTInputFuelSum(year, month, Int32.Parse(dr["id"].ToString()));
                                FTradeList.Add(new FTradeInputTable { Id = Int32.Parse(dr2["id"].ToString()), Id_fuel = Int32.Parse(dr["id"].ToString()), Fuel_name = dbOps.GetFuelNameById(Int32.Parse(dr["id_fuel"].ToString()), year, month), Value = float.Parse(dr2["value"].ToString()),
                                    Value_tyt = 0f, Value_year_tyt = 0f, B_y = (float)Math.Round(Fuel.B_y, 3), Value_year = Fsum, Value_year_back = Fsum, Ed_izm = Fuel.unit });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetFTradeInputList: " + Ex.Message);
            }
            return FTradeList;
        }
        public static float GetInputMonthFuelValue(int report_id, int fuel_id)
        {
            float val = 0;
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT * FROM [NewFuelsTrade] WHERE [id_fuel] = @fuel_id AND [id_rep] = @report_id";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@fuel_id", fuel_id);
            command.Parameters.AddWithValue("@report_id", report_id);
            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    //тут баги
                    val = float.Parse(dr["value"].ToString());
                }
            }

            return val;
        }
        public static float MakeTInputFuelSum(int year, int month, int fuel_id)
        {
            int report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, 1);
            int profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, 1);
            float yearFVal = 0;

            if (month > 1)
            {
                for (int i = 1; i < month; i++)
                {
                    report_id = dbOps.GetReportId(CurrentData.UserData.Id_org, year, i);
                    profile_num = dbOps.GetProflieNum(CurrentData.UserData.Id_org, year, i);
                    var tmplist = dbOps.GetFTradeList(CurrentData.UserData.Id_org, report_id, profile_num);
                    yearFVal += dbOps.GetInputMonthFuelValue(report_id, fuel_id);
                }
            }
            return yearFVal;
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
                                Norm=new NormTable { Id = Int32.Parse(dr["id"].ToString()), Id_org = Int32.Parse(dr["id_org"].ToString()), Id_prod = Int32.Parse(dr["id_prod"].ToString()), Code = Int32.Parse(dr["code"].ToString()), name = dr["name"].ToString(), fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0, type = Int32.Parse(dr["type"].ToString()), val_plan = float.Parse(dr2["value_plan"].ToString()), val_fact = float.Parse(dr2["value_fact"].ToString()), val_fact_ut = 0, val_plan_ut = 0 };
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return Norm;
        }

        public static FuelTable GetFuelData(int? fuel_id, int year, int month)
        {
            FuelTable Fuel = new FuelTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@fuel_id", fuel_id);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);

                int RecordExist = (int)command.ExecuteScalar();
                if (RecordExist > 0) {
                    query = "SELECT * FROM [NewFuels] where fuel_id = @fuel_id and year = @year and month = @month";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                else
                {
                    query = "SELECT * FROM [NewFuels] WHERE fuel_id = @fuel_id and time_id = (SELECT MAX(time_id) FROM [NewFuels] where fuel_id = @fuel_id and ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@fuel_id", fuel_id);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Fuel = new FuelTable {fuel_id = Int32.Parse(dr["fuel_id"].ToString()), fuel_group = Int32.Parse(dr["group_id"].ToString()), name = dr["name"].ToString(), Qn=Int32.Parse(dr["Qn"].ToString()), B_y= float.Parse(dr["B_y"].ToString()), unit = dr["unit"].ToString(), year = Int32.Parse(dr["year"].ToString()), month = Int32.Parse(dr["month"].ToString()), time_id = Int32.Parse(dr["time_id"].ToString()) };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
            }
            return Fuel;
        }

        public static FactorTable GetFactorData(int type, int month, int year)
        {
            FactorTable Factor = new FactorTable();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
               
                string query = "SELECT COUNT(*) FROM NewFactors where month = @month and year= @year";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT * FROM NewFactors where month = @month and year= @year";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                else
                {
                    query = "SELECT * FROM [NewFactors] where num = (SELECT MAX(num) FROM [NewFactors] where ((year = @year AND month <= @month) OR year< @year))";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        if (type == 2)
                            Factor = new FactorTable { type = 2, value = float.Parse(dr["gkal"].ToString()) };
                        else
                            Factor = new FactorTable { type = 3, value = float.Parse(dr["kvch"].ToString()) };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных организации: " + Ex.Message);
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

        public static ReportDataTable GetReportData(int id_org, int year, int month)
        {
            ReportDataTable rep =  new ReportDataTable();
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT * FROM [NewReport_1Per] WHERE [id_org] = @id_org AND [year] = @year AND [month] = @month";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);
            command.Parameters.AddWithValue("@year", year);
            command.Parameters.AddWithValue("@month", month);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    rep.id = Int32.Parse(dr["id"].ToString());
                    rep.num = Int32.Parse(dr["prof_num"].ToString());
                }
            }
            myConnection.Close();
            return rep;
        }

        public static int GetProflieNum(int id_org, int year, int month)
        {
            int profNum = 0;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT COUNT(*) FROM [NewProfiles] WHERE [id_org] = @id_org AND [year] = @year AND [month] = @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);
                var result = Convert.ToInt32(command.ExecuteScalar());
                if (result >= 1)
                {
                    query = "SELECT num FROM [NewProfiles] WHERE [id_org] = @id_org AND [year] = @year AND [month] = @month";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                }
                else
                {
                    query = "SELECT COUNT(*) FROM [NewProfiles] WHERE [id_org] = @id_org AND ((year = @year AND month <= @month) OR year< @year)";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                    var result2 = Convert.ToInt32(command.ExecuteScalar());

                    if (result2 > 0)
                    {
                        query = "SELECT num FROM [NewProfiles] WHERE [id_org] = @id_org AND num = (SELECT MAX(num) FROM [NewProfiles] WHERE ((year = @year AND month <= @month) OR year< @year) AND [id_org] = @id_org)";
                        command = new SqlCommand(query, myConnection);
                        command.Parameters.AddWithValue("@id_org", id_org);
                        command.Parameters.AddWithValue("@year", year);
                        command.Parameters.AddWithValue("@month", month);
                    }
                    else
                    {
                        query = "SELECT num FROM [NewProfiles] WHERE [id_org] = @id_org AND num = (SELECT MIN(num) FROM [NewProfiles] WHERE [id_org] = @id_org)";
                        command = new SqlCommand(query, myConnection);
                        command.Parameters.AddWithValue("@id_org", id_org);
                        command.Parameters.AddWithValue("@year", year);
                        command.Parameters.AddWithValue("@month", month);
                    }
                }
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        profNum = Int32.Parse(dr["num"].ToString());
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProflieNum: " + Ex.Message);
            }
            return profNum;
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

        public static List<Norm4Table> Get4Norm(int id_org, int year, int quater)
        {
            List<Norm4Table> Norm4List = new List<Norm4Table>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewRepNormData] where id_org = @id_org and year = @year and quater = @quater";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@quater", quater);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        OneNorm oneNomr = GetOneNormDescr(Int32.Parse(dr["id_norm"].ToString()), Int32.Parse(dr["id_prod"].ToString()));
                        Norm4List.Add(new Norm4Table
                        {
                            Id_org = Int32.Parse(dr["id_org"].ToString()),
                            Id_prod = Int32.Parse(dr["id_prod"].ToString()),
                            Id_local = long.Parse(dr["id_local"].ToString()),
                            Norm_name = oneNomr.name,
                            Norm_code = oneNomr.Code,
                            Norm_type = oneNomr.type,
                            Fuel_name = !String.IsNullOrWhiteSpace(dr["fuelname"].ToString()) ? dr["fuelname"].ToString() : " ",
                            Volume = float.Parse(dr["volume"].ToString()),
                            Norm = float.Parse(dr["norm"].ToString()),
                            Value = float.Parse(dr["value"].ToString())
                        });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Get4Norm: " + Ex.Message);
            }
            return Norm4List;
        }
        public static List<Norm4InputTable> Get4NormInput(int id_org, int year, int month, int quater)
        {
            List<Norm4InputTable> Norm4List = new List<Norm4InputTable>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id_org = @id_org";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Norm4List.Add(new Norm4InputTable { Id_org = Int32.Parse(dr["id_org"].ToString()), Id_prod = Int32.Parse(dr["id_prod"].ToString()), Id_local = long.Parse(dr["id_local"].ToString()), Id_norm = Int32.Parse(dr["id"].ToString()), Fuel = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? Int32.Parse(dr["fuel"].ToString()) : 0, Fuel_name = !String.IsNullOrWhiteSpace(dr["fuel"].ToString()) ? GetFuelNameById(Int32.Parse(dr["fuel"].ToString()), year, month) : " ", Id_obj = Int32.Parse(dr["id_obj"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Get4NormInput: " + Ex.Message);
            }
            return Norm4List;
        }
        public static List<Norm4InputQuater> Get4NormQuater(int id_org, int year, int month, int quater)
        {
            List<Norm4InputQuater> Norm4List = new List<Norm4InputQuater>();
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
                        string query2 = "SELECT * FROM [NewRepNormData] WHERE [id_norm] = @id_norm AND [quater] = @quater AND year = @year";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_norm", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@quater", quater);
                        command2.Parameters.AddWithValue("@year", year);
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                OneNorm oneNomr = GetOneNormDescr(Int32.Parse(dr["id"].ToString()), Int32.Parse(dr["id_prod"].ToString()));
                                Norm4List.Add(new Norm4InputQuater { Id = Int32.Parse(dr2["id"].ToString()), Id_norm = Int32.Parse(dr["id"].ToString()), Norm_name = oneNomr.name, norm = float.Parse(dr2["norm"].ToString()), value = float.Parse(dr2["value"].ToString()), volume = float.Parse(dr2["volume"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Get4NormQuater: " + Ex.Message);
            }
            return Norm4List;
        }
        public static List<Norm4InputQuater> Get4NormQuaterType(int id_org, int year, int month, int quater, int type)
        {
            List<Norm4InputQuater> Norm4List = new List<Norm4InputQuater>();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id_org = @id_org and type = @type";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@type", type);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string query2 = "SELECT * FROM [NewRepNormData] WHERE [id_norm] = @id_norm AND [quater] = @quater AND year = @year";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_norm", Int32.Parse(dr["id"].ToString()));
                        command2.Parameters.AddWithValue("@quater", quater);
                        command2.Parameters.AddWithValue("@year", year);
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                OneNorm oneNomr = GetOneNormDescr(Int32.Parse(dr["id"].ToString()), Int32.Parse(dr["id_prod"].ToString()));
                                Norm4List.Add(new Norm4InputQuater { Id = Int32.Parse(dr2["id"].ToString()), Id_norm = Int32.Parse(dr["id"].ToString()), Norm_name = oneNomr.name, norm = float.Parse(dr2["norm"].ToString()), value = float.Parse(dr2["value"].ToString()), volume = float.Parse(dr2["volume"].ToString()) });
                            }
                        }
                        myConnection2.Close();
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка Get4NormQuaterType: " + Ex.Message);
            }
            return Norm4List;
        }

        public static List<int> GetRepIdList (int id_org, int year, int quater)
        {
            List<int> RepIdList = new List<int>();
            List<int> MonthList = MakeUnQuater(quater);
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewReport_1Per] where id_org = @id_org and year = @year and (month = @month1 or month = @month2 or month = @month3)";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month1", MonthList[0]);
                command.Parameters.AddWithValue("@month2", MonthList[1]);
                command.Parameters.AddWithValue("@month3", MonthList[2]);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        RepIdList.Add(Int32.Parse(dr["id"].ToString()));
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetRepIdList: " + Ex.Message);
            }
            return RepIdList;
        }
        public static float GetNormSum(int id_org, int year, int quater, int id_norm)
        {
            float sum = 0;
            List<int> RepIdList = GetRepIdList(id_org, year, quater);
            SqlConnection myConnection = new SqlConnection(cnStr);
            foreach (var a in RepIdList)
            {
                myConnection.Open();

            string query = "SELECT * FROM [NewNormData] WHERE [id_rep] = @id_rep AND [id_norm] = @id_norm";
            SqlCommand command = new SqlCommand(query, myConnection);

                command.Parameters.AddWithValue("@id_rep", a);
                command.Parameters.AddWithValue("@id_norm", id_norm);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        sum += float.Parse(dr["value_fact"].ToString());
                    }
                }
                myConnection.Close();
            }

            return sum;
        }

        private static List<int> MakeUnQuater(int quater)
        {
            List<int> months = new List<int>();
            if (quater == 1)
            {
                months.Add(1);
                months.Add(2);
                months.Add(3);
            }
            if (quater == 2)
            {
                months.Add(4);
                months.Add(5);
                months.Add(6);
            }
            if (quater == 3)
            {
                months.Add(7);
                months.Add(8);
                months.Add(9);
            }
            if (quater == 4)
            {
                months.Add(10);
                months.Add(11);
                months.Add(12);
            }
            return months;
        }

        public static OneNorm GetOneNormDescr(int id_norm, int id_prod)
        {
            OneNorm Norm = new OneNorm();
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection.Open();

                string query = "SELECT * FROM [NewNorm] where id=@id_norm";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_norm", id_norm);

                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        myConnection2.Open();
                        string name1 = "";
                        string query2 = "SELECT * FROM [NewProduct] where id=@id_prod";
                        SqlCommand command2 = new SqlCommand(query2, myConnection2);
                        command2.Parameters.AddWithValue("@id_prod", id_prod);
                        using (SqlDataReader dr2 = command2.ExecuteReader())
                        {
                            while (dr2.Read())
                            {
                                if (dr["name"].ToString() == "Не заполнять")
                                    name1 = dr2["name"].ToString();
                                else 
                                    name1 = dr["name"].ToString();
                            }
                        }
                        myConnection2.Close();
                        Norm = new OneNorm { Code = Int32.Parse(dr["code"].ToString()), name = name1, type = Int32.Parse(dr["type"].ToString()) };
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных OneNorm: " + Ex.Message);
            }
            return Norm;
        }
        #endregion

        public static string GetObjName(int id_obj)
        {
            string name = "";
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT full_name FROM [NewObjects] WHERE [id] = @id_obj ";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_obj", id_obj);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    name = dr["full_name"].ToString();
                }
            }
            myConnection.Close();
            return name;
        }

        public static List<ObjectTable> GetObjList(int id_org)
        {
            List<ObjectTable> ObList = new List<ObjectTable>();
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT * FROM [NewObjects] WHERE id_org = @id_org ";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    ObList.Add(new ObjectTable { Id = Int32.Parse(dr["id"].ToString()), Name = dr["name"].ToString(), FullName = dr["full_name"].ToString() });
                }
            }
            myConnection.Close();
            return ObList;
        }

        public static float GetSrcValue(int id_rep, int id_src)
        {
            float value = 0;
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT value FROM [NewOrgSouces] WHERE [id_src] = @id_src AND [id_rep] = @id_rep";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_src", id_src);
            command.Parameters.AddWithValue("@id_rep", id_rep);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    value = float.Parse(dr["value"].ToString());
                }
            }
            myConnection.Close();
            return value;
        }
        public static float GetFuelFactValue(int id_org, int id_obj, int type, int fuel, int id_rep)
        {
            float value = 0;
            int id = 0;
            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();

            string query = "SELECT id FROM [NewNorm] WHERE [id_org] = @id_org AND [id_obj] = @id_obj AND [type] = @type AND [fuel] = @fuel";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);
            command.Parameters.AddWithValue("@id_obj", id_obj);
            command.Parameters.AddWithValue("@type", type);
            command.Parameters.AddWithValue("@fuel", fuel);

            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {

                    id = Int32.Parse(dr["id"].ToString());
                }
            }
            query = "SELECT value_fact FROM [NewNormData] WHERE [id_norm] = @id_norm AND [id_rep] = @id_rep";
            command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_norm", id);
            command.Parameters.AddWithValue("@id_rep", id_rep);
            using (SqlDataReader dr = command.ExecuteReader())
            {
                while (dr.Read())
                {
                    var a = dr["value_fact"].ToString();
                    value = float.Parse(dr["value_fact"].ToString());
                }
            }
            myConnection.Close();
            return value;
        }

        public static List<SourceTable1tek> GetSourcesFor1tek(int id_org)
        {
            List<SourceTable1tek> SourceList = new List<SourceTable1tek>();
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
                        string name = GetObjName(Int32.Parse(dr["id_object"].ToString()));
                        SourceList.Add(new SourceTable1tek { Id = Int32.Parse(dr["id"].ToString()), Id_object = Int32.Parse(dr["id_object"].ToString()), Name_object=name, Id_fuel = Int32.Parse(dr["id_fuel"].ToString()), Fuel_group = fuel_type, Res_type = Int32.Parse(dr["res_type"].ToString()) });
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка получения данных органerизации: " + Ex.Message);
            }
            return SourceList;
        }

        public static ProfileTable GetProfileData(int id_org, int month, int year)
        {
            ProfileTable profileTable = new ProfileTable();

            SqlConnection myConnection = new SqlConnection(cnStr);
            myConnection.Open();
            string query = "SELECT COUNT(*) FROM NewProfiles where month = @month AND year = @year AND id_org = @id_org";
            SqlCommand command = new SqlCommand(query, myConnection);
            command.Parameters.AddWithValue("@id_org", id_org);
            command.Parameters.AddWithValue("@month", month);
            command.Parameters.AddWithValue("@year", year);
            var result = Convert.ToInt32(command.ExecuteScalar());
            if (result == 1)
            {
                query = "SELECT * FROM [NewProfiles] where month = @month AND year = @year AND id_org = @id_org";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        profileTable = new ProfileTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Num = Int32.Parse(dr["num"].ToString()),
                            Month= Int32.Parse(dr["month"].ToString()),
                            Year = Int32.Parse(dr["year"].ToString()),
                        };
                    }
                }
            }
            else
            {
                query = "SELECT * FROM [NewProfiles] where id_org = @id_org AND num = (SELECT MAX(num) FROM [NewProfiles] where id_org = @id_org)";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@month", month);
                command.Parameters.AddWithValue("@year", year);
                using (SqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        profileTable = new ProfileTable
                        {
                            Id = Int32.Parse(dr["id"].ToString()),
                            Num = Int32.Parse(dr["num"].ToString()),
                            Month = Int32.Parse(dr["month"].ToString()),
                            Year = Int32.Parse(dr["year"].ToString()),
                        };
                    }
                }
            }
            myConnection.Close();
            return profileTable;
        }

        public static int GetProfileNumIfExist(int id_org, int year, int month)
        {
            int prnum = 0;
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT COUNT(*) FROM [NewProfiles] where id_org = @id_org and year >= @year and month >= @month";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@year", year);
                command.Parameters.AddWithValue("@month", month);

                int RecordExist = (int)command.ExecuteScalar();
                if (RecordExist > 0)
                {
                    query = "SELECT * FROM [NewProfiles] where id_org = @id_org and year = @year and month = @month";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);
                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            prnum = Int32.Parse(dr["num"].ToString());
                        }
                    }
                }
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка GetProfileNumIfExist: " + Ex.Message);
            }
            return prnum;
        }
        public static void AddProfile(int year, int month, int id_org, int num)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewProfiles (id_org, num, month, year) VALUES (@id_org, @num, @month, @year)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.Parameters.AddWithValue("@num", num);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddProfile: " + Ex.Message);
            }
        }
        public static void AddNewOrgFuels(int year, int month, int num, int id_org, int id_fuel, bool trade)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewOrgFuels (id_org, num, month, year, id_fuel, trade) VALUES (@id_org, @num, @month, @year, @id_fuel, @trade)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.Parameters.AddWithValue("@num", num);
                command2.Parameters.AddWithValue("@id_fuel", id_fuel);
                command2.Parameters.AddWithValue("@trade", trade);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNewOrgFuels: " + Ex.Message);
            }
        }
        public static void AddNewOrgSource(int year, int month, int num, int id_org, int id_object, int id_fuel, int res_type)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewOrgSoucesList (id_org, id_object, id_fuel, res_type, num, month, year) VALUES (@id_org, @id_object, @id_fuel, @res_type, @num, @month, @year)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@id_object", id_object);
                command2.Parameters.AddWithValue("@id_fuel", id_fuel);
                command2.Parameters.AddWithValue("@res_type", res_type);
                command2.Parameters.AddWithValue("@num", num);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNewOrgSource: " + Ex.Message);
            }
        }
        public static void AddNewOrgRec(int year, int month, int num, int id_org, int id_owner, int res_type)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewRecievedOrgList (id_org, num, month, year, id_owner, res_type) VALUES (@id_org, @num, @month, @year, @id_owner, @res_type)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.Parameters.AddWithValue("@num", num);
                command2.Parameters.AddWithValue("@id_owner", id_owner);
                command2.Parameters.AddWithValue("@res_type", res_type);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNewOrgRec: " + Ex.Message);
            }
        }
        public static void AddNewOrgSend(int year, int month, int num, int id_org, int id_owner, int res_type)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewSendedOrgList (id_org, num, month, year, id_owner, res_type) VALUES (@id_org, @num, @month, @year, @id_owner, @res_type)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.Parameters.AddWithValue("@num", num);
                command2.Parameters.AddWithValue("@id_owner", id_owner);
                command2.Parameters.AddWithValue("@res_type", res_type);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка NewSendedOrgList: " + Ex.Message);
            }
        }
        public static void AddNewOrgNorm(int id_org, int id_prod, string id_local, int code, string name, int fuel, string row_options, int type, int id_obj, int num, int month, int year, string real_name)
        {
            try
            {
                SqlConnection myConnection2 = new SqlConnection(cnStr);
                myConnection2.Open();
                string query2 = "INSERT INTO NewNorm (id_org, id_prod, id_local, code, name, row_options, type, id_obj, num, month, year, real_name) VALUES (@id_org, @id_prod, @id_local, @code, @name, @row_options, @type, @id_obj, @num, @month, @year, @real_name)";
                if (fuel >0)
                    query2 = "INSERT INTO NewNorm (id_org, id_prod, id_local, code, name, fuel, row_options, type, id_obj, num, month, year, real_name) VALUES (@id_org, @id_prod, @id_local, @code, @name, @fuel, @row_options, @type, @id_obj, @num, @month, @year, @real_name)";
                SqlCommand command2 = new SqlCommand(query2, myConnection2);
                command2 = new SqlCommand(query2, myConnection2);
                command2.Parameters.AddWithValue("@id_org", id_org);
                command2.Parameters.AddWithValue("@id_prod", id_prod);
                command2.Parameters.AddWithValue("@id_local", id_local);
                command2.Parameters.AddWithValue("@code", code);
                command2.Parameters.AddWithValue("@name", name);
                command2.Parameters.AddWithValue("@fuel", fuel);
                command2.Parameters.AddWithValue("@row_options", row_options);
                command2.Parameters.AddWithValue("@type", type);
                command2.Parameters.AddWithValue("@id_obj", id_obj);
                command2.Parameters.AddWithValue("@num", num);
                command2.Parameters.AddWithValue("@month", month);
                command2.Parameters.AddWithValue("@year", year);
                command2.Parameters.AddWithValue("@real_name", real_name);
                command2.ExecuteNonQuery();
                myConnection2.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка AddNewOrgNorm: " + Ex.Message);
            }
        }

        public static void DeleteOldProfileVariant(int num, int id_org)
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();

                //топливо
                string query = "DELETE FROM [NewOrgFuels] where id_org = @id_org and num = @num";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();

                //источники
                query = "Delete From [NewOrgSouces] From [NewOrgSoucesList] Where [NewOrgSouces].id_src = [NewOrgSoucesList].id and [NewOrgSoucesList].id_org = @id_org and [NewOrgSoucesList].num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();
                query = "DELETE FROM [NewOrgSoucesList] where id_org = @id_org and num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();

                //поставщики
                query = "Delete From [NewRecievedOrg] From [NewRecievedOrgList] Where [NewRecievedOrg].id_recieved = [NewRecievedOrgList].id and [NewRecievedOrgList].id_owner = @id_org and [NewRecievedOrgList].num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();
                query = "DELETE FROM [NewRecievedOrgList] where id_owner = @id_org and num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();

                //потребители
                query = "Delete From [NewSendedOrg] From [NewSendedOrgList] Where [NewSendedOrg].id_sended = [NewSendedOrgList].id and [NewSendedOrgList].id_owner = @id_org and [NewSendedOrgList].num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();
                query = "DELETE FROM [NewSendedOrgList] where id_owner = @id_org and num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();

                query = "Delete From [NewNormData] From [NewNorm] Where [NewNormData].id_norm= [NewNorm].id and [NewNorm].id_org = @id_org and [NewNorm].num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();
                query = "DELETE FROM [NewNorm] where id_org = @id_org and num = @num";
                command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@num", num);
                command.ExecuteNonQuery();

                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("DeleteOldProfileVariant: " + Ex.Message);
            }
        }
        public static bool LocalIdValidChecking(string id_local, int id_org, int fuel, int id_prod, string real_name)
        {
            bool valid = false;
            Int64 local_id = Int64.Parse(id_local);
            try
            {
                SqlConnection myConnection = new SqlConnection(cnStr);
                myConnection.Open();
                string query = "SELECT COUNT(*) FROM [NewNorm] where id_org = @id_org and id_local = @id_local";
                SqlCommand command = new SqlCommand(query, myConnection);
                command.Parameters.AddWithValue("@id_org", id_org);
                command.Parameters.AddWithValue("@id_local", local_id);

                int RecordExist = (int)command.ExecuteScalar();
                if (RecordExist <= 0)
                    valid = true;
                else if (RecordExist > 0)
                {
                    query = "SELECT COUNT(*) FROM [NewNorm] where id_org = @id_org and id_local = @id_local and fuel = @fuel and id_prod = @id_prod and real_name = @real_name";
                    command = new SqlCommand(query, myConnection);
                    command.Parameters.AddWithValue("@id_org", id_org);
                    command.Parameters.AddWithValue("@id_local", local_id);
                    command.Parameters.AddWithValue("@fuel", fuel);
                    command.Parameters.AddWithValue("@id_prod", id_prod);
                    command.Parameters.AddWithValue("@real_name", real_name);
                    int RecordExist2 = (int)command.ExecuteScalar();
                    if (RecordExist2 > 0)
                        valid = true;
                    else
                        valid = false;
                }
                
                myConnection.Close();
            }
            catch (Exception Ex)
            {
                KryptonMessageBox.Show("Ошибка LocalIdValidChecking: " + Ex.Message);
            }
            return valid;
        }
    }
}
