using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloLogo:ConexionSQLServer
    {
    public Tuple<int, string> InsertLogo(string strName, byte[] imgByteArr)
    {
        string error = null;

        int flag = 0;

        try
        {
            using (Connection = new SqlConnection(connectionString))
            {

                Connection.Open();

                using (SqlCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = "InsertLogo";

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@imgByte", imgByteArr);

                    cmd.Parameters.AddWithValue("@Name", strName);

                    flag = cmd.ExecuteNonQuery();

                }


            }

            Connection.Close();

            return Tuple.Create(flag, error);

        }
        catch (Exception ex)
        {
            Connection.Close();

            return Tuple.Create(flag, ex.Message);
        }

    }

    public Tuple<DataTable, string> BindImage()
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
                    cmd.CommandText = "BindImage";

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
