using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.Modelo_Socio_Negocio
{
    public class ModeloGestionarReconciliacionesAnteriores : ConexionSQLServer
    {
        public Tuple<DataTable,string> ExecuteReconciliacionesAnteriores(string txtDesdeSN, string txtHastaSN, DateTime? dpDFechaReconciliacion=null, DateTime? dpHFechaReconciliacion=null, int txtDNroReconciliacion=0, int txtHNroReconciliacion=0)
        {
            DataTable tabla = new DataTable();           

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();                   

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GenerateReconciliacionesAnteriores";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DesdeSN", txtDesdeSN);

                        cmd.Parameters.AddWithValue("@HastaSN",txtHastaSN);

                        cmd.Parameters.AddWithValue("@F_ReconDate", dpDFechaReconciliacion);

                        cmd.Parameters.AddWithValue("@T_ReconDate",dpHFechaReconciliacion);

                        cmd.Parameters.AddWithValue("@F_NroRecon", txtDNroReconciliacion);

                        cmd.Parameters.AddWithValue("@T_NroRecon",txtHNroReconciliacion);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }//end using                   

                }//end using

                Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }

        public Tuple<DataTable, string> FindReconciliacionesAnterioresLines(int reconNum)
        {
            DataTable tabla = new DataTable();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindReconciliacionesAnterioresLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ReconNum", reconNum);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }

                }

                Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }
    }
}
