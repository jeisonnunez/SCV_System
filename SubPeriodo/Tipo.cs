using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Tipo
    {
        private string code;

        private string name;
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }

        public Tipo(string Code, string Name)
        {
            this.Code = Code;

            this.Name = Name;
        }

        public Tipo()
        {

        }
    }
}
