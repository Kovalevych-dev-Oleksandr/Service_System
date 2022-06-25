using Service_System.DAO;
using Service_System.Entity;
using System.Collections.Generic;

namespace Service_System.Service
{
    public class AdminService
    {
        private AdminDao adminDao = AdminDao.GetInstance();

        public bool Create(string login, string pass)
        {
            return adminDao.Create(login, pass);
        }

        public List<Admin> FindAll()
        {
            return adminDao.FindAll();
        }

        public Admin FindById(string id)
        {
            return adminDao.FindById(id);
        }

        public bool Delete(string id)
        {
            return adminDao.Delete(id);
        }
    }
}
