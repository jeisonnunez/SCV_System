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
    public class ModeloRegistro:ModeloLogo
    {
        public Tuple<string, string> obtenerBaseDatos()
        {
            string error = null;

            try
            {
                return Tuple.Create(BaseDatosActual,error);
            }
            catch(Exception e)
            {
                return Tuple.Create(BaseDatosActual,e.Message);
            }
            
        }

        public Tuple<int, string> InsertarUsuario(List<Usuarios> listaUsuarios)
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
                        using (SqlCommand cmd = new SqlCommand("AddUser", Connection, transaction))
                        {

                            //cmd.CommandText = "AddUser";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@User", usuarios.User_code);

                            cmd.Parameters.AddWithValue("@Password", hc.PassHash(usuarios.Password));

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

        public Tuple<int, string> InsertaDeterminacionCuentasMayor()
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

                        cmd.CommandText = "AddDeterminacionCuentaMayor";

                        cmd.CommandType = CommandType.StoredProcedure;

                        flag = cmd.ExecuteNonQuery();

                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaAlicuotas()
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

                        cmd.CommandText = "AddAlicuota";

                        cmd.CommandType = CommandType.StoredProcedure;

                        flag = cmd.ExecuteNonQuery();

                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {                
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaMeses()
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

                            cmd.CommandText = "AddMonth";

                            cmd.CommandType = CommandType.StoredProcedure;                         

                            flag = cmd.ExecuteNonQuery();

                        }
                    
                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaCodigosFiscales(List<CodigosFiscales> listaCodigosFiscales)
        {
            int flag = 0;

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

                            cmd.CommandText = "AddCodigosFiscales";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserSign", codigosFiscales.UserSign);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaClasesImpuestos(List<ClasesImpuestos> listaClasesImpuestos)
        {
            int flag = 0;

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

                            cmd.CommandText = "AddClasesImpuestos";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserSign", clasesImpuestos.UserSign);

                            flag = cmd.ExecuteNonQuery();

                        }
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

        public Tuple<int, string> InsertaRetencionImpuesto(List<RetencionImpuesto> listaRetencion)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    foreach (RetencionImpuesto retenciones in listaRetencion)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {

                            cmd.CommandText = "AddRetencionImpuesto";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserSign", retenciones.UserSign);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaMonedasBasicas(List<Monedas> listaMonedas)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    foreach (Monedas cuentas in listaMonedas)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {

                            cmd.CommandText = "AddCurencyBasic";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@UserSign", cuentas.UserSign);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {                
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> InsertaCuentasPrimerNivel(List<Cuenta> listaCuentas)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    foreach (Cuenta cuentas in listaCuentas)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {

                            cmd.CommandText = "AddAccountFirstLevels";

                            cmd.CommandType = CommandType.StoredProcedure;
                          
                            cmd.Parameters.AddWithValue("@UserSign", cuentas.UserSign);                          
                           
                            flag = cmd.ExecuteNonQuery();
                      
                        }
                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {               
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }

        }

        public Tuple<int, string> DatosSociedad(List<Sociedad> listaSociedad)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    foreach (Sociedad sociedad in listaSociedad)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {

                            cmd.CommandText = "SocietyDates";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Society", sociedad.CompnyName);

                            cmd.Parameters.AddWithValue("@UpdateDate", sociedad.UpdateDate);

                            cmd.Parameters.AddWithValue("@User", sociedad.UserSign);

                            cmd.Parameters.AddWithValue("@MainCurncy", sociedad.MainCurncy);

                            cmd.Parameters.AddWithValue("@SysCurncy", sociedad.SysCurncy);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                    Connection.Close();

                    return Tuple.Create(flag, error);
                }
            }
            catch (Exception e)
            {                
                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }
        }
    }
}
