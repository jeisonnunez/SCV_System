using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;
using WPFtoolkitFrameworkStalin.ViewModels;

namespace Vista.Inicio.ValidationErrorEmpresa
{
    public class ValidationErrorEmpresa:INotifyPropertyChangeObservable,IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string _database;

        private string _sociedad;

        ControladorEmpresa cn = new ControladorEmpresa();

        public static Dictionary<string, string> ErrorCollectionMessages { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                   
                    case "Database":
                        if (string.IsNullOrWhiteSpace(Database))
                            result = "Base de datos no puede estar vacio";
                        else if (Database.Length > 16)
                            result = "Base de datos no debe exceder la cantidad de caracteres de 16";
                        else if (cn.VerifiedDatabase("SCV_"+Database))
                            result = "La base de datos " + Database + " ya existe";
                        break;

                    case "Sociedad":
                        if (string.IsNullOrWhiteSpace(Sociedad))
                            result = "Sociedad no puede estar vacio";
                        else if (Sociedad.Length > 100)
                            result = "Sociedad no debe exceder la cantidad de caracteres de 100";
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

        public string Database
        {
            get { return _database; }
            set
            {
                OnPropertyChanged(ref _database, value);
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
