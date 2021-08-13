using Entidades;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloEmpresa: ModeloInicioSesion
    {
        public Tuple<int, string> CreateSociedad(string empresa)
        {
            string error = null;

            int flag = 0;

            try
            {
                Connection = new SqlConnection(connectionString);

                Connection.Open();

                //string script = Script.Replace("SCV_SAPB1", "SCV_" + empresa);

                var lines = File.ReadAllLines(@"c:\Users\jamara\Desktop\BBDD.sql");

                lines[34] = "CREATE DATABASE SCV_" + empresa + "";

                lines[36] = "USE SCV_" + empresa + "";

                File.WriteAllLines(@"C:\Users\jamara\Desktop\BBDD.sql", lines);

                FileInfo file = new FileInfo(@"C:\Users\jamara\Desktop\BBDD.sql");

                string script = file.OpenText().ReadToEnd();

                Server server = new Server(new ServerConnection(Connection));

                flag = server.ConnectionContext.ExecuteNonQuery(script);

                file.OpenText().Close();

                Connection.Close();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(flag, e.Message);
            }

        }

        public bool VerifiedDatabase(string database)
        {
            bool sw;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    string query = "SELECT name FROM sys.databases WHERE name='" + database + "'";

                    SqlCommand cmd = new SqlCommand(query, Connection);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        sw = true;
                    }
                    else
                    {
                        sw = false;
                    }


                    Connection.Close();

                    return sw;

                }

            }
            catch (Exception e)
            {
                sw = true;

                Connection.Close();

                return sw;
            }
        }

        public Tuple<int, string> EstablecerPeriodoContable(List<PeriodoContable> listaPeriodoContable)
        {
            int flag = 0;

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (PeriodoContable periodo in listaPeriodoContable)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            Connection = new SqlConnection(connectionString);


                            cmd.CommandText = "AddPeriods";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@PeriodCat", periodo.PeriodCat);

                            cmd.Parameters.AddWithValue("@PeriodName", periodo.PeriodName);

                            cmd.Parameters.AddWithValue("@FinancYear", periodo.FinancYear);

                            cmd.Parameters.AddWithValue("@SubType", periodo.SubType);

                            cmd.Parameters.AddWithValue("@PeriodNum", periodo.PeriodNum);

                            cmd.Parameters.AddWithValue("@F_RefDate", periodo.F_RefDate);

                            cmd.Parameters.AddWithValue("@T_RefDate", periodo.T_RefDate);

                            cmd.Parameters.AddWithValue("@F_TaxDate", periodo.F_TaxDate);

                            cmd.Parameters.AddWithValue("@T_TaxDate", periodo.T_TaxDate);

                            cmd.Parameters.AddWithValue("@F_DueDate", periodo.F_DueDate);

                            cmd.Parameters.AddWithValue("@T_DueDate", periodo.T_DueDate);

                            cmd.Parameters.AddWithValue("@Year", periodo.Year);

                            cmd.Parameters.AddWithValue("@UserSign", periodo.UserSign);

                            cmd.Parameters.AddWithValue("@UpdateDate", periodo.UpdateDate);

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

        public Tuple<int, string> SubPeriodosContables(List<SubPeriodo> listaSubPeriodos)
        {
            int flag = 0;

            int i = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (SubPeriodo periodo in listaSubPeriodos)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "AddSubPeriods";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@Code", periodo.Code1);
                            cmd.Parameters.AddWithValue("@Name", periodo.Name1);
                            cmd.Parameters.AddWithValue("@F_RefDate", periodo.F_RefDate1);
                            cmd.Parameters.AddWithValue("@T_RefDate", periodo.T_RefDate1);
                            cmd.Parameters.AddWithValue("@F_DueDate", periodo.F_DueDate1);
                            cmd.Parameters.AddWithValue("@T_DueDate", periodo.T_DueDate1);
                            cmd.Parameters.AddWithValue("@F_TaxDate", periodo.F_TaxDate1);
                            cmd.Parameters.AddWithValue("@T_TaxDate", periodo.T_TaxDate1);
                            cmd.Parameters.AddWithValue("@UserSign", periodo.UserSing);
                            cmd.Parameters.AddWithValue("@SubNum", periodo.SubNum1);
                            cmd.Parameters.AddWithValue("@Category", periodo.Category1);
                            cmd.Parameters.AddWithValue("@UpdateDate", periodo.UpdateDate1);
                            cmd.Parameters.AddWithValue("@PeriodStat", periodo.PeriodState1);
                            cmd.Parameters.AddWithValue("@UserSign2", periodo.UserSing2);

                            flag = cmd.ExecuteNonQuery();

                            i = flag + i;
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(i, error);

            }
            catch (Exception e)
            {              

                Connection.Close();

                return Tuple.Create(i,e.Message);
            }


        }
    }
}
