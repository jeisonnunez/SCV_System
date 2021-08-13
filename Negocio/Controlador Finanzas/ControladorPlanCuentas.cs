using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Datos;
using Entidades;

namespace Negocio
{
    public class ControladorPlanCuentas: Negocios
    {
        ModeloPlanCuenta cn = new ModeloPlanCuenta();
        

        public List<Monedas> GetMonedas(DataTable dt)
        {
            List<Monedas> listaMonedas = new List<Monedas>();

            string currCode;

            string currName;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currCode = dt.Rows[i]["CurrCode"].ToString();
                currName = dt.Rows[i]["CurrName"].ToString();

                listaMonedas.Add(new Monedas(currCode, currName));
            }

            listaMonedas.Add(new Monedas("##","Monedas Todas"));

            return listaMonedas;
        }
        public Tuple<DataSet,string> ConsultaCuentas(int i)
        {
            var result = cn.FindAccounts(i);

            return Tuple.Create(result.Item1,result.Item2);
        }

        public Tuple<DataSet, string> ConsultaCuentas()
        {
            var result= cn.FindAccounts();

            return Tuple.Create(result.Item1, result.Item2);
        }

        public void CreateNodesNivel(TreeViewItem nodo)
        {
            TreeViewItem nodoPadre = new TreeViewItem();

            nodoPadre = (TreeViewItem)nodo.Parent;

            TreeViewItem nuevoNodo = new TreeViewItem();

            nuevoNodo.Header = "";

            nuevoNodo.Tag = "";

            nodoPadre.Items.Add(nuevoNodo);
            
            nodoPadre.IsExpanded = true;
        }

        public void CreateNodesSubordinada(TreeViewItem nodoPadre)
        {           
            TreeViewItem nuevoNodo = new TreeViewItem();

            nuevoNodo.Header = "";

            nuevoNodo.Tag = "";

            nodoPadre.Items.Add(nuevoNodo);

            nodoPadre.IsExpanded = true;
        }


        public void CreateFatherAccount(string indicePadre, TreeViewItem nodoPadre, DataSet dtSet, TreeView treeView)
        {           

            DataView dataViewHijos = new DataView(dtSet.Tables["OACT"]);

            dataViewHijos.RowFilter = dtSet.Tables["OACT"].Columns["FatherNum"].ColumnName + "='" + indicePadre + "'";

            foreach (DataRowView dataRowCurrent in dataViewHijos)
            {
                TreeViewItem nuevoNodo = new TreeViewItem();

                nuevoNodo.Header = dataRowCurrent["AcctCode"].ToString().Trim() + "   -    " + dataRowCurrent["AcctName"].ToString().Trim();

                nuevoNodo.Tag = dataRowCurrent["AcctCode"].ToString().Trim();

                //nuevoNodo.Name =dataRowCurrent["AcctCode"].ToString().Trim();

                if (nodoPadre == null)
                {
                    treeView.Items.Add(nuevoNodo);
                }
                else
                {
                    nodoPadre.Items.Add(nuevoNodo);
                    nodoPadre.IsExpanded = true;
                }

                CreateFatherAccount(dataRowCurrent["AcctCode"].ToString(), nuevoNodo,dtSet,treeView);
            }
        }

        public Tuple<DataTable,string> FindCuenta(string account)
        {
            DataTable dt = new DataTable();

            DataTable newDt = new DataTable();

            var result = cn.FindCuenta(account);

            dt = result.Item1.Copy();

            dt.AcceptChanges();

            newDt = PreparaCuentas(dt);

            newDt = ChangeTypeColumnPlanCuentas(newDt);

            newDt = AddColumnOld(newDt, "OldAcctCode", "AcctCode");

            return Tuple.Create(newDt, result.Item2);
        }

        public DataTable ChangeTypeColumnPlanCuentas(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "LocManTran" || dtCloned.Columns[i].ColumnName.ToString() == "Postable")
                {

                    dtCloned.Columns[i].DataType = typeof(bool);
                }

                else if (dtCloned.Columns[i].ColumnName.ToString() == "Finanse" || dtCloned.Columns[i].ColumnName.ToString() == "Advance")
                {

                    dtCloned.Columns[i].DataType = typeof(char);
                }

                else if (dtCloned.Columns[i].ColumnName.ToString() == "GroupMask" || dtCloned.Columns[i].ColumnName.ToString() == "UserSign")
                {

                    dtCloned.Columns[i].DataType = typeof(int);
                }

                else if (dtCloned.Columns[i].ColumnName.ToString() == "CreateDate" || dtCloned.Columns[i].ColumnName.ToString() == "UpdateDate")
                {

                    dtCloned.Columns[i].DataType = typeof(DateTime);
                }

                else
                {
                    dtCloned.Columns[i].DataType = typeof(string);
                }


                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public DataTable PreparaCuentas(DataTable table)
        {
            DataTable newTable = table.Copy();

            foreach (DataRow row in newTable.Rows)
            {
                               

                if (row["Postable"].ToString() == ((char)EstadosActivo.Titulo).ToString())
                {
                    row["Postable"] = false;

                }

                if (row["Postable"].ToString() == ((char)EstadosActivo.Activa).ToString())
                {
                    row["Postable"] = true;

                }

                if (row["LocManTran"].ToString() == ((char)EstadosAsociada.Asociada).ToString())
                {
                    row["LocManTran"] = true;

                }

                if (row["LocManTran"].ToString() == ((char)EstadosAsociada.NoAsociada).ToString())
                {
                    row["LocManTran"] = false;

                }

            }
            newTable.AcceptChanges();

            return newTable;
        }

        public char CuentaAsociada(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosAsociada.Asociada;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosAsociada.NoAsociada;

            }


            return caracter;
        }

        public bool? CuentaAsociada(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosAsociada.NoAsociada)
            {
                estado = false;

            }
            else

            if (caracter == (char)EstadosAsociada.Asociada)
            {
                estado = true;
            }


            return estado;
        }

        public char CuentaActivo(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosActivo.Titulo;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosActivo.Activa;

            }


            return caracter;
        }

        public bool? CuentaActivo(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosActivo.Titulo)
            {
                estado = false;

            }
            else

            if (caracter == (char)EstadosActivo.Activa)
            {
                estado = true;
            }


            return estado;
        }

        public Tuple<string,string> FindFatherNum(string account)
        {
            return cn.FindFatherNum(account);
        }

        public char CuentaTitulo(bool? estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosActivo.Activa;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosActivo.Titulo;

            }


            return caracter;
        }

        public List<TipoCuenta> ConsultaTipoCuenta()
        {            
            return GetAlicuota();
        }

        public List<TipoCuenta> GetAlicuota()
        {
            List<TipoCuenta> listaTipoCuenta = new List<TipoCuenta>();
           
            listaTipoCuenta.Add(new TipoCuenta("N", "Otros"));

            listaTipoCuenta.Add(new TipoCuenta("I", "Ingresos"));

            listaTipoCuenta.Add(new TipoCuenta("E", "Gastos"));

            return listaTipoCuenta;
        }

        public bool? CuentaTitulo(char caracter)
        {
            bool? estado = null;

            if (caracter == (char)EstadosActivo.Activa)
            {
                estado = false;

            }
            else

            if (caracter == (char)EstadosActivo.Titulo)
            {
                estado = true;
            }


            return estado;
        }

        public string TipoCuenta(char actType)
        {
            string tipoCuenta="";

            if (actType == (char)ClaseCuenta.Otros)
            {
                tipoCuenta = ClaseCuenta.Otros.ToString();
            }
            else if (actType == (char)ClaseCuenta.Ingresos)
            {
                tipoCuenta = ClaseCuenta.Ingresos.ToString();
            }
            else if (actType == (char)ClaseCuenta.Gastos)
            {
                tipoCuenta = ClaseCuenta.Gastos.ToString();
            }

            return tipoCuenta;
        }

        public char TipoCuenta(string actType)
        {
           char caracter = 'N';

            if (actType == ClaseCuenta.Otros.ToString())
            {
                caracter = (char)ClaseCuenta.Otros;
            }
            else if (actType == ClaseCuenta.Ingresos.ToString())
            {
               caracter= (char)ClaseCuenta.Ingresos;
            }
            else if (actType == ClaseCuenta.Gastos.ToString())
            {
               caracter =(char)ClaseCuenta.Gastos;
            }

            return caracter;
        }

        public Tuple<int,string> UpdateAccount(List<Cuenta> listaCuenta)
        {
            return cn.UpdateAccount(listaCuenta);
        }

        public Tuple<int, string> UpdateAccountTratar(List<Cuenta> listaCuenta)
        {
            return cn.UpdateAccountTratar(listaCuenta);
        }

        public Tuple<int, string> InsertAccount(List<Cuenta> listaCuenta)
        {
            return cn.InsertAccount(listaCuenta);
        }
        enum EstadosAsociada { Asociada = 'Y', NoAsociada = 'N' };

        enum EstadosActivo { Titulo = 'N', Activa = 'Y' };

        enum ClaseCuenta { Otros='N', Gastos='E', Ingresos='I' };

        public Tuple <int,string> DeleteAccount(string account)
        {
            return cn.DeleteAccount(account);
        }
    }

    public class StretchingTreeViewItem : TreeViewItem
    {
        public StretchingTreeViewItem()
        {
            this.Loaded += new RoutedEventHandler(StretchingTreeViewItem_Loaded);
        }

        private void StretchingTreeViewItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.VisualChildrenCount > 0)
            {
                Grid grid = this.GetVisualChild(0) as Grid;
                if (grid != null && grid.ColumnDefinitions.Count == 3)
                {
                    grid.ColumnDefinitions.RemoveAt(2);
                    grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                }
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new StretchingTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is StretchingTreeViewItem;
        }
    }

    public class StretchingTreeView : TreeView
    {
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new StretchingTreeViewItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is StretchingTreeViewItem;
        }
    }
}
