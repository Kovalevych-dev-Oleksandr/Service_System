using MySql.Data.MySqlClient;
using Service_System.DB;
using Service_System.Entity;
using System;
using System.Collections.Generic;

namespace Service_System.Dao
{


    public class ReaderDao
    {
        private const string SelectReader = "SELECT name, surname, email, id_readers from people.readers WHERE id_readers=@id;";
        private const string GET_MAX_ID_READER = "SELECT max(id_readers) FROM people.readers;";
        private static readonly MySqlConnection Connection = DataBaseConnection.GetConection();
        private static ReaderDao Instance;
        private static MySqlDataReader DATA_READER;

        private ReaderDao() { }

        public static ReaderDao GetInstance()
        {
            if (Instance == null)
            {
                Instance = new ReaderDao();
            }
            return Instance;
        }

        public bool Create(string name, string surname, string email)
        {
            string sql = "INSERT INTO people.readers ( name, surname, email) VALUES (@name, @surname, @email);";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@email", email);
                DATA_READER = command.ExecuteReader();
                Connection.Close();
                return CheckReader(name, surname, email, getId(GET_MAX_ID_READER));
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        public bool Update(string name, string surname, string email, int id)
        {
            string sql = "Update people.readers Set name = @name, surname = @surname, email=@email where id_readers = @id;";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@surname", surname);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                return CheckReader(name, surname, email, id);
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        public Reader FindById(string id)
        {
            MySqlCommand command = new MySqlCommand(SelectReader, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                Reader reader = new Reader();
                while (DATA_READER.Read())
                {
                    reader.Name = DATA_READER[0].ToString();
                    reader.Surname = DATA_READER[1].ToString();
                    reader.Id = int.Parse(DATA_READER[3].ToString());
                    reader.Email = DATA_READER[2].ToString();
                    DATA_READER.Close();
                    Connection.Close();
                    return reader;
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
      

        public List<Reader> FindAll()
        {
            MySqlCommand command = new MySqlCommand("SELECT name, surname, email, id_readers FROM people.readers;", Connection);
            try
            {
                Connection.Open();
                DATA_READER = command.ExecuteReader();
                // Reader reader = new Reader();
                List<Reader> list = new List<Reader>();
                while (DATA_READER.Read())
                {
                    /*reader.Name = DATA_READER[0].ToString();
                    reader.Surname = DATA_READER[1].ToString();
                    reader.Id = int.Parse(DATA_READER[3].ToString());*/
                    list.Add(new Reader { Name = DATA_READER[0].ToString(), Surname = DATA_READER[1].ToString(), Id = int.Parse(DATA_READER[3].ToString()), Email = DATA_READER[2].ToString() });
                }
                DATA_READER.Close();
                Connection.Close();
                return list;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return new List<Reader>();
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
                sql = "DELETE FROM people.readers WHERE id_readers = @id;";
                command = new MySqlCommand(sql, Connection);
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                return CheckId(id, "SELECT  id_readers from people.readers WHERE id_readers=@id;");
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        private bool CheckId(string id, string sql)
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

        private bool CheckReader(string name, string surneme, string email, int id)
        {
            bool isUpdate = false;
            MySqlCommand command = new MySqlCommand(SelectReader, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read())
                {
                    if (name == DATA_READER[0].ToString() && surneme == DATA_READER[1].ToString() && email == DATA_READER[2].ToString())
                    {
                        isUpdate = true;
                    }
                }
                DATA_READER.Close();
                Connection.Close();
                return isUpdate;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
        
        private int getId(string sql)
        {
            Connection.Open();
            MySqlCommand command = new MySqlCommand(sql, Connection);
            DATA_READER = command.ExecuteReader();
            DATA_READER.Read();
            int i = int.Parse(DATA_READER[0].ToString());
            DATA_READER.Close();
            Connection.Close();
            return i;
        }
    }
}

