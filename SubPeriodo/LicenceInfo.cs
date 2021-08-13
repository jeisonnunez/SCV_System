using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class LicenceInfo
    {
        private int code;

        private string objects;

        private int userSign;

        private DateTime createDate;

        private string server;

        private DateTime fechaInicio;

        private DateTime fechaVencimiento;

        private string hardwareKey;

        private string signature;

        private string dataBase;

        public int Code { get => code; set => code = value; }
        public string Object { get => objects; set => objects = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public string Server { get => server; set => server = value; }
        public DateTime FechaInicio { get => fechaInicio; set => fechaInicio = value; }
        public DateTime FechaVencimiento { get => fechaVencimiento; set => fechaVencimiento = value; }
        public string HardwareKey { get => hardwareKey; set => hardwareKey = value; }
        public string Signature { get => signature; set => signature = value; }
        public string DataBase { get => dataBase; set => dataBase = value; }

        public LicenceInfo()
        {

        }
    }
}
