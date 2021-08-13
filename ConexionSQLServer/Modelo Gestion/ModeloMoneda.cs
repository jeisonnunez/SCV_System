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
    public class ModeloMoneda: ConexionSQLServer
    {
        public Tuple<int, string> InsertaMonedas(List<Monedas> listaMonedas)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Monedas monedas in listaMonedas)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateCurency";

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (String.IsNullOrWhiteSpace(monedas.OldCurrCode) == false)
                            {
                                cmd.Parameters.AddWithValue("@OldCurrCode", monedas.OldCurrCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OldCurrCode", monedas.CurrCode);
                                
                            }

                            cmd.Parameters.AddWithValue("@CurrCode", monedas.CurrCode);
                            cmd.Parameters.AddWithValue("@CurrName", monedas.CurrName);
                            cmd.Parameters.AddWithValue("@DocCurrCod", monedas.DocCurrCod);
                            cmd.Parameters.AddWithValue("@UserSign", monedas.UserSign);
                            cmd.Parameters.AddWithValue("@Locked", monedas.Locked);

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

        public Tuple<int, string> EliminaMoneda(string moneda)
        {
            int flag = 0;

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();

                SqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = "DeleteCurency";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CurrCode", moneda);

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

        public Tuple<DataTable, string> ConsultaMonedas()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT CurrCode, CurrName, DocCurrCod, Locked FROM OCRN";

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
