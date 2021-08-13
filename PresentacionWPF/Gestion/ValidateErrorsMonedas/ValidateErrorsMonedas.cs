using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValidateErrorsMonedas
{
    public class ValidateErrorsMonedas: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string oldcurrCode;

        private string currCode;

        private string currName;

        private string docCurrCod;

        private string userSign;

        private bool locked;

        public string CurrCode
        {
            get
            {
                return currCode;
            }

            set
            {
                if (value != currCode)
                {
                    currCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("CurrCode");
                }
            }
        }

        public string OldCurrCode
        {
            get
            {
                return oldcurrCode;
            }

            set
            {
                if (value != oldcurrCode)
                {
                    oldcurrCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("OldCurrCode");
                }
            }
        }

        public string CurrName
        {
            get
            {
                return currName;
            }

            set
            {
                if (value != currName)
                {
                    currName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("CurrName");
                }
            }
        }

        public string DocCurrCod
        {
            get
            {
                return docCurrCod;
            }

            set
            {
                if (value != docCurrCod)
                {
                    docCurrCod = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("DocCurrCod");
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
                    OnPropertyChanged("Locked");
                }
            }
        }

        
        public string UserSign
        {
            get
            {
                return userSign;
            }

            set
            {
                if (value != userSign)
                {
                    userSign = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("UserSign");
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
                    case "CurrCode":
                        if (string.IsNullOrWhiteSpace(CurrCode))
                            result = "Codigo de moneda no puede estar vacio";
                        else if (CurrCode.Length > 3)
                            result = "Codigo de moneda no debe exceder la cantidad de caracteres de 3";

                        break;

                    case "CurrName":
                        if (string.IsNullOrWhiteSpace(CurrName))
                            result = "Nombre no puede estar vacio";
                        else if (CurrName.Length > 20)
                            result = "Nombre no debe exceder la cantidad de caracteres de 20";
                        break;


                    case "DocCurrCod":
                        if (string.IsNullOrWhiteSpace(DocCurrCod))
                            result = "Codigo internacional de moneda no puede estar vacio";
                        else if (DocCurrCod.Length > 3)
                            result = "Codigo internacional de moneda no debe exceder la cantidad de caracteres de 3";
                       

                        break;

                }


                return result;
            }
        }
    }
}
