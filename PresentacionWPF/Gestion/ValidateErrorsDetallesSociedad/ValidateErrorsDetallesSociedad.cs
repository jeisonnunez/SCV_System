using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValidateErrorsDetallesSociedad
{
    public class ValidateErrorsDetallesSociedad: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }

        private List<Entidades.Monedas> monedas;

        private string compnyName;

        private string compnyAddr;

        private string country;

        private string printHeadr;

        private string phone1;

        private string phone2;

        private string fax;

        private string zipCode;

        private string e_Mail;

        private string mainCurncy;

        private string sysCurrncy;

        private string taxIdNum;

        private string revOffice;

        private string selectedCurrCodeMain;

        private string selectedCurrCodeSys;

        public List<Entidades.Monedas> Monedas
        {
            get
            {
                return monedas;
            }

            set
            {
                if (value != monedas)
                {
                    monedas = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Monedas");
                }
            }
        }

        public string SelectedCurrCodeMain
        {
            get
            {
                return selectedCurrCodeMain;
            }

            set
            {
                if (value != selectedCurrCodeMain)
                {
                    selectedCurrCodeMain = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedCurrCodeMain");
                }
            }
        }

        public string SelectedCurrCodeSys
        {
            get
            {
                return selectedCurrCodeSys;
            }

            set
            {
                if (value != selectedCurrCodeSys)
                {
                    selectedCurrCodeSys = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedCurrCodeSys");
                }
            }
        }

        public string CompnyName
        {
            get
            {
                return compnyName;
            }

            set
            {
                if (value != compnyName)
                {
                    compnyName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("CompnyName");
                }
            }
        }

        public string CompnyAddr
        {
            get
            {
                return compnyAddr;
            }

            set
            {
                if (value != compnyAddr)
                {
                    compnyAddr = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("CompnyAddr");
                }
            }
        }

        public string Country
        {
            get
            {
                return country;
            }

            set
            {
                if (value != country)
                {
                    country = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Country");
                }
            }
        }

        public string PrintHeadr
        {
            get
            {
                return printHeadr;
            }

            set
            {
                if (value != printHeadr)
                {
                    printHeadr = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PrintHeadr");
                }
            }
        }

        public string Phone1
        {
            get
            {
                return phone1;
            }

            set
            {
                if (value != phone1)
                {
                    phone1 = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Phone1");
                }
            }
        }

        public string Phone2
        {
            get
            {
                return phone2;
            }

            set
            {
                if (value != phone2)
                {
                    phone2 = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Phone2");
                }
            }
        }

        public string Fax
        {
            get
            {
                return fax;
            }

            set
            {
                if (value != fax)
                {
                    fax = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Fax");
                }
            }
        }

        public string ZipCode
        {
            get
            {
                return zipCode;
            }

            set
            {
                if (value != zipCode)
                {
                    zipCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ZipCode");
                }
            }
        }

        public string E_Mail
        {
            get
            {
                return e_Mail;
            }

            set
            {
                if (value != e_Mail)
                {
                    e_Mail = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("E_Mail");
                }
            }
        }

        public string MainCurncy
        {
            get
            {
                return mainCurncy;
            }

            set
            {
                if (value != mainCurncy)
                {
                    mainCurncy = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("MainCurncy");
                }
            }
        }

        public string SysCurrncy
        {
            get
            {
                return sysCurrncy;
            }

            set
            {
                if (value != sysCurrncy)
                {
                    sysCurrncy = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SysCurrncy");
                }
            }
        }

        public string TaxIdNum
        {
            get
            {
                return taxIdNum;
            }

            set
            {
                if (value != taxIdNum)
                {
                    taxIdNum = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("TaxIdNum");
                }
            }
        }

        public string RevOffice
        {
            get
            {
                return revOffice;
            }

            set
            {
                if (value != revOffice)
                {
                    revOffice = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("RevOffice");
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
                    case "CompnyName":
                        if (string.IsNullOrWhiteSpace(CompnyName))
                            result = "Nombre de sociedad no puede estar vacio";
                        else if (CompnyName.Length > 100)
                            result = "Nombre de sociedad no debe exceder la cantidad de caracteres de 100";

                        break;

                    case "CompnyAddr":
                        if (string.IsNullOrWhiteSpace(CompnyAddr))
                            result = "Direccion no puede estar vacio";
                        else if (CompnyAddr.Length > 254)
                            result = "Direccion no debe exceder la cantidad de caracteres de 254";
                        break;


                    case "PrintHeadr":
                        if (string.IsNullOrWhiteSpace(PrintHeadr))
                            result = "Cabecera de impresion no puede estar vacio";
                        else if (PrintHeadr.Length > 100)
                            result = "Cabecera de impresion no debe exceder la cantidad de caracteres de 100";


                        break;

                    case "Phone1":
                        if (string.IsNullOrWhiteSpace(Phone1))
                            result = "Telefono 1 no puede estar vacio";
                        else if (Phone1.Length > 20)
                            result = "Telefono 1 no debe exceder la cantidad de caracteres de 20";
                        else if (IsPhoneNumberValidator(Phone1)==false)
                            result = "Telefono 1 no tiene formato requerido";


                        break;

                    case "Phone2":
                        if (string.IsNullOrWhiteSpace(Phone2)==false)
                        {
                            if (Phone2.Length > 20)
                            {
                                result = "Telefono 2 no debe exceder la cantidad de caracteres de 20";
                            }
                            else if (IsPhoneNumberValidator(Phone2) == false)
                                result = "Telefono 2 no tiene formato requerido";

                        }


                        break;

                    case "Fax":

                        if (string.IsNullOrWhiteSpace(Fax) == false)
                        {
                            if (Fax.Length > 20)
                            {
                                result = "Fax no debe exceder la cantidad de caracteres de 20";
                            }
                            else if (FaxValidator(Fax) == false)
                                result = "Fax no tiene formato requerido";
                        }

                        
                        break;

                    case "ZipCode":
                        if (string.IsNullOrWhiteSpace(ZipCode))
                            result = "ZipCode no puede estar vacio";
                        else if (IntegerValidator(ZipCode) == false)
                            result = "ZipCode no tiene formato requerido";

                        break;

                    case "E_Mail":
                        if (string.IsNullOrWhiteSpace(E_Mail))
                            result = "Correo no puede estar vacio";
                        else if (E_Mail.Length > 100)
                            result = "Correo no debe exceder la cantidad de caracteres de 100";
                        else if (EmailValidator(E_Mail) == false)
                            result = "Correo no tiene formato requerido";


                        break;

                    case "SysCurrncy":
                        if (string.IsNullOrWhiteSpace(SysCurrncy))
                            result = "Moneda Sistema no puede estar vacio";
                        else if (SysCurrncy.Length > 3)
                            result = "Moneda Sistema no debe exceder la cantidad de caracteres de 3";


                        break;

                    case "MainCurncy":
                        if (string.IsNullOrWhiteSpace(MainCurncy))
                            result = "Moneda Local no puede estar vacio";
                        else if (MainCurncy.Length > 3)
                            result = "Moneda Local no debe exceder la cantidad de caracteres de 3";


                        break;

                    case "TaxIdNum":
                        if (string.IsNullOrWhiteSpace(TaxIdNum))
                            result = "RIF no puede estar vacio";
                        else if (TaxIdNum.Length > 32)
                            result = "RIF no debe exceder la cantidad de caracteres de 32";
                        else if (RifValidator(TaxIdNum) == false)
                            result = "RIF no tiene el formato requerido";
                        break;


                    case "RevOffice":
                        if (string.IsNullOrWhiteSpace(RevOffice) == false)
                        {
                            if (RevOffice.Length > 100)
                            {
                                result = "Hacienda no debe exceder la cantidad de caracteres de 100";
                            }
                           
                        }
                        break;

                    case "SelectedCurrCodeMain":
                        if (string.IsNullOrWhiteSpace(SelectedCurrCodeMain))
                        {
                            
                           result = "Moneda no puede estar vacia";
                        }                        
                        break;

                    case "SelectedCurrCodeSys":
                        if (string.IsNullOrWhiteSpace(SelectedCurrCodeSys))
                        {

                            result = "Moneda no puede estar vacia";
                        }
                        break;

                }


                return result;
            }
        }


        private DateTime? updateDate;

        private int userSign;
    }
}
