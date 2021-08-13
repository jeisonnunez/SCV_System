using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValidateErrorsDeterminacionMayor
{
    public class ModelDeterminacionMayor: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string acctCode;

        private string acctName;

        private string type;

        private string id;

        public string AcctCode
        {
            get
            {
                return acctCode;
            }

            set
            {
                if (value != acctCode)
                {
                    acctCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("AcctCode");
                }
            }
        }

        public string AcctName
        {
            get
            {
                return acctName;
            }

            set
            {
                if (value != acctName)
                {
                    acctName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("AcctName");
                }
            }
        }

        public string Type { get => type; set => type = value; }
        public string ID { get => id; set => id = value; }

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "AcctCode":
                        if (string.IsNullOrWhiteSpace(AcctCode))
                            result = "Codigo de cuenta no puede estar vacio";
                        else if (AcctCode.Length > 15)
                            result = "Codigo de cuenta no debe exceder la cantidad de caracteres de 15";
                        else if (VerifyCuentasNoAsociadas(AcctCode))
                            result = "Cuenta invalida";

                        break;

                    case "AcctName":
                        if (string.IsNullOrWhiteSpace(AcctName))
                            result = "Nombre de cuenta no puede estar vacio";
                        else if (AcctName.Length > 100)
                            result = "Nombre de cuenta no debe exceder la cantidad de caracteres de 100";
                     

                        break;



                }


                return result;
            }
        }

    }
}
