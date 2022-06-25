using MySql.Data.MySqlClient;
using Service_System.DB;
using Service_System.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.DAO
{
    public class AdminDao
    {
        private const string SelectAdmin = "SELECT id, login, password from admins WHERE id=@id;";
        private static readonly MySqlConnection Connection = DataBaseConnection.GetConection();
        private static MySqlDataReader DATA_READER;
        public static AdminDao Instance;
        private AdminDao() { }

        public static AdminDao GetInstance()
        {
            if (Instance == null)
            {
                Instance = new AdminDao();
            }
            return Instance;
        }

        public bool CreateAdmin(string login, string pass)
        { 
            string sql = "INSERT INTO admins ( Login, Password,) VALUES (@Login, @Password);";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
               Connection.Open();
                command.Parameters.AddWithValue("@Login", login);
                command.Parameters.AddWithValue("@Password", pass);
                DATA_READER = command.ExecuteReader();
                Connection.Close();
                return IsExistAdmin(login, pass);
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
       
        private bool IsExistAdmin(string login, string pass)
        {
            List<Admin> admins = FindAllAdmin();
            for(int i = 0; i < admins.Count; i++)
            {
                if (admins[i].Login==login)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Admin> FindAllAdmin()
        {
            MySqlCommand command = new MySqlCommand("SELECT Id,Login,Password FROM admins;", Connection);
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

        public Admin FindByIdAdmin(string id)
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
        /*public bool Delete(string id, string doCase)
        {
            string sql;
            MySqlCommand command;
            try
            {
               
                        if (FindByIdReaders(id) == null)
                        {
                            return false;
                        }
                        Connection.Open();
                        sql = "DELETE FROM people.readers WHERE id_readers = @id;";
                        command = new MySqlCommand(sql, Connection);
                        command.Parameters.AddWithValue("@id", id);
                        DATA_READER = command.ExecuteReader();
                        DATA_READER.Close();
                        Connection.Close();
                        return CheckId(id, "SELECT  id_readers from people.readers WHERE id_readers=@id;");
                    default:
                        return false;
                
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
    }*/

    }
}
