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
    public class ModeloFacturaDeudores : ModeloDocumentoVenta
    {
      
        public Tuple<int, string> SelectDocNum()
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
                        cmd.CommandText = "SelectDocNumOINV";

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

                return Tuple.Create(DocNum, e.Message);

            }
        }

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
                        cmd.CommandText = "FindDocEntryOINV";

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

        public Tuple<int, string> FindDocEntryPreliminar(int docNum)
        {
            int DocEntry = 0;

            string error = null;

            try
            {

                DataRow dataRow = dtOINV.AsEnumerable().Where(r => ((int)r["DocNum"]).Equals(docNum) && ((string)r["DocSubType"]).Equals("--")).First();

                DocEntry = Convert.ToInt32(dataRow["DocEntry"]);
               
                return Tuple.Create(DocEntry, error);

            }
            catch (Exception e)
            {               

                return Tuple.Create(DocEntry, e.Message);

            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindSalesInvoice(List<DocumentoCabecera> listaPurchaseInvoice)
        {
            List<DocumentoCabecera> newListSales = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (DocumentoCabecera SalesInvoice in listaPurchaseInvoice)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindSales";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@DocNum", SalesInvoice.DocNum);
                            cmd.Parameters.AddWithValue("@DocDate", SalesInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", SalesInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", SalesInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@CardCode", SalesInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", SalesInvoice.CardName);
                            cmd.Parameters.AddWithValue("@NumAtCard", SalesInvoice.NumAtCard);
                            cmd.Parameters.AddWithValue("@NumControl", SalesInvoice.NumControl);
                            cmd.Parameters.AddWithValue("@TipoTrans", SalesInvoice.TipoTrans);
                            cmd.Parameters.AddWithValue("@Comments", SalesInvoice.Comments);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                DocumentoCabecera newSales = new DocumentoCabecera();

                                newSales.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                                newSales.DocNum = Convert.ToInt32(reader["DocNum"]);
                                newSales.CardCode = reader["CardCode"].ToString();
                                newSales.CardName = reader["CardName"].ToString();
                                newSales.NumAtCard = reader["NumAtCard"].ToString();
                                newSales.DocStatus = Convert.ToChar(reader["DocStatus"]);
                                newSales.NumControl = reader["U_IDA_NumControl"].ToString();
                                newSales.DocCurr = reader["DocCur"].ToString();
                                newSales.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                                newSales.DocDate = Convert.ToDateTime(reader["DocDate"]);
                                newSales.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                                newSales.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                                newSales.Address2 = reader["Address2"].ToString();
                                newSales.Comments = reader["Comments"].ToString();
                                newSales.CtlAccount = reader["CtlAccount"].ToString();
                                newSales.JrnlMemo = reader["JrnlMemo"].ToString();
                                newSales.LicTradNum = reader["LicTradNum"].ToString();
                                newSales.DocType = Convert.ToChar(reader["DocType"]);
                                newSales.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                                newSales.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                                newSales.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                                newSales.VatSum = Convert.ToDecimal(reader["VatSum"]);
                                newSales.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                                newSales.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                                newSales.WTSum = Convert.ToDecimal(reader["WTSum"]);
                                newSales.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                                newSales.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                                newSales.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                                newSales.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                                newSales.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                                newListSales.Add(newSales);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSales,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListSales, e.Message);
            }
        }

        

       

        public Tuple<List<DocumentoCabecera>, string> FindLastSalesInvoice()
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastSalesInvoice";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }



       

        public Tuple<List<DocumentoCabecera>, string> FindNextSalesInvoice(string docNum)
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextSalesInvoice";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {

                MessageBox.Show("Error General: " + e.Message, "Purchase Invoice", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindFirstSalesInvoice()
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstSalesInvoice";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }

        public Tuple<List<DocumentoCabecera>, string> FindPreviousSalesInvoice(string docNum)
        {
            List<DocumentoCabecera> newListSalesInvoice = new List<DocumentoCabecera>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousSalesInvoice";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocNum", docNum);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            DocumentoCabecera newSalesInvoice = new DocumentoCabecera();

                            newSalesInvoice.DocEntry = Convert.ToInt32(reader["DocEntry"]);
                            newSalesInvoice.DocNum = Convert.ToInt32(reader["DocNum"]);
                            newSalesInvoice.CardCode = reader["CardCode"].ToString();
                            newSalesInvoice.CardName = reader["CardName"].ToString();
                            newSalesInvoice.NumAtCard = reader["NumAtCard"].ToString();
                            newSalesInvoice.DocStatus = Convert.ToChar(reader["DocStatus"]);
                            newSalesInvoice.NumControl = reader["U_IDA_NumControl"].ToString();
                            newSalesInvoice.DocCurr = reader["DocCur"].ToString();
                            newSalesInvoice.TipoTrans = reader["U_IDA_TipoTrans"].ToString();
                            newSalesInvoice.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newSalesInvoice.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newSalesInvoice.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newSalesInvoice.Address2 = reader["Address2"].ToString();
                            newSalesInvoice.Comments = reader["Comments"].ToString();
                            newSalesInvoice.CtlAccount = reader["CtlAccount"].ToString();
                            newSalesInvoice.JrnlMemo = reader["JrnlMemo"].ToString();
                            newSalesInvoice.LicTradNum = reader["LicTradNum"].ToString();
                            newSalesInvoice.DocType = Convert.ToChar(reader["DocType"]);
                            newSalesInvoice.DocTotal = Convert.ToDecimal(reader["DocTotal"]);
                            newSalesInvoice.DocTotalFC = Convert.ToDecimal(reader["DocTotalFC"]);
                            newSalesInvoice.DocTotalSy = Convert.ToDecimal(reader["DocTotalSy"]);
                            newSalesInvoice.VatSum = Convert.ToDecimal(reader["VatSum"]);
                            newSalesInvoice.VatSumFC = Convert.ToDecimal(reader["VatSumFC"]);
                            newSalesInvoice.VatSumSy1 = Convert.ToDecimal(reader["VatSumSy"]);
                            newSalesInvoice.WTSum = Convert.ToDecimal(reader["WTSum"]);
                            newSalesInvoice.WTSumFC = Convert.ToDecimal(reader["WTSumFC"]);
                            newSalesInvoice.WTSumSC = Convert.ToDecimal(reader["WTSumSC"]);
                            newSalesInvoice.BaseAmnt = Convert.ToDecimal(reader["BaseAmnt"]);
                            newSalesInvoice.BaseAmntFC = Convert.ToDecimal(reader["BaseAmntFC"]);
                            newSalesInvoice.BaseAmntSC = Convert.ToDecimal(reader["BaseAmntSC"]);

                            newListSalesInvoice.Add(newSalesInvoice);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListSalesInvoice,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListSalesInvoice, e.Message);
            }
        }



       
    }
}
