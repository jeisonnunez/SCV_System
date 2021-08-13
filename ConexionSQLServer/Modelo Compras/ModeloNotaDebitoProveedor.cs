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
    public class ModeloNotaDebitoProveedor : Modelo_Compras.ModeloDocumentoCompra
    {
        public Tuple<int, string> FindDocEntry(int docNum)
        {
            int DocEntry = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindDocEntryOPCHND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            DocEntry = Convert.ToInt32(reader["DocEntry"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<int,string> SelectDocNum()
        {
            int DocNum = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "SelectDocNumOPCHND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            DocNum = Convert.ToInt32(reader["DocNum"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(DocNum,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(DocNum,e.Message);

            }
        }

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            int DocEntry = 0;

            string error = null;

            try
            {

                DataRow dataRow = dtOPCH.AsEnumerable().Where(r => ((int)r["DocNum"]).Equals(docNum) && ((string)r["DocSubType"]).Equals("DM")).First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);

              
                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {

            
                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<DataTable,string> FindPurchaseInvoiceLines(int docEntry)
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
                        cmd.CommandText = "FindPurchaseInvoiceLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntry);

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

                return Tuple.Create(tabla,e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindPurchaseDebitNote(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            List<DocumentoCabecera> newListPurchase = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera PurchaseInvoice in listaPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindPurchaseND";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", PurchaseInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", PurchaseInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", PurchaseInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", PurchaseInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@CardCode", PurchaseInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", PurchaseInvoice.CardName);
                            cmd.Parameters.AddWithValue("@NumAtCard", PurchaseInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", PurchaseInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", PurchaseInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", PurchaseInvoice.Comments);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                DocumentoCabecera newPurchase = new DocumentoCabecera();

                                newPurchase.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                                newPurchase.DocNum = Convert.ToInt32(reader["DocNum"]);
                                newPurchase.CardCode = reader["CardCode"].ToString();
                                newPurchase.CardName = reader["CardName"].ToString();
                                newPurchase.NumAtCard = reader["NumAtCard"].ToString();
                                newPurchase.DocStatus = Convert.ToChar(reader["DocStatus"]);
                                newPurchase.NumControl = reader["U_IDA_NumControl"].ToString();
                                newPurchase.DocCurr = reader["DocCur"].ToString();
                                newPurchase.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                                newPurchase.DocDate = Convert.ToDateTime(reader["DocDate"]);
                                newPurchase.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                                newPurchase.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                                newPurchase.Address2 = reader["Address2"].ToString();
                                newPurchase.Comments = reader["Comments"].ToString();
                                newPurchase.CtlAccount = reader["CtlAccount"].ToString();
                                newPurchase.JrnlMemo = reader["JrnlMemo"].ToString();
                                newPurchase.LicTradNum = reader["LicTradNum"].ToString();
                                newPurchase.DocType = Convert.ToChar(reader["DocType"]);
                                newPurchase.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                                newPurchase.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                                newPurchase.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                                newPurchase.VatSum = Convert.ToDecimal(reader["VatSum"]);
                                newPurchase.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                                newPurchase.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                                newPurchase.WTSum = Convert.ToDecimal(reader["WTSum"]);
                                newPurchase.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                                newPurchase.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                                newPurchase.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                                newPurchase.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                                newPurchase.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);
                                

                                newListPurchase.Add(newPurchase);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchase,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchase,e.Message);
            }
        }

      
        public Tuple<List<DocumentoCabecera>,string> FindLastPurchaseDebitNote()
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastPurchaseInvoiceND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);
                           

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice, e.Message);
            }
        }



      
        
        public Tuple<List<DocumentoCabecera>,string> FindNextPurchaseDebitNote(string docNum)
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextPurchaseInvoiceND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);
                            

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,e.Message);
            }
        }


        public Tuple<List<DocumentoCabecera>,string> FindFirstPurchaseDebitNote()
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstPurchaseInvoiceND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);
                           

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>,string> FindPreviousPurchaseDebitNote(string docNum)
        {
            List<DocumentoCabecera> newListPurchaseInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousPurchaseInvoiceND";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newPurchaseInvoice = new DocumentoCabecera();

                            newPurchaseInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newPurchaseInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newPurchaseInvoice.CardCode = reader["CardCode"].ToString();
                            newPurchaseInvoice.CardName = reader["CardName"].ToString();
                            newPurchaseInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newPurchaseInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newPurchaseInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newPurchaseInvoice.DocCurr = reader["DocCur"].ToString();
                            newPurchaseInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newPurchaseInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newPurchaseInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newPurchaseInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newPurchaseInvoice.Address2 = reader["Address2"].ToString();
                            newPurchaseInvoice.Comments = reader["Comments"].ToString();
                            newPurchaseInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newPurchaseInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newPurchaseInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newPurchaseInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newPurchaseInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newPurchaseInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newPurchaseInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newPurchaseInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newPurchaseInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newPurchaseInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newPurchaseInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newPurchaseInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newPurchaseInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newPurchaseInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newPurchaseInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newPurchaseInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);
                            

                            newListPurchaseInvoice.Add(newPurchaseInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListPurchaseInvoice, e.Message);
            }
        }


    }
}
