using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloReportes:ConexionSQLServer
    {
        public Tuple<DataTable, string> FindAllAccount()
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
                        cmd.CommandText = "FindAllAccount";

                        cmd.CommandType = CommandType.StoredProcedure;

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

        public Tuple<DataTable, string> FindAllAccountReal()
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
                        cmd.CommandText = "FindAllAccountReal";

                        cmd.CommandType = CommandType.StoredProcedure;

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

        public Tuple<DataTable, string> FindAllItems()
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
                        cmd.CommandText = "FindAllItems";

                        cmd.CommandType = CommandType.StoredProcedure;

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
    }
}
