using Datos.ModeloInformesFiscales;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Controlador_Informes_Fiscales
{
    public class ControladorLibros : Negocios
    {
        ModeloLibros cn = new ModeloLibros();
        public Tuple<List<Entidades.PeriodosContables>,string> ConsultaPeriodos()
        {
            var result = cn.ConsultaPeriodos();

            DataTable dt = result.Item1;

            return Tuple.Create(GetPeriodos(dt), result.Item2);
        }

        public Tuple<List<Entidades.PeriodosContables>, string> ConsultaPeriodosForYear(string year)
        {
            var result = cn.ConsultaPeriodosForYear(year);

            DataTable dt = result.Item1;

            return Tuple.Create(GetPeriodos(dt), result.Item2);
        }
        public Tuple<DataTable, string> ExecuteTXT(DateTime date)
        {

            return cn.ExecuteTXT(date);                        

        }

        public List<Entidades.PeriodosContables> GetPeriodos(DataTable dt)
        {
            List<Entidades.PeriodosContables> listaPeriodos = new List<Entidades.PeriodosContables>();

            string Code;

            string Name;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Code = dt.Rows[i]["Code"].ToString();

                Name = dt.Rows[i]["Name"].ToString();

                listaPeriodos.Add(new Entidades.PeriodosContables(Code, Name));
            }

            return listaPeriodos;
        }

        public Tuple<DataTable, string> ExecuteTXTQuincenal(string mes, string anno, string quincena)
        {
            return cn.ExecuteTXTQuincenal(mes, anno,quincena);
        }

        public Tuple<List<Entidades.DateTXT>, string> ConsultaDateTXT()
        {
            var result = cn.ConsultaDateTXT();

            DataTable dt = result.Item1;

            return Tuple.Create(GetDateTXT(dt), result.Item2);
        }

        public Tuple<DataTable, string> ExecuteXML(string code)
        {
            return cn.ExecuteXML(code);
        }

        public List<Entidades.DateTXT> GetDateTXT(DataTable dt)
        {
            List<Entidades.DateTXT> listaDateTXT = new List<Entidades.DateTXT>();

            DateTime RefDate;

            int Registre;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                RefDate = Convert.ToDateTime(dt.Rows[i]["RefDate"]);

                Registre =Convert.ToInt32(dt.Rows[i]["Registre"]);

                listaDateTXT.Add(new Entidades.DateTXT(RefDate, Registre));
            }

            return listaDateTXT;
        }

        public Tuple<List<Entidades.PeriodoISLR>, string> ConsultaPeriodoISLR()
        {
            var result = cn.ConsultaPeriodoISLR();

            DataTable dt = result.Item1;

            return Tuple.Create(GetPeriodoISLR(dt), result.Item2);
        }

        public List<Entidades.PeriodoISLR> GetPeriodoISLR(DataTable dt)
        {
            List<Entidades.PeriodoISLR> listaPeriodoISLR = new List<Entidades.PeriodoISLR>();

            string Code;

            int Quantity;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Code = dt.Rows[i]["Code"].ToString();

                Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);

                listaPeriodoISLR.Add(new Entidades.PeriodoISLR(Code, Quantity));
            }

            return listaPeriodoISLR;
        }

        public List<Entidades.TipoTransaccion> GetTipoTransaccion()
        {
            List<Entidades.TipoTransaccion> listaTipoTrasaccion = new List<Entidades.TipoTransaccion>();

            string Code;

            string Name;

            Code = "01";

            Name = "Factura / Nota Debito";

            listaTipoTrasaccion.Add(new Entidades.TipoTransaccion(Code, Name));

            Code = "02";

            Name = "Nota Credito";

            listaTipoTrasaccion.Add(new Entidades.TipoTransaccion(Code, Name));

            return listaTipoTrasaccion;
        }

        public Tuple<List<Entidades.ComprobanteIVA>, string> ConsultaComprobantesIVA()
        {
            var result = cn.ConsultaComprobantesIVA();

            DataTable dt = result.Item1;

            return Tuple.Create(GetComprobantesIVA(dt), result.Item2);
        }

        public Tuple<List<Entidades.ComprobanteIVA>, string> ConsultaComprobantesIVAAnulados()
        {
            var result = cn.ConsultaComprobantesIVAAnulados();

            DataTable dt = result.Item1;

            return Tuple.Create(GetComprobantesIVA(dt), result.Item2);
        }

        public List<Entidades.ComprobanteIVA> GetComprobantesIVA(DataTable dt)
        {
            List<Entidades.ComprobanteIVA> listaComprobantesIVA = new List<Entidades.ComprobanteIVA>();

            string U_IDA_NroComp;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                U_IDA_NroComp = dt.Rows[i]["U_IDA_NroComp"].ToString();

                listaComprobantesIVA.Add(new Entidades.ComprobanteIVA(U_IDA_NroComp));
            }

            return listaComprobantesIVA;
        }

        public Tuple<List<Entidades.ComprobanteISLR>, string> ConsultaComprobantesISLR()
        {
            var result = cn.ConsultaComprobantesISLR();

            DataTable dt = result.Item1;

            return Tuple.Create(GetComprobantesISLR(dt), result.Item2);
        }

        public List<Entidades.ComprobanteISLR> GetComprobantesISLR(DataTable dt)
        {
            List<Entidades.ComprobanteISLR> listaComprobantesISLR = new List<Entidades.ComprobanteISLR>();

            string U_IDA_NroComp;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                U_IDA_NroComp = dt.Rows[i]["U_IDA_NroComp"].ToString();

                listaComprobantesISLR.Add(new Entidades.ComprobanteISLR(U_IDA_NroComp));
            }

            return listaComprobantesISLR;
        }
    }
}
