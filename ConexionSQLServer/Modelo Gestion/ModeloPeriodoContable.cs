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
    public class ModeloPeriodoContable: ConexionSQLServer
    {

        public Tuple<int, string> ActualizaPeriodo(List<PeriodosContables> listaSubPeriodos)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (PeriodosContables subperiodos in listaSubPeriodos)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateSubPeriods";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@OldCode", subperiodos.OldCode1);
                            cmd.Parameters.AddWithValue("@Code", subperiodos.Code);
                            cmd.Parameters.AddWithValue("@Name", subperiodos.Name);
                            cmd.Parameters.AddWithValue("@F_RefDate", subperiodos.F_RefDate);
                            cmd.Parameters.AddWithValue("@T_RefDate", subperiodos.T_RefDate);
                            cmd.Parameters.AddWithValue("@F_DueDate", subperiodos.F_DueDate);
                            cmd.Parameters.AddWithValue("@T_DueDate", subperiodos.T_DueDate);
                            cmd.Parameters.AddWithValue("@F_TaxDate", subperiodos.F_TaxDate);
                            cmd.Parameters.AddWithValue("@T_TaxDate", subperiodos.T_TaxDate);
                            cmd.Parameters.AddWithValue("@PeriodStat", subperiodos.PeriodStat);
                            cmd.Parameters.AddWithValue("@UserSign2", subperiodos.UserSign2);
                            cmd.Parameters.AddWithValue("@UpdateDate", subperiodos.UpdateDate);

                            flag = cmd.ExecuteNonQuery();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {              

                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }

        }

        public Tuple<DateTime?, string> ConsultaPeriodoActual(string category)
        {
            DateTime? financYear = null;

            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                Connection.Open();

                SqlCommand cmde = new SqlCommand(null, Connection);

                cmde = Connection.CreateCommand();

                cmde.CommandText = "GetFinancYear";

                cmde.CommandType = CommandType.StoredProcedure;

                cmde.Parameters.AddWithValue("@Category", category);

                SqlDataReader reader = cmde.ExecuteReader();

                if (reader.Read())
                {
                    financYear = Convert.ToDateTime(reader["FinancYear"].ToString());
                }

                Connection.Close();

                return Tuple.Create(financYear, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(financYear, e.Message);
            }
        }
    }
}
