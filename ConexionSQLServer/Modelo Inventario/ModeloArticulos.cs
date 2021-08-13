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
    public class ModeloArticulos : ConexionSQLServer
    {
        public Tuple<List<Articulos>, string> FinNext(string codigo)
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "NextItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", codigo);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Articulos newItem = new Articulos();

                            newItem.OldItemCode = reader["ItemCode"].ToString();
                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.ItemName = reader["ItemName"].ToString();
                            newItem.Deleted =Convert.ToChar(reader["Deleted"]);
                            newItem.InvnItem = Convert.ToChar(reader["InvntItem"]);
                            newItem.PrchseItem = Convert.ToChar(reader["PrchseItem"]);
                            newItem.SellItem = Convert.ToChar(reader["SellItem"]);
                            newItem.IsCommited = Convert.ToDecimal(reader["IsCommited"]);
                            newItem.OnHand= Convert.ToDecimal(reader["OnHand"]);
                            newItem.OnOrders = Convert.ToDecimal(reader["OnOrder"]);
                            newItem.StockValue = Convert.ToDecimal(reader["StockValue"]);
                            newItem.VatLiable = Convert.ToChar(reader["VatLiable"]);
                            newItem.BalInvntAc = reader["BalInvntAc"].ToString();
                            newItem.RevenuesAc = reader["RevenuesAc"].ToString();
                            newItem.SaleCostAc = reader["SaleCostAc"].ToString();
                            newItem.ExpensesAc = reader["ExpensesAc"].ToString();
                            newItem.TransferAc = reader["TransferAc"].ToString();
                            newItem.EvalSystem = reader["EvalSystem"].ToString();
                            newItem.UgpEntry =Convert.ToInt32(reader["UgpEntry"].ToString());
                            newItem.InvntryUomCode = reader["InvntryUomCode"].ToString();
                            newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                            try
                            {
                                newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                            }
                            catch (Exception ex)
                            {
                                newItem.InvntryUomEntry = 0;
                            }

                            try
                            {
                                newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                            }
                            catch (Exception ex)
                            {
                                newItem.NumInCnt = 1;
                            }


                            newListaItem.Add(newItem);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem, error);

            }
            catch (Exception e)
            {              
                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<List<Articulos>, string> FinPrevious(string codigo)
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "PreviousItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", codigo);

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Articulos newItem = new Articulos();

                            newItem.OldItemCode = reader["ItemCode"].ToString();
                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.ItemName = reader["ItemName"].ToString();
                            newItem.Deleted = Convert.ToChar(reader["Deleted"]);
                            newItem.InvnItem = Convert.ToChar(reader["InvntItem"]);
                            newItem.PrchseItem = Convert.ToChar(reader["PrchseItem"]);
                            newItem.SellItem = Convert.ToChar(reader["SellItem"]);
                            newItem.IsCommited = Convert.ToDecimal(reader["IsCommited"]);
                            newItem.OnHand = Convert.ToDecimal(reader["OnHand"]);
                            newItem.OnOrders = Convert.ToDecimal(reader["OnOrder"]);
                            newItem.StockValue = Convert.ToDecimal(reader["StockValue"]);
                            newItem.VatLiable = Convert.ToChar(reader["VatLiable"]);
                            newItem.BalInvntAc = reader["BalInvntAc"].ToString();
                            newItem.RevenuesAc = reader["RevenuesAc"].ToString();
                            newItem.SaleCostAc = reader["SaleCostAc"].ToString();
                            newItem.ExpensesAc = reader["ExpensesAc"].ToString();
                            newItem.TransferAc = reader["TransferAc"].ToString();
                            newItem.UgpEntry = Convert.ToInt32(reader["UgpEntry"].ToString());
                            newItem.EvalSystem = reader["EvalSystem"].ToString();
                            newItem.InvntryUomCode = reader["InvntryUomCode"].ToString();
                            newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                            try
                            {
                                newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                            }
                            catch (Exception ex)
                            {
                                newItem.InvntryUomEntry = 0;
                            }

                            try
                            {
                                newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                            }
                            catch (Exception ex)
                            {
                                newItem.NumInCnt = 1;
                            }

                            newListaItem.Add(newItem);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem, error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<List<Articulos>, string> ConsultaItems()
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();


                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FindItemPurchase";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Articulos newItem = new Articulos();

                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.ItemName = reader["ItemName"].ToString();
                            newItem.OnHand = Convert.ToDecimal(reader["OnHand"]);
                            newItem.EvalSystem = reader["EvalSystem"].ToString();
                            newItem.UgpEntry = Convert.ToInt32(reader["UgpEntry"].ToString());
                            newItem.InvntryUomCode = reader["InvntryUomCode"].ToString();
                            newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                            try
                            {
                                newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                            }
                            catch (Exception ex)
                            {
                                newItem.InvntryUomEntry = 0;
                            }

                            try
                            {
                                newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                            }
                            catch (Exception ex)
                            {
                                newItem.NumInCnt = 1;
                            }



                            newListaItem.Add(newItem);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem, error);

            }
            catch (Exception e)
            {

                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<int, string> DeleteItem(string itemCode)
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

                        cmd.CommandText = "DeleteItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ItemCode", itemCode);

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

        public Tuple<List<Articulos>, string> FindFirst()
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "FirstItem";

                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Articulos newItem = new Articulos();

                            newItem.OldItemCode = reader["ItemCode"].ToString();
                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.ItemName = reader["ItemName"].ToString();
                            newItem.Deleted = Convert.ToChar(reader["Deleted"]);
                            newItem.InvnItem = Convert.ToChar(reader["InvntItem"]);
                            newItem.PrchseItem = Convert.ToChar(reader["PrchseItem"]);
                            newItem.SellItem = Convert.ToChar(reader["SellItem"]);
                            newItem.IsCommited = Convert.ToDecimal(reader["IsCommited"]);
                            newItem.OnHand = Convert.ToDecimal(reader["OnHand"]);
                            newItem.OnOrders = Convert.ToDecimal(reader["OnOrder"]);
                            newItem.StockValue = Convert.ToDecimal(reader["StockValue"]);
                            newItem.VatLiable = Convert.ToChar(reader["VatLiable"]);
                            newItem.BalInvntAc = reader["BalInvntAc"].ToString();
                            newItem.RevenuesAc = reader["RevenuesAc"].ToString();
                            newItem.SaleCostAc = reader["SaleCostAc"].ToString();
                            newItem.ExpensesAc = reader["ExpensesAc"].ToString();
                            newItem.TransferAc = reader["TransferAc"].ToString();
                            newItem.EvalSystem = reader["EvalSystem"].ToString();
                            newItem.UgpEntry = Convert.ToInt32(reader["UgpEntry"].ToString());
                            newItem.InvntryUomCode = reader["InvntryUomCode"].ToString();
                            newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                            try
                            {
                                newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                            }
                            catch (Exception ex)
                            {
                                newItem.InvntryUomEntry = 0;
                            }

                          
                            try
                            {
                                newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                            }
                            catch (Exception ex)
                            {
                                newItem.NumInCnt = 1;
                            }

                            

                            newListaItem.Add(newItem);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem, error);

            }
            catch (Exception e)
            {                

                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<List<Articulos>, string> FindLast()
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    using (SqlCommand cmd = Connection.CreateCommand())
                    {
                        cmd.CommandText = "LastItem";

                        cmd.CommandType = CommandType.StoredProcedure;                        

                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            Articulos newItem = new Articulos();

                            newItem.OldItemCode = reader["ItemCode"].ToString();
                            newItem.ItemCode = reader["ItemCode"].ToString();
                            newItem.ItemName = reader["ItemName"].ToString();
                            newItem.Deleted = Convert.ToChar(reader["Deleted"]);
                            newItem.InvnItem = Convert.ToChar(reader["InvntItem"]);
                            newItem.PrchseItem = Convert.ToChar(reader["PrchseItem"]);
                            newItem.SellItem = Convert.ToChar(reader["SellItem"]);
                            newItem.IsCommited = Convert.ToDecimal(reader["IsCommited"]);
                            newItem.OnHand = Convert.ToDecimal(reader["OnHand"]);
                            newItem.OnOrders = Convert.ToDecimal(reader["OnOrder"]);
                            newItem.StockValue = Convert.ToDecimal(reader["StockValue"]);
                            newItem.VatLiable = Convert.ToChar(reader["VatLiable"]);
                            newItem.BalInvntAc = reader["BalInvntAc"].ToString();
                            newItem.RevenuesAc = reader["RevenuesAc"].ToString();
                            newItem.SaleCostAc = reader["SaleCostAc"].ToString();
                            newItem.ExpensesAc = reader["ExpensesAc"].ToString();
                            newItem.TransferAc = reader["TransferAc"].ToString();
                            newItem.EvalSystem = reader["EvalSystem"].ToString();
                            newItem.UgpEntry = Convert.ToInt32(reader["UgpEntry"].ToString());
                            newItem.InvntryUomCode= reader["InvntryUomCode"].ToString();
                            newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                            try
                            {
                                newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                            }
                            catch (Exception ex)
                            {
                                newItem.InvntryUomEntry = 0;
                            }

                            try
                            {
                                newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                            }
                            catch (Exception ex)
                            {
                                newItem.NumInCnt = 1;
                            }

                            newListaItem.Add(newItem);

                        }

                        reader.Close();
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem,error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<List<Articulos>, string> ConsultaItems(List<Articulos> listaArticulo)
        {
            List<Articulos> newListaItem = new List<Articulos>();

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Articulos item in listaArticulo)
                    {


                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "FindItem";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                            cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                            cmd.Parameters.AddWithValue("@PrchseItem", item.PrchseItem);
                            cmd.Parameters.AddWithValue("@SellItem", item.SellItem);
                            cmd.Parameters.AddWithValue("@InvntItem", item.InvnItem);
                            cmd.Parameters.AddWithValue("@EvalSystem", item.EvalSystem);
                            cmd.Parameters.AddWithValue("@UgpEntry", item.UgpEntry);

                            SqlDataReader reader = cmd.ExecuteReader();

                            while (reader.Read())
                            {
                                Articulos newItem = new Articulos();

                                newItem.OldItemCode = reader["ItemCode"].ToString();
                                newItem.ItemCode = reader["ItemCode"].ToString();
                                newItem.ItemName = reader["ItemName"].ToString();
                                newItem.Deleted = Convert.ToChar(reader["Deleted"]);
                                newItem.InvnItem = Convert.ToChar(reader["InvntItem"]);
                                newItem.PrchseItem = Convert.ToChar(reader["PrchseItem"]);
                                newItem.SellItem = Convert.ToChar(reader["SellItem"]);
                                newItem.IsCommited = Convert.ToDecimal(reader["IsCommited"]);
                                newItem.OnHand = Convert.ToDecimal(reader["OnHand"]);
                                newItem.OnOrders = Convert.ToDecimal(reader["OnOrder"]);
                                newItem.StockValue = Convert.ToDecimal(reader["StockValue"]);
                                newItem.VatLiable = Convert.ToChar(reader["VatLiable"]);
                                newItem.BalInvntAc=reader["BalInvntAc"].ToString();
                                newItem.RevenuesAc = reader["RevenuesAc"].ToString();
                                newItem.SaleCostAc = reader["SaleCostAc"].ToString();
                                newItem.ExpensesAc = reader["ExpensesAc"].ToString();
                                newItem.TransferAc = reader["TransferAc"].ToString();
                                newItem.EvalSystem = reader["EvalSystem"].ToString();
                                newItem.UgpEntry =Convert.ToInt32(reader["UgpEntry"].ToString());
                                newItem.InvntryUomCode = reader["InvntryUomCode"].ToString();
                                newItem.InvntryUomName = reader["InvntryUomName"].ToString();

                                try
                                {
                                    newItem.InvntryUomEntry = Convert.ToInt32(reader["InvntryUomEntry"]);
                                }
                                catch (Exception ex)
                                {
                                    newItem.InvntryUomEntry = 0;
                                }

                                try
                                {
                                    newItem.NumInCnt = Convert.ToDecimal(reader["NumInCnt"].ToString());
                                }
                                catch (Exception ex)
                                {
                                    newItem.NumInCnt = 1;
                                }

                                newListaItem.Add(newItem);

                            }

                            reader.Close();
                        }
                    }

                }

                Connection.Close();

                return Tuple.Create(newListaItem, error);

            }
            catch (Exception e)
            {
                
                Connection.Close();

                return Tuple.Create(newListaItem, e.Message);
            }
        }

        public Tuple<int, string> InsertItems(List<Articulos> listaArticulo)
        {
            int flag = 0;

            string error = null;
           
            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Articulos item in listaArticulo)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "InsertItem";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                            cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                            cmd.Parameters.AddWithValue("@PrchseItem", item.PrchseItem);
                            cmd.Parameters.AddWithValue("@SellItem", item.SellItem);
                            cmd.Parameters.AddWithValue("@InvnItem", item.InvnItem);
                            cmd.Parameters.AddWithValue("@OnHand", item.OnHand);
                            cmd.Parameters.AddWithValue("@OnOrders", item.OnOrders);
                            cmd.Parameters.AddWithValue("@IsCommited", item.IsCommited);
                            cmd.Parameters.AddWithValue("@StockValue", item.StockValue);
                            cmd.Parameters.AddWithValue("@Deleted", item.Deleted);
                            cmd.Parameters.AddWithValue("@CreateDate", item.UpdateDate1);
                            cmd.Parameters.AddWithValue("@UserSign", item.UserSign);
                            cmd.Parameters.AddWithValue("@VatLiable", item.VatLiable);
                            cmd.Parameters.AddWithValue("@BalInvntAc", item.BalInvntAc);
                            cmd.Parameters.AddWithValue("@SaleCostAc", item.SaleCostAc);
                            cmd.Parameters.AddWithValue("@RevenuesAc", item.RevenuesAc);
                            cmd.Parameters.AddWithValue("@ExpensesAc", item.ExpensesAc);
                            cmd.Parameters.AddWithValue("@TransferAc", item.TransferAc);
                            cmd.Parameters.AddWithValue("@EvalSystem", item.EvalSystem);
                            cmd.Parameters.AddWithValue("@UgpEntry", item.UgpEntry);
                            cmd.Parameters.AddWithValue("@InvntryUomCode", item.InvntryUomCode);
                            cmd.Parameters.AddWithValue("@InvntryUomName", item.InvntryUomName);
                            cmd.Parameters.AddWithValue("@NumInCnt", item.NumInCnt);
                            cmd.Parameters.AddWithValue("@InvntryUomEntry", item.InvntryUomEntry);


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

        public Tuple<int, string> UpdateItems(List<Articulos> listaArticulo)
        {
            int flag = 0;

            string error = null;

            try
            {

                using (Connection = new SqlConnection(connectionString))
                {

                    Connection.Open();

                    foreach (Articulos item in listaArticulo)
                    {
                        using (SqlCommand cmd = Connection.CreateCommand())
                        {
                            cmd.CommandText = "UpdateItem";

                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@OldItemCode", item.OldItemCode);
                            cmd.Parameters.AddWithValue("@ItemCode", item.ItemCode);
                            cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                            cmd.Parameters.AddWithValue("@PrchseItem", item.PrchseItem);
                            cmd.Parameters.AddWithValue("@SellItem", item.SellItem);
                            cmd.Parameters.AddWithValue("@InvnItem", item.InvnItem);                     
                            cmd.Parameters.AddWithValue("@UpdateDate", item.UpdateDate1);
                            cmd.Parameters.AddWithValue("@UserSign", item.UserSign);
                            cmd.Parameters.AddWithValue("@VatLiable", item.VatLiable);
                            cmd.Parameters.AddWithValue("@BalInvntAc", item.BalInvntAc);
                            cmd.Parameters.AddWithValue("@SaleCostAc", item.SaleCostAc);
                            cmd.Parameters.AddWithValue("@RevenuesAc", item.RevenuesAc);
                            cmd.Parameters.AddWithValue("@ExpensesAc", item.ExpensesAc);
                            cmd.Parameters.AddWithValue("@TransferAc", item.TransferAc);
                            cmd.Parameters.AddWithValue("@EvalSystem", item.EvalSystem);
                            cmd.Parameters.AddWithValue("@UgpEntry", item.UgpEntry);
                            cmd.Parameters.AddWithValue("@InvntryUomCode", item.InvntryUomCode);
                            cmd.Parameters.AddWithValue("@InvntryUomName", item.InvntryUomName);
                            cmd.Parameters.AddWithValue("@NumInCnt", item.NumInCnt);
                            cmd.Parameters.AddWithValue("@InvntryUomEntry", item.InvntryUomEntry);


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
    }
}
