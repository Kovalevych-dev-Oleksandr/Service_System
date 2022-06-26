using MySql.Data.MySqlClient;
using Service_System.Dao;
using Service_System.DB;
using Service_System.Entity;
using System;
using System.Collections.Generic;

namespace Service_System.DAO
{
    public class AdminDao
    {
        private const string SelectAdmin = "SELECT id, login, password from admins WHERE id=@id;";
        private const string GET_MAX_ID_ADMINS = "SELECT max(id) FROM admins;";
        private static readonly MySqlConnection Connection = DataBaseConnection.GetConection();
        private static MySqlDataReader DATA_READER;
        private static AdminDao Instance;

        private AdminDao() { }

        public static AdminDao GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AdminDao();
            }
            return Instance;
        }

        public bool LogIn(string login, string pass)
        {
            return IsAdmiPasswordExist(pass) && IsAdmiLoginExist(login);

        }

        public bool Create(string login, string pass)
        {
            if (IsAdmiLoginExist(login))
            {
                return false;
            }

            MySqlCommand command = new MySqlCommand("INSERT INTO admins ( login, password ) VALUES (@Login, @Password);", Connection);
            try
            {
                
                Connection.Open();
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", pass);
                DATA_READER = command.ExecuteReader();
                Connection.Close();
                return IsAdmiLoginExist(login);
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        public List<Admin> FindAll()
        {
            MySqlCommand command = new MySqlCommand("SELECT id, login, password FROM admins;", Connection);
            try
            {
                Connection.Open();
                DATA_READER = command.ExecuteReader();
                List<Admin> list = new List<Admin>();
                while (DATA_READER.Read())
                {
                    
                     list.Add(new Admin(int.Parse(DATA_READER[0].ToString()), DATA_READER[1].ToString(),DATA_READER[3].ToString()));
                }
                DATA_READER.Close();
                Connection.Close();
                return list;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return null;
            }
        }

        public Admin FindById(string id)
        {
            MySqlCommand command = new MySqlCommand(SelectAdmin, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                Admin admin = new Admin();
                while (DATA_READER.Read())
                {
                    admin.Id=(int.Parse(DATA_READER[3].ToString()));
                    admin.Login=(DATA_READER[1].ToString());
                    admin.Password=(DATA_READER[2].ToString());
                    DATA_READER.Close();
                    Connection.Close();
                    return admin;
                }
                return null;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return null;
            }
        }

        private bool IsAdmiLoginExist(string login)
        {
            MySqlCommand command = new MySqlCommand("SELECT login from admins WHERE login=@login;", Connection);
            try
            {
                bool is_exist = false;
                Connection.Open();
                command.Parameters.AddWithValue("@login", login);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read()) 
                {

                    is_exist = login == DATA_READER[0].ToString();
                }
                DATA_READER.Close();
                Connection.Close();
                return is_exist;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        private bool IsAdmiPasswordExist(string password)
        {
            MySqlCommand command = new MySqlCommand("SELECT password from admins WHERE password=@pass;", Connection);
            try
            {
                bool is_exist = false;
                Connection.Open();
                command.Parameters.AddWithValue("@pass", password);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read())
                {

                    is_exist= password == DATA_READER[0].ToString();
                }
                DATA_READER.Close();
                Connection.Close();
                return is_exist;
                
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        public bool Delete(string id)
        {
            string sql;
            MySqlCommand command;
            try
            {
                if (FindById(id) == null)
                {
                    return false;
                }
                Connection.Open();
                sql = "DELETE FROM admins WHERE id = @id;";
                command = new MySqlCommand(sql, Connection);
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                return UtilDao.CheckId(id, "SELECT  id from adims WHERE id = @id;");
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

       /* private bool IsExistAdmin(string login, string pass)
        {
        }*/
    }
}
