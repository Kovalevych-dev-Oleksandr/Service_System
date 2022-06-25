using MySql.Data.MySqlClient;
using Service_System.Entity;
using System;
using System.Collections.Generic;

namespace Service_System.Dao
{
    public class BooksDao
   {
        private const string ConnectionStr = "server=localhost;user=root;database=people;password=Alyakoval;";
        private const string SelectBook = "SELECT autor, book_name, date_rent, id_readers, id_book from people.library WHERE id_book=@id;";
        private const string GET_MAX_ID_BOOK = "SELECT max(id_book) FROM people.library;";
        private static readonly MySqlConnection Connection = new MySqlConnection(ConnectionStr);
        private static MySqlDataReader DATA_READER;

        private static BooksDao Instance;
        private BooksDao() { }

        public BooksDao GetInstanse()
        {
            if (Instance == null)
            {
                Instance = new BooksDao();
            }
            return Instance;
        }
        public bool CreateBooks(string autor, string nameBook)
        {
            string sql = "INSERT INTO people.library (autor, book_name) VALUES (@autor, @book_name);";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@autor", autor);
                command.Parameters.AddWithValue("@book_name", nameBook);
                DATA_READER = command.ExecuteReader();
                Connection.Close();
                return CheckCreateBooks(autor, nameBook, getId(GET_MAX_ID_BOOK));
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
      
        public bool UpdateBooks(string autor, string bookName, int id)
        {
            string sql = "Update people.library Set autor = @autor, book_name = @book_name  where id_book = @id;";
            MySqlCommand command = new MySqlCommand(sql, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@autor", autor);
                command.Parameters.AddWithValue("@book_name", bookName);
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                return CheckUpdateBooks(autor, bookName, id);
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
       
        public Library FindByIdBook(string id)
        {
            MySqlCommand command = new MySqlCommand(SelectBook, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id);
                DATA_READER = command.ExecuteReader();
                Library library = new Library();
                while (DATA_READER.Read())
                {
                    library.Autor = DATA_READER[0].ToString();
                    library.BookName = DATA_READER[1].ToString();
                    library.Date_rent = DATA_READER[2].ToString();
                    try
                    {
                        library.IdReader = int.Parse(DATA_READER[3].ToString());
                    }
                    catch (Exception)
                    {
                        library.IdReader = null;

                    }
                    try
                    {
                        library.Id = int.Parse(DATA_READER[4].ToString());

                    }
                    catch (Exception)
                    {

                        DATA_READER.Close();
                        Connection.Close();
                        return null;
                    }
                    DATA_READER.Close();
                    Connection.Close();
                    return library;
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
       
        public List<Library> FindAllBoks()
        {
            MySqlCommand command = new MySqlCommand("SELECT autor, book_name, date_rent, id_readers, id_book from people.library", Connection);
            try
            {
                Connection.Open();
                DATA_READER = command.ExecuteReader();
                Library library = new Library();
                List<Library> list = new List<Library>();
                while (DATA_READER.Read())
                {
                    int? current;
                    try
                    {
                        current = int.Parse(DATA_READER[3].ToString());
                    }
                    catch (Exception)
                    {
                        current = null;
                    }
                    list.Add(new Library { Autor = DATA_READER[0].ToString(), BookName = DATA_READER[1].ToString(), IdReader = current, Id = int.Parse(DATA_READER[4].ToString()), Date_rent = library.Date_rent = DATA_READER[2].ToString() });
                }
                DATA_READER.Close();
                Connection.Close();
                return list;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return new List<Library>();
            }
        }
       /* public bool Delete(string id, string doCase)
        {
            string sql;
            MySqlCommand command;
            try
            {
                switch (doCase)
                {
                    case "book":
                        if (FindByIdBook(id) == null)
                        {
                            return false;
                        }
                        Connection.Open();
                        sql = "DELETE FROM people.library WHERE id_book = @id;";
                        command = new MySqlCommand(sql, Connection);
                        command.Parameters.AddWithValue("@id", id);
                        DATA_READER = command.ExecuteReader();
                        DATA_READER.Close();
                        Connection.Close();
                        return CheckId(id, "SELECT id_book from people.library WHERE id_book=@id;");
                    case "reader":
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
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }
*/
        public bool rentBook(int idReader, int idBook, string date)
        {
            MySqlCommand command = new MySqlCommand("UPDATE people.library SET date_rent=@date, id_readers=@idReaders WHERE id_book=@id;", Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", idBook);
                command.Parameters.AddWithValue("@idReaders", idReader);
                command.Parameters.AddWithValue("@date", date);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                Library library = FindByIdBook(idBook.ToString());
                return library.IdReader == idReader && library.Id == idBook;
            }
            catch (Exception)
            {
                DATA_READER.Close();
                Connection.Close();
                return false;
            }
        }

        public bool DeleteRent(int idBook)
        {
            MySqlCommand command = new MySqlCommand("UPDATE people.library SET date_rent=null, id_readers=null WHERE id_book = @Id; ", Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", idBook);
                DATA_READER = command.ExecuteReader();
                DATA_READER.Close();
                Connection.Close();
                Library library = FindByIdBook(idBook.ToString());
                return library.IdReader == null && library.Date_rent == "";
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

        private bool CheckUpdateBooks(string autor, string bookName, int id_book)
        {
            bool isUpdate = false;
            MySqlCommand command = new MySqlCommand(SelectBook, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id_book);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read())
                {
                    if (autor == DATA_READER[0].ToString() && bookName == DATA_READER[1].ToString())
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
        private bool CheckCreateBooks(string autor, string bookName, int id_book)
        {
            bool isUpdate = false;
            MySqlCommand command = new MySqlCommand(SelectBook, Connection);
            try
            {
                Connection.Open();
                command.Parameters.AddWithValue("@id", id_book);
                DATA_READER = command.ExecuteReader();
                while (DATA_READER.Read())
                {
                    if (autor == DATA_READER[0].ToString() && bookName == DATA_READER[1].ToString())
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


