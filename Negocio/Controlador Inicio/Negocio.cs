using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Entidades;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Globalization;
using Negocio.Controlador_Inicio;

namespace Negocio
{
    public class Negocios:Base
    {
        ModeloBase cn = new ModeloBase();
        public string str;
        public static readonly Regex regex = new Regex("[^0-9,.]");

        public decimal ConvertDecimalTwoPlaces<T>(T number)
        {
            string str = null;

            decimal amount = 0;

            SetNumericConfiguration();

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

        public double ConvertDoubleTwoPlaces<T>(T number)
        {
            string str = null;

            double amount = 0;

            SetNumericConfiguration();

            if (String.IsNullOrWhiteSpace(number.ToString()) == false)
            {
                str = regex.Replace(number.ToString(), String.Empty);

                amount = double.Parse(str.ToString(), nfi);

                amount = Math.Round(amount, 2);
            }
            else
            {
                amount = 0;
            }
            return amount;
        }

        public Tuple<List<Monedas>, string> ConsultaMonedas()
        {
            var result = cn.ConsultaMonedas();

            return Tuple.Create(GetMonedas(result.Item1), result.Item2);
        }

        public List<MetodoValoracion> ConsultaMetodoValoracion()
        {
            
            return GetMetodoValoracion();
        }

        private List<MetodoValoracion> GetMetodoValoracion()
        {
            List<MetodoValoracion> listaMetodoValoracion = new List<MetodoValoracion>();
                        
            listaMetodoValoracion.Add(new MetodoValoracion("FIFO", "F"));

            listaMetodoValoracion.Add(new MetodoValoracion("Promedio Ponderado", "A"));

            listaMetodoValoracion.Add(new MetodoValoracion("Estandar", "S"));

            return listaMetodoValoracion;
        }
        public Tuple<int,string> ObtenerIdUser(string username)
        {
            return cn.ObtenerIdUser(username);
        }

        public Tuple<List<Monedas>, string> ConsultaMonedasWithOutMainCurrency()
        {
            var result = cn.ConsultaMonedasWithOutMainCurrency();

            return Tuple.Create(GetMonedas(result.Item1), result.Item2);
        }


        public Tuple<string, string> GetCurrency()
        {
            return cn.GetCurrency();
        }

        public Tuple<List<SocioNegocio>, string> FindSuppliers()
        {
            return cn.FindSuppliers();
        }

        public Tuple<List<SocioNegocio>,string> FindBP()
        {
            return cn.FindBP();
        }

        public Tuple<List<SocioNegocio>, string> FindCustomer()
        {
            return cn.FindCustomer();
        }

        public Tuple<List<Monedas>, string> FindSupplierCurrency(string supplier)
        {
            var result = cn.FindSupplierCurrency(supplier);

            DataTable dt = result.Item1;

            return Tuple.Create(GetMonedasBP(dt),result.Item2);
        }

        private List<Monedas> GetMonedas(DataTable dt)
        {
            List<Monedas> listaMonedas = new List<Monedas>();

            string currCode;

            string currName;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currCode = dt.Rows[i]["CurrCode"].ToString();
                currName = dt.Rows[i]["CurrName"].ToString();

                listaMonedas.Add(new Monedas(currCode, currName));
            }

            listaMonedas.Add(new Monedas("##","Monedas Todas"));

            return listaMonedas;
        }

        private List<Monedas> GetMonedasBP(DataTable dt)
        {
            List<Monedas> listaMonedas = new List<Monedas>();

            string currCode;

            string currName;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                currCode = dt.Rows[i]["CurrCode"].ToString();
                currName = dt.Rows[i]["CurrName"].ToString();

                listaMonedas.Add(new Monedas(currCode, currName));
            }           

            return listaMonedas;
        }

        public DataTable AddColumnOld(DataTable dt, string newColunmName, string ColunmName)
        {
            try
            {
                int i = 1;

                DataColumn newCol = new DataColumn(newColunmName, typeof(string));

                newCol.AllowDBNull = true;

                dt.Columns.Add(newCol);

                for (i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][newColunmName] = dt.Rows[i][ColunmName];

                }

                return dt;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error General: " + e.Message);

                return dt;
            }


        }

        public Tuple<int, string> UpdateDeletedUser(string username)
        {
            return cn.UpdateDeletedUser(username);
        }

        public Tuple<int, string> UpdateDeletedCurrency(string currency)
        {
            return cn.UpdateDeletedCurrency(currency);
        }

        public Tuple<int, string> UpdateDeletedBusinessPartner(string businessPartner)
        {
            return cn.UpdateDeletedBusinessPartner(businessPartner);
        }

        public Tuple<int, string> UpdateDeletedAccount(string account)
        {
            return cn.UpdateDeletedBusinessPartner(account);
        }

        public Tuple<int, string> UpdateDeletedItem(string item)
        {
            return cn.UpdateDeletedItem(item);
        }

        public Tuple<int,string> GetPeriodCode(DateTime? refDate)
        {
            return cn.GetPeriodCode(refDate);
        }

       
        public Tuple<object,string> VerificaFCCurrency(string fCCurrency)
        {
            return cn.VerificaFCCurrency(fCCurrency);
        }

        public Tuple <decimal,string> FindRate(DateTime? RateDate)
        {
            return cn.FindRate(RateDate);
        }

        public Tuple<decimal, string> FindRateFCCurrency(DateTime? selectedDate, string fCCurrency)
        {
            return cn.FinDRateFCCurrency(selectedDate, fCCurrency);
        }


        public Tuple <List<Cuenta>,string> ConsultaCuentasNoAsociadas()
        {
            return cn.ConsultaCuentasNoAsociadas();
        }

        public string RemoveAmountFC(string AmountWithCurrencyDeb, string AmountWithCurrencyCred)
        {
            string currency;

            bool? sw;

            if (String.IsNullOrWhiteSpace(AmountWithCurrencyDeb) == false)
            {
                currency = AmountWithCurrencyDeb.Substring(0, 3);

                sw = true;
            }
            else
            {
                currency = "";

                sw = false;
            }

            if (sw == false)
            {           
                if (String.IsNullOrWhiteSpace(AmountWithCurrencyCred) == false)
                {
                    currency = AmountWithCurrencyCred.Substring(0, 3);
                }
                else
                {
                    currency = "";
                }
            }
            return currency;
        }

        public decimal RemoveCurrency(string AmountWithCurrency)
        {
            decimal Amount=0;

            string AmountWithOutCurrency;

            if (String.IsNullOrWhiteSpace(AmountWithCurrency) ==false)
            {
                AmountWithOutCurrency= AmountWithCurrency.Substring(4);

                Amount = Convert.ToDecimal(AmountWithOutCurrency);

                return Amount;
            }
            else
            {
                return Amount;
            }
            
        }

        public Tuple <int, string> SelectTransId()
        {
            return cn.SelectTransId();
        }

        public Tuple<int, string> SelectBaseRef()
        {
            return cn.SelectBaseRef();
        }

        public decimal CalculateLocTotal(DataTable dt)
        {
            decimal LocTotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Debit")
                    {
                        if (String.IsNullOrWhiteSpace(row["Debit"].ToString())==false)
                        {
                            LocTotal = Convert.ToDecimal(Convert.ToString(row["Debit"]).Substring(3)) + LocTotal;
                        }
                    }

                }

            }

            return LocTotal;
        }

        public decimal CalculateSysTotal(DataTable dt)
        {
            decimal sysTotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "SYSDeb")
                    {
                        if (String.IsNullOrWhiteSpace(row["SYSDeb"].ToString()) == false)
                        {
                            sysTotal = Convert.ToDecimal(Convert.ToString(row["SYSDeb"]).Substring(3)) + sysTotal;
                        }
                    }

                }

            }

            return sysTotal;
        }

       

        public decimal CalculateFCTotal(DataTable dt)
        {
            decimal fcTotal = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "FCDebit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCDebit"].ToString())== false)
                        {
                         fcTotal = Convert.ToDecimal(Convert.ToString(row["FCDebit"]).Substring(3)) + fcTotal;
                        }
                    }

                }

            }

            return fcTotal;
        }

        public string GetFC(DataTable dt)
        {
            string fcCurrency="";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "FCDebit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCDebit"].ToString()) == false)
                        {
                            fcCurrency = (Convert.ToString(row["FCDebit"]).Substring(0,3));
                        }
                    }

                    if (column.ToString() == "FCCredit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCCredit"].ToString()) == false)
                        {
                            fcCurrency = (Convert.ToString(row["FCCredit"]).Substring(0, 3));
                        }
                    }

                }

            }

           

            return fcCurrency;
        }

        public bool VerificaDebeHaber(DataTable dt)
        {
            bool sw = true;

            decimal SysDeb = 0;

            decimal SysCred = 0;

            decimal Debit = 0;

            decimal Credit = 0;

            decimal FCDebit = 0;

            decimal FCCredit = 0;

            foreach (DataRow row in dt.Rows)
            {

                
                    Debit = ConvertDecimalTwoPlaces(row["Debit"]) + ConvertDecimalTwoPlaces(Debit);
               
                    Credit = ConvertDecimalTwoPlaces(row["Credit"]) + ConvertDecimalTwoPlaces(Credit);
                
                    SysDeb = ConvertDecimalTwoPlaces(row["SysDeb"]) + ConvertDecimalTwoPlaces(SysDeb);
                
                    SysCred = ConvertDecimalTwoPlaces(row["SysCred"]) + ConvertDecimalTwoPlaces(SysCred);
                
                    FCDebit = ConvertDecimalTwoPlaces(row["FCDebit"]) + ConvertDecimalTwoPlaces(FCDebit);
                
                    FCCredit = ConvertDecimalTwoPlaces(row["FCCredit"]) + ConvertDecimalTwoPlaces(FCCredit);
               

            }

            if (ConvertDecimalTwoPlaces(Debit) == ConvertDecimalTwoPlaces(Credit))
            {
                sw = true;

                if (ConvertDecimalTwoPlaces(SysDeb) == ConvertDecimalTwoPlaces(SysCred))
                {
                    sw = true;

                    if (ConvertDecimalTwoPlaces(FCDebit) == ConvertDecimalTwoPlaces(FCCredit))
                    {
                        sw = true;
                    }
                    else
                    {
                        sw = false;
                    }
                }
                else
                {
                    sw = false;
                }
                
            }
            else
            {

                sw = false;
            }           
            

            return sw;
        }

       

        public void UpdateCreditDebitAccount(DataTable dt)
        {
            decimal SysSaldo = 0;

            decimal Saldo = 0;

            decimal FCSaldo = 0;

            string account=null;

            string shortName = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Account")
                    {
                        if (String.IsNullOrWhiteSpace(row["Account"].ToString()) == false)
                        {
                            account = row["Account"].ToString();
                        }
                    }

                    else if (column.ToString() == "ShortName")
                    {
                        if (String.IsNullOrWhiteSpace(row["ShortName"].ToString()) == false)
                        {
                            shortName = row["ShortName"].ToString();
                        }
                    }

                    if (column.ToString() == "Debit")
                    {
                        if (String.IsNullOrWhiteSpace(row["Debit"].ToString()) == false)
                        {
                            str = regex.Replace(row["Debit"].ToString(), String.Empty);

                            Saldo = Saldo + ConvertDecimalTwoPlaces(str);
                        }
                    }

                    if (column.ToString() == "Credit")
                    {
                        if (String.IsNullOrWhiteSpace(row["Credit"].ToString()) == false)
                        {
                            str = regex.Replace(row["Credit"].ToString(), String.Empty);

                            Saldo = Saldo - ConvertDecimalTwoPlaces(str);
                        }
                    }

                    if (column.ToString() == "SYSDeb")
                    {
                        if (String.IsNullOrWhiteSpace(row["SYSDeb"].ToString()) == false)
                        {
                            str = regex.Replace(row["SYSDeb"].ToString(), String.Empty);

                            SysSaldo = SysSaldo + ConvertDecimalTwoPlaces(str);
                        }
                    }

                    if (column.ToString() == "SYSCred")
                    {
                        if (String.IsNullOrWhiteSpace(row["SYSCred"].ToString()) == false)
                        {
                            str = regex.Replace(row["SYSCred"].ToString(), String.Empty);

                            SysSaldo = SysSaldo - ConvertDecimalTwoPlaces(str);
                        }
                    }

                    if (column.ToString() == "FCDebit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCDebit"].ToString()) == false)
                        {
                            str = regex.Replace(row["FCDebit"].ToString(), String.Empty);

                            FCSaldo = FCSaldo + ConvertDecimalTwoPlaces(str);
                        }

                    }

                    if (column.ToString() == "FCCredit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCCredit"].ToString()) == false)
                        {
                            str = regex.Replace(row["FCCredit"].ToString(), String.Empty);

                            FCSaldo = FCSaldo - ConvertDecimalTwoPlaces(str);
                        }
                    }

                }

                if (cn.UpdateDebitCreditAccount(account, Saldo, SysSaldo, FCSaldo) != 1)
                {
                    MessageBox.Show("Error al modificar la cuenta " + account, "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (row["Account"].ToString()!=row["ShortName"].ToString()) { //verifica si la cuenta asociada es diferente a la cuenta


                    if (cn.UpdateDebitCreditAccountBusinessPartner(shortName, Saldo, SysSaldo, FCSaldo) != 1)
                    {
                        
                        MessageBox.Show("Error al modificar la cuenta ", "Journal Entry", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                Saldo = 0;

                SysSaldo = 0;

                FCSaldo = 0;

                shortName = null;

                account = null;

            }
        }

        public decimal SumDebit(DataTable dt)
        {
            decimal SumDebit=0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Debit")
                    {
                        if (String.IsNullOrWhiteSpace(row["Debit"].ToString()) == false)
                        {
                            SumDebit = Convert.ToDecimal(Convert.ToString(row["Debit"]).Substring(3)) + SumDebit;
                        }
                    }

                }
            }

            return SumDebit;
        }

        public decimal SumSysDebit(DataTable dt)
        {
            decimal SumSysDebit = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "SysDeb")
                    {
                        if (String.IsNullOrWhiteSpace(row["SysDeb"].ToString()) == false)
                        {
                            SumSysDebit = Convert.ToDecimal(Convert.ToString(row["SysDeb"]).Substring(3)) + SumSysDebit;
                        }
                    }

                }
            }

            return SumSysDebit;
        }

        public decimal SumSysCredit(DataTable dt)
        {
            decimal SumSysCredit = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "SysCred")
                    {
                        if (String.IsNullOrWhiteSpace(row["SysCred"].ToString()) == false)
                        {
                            SumSysCredit = Convert.ToDecimal(Convert.ToString(row["SysCred"]).Substring(3)) + SumSysCredit;
                        }
                    }

                }
            }

            return SumSysCredit;
        }

        public decimal SumCredit(DataTable dt)
        {
            decimal SumCredit = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "Credit")
                    {
                        if (String.IsNullOrWhiteSpace(row["Credit"].ToString()) == false)
                        {
                            SumCredit = Convert.ToDecimal(Convert.ToString(row["Credit"]).Substring(3)) + SumCredit;
                        }
                    }

                }
            }

            return SumCredit;
        }

        public decimal SumFCDebit(DataTable dt)
        {
            decimal SumFCDebit = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "FCDebit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCDebit"].ToString()) == false)
                        {
                            SumFCDebit = Convert.ToDecimal(Convert.ToString(row["FCDebit"]).Substring(3)) + SumFCDebit;
                        }
                    }

                }
            }

            return SumFCDebit;
        }

        public decimal SumFCCredit(DataTable dt)
        {
            decimal SumFCCredit = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {

                    if (column.ToString() == "FCCredit")
                    {
                        if (String.IsNullOrWhiteSpace(row["FCCredit"].ToString()) ==false)
                        {
                            SumFCCredit = Convert.ToDecimal(Convert.ToString(row["FCCredit"]).Substring(3)) + SumFCCredit;
                        }
                    }

                }
            }

            return SumFCCredit;
        }

        public string FindAcctName(string account)
        {
            return cn.FindAcctName(account);
        }

        public int GetTransType(string transType)
        {
            int tipoTrans=0;

            if (transType == TransType.AS.ToString())
            {
                tipoTrans = (int)TransType.AS;

            }else if (transType == TransType.RF.ToString())
            {
                tipoTrans = (int)TransType.RF;
            }

            else if (transType == TransType.TT.ToString())
            {
                tipoTrans = (int)TransType.TT;
            }

            else if (transType == TransType.PR.ToString())
            {
                tipoTrans = (int)TransType.PR;
            }

            else if (transType == TransType.PP.ToString())
            {
                tipoTrans = (int)TransType.PP;
            }

            else if (transType == TransType.NE.ToString())
            {
                tipoTrans = (int)TransType.NE;
            }

            else if (transType == TransType.EP.ToString())
            {
                tipoTrans = (int)TransType.EP;
            }

            else if (transType == TransType.PC.ToString())
            {
                tipoTrans = (int)TransType.PC;
            }

            else if (transType == TransType.RC.ToString())
            {
                tipoTrans = (int)TransType.RC;
            }

            else if (transType == TransType.SI.ToString())
            {
                tipoTrans = (int)TransType.SI;
            }

            else if (transType == TransType.CB.ToString())
            {
                tipoTrans = (int)TransType.CB;
            }

            else if (transType == TransType.ID.ToString())
            {
                tipoTrans = (int)TransType.ID;
            }


            return tipoTrans;
        }

        public string GetTransType(int code)
        {
            string transType = null;

            if (code == (int)TransType.AS)
            {
                transType = TransType.AS.ToString();

            }
            else if (code == (int)TransType.RF)
            {
                transType = TransType.RF.ToString();
            }

            else if (code == (int)TransType.TT)
            {
                transType = TransType.TT.ToString();
            }

            else if (code == (int)TransType.PR)
            {
                transType = TransType.PR.ToString();
            }

            else if (code == (int)TransType.PP)
            {
                transType = TransType.PP.ToString();
            }

            else if (code == (int)TransType.NE)
            {
                transType = TransType.NE.ToString();
            }

            else if (code == (int)TransType.EP)
            {
                transType = TransType.EP.ToString();
            }

            else if (code == (int)TransType.PC)
            {
                transType = TransType.PC.ToString();
            }

            else if (code == (int)TransType.RC)
            {
                transType = TransType.RC.ToString();
            }

            else if (code== (int)TransType.SI)
            {
                transType = TransType.SI.ToString();
            }

            else if (code == (int)TransType.CB)
            {
                transType = TransType.CB.ToString();
            }

            else if (code == (int)TransType.ID)
            {
                transType = TransType.ID.ToString();
            }


            return transType;
        }

        public enum TransType { AS = 30, RF = 13, TT = 18, PR = 24, PP = 46, NE = 15, EP = 20, PC = 19, RC = 14, SI = -2, CB = -3, ID=321 };

        //public DataTable InsertaMonedasDatatable<Monedas>(List<Monedas> listaMonedas)
        //    {
        //        DataTable dataTable = new DataTable(typeof(Monedas).Name);

        //        //Get all the properties
        //        PropertyInfo[] Props = typeof(Monedas).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //        foreach (PropertyInfo prop in Props)
        //        {
        //            //Defining type of data column gives proper data table 
        //            var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
        //            //Setting column names as Property names
        //            dataTable.Columns.Add(prop.Name, type);
        //        }
        //        foreach (Monedas item in listaMonedas)
        //        {
        //            var values = new object[Props.Length];
        //            for (int i = 0; i < Props.Length; i++)
        //            {
        //                //inserting property values to datatable rows
        //                values[i] = Props[i].GetValue(item, null);
        //            }
        //            dataTable.Rows.Add(values);
        //        }
        //        //put a breakpoint here and check datatable
        //        return dataTable;


        //}

    }
}
