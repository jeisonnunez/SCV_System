using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Entidades;

namespace Datos
{
    public class ModeloSociedadActual : ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaSociedadActual()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT * FROM OADM";

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

        public Tuple<DataTable, string> ConsultaMonedas()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT CurrCode, CurrName FROM OCRN";

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

        public Tuple<int, string> ActualizaSociedad(List<Sociedad> listaSociedad)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Sociedad sociedad in listaSociedad)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateSociety";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@CompnyName", sociedad.CompnyName);
                            cmd.Parameters.AddWithValue("@CompnyAddr", sociedad.CompnyAddr);
                            cmd.Parameters.AddWithValue("@Country", sociedad.Country);
                            cmd.Parameters.AddWithValue("@PrintHeadr", sociedad.PrintHeadr);
                            cmd.Parameters.AddWithValue("@Phone1", sociedad.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", sociedad.Phone2);
                            cmd.Parameters.AddWithValue("@Fax", sociedad.Fax);
                            cmd.Parameters.AddWithValue("@E_Mail", sociedad.E_Mail);
                            cmd.Parameters.AddWithValue("@ZipCode", sociedad.ZipCode);
                            cmd.Parameters.AddWithValue("@MainCurncy", sociedad.MainCurncy);
                            cmd.Parameters.AddWithValue("@SysCurrncy", sociedad.SysCurncy);
                            cmd.Parameters.AddWithValue("@TaxIdNum", sociedad.TaxIdNum);
                            cmd.Parameters.AddWithValue("@RevOffice", sociedad.RevOffice);
                            cmd.Parameters.AddWithValue("@UpdateDate", sociedad.UpdateDate);
                            cmd.Parameters.AddWithValue("@UserSign", sociedad.UserSign);

                            flag = cmd.ExecuteNonQuery();
                           
                        }
                    }

                }

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
