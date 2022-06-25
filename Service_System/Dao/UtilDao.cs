using MySql.Data.MySqlClient;
using Service_System.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.Dao
{
    public class UtilDao
    {
        private static readonly MySqlConnection Connection = DataBaseConnection.GetConection();
        private static MySqlDataReader DATA_READER;

        public static bool CheckId(string id, string sql)
        {
            bool isDelete = true;
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read())
                {
                    isDelete = false;
                }
                DATA_READER.Close();
                Connection.Close();
                return isDelete;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
    }
}
