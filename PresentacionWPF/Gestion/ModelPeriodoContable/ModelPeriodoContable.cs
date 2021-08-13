using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Gestion.ModelPeriodoContable
{
    public class ModelPeriodoContable: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public string OldCode1 { get => OldCode; set => OldCode = value; }
        public int UserSign2 { get => userSign2; set => userSign2 = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }

        private string OldCode;

        private string periodCat;

        private string periodName;

        private string f_RefDate;

        private string t_RefDate;

        private string f_TaxDate;

        private string t_TaxDate;

        private string f_DueDate;

        private string t_DueDate;

        private string periodStat;

        private string financYear;

        private int userSign2;

        private DateTime? updateDate;

        private string year;

        private List<Entidades.EstadosPeriodos> estadosPeriodos;

        public List<Entidades.EstadosPeriodos> EstadosPeriodos
        {
            get
            {
                return estadosPeriodos;
            }

            set
            {
                if (value != estadosPeriodos)
                {
                    estadosPeriodos = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("EstadosPeriodos");
                }
            }
        }

        public string PeriodStat
        {
            get
            {
                return periodStat;
            }

            set
            {
                if (value != periodStat)
                {
                    periodStat = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PeriodStat");
                }
            }
        }

        public string FinancYear
        {
            get
            {
                return financYear;
            }

            set
            {
                if (value != financYear)
                {
                    financYear = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("FinancYear");
                }
            }
        }

        public string PeriodCat
        {
            get
            {
                return periodCat;
            }

            set
            {
                if (value != periodCat)
                {
                    periodCat = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PeriodCat");
                }
            }
        }

        public string F_RefDate
        {
            get
            {
                return f_RefDate;
            }

            set
            {
                if (value != f_RefDate)
                {
                    f_RefDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("F_RefDate");
                }
            }
        }

        public string T_RefDate
        {
            get
            {
                return t_RefDate;
            }

            set
            {
                if (value != t_RefDate)
                {
                    t_RefDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("T_RefDate");
                }
            }
        }

        public string F_TaxDate
        {
            get
            {
                return f_TaxDate;
            }

            set
            {
                if (value != f_TaxDate)
                {
                    f_TaxDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("F_TaxDate");
                }
            }
        }

        public string T_TaxDate
        {
            get
            {
                return t_TaxDate;
            }

            set
            {
                if (value != t_TaxDate)
                {
                    t_TaxDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("T_TaxDate");
                }
            }
        }

        public string T_DueDate
        {
            get
            {
                return t_DueDate;
            }

            set
            {
                if (value != t_DueDate)
                {
                    t_DueDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("T_DueDate");
                }
            }
        }

        public string F_DueDate
        {
            get
            {
                return f_DueDate;
            }

            set
            {
                if (value != f_DueDate)
                {
                    f_DueDate = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("F_DueDate");
                }
            }
        }

        public string Year
        {
            get
            {
                return year;
            }

            set
            {
                if (value != year)
                {
                    year = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("Year");
                }
            }
        }

        public string PeriodName
        {
            get
            {
                return periodName;
            }

            set
            {
                if (value != periodName)
                {
                    periodName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PeriodName");
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
                    case "PeriodCat":
                        if (string.IsNullOrWhiteSpace(PeriodCat))
                            result = "Codigo de periodo no puede estar vacio";
                        else if (PeriodCat.Length > 7)
                            result = "Codigo de periodo no debe exceder la cantidad de caracteres de 7";
                        //else if (IntegerValidator(PeriodCat) == false)
                        //    result = "Codigo de periodo debe especificar el año en formato 20XX";
                        
                        break;

                    case "PeriodName":
                        if (string.IsNullOrWhiteSpace(PeriodName))
                            result = "Nombre de periodo no puede estar vacio";
                        else if (PeriodName.Length > 7)
                            result = "Nombre de periodo no debe exceder la cantidad de caracteres de 4";
                        //else if (IntegerValidator(PeriodName) == false)
                        //    result = "Nombre de periodo debe especificar el año en formato 20XX";
                      
                        else if (PeriodCat != PeriodName)
                            result = "Nombre de periodo debe ser igual a codigo de periodo";
                        break;

                    case "FinancYear":
                        if (string.IsNullOrWhiteSpace(FinancYear))
                            result = "Ejercicio de periodo no puede estar vacio";                      
                     
                        else if (FinancYear != F_RefDate)
                            result = "Ejercicio de periodo debe ser igual a fecha de contabilizacion desde";
                        break;


                    case "F_RefDate":
                        if (string.IsNullOrWhiteSpace(F_RefDate))
                            result = "Fecha de contabilizacion desde no puede estar vacio";    
                        break;

                    case "T_RefDate":
                        if (string.IsNullOrWhiteSpace(T_RefDate))
                            result = "Fecha de contabilizacion hasta no puede estar vacio";
                        break;

                    case "F_TaxDate":
                        if (string.IsNullOrWhiteSpace(F_TaxDate))
                            result = "Fecha de documento desde no puede estar vacio";
                        break;

                    case "T_TaxDate":
                        if (string.IsNullOrWhiteSpace(T_TaxDate))
                            result = "Fecha de documento hasta no puede estar vacio";
                        break;

                    case "F_DueDate":
                        if (string.IsNullOrWhiteSpace(F_DueDate))
                            result = "Fecha de vencimiento desde no puede estar vacio";
                        break;

                    case "T_DueDate":
                        if (string.IsNullOrWhiteSpace(T_DueDate))
                            result = "Fecha de contabilizacion desde no puede estar vacio";
                        break;

                    case "Year":
                        if (string.IsNullOrWhiteSpace(Year))
                            result = "Ejercicio no puede estar vacio";
                        else if (Year.Length > 4)
                            result = "Ejercicio no debe exceder la cantidad de caracteres de 4";
                        else if (Year.Length != 4)
                            result = "Ejercicio debe tener una cantidad de caracteres de 4";
                        //else if (Year!= PeriodCat)
                        //    result = "Ejercicio debe ser igual al codigo del periodo";
                        break;

                    case "PeriodStat":
                        if (string.IsNullOrWhiteSpace(PeriodStat))
                        {

                            result = "Estado de periodo no puede estar vacio";
                        }
                        break;


                }


                return result;
            }
        }
    }
}
