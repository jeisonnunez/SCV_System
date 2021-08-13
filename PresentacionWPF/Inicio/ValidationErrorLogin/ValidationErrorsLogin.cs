using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Vista.Clases_Basicas;

namespace Vista.Inicio.ValidationErrorLogin
{
    public partial class ValidationErrorsLogin:INotifyPropertyChangeObservable,IDataErrorInfo
    {
       
        public string Error { get { return null; } }

        private string _username;

        private string _password;

        private string _sociedad;
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
                        else if (Username.Length > 25)
                            result = "Usuario no debe exceder la cantidad de caracteres de 25";
                        break;

                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password))
                            result = "Contraseña no puede estar vacio";                       
                        break;

                    case "Sociedad":
                        if (string.IsNullOrWhiteSpace(Sociedad))
                            result = "Sociedad no puede estar vacio";
                        else if (Sociedad.Length > 20)
                            result = "Sociedad no debe exceder la cantidad de caracteres de 20";
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

        public string Sociedad
        {
            get { return _sociedad; }
            set
            {
                OnPropertyChanged(ref _sociedad, value);
            }
        }
    }
}
