using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Datos.ModeloInformesFiscales
{
    public class ModeloAnularComprobantesIVA:ConexionSQLServer
    {
        public Tuple<int,string, string> ExecuteAnularComprobantesIVA(string desde, string hasta)
        {
            int flag = 0;

            string error = null;

            string message = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "IDA_AnularComprobanteIVA";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Desde", desde);
                        cmd.Parameters.AddWithValue("@Hasta", hasta);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            message = reader["Message"].ToString();
                            
                        }



                    }


                }

                Connection.Close();

                return Tuple.Create(flag, error,message);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(flag, e.Message,message);
            }
        }
    }
}
