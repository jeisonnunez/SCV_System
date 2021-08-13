using Datos.Modelo_Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using System.Data;

namespace Negocio.Controlador_Inventario
{
    public class ControladorUnidadesMedida: Negocios
    {
        ModeloUnidadesMedida cn = new ModeloUnidadesMedida();

        public Tuple<DataTable,string> ConsultaUnidadesMedidaDefinicion()
        {
            var result = cn.ConsultaUnidadesMedidaDefinicion();

            DataTable dt = result.Item1;

            dt = ChangeTypeColumnUnidadesMedida(dt);

            dt = TraduceUnidadesMedida(dt);

            return Tuple.Create(dt, result.Item2);
          
        }

        

        public DataTable TraduceUnidadesMedida(DataTable table)
        {
            DataTable newTable = table.Copy();

            foreach (DataRow row in newTable.Rows)
            {
                row["VolUnit"].ToString();

                row["VolUnit"] = GetVolUnit(Convert.ToInt32(row["VolUnit"]));
            }

            newTable.AcceptChanges();

            return newTable;
        }

        private DataTable ChangeTypeColumnUnidadesMedida(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                if (dtCloned.Columns[i].ColumnName.ToString() == "UpdateDate")
                {
                    dtCloned.Columns[i].DataType = typeof(DateTime);

                }
                else if (dtCloned.Columns[i].ColumnName.ToString() == "UserSign")
                {
                    dtCloned.Columns[i].DataType = typeof(int);
                }

                else if (dtCloned.Columns[i].ColumnName.ToString() == "UomEntry")
                {
                    dtCloned.Columns[i].DataType = typeof(int);
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

        public Tuple<int,string> InsertaUnidadesMedidaDefinicion(List<UnidadesMedida> listaUnidadesMedida)
        {
            return cn.InsertaUnidadesMedidaDefinicion(listaUnidadesMedida);
        }

        public int GetVolUnit(string volUnit)
        {           
            int value = 0;

            if (volUnit == VolUnit.cc.ToString())
            {
                value = (int)VolUnit.cc;
            }
            else if (volUnit == VolUnit.ci.ToString())
            {
                value = (int)VolUnit.ci;
            }
            else if (volUnit == VolUnit.cm.ToString())
            {
                value = (int)VolUnit.cm;
            }
            else if (volUnit == VolUnit.cmm.ToString())
            {
                value = (int)VolUnit.cmm;
            }
            else if (volUnit == VolUnit.dm3.ToString())
            {
                value = (int)VolUnit.dm3;
            }
            else if (volUnit == VolUnit.vgl.ToString())
            {
                value = (int)VolUnit.vgl;
            }

            return value;
            
        }

        public string GetVolUnit(int volUnit)
        {
            string value = null;

            if (volUnit == (int)VolUnit.cc)
            {
                value = VolUnit.cc.ToString();
            }
            else if (volUnit == (int)VolUnit.ci)
            {
                value = VolUnit.ci.ToString();
            }
            else if (volUnit == (int)VolUnit.cm)
            {
                value = VolUnit.cm.ToString();
            }
            else if (volUnit == (int)VolUnit.cmm)
            {
                value = VolUnit.cmm.ToString();
            }
            else if (volUnit == (int)VolUnit.dm3)
            {
                value = VolUnit.dm3.ToString();
            }
            else if (volUnit == (int)VolUnit.vgl)
            {
                value = VolUnit.vgl.ToString();
            }

            return value;

        }

        enum VolUnit { cc = 2, ci = 5, cm=4, cmm=1, dm3=3, vgl=6};

       
    }
}
