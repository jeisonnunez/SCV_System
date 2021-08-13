using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Datos.Modelo_Gestion
{
    public class ModeloDeterminacionCuentasMayor : ConexionSQLServer
    {
        public Tuple<List<DeterminacionCuentasMayor>, string> FindDeterminacionCuentasMayor()
        {
            List<DeterminacionCuentasMayor> newListaDeterminacion = new List<DeterminacionCuentasMayor>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindDeterminacionCuentasMayor";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DeterminacionCuentasMayor newDeterminacion = new DeterminacionCuentasMayor();

                            newDeterminacion.LinkAct_1 = reader["LinkAct_1"].ToString();
                            newDeterminacion.LinkAct_2 = reader["LinkAct_2"].ToString();
                            newDeterminacion.LinkAct_3 = reader["LinkAct_3"].ToString();
                            newDeterminacion.LinkAct_4 = reader["LinkAct_4"].ToString();
                            newDeterminacion.LinkAct_5 = reader["LinkAct_5"].ToString();
                            newDeterminacion.LinkAct_6 = reader["LinkAct_6"].ToString();
                            newDeterminacion.LinkAct_7 = reader["LinkAct_7"].ToString();


                            newListaDeterminacion.Add(newDeterminacion);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaDeterminacion, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListaDeterminacion, e.Message);

            }
        }

        public Tuple<int, string> UpdateDeterminacionCuentasMayor(List<DeterminacionCuentasMayor> listaDeterminacion)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DeterminacionCuentasMayor determinacionCuentas in listaDeterminacion)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateDeterminacionCuentasMayor";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@LinkAct_1", determinacionCuentas.LinkAct_1);
                            cmd.Parameters.AddWithValue("@LinkAct_2", determinacionCuentas.LinkAct_2);
                            cmd.Parameters.AddWithValue("@LinkAct_3", determinacionCuentas.LinkAct_3);
                            cmd.Parameters.AddWithValue("@LinkAct_4", determinacionCuentas.LinkAct_4);
                            cmd.Parameters.AddWithValue("@LinkAct_5", determinacionCuentas.LinkAct_5);
                            cmd.Parameters.AddWithValue("@LinkAct_6", determinacionCuentas.LinkAct_6);
                            cmd.Parameters.AddWithValue("@LinkAct_7", determinacionCuentas.LinkAct_7);

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
    }
}
