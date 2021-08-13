using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class MetodoValoracion
    {
        private string description;

        private string evalSystem;
        public string Description { get => description; set => description = value; }
        public string EvalSystem { get => evalSystem; set => evalSystem = value; }

        public MetodoValoracion(string description, string EvalSystem)
        {
            this.Description = description;
            this.EvalSystem = EvalSystem;
        }
    }
}
