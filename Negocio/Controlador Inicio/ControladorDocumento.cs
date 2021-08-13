using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Datos;
using Entidades;
using static Negocio.EnumTipoTrans;

namespace Negocio
{
    public class ControladorDocumento : Negocios
    {
        ModeloDocumento cn = new ModeloDocumento();

        public Tuple<string, string> FindBeneficioDiferenciaConversion()
        {
            return cn.FindBeneficioDiferenciaConversion();
        }

        public char TraduceChar(bool isTax)
        {
            char value = 'N';

            if (isTax == true)
            {
                value = 'Y';

            }
            else if (isTax == false)
            {
                value = 'N';
            }

            return value;
        }

        public bool TraduceChar(char isTax)
        {
            bool value = false;

            if (isTax == 'Y')
            {
                value = true;

            }
            else if (isTax == 'N')
            {
                value = false;
            }

            return value;
        }

        public Tuple<int, string> ReduceSaldoAdeudadoSN(string cardCode, decimal docTransId, int line_ID)
        {
            return cn.ReduceSaldoAdeudado(cardCode, docTransId, line_ID);
        }

        public Tuple<int, string> ReduceSaldoAdeudadoSN(string cardCode, decimal docTransId)
        {
            return cn.ReduceSaldoAdeudado(cardCode, docTransId);
        }


        public Tuple<string, string> FindPerdidaDiferenciaConversion()
        {
            return cn.FindPerdidaDiferenciaConversion();
        }

        public Tuple<string, string> FindBeneficioDiferenciaTipoCambio()
        {
            return cn.FindBeneficioDiferenciaTipoCambio();
        }

        public Tuple<string, string> FindCuentaRedondeo()
        {
            return cn.FindCuentaRedondeo();
        }

        public Tuple<string, string> FindPerdidaDiferenciaTipoCambio()
        {
            return cn.FindPerdidaDiferenciaTipoCambio();
        }

        public string CreateDataSetPreliminar()
        {
            return cn.CreateDataSetPreliminar();
        }

        public string ResetDataSetPreliminar()
        {
            return cn.ResetDataSetPreliminar();
        }
        public Tuple<int, string> UpdateOINMRes(int transSeq, decimal quantity, decimal openValue)
        {
            return cn.UpdateOINMRes(transSeq, quantity, openValue);
        }

        public Tuple<int, string> UpdateOINMResPreliminar(int transSeq, decimal quantity, decimal openValue)
        {
            return cn.UpdateOINMResPreliminar(transSeq, quantity, openValue);
        }

        public Tuple<int, string> UpdateOINMSum(int transSeq, decimal quantity, decimal openValue)
        {
            return cn.UpdateOINMSum(transSeq, quantity, openValue);
        }
        public Tuple<int, string> DeleteOldRecord(List<ArticuloDetalle> listArticuloDetalleOld)
        {
            return cn.DeleteOldRecord(listArticuloDetalleOld);
        }

        public Tuple<int,string> GetBaseRef(string objType)
        {
            return cn.GetBaseRef(objType);
        }

        public Tuple<string, string> FindSalesCostAct(string itemCode)
        {
            return cn.FindSalesCostAct(itemCode);
        }

        public Tuple<int, string> DeleteOINM(int docNumDeleted)
        {
            return cn.DeleteOINM(docNumDeleted);
        }


        public Tuple<int, string> DeleteJournalEntry(int transId)
        {
            return cn.DeleteJournalEntry(transId);
        }

        public Tuple<int, string> DeleteJournalEntryLines(int transId)
        {
            return cn.DeleteJournalEntryLines(transId);
        }
        public Tuple<List<ArticuloDetalle>, string> FindFirstItemTransaccion(string itemCode)
        {
            return cn.FindFirstItemTransaccion(itemCode);
        }

        public Tuple<List<ArticuloDetalle>, string> FindFirstItemTransaccionPreliminar(string itemCode)
        {
            return cn.FindFirstItemTransaccionPreliminar(itemCode);
        }
        public Tuple<decimal, string> CalculateBalanceItem(string itemCode)
        {
            return cn.CalculateBalanceItem(itemCode);
        }

        public Tuple<decimal, string> CalculateBalanceItemPreliminar(string itemCode, bool sw)
        {
            return cn.CalculateBalanceItemPreliminar(itemCode,sw);
        }

        public Tuple<decimal, string> FindQuantityItem(string ItemCode)
        {
            return cn.FindQuantityItem(ItemCode);
        }

        public Tuple<List<Cuenta>, string> ConsultaCuentasAsociadas()
        {
            return cn.ConsultaCuentasAsociadas();
        }
        public DataTable TraduceSujetoRetencion(DataTable table)
        {
            DataTable newTable = table;

            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn column in table.Columns)
                {

                    if (column.ToString() == "WtLiable" && row[column].ToString() == "Y")
                    {
                        row[column] = SujetoImpuesto.SI.ToString();

                    }
                    else

                    if (column.ToString() == "WtLiable" && row[column].ToString() == "N")
                    {
                        row[column] = SujetoImpuesto.NO.ToString();

                    }


                }
            }
            return newTable;
        }

        public DataTable ChangeTypeColumn(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("AcctName");

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public DataTable ChangeTypeColumnString(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public void UpdateItemDebit(List<DocumentoDetalle> listDocumentoDetalle)
        {
            decimal StockValueSC = 0;

            decimal StockValue = 0;

            decimal StockValueFC = 0;

            decimal quantity = 0;

            string itemCode = null;

            foreach (DocumentoDetalle row in listDocumentoDetalle)
            {


                if (String.IsNullOrWhiteSpace(row.ItemCode) == false)
                {
                    itemCode = row.ItemCode;
                }

                if (String.IsNullOrWhiteSpace(row.LineTotal.ToString()) == false)
                {                  

                    StockValue = ConvertDecimalTwoPlaces(row.LineTotal);
                }

                if (String.IsNullOrWhiteSpace(row.TotalSumSy.ToString()) == false)
                {                    

                    StockValueSC = ConvertDecimalTwoPlaces(row.TotalSumSy);
                }

                if (String.IsNullOrWhiteSpace(row.TotalFrgn.ToString()) == false)
                {
                    
                    StockValueFC = ConvertDecimalTwoPlaces(row.TotalFrgn);
                }

                if (String.IsNullOrWhiteSpace(row.InvQty.ToString()) == false)
                {                    
                    quantity = ConvertDecimalTwoPlaces(row.InvQty);
                }


                var result = cn.UpdateDebitItem(itemCode, StockValue, StockValueFC, StockValueSC, quantity);

                if (result.Item1 != 1)
                {
                    MessageBox.Show("Error al modificar el articulo " + itemCode + " " + result.Item2, "Item", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                StockValue = 0;

                StockValueFC = 0;

                StockValueSC = 0;

                itemCode = null;

                quantity = 0;

            }
        }

        public Tuple<bool,string> VerifyCuentasNoAsociadas(string acctCode)
        {
            return cn.VerifyCuentasNoAsociadas(acctCode);
        }

        public void UpdateItemCredit(DataTable dt, string mainCurrency=null)
        {
            decimal StockValueSC = 0;

            decimal StockValue = 0;

            decimal StockValueFC = 0;

            decimal quantity = 0;

            string itemCode = null;

            foreach (DataRow row in dt.Rows)
            {
                 
                    
                if (String.IsNullOrWhiteSpace(row["ItemCode"].ToString()) == false)
                {
                    itemCode = row["ItemCode"].ToString();
                }

                if (String.IsNullOrWhiteSpace(row["OpenQty"].ToString()) == false)
                {
                  
                    quantity = ConvertDecimalTwoPlaces(row["OpenQty"]);
                }

                if (String.IsNullOrWhiteSpace(row["CalcPrice"].ToString()) == false)
                {
                    
                    StockValue = ConvertDecimalTwoPlaces(row["CalcPrice"]);
                }

                if (String.IsNullOrWhiteSpace(row["Currency"].ToString()) == false)
                {
                    if (row["Currency"].ToString() == mainCurrency)
                    {
                        StockValueFC = 0;
                        StockValueSC = StockValue / ConvertDecimalTwoPlaces(row["SysRate"]);
                    }
                    else
                    {
                        StockValueFC = StockValue * ConvertDecimalTwoPlaces(row["Rate"]);
                        StockValueSC = StockValue / ConvertDecimalTwoPlaces(row["SysRate"]);
                    }

                    
                }

                var result = cn.UpdateCreditItem(itemCode, StockValue, StockValueFC, StockValueSC, quantity);

                if (result.Item1 != 1)
                {
                    MessageBox.Show("Error al modificar el articulo " + itemCode + " " + result.Item2, "Item", MessageBoxButton.OK, MessageBoxImage.Error);
                }


                StockValue = 0;

                StockValueFC = 0;

                StockValueSC = 0;

                itemCode = null;

                quantity = 0;

            }
        }

        public char GetDocType(string docTypeString)
        {
            char docType = 'S';

            if (docTypeString == DocType.Servicio.ToString())
            {
                docType = (char)DocType.Servicio;
            }
            else if (docTypeString == DocType.Articulo.ToString())
            {
                docType = (char)DocType.Articulo;
            }

            return docType;
        }

        public string GetDocType(char docType)
        {
            string docTypeString = null;

            if (docType == (char)DocType.Servicio)
            {
                docTypeString = DocType.Servicio.ToString();
            }
            else if (docType == (char)DocType.Articulo)
            {
                docTypeString = DocType.Articulo.ToString();
            }

            return docTypeString;
        }

        public char GetDocStatus(string docStatusString)
        {
            char docStatus = 'O';

            if (docStatusString == DocStatus.Abierto.ToString())
            {
                docStatus = (char)DocStatus.Abierto;
            }
            else if (docStatusString == DocStatus.Cerrado.ToString())
            {
                docStatus = (char)DocStatus.Cerrado;
            }

            return docStatus;
        }

        public Tuple<int, string> InsertOINM(List<ArticuloDetalle> listItemPurchase)
        {
            return cn.InsertOINM(listItemPurchase);
        }

        public Tuple<int, string> InsertOINMPreliminar(List<ArticuloDetalle> listItemPurchase)
        {
            return cn.InsertOINMPreliminar(listItemPurchase);
        }

        public string GetDocStatus(char docStatus)
        {
            string docStatusString = null;

            if (docStatus == (char)DocStatus.Abierto)
            {
                docStatusString = DocStatus.Abierto.ToString();
            }
            else if (docStatus == (char)DocStatus.Cerrado)
            {
                docStatusString = DocStatus.Cerrado.ToString();
            }

            return docStatusString;
        }

        public decimal CalculateVatSum(DataTable dt)
        {
            decimal vatSum = 0;

            decimal vatSumVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "VatSum")
                    {
                        if (String.IsNullOrWhiteSpace(row["VatSum"].ToString()) == false)
                        {

                            str = regex.Replace(row["VatSum"].ToString(), String.Empty);

                            vatSumVar = ConvertDecimalTwoPlaces(str);

                            vatSum = vatSumVar + vatSum;
                        }
                    }

                }

            }

            return vatSum;
        }

        public decimal CalculateVatSumFC(DataTable dt)
        {
            decimal vatSumFC = 0;

            decimal vatSumVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "VatSumFrgn")
                    {
                        if (String.IsNullOrWhiteSpace(row["VatSumFrgn"].ToString()) == false)
                        {

                            str = regex.Replace(row["VatSumFrgn"].ToString(), String.Empty);

                            vatSumVar = ConvertDecimalTwoPlaces(str);

                            vatSumFC = vatSumVar + vatSumFC;
                        }
                    }

                }

            }

            return vatSumFC;
        }

        public decimal CalculateVatSumSy(DataTable dt)
        {
            decimal vatSumSy = 0;

            decimal vatSumVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "VatSumSy")
                    {
                        if (String.IsNullOrWhiteSpace(row["VatSumSy"].ToString()) == false)
                        {

                            str = regex.Replace(row["VatSumSy"].ToString(), String.Empty);

                            vatSumVar = ConvertDecimalTwoPlaces(str);

                            vatSumSy = vatSumVar + vatSumSy;
                        }
                    }

                }

            }

            return vatSumSy;
        }


        public decimal CalculateWTSum(DataTable dt)
        {
            decimal wtSumVar = 0;

            decimal wtSum = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "WTAmnt")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmnt"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmnt"].ToString(), String.Empty);

                            wtSumVar = ConvertDecimalTwoPlaces(str);

                            wtSum = wtSum + wtSumVar;
                        }
                    }

                }

            }

            return wtSum;
        }

        public decimal CalculateWTSumTypeInvoice(DataTable dt)
        {            
            decimal wtSum = 0;           

            foreach (DataRow row in dt.Rows)
            {
                if (row["Category"].ToString() =="Factura")
                {
                    wtSum = wtSum + ConvertDecimalTwoPlaces(row["WTAmnt"]);
                }

            }

            return wtSum;
        }

        public decimal CalculateWTSumSCTypeInvoice(DataTable dt)
        {
            decimal wtSumSC = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Category"].ToString() == "Factura")
                {
                    wtSumSC = wtSumSC + ConvertDecimalTwoPlaces(row["WTAmntSC"]);
                }

            }

            return wtSumSC;
        }

        public decimal CalculateWTSumFCTypeInvoice(DataTable dt)
        {
            decimal wtSumFC = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["Category"].ToString() == "Factura")
                {
                    wtSumFC = wtSumFC + ConvertDecimalTwoPlaces(row["WTAmntFC"]);
                }

            }

            return wtSumFC;
        }

        public decimal CalculateWTSumSC(DataTable dt)
        {
            decimal wtSumSCVar = 0;

            decimal wtSumSC = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "WTAmntSC")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmntSC"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmntSC"].ToString(), String.Empty);

                            wtSumSCVar = ConvertDecimalTwoPlaces(str);

                            wtSumSC = wtSumSC + wtSumSCVar;
                        }
                    }

                }

            }

            return wtSumSC;
        }

        public DataTable ChangeTypeColumnRetenciones(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            int i = 0;

            foreach (DataColumn column in dataTable.Columns)
            {
                dtCloned.Columns[i].DataType = typeof(string);

                i++;
            }

            dtCloned.Columns.Add("WTName");

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public decimal CalculateWTSumFC(DataTable dt)
        {
            decimal wtSumFCVar = 0;

            decimal wtSumFC = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "WTAmntFC")
                    {
                        if (String.IsNullOrWhiteSpace(row["WTAmntFC"].ToString()) == false)
                        {

                            str = regex.Replace(row["WTAmntFC"].ToString(), String.Empty);

                            wtSumFCVar = ConvertDecimalTwoPlaces(str);

                            wtSumFC = wtSumFC + wtSumFCVar;
                        }
                    }

                }

            }

            return wtSumFC;
        }


        public decimal CalculateBaseAmnt(DataTable dt)
        {
            decimal lineTotal = 0;

            decimal lineTotalVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "LineTotal")
                    {
                        if (String.IsNullOrWhiteSpace(row["LineTotal"].ToString()) == false)
                        {

                            str = regex.Replace(row["LineTotal"].ToString(), String.Empty);

                            lineTotalVar = ConvertDecimalTwoPlaces(str);

                            lineTotal = lineTotal + lineTotalVar;
                        }
                    }

                }

            }

            return lineTotal;
        }

        public string GetTipoTransInv(char tipoTrans)
        {
            string tipoFact = null;

            if (tipoTrans == (char)TipoTrans.Registro)
            {

                tipoFact = TipoTrans.Registro.ToString();
            }
            else if (tipoTrans == (char)TipoTrans.Interno)
            {
                tipoFact = TipoTrans.Interno.ToString();

            }

            return tipoFact;
        }

        public Tuple<string,string> GetCurrencyName(string docCurr)
        {
            return cn.GetCurrencyName(docCurr);
        }

        public List<Monedas> CreateCurrencyTable(string currCode, string currName)
        {
            List<Monedas> listaMonedas = new List<Monedas>();

            listaMonedas.Add(new Monedas(currCode, currName));

            return listaMonedas;
        }

        public Tuple<string, string> FindWTName(string wtCode)
        {
            return cn.FindWTName(wtCode);
        }

        public decimal CalculateBaseAmntFC(DataTable dt)
        {
            decimal TotalFrgn = 0;

            decimal TotalFrgnVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "TotalFrgn")
                    {
                        if (String.IsNullOrWhiteSpace(row["TotalFrgn"].ToString()) == false)
                        {

                            str = regex.Replace(row["TotalFrgn"].ToString(), String.Empty);

                            TotalFrgnVar = ConvertDecimalTwoPlaces(str);

                            TotalFrgn = TotalFrgn + TotalFrgnVar;
                        }
                    }

                }

            }

            return TotalFrgn;
        }

        public decimal CalculateBaseAmntSC(DataTable dt)
        {
            decimal TotalSumSy = 0;

            decimal TotalSumSyVar = 0;

            string str = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "TotalSumSy")
                    {
                        if (String.IsNullOrWhiteSpace(row["TotalSumSy"].ToString()) == false)
                        {

                            str = regex.Replace(row["TotalSumSy"].ToString(), String.Empty);

                            TotalSumSyVar = ConvertDecimalTwoPlaces(str);

                            TotalSumSy = TotalSumSy + TotalSumSyVar;
                        }
                    }

                }

            }

            return TotalSumSy;
        }

        public decimal FindTaxRate(string vatGroup)
        {
            return cn.FindTaxRate(vatGroup);
        }

        public char CalculaInvStatus(string InvStatus)
        {
            char Status = 'O';

            if (InvStatus == enumInvStatus.Articulo.ToString())
            {
                Status = (char)enumInvStatus.Articulo;
            }
            else if (InvStatus == enumInvStatus.Servicio.ToString())
            {
                Status = (char)enumInvStatus.Servicio;

            }

            return Status;
        }


        public char GetTipoTrans(string tipo)
        {
            char tipoFact = '1';

            if (tipo == TipoTrans.Registro.ToString())
            {
                tipoFact = (char)TipoTrans.Registro;

            }
            else if (tipo == TipoTrans.Interno.ToString())
            {
                tipoFact = (char)TipoTrans.Interno;

            }

            return tipoFact;
        }

        public string GetTipoTransInverse(string tipo)
        {
            string tipoFact = null;

            TipoTrans1 registro = TipoTrans1.Registro;

            TipoTrans1 interno = TipoTrans1.Interno;

            TipoTrans1 anulacion = TipoTrans1.Anulacion;

            if (tipo == registro.GetStringValue())
            {
                tipoFact = registro.ToString();

            }
            else if (tipo == interno.GetStringValue())
            {
                tipoFact = interno.ToString();

            }
            else if (tipo == anulacion.GetStringValue())
            {
                tipoFact = anulacion.ToString();

            }

            return tipoFact;
        }

        public string GetTipoTransNotaCreditoInverse(string tipo)
        {
            string tipoFact = null;

            TipoTransNotaCredito complemento = TipoTransNotaCredito.Complemento;

            TipoTransNotaCredito ajuste = TipoTransNotaCredito.Ajuste;

            TipoTransNotaCredito anulacion = TipoTransNotaCredito.Anulacion;

            if (tipo == complemento.GetStringValue())
            {
                tipoFact = complemento.ToString();

            }
            else if (tipo == ajuste.GetStringValue())
            {
                tipoFact = ajuste.ToString();

            }

            else if (tipo == anulacion.GetStringValue())
            {
                tipoFact = anulacion.ToString();

            }

            return tipoFact;
        }

        public string GetTipoTransNotaCreditoReverse(string tipo)
        {
            string tipoFact = null;

            TipoTransNotaCredito complemento = TipoTransNotaCredito.Complemento;

            TipoTransNotaCredito ajuste = TipoTransNotaCredito.Ajuste;

            TipoTransNotaCredito anulacion = TipoTransNotaCredito.Anulacion;

            if (tipo == complemento.ToString())
            {
                tipoFact = complemento.GetStringValue();

            }
            else if (tipo == ajuste.ToString())
            {
                tipoFact = ajuste.GetStringValue();

            }

            else if (tipo == anulacion.ToString())
            {
                tipoFact = anulacion.GetStringValue();

            }

            return tipoFact;
        }

        public string GetTipoTransReverse(string tipo)
        {
            string tipoFact = null;

            TipoTrans1 registro = TipoTrans1.Registro;

            TipoTrans1 interno = TipoTrans1.Interno;

            TipoTrans1 anulacion = TipoTrans1.Anulacion;

            if (tipo == registro.ToString())
            {
                tipoFact = registro.GetStringValue();

            }
            else if (tipo == interno.ToString())
            {
                tipoFact = interno.GetStringValue();

            }
            else if (tipo == anulacion.ToString())
            {
                tipoFact = anulacion.GetStringValue();

            }

            return tipoFact;
        }

        public char GetWTLiable(string WtLiable)
        {
            char Status = 'Y';

            if (WtLiable == SujetoImpuesto.SI.ToString())
            {
                Status = (char)SujetoImpuesto.SI;
            }
            else if (WtLiable == SujetoImpuesto.NO.ToString())
            {
                Status = (char)SujetoImpuesto.NO;

            }

            return Status;
        }

        public Tuple<string,string> GetAccountTaxPurchase(string taxCode)
        {
            return cn.GetAccountTaxPurchase(taxCode);
        }

        public Tuple<string, string> GetAccountTaxSales(string taxCode)
        {
            return cn.GetAccountTaxSales(taxCode);
        }

        public Tuple<string, string> GetAccountTransferAc(string itemCode)
        {
            return cn.GetAccountTransferAc(itemCode);
        }

        public Tuple<string, string> GetAccountSaleCostAc(string itemCode)
        {
            return cn.GetAccountSaleCostAc(itemCode);
        }

        public Tuple<int,string> DeleteNewRecord(List<int> listTransSeq)
        {
            return cn.DeleteNewRecord(listTransSeq);
        }

        public Tuple<int, string> GetLastTransSeq()
        {
            return cn.GetLastTransSeq();
        }

        public Tuple<int, string> GetLastTransSeqPreliminar()
        {
            return cn.GetLastTransSeqPreliminar();
        }

        enum DocType { Articulo = 'I', Servicio = 'S' };

        enum DocStatus { Abierto = 'O', Cerrado = 'C' };

        enum enumInvStatus { Servicio = 'C', Articulo = 'O' };

        enum TipoTrans { Registro = '1', Interno = '5' };

        enum SujetoImpuesto { SI = 'Y', NO = 'N' };
    }

    public class StringValueAttribute : Attribute
    {

        #region Properties

        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }

        #endregion

    }

    public static class EnumTipoTrans
    {
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }

        public enum TipoTrans1 : int
        {
            [StringValue("01")]
            Registro = 1,
            [StringValue("05")]
            Interno = 2,
            [StringValue("03")]
            Anulacion = 3
        }

        public enum TipoTransNotaCredito : int
        {
            [StringValue("02")]
            Complemento = 1,
            [StringValue("03")]
            Anulacion = 2,
            [StringValue("04")]
            Ajuste = 3
        }

    }
}
