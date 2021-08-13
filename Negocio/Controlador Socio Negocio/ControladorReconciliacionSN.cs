using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Datos.Modelo_Socio_Negocio;
using Entidades;

namespace Negocio.Controlador_Socio_Negocio
{
    public class ControladorReconciliacionSN : ControladorReportes
    {
        ModeloReconciliacionSN cn = new ModeloReconciliacionSN();
        public Tuple<DataTable,string> ExecuteReconciliacionSN(string bp, DateTime? F_RefDate=null, DateTime? T_RefDate=null)
        {
            DataTable newDt = new DataTable();

            DataTable dtClone;

            var result = cn.ExecuteReconciliacionSN(bp, F_RefDate, T_RefDate);

            newDt = result.Item1.Copy();

            dtClone = AddColumnAccount(newDt);

            dtClone = ChangeTypeColumn(dtClone);

            dtClone = SetTransType(dtClone);

            return Tuple.Create(dtClone, result.Item2);
        }

        public Tuple<int, string> SelectReconNum()
        {
            return cn.SelectReconNum();
        }

        public int GetReconType(string reconType)
        {
            int tipoRecon = 0;

            if (reconType == ReconType.Manual.ToString())
            {
                tipoRecon = (int)ReconType.Manual;

            }
            else if (reconType == ReconType.Automatica.ToString())
            {
                tipoRecon = (int)ReconType.Automatica;
            }

            else if (reconType == ReconType.SemiAutomatica.ToString())
            {
                tipoRecon = (int)ReconType.SemiAutomatica;
            }

            else if (reconType == ReconType.Pago.ToString())
            {
                tipoRecon = (int)ReconType.Pago;
            }

            else if (reconType == ReconType.NotaCredito.ToString())
            {
                tipoRecon = (int)ReconType.NotaCredito;
            }

            else if (reconType == ReconType.Reversion.ToString())
            {
                tipoRecon = (int)ReconType.Reversion;
            }

            else if (reconType == ReconType.ValorCero.ToString())
            {
                tipoRecon = (int)ReconType.ValorCero;
            }

            else if (reconType == ReconType.Cancelacion.ToString())
            {
                tipoRecon = (int)ReconType.Cancelacion;
            }

            else if (reconType == ReconType.BoE.ToString())
            {
                tipoRecon = (int)ReconType.BoE;
            }

            else if (reconType == ReconType.Deposito.ToString())
            {
                tipoRecon = (int)ReconType.Deposito;
            }

            else if (reconType == ReconType.ExtractoBancario.ToString())
            {
                tipoRecon = (int)ReconType.ExtractoBancario;
            }



            else if (reconType == ReconType.PeriodoCierre.ToString())
            {
                tipoRecon = (int)ReconType.PeriodoCierre;
            }

            else if (reconType == ReconType.FacturaCorreccion.ToString())
            {
                tipoRecon = (int)ReconType.FacturaCorreccion;
            }

            else if (reconType == ReconType.Inventario.ToString())
            {
                tipoRecon = (int)ReconType.Inventario;
            }

            else if (reconType == ReconType.WIP.ToString())
            {
                tipoRecon = (int)ReconType.WIP;
            }

            else if (reconType == ReconType.ImpuestoDiferido.ToString())
            {
                tipoRecon = (int)ReconType.ImpuestoDiferido;
            }

            else if (reconType == ReconType.AsignacionAnticipo.ToString())
            {
                tipoRecon = (int)ReconType.AsignacionAnticipo;
            }

            else if (reconType == ReconType.DiferenciaConversion.ToString())
            {
                tipoRecon = (int)ReconType.DiferenciaConversion;
            }

            else if (reconType == ReconType.DocumentoProvisional.ToString())
            {
                tipoRecon = (int)ReconType.DocumentoProvisional;
            }


            return tipoRecon;
        }

       

        public string GetReconType(int reconType)
        {
            string tipoRecon = null;            

            if (reconType == (int)ReconType.Manual)
            {
                tipoRecon = ReconType.Manual.ToString();

            }
            else if (reconType == (int)ReconType.Automatica)
            {
                tipoRecon = ReconType.Automatica.ToString();
            }

            else if (reconType == (int)ReconType.SemiAutomatica)
            {
                tipoRecon =ReconType.SemiAutomatica.ToString();
            }

            else if (reconType == (int)ReconType.Pago)
            {
                tipoRecon = ReconType.Pago.ToString();
            }

            else if (reconType == (int)ReconType.NotaCredito)
            {
                tipoRecon = ReconType.NotaCredito.ToString();
            }

            else if (reconType == (int)ReconType.Reversion)
            {
                tipoRecon = ReconType.Reversion.ToString();
            }

            else if (reconType == (int)ReconType.ValorCero)
            {
                tipoRecon = ReconType.ValorCero.ToString();
            }

            else if (reconType == (int)ReconType.Cancelacion)
            {
                tipoRecon = ReconType.Cancelacion.ToString();
            }

            else if (reconType == (int)ReconType.BoE)
            {
                tipoRecon = ReconType.BoE.ToString();
            }

            else if (reconType == (int)ReconType.Deposito)
            {
                tipoRecon = ReconType.Deposito.ToString();
            }

            else if (reconType == (int)ReconType.ExtractoBancario)
            {
                tipoRecon = ReconType.ExtractoBancario.ToString();
            }


            else if (reconType == (int)ReconType.PeriodoCierre)
            {
                tipoRecon = ReconType.PeriodoCierre.ToString();
            }

            else if (reconType == (int)ReconType.FacturaCorreccion)
            {
                tipoRecon = ReconType.FacturaCorreccion.ToString();
            }

            else if (reconType == (int)ReconType.Inventario)
            {
                tipoRecon = ReconType.Inventario.ToString();
            }

            else if (reconType == (int)ReconType.WIP)
            {
                tipoRecon = ReconType.WIP.ToString();
            }

            else if (reconType == (int)ReconType.ImpuestoDiferido)
            {
                tipoRecon = ReconType.ImpuestoDiferido.ToString();
            }

            else if (reconType == (int)ReconType.AsignacionAnticipo)
            {
                tipoRecon = ReconType.AsignacionAnticipo.ToString();
            }

            else if (reconType == (int)ReconType.DiferenciaConversion)
            {
                tipoRecon = ReconType.DiferenciaConversion.ToString();
            }

            else if (reconType == (int)ReconType.DocumentoProvisional)
            {
                tipoRecon = ReconType.DocumentoProvisional.ToString();
            }


            return tipoRecon;
        }


        public Tuple<int, string> InsertReconciliationInternal(List<ReconciliacionInternaCabecera> listaInternalReconciliation)
        {
            return cn.InsertReconciliationInternal(listaInternalReconciliation);
        }

        public enum ReconType { Manual = 0, Automatica = 1, SemiAutomatica = 2, Pago = 3, NotaCredito = 4, Reversion = 5, ValorCero = 6, Cancelacion = 7, BoE = 8, Deposito = 9, ExtractoBancario = 10, PeriodoCierre=11, FacturaCorreccion=12,Inventario=13, WIP=14,ImpuestoDiferido=15, AsignacionAnticipo=16, DiferenciaConversion=17,DocumentoProvisional=18 };

        public Tuple<int,string> InsertReconciliationInternalLines(List<ReconciliacionInternaDetalles> listReconciliationInternalLines)
        {
            return cn.InsertReconciliationInternalLines(listReconciliationInternalLines);
        }

        public Tuple<decimal, string> VerifiedDiferenceReconciliation(int reconNum)
        {
            return cn.VerifiedDiferenceReconciliation(reconNum);
        }

        public Tuple<DataTable,string> GetReconciliationInternalLines(int reconNum)
        {
            return cn.GetReconciliationInternalLines(reconNum);
        }

        public Tuple<int, string> UpdateJournalEntryLines(List<ReconciliacionInternaDetalles> listReconciliationDetails)
        {
            return cn.UpdateJournalEntryLines(listReconciliationDetails);
        }

        public Tuple<int, string> DeleteITR1(int reconNum)
        {
            return cn.DeleteITR1(reconNum);
        }

        public Tuple<int, string> DeleteOITR(int reconNum)
        {
            return cn.DeleteOITR(reconNum);
        }

        public Tuple<int,string> UpdateDocument(ReconciliacionInternaDetalles reconciliacionInternaDetalles)
        {
            return cn.UpdateDocument(reconciliacionInternaDetalles);
        }
    }
}
