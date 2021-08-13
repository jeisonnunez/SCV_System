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
    public class ModeloGenerarRetencionesIVA : ConexionSQLServer
    {
        public Tuple<int, string,string> ExecuteGenerarRetencionesIVA(string txtProveedor, string txtProveedor1, string year, string month, string quincena, string tipo)
        {
            int result = 0;

            string error = null;

            string message = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();
                   
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "GenerarComprobanteIVA";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Card1", txtProveedor);
                            cmd.Parameters.AddWithValue("@Card2", txtProveedor1);
                            cmd.Parameters.AddWithValue("@Ano", year);
                            cmd.Parameters.AddWithValue("@Mes", month);
                            cmd.Parameters.AddWithValue("@Quincena", quincena);
                            cmd.Parameters.AddWithValue("@Tipo", tipo);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                message = reader["Message"].ToString();
                                result = Convert.ToInt32(reader["Result"]);
                            }


                        }
                    

                }

                Connection.Close();

                return Tuple.Create(result, error,message);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(result, e.Message,message);
            }
        }
    }
}
