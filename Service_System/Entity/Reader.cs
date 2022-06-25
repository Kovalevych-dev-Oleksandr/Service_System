using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.Entity
{
    public class Reader
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? Id { get; set; }
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            return "Id=" + Id.ToString() + "; Name=" + Name + "; Surname=" + Surname + "; Email=" + Email + ";";
        }
    }
}
