using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessSystem
{
    public class User
    {
        public uint Id { get; set; }
        public int GroupID { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MONfromTime { get; set; }
        public int MONtoTime { get; set; }
        public int TUEfromTime { get; set; }
        public int TUEtoTime { get; set; }
        public int WEDfromTime { get; set; }
        public int WEDtoTime { get; set; }
        public int THUfromTime { get; set; }
        public int THUtoTime { get; set; }
        public int FRIfromTime { get; set; }
        public int FRItoTime { get; set; }
        public int SATfromTime { get; set; }
        public int SATtoTime { get; set; }
        public int SUNfromTime { get; set; }
        public int SUNtoTime { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }
        public uint userID { get; set; }
        public string TimeStamp { get; set; }
    }
}
