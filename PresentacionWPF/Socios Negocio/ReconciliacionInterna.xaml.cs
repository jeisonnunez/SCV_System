using Entidades;
using Negocio;
using Negocio.Controlador_Socio_Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ReconciliacionInterna.xaml
    /// </summary>
    public partial class ReconciliacionInterna : Converter
    {
        ControladorAsiento cj = new ControladorAsiento();

        ControladorDocumento cn = new ControladorDocumento();

        ControladorReconciliacionSN cr = new ControladorReconciliacionSN();

        DataTable dt = new DataTable();

        DataTable dtNewInternalReconciliationLines = new DataTable();

        DataTable dtNewJournalEntry = new DataTable();

        private int reconNum;

        private int transId;

        private DateTime? desde;

        private DateTime? hasta;

        private decimal amountReconciliation=0;

        private decimal amountDiference;
        public int ReconNum { get => reconNum; set => reconNum = value; }

        public int TransId { get => transId; set => transId = value; }
        public DateTime? Desde { get => desde; set => desde = value; }
        public DateTime? Hasta { get => hasta; set => hasta = value; }
        public decimal AmountReconciliation { get => amountReconciliation; set => amountReconciliation = value; }
        public decimal AmountDiference { get => amountDiference; set => amountDiference = value; }
        public int CountInternalReconciliation { get => countInternalReconciliation; set => countInternalReconciliation = value; }

        List<ReconciliacionInternaDetalles> listReconciliationDetails = new List<ReconciliacionInternaDetalles>();

        private int countInternalReconciliation;

        public ReconciliacionInterna()
        {
            InitializeComponent();

        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetDatePicker(DateTime? date, DateTime? dpDFechaContabilizacionte, DateTime? dpHFechaContabilizacion)
        {
            Desde = dpDFechaContabilizacionte;

            Hasta = dpHFechaContabilizacion;

            dpReconciliacion.Background = Brushes.White;            

            dpReconciliacion.SelectedDate = date;
        }

        public void SetResult(DataTable listAccountesult)
        {
            dt = SetSeleccionado(listAccountesult);

            dt = AddCurrencyCode(dt);

            dgReconciliacion.ItemsSource = dt.DefaultView;

            dgReconciliacion.CanUserAddRows = false;

            dgReconciliacion.CanUserDeleteRows = false;

            dgReconciliacion.CanUserSortColumns = false;

            dgReconciliacion.IsReadOnly = false;
        }

        private DataTable AddCurrencyCode(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Importe" && Convert.ToDecimal(row["Importe"]) != 0)
                    {
                        row["Importe"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["Importe"])).ToString("N", nfi);

                    }

                    else if (column.ToString() == "SaldoVencido" && Convert.ToDecimal(row["SaldoVencido"]) != 0)
                    {
                        row["SaldoVencido"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["SaldoVencido"])).ToString("N", nfi);
                    }

                    else if (column.ToString() == "ImporteReconciliar" && Convert.ToDecimal(row["ImporteReconciliar"]) != 0)
                    {
                        row["ImporteReconciliar"] = Properties.Settings.Default.MainCurrency + " " + (ConvertDecimalTwoPlaces(row["ImporteReconciliar"])).ToString("N", nfi);
                    }

                }

            }

            return dt;
        }

        public void SetField(string sn)
        {
            txtSN.Text = sn;
        }

       

        private void seleccionado_Checked(object sender, RoutedEventArgs e)
        {           

            var row_list = GetDataGridRows(dgReconciliacion);

            DataRowView row_Selected = dgReconciliacion.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                if (checkBox.IsFocused==true)
                {
                    if (single_row.IsSelected == true)
                    {
                        DockPanel dpSeleccionado = FindChild<DockPanel>(single_row, "dpSeleccionado");

                        TextBlock txtTransId = FindChild<TextBlock>(single_row, "txtTransId");

                        DockPanel dpTransId = FindChild<DockPanel>(single_row, "dpTransId");

                        TextBlock txtTransType = FindChild<TextBlock>(single_row, "txtTransType");

                        DockPanel dpTransType = FindChild<DockPanel>(single_row, "dpTransType");

                        TextBlock txtBaseRef = FindChild<TextBlock>(single_row, "txtBaseRef");

                        DockPanel dpBaseRef = FindChild<DockPanel>(single_row, "dpBaseRef");

                        TextBlock txtRefDate = FindChild<TextBlock>(single_row, "txtRefDate");

                        DockPanel dpRefDate = FindChild<DockPanel>(single_row, "dpRefDate");

                        TextBlock txtImporte = FindChild<TextBlock>(single_row, "txtImporte");

                        DockPanel dpImporte = FindChild<DockPanel>(single_row, "dpImporte");

                        DockPanel dpImporteReconciliar = FindChild<DockPanel>(single_row, "dpImporteReconciliar");

                        TextBlock txtSaldoVencido = FindChild<TextBlock>(single_row, "txtSaldoVencido");

                        DockPanel dpSaldoVencido = FindChild<DockPanel>(single_row, "dpSaldoVencido");

                        TextBlock txtImporteReconciliar = FindChild<TextBlock>(single_row, "txtImporteReconciliar");

                        TextBlock txtLineMemo = FindChild<TextBlock>(single_row, "txtLineMemo");

                        DockPanel dpLineMemo = FindChild<DockPanel>(single_row, "dpLineMemo");

                        dpLineMemo.Background = Brushes.LightBlue;

                        dpImporteReconciliar.Background = Brushes.LightBlue;

                        dpSaldoVencido.Background = Brushes.LightBlue;

                        dpImporte.Background = Brushes.LightBlue;

                        dpRefDate.Background = Brushes.LightBlue;

                        dpBaseRef.Background = Brushes.LightBlue;

                        dpSeleccionado.Background = Brushes.LightBlue;

                        txtTransId.Background = Brushes.LightBlue;

                        dpTransId.Background = Brushes.LightBlue;

                        txtTransType.Background = Brushes.LightBlue;

                        dpTransType.Background = Brushes.LightBlue;

                        txtBaseRef.Background = Brushes.LightBlue;

                        txtSaldoVencido.Background = Brushes.LightBlue;

                        txtRefDate.Background = Brushes.LightBlue;

                        txtImporte.Background = Brushes.LightBlue;

                        txtSaldoVencido.Background = Brushes.LightBlue;

                        txtImporteReconciliar.Background = Brushes.LightBlue;

                        txtLineMemo.Background = Brushes.LightBlue;

                        AmountReconciliation = AmountReconciliation + ConvertDecimalTwoPlaces(txtImporteReconciliar.Text);

                        txtTotalReconciliacion.Text = Properties.Settings.Default.MainCurrency + " " + AmountReconciliation.ToString("N", nfi);
                    }

                    

                }
            }


        }

        private void seleccionado_Unchecked(object sender, RoutedEventArgs e)
        {           

            var row_list = GetDataGridRows(dgReconciliacion);

            DataRowView row_Selected = dgReconciliacion.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                CheckBox checkBox = FindChild<CheckBox>(single_row, "seleccionado");

                if (checkBox.IsFocused == true)
                {
                    if (single_row.IsSelected == true)
                    {
                        var bc = new BrushConverter();

                        DockPanel dpSeleccionado = FindChild<DockPanel>(single_row, "dpSeleccionado");

                        TextBlock txtTransId = FindChild<TextBlock>(single_row, "txtTransId");

                        DockPanel dpTransId = FindChild<DockPanel>(single_row, "dpTransId");

                        TextBlock txtTransType = FindChild<TextBlock>(single_row, "txtTransType");

                        DockPanel dpTransType = FindChild<DockPanel>(single_row, "dpTransType");

                        TextBlock txtBaseRef = FindChild<TextBlock>(single_row, "txtBaseRef");

                        DockPanel dpBaseRef = FindChild<DockPanel>(single_row, "dpBaseRef");

                        TextBlock txtRefDate = FindChild<TextBlock>(single_row, "txtRefDate");

                        DockPanel dpRefDate = FindChild<DockPanel>(single_row, "dpRefDate");

                        TextBlock txtImporte = FindChild<TextBlock>(single_row, "txtImporte");

                        DockPanel dpImporte = FindChild<DockPanel>(single_row, "dpImporte");

                        TextBlock txtSaldoVencido = FindChild<TextBlock>(single_row, "txtSaldoVencido");

                        DockPanel dpSaldoVencido = FindChild<DockPanel>(single_row, "dpSaldoVencido");

                        TextBlock txtImporteReconciliar = FindChild<TextBlock>(single_row, "txtImporteReconciliar");

                        DockPanel dpImporteReconciliar = FindChild<DockPanel>(single_row, "dpImporteReconciliar");

                        TextBlock txtLineMemo = FindChild<TextBlock>(single_row, "txtLineMemo");

                        DockPanel dpLineMemo = FindChild<DockPanel>(single_row, "dpLineMemo");

                        dpSeleccionado.Background = Brushes.White;

                        txtTransId.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpTransId.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtTransType.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpTransType.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtBaseRef.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpBaseRef.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtSaldoVencido.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtRefDate.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpRefDate.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtImporte.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpImporte.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtSaldoVencido.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpSaldoVencido.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        txtImporteReconciliar.Background = Brushes.White;

                        dpImporteReconciliar.Background = Brushes.White;

                        txtLineMemo.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        dpLineMemo.Background = (Brush)bc.ConvertFrom("#FFE5E5E5");

                        AmountReconciliation = AmountReconciliation - ConvertDecimalTwoPlaces(txtImporteReconciliar.Text);

                        txtTotalReconciliacion.Text = Properties.Settings.Default.MainCurrency + " " + AmountReconciliation.ToString("N", nfi);
                    }

                       
                }

            }
        }

        private Tuple<bool?,decimal> GetAndVerifyAmountReconciliation(DataTable dt)
        {
            decimal DebitAmount = 0;

            decimal CreditAmount = 0;

            decimal result;

            bool? sw = null;

            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToBoolean(row["Seleccionado"]) == true)
                {
                    if (ConvertDecimalTwoPlaces(row["ImporteReconciliar"]) > 0)
                    {
                        DebitAmount = DebitAmount + ConvertDecimalTwoPlaces(row["ImporteReconciliar"]);

                    }else if (ConvertDecimalTwoPlaces(row["ImporteReconciliar"]) < 0)
                    {
                        CreditAmount = CreditAmount + ConvertDecimalTwoPlaces(row["ImporteReconciliar"]);
                    }
                }

            }

            result = DebitAmount + CreditAmount;

            if (result == 0)
            {
                sw = true;
            }
            else
            {
                sw = false;
            }

            return Tuple.Create(sw,DebitAmount);
        }

        private void DeleteDataRow(DataRow row)
        {
            dt.Rows.Remove(row);

            dt.AcceptChanges();

            dgReconciliacion.ItemsSource = dt.DefaultView;
        }
        private void DeletedAllInsert(bool? OITR, bool? ITR1,  bool? jDT1UpDate)
        {
            if (ITR1 != null)
            {
                var deleteITR1 = cr.DeleteITR1(ReconNum);

                if (deleteITR1.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se eliminaron todas las lineas del asiento de reconciliacion : " + ReconNum, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (OITR == true)
            {
                var deleteOITR = cr.DeleteOITR(ReconNum);

                if (deleteOITR.Item2 != null)
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se elimino el asiento de reconciliacion: " + ReconNum, Brushes.LightBlue, Brushes.Black, "003-interface-2.png");
                }
            }

            if (jDT1UpDate != null)
            {

            }

        }

        private List<ReconciliacionInternaCabecera> CreateReconciliationInternal()
        {          
            List<ReconciliacionInternaCabecera> listaInternalReconciliation = new List<ReconciliacionInternaCabecera>();

            ReconciliacionInternaCabecera InternalReconciliation = new ReconciliacionInternaCabecera();
            
            var result = cr.SelectReconNum();

            ReconNum = result.Item1;

            InternalReconciliation.ReconNum = result.Item1;
            InternalReconciliation.IsCard = 'C';
            InternalReconciliation.ReconType = cr.GetReconType("Manual");
            InternalReconciliation.ReconDate = (DateTime)dpReconciliacion.SelectedDate;

            var total= GetAndVerifyAmountReconciliation(dt);

            if (total.Item1 == true)
            {
                InternalReconciliation.Total = total.Item2;
            }
            else
            {
                InternalReconciliation.Total = 0;
            }

            InternalReconciliation.ReconCurr = Properties.Settings.Default.MainCurrency;//verificar

            InternalReconciliation.CancelAbs = 0;
            InternalReconciliation.Canceled = 'N';
            InternalReconciliation.IsSystem = 'N';
            InternalReconciliation.InitObjTyp = null;
            InternalReconciliation.InitObjAbs = 0;
            InternalReconciliation.CreateDate = (DateTime)fechaActual.GetFechaActual();
            InternalReconciliation.UserSign = Properties.Settings.Default.Usuario;
            InternalReconciliation.ReconJEId = 0; //campo indica si creo un asiento. Cero sino creo. Sino coloca el numero del asiento
            InternalReconciliation.ObjType = cr.GetTransType("ID").ToString();

            listaInternalReconciliation.Add(InternalReconciliation);

            return listaInternalReconciliation;

        }

        private Tuple<List<ReconciliacionInternaDetalles>,int> AddReconciliationInternalLines(DataTable dt)
        {
          
            DataTable newDt= AddDatatableReconcilitionInternalLines(dt);

            List<ReconciliacionInternaDetalles> listaInternalReconciliationLines = new List<ReconciliacionInternaDetalles>();

            int k = CountInternalReconciliation;

            int i = 0;

            foreach(DataRow row in newDt.Rows)
            {
                ReconciliacionInternaDetalles InternalReconciliationLines = new ReconciliacionInternaDetalles();

                InternalReconciliationLines.ReconNum = ReconNum;
                InternalReconciliationLines.LineSeq = k;
                InternalReconciliationLines.TransId = Convert.ToInt32(row["TransId"]);
                InternalReconciliationLines.ShortName = row["ShortName"].ToString();
                InternalReconciliationLines.Account = row["Account"].ToString();
                InternalReconciliationLines.TransRowId =Convert.ToInt32(row["TransRowId"]);
                InternalReconciliationLines.SrcObjTyp = row["SrcObjTyp"].ToString();
                InternalReconciliationLines.SrcObjAbs = Convert.ToInt32(row["SrcObjAbs"]);
                InternalReconciliationLines.ReconSum = ConvertDecimalTwoPlaces(row["ReconSum"]);
                InternalReconciliationLines.ReconSumFC = ConvertDecimalTwoPlaces(row["ReconSumFC"]);
                InternalReconciliationLines.ReconSumSC = ConvertDecimalTwoPlaces(row["ReconSumSC"]);
                InternalReconciliationLines.IsCredit = Convert.ToChar(row["IsCredit"]);          
                InternalReconciliationLines.SumMthCurr = ConvertDecimalTwoPlaces(row["SumMthCurr"]);
                InternalReconciliationLines.WTSum = ConvertDecimalTwoPlaces(row["WTSum"]);
                InternalReconciliationLines.WTSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"]);
                InternalReconciliationLines.WTSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"]);
                InternalReconciliationLines.ExpSum = ConvertDecimalTwoPlaces(row["ExpSum"]);
                InternalReconciliationLines.ExpSumFC = ConvertDecimalTwoPlaces(row["ExpSumFC"]);
                InternalReconciliationLines.ExpSumSC = ConvertDecimalTwoPlaces(row["ExpSumSC"]);
                InternalReconciliationLines.FrgnCurr = row["FrgnCurr"].ToString();

                listaInternalReconciliationLines.Add(InternalReconciliationLines);

                k++;

                i++;
            }

            
            
            return Tuple.Create(listaInternalReconciliationLines,i);
        }

        

        private Tuple<List<ReconciliacionInternaDetalles>, int> CreateReconciliationInternalLines(List<ReconciliacionInternaCabecera> listaJournalEntry, DataTable dt)
        {
            dtNewInternalReconciliationLines = CreateDatatableReconcilitionInternalLines(dt);

            int ReconNum = 0;


            foreach (ReconciliacionInternaCabecera journalEntry in listaJournalEntry)
            {
                ReconNum = journalEntry.ReconNum;
               

            }

            int k = 0;

            List<ReconciliacionInternaDetalles> listaInternalReconciliationLines = new List<ReconciliacionInternaDetalles>();

            foreach (DataRow row in dtNewInternalReconciliationLines.Rows)
            {
                ReconciliacionInternaDetalles InternalReconciliationLines = new ReconciliacionInternaDetalles();

                InternalReconciliationLines.ReconNum = ReconNum;
                InternalReconciliationLines.LineSeq = k;
                InternalReconciliationLines.TransId = Convert.ToInt32(row["TransId"]);
                InternalReconciliationLines.Account = row["Account"].ToString();
                InternalReconciliationLines.ShortName = row["ShortName"].ToString();               
                InternalReconciliationLines.TransRowId = Convert.ToInt32(row["TransRowId"]);
                InternalReconciliationLines.SrcObjTyp = row["SrcObjTyp"].ToString();
                InternalReconciliationLines.SrcObjAbs = Convert.ToInt32(row["SrcObjAbs"]);
                InternalReconciliationLines.ReconSum = ConvertDecimalTwoPlaces(row["ReconSum"].ToString());
                InternalReconciliationLines.ReconSumFC = ConvertDecimalTwoPlaces(row["ReconSumFC"].ToString());
                InternalReconciliationLines.ReconSumSC = ConvertDecimalTwoPlaces(row["ReconSumSC"].ToString());
                InternalReconciliationLines.FrgnCurr = row["FrgnCurr"].ToString();
                InternalReconciliationLines.SumMthCurr = ConvertDecimalTwoPlaces(row["SumMthCurr"].ToString());
                InternalReconciliationLines.IsCredit = Convert.ToChar(row["IsCredit"]);
                InternalReconciliationLines.WTSum = ConvertDecimalTwoPlaces(row["WTSum"].ToString());
                InternalReconciliationLines.WTSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"].ToString());
                InternalReconciliationLines.WTSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"].ToString());
                InternalReconciliationLines.ExpSum = ConvertDecimalTwoPlaces(row["ExpSum"].ToString());
                InternalReconciliationLines.ExpSumFC = ConvertDecimalTwoPlaces(row["ExpSumFC"].ToString());
                InternalReconciliationLines.ExpSumSC = ConvertDecimalTwoPlaces(row["ExpSumSC"].ToString());

                listaInternalReconciliationLines.Add(InternalReconciliationLines);

                k++;

            }

            return Tuple.Create(listaInternalReconciliationLines, k);
        }

        private DataTable CreateDatatableReconcilitionInternalLines(DataTable dt)
        {
            decimal importeAplicado=0;
            decimal saldoVencido=0;
            decimal saldoVencidoMS=0;
            decimal saldoVencidoME=0;
            decimal importeAplicadoMS=0;
            decimal importeAplicadoME=0;
            decimal wtSum = 0;
            decimal wtSumFC = 0;
            decimal wtSumSC = 0;
            char isCredit='D';
            decimal rate = 0;

            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("ShortName");           
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("Line_ID");
            dtResult.Columns.Add("TransId");
            dtResult.Columns.Add("TransRowId");
            dtResult.Columns.Add("SrcObjTyp");
            dtResult.Columns.Add("SrcObjAbs");
            dtResult.Columns.Add("ReconSum");
            dtResult.Columns.Add("ReconSumFC");
            dtResult.Columns.Add("ReconSumSC");
            dtResult.Columns.Add("FrgnCurr");
            dtResult.Columns.Add("SumMthCurr");
            dtResult.Columns.Add("IsCredit");
            dtResult.Columns.Add("WTSum");
            dtResult.Columns.Add("WTSumFC");
            dtResult.Columns.Add("WTSumSC");
            dtResult.Columns.Add("ExpSum");
            dtResult.Columns.Add("ExpSumFC");
            dtResult.Columns.Add("ExpSumSC");

            foreach (DataRow row in dt.Rows)
            {  
               if (Convert.ToBoolean(row["Seleccionado"])==true)
               {

                    if (ConvertDecimalTwoPlaces(row["SaldoVencido"]) >= 0)
                    {
                        saldoVencido = ConvertDecimalTwoPlaces(row["SaldoVencido"]);

                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoVencido"]) < 0)
                    {
                        saldoVencido = ConvertDecimalTwoPlaces(row["SaldoVencido"]) * (-1);
                    }


                    if (ConvertDecimalTwoPlaces(row["ImporteReconciliar"]) >= 0)
                    {
                        importeAplicado = ConvertDecimalTwoPlaces(row["ImporteReconciliar"]);

                        isCredit = 'D';

                    }else if (ConvertDecimalTwoPlaces(row["ImporteReconciliar"]) < 0)
                    {
                        importeAplicado = ConvertDecimalTwoPlaces(row["ImporteReconciliar"])*(-1);

                        isCredit = 'C';
                    }


                    if (ConvertDecimalTwoPlaces(row["SaldoMS"]) >= 0)
                    {
                        saldoVencidoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]);

                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoMS"]) < 0)
                    {
                        saldoVencidoMS = ConvertDecimalTwoPlaces(row["SaldoMS"]) * (-1);
                    }


                    if (ConvertDecimalTwoPlaces(row["SaldoME"]) >= 0)
                    {
                        saldoVencidoME = ConvertDecimalTwoPlaces(row["SaldoME"]);

                    }
                    else if (ConvertDecimalTwoPlaces(row["SaldoME"]) < 0)
                    {
                        saldoVencidoME = ConvertDecimalTwoPlaces(row["SaldoME"]) * (-1);
                    }

                    if (ConvertDecimalTwoPlaces(row["WTSumSC"]) >= 0)
                    {
                        wtSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"]);

                    }
                    else if (ConvertDecimalTwoPlaces(row["WTSumSC"]) < 0)
                    {
                        wtSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"]) * (-1);
                    }


                    if (ConvertDecimalTwoPlaces(row["WTSum"]) >= 0)
                    {
                        wtSum = ConvertDecimalTwoPlaces(row["WTSum"]);                        

                    }
                    else if (ConvertDecimalTwoPlaces(row["WTSum"]) < 0)
                    {
                        wtSum = ConvertDecimalTwoPlaces(row["WTSum"]) * (-1);

                    }


                    if (ConvertDecimalTwoPlaces(row["WTSumFC"]) >= 0)
                    {
                        wtSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"]);

                    }
                    else if (ConvertDecimalTwoPlaces(row["WTSumFC"]) < 0)
                    {
                        wtSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"]) * (-1);
                    }

                    importeAplicadoMS =CalculateSysValue(saldoVencido,importeAplicado,saldoVencidoMS);

                    importeAplicadoME=CalculateFCValue(saldoVencido,importeAplicado,saldoVencidoME);

                    rate = ConvertDecimalTwoPlaces(row["Importe"]) / ConvertDecimalTwoPlaces(row["SaldoMS"]);

                    DataRow newRow = dtResult.NewRow();

                    newRow["ShortName"] = row["ShortName"];

                    newRow["Account"] = row["Account"];

                    newRow["TransId"] = row["TransId"];

                    newRow["TransRowId"] = row["Line_ID"];

                    string transType=regexString.Replace(row["TransType"].ToString(), String.Empty);

                    newRow["SrcObjTyp"] =ConvertDecimalTwoPlaces((cn.GetTransType(transType)).ToString());

                    newRow["SrcObjAbs"] = row["BaseRef"];

                    newRow["ReconSum"] = importeAplicado;

                    newRow["ReconSumFC"] = importeAplicadoME;

                    newRow["ReconSumSC"] = importeAplicadoMS;

                    newRow["WTSum"] = wtSum;

                    newRow["WTSumFC"] = wtSumFC;

                    newRow["WTSumSC"] = wtSumSC;

                    newRow["FrgnCurr"] = row["FCCurrency"];

                    newRow["IsCredit"] = isCredit;

                    newRow["SumMthCurr"] = importeAplicadoME*rate;

                    newRow["ExpSum"] = 0;

                    newRow["ExpSumFC"] = 0;

                    newRow["ExpSumSC"] = 0;

                    dtResult.Rows.Add(newRow);

                }                
            }

            return dtResult;
        }

        private DataTable AddDatatableReconcilitionInternalLines(DataTable dt)
        {
            char isCredit = 'D';
            
            int cont = dt.Rows.Count;

            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("ShortName");
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("Line_ID");
            dtResult.Columns.Add("TransId");
            dtResult.Columns.Add("TransRowId");
            dtResult.Columns.Add("SrcObjTyp");
            dtResult.Columns.Add("SrcObjAbs");
            dtResult.Columns.Add("ReconSum");
            dtResult.Columns.Add("ReconSumFC");
            dtResult.Columns.Add("ReconSumSC");
            dtResult.Columns.Add("FrgnCurr");
            dtResult.Columns.Add("SumMthCurr");
            dtResult.Columns.Add("IsCredit");
            dtResult.Columns.Add("WTSum");
            dtResult.Columns.Add("WTSumFC");
            dtResult.Columns.Add("WTSumSC");
            dtResult.Columns.Add("ExpSum");
            dtResult.Columns.Add("ExpSumFC");
            dtResult.Columns.Add("ExpSumSC");

            int i = 1;

            int j = 0;

          
            foreach (DataRow row in dt.Rows)
            {
                if (i != cont)
                {
                    
                    DataRow newRow = dtResult.NewRow();

                    newRow["ShortName"] = row["ShortName"];

                    newRow["Account"] = row["Account"];

                    newRow["TransId"] = TransId;

                    newRow["TransRowId"] = j;
                    
                    newRow["SrcObjTyp"] = "321";

                    newRow["SrcObjAbs"] = 1;

                    if(ConvertDecimalTwoPlaces(row["Debit"])>0 && ConvertDecimalTwoPlaces(row["Credit"]) == 0)
                    {
                        newRow["ReconSum"] = ConvertDecimalTwoPlaces(row["Debit"]);

                        isCredit = 'D';
                    }
                    else if (ConvertDecimalTwoPlaces(row["Credit"]) > 0 && ConvertDecimalTwoPlaces(row["Debit"]) == 0)
                    {
                        newRow["ReconSum"] = ConvertDecimalTwoPlaces(row["Credit"]);

                        isCredit = 'C';
                    }

                    if (ConvertDecimalTwoPlaces(row["SYSDeb"]) > 0 && ConvertDecimalTwoPlaces(row["SYSCred"]) == 0)
                    {
                        newRow["ReconSumSC"] = ConvertDecimalTwoPlaces(row["SYSDeb"]);
                    }
                    else if (ConvertDecimalTwoPlaces(row["SYSCred"]) > 0 && ConvertDecimalTwoPlaces(row["SYSDeb"]) == 0)
                    {
                        newRow["ReconSumSC"] = ConvertDecimalTwoPlaces(row["SYSCred"]);
                    }

                    newRow["ReconSumFC"] = 0;                  

                    newRow["WTSum"] = 0;

                    newRow["WTSumFC"] = 0;

                    newRow["WTSumSC"] = 0;

                    newRow["FrgnCurr"] = Properties.Settings.Default.MainCurrency;

                    newRow["IsCredit"] = isCredit;

                    newRow["SumMthCurr"] = newRow["ReconSum"];

                    newRow["ExpSum"] = 0;

                    newRow["ExpSumFC"] = 0;

                    newRow["ExpSumSC"] = 0;

                    dtResult.Rows.Add(newRow);

                }

                i++;

                
            }

            return dtResult;
        }

        private decimal CalculateFCValue(decimal saldoVencido, decimal importeAplicado, decimal saldoVencidoME)
        {
            decimal importeAlicadoME = 0;

            importeAlicadoME = ConvertDecimalTwoPlaces((importeAplicado * saldoVencidoME) / saldoVencido);

            return importeAlicadoME;
        }

        private decimal CalculateSysValue(decimal saldoVencido, decimal importeAplicado, decimal saldoVencidoMS)
        {
            decimal importeAlicadoMS = 0;

            importeAlicadoMS = ConvertDecimalTwoPlaces((importeAplicado * saldoVencidoMS) / saldoVencido);

            return importeAlicadoMS;

        }

        private void DeleteDataRow(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = dt.Rows[i];

                if (ConvertDecimalTwoPlaces(row["SaldoVencido"]) == 0)
                {
                    dt.Rows.Remove(row);

                    dt.AcceptChanges();
                }               
            }

            dgReconciliacion.ItemsSource = dt.DefaultView;
        }

        private DataTable SetSeleccionado(DataTable dt)
        {

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Seleccionado")
                    {
                        row["Seleccionado"] = false;

                    }

                }

            }

            return dt;
        }


        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgSN_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var result = cn.FindBP();

            if (result.Item2 == null)
            {
                RecorreListaSN(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }
        }

        private void RecorreListaSN(List<SocioNegocio> listSuppliers)
        {
            if (listSuppliers.Count == 1)
            {
                GetSocioNegocio(listSuppliers);


            }
            else if (listSuppliers.Count == 0)
            {

                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

            }

            else if (listSuppliers.Count > 1)
            {
                ListaSociosNegocios ventanaListBox = new ListaSociosNegocios(listSuppliers);

                ventanaListBox.ShowDialog();

                if (ventanaListBox.Visibility == Visibility.Hidden)
                {
                    if (ventanaListBox.GetListSN().Count == 0)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun elemento", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");

                    }
                    else
                    {

                        GetSocioNegocio(ventanaListBox.GetListSN());

                        txtSN.Background = Brushes.White;

                        dpSN.Background = Brushes.White;

                        bdSN.Background = Brushes.White;

                        imgSN.Visibility = Visibility.Hidden;

                        

                    }
                }
            }
        }

        private void FindReconciliacionSN()
        {
            var result = cr.ExecuteReconciliacionSN(txtSN.Text, Desde, Hasta);

            if (result.Item2 == null)
            {

                if (result.Item1.Rows.Count >= 1)
                {
                    ClearDatatable();                   

                                   
                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontraron resultados con los parametros establecidos: " + result.Item2, Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
                }

                SetResult(result.Item1);
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void GetSocioNegocio(List<SocioNegocio> listSuppliers)
        {
            foreach (SocioNegocio Suppliers in listSuppliers)
            {

                txtSN.Text = Suppliers.CardCode;

            }
        }

        private void txtSN_GotFocus(object sender, RoutedEventArgs e)
        {
            dpSN.Background = Brushes.LightBlue;

            bdSN.Background = Brushes.LightBlue;

            txtSN.Background = Brushes.LightBlue;

            imgSN.Visibility = Visibility.Visible;
        }

        private void txtSN_LostFocus(object sender, RoutedEventArgs e)
        {
            dpSN.Background = Brushes.White;

            bdSN.Background = Brushes.White;

            txtSN.Background = Brushes.White;

            imgSN.Visibility = Visibility.Hidden;
        }

        private void dpReconciliacion_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FindReconciliacionSN();
        }

        private void Converter_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void imgTransId_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void imgTransType_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtSN_TextChanged(object sender, TextChangedEventArgs e)
        {
            FindReconciliacionSN();
        }

        private void dgReconciliacion_LayoutUpdated(object sender, EventArgs e)
        {

        }

        private void btnReconciliar_Click(object sender, RoutedEventArgs e)
        {
            if (AmountReconciliation == 0)
            {
               
                List<ReconciliacionInternaCabecera> listaInternalReconciliation = new List<ReconciliacionInternaCabecera>();

                ReconciliacionInternaCabecera internalReconciliation = new ReconciliacionInternaCabecera();

                List<ReconciliacionInternaDetalles> ReconciliacionInternaDetallesLines = new List<ReconciliacionInternaDetalles>();
                                               
                bool? OITR = null;                                      

                bool? ITR1 = null;

                bool? JDT1UpDate = null;

                listaInternalReconciliation = CreateReconciliationInternal();

                var result=cr.InsertReconciliationInternal(listaInternalReconciliation);

                if (result.Item1 == 1)
                {
                    OITR = true;

                    var listInternalReconciliationLines = CreateReconciliationInternalLines(listaInternalReconciliation,dt);

                    CountInternalReconciliation = listInternalReconciliationLines.Item1.Count;

                    var result3 = cr.InsertReconciliationInternalLines(listInternalReconciliationLines.Item1);

                    if (listInternalReconciliationLines.Item2 == result3.Item1)
                    {
                        ITR1 = true;

                        listReconciliationDetails = listInternalReconciliationLines.Item1; //captura lista de reconciliacion detalle sin asiento de revaluacion

                        var result4 = cr.VerifiedDiferenceReconciliation(ReconNum);

                        if(result4.Item2==null && result4.Item1 == 0) //no existe diferencia 
                        {
                            //Actualiza valores en el asiento

                            var updateJournalEntryLines = cr.UpdateJournalEntryLines(listReconciliationDetails);

                            if (updateJournalEntryLines.Item2 == null && listReconciliationDetails.Count==updateJournalEntryLines.Item1)
                            {
                                JDT1UpDate = true;

                                bool swUpdateDocument = true;

                                foreach (ReconciliacionInternaDetalles reconciliacionInternaDetalles in listReconciliationDetails)
                                {
                                    if(reconciliacionInternaDetalles.SrcObjTyp=="13" || reconciliacionInternaDetalles.SrcObjTyp == "18" || reconciliacionInternaDetalles.SrcObjTyp == "14" || reconciliacionInternaDetalles.SrcObjTyp == "19")//verifica si se esta reconciliando un doucmento
                                    {
                                        var updateDocument = cr.UpdateDocument(reconciliacionInternaDetalles);

                                        if (updateDocument.Item2 == null && swUpdateDocument==true)
                                        {
                                            
                                        }
                                        else
                                        {
                                            swUpdateDocument= false;

                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar los documentos de tipo: " + updateDocument.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                                        }
                                    }
                                }

                                if (swUpdateDocument == true)
                                {
                                    FindReconciliacionSN();

                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizar Exitosamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                                }
                                else
                                {
                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: ", Brushes.Red, Brushes.White, "003-interface-2.png");
                                }                               
                            }
                            else
                            {
                                JDT1UpDate = false;

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + updateJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                DeletedAllInsert(OITR, ITR1, JDT1UpDate);
                            }
                            
                           
                        }
                        else //existe diferencia
                        {

                            AmountDiference = result4.Item1;

                            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

                            AsientoCabecera journalEntry = new AsientoCabecera();

                            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

                            bool? OJDT = null;

                            bool? JDT1Create = null;

                            bool? JDT1Update = null;

                            bool? ITR1Get = null;

                            bool? ITR1Update = null;

                            var getReconciliationInternalLines = cr.GetReconciliationInternalLines(ReconNum);

                            if (getReconciliationInternalLines.Item2 == null)
                            {                               
                                ITR1Get = true;

                                //Create Journal Entry

                                listaJournalEntry = CreateJournalEntry(getReconciliationInternalLines.Item1);

                                var result2 = cj.InsertJournalEntry(listaJournalEntry);

                                if (result2.Item1 == 1)
                                {                                    
                                    OJDT = true;

                                    //Create Journal Entry Lines

                                    var listJournalEntryLines = CreateListJournalEntryLines(listaJournalEntry, getReconciliationInternalLines.Item1);

                                    var resultJournalEntryLines = cj.InsertJournalEntryLines(listJournalEntryLines.Item1);

                                    if (listJournalEntryLines.Item2 == resultJournalEntryLines.Item1)
                                    {
                                        JDT1Create = true;

                                        //Agrega lineas de asiento diferencial al detalle de reconciliacion

                                        var addReconciliationInternalLines= AddReconciliationInternalLines(dtNewJournalEntry);

                                        var insterReconciliationInternalLines= cr.InsertReconciliationInternalLines(addReconciliationInternalLines.Item1);

                                        if (insterReconciliationInternalLines.Item1== addReconciliationInternalLines.Item2)
                                        {
                                            ITR1Update = true;

                                            var updateJournalEntryLines = cr.UpdateJournalEntryLines(listReconciliationDetails);

                                            if (updateJournalEntryLines.Item2 == null && listReconciliationDetails.Count == updateJournalEntryLines.Item1)
                                            {
                                                JDT1Update = true;

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion se registro exitosamente: " , Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                                FindReconciliacionSN();
                                            }
                                            else
                                            {
                                                JDT1Update = false;

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + updateJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                                DeletedAllInsert(ITR1Get, OJDT, JDT1Create, ITR1Update, JDT1Update);
                                            }

                                        }
                                        else
                                        {
                                            ITR1Update = false;

                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + insterReconciliationInternalLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            DeletedAllInsert(ITR1Get, OJDT, JDT1Create, ITR1Update, JDT1Update);
                                        }

                                    }
                                    else
                                    {
                                        JDT1Create = false;                                        

                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + resultJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                        DeletedAllInsert(ITR1Get, OJDT, JDT1Create, ITR1Update, JDT1Update);
                                    }
                                }
                                else
                                {
                                    OJDT = false;

                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                    DeletedAllInsert(ITR1Get, OJDT, JDT1Create, ITR1Update, JDT1Update);
                                }
                            }
                            else
                            {
                                ITR1Get = false;                              

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + getReconciliationInternalLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                DeletedAllInsert(ITR1Get, OJDT, JDT1Create,ITR1Update,JDT1Update);
                            }
                        }
                    }
                    else
                    {                        
                        ITR1 = false;

                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result3.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                        DeletedAllInsert(OITR, ITR1, JDT1UpDate);
                    }
                }
                else
                {
                    OITR = false;

                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");                    

                    DeletedAllInsert(OITR, ITR1, JDT1UpDate);
                }                                            
                    
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Diferencia de reconciliacion debe ser cero antes de reconciliar" , Brushes.Red, Brushes.White, "003-interface-2.png");
            }
        }

        private void DeletedAllInsert(bool? ITR1Get, bool? OJDT, bool? JDT1Create, bool? ITR1Update, bool? JDT1Update)
        {
            throw new NotImplementedException();
        }

        private Tuple<List<AsientoDetalle>, int> CreateListJournalEntryLines(List<AsientoCabecera> listaJournalEntry, DataTable dt)
        {
            dtNewJournalEntry = CreateDatatableJournalEntryLines(dt);

            int TransId = 0;

            string TransCurr = null;

            DateTime? RefDate = null;

            DateTime? DueDate = null;

            DateTime? TaxDate = null;

            string Memo = null;

            int FinncPriod = 0;

            int TransType = 0;

            foreach (AsientoCabecera journalEntry in listaJournalEntry)
            {
                TransId = journalEntry.TransId;
                TransCurr = journalEntry.TransCurr;
                RefDate = journalEntry.RefDate;
                TaxDate = journalEntry.TaxDate;
                DueDate = journalEntry.DueDate;
                Memo = journalEntry.Memo;
                FinncPriod = journalEntry.FinncPriod;
                TransType = journalEntry.TransType;

            }

            int k = 0;

            int rowQuantity = dtNewJournalEntry.Rows.Count;

            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

            foreach (DataRow row in dtNewJournalEntry.Rows)
            {
                AsientoDetalle journalEntryLines = new AsientoDetalle();

                journalEntryLines.TransId = TransId;
                journalEntryLines.Line_ID = k;
                journalEntryLines.RefDate = RefDate;
                journalEntryLines.DueDate = DueDate;
                journalEntryLines.TaxDate = TaxDate;
                journalEntryLines.Account = row["Account"].ToString();
                journalEntryLines.ShortName = row["ShortName"].ToString();
                journalEntryLines.ContraAct = row["ContraAct"].ToString();
                journalEntryLines.LineMemo = Memo;
                journalEntryLines.TransType = TransType;
                journalEntryLines.Debit = ConvertDecimalTwoPlaces(row["Debit"].ToString());
                journalEntryLines.Credit = ConvertDecimalTwoPlaces(row["Credit"].ToString());
                journalEntryLines.FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"].ToString());
                journalEntryLines.FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"].ToString());
                journalEntryLines.SysCred = ConvertDecimalTwoPlaces(row["SYSCred"].ToString());
                journalEntryLines.SysDeb = ConvertDecimalTwoPlaces(row["SYSDeb"].ToString());

                if (rowQuantity - 1 == k) {

                    journalEntryLines.BalDueDeb = journalEntryLines.Debit;
                    journalEntryLines.BalDueCred = journalEntryLines.Credit;
                    journalEntryLines.BalFcDeb = journalEntryLines.FCDebit;
                    journalEntryLines.BalFcCred = journalEntryLines.FCCredit;
                    journalEntryLines.BalScCred = journalEntryLines.SysCred;
                    journalEntryLines.BalScDeb = journalEntryLines.SysDeb;
                }
                else
                {
                    journalEntryLines.BalDueDeb = 0;
                    journalEntryLines.BalDueCred = 0;
                    journalEntryLines.BalFcDeb =0;
                    journalEntryLines.BalFcCred = 0;
                    journalEntryLines.BalScCred = 0;
                    journalEntryLines.BalScDeb = 0;
                }

                
                journalEntryLines.UserSign = Properties.Settings.Default.Usuario;
                journalEntryLines.FinncPriod = FinncPriod;
                journalEntryLines.FCCurrency = TransCurr;
                journalEntryLines.DataSource = 'N';

                listaJournalEntryLines.Add(journalEntryLines);

                k++;

            }

            return Tuple.Create(listaJournalEntryLines, k);
        }

        private DataTable CreateDatatableJournalEntryLines(DataTable dt)
        {
            DataTable dtResult = new DataTable();

            dtResult.Columns.Add("ShortName");
            dtResult.Columns.Add("AcctName");
            dtResult.Columns.Add("Account");
            dtResult.Columns.Add("FCDebit");
            dtResult.Columns.Add("FCCredit");
            dtResult.Columns.Add("Debit");
            dtResult.Columns.Add("Credit");
            dtResult.Columns.Add("SYSDeb");
            dtResult.Columns.Add("SYSCred");
            dtResult.Columns.Add("LineMemo");
            dtResult.Columns.Add("ContraAct");

            foreach (DataRow row in dt.Rows)
            {
              
                DataRow newRow = dtResult.NewRow();

                if (ConvertDecimalTwoPlaces(row["ReconSum"]) > 0)
                {
                    newRow["Credit"] = ConvertDecimalTwoPlaces(row["ReconSum"]);

                    newRow["Debit"] = 0;
                }
                else if(ConvertDecimalTwoPlaces(row["ReconSum"]) < 0)
                {
                    newRow["Credit"] = 0;

                    newRow["Debit"] =ConvertDecimalTwoPlaces(row["ReconSum"])*(-1);
                }

                

                if (ConvertDecimalTwoPlaces(row["ReconSumSC"]) > 0)
                {
                    newRow["ShortName"] = row["ShortName"];

                    newRow["Account"] = row["Account"];

                    var result = cn.FindBeneficioDiferenciaConversion();

                    newRow["ContraAct"] = result.Item1;

                    newRow["LineMemo"] = "Transaccion de Reconciliacion Manual";

                    newRow["FCDebit"] = 0;

                    newRow["FCCredit"] = 0;                    

                    newRow["SYSCred"] = ConvertDecimalTwoPlaces(row["ReconSumSC"]);

                    newRow["SYSDeb"] = 0;

                    

                }
                else if (ConvertDecimalTwoPlaces(row["ReconSumSC"]) < 0)
                {
                    newRow["ShortName"] = row["ShortName"];

                    newRow["Account"] = row["Account"];

                    var result = cn.FindPerdidaDiferenciaConversion();                   

                    newRow["ContraAct"] = row["ShortName"];

                    newRow["LineMemo"] = "Transaccion de Reconciliacion Manual";

                    newRow["FCDebit"] = 0;

                    newRow["FCCredit"] = 0;

                    newRow["SYSDeb"] = ConvertDecimalTwoPlaces(row["ReconSumSC"]) * (-1);

                    newRow["SYSCred"] = 0;
                }

                dtResult.Rows.Add(newRow);

            }

            //Create Row Reconcliation

            DataRow newRow1 = dtResult.NewRow();

            newRow1["ContraAct"] = txtSN.Text;

            newRow1["LineMemo"] = "Transaccion de Reconciliacion Manual";

            newRow1["Debit"] = 0;

            newRow1["Credit"] = 0;

            if (AmountDiference > 0)
            {
                newRow1["SYSDeb"] = AmountDiference;

                newRow1["SYSCred"] = 0;

                var result2 = cn.FindBeneficioDiferenciaConversion();

                newRow1["ShortName"] = result2.Item1;                

                newRow1["Account"] = result2.Item1;


            }
            else if (AmountDiference < 0)
            {

                newRow1["SYSCred"] = AmountDiference*(-1);

                newRow1["SYSDeb"] = 0;

                var result2 = cn.FindPerdidaDiferenciaConversion();

                newRow1["ShortName"] = result2.Item1;

                newRow1["Account"] = result2.Item1;

            }

            dtResult.Rows.Add(newRow1);

            return dtResult;
        }

        private List<AsientoCabecera> CreateJournalEntry(DataTable dt)
        {
            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

            AsientoCabecera journalEntry = new AsientoCabecera();

            var result = cn.SelectTransId();

            TransId = result.Item1;

            journalEntry.TransId = result.Item1;
            journalEntry.RefDate = Convert.ToDateTime(dpReconciliacion.SelectedDate);
            journalEntry.TaxDate = Convert.ToDateTime(dpReconciliacion.SelectedDate);
            journalEntry.DueDate = Convert.ToDateTime(dpReconciliacion.SelectedDate);
            journalEntry.Memo = "Transaccion Reconciliacion Manual";
            journalEntry.BaseRef = 1; //revisar
            journalEntry.Ref1 = "";
            journalEntry.Ref2 = "";
            journalEntry.UserSign = Properties.Settings.Default.Usuario;
            journalEntry.UpdateDate = fechaActual.GetFechaActual();
            var result1 = cn.GetPeriodCode(journalEntry.RefDate);
            journalEntry.FinncPriod = result1.Item1;
            journalEntry.LocTotal = calculateLoctTotal(dt);           
            journalEntry.SysTotal = calculateSYSTotal(dt);
            journalEntry.FcTotal = 0;
            journalEntry.TransType = cn.GetTransType("ID");
            journalEntry.TransCurr = Properties.Settings.Default.MainCurrency;

            listaJournalEntry.Add(journalEntry);

            return listaJournalEntry;

        }

        private decimal calculateLoctTotal(DataTable dt)
        {
            decimal locTotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (ConvertDecimalTwoPlaces(row["ReconSum"])>0){

                    locTotal = locTotal + ConvertDecimalTwoPlaces(row["ReconSum"]);
                }

            }

            return locTotal;
        }

        private decimal calculateSYSTotal(DataTable dt)
        {
            decimal sysTotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                
                sysTotal = sysTotal + ConvertDecimalTwoPlaces(row["ReconSumSC"]);               

            }


            if (sysTotal < 0)
            {
                sysTotal= sysTotal * (-1);
            }

            return sysTotal;
        }

        private void txtSN_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void txtImporteReconciliar_GotFocus(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(dgReconciliacion);

            DataRowView row_Selected = dgReconciliacion.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                var bc = new BrushConverter();

                if (single_row.IsSelected== true)
                {

                    DockPanel dpImporteReconciliar = FindChild<DockPanel>(single_row, "dpImporteReconciliar");

                    dpImporteReconciliar.Background = (Brush)bc.ConvertFrom("#FF74C1FF");
                }
            }

        }

        private void txtImporteReconciliar_LostFocus(object sender, RoutedEventArgs e)
        {
            var row_list = GetDataGridRows(dgReconciliacion);

            DataRowView row_Selected = dgReconciliacion.SelectedItem as DataRowView;

            foreach (DataGridRow single_row in row_list)
            {
                if (single_row.IsSelected == true)
                {

                    DockPanel dpImporteReconciliar = FindChild<DockPanel>(single_row, "dpImporteReconciliar");

                    dpImporteReconciliar.Background = Brushes.White;

                    
                }

            }
        }
    }
}
