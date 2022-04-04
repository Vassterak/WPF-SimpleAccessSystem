using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessSystem
{
    public class DatabaseContent
    {

    }

    public class Users
    {
        public uint Id { get; set; }
        public int GroupID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }
}
