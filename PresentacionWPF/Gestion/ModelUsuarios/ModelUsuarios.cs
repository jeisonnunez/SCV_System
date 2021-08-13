using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ModelUsuarios
{
    public class ModelUsuarios: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public string Sociedad { get => sociedad; set => sociedad = value; }
        
        public int UserID { get => userID; set => userID = value; }
        public char Deleted { get => deleted; set => deleted = value; }

        private string password;

        private string user_code;

        private string user_name;

        private bool locked;

        private string sociedad;

        private int userID;

        private char deleted;

        public string User_code
        {
            get
            {
                return user_code;
            }

            set
            {
                if (value != user_code)
                {
                    user_code = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("User_code");
                }
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                if (value != password)
                {
                    password = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Password");
                }
            }
        }

        public bool Locked
        {
            get
            {
                return locked;
            }

            set
            {
                if (value != locked)
                {
                    locked = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("locked");
                }
            }
        }


        public string User_name
        {
            get
            {
                return user_name;
            }

            set
            {
                if (value != user_name)
                {
                    user_name = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("User_name");
                }
            }
        }



        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "User_code":
                        if (string.IsNullOrWhiteSpace(User_code))
                            result = "Codigo de usuario no puede estar vacio";
                        else if (User_code.Length > 25)
                            result = "Codigo de usuario no debe exceder la cantidad de caracteres de 25";

                        break;

                    case "User_name":
                        if (string.IsNullOrWhiteSpace(User_name))
                            result = "Nombre de usuario no puede estar vacio";
                        else if (User_name.Length > 155)
                            result = "Nombre no debe exceder la cantidad de caracteres de 155";
                        break;


                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Contraseña no puede estar vacio";
                        else if (Password.Length > 254)
                            result = "Contraseña no debe exceder la cantidad de caracteres de 254";
                         break;

                }


                return result;
            }
        }
    }
}
