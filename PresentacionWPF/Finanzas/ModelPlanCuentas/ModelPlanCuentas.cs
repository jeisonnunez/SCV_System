using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vista.Clases_Basicas;

namespace Vista.Finanzas
{
    public class ModelPlanCuentas: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private List<Entidades.Monedas> monedas;

        private List<Entidades.TipoCuenta> tipocuenta;

        private string selectedActType;

        private string acctCode;

        private string oldAcctCode;

        private string acctName;

        private string currTotal;

        private char finanse;

        private bool postable;

        private string levels;

        private string grpLine;

        private string actType;

        private string selectedCurrCode;

        private string sysTotal;

        private string fcTotal;

        private char advance;

        private DateTime createDate;

        private int userSign;

        private bool locManTran;

        private string fatherNum;

        private string actCurr;

        private int groupMask;

        public string AcctCode
        {
            get { return acctCode; }
            set
            {
                OnPropertyChanged(ref acctCode, value);
            }
        }

        public bool Postable
        {
            get { return postable; }
            set
            {
                OnPropertyChanged(ref postable, value);
            }
        }

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

        public List<Entidades.TipoCuenta> TipoCuenta
        {
            get
            {
                return tipocuenta;
            }

            set
            {
                if (value != tipocuenta)
                {
                    tipocuenta = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("TipoCuenta");
                }
            }
        }

        public string SelectedCurrCode
        {
            get { return selectedCurrCode; }
            set
            {
                OnPropertyChanged(ref selectedCurrCode, value);
            }
        }

        public string SelectedActType
        {
            get { return selectedActType; }
            set
            {
                OnPropertyChanged(ref selectedActType, value);
            }
        }

        public string AcctName
        {
            get { return acctName; }
            set
            {
                OnPropertyChanged(ref acctName, value);
            }
        }

        public string ActCurr
        {
            get { return actCurr; }
            set
            {
                OnPropertyChanged(ref actCurr, value);
            }
        }

        public string CurrTotal
        {
            get { return currTotal; }
            set
            {
                OnPropertyChanged(ref currTotal, value);
            }
        }

        public bool LocManTran
        {
            get { return locManTran; }
            set
            {
                OnPropertyChanged(ref locManTran, value);
            }
        }

        public string Levels
        {
            get { return levels; }
            set
            {
                OnPropertyChanged(ref levels, value);
            }
        }

        public string ActType
        {
            get { return actType; }
            set
            {
                OnPropertyChanged(ref actType, value);
            }
        }

        public char Finanse { get => finanse; set => finanse = value; }
        
       
        public string GrpLine { get => grpLine; set => grpLine = value; }
        public string SysTotal { get => sysTotal; set => sysTotal = value; }
        public string FcTotal { get => fcTotal; set => fcTotal = value; }
        public char Advance { get => advance; set => advance = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
       
        public string FatherNum { get => fatherNum; set => fatherNum = value; }
        
        public int GroupMask { get => groupMask; set => groupMask = value; }

        public string OldAcctCode { get => oldAcctCode; set => oldAcctCode = value; }

        public static Dictionary<string, string> ErrorCollectionMessages { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string name]
        {
            get
            {
                string result = null;

                switch (name)
                {
                    case "AcctCode":
                        if (string.IsNullOrWhiteSpace(AcctCode))
                            result = "Cuenta no puede estar vacio";
                        else if (AcctCode.Length > 15)
                            result = "Cuenta no debe exceder la cantidad de caracteres de 15";
                        break;

                    case "AcctName":
                        if (string.IsNullOrWhiteSpace(AcctName))
                            result = "Nombre no puede estar vacio";
                        else if (AcctCode.Length > 100)
                            result = "Nombre no debe exceder la cantidad de caracteres de 100";
                        break;

                    case "Levels":
                        if (string.IsNullOrWhiteSpace(Levels))
                            result = "Nivel no puede estar vacio";                       
                        break;

                    case "ActType":
                        if (string.IsNullOrWhiteSpace(ActType))
                            result = "Tipo de cuenta no puede estar vacio";
                        break;
                   
                    case "SelectedCurrCode":
                        if (string.IsNullOrWhiteSpace(SelectedCurrCode))
                            result = "Moneda no puede estar vacio";
                        break;

                    case "SelectedActType":
                        if (string.IsNullOrWhiteSpace(SelectedActType))
                            result = "Tipo cuenta no puede estar vacio";
                        break;


                }

                if (ErrorCollection.ContainsKey(name))
                {
                    ErrorCollection[name] = result;
                }
                else if (result != null)
                {
                    ErrorCollection.Add(name, result);


                }

                OnPropertyChanged("ErrorCollection");

                ErrorCollectionMessages = ErrorCollection;

                return result;
            }
        }
    }
}
