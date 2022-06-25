using MySql.Data.MySqlClient;

namespace Service_System.DB
{
    public class DataBaseConnection
    {
        private const string ConnectionStr = "server=localhost;user=root;database=people;password=Alyakoval;";
        private static readonly MySqlConnection Connection = new MySqlConnection(ConnectionStr);

        private DataBaseConnection() { }

        public static MySqlConnection GetConection()
        {
            return Connection;
        }
       
    }
}
