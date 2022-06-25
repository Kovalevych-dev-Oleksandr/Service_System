using Service_System.Dao;
using Service_System.Entity;
using System.Collections.Generic;

namespace Service_System.Service
{
    public class BooksService
    {
        private BooksDao booksService = BooksDao.GetInstanse();

        public bool Create(string autor, string nameBook)
        {
            return booksService.Create(autor, nameBook);
        }

        public bool Update(string autor, string bookName, int id)
        {
            return booksService.Update(autor, bookName, id);
        }

        public Library FindById(string id)
        {
            return booksService.FindById(id);
        }

        public List<Library> FindAll()
        {
            return FindAll();
        }

        public bool Delete(string id)
        {
            return booksService.Delete(id);
        }
    }
}
