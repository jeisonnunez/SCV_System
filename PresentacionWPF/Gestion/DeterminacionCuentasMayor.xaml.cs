using Entidades;
using Microsoft.Office.Interop.Excel;
using Negocio.Controlador_Gestion;
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
using Vista.Gestion.ValidateErrorsDeterminacionMayor;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para DeterminacionCuentasMayor.xaml
    /// </summary>
    public partial class DeterminacionCuentasMayor :Document
    {
        ControladorDeterminacionCuentasMayor cn = new ControladorDeterminacionCuentasMayor();

        System.Data.DataTable dtCuentas = new System.Data.DataTable();

        List<ModelDeterminacionMayor> listDeterminacionMayor = new List<ModelDeterminacionMayor>();
        public DeterminacionCuentasMayor()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDgCuentas();
        }

        public void LoadedWindow()
        {
            List<Entidades.DeterminacionCuentasMayor> listDeterminacion = new List<Entidades.DeterminacionCuentasMayor>();

            var result = cn.FindDeterminacionCuentasMayor();

            if (result.Item2 == null)
            {

                listDeterminacion = result.Item1;

                //listDeterminacionMayor = ConvertModelDeterminacionMayor(listDeterminacion);

                //SetDeterminacionModel(listDeterminacion);

                //SetDeterminacion(listDeterminacionMayor);

                SetDeterminacion(listDeterminacion);

                btnOK.Content = "OK";
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

           
        }

        private List<ModelDeterminacionMayor> ConvertModelDeterminacionMayor(List<Entidades.DeterminacionCuentasMayor> listaResultado)
        {
            List<ModelDeterminacionMayor> newList = new List<ModelDeterminacionMayor>();

            foreach (Entidades.DeterminacionCuentasMayor row in listaResultado)
            {
                ModelDeterminacionMayor list = new ModelDeterminacionMayor();

                //list.Old_Code = row.Old_Code;
                //list.Code = row.Code;
                //list.Name = row.Name;
                //list.Rate = row.Rate.ToString();
                //list.UpdateDate = row.UpdateDate;
                //list.UserSign = row.UserSign;
                //list.ValidForAP = (bool)cn.EstadoComprasInverso(row.ValidForAP);
                //list.ValidForAR = (bool)cn.EstadoVentasInverso(row.ValidForAR);
                //list.Lock1 = (bool)cn.EstadoLockInverso(row.Lock1);
                //list.U_IDA_Alicuota = row.U_IDA_Alicuota;

                newList.Add(list);

            }

            return newList;
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void LoadDgCuentas()
        {
            DataColumn dcID = new DataColumn();

            dcID.ColumnName = "ID";
            dcID.DataType = System.Type.GetType("System.Int32");
            dcID.AutoIncrement = true;
            dcID.AutoIncrementSeed = 1;
            dcID.AutoIncrementStep = 1;
            dcID.AllowDBNull = false;
            dcID.ReadOnly = true;

            DataColumn dcType = new DataColumn();

            dcType.ColumnName = "Type";
            dcType.DataType = System.Type.GetType("System.String");
            dcType.AllowDBNull = false;
            dcType.ReadOnly = true;

            DataColumn dcAcctCode = new DataColumn();

            dcAcctCode.ColumnName = "AcctCode";
            dcAcctCode.DataType = System.Type.GetType("System.String");
            dcAcctCode.AllowDBNull = true;
            dcAcctCode.ReadOnly = false;

            DataColumn dcAcctName = new DataColumn();

            dcAcctName.ColumnName = "AcctName";
            dcAcctName.DataType = System.Type.GetType("System.String");
            dcAcctName.AllowDBNull = true;
            dcAcctName.ReadOnly = false;

            dtCuentas.Columns.Add(dcID);
            dtCuentas.Columns.Add(dcType);
            dtCuentas.Columns.Add(dcAcctCode);
            dtCuentas.Columns.Add(dcAcctName);

            DataRow newRow = dtCuentas.NewRow();

            newRow["Type"] = "Diferencia Reconciliacion Automatica";

            newRow["AcctCode"] = "";

            newRow["AcctName"] = "";

            dtCuentas.Rows.Add(newRow);            

            DataRow newRow1 = dtCuentas.NewRow();

            newRow1["Type"] = "Cuenta Cierre Periodo";

            newRow1["AcctCode"] = "";

            newRow1["AcctName"] = "";

            dtCuentas.Rows.Add(newRow1);

           
            DataRow newRow2 = dtCuentas.NewRow();

            newRow2["Type"] = "Beneficio Diferencia Tipo Cambio";

            newRow2["AcctCode"] = "";

            newRow2["AcctName"] = "";

            dtCuentas.Rows.Add(newRow2);

            

            DataRow newRow3 = dtCuentas.NewRow();

            newRow3["Type"] = "Perdida Diferencia Tipo Cambio";

            newRow3["AcctCode"] = "";

            newRow3["AcctName"] = "";

            dtCuentas.Rows.Add(newRow3);

            

            DataRow newRow4 = dtCuentas.NewRow();

            newRow4["Type"] = "Beneficio Diferencia Conversion";

            newRow4["AcctCode"] = "";

            newRow4["AcctName"] = "";

            dtCuentas.Rows.Add(newRow4);

            

            DataRow newRow5= dtCuentas.NewRow();

            newRow5["Type"] = "Perdida Diferencia Conversion";

            newRow5["AcctCode"] = "";

            newRow5["AcctName"] = "";

            dtCuentas.Rows.Add(newRow5);

            

            DataRow newRow6 = dtCuentas.NewRow();

            newRow6["Type"] = "Cuenta Redondeo";

            newRow6["AcctCode"] = "";

            newRow6["AcctName"] = "";

            dtCuentas.Rows.Add(newRow6);

            

            dgCuentas.ItemsSource = dtCuentas.DefaultView;
        }

        private void SetDeterminacion(List<ModelDeterminacionMayor> listaDeterminacion)
        {
            
            LoadedDgCuentas(listaDeterminacion);
        }

        private void SetDeterminacion(List<Entidades.DeterminacionCuentasMayor> listaDeterminacion)
        {

            LoadedDgCuentas(listaDeterminacion);
        }

        private void SetDeterminacionModel(List<Entidades.DeterminacionCuentasMayor> listaDeterminacion)
        {

            LoadedDgCuentasModel(listaDeterminacion);
        }

        private void LoadedDgCuentas(List<ModelDeterminacionMayor> listaDeterminacion)
        {
            foreach (ModelDeterminacionMayor account in listaDeterminacion)
            {
                foreach (DataRow row in dtCuentas.Rows)
                {
                    foreach (DataColumn column in dtCuentas.Columns)
                    {

                        if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Diferencia Reconciliacion Automatica")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Cierre Periodo")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Tipo Cambio")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Tipo Cambio")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Conversion")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Conversion")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Redondeo")
                        {
                            row["AcctCode"] = account.AcctCode;
                            row["AcctName"] = cn.FindAcctName(account.AcctCode);

                        }
                    }
                }
            }
        }

        private void LoadedDgCuentasModel(List<Entidades.DeterminacionCuentasMayor> listaDeterminacion)
        {
            listDeterminacionMayor = new List<ModelDeterminacionMayor>();

            foreach (Entidades.DeterminacionCuentasMayor account in listaDeterminacion)
            {
                foreach (DataRow row in dtCuentas.Rows)
                {
                    ModelDeterminacionMayor modelDeterminacionMayor = new ModelDeterminacionMayor();

                    foreach (DataColumn column in dtCuentas.Columns)
                    {

                        if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Diferencia Reconciliacion Automatica")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_1;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_1);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Cierre Periodo")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_2;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_2);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Tipo Cambio")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_3;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_3);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Tipo Cambio")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_4;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_4);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Conversion")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_5;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_5);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Conversion")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_6;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_6);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Redondeo")
                        {
                            modelDeterminacionMayor.AcctCode = account.LinkAct_7;
                            modelDeterminacionMayor.AcctName = cn.FindAcctName(account.LinkAct_7);

                        }
                    }

                    listDeterminacionMayor.Add(modelDeterminacionMayor);
                }
            }
        }

        private void LoadedDgCuentas(List<Entidades.DeterminacionCuentasMayor> listaDeterminacion)
        {
            //listDeterminacionMayor = new List<ModelDeterminacionMayor>();

            foreach (Entidades.DeterminacionCuentasMayor account in listaDeterminacion)
            {
                foreach (DataRow row in dtCuentas.Rows)
                {
                   

                    foreach (DataColumn column in dtCuentas.Columns)
                    {

                        if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Diferencia Reconciliacion Automatica")
                        {
                            row["AcctCode"] = account.LinkAct_1;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_1);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Cierre Periodo")
                        {
                            row["AcctCode"] = account.LinkAct_2;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_2);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Tipo Cambio")
                        {
                            row["AcctCode"] = account.LinkAct_3;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_3);

                        }
                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Tipo Cambio")
                        {
                            row["AcctCode"] = account.LinkAct_4;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_4);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Conversion")
                        {
                            row["AcctCode"] = account.LinkAct_5;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_5);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Conversion")
                        {
                            row["AcctCode"] = account.LinkAct_6;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_6);

                        }

                        else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Redondeo")
                        {
                            row["AcctCode"] = account.LinkAct_7;
                            row["AcctName"] = cn.FindAcctName(account.LinkAct_7);

                        }
                    }

                    //listDeterminacionMayor.Add(modelDeterminacionMayor);
                }
            }
        }

        private void imgAccount_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row_list = GetDataGridRows(dgCuentas);

                DataRowView row_Selected = dgCuentas.SelectedItem as DataRowView;

                foreach (DataGridRow single_row in row_list)
                {
                    if (single_row.IsSelected == true)
                    {
                        System.Windows.Controls.TextBox txtShortName = FindChild<System.Windows.Controls.TextBox>(single_row, "txtAccount");

                        TextBlock txtAcctName = FindChild<TextBlock>(single_row, "txtAcctName");

                        var result = cn.ConsultaCuentasNoAsociadas();

                        if (result.Item2 == null)
                        {
                            RecorreListaAccount(result.Item1, txtShortName, txtAcctName, row_Selected);

                            btnOK.Content = "Actualizar";
                        }
                        else
                        {
                            EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                        }


                    }
                }
            }

            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error" + ex.Message, Brushes.Red, Brushes.Black, "003-interface-2.png");
            }

        }

        private void RecorreListaAccount(List<Cuenta> listAccountResultante, System.Windows.Controls.TextBox txtShortName, TextBlock txtAcctName, DataRowView row_Selected)
        {
            if (listAccountResultante.Count == 0)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se encontro ningun resultado con los parametros establecidos", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }

            else if (listAccountResultante.Count > 0)
            {
                ListaCuentas ventanaListaCuentaAsociada = new ListaCuentas(listAccountResultante);

                ventanaListaCuentaAsociada.ShowDialog();

                if (ventanaListaCuentaAsociada.Visibility == Visibility.Hidden)
                {
                    if (ventanaListaCuentaAsociada.GetListAccount().Count == 0)
                    {

                    }
                    else
                    {
                        GetAcctCode(ventanaListaCuentaAsociada.GetListAccount(), txtShortName, txtAcctName, row_Selected);
                    }
                }
            }
        }

        private void GetAcctCode(List<Cuenta> listaCuenta, System.Windows.Controls.TextBox txtShortName, TextBlock txtAcctName, DataRowView row_Selected)
        {
            foreach (Cuenta cuenta in listaCuenta)
            {
                txtShortName.Text = cuenta.AcctCode;

                row_Selected["AcctCode"] = cuenta.AcctCode;

                row_Selected["AcctName"] = cuenta.AcctName;

                txtAcctName.Text = row_Selected["AcctName"].ToString();
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            List<Entidades.DeterminacionCuentasMayor> listaDeterminacion = new List<Entidades.DeterminacionCuentasMayor>();

            Entidades.DeterminacionCuentasMayor DeterminacionAccount = new Entidades.DeterminacionCuentasMayor();

            switch (btnOK.Content.ToString())
            {
                case "OK":

                    this.Hide();

                    break;

                case "Actualizar":

                    DeterminacionAccount = GetDeterminacion();

                    listaDeterminacion.Add(DeterminacionAccount);

                    var result2 = cn.UpdateDeterminacionCuentasMayor(listaDeterminacion);

                    if (result2.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Operacion Realizada Exitosamente " , Brushes.LightGreen, Brushes.Black, "001-interface.png");
                        btnOK.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion de la determinacion de cuenta " +  result2.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    break;
            }
                    
        }

        private Entidades.DeterminacionCuentasMayor GetDeterminacion()
        {
            Entidades.DeterminacionCuentasMayor determinacionCuentasMayor = new Entidades.DeterminacionCuentasMayor();

            foreach (DataRow row in dtCuentas.Rows)
            {
                foreach (DataColumn column in dtCuentas.Columns)
                {

                    if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Diferencia Reconciliacion Automatica")
                    {
                        determinacionCuentasMayor.LinkAct_1 = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Cierre Periodo")
                    {
                        determinacionCuentasMayor.LinkAct_2 = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Tipo Cambio")
                    {
                        determinacionCuentasMayor.LinkAct_3 = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Tipo Cambio")
                    {
                        determinacionCuentasMayor.LinkAct_4 = row["AcctCode"].ToString();

                    }

                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Beneficio Diferencia Conversion")
                    {
                        determinacionCuentasMayor.LinkAct_5 = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Perdida Diferencia Conversion")
                    {
                        determinacionCuentasMayor.LinkAct_6 = row["AcctCode"].ToString();

                    }
                    else if (column.ToString() == "AcctCode" && row["Type"].ToString() == "Cuenta Redondeo")
                    {
                        determinacionCuentasMayor.LinkAct_7 = row["AcctCode"].ToString();

                    }
                }
            }

            return determinacionCuentasMayor;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void txtAccount_KeyDown(object sender, KeyEventArgs e)
        {
            btnOK.Content = "Actualizar";
        }
    }
}
