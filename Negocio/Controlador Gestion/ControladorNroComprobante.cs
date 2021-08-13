using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Datos;
using Entidades;

namespace Negocio
{
   public class ControladorNroComprobante: Negocios
    {
        ModeloNroComprobante cn = new ModeloNroComprobante();

        public Tuple <DataTable,string> ConsultaNroComprobante()
        {
            DataTable dt = new DataTable();

            DataTable newDt = new DataTable();

            var result = cn.ConsultaNroComprobante();

            dt = result.Item1.Copy();

            dt.AcceptChanges();

            newDt = TraduceNroComprobantes(dt);

            newDt = ChangeTypeColumnNroComprobantes(newDt);

            newDt = AddColumnOld(newDt, "OldCode", "Code");

            return Tuple.Create(newDt, result.Item2);       
         
        }

        public DataTable ChangeTypeColumnNroComprobantes(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "Activo")
                {

                    dtCloned.Columns[i].DataType = typeof(bool);
                }
                else if (dtCloned.Columns[i].ColumnName.ToString() == "Canceled")
                {

                    dtCloned.Columns[i].DataType = typeof(char);
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

        public Tuple <int,string> EliminaNroComprobante(string nroComprobante)
        {
            return cn.EliminaNroComprobante(nroComprobante);
        }

        public Tuple <int,string> InsertaNroComprobante(List<NroComprobante> listaNroComprobante)
        {
            return cn.InsertaNroComprobante(listaNroComprobante);

        }

        public DataTable TraduceNroComprobantes(DataTable table)
        {
            DataTable newTable = table.Copy();

            foreach (DataRow row in newTable.Rows)
            {
                foreach (DataColumn column in newTable.Columns)
                {                   

                    if (column.ToString() == "Activo" && row[column].ToString() == ((char)EstadosActivo.Activo).ToString())
                    {
                        row[column] = true;

                    }
                    else

                    if (column.ToString() == "Activo" && row[column].ToString() == ((char)EstadosActivo.Inactivo).ToString())
                    {
                        row[column] = false;

                    }


                }
            }

            newTable.AcceptChanges();

            return newTable;
        }

        public char EstadoNroComprobantes(bool estado)
        {
            char caracter = 'N';

            if (estado == true)
            {
                caracter = (char)EstadosActivo.Activo;

            }
            else

            if (estado == false)
            {
                caracter = (char)EstadosActivo.Inactivo;

            }


            return caracter;
        }

     
        enum EstadosActivo { Activo = 'Y', Inactivo='N'};

   }
}
