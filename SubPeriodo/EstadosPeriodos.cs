using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class EstadosPeriodos
    {
        private string periodStatCode;

        private string periodStatName;

        public string PeriodStatCode { get => periodStatCode; set => periodStatCode = value; }
        public string PeriodStatName { get => periodStatName; set => periodStatName = value; }
        public EstadosPeriodos(string periodStatCode, string periodStatName)
        {
            this.PeriodStatCode = periodStatCode;
            this.PeriodStatName = periodStatName;
        }

        public EstadosPeriodos()
        {
           
        }


    }
}
