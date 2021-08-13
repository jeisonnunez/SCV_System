using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Inicio.ValidationErrorUsuarioSitio
{
    public class ValidationErrorUsuarioSitio: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string _username;

        private string _password;

        public static Dictionary<string, string> ErrorCollectionMessages { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {

                    case "Username":
                        if (string.IsNullOrWhiteSpace(Username))
                            result = "Usuario no puede estar vacio";
                        else if (Username.Length > 50)
                            result = "Usuario no debe exceder la cantidad de caracteres de 50";
                       
                        break;

                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Password no puede estar vacio";
                        else if (Password.Length > 50)
                            result = "Password no debe exceder la cantidad de caracteres de 50";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                {
                    ErrorCollection[name] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(name, result);


                }

                OnPropertyChanged("ErrorCollection");

                ErrorCollectionMessages = ErrorCollection;

                return result;
            }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                OnPropertyChanged(ref _username, value);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                OnPropertyChanged(ref _password, value);
            }
        }
    }
}
