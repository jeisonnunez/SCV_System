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
    public class ModeloTablaRetencionImpuesto : ConexionSQLServer
    {
        public Tuple<List<RetencionImpuesto>, string> FindHoldingTax()
        {
            List<RetencionImpuesto> newListaRetencionImpuesto = new List<RetencionImpuesto>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindHoldingTax";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            RetencionImpuesto newRetencionImpuesto = new RetencionImpuesto();

                            newRetencionImpuesto.Wt_Code = reader["WTCode"].ToString();
                            newRetencionImpuesto.Wt_Name = reader["WTName"].ToString();
                            newRetencionImpuesto.Rate = Convert.ToDecimal(reader["Rate"]);
                            newRetencionImpuesto.EffecDate = Convert.ToDateTime(reader["EffecDate"]);
                            newRetencionImpuesto.Category = Convert.ToChar(reader["Category"]);
                            newRetencionImpuesto.BaseType = Convert.ToChar(reader["BaseType"]);
                            newRetencionImpuesto.PrctBsAmnt =Convert.ToDecimal(reader["PrctBsAmnt"]);
                            newRetencionImpuesto.Account = reader["Account"].ToString();

                            newListaRetencionImpuesto.Add(newRetencionImpuesto);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaRetencionImpuesto,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaRetencionImpuesto, e.Message);

            }
        }
    }
}
