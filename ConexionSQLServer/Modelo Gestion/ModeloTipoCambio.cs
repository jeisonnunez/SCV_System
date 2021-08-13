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
    public class ModeloTipoCambio: ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaTipoCambio()
        {
            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                string query = "SELECT CurrCode FROM OCRN T0 INNER JOIN OADM T1 ON T1.MainCurncy<>T0.CurrCode";

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

                return Tuple.Create(tabla, e.Message);
            }

        }

        public Tuple<DataTable, string> ConsultaYears()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT DISTINCT Category FROM OFPR";

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

        public Tuple<DataTable, string> ConsultaTiposCambiosDefinidos()
        {
            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                string query = "SELECT RateDate, Currency, Rate FROM ORTT";

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

                return Tuple.Create(tabla,e.Message);
            }

        }

        public Tuple<DataTable, string> ConsultaMeses()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT Code, Month FROM MONTHS";

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

        public Tuple<int, string> InsertaTipoCambio(List<TipoCambio> listaTipoCambio)
        {
            int flag = 0;

            int i = 0;

            string error = null;
                     
                using (Connection = new SqlConnection(connectionString))
                {

                Connection.Open();

                transaction = Connection.BeginTransaction(); //inicia la transaccion   

                try
                {

                    foreach (TipoCambio tiposCambio in listaTipoCambio)
                    {

                        using (SqlCommand cmd = new SqlCommand("AddUpdateTipoCambio", Connection, transaction))
                    {
                        
                            //cmd.CommandText = "AddUpdateTipoCambio";

                            cmd.CommandType = CommandType.StoredProcedure;

                            //cmd.Transaction = sqlTransaction;

                            cmd.Parameters.AddWithValue("@RateDate", tiposCambio.RateDate);
                            cmd.Parameters.AddWithValue("@Currency", tiposCambio.Currency);
                            cmd.Parameters.AddWithValue("@Rate", tiposCambio.Rate);
                            cmd.Parameters.AddWithValue("@UserSign", tiposCambio.UserSign);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                    transaction.Commit();                  

                    Connection.Close();                  

                    return Tuple.Create(i, error);

                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    Connection.Close();

                    return Tuple.Create(i, e.Message);
                }

            }

               

            
            
        }

        public Tuple<bool, string> FindUSDColumn()
        {
            bool sw = false;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindUSDColumn";

                        cmd.CommandType = CommandType.StoredProcedure;                        

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            sw = true;
                        }
                        else
                        {
                            sw = false;
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(sw, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(sw, e.Message);
            }
        }

        public Tuple<decimal, string> FindRateUSD(DateTime datetimePage)
        {
            decimal rate = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindRateUSD";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RateDate", datetimePage);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            rate = Convert.ToDecimal(reader["Rate"]);
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(rate, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(rate, e.Message);
            }
        }
    }
}
