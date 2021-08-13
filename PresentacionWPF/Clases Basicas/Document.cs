using BeautySolutions.View.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Vista
{
    public class Document:NumericConfiguration
    {
        private dynamic screen = null;

        protected void GetPresentationPreliminarJournalEntry(List<Entidades.AsientoCabecera> journalEntry, DataTable journalEntryLines)
        {
            foreach (SubItem subItem in Menu.listMenus)
            {
                if (subItem.Screen.GetType().FullName == "Vista.Asiento")
                {
                    screen = subItem.Screen;
                }
               
            }

            if (screen != null)
            {
                
               DoubleAnimation animation = new DoubleAnimation(0, 1,
                                (Duration)TimeSpan.FromSeconds(1));
               screen.BeginAnimation(UIElement.OpacityProperty, animation);

               if (screen.Visibility == Visibility.Collapsed || screen.Visibility == Visibility.Hidden)
               {
                    screen.Show();

                    screen.LoadedWindow();

                    screen.ReestablecerFondo();

                    screen.DisableElements();

                    screen.GetJournalEntry(journalEntry);

                    screen.GetJournalEntryLines(journalEntryLines);
               }


            }
        }

        public Document(): base()
        {

        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            // Validate all the bindings on the children
            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child)) { return false; }
            }

            return true;
        }

        protected DataTable ChangeNameColumnDatatable(DataTable dt)
        {
            dt.Columns["SysCred"].ColumnName = "SYSCred";

            dt.Columns["SysDeb"].ColumnName = "SYSDeb";

            return dt;
        }

        protected static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        protected DataTable GroupByListItem(DataTable dtArticuloDetalleNew)
        {
            var result = dtArticuloDetalleNew.AsEnumerable()
            .GroupBy(row => new
            {

                ItemCode = row.Field<string>("ItemCode"),
                Currency = row.Field<string>("Currency"),
                Rate = row.Field<decimal>("Rate"),
                SysRate = row.Field<decimal>("SysRate")
            })
            .Select(g =>
            {
                var row = g.First();

                row.SetField("OpenQty", g.Sum(r => r.Field<decimal>("OpenQty")));
                row.SetField("CalcPrice", g.Sum(r => r.Field<decimal>("CalcPrice")));



                return row;
            });

            var resultTable = result.CopyToDataTable();

            return resultTable;
        }


        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;

            if (null == itemsSource)
            {
                yield return null;
            }

            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;

                if (null != row)
                {
                    yield return row;
                }
            }
        }

        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);

            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                T childType = child as T;

                if (childType == null)
                {
                    foundChild = FindChild<T>(child, childName);

                    if (foundChild != null) break;
                }
                else
                    if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;

                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        foundChild = FindChild<T>(child, childName);

                        if (foundChild != null)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        protected static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

       
        protected static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {

                    obj.ToString();

                    dr[column.ColumnName].ToString();

                    if (pro.Name == column.ColumnName)
                    {
                        if (String.IsNullOrWhiteSpace(dr[column.ColumnName].ToString())==true)
                        {
                            pro.SetValue(obj, "", null);
                        }
                        else
                        {
                            pro.SetValue(obj, dr[column.ColumnName], null);
                        }
                        
                    }
                    else
                    {

                    }                        
                        continue;
                }
            }
            return obj;
        }
    }

    
}
