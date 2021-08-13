using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Datos
{
    public class ModeloAuditoriaStock : ModeloDocumento
    {
        
        public Tuple<DataTable, string> ExecuteAuditoriaStock(DataTable dt, char cbxCompras, char cbxVentas, DateTime? dpHFechaContabilizacion, DateTime? dpDFechaContabilizacion = null)
        {
            string itemCode = null;

            decimal balance = 0;

            decimal inValue = 0;

            decimal outValue = 0;

            decimal outQty = 0;

            decimal inQty = 0;           

            decimal quantity = 0;

            decimal saldoInicial = 0;

            decimal cantidadInicial = 0;

            int ugpEntry = 0;

            DataTable newDt = new DataTable();

            newDt.Columns.Add("ItemCode", typeof(string));
            newDt.Columns.Add("Dscription", typeof(string));
            newDt.Columns.Add("DocDate", typeof(string));            
            newDt.Columns.Add("TransType", typeof(string));
            newDt.Columns.Add("BASE_REF", typeof(string));
            newDt.Columns.Add("Quantity", typeof(decimal));
            newDt.Columns.Add("CalcPrice", typeof(decimal));
            newDt.Columns.Add("TransValue", typeof(decimal));
            newDt.Columns.Add("Balance", typeof(decimal));
            newDt.Columns.Add("QuantityAcum", typeof(decimal));                    
            newDt.Columns.Add("Title", typeof(string));
            newDt.Columns.Add("Num", typeof(int));
            newDt.Columns.Add("InvntryUomCode", typeof(string));
            newDt.Columns.Add("InvntryUomEntry", typeof(int));
            newDt.Columns.Add("UgpEntry", typeof(int));
            newDt.Columns.Add("UomCode", typeof(string));
            newDt.Columns.Add("UomCode2", typeof(string));
            newDt.Columns.Add("InValue", typeof(decimal));
            newDt.Columns.Add("OutValue", typeof(decimal));
            newDt.Columns.Add("OutQty", typeof(decimal));
            newDt.Columns.Add("InQty", typeof(decimal));
            newDt.Columns.Add("InValueAcum", typeof(decimal));
            newDt.Columns.Add("OutValueAcum", typeof(decimal));
            newDt.Columns.Add("OutQtyAcum", typeof(decimal));
            newDt.Columns.Add("InQtyAcum", typeof(decimal));
            newDt.Columns.Add("SaldoInicial", typeof(decimal));
            newDt.Columns.Add("CantidadInicial", typeof(decimal));

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();                   

                    int i = 0;

                    int j = 0;

                    foreach (DataRow row in dt.Rows)
                    {


                        if (Convert.ToBoolean(row["Seleccionado"]) == true) //Verifica si la cuenta se selecciono
                        {
                            //Crear fila de cabecera

                            j = 0;

                            DataRow newRow = newDt.NewRow();

                            newDt.Rows.Add(newRow);

                            itemCode = row["ItemCode"].ToString();
                            newRow["ItemCode"] = row["ItemCode"].ToString();
                            newRow["Dscription"] = row["ItemName"].ToString();
                            newRow["DocDate"] = "";
                            newRow["TransType"] = "";
                            newRow["BASE_REF"] = "";
                            newRow["Quantity"] = 0;
                            newRow["CalcPrice"] = 0;
                            newRow["TransValue"] = 0;
                            newRow["InValue"] = 0;
                            newRow["OutValue"] = 0;
                            newRow["OutQty"] = 0;
                            newRow["InQty"] = 0;
                            newRow["Title"] = "Y";
                            newRow["InvntryUomCode"] = "";
                            newRow["InvntryUomEntry"] = 0;
                            newRow["UomCode"] = "";
                            newRow["UomCode2"] = "";

                            if (i % 2 == 0)
                            {
                                newRow["Num"] = 0;
                            }
                            else
                            {
                                newRow["Num"] = 1;
                            }

                            //Crear fila de Saldo Inicial

                            DataRow newRow1 = newDt.NewRow();

                            newDt.Rows.Add(newRow1);

                            newRow1["ItemCode"] = "";
                            newRow1["Dscription"] = "";
                            newRow1["DocDate"] = "";
                            newRow1["TransType"] = "Saldo Inicial";
                            newRow1["BASE_REF"] = "";
                            newRow1["Quantity"] = 0;
                            newRow1["CalcPrice"] = 0;
                            newRow1["TransValue"] = 0;
                            newRow1["InValue"] = 0;
                            newRow1["OutValue"] = 0;
                            newRow1["OutQty"] = 0;
                            newRow1["InQty"] = 0;
                            newRow1["Balance"] = 0.00; //consulta bbdd
                            newRow1["QuantityAcum"] = 0.00; //consulta bbdd
                            newRow1["InValueAcum"] = 0.00; //consulta bbdd
                            newRow1["OutValueAcum"] = 0.00; //consulta bbdd
                            newRow1["OutQtyAcum"] = 0.00; //consulta bbdd
                            newRow1["InQtyAcum"] = 0.00; //consulta bbdd
                           

                            newRow1["Title"] = "E";
                            newRow1["InvntryUomCode"] = "";
                            newRow1["InvntryUomEntry"] = 0;
                            newRow1["UgpEntry"] = 0;
                            newRow1["UomCode"] = "";
                            newRow1["UomCode2"] = "";

                            if (i % 2 == 0)
                            {
                                newRow1["Num"] = 0;
                            }
                            else
                            {
                                newRow1["Num"] = 1;
                            }

                            //----------------------------------------------------------------------



                            using (SqlCommand cmd = Connection.CreateCommand())
                            {
                                cmd.CommandText = "FindStockAudit";

                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@ItemCode", row["ItemCode"].ToString());

                                if (String.IsNullOrWhiteSpace(dpDFechaContabilizacion.ToString()) == false)
                                {
                                    cmd.Parameters.AddWithValue("@F_RefDate", dpDFechaContabilizacion);
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue("@F_RefDate", DBNull.Value);
                                }

                                cmd.Parameters.AddWithValue("@Compras", cbxCompras);

                                cmd.Parameters.AddWithValue("@Ventas", cbxVentas);

                                cmd.Parameters.AddWithValue("@T_RefDate", dpHFechaContabilizacion);

                                SqlDataReader reader = cmd.ExecuteReader();

                                while (reader.Read())
                                {
                                    DataRow newRow2 = newDt.NewRow();

                                    newDt.Rows.Add(newRow2);

                                    newRow2["ItemCode"] = reader["ItemCode"].ToString();
                                    newRow2["Dscription"] = reader["Dscription"].ToString();
                                    newRow2["DocDate"] = reader["DocDate"].ToString();
                                    newRow2["TransType"] = reader["TransType"].ToString();
                                    newRow2["BASE_REF"] = reader["BASE_REF"].ToString();
                                    newRow2["Quantity"] = Convert.ToDecimal(reader["Quantity"]);
                                    newRow2["OutQty"] = Convert.ToDecimal(reader["OutQty"]);
                                    newRow2["InQty"] = Convert.ToDecimal(reader["InQty"]);
                                    newRow2["CalcPrice"] = Convert.ToDecimal(reader["CalcPrice"]);
                                    newRow2["TransValue"] = Convert.ToDecimal(reader["TransValue"]);
                                    newRow2["InValue"] = Convert.ToDecimal(reader["InValue"]);
                                    newRow2["OutValue"] = Convert.ToDecimal(reader["OutValue"]);

                                    

                                    newRow2["Balance"] = Convert.ToDecimal(reader["Balance"]);
                                    newRow2["InvntryUomCode"] = reader["InvntryUomCode"].ToString() == "" ? "" : reader["InvntryUomCode"].ToString();
                                    newRow2["InvntryUomEntry"] = reader["InvntryUomEntry"].ToString() == "" ? 0 : Convert.ToInt32(reader["InvntryUomEntry"]);
                                    newRow2["UomCode"] = reader["UomCode"].ToString() == "" ? "" : reader["UomCode"].ToString();
                                    newRow2["UomCode2"] = reader["UomCode2"].ToString() == "" ? "" : reader["UomCode2"].ToString();
                                    newRow2["UgpEntry"] = reader["UgpEntry"].ToString() == "" ? 0 : Convert.ToInt32(reader["UgpEntry"]);

                                    if (ConvertDecimalTwoPlaces(newRow2["InQty"])==0 && ConvertDecimalTwoPlaces(newRow2["InValue"]) == 0)
                                    {
                                        newRow2["OutValueAcum"] = CalculateOutValue(newDt, itemCode);
                                        newRow2["OutQtyAcum"] = CalculateOutQty(newDt, itemCode);
                                        newRow2["InValueAcum"] = 0;
                                        newRow2["InQtyAcum"] = 0;

                                    }
                                    else if(ConvertDecimalTwoPlaces(newRow2["OutQty"]) == 0 && ConvertDecimalTwoPlaces(newRow2["OutValue"]) == 0)
                                    {
                                        newRow2["OutValueAcum"] = 0;
                                        newRow2["OutQtyAcum"] = 0;
                                        newRow2["InValueAcum"] = CalculateInValue(newDt, itemCode);
                                        newRow2["InQtyAcum"] = CalculateInQty(newDt, itemCode);
                                    }

                                    newRow2["QuantityAcum"] = CalculateQuantity(newDt, itemCode);

                                    newRow2["Title"] = "N";

                                    if (i % 2 == 0)
                                    {
                                        newRow2["Num"] = 0;
                                    }
                                    else
                                    {
                                        newRow2["Num"] = 1;
                                    }



                                    balance = Convert.ToDecimal(reader["Balance"]);

                                    quantity = CalculateQuantity(newDt, itemCode);

                                    inValue = CalculateInValue(newDt, itemCode);

                                    outValue = CalculateOutValue(newDt, itemCode);

                                    outQty = CalculateOutQty(newDt, itemCode);

                                    inQty = CalculateInQty(newDt, itemCode);

                                    saldoInicial = CalculateSaldoInicial(newDt, itemCode);

                                    cantidadInicial = CalculateCantidadInicial(newDt, itemCode);

                                    ugpEntry =Convert.ToInt32(reader["UgpEntry"]);

                                    j = 1;

                                }

                                if (j == 0)
                                {
                                    newRow["Balance"] = 0;
                                    newRow["QuantityAcum"] = 0;
                                    newRow["UgpEntry"] = 0;
                                    newRow["InValueAcum"] = 0;
                                    newRow["OutValueAcum"] = 0;
                                    newRow["OutQtyAcum"] = 0;
                                    newRow["InQtyAcum"] = 0;
                                    newRow["SaldoInicial"] = 0;
                                    newRow["CantidadInicial"] = 0;
                                }
                                else
                                {
                                    newRow["Balance"] = balance;
                                    newRow["QuantityAcum"] = quantity;
                                    newRow["UgpEntry"] = ugpEntry;
                                    newRow["InValueAcum"] = inValue;
                                    newRow["OutValueAcum"] = outValue;
                                    newRow["OutQtyAcum"] = outQty;
                                    newRow["InQtyAcum"] = inQty;
                                    newRow["SaldoInicial"] = saldoInicial;
                                    newRow["CantidadInicial"] = cantidadInicial;
                                }



                                reader.Close();



                            }//end using

                            i++;
                        }//end if                      

                    }//end foreach row


                }
                Connection.Close();

              

                return Tuple.Create(newDt, error);

            }
            catch (Exception e)
            {             

                Connection.Close();

                return Tuple.Create(newDt, e.Message);
            }
        }

        private decimal CalculateCantidadInicial(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "CantidadInicial" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + ConvertDecimalTwoPlaces(row["CantidadInicial"]);
                    }
                }
            }

            return quantity;
        }

        private decimal CalculateSaldoInicial(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "SaldoInicial" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + ConvertDecimalTwoPlaces(row["SaldoInicial"]);
                    }
                }
            }

            return quantity;
        }

        public Tuple<DataTable, string> ExecuteAuditoriaStockUnidades(DataRow[] tableResult)
        {
            SetNumericConfiguration();

            DataTable newDt = new DataTable();

            newDt.Columns.Add("ItemCode", typeof(object));
            newDt.Columns.Add("Dscription", typeof(object));            
            newDt.Columns.Add("UomCode", typeof(object));
            newDt.Columns.Add("Quantity", typeof(object));
            newDt.Columns.Add("Num", typeof(int));

            string error = null;

            try
            {
                using (Connection = new SqlConnection(connectionString))
                {
                    Connection.Open();

                    int i = 0;                  

                    foreach (DataRow row in tableResult)
                    {
                        //Crear fila de cabecera                      

                        DataRow newRow = newDt.NewRow();

                        newDt.Rows.Add(newRow);
                        
                        newRow["ItemCode"] = row["ItemCode"].ToString();
                        newRow["Dscription"] = row["Dscription"].ToString();
                        newRow["UomCode"] = "";
                        newRow["Quantity"] = row["QuantityAcum"].ToString() == "" ? 0 : Convert.ToDecimal(row["QuantityAcum"]);

                        if (i % 2 == 0)
                        {
                            newRow["Num"] = 0;
                        }
                        else
                        {
                            newRow["Num"] = 1;
                        }
                        //----------------------------------------------------------------------

                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindStockAuditUnidades";

                            cmd.CommandType = CommandType.StoredProcedure;
                           
                            cmd.Parameters.AddWithValue("@UgpEntry", Convert.ToInt32(row["UgpEntry"]));

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                DataRow newRow1 = newDt.NewRow();

                                newDt.Rows.Add(newRow1);

                                newRow1["ItemCode"] = "";
                                newRow1["Dscription"] = "";
                                newRow1["UomCode"] = reader["UomCode"].ToString();
                                newRow1["Quantity"] =ConvertDecimalTwoPlaces(row["QuantityAcum"]) * ConvertDecimalTwoPlaces(reader["Quantity"]);

                                if (i % 2 == 0)
                                {
                                    newRow1["Num"] = 0;
                                }
                                else
                                {
                                    newRow1["Num"] = 1;
                                }
                            }

                            reader.Close();

                            //Linea en Blanco

                            DataRow newRow2 = newDt.NewRow();

                            newDt.Rows.Add(newRow2);

                            newRow2["ItemCode"] = "";
                            newRow2["Dscription"] = "";
                            newRow2["UomCode"] = "";
                            newRow2["Quantity"] = "";

                            if (i % 2 == 0)
                            {
                                newRow2["Num"] = 0;
                            }
                            else
                            {
                                newRow2["Num"] = 1;
                            }

                        }//end using

                        i++;
                    }//end if  
                }

                Connection.Close();



                return Tuple.Create(newDt, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newDt, e.Message);
            }
        }

       

        private decimal CalculateQuantity(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Quantity" && itemCode==row["ItemCode"].ToString())
                    {
                        quantity = quantity + Convert.ToDecimal(row["Quantity"]);
                    }
                }
            }

            return quantity;
        }

        private decimal CalculateOutQty(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "OutQty" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + Convert.ToDecimal(row["OutQty"]);
                    }
                }
            }

            return quantity;
        }

        private decimal CalculateInQty(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "InQty" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + Convert.ToDecimal(row["InQty"]);
                    }
                }
            }

            return quantity;
        }

        private decimal CalculateInValue(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "InValue" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + Convert.ToDecimal(row["InValue"]);
                    }
                }
            }

            return quantity;
        }
        private decimal CalculateOutValue(DataTable dt, string itemCode)
        {
            decimal quantity = 0;

            foreach (DataRow row in dt.Rows)
            {

                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "OutValue" && itemCode == row["ItemCode"].ToString())
                    {
                        quantity = quantity + Convert.ToDecimal(row["OutValue"]);
                    }
                }
            }

            return quantity;
        }

    }
}
