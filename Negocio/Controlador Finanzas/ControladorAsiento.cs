using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;


namespace Negocio
{
    public class ControladorAsiento : ControladorDocumento
    {
        ModeloAsiento cn = new ModeloAsiento();
        public Tuple <List<AsientoCabecera>, string> FindLastJournalEntry()
        {
            return cn.FindLastJournalEntry();
        }

        public Tuple<List<AsientoCabecera>, string> FindNextJournalEntry(string transid)
        {
            return cn.FindNextJournalEntry(transid);
        }

        public Tuple <DataTable,string> FindJournalEntryLines(int transid)
        {
            DataTable dtClone;

            var result = cn.FindJournalEntryLines(transid);

            dtClone =ChangeTypeColumn(result.Item1);

            return Tuple.Create(dtClone,result.Item2);
        }

        private DataTable ChangeTypeColumn(DataTable dataTable)
        {
            DataTable dtCloned = dataTable.Clone();

            dtCloned.Columns[3].DataType = typeof(string); //Debit
            dtCloned.Columns[4].DataType = typeof(string); //Credit
            dtCloned.Columns[5].DataType = typeof(string); //SysDeb
            dtCloned.Columns[6].DataType = typeof(string); //SysCred
            dtCloned.Columns[7].DataType = typeof(string); //FCDebit
            dtCloned.Columns[8].DataType = typeof(string); //FCCredit
            dtCloned.Columns.Add("AcctName");

            foreach (DataRow row in dataTable.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return dtCloned;
        }

        public Tuple <List<AsientoCabecera>,string> FindPreviousJournalEntry(string transid)
        {
            return cn.FindPreviousJournalEntry(transid);
        }

        public Tuple<List<AsientoCabecera>, string> FindFirstJournalEntry()
        {
            return cn.FindFirstJournalEntry();
        }

        public Tuple<List<AsientoCabecera>, string> ConsultaJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            return cn.ConsultaJournalEntry(listaJournalEntry);
        }

        public Tuple<List<AsientoCabecera>, string> FindJournalEntrySpecific(int TransId)
        {
            return cn.FindJournalEntrySpecific(TransId);
        }

        public Tuple<int,string> InsertJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            return cn.InsertJournalEntry(listaJournalEntry);
        }

        public Tuple<int, string> InsertJournalEntryLines(List<AsientoDetalle> listaJournalEntryLines)
        {
            return cn.InsertJournalEntryLines(listaJournalEntryLines);
        }

        public Tuple<int, string> InsertJournalEntryPreliminar(List<AsientoCabecera> listaJournalEntry)
        {
            return cn.InsertJournalEntryPreliminar(listaJournalEntry);
        }

        public Tuple<int, string> InsertJournalEntryPreliminarAlternativo(List<AsientoCabecera> listaJournalEntry)
        {
            return cn.InsertJournalEntryPreliminarAlternativo(listaJournalEntry);
        }

        public Tuple<int, string> InsertJournalEntryLinesPreliminar(List<AsientoDetalle> listaJournalEntryLines)
        {
            return cn.InsertJournalEntryLinesPreliminar(listaJournalEntryLines);
        }

        public Tuple<int, string> InsertJournalEntryLinesPreliminarAlternativo(List<AsientoDetalle> listaJournalEntryLines)
        {
            return cn.InsertJournalEntryLinesPreliminarAlternativo(listaJournalEntryLines);
        }


        public Tuple<int, string> UpdateJournalEntry(List<AsientoCabecera> listaJournalEntry)
        {
            return cn.UpdateJournalEntry(listaJournalEntry);
        }

        public Tuple<int, string> UpdateJournalEntryLines(List<AsientoDetalle> listaJournalEntryLines)
        {
            return cn.UpdateJournalEntryLines(listaJournalEntryLines);
        }

        public Tuple<int,string> InsertJournalEntryPreliminarTest(List<AsientoCabecera> listaJournalEntryDef)
        {
            return cn.InsertJournalEntryPreliminarTest(listaJournalEntryDef);
        }

        public Tuple<int,string> InsertJournalEntryLinesPreliminarTest(List<AsientoDetalle> listaJournalEntryLinesDef)
        {
            return cn.InsertJournalEntryLinesTest(listaJournalEntryLinesDef);
        }

        public Tuple<int,string> InsertJournalEntryComplete(List<AsientoCabecera> listJournalEntry, List<AsientoDetalle> listJournalEntryLines)
        {
            return cn.InsertJournalEntryComplete(listJournalEntry, listJournalEntryLines);
        }

        public string checkidentOjdt(int identiny)
        {
            return cn.checkidentOjdt(identiny);
        }
    }
}
