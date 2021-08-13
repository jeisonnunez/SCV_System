using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ModelRegistros
{
    public class ModelRegistros: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string password;

        private string passwordConfirm;

        private string user_code;

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
        public string PasswordConfirm
        {
            get
            {
                return passwordConfirm;
            }

            set
            {
                if (value != passwordConfirm)
                {
                    passwordConfirm = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PasswordConfirm");
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

                    
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Contraseña no puede estar vacio";
                        else if (Password.Length > 254)
                            result = "Contraseña no debe exceder la cantidad de caracteres de 254";
                        break;

                    case "PasswordConfirm":
                        if (string.IsNullOrWhiteSpace(PasswordConfirm))
                            result = "Contraseña no puede estar vacio";
                        else if (PasswordConfirm.Length > 254)
                            result = "Contraseña no debe exceder la cantidad de caracteres de 254";
                        else if (PasswordConfirm!=Password)
                            result = "Confirmación de constraseña debe ser igual a la contraseña";
                        break;

                }


                return result;
            }
        }


    }
}
