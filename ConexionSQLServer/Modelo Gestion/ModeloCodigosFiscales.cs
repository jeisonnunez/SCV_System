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
    public class ModeloCodigosFiscales: ConexionSQLServer
    {
        public Tuple<DataTable,string> ConsultaAlicuota()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT Code, Name FROM Alicuotas";

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

                return Tuple.Create(tabla,e.Message);
            }

        }

        public Tuple<List<CodigosFiscales>, string> ConsultaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            List<CodigosFiscales> newListaCosigosFiscales = new List<CodigosFiscales>();

            string error = null;
          
            try
            {               

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (CodigosFiscales codigosFiscales in listaCodigosFiscales)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindCodigosFiscales";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", codigosFiscales.Code);
                            cmd.Parameters.AddWithValue("@Name", codigosFiscales.Name);
                            cmd.Parameters.AddWithValue("@U_IDA_Alicuota", codigosFiscales.U_IDA_Alicuota);
                            cmd.Parameters.AddWithValue("@ValidForAP", codigosFiscales.ValidForAP);
                            cmd.Parameters.AddWithValue("@ValidForAR", codigosFiscales.ValidForAR);
                            cmd.Parameters.AddWithValue("@Lock", codigosFiscales.Lock1);
                            

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                                newCodigosFiscales.Code= reader["Code"].ToString();
                                newCodigosFiscales.Name = reader["Name"].ToString();
                                newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                                newCodigosFiscales.ValidForAP= Convert.ToChar(reader["ValidForAP"].ToString());
                                newCodigosFiscales.ValidForAR = Convert.ToChar(reader["ValidForAR"].ToString());
                                newCodigosFiscales.Lock1 = Convert.ToChar(reader["Lock"].ToString());
                                newCodigosFiscales.U_IDA_Alicuota = reader["U_IDA_Alicuota"].ToString();

                                newListaCosigosFiscales.Add(newCodigosFiscales);

                                
                            }
                            
                            reader.Close();
                            
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales, e.Message);
            }

          

        }

        public Tuple<List<CodigosFiscales>, string> FindFirst()
        {
            List<CodigosFiscales> newListaCosigosFiscales = new List<CodigosFiscales>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstCodigoFiscal";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                            newCodigosFiscales.Code = reader["Code"].ToString();
                            newCodigosFiscales.Name = reader["Name"].ToString();
                            newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                            newCodigosFiscales.ValidForAP = Convert.ToChar(reader["ValidForAP"].ToString());
                            newCodigosFiscales.ValidForAR = Convert.ToChar(reader["ValidForAR"].ToString());
                            newCodigosFiscales.Lock1 = Convert.ToChar(reader["Lock"].ToString());
                            newCodigosFiscales.U_IDA_Alicuota = reader["U_IDA_Alicuota"].ToString();

                            newListaCosigosFiscales.Add(newCodigosFiscales);


                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales, e.Message);
            }
        }

        public Tuple<List<ClasesImpuestos>, string> ConsultaClasesImpuestos()
        {
            List<ClasesImpuestos> newListaClasesImpuestos = new List<ClasesImpuestos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindClasesImpuestos";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ClasesImpuestos newClasesImpuestos = new ClasesImpuestos();

                            newClasesImpuestos.Code = reader["Code"].ToString();
                            newClasesImpuestos.Rate = Convert.ToDecimal(reader["Rate"]);
                            newClasesImpuestos.Name = reader["Name"].ToString();
                            newClasesImpuestos.PurchTax = reader["PurchTax"].ToString();
                            newClasesImpuestos.SalesTax = reader["SalesTax"].ToString();

                            newListaClasesImpuestos.Add(newClasesImpuestos);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaClasesImpuestos, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaClasesImpuestos, e.Message);

            }
        }

        public Tuple<int, string> InsertCodigosFiscalesLines(List<CodigosFiscalesLine> listaCodigosFiscalesLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (CodigosFiscalesLine codigosFiscalesLine in listaCodigosFiscalesLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertCodigosFiscalesLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@STCCode", codigosFiscalesLine.STCCode);
                            cmd.Parameters.AddWithValue("@LineId", codigosFiscalesLine.Line_ID);
                            cmd.Parameters.AddWithValue("@STACode", codigosFiscalesLine.STACode);
                            cmd.Parameters.AddWithValue("@EfctivRate", codigosFiscalesLine.EfctivRate);

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

        public Tuple<int, string> UpdateCodigosFiscalesLines(List<CodigosFiscalesLine> listaCodigosFiscalesLines)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (CodigosFiscalesLine codigosFiscalesLine in listaCodigosFiscalesLines)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateCodigosFiscalesLines";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@STCCode", codigosFiscalesLine.STCCode);
                            cmd.Parameters.AddWithValue("@LineId", codigosFiscalesLine.Line_ID);
                            cmd.Parameters.AddWithValue("@STACode", codigosFiscalesLine.STACode);
                            cmd.Parameters.AddWithValue("@EfctivRate", codigosFiscalesLine.EfctivRate);
                           
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

        public Tuple<DataTable, string> FindCodigosFiscalesLines(string code)
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
                        cmd.CommandText = "FindCodigosFiscalesLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Code", code);

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

        public Tuple<int, string> DeleteCodigoFiscal(string codigoFiscal)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {

                        cmd.CommandText = "DeleteCodigoFiscal";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Code", codigoFiscal);

                        flag = cmd.ExecuteNonQuery();

                    }

                    Connection.Close();

                    return Tuple.Create(flag,error);
                }
            }
            catch (Exception e)
            {
             
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaCodigoFiscal(List<CodigosFiscales> listaCodigosFiscales)
        {
            int flag = 0;

            string error = null;
           
            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (CodigosFiscales codigoFiscal in listaCodigosFiscales)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddCodigoFiscal";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", codigoFiscal.Code);
                            cmd.Parameters.AddWithValue("@Name", codigoFiscal.Name);
                            cmd.Parameters.AddWithValue("@Rate", codigoFiscal.Rate);
                            cmd.Parameters.AddWithValue("@U_IDA_Alicuota", codigoFiscal.U_IDA_Alicuota);
                            cmd.Parameters.AddWithValue("@ValidForAP", codigoFiscal.ValidForAP);
                            cmd.Parameters.AddWithValue("@ValidForAR", codigoFiscal.ValidForAR);
                            cmd.Parameters.AddWithValue("@Lock", codigoFiscal.Lock1);
                            cmd.Parameters.AddWithValue("@UpdateDate", codigoFiscal.UpdateDate);
                            cmd.Parameters.AddWithValue("@UserSign", codigoFiscal.UserSign);
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

        public Tuple<int, string> ActualizaCodigoFiscal(List<CodigosFiscales> listaCodigosFiscales)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (CodigosFiscales codigoFiscal in listaCodigosFiscales)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateCodigoFiscal";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", codigoFiscal.Code);
                            cmd.Parameters.AddWithValue("@Name", codigoFiscal.Name);
                            cmd.Parameters.AddWithValue("@Rate", codigoFiscal.Rate);
                            cmd.Parameters.AddWithValue("@U_IDA_Alicuota", codigoFiscal.U_IDA_Alicuota);
                            cmd.Parameters.AddWithValue("@ValidForAP", codigoFiscal.ValidForAP);
                            cmd.Parameters.AddWithValue("@ValidForAR", codigoFiscal.ValidForAR);
                            cmd.Parameters.AddWithValue("@Lock", codigoFiscal.Lock1);
                            cmd.Parameters.AddWithValue("@UpdateDate", codigoFiscal.UpdateDate);
                            cmd.Parameters.AddWithValue("@UserSign", codigoFiscal.UserSign);

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

        public Tuple<List<CodigosFiscales>, string> FindLast()
        {
            List<CodigosFiscales> newListaCosigosFiscales = new List<CodigosFiscales>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastCodigoFiscal";

                        cmd.CommandType = CommandType.StoredProcedure;
                       
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                            newCodigosFiscales.Code = reader["Code"].ToString();
                            newCodigosFiscales.Name = reader["Name"].ToString();
                            newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                            newCodigosFiscales.ValidForAP = Convert.ToChar(reader["ValidForAP"].ToString());
                            newCodigosFiscales.ValidForAR = Convert.ToChar(reader["ValidForAR"].ToString());
                            newCodigosFiscales.Lock1 = Convert.ToChar(reader["Lock"].ToString());
                            newCodigosFiscales.U_IDA_Alicuota = reader["U_IDA_Alicuota"].ToString();

                            newListaCosigosFiscales.Add(newCodigosFiscales);


                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales, e.Message);
            }
        }

        public Tuple<List<CodigosFiscales>, string> FindPrevious(string codigo)
        {
            List<CodigosFiscales> newListaCosigosFiscales = new List<CodigosFiscales>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousCodigoFiscal";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Code", codigo);


                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                            newCodigosFiscales.Code = reader["Code"].ToString();
                            newCodigosFiscales.Name = reader["Name"].ToString();
                            newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                            newCodigosFiscales.ValidForAP = Convert.ToChar(reader["ValidForAP"].ToString());
                            newCodigosFiscales.ValidForAR = Convert.ToChar(reader["ValidForAR"].ToString());
                            newCodigosFiscales.Lock1 = Convert.ToChar(reader["Lock"].ToString());
                            newCodigosFiscales.U_IDA_Alicuota = reader["U_IDA_Alicuota"].ToString();

                            newListaCosigosFiscales.Add(newCodigosFiscales);


                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales, e.Message);
            }
        }
        public Tuple<List<CodigosFiscales>, string> FindNext(string codigo)
        {
            List<CodigosFiscales> newListaCosigosFiscales = new List<CodigosFiscales>();

            string error = null;

            try
            {
                
                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();
                   
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "NextCodigoFiscal";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", codigo);
                          

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                                newCodigosFiscales.Code = reader["Code"].ToString();
                                newCodigosFiscales.Name = reader["Name"].ToString();
                                newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"].ToString());
                                newCodigosFiscales.ValidForAP = Convert.ToChar(reader["ValidForAP"].ToString());
                                newCodigosFiscales.ValidForAR = Convert.ToChar(reader["ValidForAR"].ToString());
                                newCodigosFiscales.Lock1 = Convert.ToChar(reader["Lock"].ToString());
                                newCodigosFiscales.U_IDA_Alicuota = reader["U_IDA_Alicuota"].ToString();

                                newListaCosigosFiscales.Add(newCodigosFiscales);


                            }

                            reader.Close();

                        
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaCosigosFiscales, e.Message);
            }
        }
    }
}
