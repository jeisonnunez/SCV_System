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
    public class ModeloClasesImpuesto : ConexionSQLServer
    {
        public Tuple<int, string> InsertaClasesImpuestos(List<ClasesImpuestos> listaClasesImpuestos)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ClasesImpuestos clasesImpuestos in listaClasesImpuestos)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateClasesImpuestos";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", clasesImpuestos.Code);
                            cmd.Parameters.AddWithValue("@Name", clasesImpuestos.Name);
                            cmd.Parameters.AddWithValue("@Rate", clasesImpuestos.Rate);
                            cmd.Parameters.AddWithValue("@SalesTax", clasesImpuestos.SalesTax);
                            cmd.Parameters.AddWithValue("@PurchTax", clasesImpuestos.PurchTax);
                            cmd.Parameters.AddWithValue("@UserSign", clasesImpuestos.UserSign);

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

                return Tuple.Create(i,e.Message);
            }

        }

        public List<Cuenta> ConsultaCuentasNoAsociadas()
        {
            List<Cuenta> newListaCuenta = new List<Cuenta>();

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindAccountNoAsociada";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Cuenta newCuentaAsociada = new Cuenta();

                            newCuentaAsociada.AcctCode = reader["AcctCode"].ToString();
                            newCuentaAsociada.AcctName = reader["AcctName"].ToString();


                            newListaCuenta.Add(newCuentaAsociada);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return newListaCuenta;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Account", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return newListaCuenta;

            }
        }

        public Tuple<int, string> EliminaClasesImpuesto(string claseImpuesto)
        {
            int flag = 0;

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();

                SqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = "DeleteClaseImpuesto";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Code", claseImpuesto);

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

        public Tuple<DataTable, string> ConsultaClasesImpuestos()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT Code, Name, Rate, SalesTax, UserSign, PurchTax FROM OSTA";

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
