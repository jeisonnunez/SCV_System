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
    public class ModeloUsuario : ConexionSQLServer
    {
        public Tuple<List<Usuarios>, string> ConsultaUsuarios(List<Usuarios> listaUsuarios)
        {
            List<Usuarios> newListaUsuarios = new List<Usuarios>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Usuarios usuarios in listaUsuarios)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindUsuarios";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@User_code", usuarios.User_code);
                            cmd.Parameters.AddWithValue("@User_name", usuarios.User_name);
                            cmd.Parameters.AddWithValue("@Locked", usuarios.Locked);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                Usuarios newUsuarios = new Usuarios();

                                
                                newUsuarios.UserID = Convert.ToInt32(reader["USERID"]);
                                newUsuarios.User_code = reader["USER_CODE"].ToString();
                                newUsuarios.User_name = reader["U_NAME"].ToString();            
                                newUsuarios.Locked = Convert.ToChar(reader["Locked"].ToString());                                

                                newListaUsuarios.Add(newUsuarios);


                            }

                            reader.Close();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaUsuarios,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaUsuarios, e.Message);
            }


        }

        public Tuple<int, string> DeleteUser(string userCode)
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

                        cmd.CommandText = "DeleteUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@USER_CODE", userCode);                     

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

        public Tuple<int, string> UpdatePassword(string password, int userID)
        {
            int flag = 0;

            string error = null;
           
            using (Connection = new SqlConnection(connectionString))
            {

                try
                {
                    Connection.Open();

                    transaction = Connection.BeginTransaction(); //inicia la transaccion 

                    using (SqlCommand cmd = new SqlCommand("UpdatePasswordUser", Connection, transaction))
                    {

                        //cmd.CommandText = "UpdatePasswordUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserID", userID);

                        cmd.Parameters.AddWithValue("@Password", hc.PassHash(password));

                        flag = cmd.ExecuteNonQuery();

                    }

                    transaction.Commit();
                    
                    Connection.Close();

                    return Tuple.Create(flag,error);
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    Connection.Close();

                    return Tuple.Create(flag, e.Message);
                }
            }
            
        }

        public Tuple<int, string> UpdateUser(List<Usuarios> listaUsuarios)
        {
            int flag = 0;

            string error = null;

            using (Connection = new SqlConnection(connectionString))
            {
                Connection.Open();

                transaction = Connection.BeginTransaction(); //inicia la transaccion 

                try
                {

                    foreach (Usuarios usuarios in listaUsuarios)
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateUser", Connection, transaction))
                        {

                            //cmd.CommandText = "UpdateUser";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserID", usuarios.UserID);

                            cmd.Parameters.AddWithValue("@User", usuarios.User_code);

                            cmd.Parameters.AddWithValue("@FullName", usuarios.User_name);

                            cmd.Parameters.AddWithValue("@Locked", usuarios.Locked);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                    transaction.Commit();

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }

                catch (Exception e)
                {
                    transaction.Rollback();

                    Connection.Close();

                    return Tuple.Create(flag, e.Message);
                }
            }
        }
        
        public Tuple<List<Usuarios>, string> FindFirst()
        {
            List<Usuarios> newListaUsuarios = new List<Usuarios>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuarios newUsuario = new Usuarios();

                            newUsuario.UserID = Convert.ToInt32(reader["USERID"]);
                            newUsuario.User_code = reader["USER_CODE"].ToString();
                            newUsuario.User_name = reader["U_NAME"].ToString();                           
                            newUsuario.Locked = Convert.ToChar(reader["Locked"].ToString());
                            

                            newListaUsuarios.Add(newUsuario);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaUsuarios,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaUsuarios, e.Message);
            }
        }

        public Tuple<List<Usuarios>, string> FindPrevious(int userid)
        {
            List<Usuarios> newListaUsuarios = new List<Usuarios>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@USERID", userid);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuarios newUsuario = new Usuarios();

                            newUsuario.UserID = Convert.ToInt32(reader["USERID"]);
                            newUsuario.User_code = reader["USER_CODE"].ToString();
                            newUsuario.User_name = reader["U_NAME"].ToString();
                            newUsuario.Locked = Convert.ToChar(reader["Locked"].ToString());
                           
                            newListaUsuarios.Add(newUsuario);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaUsuarios,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaUsuarios,e.Message);
            }
        }

        public Tuple<List<Usuarios>, string> FindLast()
        {
            List<Usuarios> newListaUsuarios = new List<Usuarios>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuarios newUsuario = new Usuarios();

                            newUsuario.UserID = Convert.ToInt32(reader["USERID"]);
                            newUsuario.User_code = reader["USER_CODE"].ToString();
                            newUsuario.User_name = reader["U_NAME"].ToString();
                            newUsuario.Locked = Convert.ToChar(reader["Locked"].ToString());
                          

                            newListaUsuarios.Add(newUsuario);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaUsuarios,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(newListaUsuarios, e.Message);
            }
        }

        public Tuple<List<Usuarios>, string> FindNext(int userid)
        {
            List<Usuarios> newListaUsuarios = new List<Usuarios>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextUser";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@USERID", userid);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Usuarios newUsuario = new Usuarios();

                            newUsuario.UserID = Convert.ToInt32(reader["USERID"]);
                            newUsuario.User_code = reader["USER_CODE"].ToString();
                            newUsuario.User_name = reader["U_NAME"].ToString();
                            newUsuario.Locked = Convert.ToChar(reader["Locked"].ToString());
                           

                            newListaUsuarios.Add(newUsuario);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaUsuarios,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaUsuarios, e.Message);
            }
        }
    }
}
