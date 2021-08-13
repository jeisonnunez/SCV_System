using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValitadeErrorsCodigosFiscales
{
    public class ValidateErrorsCodigosFiscales: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public int UserSign { get => userSign; set => userSign = value; }
        public string Old_Code { get => old_Code; set => old_Code = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public char Freight { get => freight; set => freight = value; }

        private string code;

        private string code_OSTA;

        private string name_OSTA;

        private string rate_OSTA;

        private string purchTax;

        private string salesTax;

        private string old_Code;

        private string name;

        private string rate;

        private char freight;

        private int userSign;

        private bool validForAP;

        private bool validForAR;

        private bool Lock;

        private DateTime? updateDate;

        private string u_IDA_Alicuota;

        private string selected_U_IDA_Alicuota;

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

        public string Code_OSTA
        {
            get
            {
                return code_OSTA;
            }

            set
            {
                if (value != code_OSTA)
                {
                    code_OSTA = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Code_OSTA");
                }
            }
        }

        public string Name_OSTA
        {
            get
            {
                return name_OSTA;
            }

            set
            {
                if (value != name_OSTA)
                {
                    name_OSTA = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Name_OSTA");
                }
            }
        }

        public string Rate_OSTA
        {
            get
            {
                return rate_OSTA;
            }

            set
            {
                if (value != rate_OSTA)
                {
                    rate_OSTA = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Rate_OSTA");
                }
            }
        }

        public string Selected_U_IDA_Alicuota
        {
            get
            {
                return selected_U_IDA_Alicuota;
            }

            set
            {
                if (value != selected_U_IDA_Alicuota)
                {
                    selected_U_IDA_Alicuota = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Selected_U_IDA_Alicuota");
                }
            }
        }

        public string U_IDA_Alicuota
        {
            get
            {
                return u_IDA_Alicuota;
            }

            set
            {
                if (value != u_IDA_Alicuota)
                {
                    u_IDA_Alicuota = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("U_IDA_Alicuota");
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

        public bool Lock1
        {
            get
            {
                return Lock;
            }

            set
            {
                if (value != Lock)
                {
                    Lock = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Lock1");
                }
            }
        }

        public bool ValidForAP
        {
            get
            {
                return validForAP;
            }

            set
            {
                if (value != validForAP)
                {
                    validForAP = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ValidForAP");
                }
            }
        }

        public bool ValidForAR
        {
            get
            {
                return validForAR;
            }

            set
            {
                if (value != validForAR)
                {
                    validForAR = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ValidForAR");
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
                            result = "Codigo fiscal no puede estar vacio";
                        else if (Code.Length > 8)
                            result = "Codigo fiscal no debe exceder la cantidad de caracteres de 8";                     
                        break;

                    case "Name":
                        if (string.IsNullOrWhiteSpace(Name))
                            result = "Nombre fiscal no puede estar vacio";
                        else if (Name.Length > 100)
                            result = "Nombre fiscal no debe exceder la cantidad de caracteres de 100";
                        break;

                    case "Code_OSTA":
                        if (string.IsNullOrWhiteSpace(Code_OSTA))
                            result = "Clase de impuesto no puede estar vacio";
                        else if (Code_OSTA.Length > 8)
                            result = "Clase de impuesto no debe exceder la cantidad de caracteres de 8";
                        break;

                    case "Name_OSTA":
                        if (string.IsNullOrWhiteSpace(Name_OSTA))
                            result = "Nombre de impuesto no puede estar vacio";
                        else if (Name_OSTA.Length > 100)
                            result = "Nombre de impuesto no debe exceder la cantidad de caracteres de 100";
                        break;

                    case "Rate":
                        if (string.IsNullOrWhiteSpace(Rate))
                            result = "Tasa no puede estar vacio";                       
                        else if (ConvertToDecimal(Rate) == false)
                            result = "Entrada invalidad";
                        else if (NumericValidator(Rate) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
                        break;

                    case "Rate_OSTA":
                        if (string.IsNullOrWhiteSpace(Rate_OSTA))
                            result = "Tasa no puede estar vacio";
                        else if (ConvertToDecimal(Rate_OSTA) == false)
                            result = "Entrada invalidad";
                        else if (NumericValidator(Rate_OSTA) == false)
                            result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";
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

                    case "U_IDA_Alicuota":
                        if (string.IsNullOrWhiteSpace(U_IDA_Alicuota))
                            result = "Alicuota no puede estar vacio";
                        else if (U_IDA_Alicuota.Length > 20)
                            result = "Alicuota no debe exceder la cantidad de caracteres de 20";
                        break;

                    case "Selected_U_IDA_Alicuota":
                        if (string.IsNullOrWhiteSpace(Selected_U_IDA_Alicuota))
                            result = "Alicuota no puede estar vacio";
                        else if (Selected_U_IDA_Alicuota.Length > 20)
                            result = "Alicuota no debe exceder la cantidad de caracteres de 20";
                        break;


                }
                return result;
            }
        }
    }
}
