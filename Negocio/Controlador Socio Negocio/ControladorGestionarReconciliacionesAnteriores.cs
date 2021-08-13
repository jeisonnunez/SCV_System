using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo_Socio_Negocio;

namespace Negocio.Controlador_Socio_Negocio
{
    public class ControladorGestionarReconciliacionesAnteriores: ControladorReconciliacionSN
    {
        ModeloGestionarReconciliacionesAnteriores cn = new ModeloGestionarReconciliacionesAnteriores();

        public Tuple<DataTable, string> ExecuteReconciliacionesAnteriores(string txtDesdeSN, string txtHastaSN=null, DateTime? dpDFechaReconciliacion=null, DateTime? dpHFechaReconciliacion=null, int txtDNroReconciliacion=0, int txtHNroReconciliacion=0)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.ExecuteReconciliacionesAnteriores(txtDesdeSN, txtHastaSN, dpDFechaReconciliacion,dpHFechaReconciliacion,txtDNroReconciliacion,txtHNroReconciliacion);

            newDt = result.Item1.Copy();

            dtClone = ChangeTypeColumn(newDt);

            dtClone = TraduceReconType(dtClone);

            dtClone = AddReconCurr(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        private DataTable AddReconCurr(DataTable dt)
        {
            string ReconCurr = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "Total")
                    {
                        ReconCurr = row["ReconCurr"].ToString() + " " + row["Total"].ToString();

                        row["Total"] = ReconCurr;

                    }

                }

            }

            return dt;
        }

        private DataTable TraduceReconType(DataTable dt)
        {
            string ReconType = null;            

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "ReconType")
                    {
                        ReconType = GetReconType(Convert.ToInt32(row["ReconType"]));

                        row["ReconType"] = ReconType;

                    }

                }

            }

            return dt;
        }

        public Tuple<DataTable,string> FindReconciliacionesAnterioresLines(int reconNum)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.FindReconciliacionesAnterioresLines(reconNum);

            newDt = result.Item1.Copy();

            dtClone = ChangeTypeColumn(newDt);

            dtClone = TraduceSrcObjTyp(dtClone);

            dtClone = AddReconCurrLines(dtClone);

            return Tuple.Create(dtClone, result.Item2);
           
        }

        private DataTable AddReconCurrLines(DataTable dt)
        {
            string ReconCurr = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "ReconSum")
                    {
                        ReconCurr = row["ReconCurr"].ToString() + " " + row["ReconSum"].ToString();

                        row["ReconSum"] = ReconCurr;

                    }

                }

            }

            return dt;
        }

        private DataTable TraduceSrcObjTyp(DataTable dt)
        {
            string SrcObjTyp = null;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    if (column.ToString() == "SrcObjTyp")
                    {
                        SrcObjTyp = GetTransType(Convert.ToInt32(row["SrcObjTyp"]));

                        row["SrcObjTyp"] = SrcObjTyp;

                    }

                }

            }

            return dt;
        }
    }
}
