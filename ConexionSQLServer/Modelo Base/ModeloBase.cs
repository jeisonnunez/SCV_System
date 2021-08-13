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
    public class ModeloBase: ConexionSQLServer
    {
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

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {
                
                DataTable tabla = new DataTable();

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }

        }

        public Tuple<DataTable, string> ConsultaMonedasWithOutMainCurrency()
        {
            string error = null;

            try
            {

                Connection = new SqlConnection(connectionString);

                string query = "SELECT T0.CurrCode, T0.CurrName FROM OCRN T0 INNER JOIN OADM T1 ON T0.CurrCode<>T1.MainCurncy";

                SqlCommand cmd = new SqlCommand(query, Connection);

                data = new SqlDataAdapter(cmd);

                DataTable tabla = new DataTable();

                data.Fill(tabla);

                Connection.Close();

                return Tuple.Create(tabla, error);

            }
            catch (Exception e)
            {

                DataTable tabla = new DataTable();

                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }

        }

        

        public Tuple<int, string> ObtenerIdUser(string username)
        {
            int usercode = 0;

            string error = null;


            using (Connection = new SqlConnection(connectionString))
            {
                Connection.Open();

                //transaction = Connection.BeginTransaction(); //inicia la transaccion 

                try
                {

                    using (SqlCommand cmde = new SqlCommand("GetUserCode", Connection))
                    {

                        //cmde.CommandText = "GetUserCode";

                        cmde.CommandType = CommandType.StoredProcedure;

                        cmde.Parameters.AddWithValue("@User", username);

                        SqlDataReader reader = cmde.ExecuteReader();

                        if (reader.Read())
                        {
                            usercode = Int32.Parse(reader["USERID"].ToString());
                        }


                    }

                   // transaction.Commit();

                    Connection.Close();

                    return Tuple.Create(usercode, error);
                }
                catch (Exception e)
                {
                    //transaction.Rollback();

                    Connection.Close();

                    return Tuple.Create(usercode, e.Message);
                }

            }

        }

        public Tuple<DataTable,string> FindSupplierCurrency(string supplier)
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
                        cmd.CommandText = "FindBusinessPartnerCurrency";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", supplier);

                        data = new SqlDataAdapter(cmd);

                        data.Fill(tabla);

                    }

                }

                Connection.Close();

                return Tuple.Create(tabla,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(tabla, e.Message);
            }
        }

        public Tuple<List<SocioNegocio>, string> FindBP()
        {
            List<SocioNegocio> newListaBP = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindBP";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SocioNegocio newSN = new SocioNegocio();

                            newSN.OldCardCode = reader["CardCode"].ToString();
                            newSN.CardCode = reader["CardCode"].ToString();
                            newSN.CardName = reader["CardName"].ToString();
                            newSN.CardType = Convert.ToChar(reader["CardType"]);
                            newSN.TipoPersona = reader["U_IDA_TipoPersona"].ToString();
                            newSN.LicTradNum = reader["LicTradNum"].ToString();
                            newSN.Currency = reader["Currency"].ToString();
                            newSN.Sucursal = Convert.ToChar(reader["U_IDA_Sucursal"]);
                            newSN.Contribuyente = Convert.ToChar(reader["U_IDA_Contribuyente"]);
                            newSN.AplicaITF = Convert.ToChar(reader["U_IDA_AplicaITF"]);
                            newSN.Balance = Convert.ToDecimal(reader["Balance"]);
                            newSN.DNotesBal = Convert.ToDecimal(reader["DNotesBal"]);
                            newSN.Phone1 = reader["Phone1"].ToString();
                            newSN.Phone2 = reader["Phone2"].ToString();
                            newSN.Fax = reader["Fax"].ToString();
                            newSN.E_mail = reader["E_Mail"].ToString();
                            newSN.MailAddress = reader["MailAddres"].ToString();
                            newSN.CntctPrsn = reader["CntctPrsn"].ToString();
                            newSN.VatGroup = reader["VatGroup"].ToString();
                            newSN.DebPayAcct = reader["DebPayAcct"].ToString();
                            newSN.Address = reader["Address"].ToString();

                            newListaBP.Add(newSN);

                        }

                        reader.Close();
                    }



                }

                Connection.Close();

                return Tuple.Create(newListaBP, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaBP, e.Message);
            }
        }

        public Tuple<List<SocioNegocio>, string> FindCustomer()
        {
            List<SocioNegocio> newListaCustomer = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindCustomer";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SocioNegocio newSN = new SocioNegocio();

                            newSN.OldCardCode = reader["CardCode"].ToString();
                            newSN.CardCode = reader["CardCode"].ToString();
                            newSN.CardName = reader["CardName"].ToString();
                            newSN.CardType = Convert.ToChar(reader["CardType"]);
                            newSN.TipoPersona = reader["U_IDA_TipoPersona"].ToString();
                            newSN.LicTradNum = reader["LicTradNum"].ToString();
                            newSN.Currency = reader["Currency"].ToString();
                            newSN.Sucursal = Convert.ToChar(reader["U_IDA_Sucursal"]);
                            newSN.Contribuyente = Convert.ToChar(reader["U_IDA_Contribuyente"]);
                            newSN.AplicaITF = Convert.ToChar(reader["U_IDA_AplicaITF"]);
                            newSN.Balance = Convert.ToDecimal(reader["Balance"]);
                            newSN.DNotesBal = Convert.ToDecimal(reader["DNotesBal"]);
                            newSN.Phone1 = reader["Phone1"].ToString();
                            newSN.Phone2 = reader["Phone2"].ToString();
                            newSN.Fax = reader["Fax"].ToString();
                            newSN.E_mail = reader["E_Mail"].ToString();
                            newSN.MailAddress = reader["MailAddres"].ToString();
                            newSN.CntctPrsn = reader["CntctPrsn"].ToString();
                            newSN.VatGroup = reader["VatGroup"].ToString();
                            newSN.DebPayAcct = reader["DebPayAcct"].ToString();
                            newSN.Address = reader["Address"].ToString();

                            newListaCustomer.Add(newSN);

                        }

                        reader.Close();
                    }



                }

                Connection.Close();

                return Tuple.Create(newListaCustomer,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaCustomer, e.Message);
            }
        }

        public Tuple<List<SocioNegocio>, string> FindSuppliers()
        {
            List<SocioNegocio> newListaSuppliers = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindSuppliers";

                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                SocioNegocio newSN = new SocioNegocio();

                                newSN.OldCardCode = reader["CardCode"].ToString();
                                newSN.CardCode = reader["CardCode"].ToString();
                                newSN.CardName = reader["CardName"].ToString();
                                newSN.CardType = Convert.ToChar(reader["CardType"]);
                                newSN.TipoPersona = reader["U_IDA_TipoPersona"].ToString();
                                newSN.LicTradNum = reader["LicTradNum"].ToString();
                                newSN.Currency = reader["Currency"].ToString();
                                newSN.Sucursal = Convert.ToChar(reader["U_IDA_Sucursal"]);
                                newSN.Contribuyente = Convert.ToChar(reader["U_IDA_Contribuyente"]);
                                newSN.AplicaITF = Convert.ToChar(reader["U_IDA_AplicaITF"]);
                                newSN.Balance = Convert.ToDecimal(reader["Balance"]);
                                newSN.DNotesBal = Convert.ToDecimal(reader["DNotesBal"]);
                                newSN.Phone1 = reader["Phone1"].ToString();
                                newSN.Phone2 = reader["Phone2"].ToString();
                                newSN.Fax = reader["Fax"].ToString();
                                newSN.E_mail = reader["E_Mail"].ToString();
                                newSN.MailAddress = reader["MailAddres"].ToString();
                                newSN.CntctPrsn = reader["CntctPrsn"].ToString();
                                newSN.VatGroup = reader["VatGroup"].ToString();
                                newSN.DebPayAcct = reader["DebPayAcct"].ToString();
                                newSN.Address = reader["Address"].ToString();

                                newListaSuppliers.Add(newSN);

                            }

                            reader.Close();
                        }

                   

                }

                Connection.Close();

                return Tuple.Create(newListaSuppliers,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaSuppliers, e.Message);
            }
        }

        public Tuple<string, string> GetCurrency()
        {
            string mainCurrency = "";

            string sysCurrency = "";

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindMainSysCurrency";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            mainCurrency = reader["MainCurncy"].ToString();
                            sysCurrency = reader["SysCurrncy"].ToString();
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(mainCurrency, sysCurrency);

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Journal Entry Lines", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return Tuple.Create(mainCurrency, sysCurrency);
            }
        }

        public Tuple<int, string> UpdateDeletedUser(string username)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> UpdateDeletedCurrency(string currency)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> UpdateDeletedBusinessPartner(string businessPartner)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> UpdateDeletedItem(string item)
        {
            throw new NotImplementedException();
        }

        public Tuple<int, string> GetPeriodCode(DateTime? refDate)
        {
            int code = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindPeriodCode";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RefDate", refDate);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            code = Convert.ToInt32(reader["AbsEntry"]);
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(code,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(code,e.Message);
            }
        }

        public Tuple<int, string> SelectTransId()
        {
            int transid=0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "SelectTransId";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            transid =Convert.ToInt32(reader["TransId"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(transid, error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(transid, e.Message);

            }

        }

        public Tuple<int, string> SelectBaseRef()
        {
            int baseRef = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "SelectBaseRef";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {

                            baseRef = Convert.ToInt32(reader["BaseRef"]);


                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(baseRef,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(baseRef, e.Message);

            }
        }

        public Tuple<List<Cuenta>, string> ConsultaCuentasNoAsociadas()
        {
            List<Cuenta> newListaCuenta = new List<Cuenta>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindAccountNoAsociada";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Cuenta newCuentaAsociada = new Cuenta();

                            newCuentaAsociada.AcctCode = reader["AcctCode"].ToString();
                            newCuentaAsociada.AcctName = reader["AcctName"].ToString();

                            newListaCuenta.Add(newCuentaAsociada);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCuenta,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(newListaCuenta, e.Message);

            }
        }

        public Tuple<object, string> VerificaFCCurrency(string fCCurrency)
        {
            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindExistFCCurrency";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@FCCurrency", fCCurrency);

                        var result = cmd.ExecuteScalar();

                        Connection.Close();

                        return Tuple.Create(result,error);
                    }

                }



            }
            catch (Exception e)
            {
                
                Connection.Close();

                object error1 = null;

                return Tuple.Create(error1,e.Message);
            }
        }

        public Tuple<decimal, string> FinDRateFCCurrency(DateTime? selectedDate, string fCCurrency)
        {
            decimal rateFC = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindRateFC";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RateDate", selectedDate);

                        cmd.Parameters.AddWithValue("@Currency", fCCurrency);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            rateFC = Convert.ToDecimal(reader["Rate"]);
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(rateFC, error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(rateFC,e.Message);
            }
        }

       

        public int UpdateDebitCreditAccountBusinessPartner(string shortName, decimal saldo, decimal sysSaldo, decimal fCSaldo)
        {
            int flag = 0;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "UpdateDebitCreditAccountBusinessPartner";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", shortName);
                        cmd.Parameters.AddWithValue("@Balance", saldo);
                        cmd.Parameters.AddWithValue("@BalanceSys", sysSaldo);
                        cmd.Parameters.AddWithValue("@BalanceFC", fCSaldo);

                        flag = cmd.ExecuteNonQuery();

                    }


                }

                Connection.Close();

                return flag;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return flag;
            }
        }

        public Tuple<decimal, string> FindRate(DateTime? rateDate)
        {
            decimal rate = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindRate";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RateDate", rateDate);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            rate = Convert.ToDecimal(reader["Rate"]);
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return Tuple.Create(rate,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(rate, e.Message);
            }
        }

        public int UpdateDebitCreditAccount(string account, decimal saldo, decimal sysSaldo, decimal fCSaldo)
        {
            int flag = 0;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "UpdateDebitCreditAccount";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", account);
                        cmd.Parameters.AddWithValue("@CurrTotal", saldo);
                        cmd.Parameters.AddWithValue("@SysTotal", sysSaldo);
                        cmd.Parameters.AddWithValue("@FcTotal", fCSaldo);

                        flag = cmd.ExecuteNonQuery();
                        
                    }


                }

                Connection.Close();

                return flag;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return flag;
            }
        }

        public string FindAcctName(string account)
        {
            string acctName = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindAcctName";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", account);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            acctName =reader["AcctName"].ToString();
                        }

                        reader.Close();
                    }


                }

                Connection.Close();

                return acctName;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return acctName;
            }
        }
    }
}
