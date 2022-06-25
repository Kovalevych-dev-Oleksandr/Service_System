using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.Entity
{
    public class Admin
    {
        public Admin() { }
        public Admin(int id, string login, string password ) {

            Id = id;
            Login = login;
            Password = password;
        }
       
        public int Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
