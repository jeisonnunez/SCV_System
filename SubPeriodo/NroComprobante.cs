using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class NroComprobante
    {
        private string oldCode;

        private string code;

        private int docEntry;

        private char canceled;

        private int userSign;
       
        private DateTime? updateDate;

        private string nombreSerie;

        private string descripcion;

        private string tipoSerie;

        private int inicio;

        private int siguiente;

        private int fin;

        private char activo;

        public string Code { get => code; set => code = value; }
        public int DocEntry { get => docEntry; set => docEntry = value; }
        public char Canceled { get => canceled; set => canceled = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime? UpdateDate { get => updateDate; set => updateDate = value; }
        public string NombreSerie { get => nombreSerie; set => nombreSerie = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public string TipoSerie { get => tipoSerie; set => tipoSerie = value; }
        public int Inicio { get => inicio; set => inicio = value; }
        public int Siguiente { get => siguiente; set => siguiente = value; }
        public int Fin { get => fin; set => fin = value; }
        public char Activo { get => activo; set => activo = value; }
        public string OldCode { get => oldCode; set => oldCode = value; }
    }
}
