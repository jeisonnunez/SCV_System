using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ValidateErrorsNroComprobante
{
    public class ValidateErrorsNroComprobante: INotifyPropertyChangeObservable, IDataErrorInfo
    {
      
        private string oldCode;

        private string code;

        private int docEntry;

        private char canceled;

        private int userSign;

        private DateTime? updateDate;

        private string nombreSerie;

        private string descripcion;

        private string tipoSerie;

        private string inicio;

        private string siguiente;

        private string fin;

        private bool activo;
        public string Error { get { return null; } }

        public string OldCode { get => oldCode; set => oldCode = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public char Canceled { get => canceled; set => canceled = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }

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

        public ValidateErrorsNroComprobante()
        {
            //TipoList1 = new List<string>();
            //TipoList1.Add("IVA");
            //TipoList1.Add("ISLR");

        }

        private ObservableCollection<string> _tipo = new ObservableCollection<string>() { "IVA", "ISLR" };

        public ObservableCollection<string> TipoSerieList
        {
            
                get { return _tipo; }
                set {
                _tipo = value;
                    OnPropertyChanged("TipoSerieList");
                }
            
        }

        public string NombreSerie
        {
            get
            {
                return nombreSerie;
            }

            set
            {
                if (value != nombreSerie)
                {
                    nombreSerie = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("NombreSerie");
                }
            }
        }

        public string Inicio
        {
            get
            {
                return inicio;
            }

            set
            {
                if (value != inicio)
                {
                    inicio = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Inicio");
                }
            }
        }

        public string Siguiente
        {
            get
            {
                return siguiente;
            }

            set
            {
                if (value != siguiente)
                {
                    siguiente = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Siguiente");
                }
            }
        }

        public string Fin
        {
            get
            {
                return fin;
            }

            set
            {
                if (value != fin)
                {
                    fin = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Fin");
                }
            }
        }

      
        public class ListTipoSerie : List<string>
        {
            public ListTipoSerie()
            {
                this.Add("IVA");
                this.Add("ISLR");

            }
        }

        
        public bool Activo
        {
            get
            {
                return activo;
            }

            set
            {
                if (value != activo)
                {
                    activo = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Activo");
                }
            }
        }

        public string TipoSerie
        {
            get
            {
                return tipoSerie;
            }

            set
            {
                if (value != tipoSerie)
                {
                    tipoSerie = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("TipoSerie");
                }
            }
        }


        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                if (value != descripcion)
                {
                    descripcion = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Descripcion");
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
                            result = "Codigo de comprobante no puede estar vacio";
                        else if (Code.Length > 50)
                            result = "Codigo de comprobante no debe exceder la cantidad de caracteres de 50";

                        break;

                    case "NombreSerie":
                        if (string.IsNullOrWhiteSpace(NombreSerie))
                            result = "Nombre no puede estar vacio";
                        else if (NombreSerie.Length >50)
                            result = "Nombre no debe exceder la cantidad de caracteres de 50";
                        break;


                    case "Descripcion":
                       if (Descripcion.Length > 100)
                            result = "Descripcion no debe exceder la cantidad de caracteres de 100";
                        break;

                    case "TipoSerie":
                        if (string.IsNullOrWhiteSpace(TipoSerie))
                            result = "Tipo serie no puede estar vacio";
                        else if (TipoSerie.Length > 10)
                            result = "Tipo serie no debe exceder la cantidad de caracteres de 10";

                        break;

                    case "Inicio":
                        if (string.IsNullOrWhiteSpace(Inicio))
                            result = "Inicio no puede estar vacio";
                        else if (ConvertToInteger(Inicio) == false)
                            result = "Entrada invalidad";
                        else if (Inicio.Length>15)
                            result = "Cantidad de digito no debe exceder los 15";

                        break;

                    case "Siguiente":
                        if (string.IsNullOrWhiteSpace(Siguiente))
                            result = "Siguiente no puede estar vacio";
                        else if (ConvertToInteger(Siguiente) == false)
                            result = "Entrada invalidad";
                        else if (Siguiente.Length > 15)
                            result = "Cantidad de digito no debe exceder los 15";
                        else if (Convert.ToInt32(Siguiente) <= Convert.ToInt32(Inicio))
                            result = "Siguiente debe ser mayor en al menos 1 valor de inicio";

                        break;

                    case "Fin":
                        if (string.IsNullOrWhiteSpace(Fin))
                            result = "Fin no puede estar vacio";
                        else if (ConvertToInteger(Fin) == false)
                            result = "Entrada invalidad";
                        else if (Fin.Length > 15)
                            result = "Cantidad de digito no debe exceder los 15";
                        else if ((Convert.ToInt32(Fin) <= Convert.ToInt32(Inicio)) || (Convert.ToInt32(Fin) <= Convert.ToInt32(Siguiente)))
                            result = "Fin debe ser mayor en al menos 1 valor de inicio y siguiente";

                        break;
                   


                }


                return result;
            }
        }
    }
}
