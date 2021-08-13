using Entidades;
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
    public class ModeloNroComprobante: ConexionSQLServer
    {       
        public Tuple<DataTable, string> ConsultaNroComprobante()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT Code, Canceled, NombreSerie, Descripcion, TipoSerie, Inicio,Siguiente, Fin, Activo FROM NRO_COMPROBANTE";

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

        public Tuple<int, string> InsertaNroComprobante(List<NroComprobante> listaNroComprobante)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (NroComprobante nroComprobante in listaNroComprobante)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateNroComprobante";

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (String.IsNullOrWhiteSpace(nroComprobante.OldCode) == false)
                            {
                                cmd.Parameters.AddWithValue("@OldCode", nroComprobante.OldCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OldCode", nroComprobante.Code);

                            }                         


                            cmd.Parameters.AddWithValue("@Code", nroComprobante.Code);
                            cmd.Parameters.AddWithValue("@NombreSerie", nroComprobante.NombreSerie);
                            cmd.Parameters.AddWithValue("@Descripcion", nroComprobante.Descripcion);
                            cmd.Parameters.AddWithValue("@UserSign", nroComprobante.UserSign);
                            cmd.Parameters.AddWithValue("@TipoSerie", nroComprobante.TipoSerie);
                            cmd.Parameters.AddWithValue("@Inicio", nroComprobante.Inicio);
                            cmd.Parameters.AddWithValue("@Siguiente", nroComprobante.Siguiente);
                            cmd.Parameters.AddWithValue("@Fin", nroComprobante.Fin);
                            cmd.Parameters.AddWithValue("@Activo", nroComprobante.Activo);
                            cmd.Parameters.AddWithValue("@UpdateDate", nroComprobante.UpdateDate);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(i, e.Message);
            }

        }

        public Tuple<int, string> EliminaNroComprobante(string nroComprobante)
        {
            int flag = 0;

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();

                SqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = "DeleteNroComprobante";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Code", nroComprobante);

                flag = cmd.ExecuteNonQuery();

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }
    }
}
