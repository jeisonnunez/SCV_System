using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ModelTipoCambio
{
    public class ModelTipoCambio: INotifyPropertyChangeObservable,IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string rateDate;

        private string currency;

        private string usd;

        private string selectedValueYear;

        private string selectedValueMonth;
        public string SelectedValueYear
        {
            get
            {
                return selectedValueYear;
            }

            set
            {
                if (value != selectedValueYear)
                {
                    selectedValueYear = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedValueYear");
                }
            }
        }

        public string SelectedValueMonth
        {
            get
            {
                return selectedValueMonth;
            }

            set
            {
                if (value != selectedValueMonth)
                {
                    selectedValueMonth = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedValueMonth");
                }
            }
        }

        public string RateDate
        {
            get
            {
                return rateDate;
            }

            set
            {
                if (value != rateDate)
                {
                    rateDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("RateDate");
                }
            }
        }

        public string USD
        {
            get
            {
                return usd;
            }

            set
            {
                if (value != usd)
                {
                    usd = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("USD");
                }
            }
        }

        public string Currency
        {
            get
            {
                return currency;
            }

            set
            {
                if (value != currency)
                {
                    currency = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Currency");
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
                    case "Currency":
                        if (string.IsNullOrWhiteSpace(Currency))
                            result = "Codigo de moneda no puede estar vacio";
                        else if (Currency.Length > 3)
                            result = "Codigo de moneda no debe exceder la cantidad de caracteres de 3";
                        break;


                    case "RateDate":
                        if (string.IsNullOrWhiteSpace(RateDate))
                            result = "Fecha no puede estar vacio";                       
                        break;

                    case "USD":
                        if (string.IsNullOrWhiteSpace(USD))
                        {
                            result = "Entrada invalidad";
                        }

                        else if (ConvertToDecimal(USD) == false)
                                result = "Entrada invalidad";
                            else if (NumericValidator(USD) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6 ";

                        
                        break;

                    case "SelectedValueYear":
                        if (string.IsNullOrWhiteSpace(SelectedValueYear))
                        {

                            result = "Año no puede estar vacio";
                        }
                        break;

                    case "SelectedValueMonth":
                        if (string.IsNullOrWhiteSpace(SelectedValueMonth))
                        {

                            result = "Mes no puede estar vacio";
                        }
                        break;

                }


                return result;
            }
        }


        private int userSign;     
        public int UserSign { get => userSign; set => userSign = value; }
    }
}
