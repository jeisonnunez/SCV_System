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
    public class ModeloGrupoUnidadesMedida : ConexionSQLServer
    {
        public Tuple<DataTable,string> ConsultaGrupoUnidadesMedida()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT T0.UgpEntry, T0.UgpCode, T0.UgpName, T0.BaseUom, T1.UomCode FROM OUGP T0 INNER JOIN OUOM T1 ON T0.BaseUom=T1.UomEntry";

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);

                DataTable tabla = new DataTable();

                data.Fill(tabla);

                Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {

                DataTable tabla = new DataTable();               

                return Tuple.Create(tabla, e.Message);
            }
            finally
            {
                Connection.Close();
            }
        }

        public Tuple<List<GrupoUnidadesMedidaCabecera>, string> ConsultaGrupoUnidadesMedidas()
        {
            List<GrupoUnidadesMedidaCabecera> newListaGrupoUnidadesMedida = new List<GrupoUnidadesMedidaCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindGrupoUnidadesMedida";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            GrupoUnidadesMedidaCabecera newGrupoUnidades = new GrupoUnidadesMedidaCabecera();
                            
                            newGrupoUnidades.UgpEntry =Convert.ToInt32(reader["UgpEntry"].ToString());
                            newGrupoUnidades.UgpCode = reader["UgpCode"].ToString();
                            newGrupoUnidades.UgpName = reader["UgpName"].ToString();                            
                            newGrupoUnidades.BaseUom=Convert.ToInt32(reader["BaseUom"].ToString());

                            newListaGrupoUnidadesMedida.Add(newGrupoUnidades);

                        }

                        reader.Close();
                    }



                }

                Connection.Close();

                return Tuple.Create(newListaGrupoUnidadesMedida, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaGrupoUnidadesMedida, e.Message);
            }
        }

        public Tuple<int, string> InsertaGrupoUnidadesMedidaCabecera(List<GrupoUnidadesMedidaCabecera> listaGrupoUnidadesMedida)
        {
            int ugpEntry = 0;

            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

               


                    foreach (GrupoUnidadesMedidaCabecera grupoUnidadesMedida in listaGrupoUnidadesMedida)
                    {

                    using (Connection = new SqlConnection(connectionString))
                    {
                        Connection.Open();

                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateGruposUnidadesMedidaCabecera";

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (String.IsNullOrWhiteSpace(grupoUnidadesMedida.OldUgpCode) == false)
                            {
                                cmd.Parameters.AddWithValue("@OldUgpCode", grupoUnidadesMedida.OldUgpCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OldUgpCode", grupoUnidadesMedida.UgpCode);

                            }
                           
                            cmd.Parameters.AddWithValue("@UgpCode", grupoUnidadesMedida.UgpCode);
                            cmd.Parameters.AddWithValue("@UgpName", grupoUnidadesMedida.UgpName);
                            cmd.Parameters.AddWithValue("@BaseUom", grupoUnidadesMedida.BaseUom);
                            cmd.Parameters.AddWithValue("@UserSign", grupoUnidadesMedida.UserSign);
                            cmd.Parameters.AddWithValue("@LogInstanc", grupoUnidadesMedida.LogInstanc);
                            cmd.Parameters.AddWithValue("@UpdateDate", grupoUnidadesMedida.UpdateDate);
                          

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;


                        }

                        //----------------------------------GetUpgEntry.--------------------------------------

                        using (Connection = new SqlConnection(connectionString))
                        {

                            Connection.Open();

                            using (SqlCommand cmd = Connection.CreateCommand())
                            {
                                cmd.CommandText = "FindUgpEntry";

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@UgpCode", grupoUnidadesMedida.UgpCode);

                                SqlDataReader reader = cmd.ExecuteReader();

                                if (reader.Read())
                                {
                                    ugpEntry = Convert.ToInt32(reader["UgpEntry"]);
                                }

                                reader.Close();
                            }


                        }

                        //----------------------------------------------------------------------------------------

                        using (Connection = new SqlConnection(connectionString))
                        {

                            Connection.Open();


                            using (SqlCommand cmd = Connection.CreateCommand())
                            {
                                cmd.CommandText = "AddUpdateGruposUnidadesMedidaDetalleFirstLine";

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@UgpEntry", ugpEntry);
                                cmd.Parameters.AddWithValue("@UomEntry", grupoUnidadesMedida.BaseUom);
                                cmd.Parameters.AddWithValue("@AltQty", 1);
                                cmd.Parameters.AddWithValue("@BaseQty", 1);
                                cmd.Parameters.AddWithValue("@LineNum", 1);
                                cmd.Parameters.AddWithValue("@LogInstanc", grupoUnidadesMedida.LogInstanc);
                                cmd.Parameters.AddWithValue("@WghtFactor", 0);
                                cmd.Parameters.AddWithValue("@UdfFactor", -1);

                                flag = cmd.ExecuteNonQuery();

                                i = flag + i;
                            }


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

        public Tuple<int, string> EliminaGrupoUnidadMedida(string ugpCode)
        {
            throw new NotImplementedException();
        }
    }
}
