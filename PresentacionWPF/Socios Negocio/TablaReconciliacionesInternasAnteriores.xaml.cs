using Entidades;
using Negocio;
using Negocio.Controlador_Socio_Negocio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TablaReconciliacionesInternasAnteriores.xaml
    /// </summary>
    public partial class TablaReconciliacionesInternasAnteriores : Converter
    {
        DataTable dt = new DataTable();

        DataRowView row_Selected;

        ControladorGestionarReconciliacionesAnteriores cn = new ControladorGestionarReconciliacionesAnteriores();

        ControladorReconciliacionSN cr = new ControladorReconciliacionSN();

        ControladorAsiento cj = new ControladorAsiento();

        private int reconJEId;

        public int ReconJEId { get => reconJEId; set => reconJEId = value; }

        private int reconNum;

        public int ReconNum { get => reconNum; set => reconNum = value; }

        public TablaReconciliacionesInternasAnteriores()
        {
            InitializeComponent();
        }

        public void ClearDatatable()
        {
            dt.Rows.Clear();

        }

        public void SetResult(DataTable dt)
        {           

            dgHistorialReconciliacion.ItemsSource = dt.DefaultView;

            dgHistorialReconciliacion.CanUserAddRows = false;

            dgHistorialReconciliacion.CanUserDeleteRows = false;

            dgHistorialReconciliacion.CanUserSortColumns = false;

            dgHistorialReconciliacion.IsReadOnly = false;
        }

       

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void imgReturn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnCancelarReconciliacion_Click(object sender, RoutedEventArgs e)
        {
            if (row_Selected !=null)
            {
                bool? OITR = null;

                bool? ITR1 = null;

                bool? JDT1UpDate = null;

                List<ReconciliacionInternaCabecera> listaInternalReconciliation = new List<ReconciliacionInternaCabecera>();

                listaInternalReconciliation = ConvertDataRowView(row_Selected); //convierte datarow selected in list

                List<ReconciliacionInternaDetalles> ReconciliacionInternaDetallesLines = new List<ReconciliacionInternaDetalles>();

                ReconciliacionInternaDetallesLines = ConvertDataTable(dt); //obtiene detalles con montos a insertar

                var result = cr.InsertReconciliationInternal(listaInternalReconciliation);

                if (result.Item1 == 1)
                {
                    OITR = true;

                    var result3 = cr.InsertReconciliationInternalLines(ReconciliacionInternaDetallesLines);

                    if (ReconciliacionInternaDetallesLines.Count == result3.Item1)
                    {
                        ITR1 = true;

                        if (ReconJEId != 0)//no se realizo asiento de reconciliacion
                        {
                            //Actualiza valores en el asiento

                            //ReconciliacionInternaDetallesLines = RemoveLastItemList(ReconciliacionInternaDetallesLines);

                            var updateJournalEntryLines = cr.UpdateJournalEntryLines(ReconciliacionInternaDetallesLines);

                            if (updateJournalEntryLines.Item2 == null && ReconciliacionInternaDetallesLines.Count == updateJournalEntryLines.Item1)
                            {
                                JDT1UpDate = true;

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizar Exitosamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                //Actualizar datagrids
                            }
                            else
                            {
                                JDT1UpDate = false;

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el asiento: " + updateJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                DeletedAllInsert(OITR, ITR1, JDT1UpDate);
                            }
                        }
                        else
                        {
                            List<AsientoCabecera> listaJournalEntry = new List<AsientoCabecera>();

                            AsientoCabecera journalEntry = new AsientoCabecera();

                            List<AsientoDetalle> listaJournalEntryLines = new List<AsientoDetalle>();

                            bool? OJDT = null;

                            bool? JDT1Create = null;

                            bool? JDT1Update = null;

                            bool? JDT1Find = null;

                            bool? OJDTGet = null;                                                    

                            //FindJournalEntry

                            var JournalEntry = cj.FindJournalEntrySpecific(ReconJEId);

                            if (JournalEntry.Item2==null)
                            {
                                OJDTGet = true;

                                //Create Journal Entry

                                listaJournalEntry = CreateJournalEntry(JournalEntry.Item1);

                                var result2 = cj.InsertJournalEntry(listaJournalEntry);

                                if (result2.Item1 == 1)
                                {
                                    OJDT = true;

                                    //Find Journal Entry Lines

                                    var result1 = cj.FindJournalEntryLines(ReconJEId);

                                    if (result1.Item2 == null)
                                    {
                                        JDT1Find = true;

                                        listaJournalEntryLines = CreateListJournalEntryLines(result1.Item1);

                                        //Create Journal Entry Lines

                                        var resultJournalEntryLines = cj.InsertJournalEntryLines(listaJournalEntryLines);

                                        if (listaJournalEntryLines.Count == resultJournalEntryLines.Item1)
                                        {
                                            JDT1Create = true;

                                            ReconciliacionInternaDetallesLines = RemoveLastItemList(ReconciliacionInternaDetallesLines);

                                            var updateJournalEntryLines = cr.UpdateJournalEntryLines(ReconciliacionInternaDetallesLines);

                                            if (updateJournalEntryLines.Item2 == null && ReconciliacionInternaDetallesLines.Count == updateJournalEntryLines.Item1)
                                            {
                                                JDT1UpDate = true;

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizar Exitosamente: ", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                                                //Actualizar datagrids
                                            }
                                            else
                                            {
                                                JDT1UpDate = false;

                                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al actualizar el asiento: " + updateJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                                DeletedAllInsert(OJDTGet, OJDT, JDT1Create, JDT1Update, JDT1Find);
                                            }

                                        }
                                        else
                                        {
                                            JDT1Create = false;

                                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + resultJournalEntryLines.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                            DeletedAllInsert(OJDTGet, OJDT, JDT1Create, JDT1Update, JDT1Find);
                                        }
                                    }
                                    else
                                    {
                                        JDT1Find = false;

                                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al obtener lineas de el asiento: " + result1.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                        DeletedAllInsert(OJDTGet, OJDT, JDT1Create, JDT1Update, JDT1Find);
                                    }

                                    
                                }
                                else
                                {
                                    OJDT = false;

                                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al crear el asiento: " + result2.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                    DeletedAllInsert(OJDTGet, OJDT, JDT1Create, JDT1Update, JDT1Find);
                                }
                            }
                            else
                            {
                                OJDTGet = false;

                                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error al obtener el asiento: " + JournalEntry.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                                DeletedAllInsert(OJDTGet, OJDT, JDT1Create, JDT1Update, JDT1Find);
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
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ninguna reconciliacion a cancelar", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void DeletedAllInsert(bool? oJDTGet, bool? oJDT, bool? jDT1Create, bool? jDT1Update, bool? jDT1Find)
        {
            throw new NotImplementedException();
        }

        private List<AsientoDetalle> CreateListJournalEntryLines(DataTable dt)
        {
            throw new NotImplementedException();
        }

        private List<AsientoCabecera> CreateJournalEntry(List<AsientoCabecera> list)
        {
            throw new NotImplementedException();
        }

        private List<ReconciliacionInternaDetalles> RemoveLastItemList(List<ReconciliacionInternaDetalles> reconciliacionInternaDetallesLines)
        {
            reconciliacionInternaDetallesLines.RemoveAt(reconciliacionInternaDetallesLines.Count - 1);

            return reconciliacionInternaDetallesLines;
        }

        private void DeletedAllInsert(bool? OITR, bool? ITR1, bool? jDT1UpDate)
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

        private List<ReconciliacionInternaDetalles> ConvertDataTable(DataTable dt)
        {
            List<ReconciliacionInternaDetalles> listReconciliacionInternaDetallesLines = new List<ReconciliacionInternaDetalles>();

            ReconciliacionInternaDetalles ReconciliacionInternaDetallesLines = new ReconciliacionInternaDetalles();

            foreach (DataRow row in dt.Rows)
            {
                ReconciliacionInternaDetallesLines.ReconNum = Convert.ToInt32(row["ReconNum"]);
                ReconciliacionInternaDetallesLines.LineSeq= Convert.ToInt32(row["LineSeq"]); //
                ReconciliacionInternaDetallesLines.SrcObjTyp =(cr.GetReconType(row["SrcObjTyp"].ToString())).ToString();
                ReconciliacionInternaDetallesLines.ShortName = row["ShortName"].ToString();
                ReconciliacionInternaDetallesLines.TransId =Convert.ToInt32(row["TransId"]);
                ReconciliacionInternaDetallesLines.TransRowId = Convert.ToInt32(row["TransRowId"]);
                ReconciliacionInternaDetallesLines.Account = row["Account"].ToString();
                ReconciliacionInternaDetallesLines.SrcObjAbs= Convert.ToInt32(row["SrcObjAbs"]);
                ReconciliacionInternaDetallesLines.FrgnCurr = row["FrgnCurr"].ToString();

                if (ConvertDecimalTwoPlaces(row["ReconSum"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSum = ConvertDecimalTwoPlaces(row["ReconSum"]) * (-1);

                }else if(ConvertDecimalTwoPlaces(row["ReconSum"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSum = ConvertDecimalTwoPlaces(row["ReconSum"]);
                }

                if (ConvertDecimalTwoPlaces(row["ReconSumFC"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSumFC = ConvertDecimalTwoPlaces(row["ReconSumFC"]) * (-1);

                }
                else if (ConvertDecimalTwoPlaces(row["ReconSumFC"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSumFC = ConvertDecimalTwoPlaces(row["ReconSumFC"]);
                }

                if (ConvertDecimalTwoPlaces(row["ReconSumSC"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSumSC = ConvertDecimalTwoPlaces(row["ReconSumSC"]) * (-1);

                }
                else if (ConvertDecimalTwoPlaces(row["ReconSumSC"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.ReconSumSC = ConvertDecimalTwoPlaces(row["ReconSumSC"]);
                }

                if (ConvertDecimalTwoPlaces(row["WTSum"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.WTSum = ConvertDecimalTwoPlaces(row["WTSum"]) * (-1);

                }
                else if (ConvertDecimalTwoPlaces(row["WTSum"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.WTSum = ConvertDecimalTwoPlaces(row["WTSum"]);
                }

                if (ConvertDecimalTwoPlaces(row["WTSumFC"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.WTSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"]) * (-1);

                }
                else if (ConvertDecimalTwoPlaces(row["WTSumFC"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.WTSumFC = ConvertDecimalTwoPlaces(row["WTSumFC"]);
                }

                if (ConvertDecimalTwoPlaces(row["WTSumSC"]) > 0)
                {
                    ReconciliacionInternaDetallesLines.WTSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"]) * (-1);

                }
                else if (ConvertDecimalTwoPlaces(row["WTSumSC"]) <= 0)
                {
                    ReconciliacionInternaDetallesLines.WTSumSC = ConvertDecimalTwoPlaces(row["WTSumSC"]);
                }

                listReconciliacionInternaDetallesLines.Add(ReconciliacionInternaDetallesLines);

            }

            return listReconciliacionInternaDetallesLines;
        }

        private List<ReconciliacionInternaCabecera> ConvertDataRowView(DataRowView dataRow)
        {
            List<ReconciliacionInternaCabecera> listaInternalReconciliation = new List<ReconciliacionInternaCabecera>();

            ReconciliacionInternaCabecera internalReconciliation = new ReconciliacionInternaCabecera();

            if (row_Selected != null)
            {
                ReconJEId = Convert.ToInt32(row_Selected["ReconJEId"].ToString());//obtiene asiento de la reconciliacion

                var result = cr.SelectReconNum();
                internalReconciliation.ReconNum = result.Item1;
                internalReconciliation.IsCard= Convert.ToChar(row_Selected["IsCard"].ToString());
                internalReconciliation.Canceled = 'Y';
                internalReconciliation.InitObjAbs = 0;
                internalReconciliation.InitObjTyp = null;
                internalReconciliation.ReconJEId = 0;
                internalReconciliation.ObjType= row_Selected["ObjType"].ToString();
                internalReconciliation.Total = ConvertDecimalTwoPlaces(row_Selected["Total"].ToString());
                internalReconciliation.ReconType = cr.GetReconType("Cancelacion"); 
                internalReconciliation.ReconDate = Convert.ToDateTime(row_Selected["ReconDate"].ToString());
                internalReconciliation.CreateDate = (DateTime)fechaActual.GetFechaActual();
                internalReconciliation.CancelAbs = Convert.ToInt32(row_Selected["ReconNum"].ToString());
                internalReconciliation.ReconCurr = row_Selected["ReconCurr"].ToString();
                internalReconciliation.UserSign = Properties.Settings.Default.Usuario;

                listaInternalReconciliation.Add(internalReconciliation);

            }

            return listaInternalReconciliation;
        }

        
        private void imgSrcObjTyp_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void dgHistorialReconciliacion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int reconNum = 0;

            string tipoRecon = null;

            int cancelAbs = 0;

            DataGrid dg = (DataGrid)sender;

            row_Selected = dg.SelectedItem as DataRowView;

            if (row_Selected != null)
            {              

                tipoRecon = row_Selected["ReconType"].ToString();

                try
                {
                    cancelAbs = Convert.ToInt32(row_Selected["CancelAbs"].ToString());
                }
                catch(Exception ex)
                {
                    cancelAbs = 0;
                }

                DisableBtnCancelarReconciliation(tipoRecon, cancelAbs);

                reconNum = Convert.ToInt32(row_Selected["ReconNum"].ToString());

                FindReconciliationInternalAnteriores(reconNum);

                
            }
        }

        private void DisableBtnCancelarReconciliation(string tipoRecon, int cancelAbs)
        {
            if(tipoRecon=="Manual" && cancelAbs == 0)
            {
                btnCancelarReconciliacion.IsEnabled = true;
            }
            else
            {
                btnCancelarReconciliacion.IsEnabled = false;
            }
        }

        private void FindReconciliationInternalAnteriores(int reconNum)
        {
            var result = cn.FindReconciliacionesAnterioresLines(reconNum);

            if (result.Item2 == null)
            {
                dt = result.Item1;

                dgDetallesReconciliacion.ItemsSource = dt.DefaultView;

                dgDetallesReconciliacion.CanUserAddRows = false;

                dgDetallesReconciliacion.CanUserDeleteRows = false;

                dgDetallesReconciliacion.CanUserSortColumns = false;
            }
            else
            {

            }
        }
    }
}
