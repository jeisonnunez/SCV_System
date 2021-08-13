using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Datos
{
    public class ModeloDocumento:ConexionSQLServer
    {
        protected static readonly Regex regex = new Regex("[^0-9,.-]");

        protected static readonly Regex regexString = new Regex("[^A-Za-z]");

        protected static NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

        protected static DataSet dataSet;

        protected static DataTable dtOPCH;

        protected static DataTable dtPCH1;

        protected static DataTable dtPCH5;
        
        protected static DataTable dtOINV;

        protected static DataTable dtINV1;

        protected static DataTable dtINV5;

        protected static DataTable dtOJDT;

        protected static DataTable dtJDT1;

        protected static DataTable dtOINM;       

        protected static DataTable dtODLN;

        protected static DataTable dtDLN1;

        protected static DataTable dtORPC;

        protected static DataTable dtRPC1;

        protected static DataTable dtRPC5;

        protected static DataTable dtORIN;

        protected static DataTable dtRIN1;

        protected static DataTable dtRIN5;

        protected static DataTable dtOPDN;

        protected static DataTable dtPDN1;

        protected static DataTable dtORCT;

        protected static DataTable dtRCT2;

        protected static DataTable dtOVPM;

        protected static DataTable dtVPM2;

        protected decimal ConvertDecimalTwoPlaces<T>(T number)
        {
            string str = null;

            decimal amount = 0;

            if (String.IsNullOrWhiteSpace(number.ToString()) == false)
            {
                str = regex.Replace(number.ToString(), String.Empty);

                amount = decimal.Parse(str.ToString(), nfi);

                amount = Math.Round(amount, 2);
            }
            else
            {
                amount = 0;
            }
            return amount;
        }

        protected static void SetNumericConfiguration()
        {
            nfi.NumberGroupSeparator = ".";
            nfi.NumberDecimalSeparator = ",";
        }

        public Tuple<int, string> ReduceSaldoAdeudado(string cardCode, decimal docTransId, int line_ID)
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
                        cmd.CommandText = "ReduceSaldoAdeudadoJDT1";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", cardCode);

                        cmd.Parameters.AddWithValue("@DocTransId", docTransId);

                        cmd.Parameters.AddWithValue("@Line_ID", line_ID);

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

        public Tuple<int, string> ReduceSaldoAdeudado(string cardCode, decimal docTransId)
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
                        cmd.CommandText = "ReduceSaldoAdeudadoSN";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CardCode", cardCode);

                        cmd.Parameters.AddWithValue("@DocTransId", docTransId);

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

        public Tuple<string, string> FindBeneficioDiferenciaTipoCambio()
        {
            string AcctCode = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindBeneficioDiferenciaTipoCambio";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            AcctCode = reader["AcctCode"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(AcctCode, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(AcctCode, e.Message);

            }
        }

        public Tuple<string, string> FindCuentaRedondeo()
        {
            string AcctCode = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindCuentaRedondeo";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            AcctCode = reader["AcctCode"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(AcctCode, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(AcctCode, e.Message);

            }
        }

        public Tuple<string, string> FindPerdidaDiferenciaTipoCambio()
        {
            string AcctCode = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindPerdidaDiferenciaTipoCambio";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            AcctCode = reader["AcctCode"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(AcctCode, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(AcctCode, e.Message);

            }
        }

        public Tuple<string, string> FindBeneficioDiferenciaConversion()
        {
            string AcctCode = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindBeneficioDiferenciaConversion";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            AcctCode = reader["AcctCode"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(AcctCode, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(AcctCode, e.Message);

            }
        }

        public Tuple<string, string> FindPerdidaDiferenciaConversion()
        {
            string AcctCode = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindPerdidaDiferenciaConversion";

                        cmd.CommandType = CommandType.StoredProcedure;                      

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            AcctCode = reader["AcctCode"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(AcctCode, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(AcctCode, e.Message);

            }
        }


        public Tuple<DataTable, string> FindSalesInvoiceLines(int docEntry)
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
                        cmd.CommandText = "FindSalesInvoiceLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docEntry);

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

        public Tuple<int, string> InsertSalesInvoiceLinesPreliminar(List<DocumentoDetalle> listSalesInvoiceLines)
        {
            //LoadDatatable INV1_TEST

            dtINV1 = dataSet.Tables["INV1_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (DocumentoDetalle SalesInvoice in listSalesInvoiceLines)
                {
                    DocEntry = SalesInvoice.DocEntry;

                    DataRow newRow = dtINV1.NewRow();

                    newRow["DocEntry"] = SalesInvoice.DocEntry;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["LineNum"] = SalesInvoice.LineNum;
                    newRow["LineStatus"] = SalesInvoice.LineStatus;
                    newRow["ItemCode"] = SalesInvoice.ItemCode;
                    newRow["Dscription"] = SalesInvoice.Dscription;
                    newRow["Quantity"] = SalesInvoice.Quantity;
                    newRow["OpenQty"] = SalesInvoice.OpenQty;
                    newRow["InvQty"] = SalesInvoice.InvQty;
                    newRow["OpenInvQty"] = SalesInvoice.OpenInvQty;
                    newRow["Price"] = SalesInvoice.Price;
                    newRow["Currency"] = SalesInvoice.Currency;
                    newRow["LineTotal"] = SalesInvoice.LineTotal;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["AcctCode"] = SalesInvoice.AcctCode;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["BaseCard"] = SalesInvoice.BaseCard;
                    newRow["TotalSumSy"] = SalesInvoice.TotalSumSy;
                    newRow["TotalFrgn"] = SalesInvoice.TotalFrgn;
                    newRow["VatSum"] = SalesInvoice.VatSum;
                    newRow["VatSumFrgn"] = SalesInvoice.VatSumFrgn;
                    newRow["VatGroup"] = SalesInvoice.VatGroup;
                    newRow["VatPrcnt"] = SalesInvoice.VatPrcnt;
                    newRow["FinncPriod"] = SalesInvoice.FinncPriod;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["Address"] = SalesInvoice.Address;
                    newRow["GTotal"] = SalesInvoice.Gtotal;
                    newRow["GTotalFC"] = SalesInvoice.GtotalFC;
                    newRow["GTotalSC"] = SalesInvoice.GtotalSC;
                    newRow["StockSum"] = SalesInvoice.StockSum;
                    newRow["StockSumFc"] = SalesInvoice.StockSumFc;
                    newRow["StockSumSc"] = SalesInvoice.StockSumSc;
                    newRow["InvntSttus"] = SalesInvoice.InvntSttus;
                    newRow["WtLiable"] = SalesInvoice.WtLiable;
                    newRow["DataSource"] = SalesInvoice.DataSource;

                    dtINV1.Rows.Add(newRow);

                    dtINV1.AcceptChanges();
                }

                DataRow[] selected = dtINV1.Select("DocEntry = " + DocEntry);

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }

        }

        public Tuple<bool, string> VerifyCuentasNoAsociadas(string acctCode)
        {
            bool sw = false;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "VerifyCuentasNoAsociadas";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@AcctCode", acctCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            sw = true;

                        }
                        else
                        {
                            sw = false;
                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(sw, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(sw, e.Message);
            }
        }

        public string CreateDataSetPreliminar()
        {
            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    dataSet = new DataSet("dsOINV");

                    data = new SqlDataAdapter("SELECT * FROM dbo.OJDT", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OJDT_TEST");

                    data.Fill(dataSet, "OJDT_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.JDT1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "JDT1_TEST");

                    data.Fill(dataSet, "JDT1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.OINV", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OINV_TEST");

                    data.Fill(dataSet, "OINV_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.INV1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "INV1_TEST");

                    data.Fill(dataSet, "INV1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.INV5", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "INV5_TEST");

                    data.Fill(dataSet, "INV5_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.ODLN", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "ODLN_TEST");

                    data.Fill(dataSet, "ODLN_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.DLN1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "DLN1_TEST");

                    data.Fill(dataSet, "DLN1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.ORPC", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "ORPC_TEST");

                    data.Fill(dataSet, "ORPC_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.RPC1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "RPC1_TEST");

                    data.Fill(dataSet, "RPC1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.RPC5", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "RPC5_TEST");

                    data.Fill(dataSet, "RPC5_TEST");


                    data = new SqlDataAdapter("SELECT * FROM dbo.ORIN", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "ORIN_TEST");

                    data.Fill(dataSet, "ORIN_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.RIN1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "RIN1_TEST");

                    data.Fill(dataSet, "RIN1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.RIN5", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "RIN5_TEST");

                    data.Fill(dataSet, "RIN5_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.OINM", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OINM_TEST");

                    data.Fill(dataSet, "OINM_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.OPDN", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OPDN_TEST");

                    data.Fill(dataSet, "OPDN_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.PDN1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "PDN1_TEST");

                    data.Fill(dataSet, "PDN1_TEST");


                    data = new SqlDataAdapter("SELECT * FROM dbo.OPCH", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "OPCH_TEST");

                    data.Fill(dataSet, "OPCH_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.PCH1", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "PCH1_TEST");

                    data.Fill(dataSet, "PCH1_TEST");

                    data = new SqlDataAdapter("SELECT * FROM dbo.PCH5", Connection);

                    data.FillSchema(dataSet, SchemaType.Source, "PCH5_TEST");

                    data.Fill(dataSet, "PCH5_TEST");

                    CreateConstraintJournalEntry("OJDT_TEST", "JDT1_TEST");

                    CreateDataRelationJournalEntry("OJDT_TEST", "JDT1_TEST");

                    CreateConstraintSalesInvoiceTAX("OINV_TEST", "INV5_TEST");

                    CreateDataRelationSalesInvoiceTAX("OINV_TEST", "INV5_TEST");

                    CreateConstraintSalesInvoice("OINV_TEST", "INV1_TEST");

                    CreateDataRelationSalesInvoice("OINV_TEST", "INV1_TEST");

                    CreateConstraintEntregaMercancia("ODLN_TEST", "DLN1_TEST");

                    CreateDataRelationEntregaMercancia("ODLN_TEST", "DLN1_TEST");

                    CreateConstraintPurchaseCreditNoteTAX("ORPC_TEST", "RPC5_TEST");

                    CreateDataRelationPurchaseCreditNoteTAX("ORPC_TEST", "RPC5_TEST");

                    CreateConstraintPurchaseCreditNote("ORPC_TEST", "RPC1_TEST");

                    CreateDataRelationPurchaseCreditNote("ORPC_TEST", "RPC1_TEST");

                    CreateConstraintPurchaseInvoice("OPCH_TEST", "PCH1_TEST");

                    CreateDataRelationPurchaseInvoice("OPCH_TEST", "PCH1_TEST");

                    CreateConstraintPurchaseInvoiceTAX("OPCH_TEST", "PCH5_TEST");

                    CreateDataRelationPurchaseInvoiceTAX("OPCH_TEST", "PCH5_TEST");


                    CreateConstraintEntradaMercancia("OPDN_TEST", "PDN1_TEST");

                    CreateDataRelationEntradaMercancia("OPDN_TEST", "PDN1_TEST");

                    CreateConstraintSalesCreditNoteTAX("ORIN_TEST", "RIN5_TEST");

                    CreateDataRelationSalesCreditNoteTAX("ORIN_TEST", "RIN5_TEST");

                    CreateConstraintSalesCreditNote("ORIN_TEST", "RIN1_TEST");

                    CreateDataRelationSalesCreditNote("ORIN_TEST", "RIN1_TEST");

                    dataSet.AcceptChanges();

                }

                Connection.Close();

                return error;

            }
            catch (Exception e)
            {

                Connection.Close();

                return e.Message;
            }

        }

        private void CreateDataRelationSalesCreditNote(string v1, string v2)
        {

            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("SalesCreditNote",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintSalesCreditNote(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("SalesCreditNoteConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        private void CreateDataRelationSalesCreditNoteTAX(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["AbsEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("SalesCreditNoteTax",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintSalesCreditNoteTAX(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["AbsEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("SalesInvoiceTaxConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        private void CreateDataRelationEntradaMercancia(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("EntradaMercancia",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintEntradaMercancia(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("EntradaMercanciaConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        protected void CreateDataRelationPurchaseInvoiceTAX(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["AbsEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("PurchaseInvoiceTax",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        protected void CreateConstraintPurchaseInvoiceTAX(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["AbsEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("PurchaseInvoiceTaxConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        protected void CreateDataRelationPurchaseInvoice(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("PurchaseInvoice",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        protected void CreateConstraintPurchaseInvoice(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("PurchaseInvoiceConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        private void CreateDataRelationPurchaseCreditNote(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("PurchaseCreditNote",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintPurchaseCreditNote(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("PurchaseCreditNoteConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        private void CreateDataRelationPurchaseCreditNoteTAX(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["AbsEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("PurchaseCreditNoteTax",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintPurchaseCreditNoteTAX(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["AbsEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("PurchaseCreditNoteTaxConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        private void CreateDataRelationEntregaMercancia(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("EntregaMercancia",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        private void CreateConstraintEntregaMercancia(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("EntregaMercanciaConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        public string ResetDataSetPreliminar()
        {
            string error = null;

            try
            {

                dataSet.Clear();

                dataSet.Reset();

                dataSet.AcceptChanges();

                return error;

            }
            catch (Exception e)
            {
                               
                return e.Message;
            }

        }

        protected void CreateDataRelationSalesInvoiceTAX(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["AbsEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("SalesInvoiceTax",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        protected void CreateConstraintSalesInvoiceTAX(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["AbsEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("SalesInvoiceTaxConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        protected void CreateDataRelationSalesInvoice(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["DocEntry"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["DocEntry"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("SalesInvoice",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        protected void CreateConstraintSalesInvoice(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["DocEntry"];
            childColumn = dataSet.Tables[v2].Columns["DocEntry"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("SalesInvoiceConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        public Tuple<int, string> InsertSalesInvoicePreliminar(List<DocumentoCabecera> listaSalesInvoice)
        {
            //LoadDatatable OINV_TEST

            dtOINV = dataSet.Tables["OINV_TEST"];

            int flag = 0;

            string error = null;

            try
            {
                foreach (DocumentoCabecera SalesInvoice in listaSalesInvoice)
                {
                    int DocEntry = dtOINV.Rows.Count;

                    DataRow newRow = dtOINV.NewRow();

                    newRow["DocNum"] = SalesInvoice.DocNum;
                    newRow["DocDate"] = SalesInvoice.DocDate;
                    newRow["TaxDate"] = SalesInvoice.TaxDate;
                    newRow["DocDueDate"] = SalesInvoice.DocDueDate;
                    newRow["Comments"] = SalesInvoice.Comments;
                    newRow["DocType"] = SalesInvoice.DocType;
                    newRow["CANCELED"] = SalesInvoice.Canceled;
                    newRow["UserSign"] = SalesInvoice.UserSign;
                    newRow["UpdateDate"] = SalesInvoice.UpdateDate;
                    newRow["FinncPriod"] = SalesInvoice.FinncPriod;
                    newRow["Address2"] = SalesInvoice.Address2;
                    newRow["InvntSttus"] = SalesInvoice.InvntSttus;
                    newRow["VatSum"] = SalesInvoice.VatSum;
                    newRow["VatSumFC"] = SalesInvoice.VatSumFC;
                    newRow["VatSumSy"] = SalesInvoice.VatSumSy1;
                    newRow["DocCur"] = SalesInvoice.DocCurr;
                    newRow["ObjType"] = SalesInvoice.ObjType;
                    newRow["CardCode"] = SalesInvoice.CardCode;
                    newRow["CardName"] = SalesInvoice.CardName;
                    newRow["JrnlMemo"] = SalesInvoice.JrnlMemo;
                    newRow["LicTradNum"] = SalesInvoice.LicTradNum;
                    newRow["TransId"] = SalesInvoice.TransId;
                    newRow["VatPaid"] = SalesInvoice.VatPaid;
                    newRow["VatPaidFC"] = SalesInvoice.VatPaidFC;
                    newRow["VatPaidSys"] = SalesInvoice.VatPaidSys;
                    newRow["PaidToDate"] = SalesInvoice.PaidToDate;
                    newRow["PaidSum"] = SalesInvoice.PaidSum;
                    newRow["PaidSumFc"] = SalesInvoice.PaidSumFc;
                    newRow["PaidSumSc"] = SalesInvoice.PaidSumSc;
                    newRow["WTApplied"] = SalesInvoice.WTApplied;
                    newRow["WTAppliedF"] = SalesInvoice.WTAppliedF;
                    newRow["WTAppliedS"] = SalesInvoice.WTAppliedS;
                    newRow["WTSum"] = SalesInvoice.WTSum;
                    newRow["WTSumFC"] = SalesInvoice.WTSumFC;
                    newRow["WTSumSC"] = SalesInvoice.WTSumSC;
                    newRow["SysRate"] = SalesInvoice.SysRate;
                    newRow["DocRate"] = SalesInvoice.DocRate;
                    newRow["CtlAccount"] = SalesInvoice.CtlAccount;
                    newRow["BaseAmnt"] = SalesInvoice.BaseAmnt;
                    newRow["BaseAmntFC"] = SalesInvoice.BaseAmntFC;
                    newRow["DocTotal"] = SalesInvoice.DocTotal;
                    newRow["DocTotalFC"] = SalesInvoice.DocTotalFC;
                    newRow["DocTotalSy"] = SalesInvoice.DocTotalSy;
                    newRow["DocSubType"] = SalesInvoice.DocSubType;
                    newRow["NumAtCard"] = SalesInvoice.NumAtCard;
                    newRow["U_IDA_NumControl"] = SalesInvoice.NumControl;
                    newRow["U_IDA_TipoTrans"] = SalesInvoice.TipoTrans;
                    newRow["Max1099"] = SalesInvoice.Max1099;

                    dtOINV.Rows.Add(newRow);

                    if (dtOINV.Rows.Contains(DocEntry + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtOINV.AcceptChanges();
                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }


        }

        public Tuple<int, string> InsertTaxHoldingPreliminar(List<TablaRetencionImpuesto> listTablaRetenciones)
        {
            //LoadDatatable INV5_TEST

            dtINV5 = dataSet.Tables["INV5_TEST"];

            int flag = 0;

            string error = null;

            int DocEntry = 0;

            try
            {
                foreach (TablaRetencionImpuesto TablaRetenciones in listTablaRetenciones)
                {
                    DocEntry = TablaRetenciones.AbsEntry;

                    DataRow newRow = dtINV5.NewRow();

                    newRow["AbsEntry"] = TablaRetenciones.AbsEntry;
                    newRow["WTCode"] = TablaRetenciones.WTCode;
                    newRow["Type"] = TablaRetenciones.Type;
                    newRow["Category"] = TablaRetenciones.Category;
                    newRow["BaseType"] = TablaRetenciones.BaseType;
                    newRow["Rate"] = TablaRetenciones.Rate;
                    newRow["TaxbleAmnt"] = TablaRetenciones.TaxbleAmnt;
                    newRow["TaxbleAmntFC"] = TablaRetenciones.TaxbleAmntFC;
                    newRow["TaxbleAmntSC"] = TablaRetenciones.TaxbleAmntSC;
                    newRow["WTAmnt"] = TablaRetenciones.WTAmnt;
                    newRow["WTAmntFC"] = TablaRetenciones.WTAmntFC;
                    newRow["WTAmntSC"] = TablaRetenciones.WTAmntSC;
                    newRow["BaseAmnt"] = TablaRetenciones.BaseAmnt;
                    newRow["BaseAmntFC"] = TablaRetenciones.BaseAmntFC;
                    newRow["BaseAmntSC"] = TablaRetenciones.BaseAmntSC;
                    newRow["LineNum"] = TablaRetenciones.LineNum;
                    newRow["ObjType"] = TablaRetenciones.ObjType;
                    newRow["Status"] = TablaRetenciones.Status;
                    newRow["Account"] = TablaRetenciones.Account;

                    dtINV5.Rows.Add(newRow);

                    dtINV5.AcceptChanges();
                }

                DataRow[] selected = dtINV5.Select("AbsEntry = " + DocEntry);                

                flag = selected.Count();

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(flag, e.Message);
            }

        }

        protected void CreateDataRelationJournalEntry(string v1, string v2)
        {
            // Get the DataColumn objects from two DataTable objects
            // in a DataSet. Code to get the DataSet not shown here.
            DataColumn parentColumn = dataSet
                .Tables[v1].Columns["TransId"];
            DataColumn childColumn =
                dataSet.Tables[v2].Columns["TransId"];
            // Create DataRelation.
            DataRelation relCustOrder;
            relCustOrder = new DataRelation("JournalEntry",
                parentColumn, childColumn);
            // Add the relation to the DataSet.
            dataSet.Relations.Add(relCustOrder);
        }

        protected void CreateConstraintJournalEntry(string v1, string v2)
        {
            // Declare parent column and child column variables.
            DataColumn parentColumn;
            DataColumn childColumn;
            ForeignKeyConstraint foreignKeyConstraint;

            // Set parent and child column variables.
            parentColumn = dataSet.Tables[v1].Columns["TransId"];
            childColumn = dataSet.Tables[v2].Columns["TransId"];
            foreignKeyConstraint = new ForeignKeyConstraint
               ("JournalEntryConstraint", parentColumn, childColumn);

            // Set null values when a value is deleted.
            foreignKeyConstraint.DeleteRule = Rule.None;
            foreignKeyConstraint.UpdateRule = Rule.Cascade;
            foreignKeyConstraint.AcceptRejectRule = AcceptRejectRule.None;


            // Add the constraint, and set EnforceConstraints to true.
            dataSet.Tables[v2].Constraints.Add(foreignKeyConstraint);
            dataSet.EnforceConstraints = true;
        }

        public Tuple<int, string> GetBaseRef(string objType)
        {
            int BaseRef = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetBaseRef";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ObjType", objType);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            BaseRef = Convert.ToInt32(reader["BaseRef"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(BaseRef, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(BaseRef, e.Message);

            }
        }

        public Tuple<int, string> DeleteOldRecord(List<ArticuloDetalle> listArticuloDetalleOld)
        {
            int i = 0;

            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ArticuloDetalle SalesInvoice in listArticuloDetalleOld)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "DeleteOldRecord";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransSeq", SalesInvoice.TransSeq);
                            cmd.Parameters.AddWithValue("@TransType", SalesInvoice.TransType);
                            cmd.Parameters.AddWithValue("@CreatedBy", SalesInvoice.CreatedBy);
                            cmd.Parameters.AddWithValue("@BASE_REF", SalesInvoice.BASE_REF);
                            cmd.Parameters.AddWithValue("@DocLineNum", SalesInvoice.DocLineNum);
                            cmd.Parameters.AddWithValue("@ItemCode", SalesInvoice.ItemCode);
                            cmd.Parameters.AddWithValue("@Dscription", SalesInvoice.Dscription);
                            cmd.Parameters.AddWithValue("@InQty", SalesInvoice.InQty);
                            cmd.Parameters.AddWithValue("@OutQty", SalesInvoice.OutQty);
                            cmd.Parameters.AddWithValue("@Price", SalesInvoice.Price);
                            cmd.Parameters.AddWithValue("@Currency", SalesInvoice.Currency);
                            cmd.Parameters.AddWithValue("@CalcPrice", SalesInvoice.CalcPrice);
                            cmd.Parameters.AddWithValue("@OpenQty", SalesInvoice.OpenQty);
                            cmd.Parameters.AddWithValue("@Balance", SalesInvoice.Balance);
                            cmd.Parameters.AddWithValue("@CreateDate", SalesInvoice.CreateDate);
                            cmd.Parameters.AddWithValue("@TransValue", SalesInvoice.TransValue);
                            cmd.Parameters.AddWithValue("@InvntAct", SalesInvoice.InvntAct);
                            cmd.Parameters.AddWithValue("@OpenValue", SalesInvoice.OpenValue);
                            cmd.Parameters.AddWithValue("@CostMethod", SalesInvoice.CostMethod);
                            cmd.Parameters.AddWithValue("@Type", SalesInvoice.Type);
                            cmd.Parameters.AddWithValue("@DocDate", SalesInvoice.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", SalesInvoice.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", SalesInvoice.DocDueDate);
                            cmd.Parameters.AddWithValue("@Comments", SalesInvoice.Comments);
                            cmd.Parameters.AddWithValue("@UserSign", SalesInvoice.UserSign);
                            cmd.Parameters.AddWithValue("@CardCode", SalesInvoice.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", SalesInvoice.CardName);
                            cmd.Parameters.AddWithValue("@JrnlMemo", SalesInvoice.JrnlMemo);
                            cmd.Parameters.AddWithValue("@SysRate", SalesInvoice.SysRate);
                            cmd.Parameters.AddWithValue("@Rate", SalesInvoice.Rate);


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

                return Tuple.Create(i, e.Message);
            }
        }
        public Tuple<int, string> DeleteOINM(int docNumDeleted)
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
                        cmd.CommandText = "DeleteOINM";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@DocEntry", docNumDeleted);

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
        public Tuple<string, string> FindSalesCostAct(string itemCode)
        {
            string SalesCostAct = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindSalesCostAct";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            SalesCostAct = reader["SaleCostAc"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(SalesCostAct, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(SalesCostAct, e.Message);

            }
        }

        public Tuple<int, string> UpdateOINMSum(int transSeq, decimal quantity, decimal openValue)
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
                        cmd.CommandText = "UpdateOINMSum";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransSeq", transSeq);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@OpenValue", openValue);

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

        public Tuple<int, string> UpdateOINMRes(int transSeq, decimal quantity, decimal openValue)
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
                        cmd.CommandText = "UpdateOINMRes";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransSeq", transSeq);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);
                        cmd.Parameters.AddWithValue("@OpenValue", openValue);

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

        public Tuple<int, string> UpdateOINMResPreliminar(int transSeq, decimal quantity, decimal openValue)
        {
            int flag = 0;

            string error = null;

            try
            {
                var row = dtOINM.AsEnumerable().Where(x => x.Field<int>("TransSeq") == transSeq).First();

                row["OpenQty"] = (Convert.ToDecimal(row["OpenQty"])-quantity);

                row["OpenValue"] = (Convert.ToDecimal(row["OpenValue"]) - openValue);

                dtOINM.AcceptChanges();

                flag = 1;

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }
        }




        public Tuple<int, string> DeleteJournalEntryLines(int transId)
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
                        cmd.CommandText = "DeleteJournalEntryLines";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transId);

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

        public Tuple<int, string> DeleteJournalEntry(int transId)
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
                        cmd.CommandText = "DeleteJournalEntry";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TransId", transId);

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


        public Tuple<List<ArticuloDetalle>, string> FindFirstItemTransaccion(string itemCode)
        {
            List<ArticuloDetalle> newList = new List<ArticuloDetalle>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindFirstItemTransaccion";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            ArticuloDetalle newItem = new ArticuloDetalle();

                            newItem.TransSeq = Convert.ToInt32(reader["TransSeq"]);
                            newItem.TransType = Convert.ToInt32(reader["TransType"]);
                            newItem.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                            newItem.BASE_REF = reader["BASE_REF"].ToString();
                            newItem.DocLineNum = Convert.ToInt32(reader["DocLineNum"]);
                            newItem.Dscription = reader["Dscription"].ToString();
                            newItem.OpenQty = Convert.ToDecimal(reader["OpenQty"]);
                            newItem.CalcPrice = Convert.ToDecimal(reader["CalcPrice"]);
                            newItem.Rate = Convert.ToDecimal(reader["Rate"]);
                            newItem.SysRate = Convert.ToDecimal(reader["SysRate"]);
                            newItem.Currency = reader["Currency"].ToString();
                            newItem.InvntAct = reader["InvntAct"].ToString();
                            newItem.OpenValue = Convert.ToDecimal(reader["OpenValue"]);
                            newItem.InQty = Convert.ToDecimal(reader["InQty"]);
                            newItem.OutQty = Convert.ToDecimal(reader["OutQty"]);
                            newItem.Price = Convert.ToDecimal(reader["Price"]);
                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.Balance = Convert.ToDecimal(reader["Balance"]);
                            newItem.TransValue = Convert.ToDecimal(reader["TransValue"]);
                            newItem.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                            newItem.CostMethod = Convert.ToChar(reader["CostMethod"]);
                            newItem.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                            newItem.DocDate = Convert.ToDateTime(reader["DocDate"]);
                            newItem.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                            newItem.Comments = reader["Comments"].ToString();
                            newItem.CardCode = reader["CardCode"].ToString();
                            newItem.CardName = reader["CardName"].ToString();
                            newItem.JrnlMemo = reader["JrnlMemo"].ToString();
                            newItem.Type = Convert.ToChar(reader["Type"]);
                            newItem.UserSign = Convert.ToInt32(reader["UserSign"]);

                            newList.Add(newItem);

                        }

                        reader.Close();
                    }
                }

                Connection.Close();

                return Tuple.Create(newList, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newList, e.Message);
            }
        }

        public Tuple<List<ArticuloDetalle>, string> FindFirstItemTransaccionPreliminar(string itemCode)
        {
            List<ArticuloDetalle> newList = new List<ArticuloDetalle>();

            string error = null;
            
            dtOINM = dataSet.Tables["OINM_TEST"];
            
            try
            {
                var reader = dtOINM.AsEnumerable().Where(x => x.Field<string>("Type") == "E" && x.Field<string>("ItemCode") == itemCode && x.Field<decimal>("Openqty") > 0).OrderBy(x => x.Field<int>("TransSeq")).First();

                ArticuloDetalle newItem = new ArticuloDetalle();

                newItem.TransSeq = Convert.ToInt32(reader["TransSeq"]);
                newItem.TransType = Convert.ToInt32(reader["TransType"]);
                newItem.CreatedBy = Convert.ToInt32(reader["CreatedBy"]);
                newItem.BASE_REF = reader["BASE_REF"].ToString();
                newItem.DocLineNum = Convert.ToInt32(reader["DocLineNum"]);
                newItem.Dscription = reader["Dscription"].ToString();
                newItem.OpenQty = Convert.ToInt32(reader["OpenQty"]);
                newItem.CalcPrice = Convert.ToDecimal(reader["CalcPrice"]);
                newItem.Rate = Convert.ToDecimal(reader["Rate"]);
                newItem.SysRate = Convert.ToDecimal(reader["SysRate"]);
                newItem.Currency = reader["Currency"].ToString();
                newItem.InvntAct = reader["InvntAct"].ToString();
                newItem.OpenValue = Convert.ToDecimal(reader["OpenValue"]);
                newItem.InQty = Convert.ToInt32(reader["InQty"]);
                newItem.OutQty = Convert.ToInt32(reader["OutQty"]);
                newItem.Price = Convert.ToDecimal(reader["Price"]);
                newItem.ItemCode = reader["ItemCode"].ToString();
                newItem.Balance = Convert.ToDecimal(reader["Balance"]);
                newItem.TransValue = Convert.ToDecimal(reader["TransValue"]);
                newItem.CreateDate = Convert.ToDateTime(reader["CreateDate"]);
                newItem.CostMethod = Convert.ToChar(reader["CostMethod"]);
                newItem.TaxDate = Convert.ToDateTime(reader["TaxDate"]);
                newItem.DocDate = Convert.ToDateTime(reader["DocDate"]);
                newItem.DocDueDate = Convert.ToDateTime(reader["DocDueDate"]);
                newItem.Comments = reader["Comments"].ToString();
                newItem.CardCode = reader["CardCode"].ToString();
                newItem.CardName = reader["CardName"].ToString();
                newItem.JrnlMemo = reader["JrnlMemo"].ToString();
                newItem.Type = Convert.ToChar(reader["Type"]);
                newItem.UserSign = Convert.ToInt32(reader["UserSign"]);

                newList.Add(newItem);

                return Tuple.Create(newList, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(newList, e.Message);
            }
        }
        public Tuple<string, string> FindWTName(string wtCode)
        {
            string WTName = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindWTName";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@WTCode", wtCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            WTName = reader["WTName"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(WTName,error);

            }
            catch (Exception e)
            {
               
                Connection.Close();

                return Tuple.Create(WTName, e.Message);

            }
        }

        public Tuple<List<Cuenta>, string> ConsultaCuentasAsociadas()
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
                        cmd.CommandText = "FindAccountAsociada";

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

                return Tuple.Create(newListaCuenta, error);

            }
            catch (Exception e)
            {
                Connection.Close();

                return Tuple.Create(newListaCuenta, e.Message);

            }
        }

        public decimal FindTaxRate(string vatGroup)
        {
            decimal Rate = 0;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindTaxRate";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Code", vatGroup);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Rate = Convert.ToDecimal(reader["Rate"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Rate;

            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message, "Tax Code", MessageBoxButton.OK, MessageBoxImage.Error);

                Connection.Close();

                return Rate;

            }
        }

        public Tuple<decimal, string> CalculateBalanceItem(string itemCode)
        {
            decimal balance = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "CalculateBalanceItemE";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            balance = Convert.ToDecimal(reader["Value"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(balance, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-ES");
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");

                return Tuple.Create(balance, e.Message);



            }
        }

        public Tuple<decimal, string> CalculateBalanceItemPreliminar(string itemCode, bool sw)
        {
            decimal balance = 0;

            string error = null;

            try
            {
                if (sw == true)
                {
                    dtOINM = dataSet.Tables["OINM_TEST"];

                    balance = dtOINM.AsEnumerable()
               .Where(r => r.Field<string>("ItemCode") == itemCode && r.Field<string>("Type") == "E")
               .Sum(r => r.Field<decimal>("TransValue"));
                }
                else
                {
                    balance = dtOINM.AsEnumerable()
               .Where(r => r.Field<string>("ItemCode") == itemCode && r.Field<string>("Type") == "E")
               .Sum(r => r.Field<decimal>("TransValue"));
                }

                return Tuple.Create(balance, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(balance, e.Message);

            }
        }

        public Tuple<decimal, string> FindQuantityItem(string itemCode)
        {
            decimal Quantity = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindQuantityItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Quantity = Convert.ToDecimal(reader["Quantity"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(Quantity, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(Quantity, e.Message);

            }
        }

        public Tuple<string, string> GetCurrencyName(string docCurr)
        {
            string CurrName = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetCurrencyName";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CurrCode", docCurr);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            CurrName = reader["CurrName"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(CurrName,error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(CurrName, e.Message);

            }
        }

        public Tuple<int, string> InsertOINM(List<ArticuloDetalle> listItemPurchase)
        {
            int flag = 0;            

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (ArticuloDetalle ItemPurchaseLines in listItemPurchase)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertOINM";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransType", ItemPurchaseLines.TransType);
                            cmd.Parameters.AddWithValue("@CreatedBy", ItemPurchaseLines.CreatedBy);
                            cmd.Parameters.AddWithValue("@BASE_REF", ItemPurchaseLines.BASE_REF);
                            cmd.Parameters.AddWithValue("@DocLineNum", ItemPurchaseLines.DocLineNum);
                            cmd.Parameters.AddWithValue("@DocDate", ItemPurchaseLines.DocDate);
                            cmd.Parameters.AddWithValue("@TaxDate", ItemPurchaseLines.TaxDate);
                            cmd.Parameters.AddWithValue("@DocDueDate", ItemPurchaseLines.DocDueDate);
                            cmd.Parameters.AddWithValue("@CardCode", ItemPurchaseLines.CardCode);
                            cmd.Parameters.AddWithValue("@CardName", ItemPurchaseLines.CardName);
                            cmd.Parameters.AddWithValue("@Comments", ItemPurchaseLines.Comments);
                            cmd.Parameters.AddWithValue("@JrnlMemo", ItemPurchaseLines.JrnlMemo);
                            cmd.Parameters.AddWithValue("@ItemCode", ItemPurchaseLines.ItemCode);
                            cmd.Parameters.AddWithValue("@Dscription", ItemPurchaseLines.Dscription);
                            cmd.Parameters.AddWithValue("@InQty", ItemPurchaseLines.InQty);
                            cmd.Parameters.AddWithValue("@OutQty", ItemPurchaseLines.OutQty);
                            cmd.Parameters.AddWithValue("@Price", ItemPurchaseLines.Price);
                            cmd.Parameters.AddWithValue("@Currency", ItemPurchaseLines.Currency);
                            cmd.Parameters.AddWithValue("@Rate", ItemPurchaseLines.Rate);
                            cmd.Parameters.AddWithValue("@SysRate", ItemPurchaseLines.SysRate);
                            cmd.Parameters.AddWithValue("@Type", ItemPurchaseLines.Type);
                            cmd.Parameters.AddWithValue("@UserSign", ItemPurchaseLines.UserSign);
                            cmd.Parameters.AddWithValue("@CalcPrice", ItemPurchaseLines.CalcPrice);
                            cmd.Parameters.AddWithValue("@OpenQty", ItemPurchaseLines.OpenQty);
                            cmd.Parameters.AddWithValue("@CreateDate", ItemPurchaseLines.CreateDate);
                            cmd.Parameters.AddWithValue("@CostMethod", ItemPurchaseLines.CostMethod);
                            cmd.Parameters.AddWithValue("@TransValue", ItemPurchaseLines.TransValue);
                            cmd.Parameters.AddWithValue("@OpenValue", ItemPurchaseLines.OpenValue);
                            cmd.Parameters.AddWithValue("@InvntAct", ItemPurchaseLines.InvntAct);
                            cmd.Parameters.AddWithValue("@Balance", ItemPurchaseLines.Balance);
                            
                            flag = cmd.ExecuteNonQuery();                           
                        }
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

        public Tuple<int, string> InsertOINMPreliminar(List<ArticuloDetalle> listItemPurchase)
        {
            int flag = 0;

            string error = null;

            try
            {
                foreach (ArticuloDetalle ItemPurchaseLines in listItemPurchase)
                {
                    int TransSeq = dtOINM.Rows.Count;

                    DataRow newRow = dtOINM.NewRow();

                    newRow["TransType"] = ItemPurchaseLines.TransType;
                    newRow["CreatedBy"] = ItemPurchaseLines.CreatedBy;
                    newRow["BASE_REF"] = ItemPurchaseLines.BASE_REF;
                    newRow["DocLineNum"] = ItemPurchaseLines.DocLineNum;
                    newRow["DocDate"] = ItemPurchaseLines.DocDate;
                    newRow["TaxDate"] = ItemPurchaseLines.TaxDate;
                    newRow["DocDueDate"] = ItemPurchaseLines.DocDueDate;
                    newRow["CardCode"] = ItemPurchaseLines.CardCode;
                    newRow["CardName"] = ItemPurchaseLines.CardName;
                    newRow["Comments"] = ItemPurchaseLines.Comments;
                    newRow["JrnlMemo"] = ItemPurchaseLines.JrnlMemo;
                    newRow["ItemCode"] = ItemPurchaseLines.ItemCode;
                    newRow["Dscription"] = ItemPurchaseLines.Dscription;
                    newRow["InQty"] = ItemPurchaseLines.InQty;
                    newRow["OutQty"] = ItemPurchaseLines.OutQty;
                    newRow["Price"] = ItemPurchaseLines.Price;
                    newRow["Currency"] = ItemPurchaseLines.Currency;
                    newRow["Rate"] = ItemPurchaseLines.Rate;
                    newRow["SysRate"] = ItemPurchaseLines.SysRate;
                    newRow["Type"] = ItemPurchaseLines.Type;
                    newRow["UserSign"] = ItemPurchaseLines.UserSign;
                    newRow["CalcPrice"] = ItemPurchaseLines.CalcPrice;
                    newRow["OpenQty"] = ItemPurchaseLines.OpenQty;
                    newRow["CreateDate"] = ItemPurchaseLines.CreateDate;
                    newRow["CostMethod"] = ItemPurchaseLines.CostMethod;
                    newRow["TransValue"] = ItemPurchaseLines.TransValue;
                    newRow["OpenValue"] = ItemPurchaseLines.OpenValue;
                    newRow["InvntAct"] = ItemPurchaseLines.InvntAct;
                    newRow["Balance"] = ItemPurchaseLines.Balance;

                    dtOINM.Rows.Add(newRow);

                    if (dtOINM.Rows.Contains(TransSeq + 1) == true)
                    {
                        flag = 1;
                    }
                    else
                    {
                        flag = 0;
                    }

                    dtOINM.AcceptChanges();
                }

                return Tuple.Create(flag, error);

            }
            catch (Exception e)
            {

                return Tuple.Create(flag, e.Message);
            }
        }

        public Tuple<int, string> GetLastTransSeq()
        {
            int TransSeq = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetLastTransSeq";

                        cmd.CommandType = CommandType.StoredProcedure;

                       
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            TransSeq = Convert.ToInt32(reader["TransSeq"]);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(TransSeq, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(TransSeq, e.Message);

            }
        }

        public Tuple<int, string> GetLastTransSeqPreliminar()
        {
            int TransSeq = 0;

            string error = null;

            try
            {

                var reader = dtOINM.AsEnumerable().OrderByDescending(x => x.Field<int>("TransSeq")).First();

                TransSeq=Convert.ToInt32(reader["TransSeq"]);

                return Tuple.Create(TransSeq, error);

            }
            catch (Exception e)
            {

               
                return Tuple.Create(TransSeq, e.Message);

            }
        }

        public Tuple<int, string> DeleteNewRecord(List<int> listTransSeq)
        {
            int i = 0;

            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (int TransSeq in listTransSeq)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "DeleteNewRecord";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@TransSeq", TransSeq);

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

                return Tuple.Create(i, e.Message);
            }
        }

        public Tuple<string, string> GetAccountSaleCostAc(string itemCode)
        {
            string SaleCostAc = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetAccountSaleCostAc";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            SaleCostAc = reader["SaleCostAc"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(SaleCostAc, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(SaleCostAc, e.Message);

            }
        }

        public Tuple<string, string> GetAccountTaxPurchase(string taxCode)
        {
            string Account = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindTaxAccountOPCH";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TaxCode", taxCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Account = reader["PurchTax"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(Account,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(Account, e.Message);

            }
        }

        public Tuple<string, string> GetAccountTaxSales(string taxCode)
        {
            string Account = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindTaxAccountSales";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TaxCode", taxCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            Account = reader["SalesTax"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(Account, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(Account, e.Message);

            }
        }

      

        public Tuple<int, string> UpdateDebitItem(string itemCode, decimal stockValue, decimal stockValueFC, decimal stockValueSC, decimal quantity)
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
                        cmd.CommandText = "UpdateDebitItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                        cmd.Parameters.AddWithValue("@StockValue", stockValue);
                        cmd.Parameters.AddWithValue("@StockValueFC", stockValueFC);
                        cmd.Parameters.AddWithValue("@StockValueSC", stockValueSC);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

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

        public Tuple<int, string> UpdateCreditItem(string itemCode, decimal stockValue, decimal stockValueFC, decimal stockValueSC, decimal quantity)
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
                        cmd.CommandText = "UpdateCreditItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);
                        cmd.Parameters.AddWithValue("@StockValue", stockValue);
                        cmd.Parameters.AddWithValue("@StockValueFC", stockValueFC);
                        cmd.Parameters.AddWithValue("@StockValueSC", stockValueSC);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

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

        public Tuple<string, string> GetAccountTransferAc(string itemCode)
        {
            string TransferAc = null;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "GetAccountTransferAc";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            TransferAc = reader["TransferAc"].ToString();

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(TransferAc, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(TransferAc, e.Message);

            }
        }
    }
}
