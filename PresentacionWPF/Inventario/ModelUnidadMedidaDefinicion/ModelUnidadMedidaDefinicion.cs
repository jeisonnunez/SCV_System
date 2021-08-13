using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Inventario.ModelUnidadMedidaDefinicion
{
    public class ModelUnidadMedidaDefinicion : INotifyPropertyChangeObservable, IDataErrorInfo
    {
        private int uomEntry;

        private string uomCode;

        private string oldUomCode;

        private string uomName;

        private char locked;

        private int userSign;

        private DateTime updateDate;

        private string length;

        private string width;

        private string height;

        private string volume;

        private string weight;

        private string volUnit;

        public int UomEntry { get => uomEntry; set => uomEntry = value; }
        public string OldUomCode { get => oldUomCode; set => oldUomCode = value; }       
        public char Locked { get => locked; set => locked = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime UpdateDate { get => updateDate; set => updateDate = value; }

        private ObservableCollection<string> _volUnit = new ObservableCollection<string>() { "cc", "ci", "cm", "cmm", "dm3", "vgl" };

        public ObservableCollection<string> VolUnitList
        {

            get { return _volUnit; }
            set
            {
                _volUnit = value;
                OnPropertyChanged("VolUnitList");
            }

        }
        public string UomCode
        {
            get
            {
                return uomCode;
            }

            set
            {
                if (value != uomCode)
                {
                    uomCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("UomCode");
                }
            }
        }

        public string Length
        {
            get
            {
                return length;
            }

            set
            {
                if (value != length)
                {
                    length = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Length");
                }
            }
        }

        public string VolUnit
        {
            get
            {
                return volUnit;
            }

            set
            {
                if (value != volUnit)
                {
                    volUnit = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("VolUnit");
                }
            }
        }

        public string Volume
        {
            get
            {
                return volume;
            }

            set
            {
                if (value != volume)
                {
                    volume = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Volume");
                }
            }
        }

        public string Weight
        {
            get
            {
                return weight;
            }

            set
            {
                if (value != weight)
                {
                    weight = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Weight");
                }
            }
        }

        public string Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value != width)
                {
                    width = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Width");
                }
            }
        }

        public string Height
        {
            get
            {
                return height;
            }

            set
            {
                if (value != height)
                {
                    height = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Height");
                }
            }
        }

        public string UomName
        {
            get
            {
                return uomName;
            }

            set
            {
                if (value != uomName)
                {
                    uomName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("UomName");
                }
            }
        }
        public string Error { get { return null; } }
        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "UomCode":
                        if (string.IsNullOrWhiteSpace(UomCode))
                            result = "Codigo de unidad de medida no puede estar vacio";
                        else if (UomCode.Length > 20)
                            result = "Codigo de unidad de medida no debe exceder la cantidad de caracteres de 20";

                        break;

                    case "UomName":
                        if (string.IsNullOrWhiteSpace(UomName))
                            result = "Nombre de unidad de medida no puede estar vacio";
                        else if (UomName.Length > 100)
                            result = "Nombre de unidad de medida no debe exceder la cantidad de caracteres de 100";
                        break;


                    case "Length":

                        if (string.IsNullOrWhiteSpace(Length)==false)
                        {
                            if (ConvertToDecimal(Length) == false)
                                result = "Entrada invalida";
                            else if (NumericValidator(Length) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6";
                        }
                           
                       
                        break;

                    case "VolUnit":

                        if (string.IsNullOrWhiteSpace(VolUnit))
                            result = "Volumen de unidad de medida no puede estar vacio";
                        break;

                    case "Volume":
                        if (string.IsNullOrWhiteSpace(Volume) == false)
                        {
                            if (ConvertToDecimal(Volume) == false)
                                result = "Entrada invalida";
                            else if (NumericValidator(Volume) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6";
                        }

                        break;

                    case "Weight":
                        if (string.IsNullOrWhiteSpace(Weight) == false)
                        {
                            if (ConvertToDecimal(Weight) == false)
                                result = "Entrada invalida";
                            else if (NumericValidator(Weight) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6";

                        }

                        break;

                    case "Width":
                        if (string.IsNullOrWhiteSpace(Width) == false)
                        {
                            if (ConvertToDecimal(Width) == false)
                                result = "Entrada invalida";
                            else if (NumericValidator(Width) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6";
                        }

                        break;

                    case "Height":
                        if (string.IsNullOrWhiteSpace(Height) == false)
                        {
                            if (ConvertToDecimal(Height) == false)
                                result = "Entrada invalida";
                            else if (NumericValidator(Height) == false)
                                result = "Cantidad de enteros no debe exceder los 19 y decimales no debe exceder los 6";
                        }

                        break;



                }


                return result;
            }
        }
    }
}
