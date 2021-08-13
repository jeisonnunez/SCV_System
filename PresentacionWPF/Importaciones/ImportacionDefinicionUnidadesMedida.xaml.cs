using Entidades;
using Microsoft.Office.Interop.Excel;
using Negocio.Controlador_Inventario;
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

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para ImportacionDefinicionUnidadesMedida.xaml
    /// </summary>
    public partial class ImportacionDefinicionUnidadesMedida : Document
    {
        ControladorUnidadesMedida cn = new ControladorUnidadesMedida();

        System.Data.DataTable dataTable = new System.Data.DataTable();
        public ImportacionDefinicionUnidadesMedida()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, RoutedEventArgs e)
        {
            int rows = dataTable.Rows.Count;

            int j = 0;

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                List<Entidades.UnidadesMedida> listaUnidadesMedida = new List<Entidades.UnidadesMedida>();

                DataRow row = dataTable.Rows[i];
                               
                Entidades.UnidadesMedida unidadesMedida = new Entidades.UnidadesMedida();

                unidadesMedida.OldUomCode =row["UomCode"].ToString();
                unidadesMedida.UomCode = row["UomCode"].ToString();
                unidadesMedida.UomName = row["UomName"].ToString();
                unidadesMedida.UpdateDate = (DateTime)fechaActual.GetFechaActual();
                unidadesMedida.UserSign = Properties.Settings.Default.Usuario;
                unidadesMedida.Length = ConvertDecimalTwoPlaces(row["Length"]);
                unidadesMedida.Width = ConvertDecimalTwoPlaces(row["Width"]);
                unidadesMedida.Height = ConvertDecimalTwoPlaces(row["Height"]);
                unidadesMedida.Volume = ConvertDecimalTwoPlaces(row["Volume"]);
                unidadesMedida.Weight = ConvertDecimalTwoPlaces(row["Weight"]);
                unidadesMedida.VolUnit = cn.GetVolUnit(row["VolUnit"].ToString());

                listaUnidadesMedida.Add(unidadesMedida);

                var result = cn.InsertaUnidadesMedidaDefinicion(listaUnidadesMedida);

                if (result.Item1 == 1)
                {

                    j++;

                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                    dgImportarUnidadesMedida.ItemsSource = dataTable.DefaultView;

                }
                else
                {
                    EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la unidad de medida " + unidadesMedida.UomCode + ": " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");

                }

            }

            if (j == rows)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Importacion exitosa de " + rows + " unidad de medida", Brushes.LightGreen, Brushes.Black, "001-interface.png");
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la importacion de alguna unidad de medida. Debe revisar el log del sistema: ", Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataTable.Columns.Add("UomCode");
            dataTable.Columns.Add("UomName");
            dataTable.Columns.Add("Length");
            dataTable.Columns.Add("Width");
            dataTable.Columns.Add("Height");
            dataTable.Columns.Add("Volume");
            dataTable.Columns.Add("Weight");
            dataTable.Columns.Add("VolUnit");
           
        }

        private void RemoveDataRow(System.Data.DataTable dataTable)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                DataRow row = dataTable.Rows[i];

                if (AreAllColumnsEmpty(row) == true)
                {
                    dataTable.Rows.Remove(row);

                    dataTable.AcceptChanges();

                    i--;

                }

            }

            dgImportarUnidadesMedida.ItemsSource = dataTable.DefaultView;


        }

        private bool AreAllColumnsEmpty(DataRow dr)
        {

            foreach (var value in dr.ItemArray)
            {
                if (String.IsNullOrWhiteSpace(value.ToString()) == false)
                {
                    return false;
                }
            }
            return true;

        }

        public void LoadedWindow()
        {
            dataTable.Rows.Clear();

            dgImportarUnidadesMedida.ItemsSource = dataTable.DefaultView;
        }

        public System.Data.DataTable formofDataTable(Microsoft.Office.Interop.Excel.Worksheet ws)
        {
            try
            {
                string worksheetName = ws.Name;

                dataTable.Rows.Clear();

                dataTable.AcceptChanges();

                dataTable.TableName = worksheetName;
                Microsoft.Office.Interop.Excel.Range xlRange = ws.UsedRange;
                object[,] valueArray = (object[,])xlRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);


                object[] singleDValue = new object[valueArray.GetLength(1)]; //value array first row contains column names. so loop starts from 2 instead of 1
                for (int i = 2; i <= valueArray.GetLength(0); i++)
                {
                    for (int j = 0; j < valueArray.GetLength(1); j++)
                    {
                        if (valueArray[i, j + 1] != null)
                        {
                            singleDValue[j] = valueArray[i, j + 1].ToString();
                        }
                        else
                        {
                            singleDValue[j] = valueArray[i, j + 1];
                        }
                    }
                    dataTable.LoadDataRow(singleDValue, System.Data.LoadOption.PreserveChanges);
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error:" + ex.Message, Brushes.Red, Brushes.White, "003-interface-2.png");

                return dataTable;
            }


        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
