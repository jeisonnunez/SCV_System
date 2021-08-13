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
    public class ModeloRetencionImpuesto: ConexionSQLServer
    {
        public Tuple<DataTable, string> ConsultaRetencion()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT WTCode, WTName, Rate, EffecDate, Category, BaseType, PrctBsAmnt, Account, Inactive, U_IDA_TipoRetencion, U_IDA_BaseMinima,  U_IDA_Sustraendo, Offclcode FROM OWHT";

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

        public Tuple<int, string> InsertaRetenciones(List<RetencionImpuesto> listaRetencion)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (RetencionImpuesto retencionImpuesto in listaRetencion)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddUpdateTaxHolding";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@OldWtCode", retencionImpuesto.OldWtCode);
                            cmd.Parameters.AddWithValue("@Wt_Code", retencionImpuesto.Wt_Code);
                            cmd.Parameters.AddWithValue("@Wt_Name", retencionImpuesto.Wt_Name);
                            cmd.Parameters.AddWithValue("@Rate", retencionImpuesto.Rate);
                            cmd.Parameters.AddWithValue("@EffecDate", retencionImpuesto.EffecDate);
                            cmd.Parameters.AddWithValue("@Category", retencionImpuesto.Category);
                            cmd.Parameters.AddWithValue("@BaseType", retencionImpuesto.BaseType);
                            cmd.Parameters.AddWithValue("@PrctBsAmnt", retencionImpuesto.PrctBsAmnt);
                            cmd.Parameters.AddWithValue("@Account", retencionImpuesto.Account);
                            cmd.Parameters.AddWithValue("@OffclCode", retencionImpuesto.OffclCode);
                            cmd.Parameters.AddWithValue("@UserSign", retencionImpuesto.UserSign);
                            cmd.Parameters.AddWithValue("@Inactive", retencionImpuesto.Inactive);
                            cmd.Parameters.AddWithValue("@U_IDA_TipoRetencion", retencionImpuesto.TipoRetencion);
                            cmd.Parameters.AddWithValue("@U_IDA_BaseMinima", retencionImpuesto.BaseMinima);
                            cmd.Parameters.AddWithValue("@U_IDA_Sustraendo", retencionImpuesto.Sustraendo);


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

        public Tuple<int, string> EliminaRetencion(string retencionImpuesto)
        {
            int flag = 0;

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();

                SqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = "DeleteTaxHolding";

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@WTCode", retencionImpuesto);

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

      
    }
}
