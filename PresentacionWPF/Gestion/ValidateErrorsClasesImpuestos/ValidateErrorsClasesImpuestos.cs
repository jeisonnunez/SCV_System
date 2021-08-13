using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValidateErrorsClasesImpuestos
{
    public class ValidateErrorsClasesImpuestos : INotifyPropertyChangeObservable, IDataErrorInfo
    {
       
        public string Error { get { return null; } }

        private string code;

        private string name;

        private string rate;

        private string salesTax;

        private string purchTax;

        private string userSign;

        public string SalesTax
        {
            get
            {
                return salesTax;
            }

            set
            {
                if (value != salesTax)
                {
                    salesTax = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SalesTax");
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

        public string PurchTax
        {
            get
            {
                return purchTax;
            }

            set
            {
                if (value != purchTax)
                {
                    purchTax = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PurchTax");
                }
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                if (value != code)
                {
                    code = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Code");
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (value != name)
                {
                    name = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Name");
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
                    case "Code":
                        if (string.IsNullOrWhiteSpace(Code))
                            result = "Codigo de impuesto no puede estar vacio";
                        else if (Code.Length > 8)
                            result = "Codigo de impuesto no debe exceder la cantidad de caracteres de 8";

                        break;

                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                            result = "Nombre no puede estar vacio";
                        else if (Name.Length > 100)
                            result = "Nombre no debe exceder la cantidad de caracteres de 100";
                        break;


                    case "SalesTax":
                        if (string.IsNullOrWhiteSpace(SalesTax))
                            result = "Cuenta impuesto de venta no puede estar vacio";
                        else if (SalesTax.Length > 15)
                            result = "Cuenta impuesto de venta no debe exceder la cantidad de caracteres de 15";
                        else if (VerifyCuentasNoAsociadas(SalesTax))
                            result = "Cuenta invalida";

                        break;

                    case "PurchTax":
                        if (string.IsNullOrWhiteSpace(PurchTax))
                            result = "Cuenta impuesto de compra no puede estar vacio";
                        else if (PurchTax.Length > 15)
                            result = "Cuenta impuesto de compra no debe exceder la cantidad de caracteres de 15";
                        else if (VerifyCuentasNoAsociadas(PurchTax))
                            result = "Cuenta invalida";
                        break;

                    case "Rate":
                        if (string.IsNullOrWhiteSpace(Rate))
                            result = "Tasa no puede estar vacio";
                        else if (ConvertToDecimal(Rate) == false)
                            result = "Entrada invalidad";
                        else if (GetDecimalCount(Convert.ToDecimal(Rate)) == false)
                            result = "Cantidad de decimales no debe exceder los 6 digitos";
                        else if (GetNumberOfDigits(Convert.ToDecimal(Rate)) == false)
                            result = "Cantidad de enteros no debe exceder los 19 digitos";
                        break;

                    case "UserSign":
                        if (string.IsNullOrWhiteSpace(UserSign))
                            result = "Usuario no puede estar vacio";
                        break;


                }

              
                return result;
            }
        }

        
    }
}
