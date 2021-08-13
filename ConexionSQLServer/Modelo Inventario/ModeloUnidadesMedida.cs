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
    public class ModeloUnidadesMedida : ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaUnidadesMedidaDefinicion()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT UomEntry,UomCode,UomName,UserSign,LogInstanc,Length1 AS 'Length',Width1 AS 'Width' ,Height1 AS 'Height' ,Volume ,VolUnit ,Weight1 AS 'Weight' FROM OUOM";

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

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }

        }

       

        public Tuple<int, string> InsertaUnidadesMedidaDefinicion(List<UnidadesMedida> listaUnidadesMedida)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (UnidadesMedida unidadesMedida in listaUnidadesMedida)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateUnidadesMedidaDefinicion";

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (String.IsNullOrWhiteSpace(unidadesMedida.OldUomCode) == false)
                            {
                                cmd.Parameters.AddWithValue("@OldUomCode", unidadesMedida.OldUomCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OldUomCode", unidadesMedida.UomCode);

                            }

                            cmd.Parameters.AddWithValue("@UomCode", unidadesMedida.UomCode);
                            cmd.Parameters.AddWithValue("@UomName", unidadesMedida.UomName);                          
                            cmd.Parameters.AddWithValue("@UserSign", unidadesMedida.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", unidadesMedida.UpdateDate);
                            cmd.Parameters.AddWithValue("@Length", unidadesMedida.Length);
                            cmd.Parameters.AddWithValue("@Width", unidadesMedida.Width);
                            cmd.Parameters.AddWithValue("@Height", unidadesMedida.Height);
                            cmd.Parameters.AddWithValue("@Volume", unidadesMedida.Volume);
                            cmd.Parameters.AddWithValue("@Weight", unidadesMedida.Weight);
                            cmd.Parameters.AddWithValue("@VolUnit", unidadesMedida.VolUnit);

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
    }
}
