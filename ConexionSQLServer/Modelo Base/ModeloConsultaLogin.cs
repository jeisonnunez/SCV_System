using Datos;
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
    public class ModeloConsultaLogin:ModeloInicioSesion
    {
        public Tuple<object, string> ConsultaLogin(List<Usuarios> listaUsuarios)
        {
            string error = null;

            string usuario="";

            string clave = "";

            string sociedad = "";

            try
            {             
                foreach (Usuarios usuarios in listaUsuarios)
                {
                    usuario = usuarios.User_code;

                    clave = usuarios.Password;

                    sociedad = usuarios.Sociedad;
                }

                MessageBox.Show("Prueba");

                Connection = new SqlConnection(connectionString);

                Connection.Open();

                transaction = Connection.BeginTransaction(); //inicia la transaccion      

                MessageBox.Show("Prueba1");

                using (SqlCommand cmd = new SqlCommand("LoginUser", Connection, transaction))
                {
                   
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@txtUsuario", usuario);

                    cmd.Parameters.AddWithValue("@txtContrasena", hc.PassHash(clave));

                    cmd.Parameters.AddWithValue("@txtSociedad", sociedad);

                    var result = cmd.ExecuteScalar();

                    transaction.Commit();

                    Connection.Close();

                    return Tuple.Create(result, error);

                }              
            }
            catch (Exception e)
            {

                transaction.Rollback();

                Connection.Close();              
              
                object result = null;

                

                return Tuple.Create(result, e.Message);
            }
        }

        public string CommitTransaction()
        {
            string error = null;

            try
            {
               
                transaction.Commit();

                Connection.Close();

                return error;

            }catch(Exception ex)
            {
                transaction.Rollback();

                Connection.Close();

                return ex.Message;
            }
        }

        public Tuple<string, string> GetNameSociety()
        {
            string sociedad=null;

            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                Connection.Open();

                transaction = Connection.BeginTransaction();

                using (SqlCommand cmde = new SqlCommand("GetSociety", Connection, transaction))
                {
                    //cmde = Connection.CreateCommand();

                    //cmde.Transaction = transaction;                  

                    cmde.CommandType = CommandType.StoredProcedure;

                    SqlDataReader reader = cmde.ExecuteReader();

                    if (reader.Read())
                    {
                        sociedad = reader["CompnyName"].ToString();
                    }

                    reader.Close();
                }

                transaction.Commit();

                Connection.Close();

                return Tuple.Create(sociedad, error);

            }
            catch (Exception e)
            {

                transaction.Rollback();

                Connection.Close();                

                return Tuple.Create(sociedad, e.Message);
            }
        }

        public Tuple<int, string> ObtenerIdUser(string username)
        {
            int usercode = 0;

            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                Connection.Open();

                transaction = Connection.BeginTransaction(); //inicia la transaccion   

                using (SqlCommand cmde = new SqlCommand("GetUserCode", Connection, transaction))
                {
                   
                    cmde.CommandType = CommandType.StoredProcedure;

                    cmde.Parameters.AddWithValue("@User", username);

                    SqlDataReader reader = cmde.ExecuteReader();

                    if (reader.Read())
                    {
                        usercode = Int32.Parse(reader["USERID"].ToString());
                    }

                    reader.Close();
                }

                

                //Connection.Close();

                return Tuple.Create(usercode, error);

            }
            catch (Exception e)
            {                
                Connection.Close();

                transaction.Rollback();

                return Tuple.Create(usercode, e.Message);

               
            }
        }
    }
}
