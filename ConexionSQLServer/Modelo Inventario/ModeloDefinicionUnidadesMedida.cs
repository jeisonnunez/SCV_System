using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.Modelo_Inventario
{
    public class ModeloDefinicionUnidadesMedida : ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaDefinicionUnidadesMedida(int ugpEntry)
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
                        cmd.CommandText = "FindDefinicionUnidadesMedida";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UgpEntry", ugpEntry);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }

                }

                //Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {
                //Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<List<UnidadesMedida>, string> ConsultaDefinicionUnidadesMedidaSpecific(int ugpEntry)
        {
            List<UnidadesMedida> newListaUnidadesMedida = new List<UnidadesMedida>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindDefinicionUnidadesMedida";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UgpEntry", ugpEntry);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UnidadesMedida newCuentaAsociada = new UnidadesMedida();

                            newCuentaAsociada.UomCode = reader["UomCode"].ToString();
                            newCuentaAsociada.UomName = reader["UomName"].ToString();
                            newCuentaAsociada.UomEntry = Convert.ToInt32(reader["UomEntry"].ToString());
                            newCuentaAsociada.AltQty = Convert.ToDecimal(reader["AltQty"].ToString());
                            newCuentaAsociada.BaseQty = Convert.ToDecimal(reader["BaseQty"].ToString());

                            newListaUnidadesMedida.Add(newCuentaAsociada);

                        }

                        reader.Close();
                    }

                }

                return Tuple.Create(newListaUnidadesMedida, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(newListaUnidadesMedida, e.Message);

            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<DataTable, string> ConsultaDefinicionUnidadesMedidaFirstLine(int ugpEntry)
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
                        cmd.CommandText = "FindDefinicionUnidadesMedidaFirst";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UgpEntry", ugpEntry);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }

                }

                //Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {
                //Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<List<UnidadesMedida>, string> ConsultaUnidadesBaseMedidas(int baseUom)
        {
            List<UnidadesMedida> newListaUnidadesMedida = new List<UnidadesMedida>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindUnidadesBaseMedidasSpecific";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@BaseUom", baseUom);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UnidadesMedida newCuentaAsociada = new UnidadesMedida();

                            newCuentaAsociada.UomCode = reader["UomCode"].ToString();
                            newCuentaAsociada.UomName = reader["UomName"].ToString();
                            newCuentaAsociada.UomEntry = Convert.ToInt32(reader["UomEntry"].ToString());

                            newListaUnidadesMedida.Add(newCuentaAsociada);

                        }

                        reader.Close();
                    }

                }
               
                return Tuple.Create(newListaUnidadesMedida, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(newListaUnidadesMedida, e.Message);

            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<List<UnidadesMedida>, string> ConsultaUnidadesBaseMedidas()
        {
            List<UnidadesMedida> newListaUnidadesMedida = new List<UnidadesMedida>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindUnidadesBaseMedidas";

                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            UnidadesMedida newCuentaAsociada = new UnidadesMedida();

                            newCuentaAsociada.UomCode = reader["UomCode"].ToString();
                            newCuentaAsociada.UomName = reader["UomName"].ToString();
                            newCuentaAsociada.UomEntry = Convert.ToInt32(reader["UomEntry"].ToString());

                            newListaUnidadesMedida.Add(newCuentaAsociada);

                        }

                        reader.Close();
                    }

                }

                return Tuple.Create(newListaUnidadesMedida, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(newListaUnidadesMedida, e.Message);

            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<int, string> InsertaUnidadesMedidaDefinicionDetalle(List<GrupoUnidadesMedidaDetalle> listaUnidadesMedida)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (GrupoUnidadesMedidaDetalle unidadesMedida in listaUnidadesMedida)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateGruposUnidadesMedidaDetalle";

                            cmd.CommandType = CommandType.StoredProcedure;
                            
                            cmd.Parameters.AddWithValue("@UgpEntry", unidadesMedida.UgpEntry);            
                            cmd.Parameters.AddWithValue("@UomEntry", unidadesMedida.UomEntry);
                            cmd.Parameters.AddWithValue("@AltQty", unidadesMedida.AltQty);
                            cmd.Parameters.AddWithValue("@BaseQty", unidadesMedida.BaseQty);
                            cmd.Parameters.AddWithValue("@LineNum", unidadesMedida.LineNum);
                            cmd.Parameters.AddWithValue("@LogInstanc", unidadesMedida.LogInstanc);
                            cmd.Parameters.AddWithValue("@WghtFactor", unidadesMedida.WghtFactor);
                            cmd.Parameters.AddWithValue("@UdfFactor", unidadesMedida.UdfFactor);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<int, string> EliminaDefinicionGrupoUnidadMedida(int ugpEntry, int lineNum)
        {
            throw new NotImplementedException();
        }
    }
}
