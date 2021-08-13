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
    public class ModeloPeriodos: ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaPeriodos()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT * FROM OFPR";

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);

                DataTable tabla = new DataTable();                

                data.Fill(tabla);               

                Connection.Close();

                return Tuple.Create(tabla,error);

            }
            catch (Exception e)
            {              
                DataTable tabla = new DataTable();

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }
    }
}
