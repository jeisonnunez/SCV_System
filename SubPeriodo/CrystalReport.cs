using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class CrystalReport
    {
        private string user_ID;

        private string serverName;

        private string password;

        private string dataBase;

        public string User_ID { get => user_ID; set => user_ID = value; }
        public string ServerName { get => serverName; set => serverName = value; }
        public string Password { get => password; set => password = value; }
        public string DataBase { get => dataBase; set => dataBase = value; }
    }
}
