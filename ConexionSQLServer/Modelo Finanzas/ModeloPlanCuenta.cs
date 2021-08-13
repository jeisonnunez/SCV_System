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
    public class ModeloPlanCuenta:ConexionSQLServer
    {
        
        public Tuple<DataSet, string> FindAccounts(int i)
        {
            DataSet dts = new DataSet();

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                string query = "SELECT AcctCode, AcctName, FatherNum FROM OACT WHERE GroupMask=" + i;

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);               

                data.Fill(dts, "OACT");

                Connection.Close();

                return Tuple.Create(dts,error);
            }

            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(dts, e.Message);
            }

        }

        public Tuple<DataSet, string> FindAccounts()
        {
            DataSet dts = new DataSet();

            string error = null;

            try
            {
                Connection = new SqlConnection(connectionString);

                string query = "SELECT AcctCode, AcctName, FatherNum FROM OACT";

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);

                data.Fill(dts, "OACT");

                Connection.Close();

                return Tuple.Create(dts,error);
            }

            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(dts, e.Message);
            }

        }

        public Tuple<DataTable, string> ConsultaMonedas()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT CurrCode, CurrName FROM OCRN";

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

        public Tuple<DataTable, string> FindCuenta(string account)
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
                        cmd.CommandText = "FindAccount";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", account);

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

        public Tuple<string, string> FindFatherNum(string account)
        {
            string fatherNum="";

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindFatherNum";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", account);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            
                            fatherNum = reader["FatherNum"].ToString();
                           
                        }

                        reader.Close();

                    }


                }

                Connection.Close();

                return Tuple.Create(fatherNum,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(fatherNum, e.Message);
            }
        }

        public Tuple<int, string> UpdateAccount(List<Cuenta> listaCuenta)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Cuenta cuenta in listaCuenta)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateAccount";

                            cmd.CommandType = CommandType.StoredProcedure;

                            if (String.IsNullOrWhiteSpace(cuenta.OldAcctCode) == false)
                            {
                                cmd.Parameters.AddWithValue("@OldAcctCode", cuenta.OldAcctCode);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@OldAcctCode", cuenta.AcctCode);

                            }

                            cmd.Parameters.AddWithValue("@AcctCode", cuenta.AcctCode);
                            cmd.Parameters.AddWithValue("@AcctName", cuenta.AcctName);
                            cmd.Parameters.AddWithValue("@ActCurr",cuenta.ActCurr);
                            cmd.Parameters.AddWithValue("@LocManTran", cuenta.LocManTran);
                            cmd.Parameters.AddWithValue("@UserSign", cuenta.UserSign);
                            cmd.Parameters.AddWithValue("@ActType", cuenta.ActType);
                            cmd.Parameters.AddWithValue("@CreateDate", cuenta.CreateDate);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(flag,e.Message);
            }
        }

        public Tuple<int, string> DeleteAccount(string account)
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
                        cmd.CommandText = "DeleteAccount";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", account);
                            
                        flag = cmd.ExecuteNonQuery();

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

        public Tuple<int, string> UpdateAccountTratar(List<Cuenta> listaCuenta)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Cuenta cuenta in listaCuenta)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateAccountTratar";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@AcctCode", cuenta.AcctCode);
                            cmd.Parameters.AddWithValue("@AcctName", cuenta.AcctName);
                            cmd.Parameters.AddWithValue("@Postable", cuenta.Postable);
                            cmd.Parameters.AddWithValue("@UserSign", cuenta.UserSign);
                            cmd.Parameters.AddWithValue("@FatherNum", cuenta.FatherNum);
                            cmd.Parameters.AddWithValue("@CreateDate", cuenta.CreateDate);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(flag,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(flag,e.Message);
            }
        }

        public Tuple<int, string> InsertAccount(List<Cuenta> listaCuenta)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Cuenta cuenta in listaCuenta)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertAccount";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@AcctCode", cuenta.AcctCode);
                            cmd.Parameters.AddWithValue("@AcctName", cuenta.AcctName);
                            cmd.Parameters.AddWithValue("@ActCurr", cuenta.ActCurr);
                            cmd.Parameters.AddWithValue("@LocManTran", cuenta.LocManTran);
                            cmd.Parameters.AddWithValue("@UserSign", cuenta.UserSign);
                            cmd.Parameters.AddWithValue("@ActType", cuenta.ActType);
                            cmd.Parameters.AddWithValue("@CreateDate", cuenta.CreateDate);
                            cmd.Parameters.AddWithValue("@Postable", cuenta.Postable);
                            cmd.Parameters.AddWithValue("@FatherNum", cuenta.FatherNum);
                            cmd.Parameters.AddWithValue("@CurrTotal", cuenta.CurrTotal);
                            cmd.Parameters.AddWithValue("@Finanse", cuenta.Finanse);
                            cmd.Parameters.AddWithValue("@Levels", cuenta.Levels);
                            cmd.Parameters.AddWithValue("@SysTotal", cuenta.SysTotal);
                            cmd.Parameters.AddWithValue("@FcTotal", cuenta.FcTotal);
                            cmd.Parameters.AddWithValue("@Advance", cuenta.Advance);
                            cmd.Parameters.AddWithValue("@GroupMask", cuenta.GroupMask);

                            flag = cmd.ExecuteNonQuery();

                        }
                    }

                }

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
