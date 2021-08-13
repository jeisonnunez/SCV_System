using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Negocio
{
    public class ControladorTipoCambio: Negocios
    {
        ModeloTipoCambio cn = new ModeloTipoCambio();

        public List<PeriodosContables> ListaYears = new List<PeriodosContables>();

        public List<PeriodosContables> GetYears(DataTable dt)
        {
            List<PeriodosContables> listaYears = new List<PeriodosContables>();

            string year;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                year = dt.Rows[i]["Category"].ToString();

                listaYears.Add(new PeriodosContables(year));
            }

            ListaYears = listaYears;

            return listaYears;
        }

        public List<Meses> GetMeses(DataTable dt)
        {
            List<Meses> listaMeses = new List<Meses>();

            string codigo;

            string mes;


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                codigo = dt.Rows[i]["Code"].ToString();
                mes = dt.Rows[i]["Month"].ToString();

                listaMeses.Add(new Meses(codigo,mes));
            }

            return listaMeses;
        }

        public DataTable AsignaTiposCambio(DataTable dt, DataTable dtTipoCambio)
        {            

            foreach (DataRow row in dtTipoCambio.Rows)
            {
                foreach (DataColumn column in dtTipoCambio.Columns)
                {                    
                     
                    if (column.ToString() == "Currency")
                    {
                        row["RateDate"]= String.Format("{0:yyyy/MM/dd}", row["RateDate"]);

                        string searchExpression="Dia = " + "#" +  String.Format("{0:yyyy/MM/dd}", row["RateDate"]) + "#";

                        DataRow[] find = dt.Select(searchExpression);

                        foreach (DataRow rows in find)
                        {                           
                           int indice= dt.Rows.IndexOf(rows);

                            row["Rate"]= String.Format("{0:#,#.00}", row["Rate"]);

                            dt.Rows[indice][row[column].ToString()] = row["Rate"];
                        }

                    }

                }

            }
           
            return dt;
        }



        public Tuple<DataTable,string> ConsultaTiposCambiosDefinidos()
        {
            var result= cn.ConsultaTiposCambiosDefinidos();

            DataTable tabla = result.Item1;

            return Tuple.Create(tabla,result.Item2);
        }

        public Tuple<List<PeriodosContables>,string> ConsultaYears()
        {
            var result = cn.ConsultaYears();

            DataTable dt = result.Item1;

            return Tuple.Create(GetYears(dt),result.Item2);
        }

        public Tuple<List<Meses>,string> ConsultaMeses()
        {
            var result = cn.ConsultaMeses();

            DataTable dt = result.Item1;

            return Tuple.Create(GetMeses(dt),result.Item2);
        }

        public Tuple<DataTable,string> ConsultaTipoCambio()
        {
            var result = cn.ConsultaTipoCambio();

            DataTable tabla = result.Item1;
          
            tabla = InvierteColumnas(tabla);

            tabla= ContruyeTabla(tabla);

            return Tuple.Create(tabla,result.Item2);
        }

        public Tuple<int,string> InsertaTipoCambio(List<TipoCambio> listaTipoCambio)
        {
            return cn.InsertaTipoCambio(listaTipoCambio);
        }

        private DataTable ContruyeTabla(DataTable tabla)
        {
            tabla.Columns.Add("Dia", typeof(DateTime)).SetOrdinal(0);
            foreach (PeriodosContables year in ListaYears)
            {
            
                var dateString = "1/1/" + year.Category.ToString();
                //var dateString = "1/1/2020 8:30:52 AM";

                int days = GetDaysOfYear(year.Category.ToString());

                DataRow row;

                DateTime? date = DateTime.Parse(dateString, CultureInfo.InvariantCulture);

             
                row = tabla.NewRow();

                row["Dia"] = String.Format("{0:yyyy/MM/dd}", date);

               // row["Dia"] = date;

                tabla.Rows.Add(row);

                for (int i = 0; i < days; i++)
                {
                    row = tabla.NewRow();

                    date = date.Value.AddDays(1);

                    row["Dia"] = String.Format("{0:yyyy/MM/dd}", date);

                   // row["Dia"] = date;

                    tabla.Rows.Add(row);
                }

                
            }

            return tabla;
        }

        public Tuple<bool,string> FindUSDColumn()
        {
            return cn.FindUSDColumn();
        }

        public Tuple<decimal,string> FindRateUSD(DateTime datetimePage)
        {
            return cn.FindRateUSD(datetimePage);
        }

        private int GetDaysOfYear(string year)
        {
            int days;

            if (DateTime.IsLeapYear(Convert.ToInt32(year))==true)
            {
                days = 366;
            }
            else
            {
                days = 365;
            }

            return days;
        }

        public DataTable InvierteColumnas(DataTable dataTable)
        {

            DataTable dt = new DataTable();

            // crear las nuevas columnas (primera columna de todas las filas)
            DataColumn[] newCols = dataTable.Rows.OfType<DataRow>().Select((row) => new DataColumn((string)row[0])).ToArray();

            dt.Columns.AddRange(newCols);

            // crear las filas (el resto de las columnas anteriores)
            for (int i = 1; i < dataTable.Columns.Count; i++)
            {
                // obter la columna correspondiente
                var a = dataTable.Rows.OfType<DataRow>().Select((row) => row[i]).ToArray();

                // añadir la columna anterior como fila
                dt.Rows.Add(a);
            }

            return dt;
        }
       
    }
}
