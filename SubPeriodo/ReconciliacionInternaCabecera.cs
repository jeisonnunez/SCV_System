using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class ReconciliacionInternaCabecera
    {
        private int reconNum;

        private char isCard;

        private int reconType;

        private DateTime reconDate;

        private decimal total;

        private string reconCurr;

        private char canceled;

        private int cancelAbs;

        private char isSystem;

        private string initObjTyp;

        private int initObjAbs;

        private DateTime createDate;

        private int userSign;

        private int reconJEId;

        private string objType;

        public int ReconNum { get => reconNum; set => reconNum = value; }
        public char IsCard { get => isCard; set => isCard = value; }
        public int ReconType { get => reconType; set => reconType = value; }
        public DateTime ReconDate { get => reconDate; set => reconDate = value; }
        public decimal Total { get => total; set => total = value; }
        public string ReconCurr { get => reconCurr; set => reconCurr = value; }
        public char Canceled { get => canceled; set => canceled = value; }
        public int CancelAbs { get => cancelAbs; set => cancelAbs = value; }
        public char IsSystem { get => isSystem; set => isSystem = value; }
        public string InitObjTyp { get => initObjTyp; set => initObjTyp = value; }
        public int InitObjAbs { get => initObjAbs; set => initObjAbs = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int UserSign { get => userSign; set => userSign = value; }
        public int ReconJEId { get => reconJEId; set => reconJEId = value; }
        public string ObjType { get => objType; set => objType = value; }
    }
}
