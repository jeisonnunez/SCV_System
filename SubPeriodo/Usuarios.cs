using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Usuarios
    {
       
        private string password;

        private string user_code;

        private string user_name;

        private char locked;

        private string sociedad;

        private int userID;

        private char deleted;

        public string Password { get => password; set => password = value; }
        public string User_code { get => user_code; set => user_code = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public char Locked { get => locked; set => locked = value; }
        public string Sociedad { get => sociedad; set => sociedad = value; }
        public int UserID { get => userID; set => userID = value; }
        public char Deleted { get => deleted; set => deleted = value; }
    }
}
