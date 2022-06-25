using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service_System.Entity
{
    public class Library
    {
        public int Id { get; set; }
        public string Autor { get; set; }
        public string BookName { get; set; }
        public string Date_rent { get; set; }
        public int? IdReader { get; set; }
        public override string ToString()
        {
            return "Id=" + Id.ToString() + "; Autor= " + Autor + "; Book Name=" + BookName + "; Date rent=" + Date_rent + "; Id reader=" + IdReader.ToString() + ";";
        }
    }
}
