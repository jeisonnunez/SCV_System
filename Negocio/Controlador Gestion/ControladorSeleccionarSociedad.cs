using Datos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Negocio
{
    public class ControladorSeleccionarSociedad: ControladorLogin
    {
        ModeloSeleccionarSociedad cn = new ModeloSeleccionarSociedad();

        public Tuple <DataView,string> ConsultaSociedades()
        {
            var result = cn.ConsultaSociedades();

            DataTable tabla = result.Item1;

            DataView vista;

            AgregaNombreEmpresa(tabla);

            vista = tabla.DefaultView;

            return Tuple.Create(vista,result.Item2);
        }

        public void AgregaNombreEmpresa(DataTable dataTable)
        {
            try
            {       
                DataColumn newCol = new DataColumn("NombreSociedad", typeof(string));

                newCol.AllowDBNull = true;

                dataTable.Columns.Add(newCol);

                foreach (DataRow row in dataTable.Rows)
                {
                    EstableceConexionStringTemp(row["name"].ToString());

                    obtenerCadenaConexion();

                    var society= GetNameSociety();

                    row["NombreSociedad"] = society.Item1;

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message);
            }
        }

        public string obtenerEmpresa()
        {
            return cn.obtenerEmpresa();
        }
    }
}
