using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Vista.Clases_Basicas;

namespace Vista.Inventario.ModelArticulos
{
    public class ModelArticulos: INotifyPropertyChangeObservable, IDataErrorInfo
    {
        public string Error { get { return null; } }

        private string oldItemCode;

        private string itemCode;

        private string itemName;

        private bool vatLiable;

        private bool prchseItem;

        private bool sellItem;

        private bool invnItem;

        private decimal onHand;

        private decimal isCommited;

        private decimal onOrders;

        private char deleted;

        private int userSign;

        private DateTime? UpdateDate;

        private int docEntry;

        private decimal stockValue;

        private string balInvntAc;

        private string saleCostAc;

        private string revenuesAc;

        private string expensesAc;

        private string transferAc;

        private string evalSystem;

        private string ugpEntry;

        private string ugpCode;

        private string uomCode;

        private string uomName;

        private string selectedEvalSystem;

        private string selectedGrupoUnidad;

        private string numInCnt;

        private string invntryUomName;

        public string OldItemCode { get => oldItemCode; set => oldItemCode = value; }

        private List<Entidades.MetodoValoracion> metodoValoracion;

        private List<Entidades.GrupoUnidadesMedidaCabecera> grupoUnidad;

        private string selectedNombreUnidad;

        private List<UnidadesMedida> nombreUnidad;

        public string UgpEntry
        {
            get
            {
                return ugpEntry;
            }

            set
            {
                if (value != ugpEntry)
                {
                    ugpEntry = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("UgpEntry");
                }
            }
        }

        public string InvntryUomName
        {
            get
            {
                return invntryUomName;
            }

            set
            {
                if (value != invntryUomName)
                {
                    invntryUomName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("InvntryUomName");
                }
            }
        }

        public string NumInCnt
        {
            get
            {
                return numInCnt;
            }

            set
            {
                if (value != numInCnt)
                {
                    numInCnt = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("NumInCnt");
                }
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }

            set
            {
                if (value != itemCode)
                {
                    itemCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ItemCode");
                }
            }
        }

        public string UgpCode
        {
            get
            {
                return ugpCode;
            }

            set
            {
                if (value != ugpCode)
                {
                    ugpCode = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("UgpCode");
                }
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

        public string EvalSystem
        {
            get
            {
                return evalSystem;
            }

            set
            {
                if (value != evalSystem)
                {
                    evalSystem = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("evalSystem");
                }
            }
        }

        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                if (value != itemName)
                {
                    itemName = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ItemName");
                }
            }
        }

        public bool VatLiable
        {
            get
            {
                return vatLiable;
            }

            set
            {
                if (value != vatLiable)
                {
                    vatLiable = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("VatLiable");
                }
            }
        }

        public bool PrchseItem
        {
            get
            {
                return prchseItem;
            }

            set
            {
                if (value != prchseItem)
                {
                    prchseItem = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("PrchseItem");
                }
            }
        }

        public bool SellItem
        {
            get
            {
                return sellItem;
            }

            set
            {
                if (value != sellItem)
                {
                    sellItem = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SellItem");
                }
            }
        }

        public bool InvnItem
        {
            get
            {
                return invnItem;
            }

            set
            {
                if (value != invnItem)
                {
                    invnItem = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("InvnItem");
                }
            }
        }

        public List<Entidades.MetodoValoracion> MetodoValoracion
        {
            get
            {
                return metodoValoracion;
            }

            set
            {
                if (value != metodoValoracion)
                {
                    metodoValoracion = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("MetodoValoracion");
                }
            }
        }

        public List<Entidades.GrupoUnidadesMedidaCabecera> GrupoUnidad
        {
            get
            {
                return grupoUnidad;
            }

            set
            {
                if (value != grupoUnidad)
                {
                    grupoUnidad = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("GrupoUnidad");
                }
            }
        }

        public List<Entidades.UnidadesMedida> NombreUnidad
        {
            get
            {
                return nombreUnidad;
            }

            set
            {
                if (value != nombreUnidad)
                {
                    nombreUnidad = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("NombreUnidad");
                }
            }
        }
        public string SelectedEvalSystem
        {
            get
            {
                return selectedEvalSystem;
            }

            set
            {
                if (value != selectedEvalSystem)
                {
                    selectedEvalSystem = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedEvalSystem");
                }
            }
        }

        public string SelectedGrupoUnidad
        {
            get
            {
                return selectedGrupoUnidad;
            }

            set
            {
                if (value != selectedGrupoUnidad)
                {
                    selectedGrupoUnidad = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedGrupoUnidad");
                }
            }
        }

        public string SelectedNombreUnidad
        {
            get
            {
                return selectedNombreUnidad;
            }

            set
            {
                if (value != selectedNombreUnidad)
                {
                    selectedNombreUnidad = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SelectedNombreUnidad");
                }
            }
        }

        public string BalInvntAc
        {
            get
            {
                return balInvntAc;
            }

            set
            {
                if (value != balInvntAc)
                {
                    balInvntAc = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("BalInvntAc");
                }
            }
        }

        public string SaleCostAc
        {
            get
            {
                return saleCostAc;
            }

            set
            {
                if (value != saleCostAc)
                {
                    saleCostAc = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("SaleCostAc");
                }
            }
        }

        public string RevenuesAc
        {
            get
            {
                return revenuesAc;
            }

            set
            {
                if (value != revenuesAc)
                {
                    revenuesAc = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("RevenuesAc");
                }
            }
        }

        public string ExpensesAc
        {
            get
            {
                return expensesAc;
            }

            set
            {
                if (value != expensesAc)
                {
                    expensesAc = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("ExpensesAc");
                }
            }
        }

        public string TransferAc
        {
            get
            {
                return transferAc;
            }

            set
            {
                if (value != transferAc)
                {
                    transferAc = value;
                    //notify the binding that my value has been changed
                    OnPropertyChanged("TransferAc");
                }
            }
        }

        public string this[string name]
        {
            get
            {
                string result =null;

                //switch (name)
                //{
                //    case "CompnyName":
                //        if (string.IsNullOrWhiteSpace(CompnyName))
                //            result = "Nombre de sociedad no puede estar vacio";
                //        else if (CompnyName.Length > 100)
                //            result = "Nombre de sociedad no debe exceder la cantidad de caracteres de 100";

                //        break;

                //    case "CompnyAddr":
                //        if (string.IsNullOrWhiteSpace(CompnyAddr))
                //            result = "Direccion no puede estar vacio";
                //        else if (CompnyAddr.Length > 254)
                //            result = "Direccion no debe exceder la cantidad de caracteres de 254";
                //        break;


                //    case "PrintHeadr":
                //        if (string.IsNullOrWhiteSpace(PrintHeadr))
                //            result = "Cabecera de impresion no puede estar vacio";
                //        else if (PrintHeadr.Length > 100)
                //            result = "Cabecera de impresion no debe exceder la cantidad de caracteres de 100";


                //        break;

                //    case "Phone1":
                //        if (string.IsNullOrWhiteSpace(Phone1))
                //            result = "Telefono 1 no puede estar vacio";
                //        else if (Phone1.Length > 20)
                //            result = "Telefono 1 no debe exceder la cantidad de caracteres de 20";
                //        else if (IsPhoneNumberValidator(Phone1) == false)
                //            result = "Telefono 1 no tiene formato requerido";


                //        break;

                //    case "Phone2":
                //        if (string.IsNullOrWhiteSpace(Phone2) == false)
                //        {
                //            if (Phone2.Length > 20)
                //            {
                //                result = "Telefono 2 no debe exceder la cantidad de caracteres de 20";
                //            }
                //            else if (IsPhoneNumberValidator(Phone2) == false)
                //                result = "Telefono 2 no tiene formato requerido";

                //        }


                //        break;

                //    case "Fax":

                //        if (string.IsNullOrWhiteSpace(Fax) == false)
                //        {
                //            if (Fax.Length > 20)
                //            {
                //                result = "Fax no debe exceder la cantidad de caracteres de 20";
                //            }
                //            else if (FaxValidator(Fax) == false)
                //                result = "Fax no tiene formato requerido";
                //        }


                //        break;

                //    case "ZipCode":
                //        if (string.IsNullOrWhiteSpace(ZipCode))
                //            result = "ZipCode no puede estar vacio";
                //        else if (IntegerValidator(ZipCode) == false)
                //            result = "ZipCode no tiene formato requerido";

                //        break;

                //    case "E_Mail":
                //        if (string.IsNullOrWhiteSpace(E_Mail))
                //            result = "Correo no puede estar vacio";
                //        else if (E_Mail.Length > 100)
                //            result = "Correo no debe exceder la cantidad de caracteres de 100";
                //        else if (EmailValidator(E_Mail) == false)
                //            result = "Correo no tiene formato requerido";


                //        break;

                //    case "SysCurrncy":
                //        if (string.IsNullOrWhiteSpace(SysCurrncy))
                //            result = "Moneda Sistema no puede estar vacio";
                //        else if (SysCurrncy.Length > 3)
                //            result = "Moneda Sistema no debe exceder la cantidad de caracteres de 3";


                //        break;

                //    case "MainCurncy":
                //        if (string.IsNullOrWhiteSpace(MainCurncy))
                //            result = "Moneda Local no puede estar vacio";
                //        else if (MainCurncy.Length > 3)
                //            result = "Moneda Local no debe exceder la cantidad de caracteres de 3";


                //        break;

                //    case "TaxIdNum":
                //        if (string.IsNullOrWhiteSpace(TaxIdNum))
                //            result = "RIF no puede estar vacio";
                //        else if (TaxIdNum.Length > 32)
                //            result = "RIF no debe exceder la cantidad de caracteres de 32";
                //        else if (RifValidator(TaxIdNum) == false)
                //            result = "RIF no tiene el formato requerido";
                //        break;


                //    case "RevOffice":
                //        if (string.IsNullOrWhiteSpace(RevOffice) == false)
                //        {
                //            if (RevOffice.Length > 100)
                //            {
                //                result = "Hacienda no debe exceder la cantidad de caracteres de 100";
                //            }

                //        }
                //        break;

                //    case "SelectedCurrCodeMain":
                //        if (string.IsNullOrWhiteSpace(SelectedCurrCodeMain))
                //        {

                //            result = "Moneda no puede estar vacia";
                //        }
                //        break;

                //    case "SelectedCurrCodeSys":
                //        if (string.IsNullOrWhiteSpace(SelectedCurrCodeSys))
                //        {

                //            result = "Moneda no puede estar vacia";
                //        }
                //        break;

                //}


                return result;
            }
        }

        public decimal OnHand { get => onHand; set => onHand = value; }
        public decimal IsCommited { get => isCommited; set => isCommited = value; }
        public decimal OnOrders { get => onOrders; set => onOrders = value; }
        public char Deleted { get => deleted; set => deleted = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime? UpdateDate1 { get => UpdateDate; set => UpdateDate = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public decimal StockValue { get => stockValue; set => stockValue = value; }      
    
       
    }
}
