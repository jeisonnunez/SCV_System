using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ModelRetencionImpuestos
{
    public class ModelRetencionImpuestos: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public int UserSign { get => userSign; set => userSign = value; }

        private string oldWtCode;

        private string wt_Code;

        private string wt_Name;

        private string rate;

        private string effecDate;

        private string category;

        private string baseType;

        private string prctBsAmnt;

        private string account;

        private string offclCode;

        private int userSign;

        private bool inactive;

        private string tipoRetencion;

        private string baseMinima;

        private string sustraendo;

        private ObservableCollection<string> _category = new ObservableCollection<string>() { "Pago", "Factura" };

        private ObservableCollection<string> _tipo = new ObservableCollection<string>() { "Neto", "IVA" };

        private ObservableCollection<string> _tipoRetencion = new ObservableCollection<string>() { "IVA", "ISLR" };
        public ObservableCollection<string> CategoryList
        {

            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged("CategoryList");
            }

        }

        public ObservableCollection<string> TipoRetencionList
        {

            get { return _tipoRetencion; }
            set
            {
                _tipoRetencion = value;
                OnPropertyChanged("TipoRetencionList");
            }

        }

        public ObservableCollection<string> TipoBaseList
        {

            get { return _tipo; }
            set
            {
                _tipo = value;
                OnPropertyChanged("TipoBaseList");
            }

        }

        public string WTCode
        {
            get
            {
                return wt_Code;
            }

            set
            {
                if (value != wt_Code)
                {
                    wt_Code = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Wt_Code");
                }
            }
        }

        public string U_IDA_TipoRetencion
        {
            get
            {
                return tipoRetencion;
            }

            set
            {
                if (value != tipoRetencion)
                {
                    tipoRetencion = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("U_IDA_TipoRetencion");
                }
            }
        }

        public string U_IDA_Sustraendo
        {
            get
            {
                return sustraendo;
            }

            set
            {
                if (value != sustraendo)
                {
                    sustraendo = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("U_IDA_Sustraendo");
                }
            }
        }

        public string U_IDA_BaseMinima
        {
            get
            {
                return baseMinima;
            }

            set
            {
                if (value != baseMinima)
                {
                    baseMinima = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("U_IDA_BaseMinima");
                }
            }
        }

        public string Account
        {
            get
            {
                return account;
            }

            set
            {
                if (value != account)
                {
                    account = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Account");
                }
            }
        }

        public string Offclcode
        {
            get
            {
                return offclCode;
            }

            set
            {
                if (value != offclCode)
                {
                    offclCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("OffclCode");
                }
            }
        }

        public string PrctBsAmnt
        {
            get
            {
                return prctBsAmnt;
            }

            set
            {
                if (value != prctBsAmnt)
                {
                    prctBsAmnt = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PrctBsAmnt");
                }
            }
        }

        public string Category
        {
            get
            {
                return category;
            }

            set
            {
                if (value != category)
                {
                    category = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Category");
                }
            }
        }

        public bool Inactive
        {
            get
            {
                return inactive;
            }

            set
            {
                if (value != inactive)
                {
                    inactive = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Inactive");
                }
            }
        }

        public string BaseType
        {
            get
            {
                return baseType;
            }

            set
            {
                if (value != baseType)
                {
                    baseType = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("BaseType");
                }
            }
        }

        public string EffecDate
        {
            get
            {
                return effecDate;
            }

            set
            {
                if (value != effecDate)
                {
                    effecDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("EffecDate");
                }
            }
        }

        public string Rate
        {
            get
            {
                return rate;
            }

            set
            {
                if (value != rate)
                {
                    rate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Rate");
                }
            }
        }

        public string WTName
        {
            get
            {
                return wt_Name;
            }

            set
            {
                if (value != wt_Name)
                {
                    wt_Name = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Wt_Name");
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
                    case "WTCode":
                        if (string.IsNullOrWhiteSpace(WTCode))
                            result = "Codigo de retencion no puede estar vacio";
                        else if (WTCode.Length > 4)
                            result = "Codigo de retencion no debe exceder la cantidad de caracteres de 4";

                        break;

                    case "WTName":
                        if (string.IsNullOrWhiteSpace(WTName))
                            result = "Nombre no puede estar vacio";
                        else if (WTName.Length > 50)
                            result = "Nombre no debe exceder la cantidad de caracteres de 50";
                        break;


                    case "Rate":                       
                        if (string.IsNullOrWhiteSpace(Rate))
                            result = "Tasa no puede estar vacio";
                        else if (ConvertToDecimal(Rate) == false)
                            result = "Entrada invalidad";
                        else if (NumericValidator(Rate) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
                        break;

                    case "EffecDate":
                        if (string.IsNullOrWhiteSpace(EffecDate))
                            result = "Fecha no puede estar vacio";                       

                        break;

                    case "Category":
                        if (string.IsNullOrWhiteSpace(Category))
                            result = "Categoria no puede estar vacio";
                        

                        break;

                    case "BaseType":
                        if (string.IsNullOrWhiteSpace(BaseType))
                            result = "Tipo no puede estar vacio";


                        break;

                    case "PrctBsAmnt":
                        if (string.IsNullOrWhiteSpace(PrctBsAmnt))
                            result = "Porcentaje no puede estar vacio";
                        else if (ConvertToDecimal(PrctBsAmnt) == false)
                            result = "Entrada invalidad";
                        else if (NumericValidator(PrctBsAmnt) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
                        break;


                    case "Offclcode":
                        if (Offclcode.Length > 15)
                            result = "Codigo Oficial no debe exceder los 15 digitos";                      

                        break;

                    case "Account":
                        if (Account.Length > 15)
                            result = "Cuenta no debe exceder los 15 digitos";

                        break;

                    case "U_IDA_Sustraendo":
                        if (ConvertToDecimal(U_IDA_Sustraendo) == false)
                            result = "Entrada invalida";
                        else if (NumericValidator(U_IDA_Sustraendo) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
                        break;

                    case "U_IDA_BaseMinima":
                        if (ConvertToDecimal(U_IDA_BaseMinima) == false)
                            result = "Entrada invalida";
                        else if (NumericValidator(U_IDA_BaseMinima) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
                        break;

                    case "U_IDA_TipoRetencion":
                        if (string.IsNullOrWhiteSpace(U_IDA_TipoRetencion))
                            result = "Tipo retencion no puede estar vacio";
                        else if (U_IDA_TipoRetencion.Length > 20)
                            result = "Tipo retencion no debe exceder la cantidad de caracteres de 20";
                        break;

                  

                }


                return result;
            }
        }

        public string OldWtCode { get => oldWtCode; set => oldWtCode = value; }
    }
}
