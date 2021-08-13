using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Vista;
using Negocio;
using Entidades;
using System.Text.RegularExpressions;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TablaRetencionImpuesto.xaml
    /// </summary>
    public partial class TablaRetencionImpuesto : Document
    {
        ControladorTablaRetencionImpuesto cn = new ControladorTablaRetencionImpuesto();

        ControladorRetencionImpuesto cr = new ControladorRetencionImpuesto();

        public static DataTable dt = new DataTable();

        DataTable dtRetenciones = new DataTable();

        public static DataGrid dg;

        private decimal baseAmnt;

        private decimal baseAmntIVA;

        private string currency;

        private string str;

        private decimal rate;

        private decimal rateFC;

        private static decimal wtAmnt=0;

        private static decimal wtAmntFC=0;

        private static decimal wtAmntSC=0;

        private static int cont = 0;
        public decimal BaseAmnt { get => baseAmnt; set => baseAmnt = value; }
        public decimal BaseAmntIVA { get => baseAmntIVA; set => baseAmntIVA = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Str { get => str; set => str = value; }
        public decimal Rate { get => rate; set => rate = value; }
        public decimal RateFC { get => rateFC; set => rateFC = value; }
        public static decimal WtAmnt { get => wtAmnt; set => wtAmnt = value; }
        public static decimal WtAmntFC { get => wtAmntFC; set => wtAmntFC = value; }
        public static decimal WtAmntSC { get => wtAmntSC; set => wtAmntSC = value; }
        public static int Cont { get => cont; set => cont = value; }

        public void LoadTablaRetenciones(string baseAmnt, string baseAmntIVA, string currency, decimal Rate, decimal RateFC)
        {
            Str = regex.Replace(baseAmnt, String.Empty);

            this.BaseAmnt = ConvertDecimalTwoPlaces(Str);

            Str = regex.Replace(baseAmntIVA, String.Empty);

            this.BaseAmntIVA = ConvertDecimalTwoPlaces(Str);

            this.Currency = currency;

            this.Rate = Rate;

            this.RateFC = RateFC;

           
        }

        public void LoadDatagrid(string currency)
        {
            if (currency == Properties.Settings.Default.MainCurrency)
            {
                importeSujeto.Visibility = Visibility.Visible;
                wimporte.Visibility = Visibility.Visible;
                importebase.Visibility = Visibility.Visible;
                importebaseFC.Visibility = Visibility.Hidden;
                importeSujetoFC.Visibility = Visibility.Hidden;
                wimporteFC.Visibility = Visibility.Hidden;

            }
            else
            {
                importeSujeto.Visibility = Visibility.Hidden;
                wimporte.Visibility = Visibility.Hidden;
                importebase.Visibility = Visibility.Hidden;
                importebaseFC.Visibility = Visibility.Visible;
                importeSujetoFC.Visibility = Visibility.Visible;
                wimporteFC.Visibility = Visibility.Visible;
            }
        }

        public TablaRetencionImpuesto()
        {
            InitializeComponent();

            LoadTable();

        }

        private void LoadTable()
        {
            
            dt.Columns.Add("AbsEntry");
            dt.Columns.Add("WTCode");
            dt.Columns.Add("WTName");
            dt.Columns.Add("Type");
            dt.Columns.Add("Rate");
            dt.Columns.Add("BaseAmnt");
            dt.Columns.Add("BaseAmntSC");
            dt.Columns.Add("BaseAmntFC");
            dt.Columns.Add("TaxbleAmnt");
            dt.Columns.Add("TaxbleAmntFC");
            dt.Columns.Add("TaxbleAmntSC");
            dt.Columns.Add("WTAmnt");
            dt.Columns.Add("WTAmntSC");
            dt.Columns.Add("WTAmntFC");
            dt.Columns.Add("Category");
            dt.Columns.Add("BaseType");
            dt.Columns.Add("Account");
            dt.Columns.Add("PrctBsAmnt");
            dt.Columns.Add("LineNum");
            dt.Columns.Add("Status");
            dt.Columns.Add("ObjType");

            dt.NewRow();
           
            dgRetenciones.ItemsSource = dt.DefaultView;

        }

        private void imgWTCode_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                var row_list = GetDataGridRows(dgRetenciones);

                DataRowView row_Selected = dgRetenciones.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {

                        TextBlock txtBaseAmnt = new TextBlock();

                        TextBlock txtTaxbleAmnt = new TextBlock();

                        TextBlock txtWTAmnt = new TextBlock();

                        TextBox txtWTCode = FindChild<TextBox>(single_row, "txtWTCode");

                        TextBlock txtWTName = FindChild<TextBlock>(single_row, "txtWTName");
                       
                        TextBlock txtRate = FindChild<TextBlock>(single_row, "txtRate");

                        if (Currency == Properties.Settings.Default.MainCurrency)
                        {
                             txtBaseAmnt = FindChild<TextBlock>(single_row, "txtBaseAmnt");

                             txtTaxbleAmnt = FindChild<TextBlock>(single_row, "txtTaxbleAmnt");

                             txtWTAmnt = FindChild<TextBlock>(single_row, "txtWTAmnt");
                        }
                        else
                        {
                            txtBaseAmnt = FindChild<TextBlock>(single_row, "txtBaseAmntFC");

                            txtTaxbleAmnt = FindChild<TextBlock>(single_row, "txtTaxbleAmntFC");

                            txtWTAmnt = FindChild<TextBlock>(single_row, "txtWTAmntFC");
                        }
                                                

                        TextBlock txtCategory = FindChild<TextBlock>(single_row, "txtCategory");

                        TextBlock txtBaseType = FindChild<TextBlock>(single_row, "txtBaseType");

                        TextBlock txtAccount = FindChild<TextBlock>(single_row, "txtAccount");

                        var result = cn.FindHoldingTax();

                        if (result.Item2 == null)
                        {
                            RecorreListAccount(result.Item1, txtWTCode, txtWTName, txtRate, txtBaseAmnt, txtTaxbleAmnt, txtWTAmnt, txtCategory, txtBaseType, txtAccount, row_Selected);
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }

                    }
                }
            }

            catch(Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }

        }

        private void RecorreListAccount(List<Entidades.RetencionImpuesto> listTaxHolding, TextBox txtWTCode, TextBlock txtWTName, TextBlock txtRate, TextBlock txtBaseAmnt, TextBlock txtTaxbleAmnt, TextBlock txtWTAmnt, TextBlock txtCategory, TextBlock txtBaseType, TextBlock txtAccount, DataRowView row_Selected)
        {
            if (listTaxHolding.Count == 0)
            {                
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listTaxHolding.Count > 0)
            {
                ListaRetencionImpuesto ventanaListaCuentaAsociada = new ListaRetencionImpuesto(listTaxHolding);

                ventanaListaCuentaAsociada.ShowDialog();

                if (ventanaListaCuentaAsociada.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCuentaAsociada.GetListRetencionImpuesto().Count == 0)
                    {

                    }
                    else
                    {
                        GetRetencionImpuesto(ventanaListaCuentaAsociada.GetListRetencionImpuesto(), txtWTCode, txtWTName, txtRate, txtBaseAmnt, txtTaxbleAmnt, txtWTAmnt, txtCategory, txtBaseType, txtAccount,row_Selected);
                    }
                }
            }
        }

        private void GetRetencionImpuesto(List<Entidades.RetencionImpuesto> listRetencion, TextBox txtWTCode, TextBlock txtWTName, TextBlock txtRate, TextBlock txtBaseAmnt, TextBlock txtTaxbleAmnt, TextBlock txtWTAmnt, TextBlock txtCategory, TextBlock txtBaseType, TextBlock txtAccount, DataRowView row_Selected)
        {

            decimal rate = 0;

            decimal baseAmnt = 0;

            decimal baseAmntFC = 0;

            decimal baseAmntSC = 0;

            decimal PrctBsAmnt = 0;

            decimal TaxbleAmnt = 0;

            decimal TaxbleAmntSC = 0;

            decimal TaxbleAmntFC = 0;

            decimal WTAmnt = 0;

            decimal WTAmntSC = 0;

            decimal WTAmntFC = 0;          

            foreach (Entidades.RetencionImpuesto retencionImpuesto in listRetencion)
            {
                if (Currency==Properties.Settings.Default.MainCurrency) {                      

                    txtWTCode.Text = retencionImpuesto.Wt_Code;

                    row_Selected["WTName"] = retencionImpuesto.Wt_Name;                

                    txtWTName.Text = row_Selected["WTName"].ToString();

                    row_Selected["Rate"] = retencionImpuesto.Rate;

                    rate = ConvertDecimalTwoPlaces(row_Selected["Rate"]);

                    txtRate.Text = rate.ToString();

                    row_Selected["BaseType"] = cr.GetBaseType(retencionImpuesto.BaseType);

                    txtBaseType.Text = row_Selected["BaseType"].ToString();

                    row_Selected["Category"] = cr.GetCategoria(retencionImpuesto.Category);

                    txtCategory.Text = row_Selected["Category"].ToString();

                    row_Selected["Account"] = retencionImpuesto.Account;

                    txtAccount.Text = row_Selected["Account"].ToString();

                    if (row_Selected["BaseType"].ToString() == "IVA")
                    {

                        row_Selected["BaseAmnt"] = BaseAmntIVA;

                        row_Selected["BaseAmnt"] = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        baseAmnt = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        row_Selected["BaseAmntSC"] = baseAmnt / Rate;

                        row_Selected["TaxbleAmnt"] = baseAmnt;

                        TaxbleAmnt = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmnt"]);

                        row_Selected["TaxbleAmntSC"] = row_Selected["BaseAmntSC"];

                        row_Selected["WTAmnt"] = baseAmnt * (Convert.ToDecimal(row_Selected["Rate"]) / 100);

                        row_Selected["WTAmnt"] = ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        WTAmnt = ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        row_Selected["WTAmntSC"] = WTAmnt / Rate;

                        row_Selected["WTAmntFC"] = 0;

                        row_Selected["BaseAmntFC"] = 0;

                        row_Selected["TaxbleAmntFC"] = 0;

                        txtBaseAmnt.Text = Currency + " " + baseAmnt.ToString();

                        txtWTAmnt.Text = Currency + " " + WTAmnt.ToString();

                        txtTaxbleAmnt.Text = Currency + " " + TaxbleAmnt.ToString();

                    }
                    else
                    {

                        row_Selected["PrctBsAmnt"] = retencionImpuesto.PrctBsAmnt;

                        row_Selected["PrctBsAmnt"] = ConvertDecimalTwoPlaces(row_Selected["PrctBsAmnt"]);

                        PrctBsAmnt = ConvertDecimalTwoPlaces(row_Selected["PrctBsAmnt"]);

                        row_Selected["BaseAmnt"] = BaseAmnt;

                        row_Selected["BaseAmnt"] = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        baseAmnt = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        row_Selected["BaseAmntSC"] = baseAmnt / Rate;

                        str = regex.Replace(row_Selected["BaseAmnt"].ToString(), String.Empty);

                        baseAmnt = ConvertDecimalTwoPlaces(str);

                        row_Selected["TaxbleAmnt"] = ((baseAmnt) * ((PrctBsAmnt) / 100));

                        TaxbleAmnt = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmnt"]);

                        row_Selected["TaxbleAmntSC"] = TaxbleAmnt / Rate;

                        row_Selected["WTAmnt"] = row_Selected["TaxbleAmnt"];

                        WTAmnt = ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        row_Selected["WTAmntSC"] = row_Selected["TaxbleAmntSC"];

                        row_Selected["WTAmntFC"] = 0;

                        row_Selected["BaseAmntFC"] = 0;

                        row_Selected["TaxbleAmntFC"] = 0;

                        txtBaseAmnt.Text = Currency + " " + baseAmnt.ToString();

                        txtWTAmnt.Text = Currency + " " + WTAmnt.ToString();

                        txtTaxbleAmnt.Text = Currency + " " + TaxbleAmnt.ToString();
                    }                   
                }
                else
                {
                    txtWTCode.Text = retencionImpuesto.Wt_Code;

                    row_Selected["WTName"] = retencionImpuesto.Wt_Name;

                    txtWTName.Text = row_Selected["WTName"].ToString();

                    row_Selected["Rate"] = retencionImpuesto.Rate;

                    rate = ConvertDecimalTwoPlaces(row_Selected["Rate"]);

                    txtRate.Text = rate.ToString();

                    row_Selected["BaseType"] = cr.GetBaseType(retencionImpuesto.BaseType);

                    txtBaseType.Text = row_Selected["BaseType"].ToString();

                    row_Selected["Category"] = cr.GetCategoria(retencionImpuesto.Category);

                    txtCategory.Text = row_Selected["Category"].ToString();

                    row_Selected["Account"] = retencionImpuesto.Account;

                    txtAccount.Text = row_Selected["Account"].ToString();

                    if (row_Selected["BaseType"].ToString() == "IVA")
                    {

                        row_Selected["BaseAmntFC"] = BaseAmntIVA;

                        row_Selected["BaseAmntFC"] = ConvertDecimalTwoPlaces(row_Selected["BaseAmntFC"]);

                        baseAmntFC = ConvertDecimalTwoPlaces(row_Selected["BaseAmntFC"]);

                        row_Selected["BaseAmnt"] = baseAmntFC * RateFC;

                        baseAmnt = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        row_Selected["TaxbleAmntFC"] = baseAmntFC;

                        TaxbleAmntFC = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmntFC"]);

                        row_Selected["TaxbleAmnt"] = baseAmnt;

                        row_Selected["BaseAmntSC"] = baseAmnt / Rate;                      

                        TaxbleAmnt = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmnt"]);

                        row_Selected["TaxbleAmntSC"] = row_Selected["BaseAmntSC"];

                        row_Selected["WTAmntFC"] = baseAmntFC * (Convert.ToDecimal(row_Selected["Rate"]) / 100);

                        row_Selected["WTAmntFC"] = ConvertDecimalTwoPlaces(row_Selected["WTAmntFC"]);

                        WTAmntFC = ConvertDecimalTwoPlaces(row_Selected["WTAmntFC"]);

                        row_Selected["WTAmnt"] = WTAmntFC * RateFC;

                        row_Selected["WTAmnt"] = ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        WTAmnt= ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        row_Selected["WTAmntSC"] = WTAmnt / Rate;                       

                        txtBaseAmnt.Text = Currency + " " + baseAmntFC.ToString();

                        txtWTAmnt.Text = Currency + " " + WTAmntFC.ToString();

                        txtTaxbleAmnt.Text = Currency + " " + TaxbleAmntFC.ToString();

                    }
                    else
                    {

                        row_Selected["PrctBsAmnt"] = retencionImpuesto.PrctBsAmnt;

                        row_Selected["PrctBsAmnt"] = ConvertDecimalTwoPlaces(row_Selected["PrctBsAmnt"]);

                        PrctBsAmnt = ConvertDecimalTwoPlaces(row_Selected["PrctBsAmnt"]);

                        row_Selected["BaseAmntFC"] = BaseAmnt;

                        row_Selected["BaseAmntFC"] = ConvertDecimalTwoPlaces(row_Selected["BaseAmntFC"]);

                        baseAmntFC = ConvertDecimalTwoPlaces(row_Selected["BaseAmntFC"]);

                        str = regex.Replace(row_Selected["BaseAmntFC"].ToString(), String.Empty);

                        baseAmntFC = ConvertDecimalTwoPlaces(str);

                        row_Selected["BaseAmnt"] = baseAmntFC * RateFC;

                        row_Selected["BaseAmnt"] = ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        baseAmnt= ConvertDecimalTwoPlaces(row_Selected["BaseAmnt"]);

                        row_Selected["BaseAmntSC"] = baseAmnt / Rate;

                        row_Selected["TaxbleAmntFC"] = ((baseAmntFC) * ((PrctBsAmnt) / 100));

                        TaxbleAmntFC = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmntFC"]);

                        row_Selected["TaxbleAmnt"] = TaxbleAmntFC * RateFC;

                        TaxbleAmnt = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmnt"]);

                        row_Selected["WTAmntFC"] = TaxbleAmntFC;

                        WTAmntFC = ConvertDecimalTwoPlaces(row_Selected["WTAmntFC"]);

                        row_Selected["WTAmnt"] = TaxbleAmnt;

                        WTAmnt = ConvertDecimalTwoPlaces(row_Selected["WTAmnt"]);

                        row_Selected["TaxbleAmntSC"] = TaxbleAmnt / Rate;

                        row_Selected["TaxbleAmntSC"] = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmntSC"]);

                        TaxbleAmntSC = ConvertDecimalTwoPlaces(row_Selected["TaxbleAmntSC"]);

                        row_Selected["WTAmntSC"] = TaxbleAmntSC;                       

                        txtBaseAmnt.Text = Currency + " " + baseAmntFC.ToString();

                        txtWTAmnt.Text = Currency + " " + WTAmntFC.ToString();

                        txtTaxbleAmnt.Text = Currency + " " + TaxbleAmntFC.ToString();
                    }
                }
            }

            btnCrear.Content = "Actualizar";
        }

        public void SetRetenciones(DataTable dt)
        {            
            dgRetenciones.ItemsSource = dt.DefaultView;

            dgRetenciones.CanUserAddRows = false;

            dgRetenciones.CanUserDeleteRows = false;

            dgRetenciones.CanUserSortColumns = false;
        }

        public void ClearRetencionesImpuesto()
        {
            dt.Rows.Clear();

            dgRetenciones.ItemsSource = dt.DefaultView;

            dgRetenciones.CanUserAddRows = true;

            dgRetenciones.CanUserDeleteRows = true;

            dgRetenciones.CanUserSortColumns = true;
        }

        private void Document_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {
            switch (btnCrear.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Actualizar":

                    CalculateWtSum(dt);

                    btnCrear.Content = "OK";

                    break;
            }
        }

        public static decimal GetWtAmnt()
        {
            return WtAmnt;
        }

        public static decimal GetWtAmntFC()
        {
            return WtAmntFC;
        }

        private void CalculateWtSum(DataTable dt)
        {
            decimal wtSumVar = 0;

            decimal wtSumFCVar = 0;

            decimal wtSumSCVar = 0;
           
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "WTAmnt")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmnt"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmnt"].ToString(), String.Empty);

                            wtSumVar = ConvertDecimalTwoPlaces(str);

                            WtAmnt = WtAmnt + wtSumVar;
                        }
                    }

                    else if(column.ToString() == "WTAmntFC")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmntFC"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmntFC"].ToString(), String.Empty);

                            wtSumFCVar= ConvertDecimalTwoPlaces(str);

                            WtAmntFC = WtAmntFC + wtSumFCVar;
                        }
                    }

                    else if (column.ToString() == "WTAmntSC")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmntSC"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmntSC"].ToString(), String.Empty);

                            wtSumSCVar = ConvertDecimalTwoPlaces(str);

                            WtAmntSC = WtAmntSC + wtSumSCVar;
                        }
                    }

                }

            }
          
        }

        private void Document_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

       
        private void txtWTCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnCrear.Content = "Actualizar";
        }

        private void txtTaxbleAmnt_KeyUp(object sender, KeyEventArgs e)
        {
            btnCrear.Content = "Actualizar";
        }

        public static DataTable GetRetenciones()
        {
            return dt;
        }

        public static void ClearRetenciones()
        {
            dt.Rows.Clear();
        }
    }
}
