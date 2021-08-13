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
    public class ModeloSocioNegocio : ConexionSQLServer
    {
        public Tuple<List<SocioNegocio>, string> FindNext(string codigo)
        {
            List<SocioNegocio> newListaSN = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextBusinessPartner";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", codigo);

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

                            newListaSN.Add(newSN);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaSN,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaSN, e.Message);
            }
        }

        public Tuple<List<CodigosFiscales>, string> ConsultaCodigosFiscales()
        {
            List<CodigosFiscales> newListaCodigosFiscales = new List<CodigosFiscales>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindCodigosFiscalesActivos";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            CodigosFiscales newCodigosFiscales = new CodigosFiscales();

                            newCodigosFiscales.Code = reader["Code"].ToString();
                            newCodigosFiscales.Rate = Convert.ToDecimal(reader["Rate"]);

                            newListaCodigosFiscales.Add(newCodigosFiscales);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaCodigosFiscales,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaCodigosFiscales, e.Message);

            }
        }

        public Tuple<int, string> DeleteBusinessPartner(string cardCode)
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

                        cmd.CommandText = "DeleteBusinessPartner";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", cardCode);

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

        

        public Tuple<int, string> UpdateBP(List<SocioNegocio> listaSocioNegocio)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (SocioNegocio socioNegocio in listaSocioNegocio)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateBusinessPartner";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@OldCardCode", socioNegocio.OldCardCode);
                            cmd.Parameters.AddWithValue("@CardCode", socioNegocio.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", socioNegocio.CardName);
                            cmd.Parameters.AddWithValue("@CardType", socioNegocio.CardType);
                            cmd.Parameters.AddWithValue("@TipoPersona", socioNegocio.TipoPersona);
                            cmd.Parameters.AddWithValue("@LicTradNum", socioNegocio.LicTradNum);
                            cmd.Parameters.AddWithValue("@Sucursal", socioNegocio.Sucursal);
                            cmd.Parameters.AddWithValue("@Contribuyente", socioNegocio.Contribuyente);
                            cmd.Parameters.AddWithValue("@AplicaITF", socioNegocio.AplicaITF);                           
                            cmd.Parameters.AddWithValue("@Phone1", socioNegocio.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", socioNegocio.Phone2);
                            cmd.Parameters.AddWithValue("@Fax", socioNegocio.Fax);
                            cmd.Parameters.AddWithValue("@Currency", socioNegocio.Currency);
                            cmd.Parameters.AddWithValue("@E_Mail", socioNegocio.E_mail);
                            cmd.Parameters.AddWithValue("@MailAddress", socioNegocio.MailAddress);
                            cmd.Parameters.AddWithValue("@CntctPrsn", socioNegocio.CntctPrsn);
                            cmd.Parameters.AddWithValue("@VatGroup", socioNegocio.VatGroup);
                            cmd.Parameters.AddWithValue("@DebPayAcct", socioNegocio.DebPayAcct);
                            cmd.Parameters.AddWithValue("@Address", socioNegocio.Address);
                            cmd.Parameters.AddWithValue("@UserSign", socioNegocio.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", socioNegocio.UpdateDate);
                            

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

        public Tuple<int, string> InsertBP(List<SocioNegocio> listaSocioNegocio)
        {
            int flag = 0;

            string error = null;

            using (Connection = new SqlConnection(connectionString))
            {
                try
                {
                    Connection.Open();

                    transaction = Connection.BeginTransaction(); //inicia la transaccion  

                    foreach (SocioNegocio socioNegocio in listaSocioNegocio)
                    {
                        using (SqlCommand cmd = new SqlCommand("InsertBusinessPartner", Connection, transaction))
                        {
                            // cmd.CommandText = "InsertBusinessPartner";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@CardCode", socioNegocio.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", socioNegocio.CardName);
                            cmd.Parameters.AddWithValue("@CardType", socioNegocio.CardType);
                            cmd.Parameters.AddWithValue("@TipoPersona", socioNegocio.TipoPersona);
                            cmd.Parameters.AddWithValue("@LicTradNum", socioNegocio.LicTradNum);
                            cmd.Parameters.AddWithValue("@Sucursal", socioNegocio.Sucursal);
                            cmd.Parameters.AddWithValue("@Contribuyente", socioNegocio.Contribuyente);
                            cmd.Parameters.AddWithValue("@AplicaITF", socioNegocio.AplicaITF);
                            cmd.Parameters.AddWithValue("@Balance", socioNegocio.Balance);
                            cmd.Parameters.AddWithValue("@BalanceSys", socioNegocio.BalanceSys);
                            cmd.Parameters.AddWithValue("@BalanceFC", socioNegocio.BalanceFC);
                            cmd.Parameters.AddWithValue("@DNotesBal", socioNegocio.DNotesBal);
                            cmd.Parameters.AddWithValue("@DNoteBalSy", socioNegocio.DNoteBalSy);
                            cmd.Parameters.AddWithValue("@DNoteBalFC", socioNegocio.DNoteBalFC);
                            cmd.Parameters.AddWithValue("@Phone1", socioNegocio.Phone1);
                            cmd.Parameters.AddWithValue("@Phone2", socioNegocio.Phone2);
                            cmd.Parameters.AddWithValue("@Fax", socioNegocio.Fax);
                            cmd.Parameters.AddWithValue("@Currency", socioNegocio.Currency);
                            cmd.Parameters.AddWithValue("@E_Mail", socioNegocio.E_mail);
                            cmd.Parameters.AddWithValue("@MailAddress", socioNegocio.MailAddress);
                            cmd.Parameters.AddWithValue("@CntctPrsn", socioNegocio.CntctPrsn);
                            cmd.Parameters.AddWithValue("@VatGroup", socioNegocio.VatGroup);
                            cmd.Parameters.AddWithValue("@DebPayAcct", socioNegocio.DebPayAcct);
                            cmd.Parameters.AddWithValue("@Address", socioNegocio.Address);
                            cmd.Parameters.AddWithValue("@UserSign", socioNegocio.UserSign);
                            cmd.Parameters.AddWithValue("@UpdateDate", socioNegocio.UpdateDate);
                            cmd.Parameters.AddWithValue("@Deleted", socioNegocio.Deleted);

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

        public Tuple<List<SocioNegocio>, string> ConsultaSocioNegocio(List<SocioNegocio> listaSocioNegocio)
        {
            List<SocioNegocio> newListaSN = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (SocioNegocio socioNegocio in listaSocioNegocio)
                    {

                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindBusinessPartner";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@CardCode", socioNegocio.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", socioNegocio.CardName);
                            cmd.Parameters.AddWithValue("@CardType", socioNegocio.CardType);
                            cmd.Parameters.AddWithValue("@LicTradNum", socioNegocio.LicTradNum);
                            cmd.Parameters.AddWithValue("@Currency", socioNegocio.Currency);
                            cmd.Parameters.AddWithValue("@TipoPersona", socioNegocio.TipoPersona);
                            cmd.Parameters.AddWithValue("@Contribuyente", socioNegocio.Contribuyente);
                            cmd.Parameters.AddWithValue("@Sucursal", socioNegocio.Sucursal);
                            cmd.Parameters.AddWithValue("@AplicaITF", socioNegocio.AplicaITF);
                            

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

                                newListaSN.Add(newSN);

                            }

                            reader.Close();
                        }

                    }

                }

                Connection.Close();

                return Tuple.Create(newListaSN,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaSN, e.Message);
            }
        }

        public Tuple<List<SocioNegocio>, string> FindPrevious(string codigo)
        {
            List<SocioNegocio> newListaSN = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousBusinessPartner";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", codigo);

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
                            newSN.AplicaITF= Convert.ToChar(reader["U_IDA_AplicaITF"]);
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

                            newListaSN.Add(newSN);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaSN,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaSN, e.Message);
            }
        }
        
        public Tuple<List<SocioNegocio>, string> FindLast()
        {
            List<SocioNegocio> newListaSN = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastBusinessPartner";

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

                            newListaSN.Add(newSN);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaSN,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListaSN, e.Message);
            }
        }

        public Tuple<List<SocioNegocio>, string> FindFirst()
        {
            List<SocioNegocio> newListaSN = new List<SocioNegocio>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstBusinessPartner";

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

                            newListaSN.Add(newSN);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaSN,error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaSN, e.Message);
            }
        }
    }
}
