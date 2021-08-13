using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloSeleccionarSociedad: ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaSociedades()
        {
            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                string query = "SELECT name, create_date FROM sys.databases where name LIKE 'SCV_%' AND name NOT IN('master', 'tempdb', 'model', 'msdb')";

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);

                DataTable tabla = new DataTable();

                data.Fill(tabla);

                return Tuple.Create(tabla,error);

            }
            catch (Exception e)
            {

                DataTable tabla = new DataTable();

                return Tuple.Create(tabla, e.Message);
            }

        }

        public string obtenerEmpresa()
        {
            return BaseDatosActual;
        }
    }
}
