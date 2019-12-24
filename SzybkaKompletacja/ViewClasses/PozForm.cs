using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
 
      public partial class PozForm
    {
        public int ID_IHP_POZDOK { get; set; }
        public int ID_IHP_KARTOTEKA { get; set; }
        public int ID_IHP_NAGLDOK { get; set; }
        public int LP { get; set; }
        public decimal ILOSC { get; set; }
        public string NAZWASKRPOZ { get; set; }
        public decimal CENANETTO { get; set; }
        public decimal CENABRUTTO { get; set; }
        public decimal WARTVAT { get; set; }
        public decimal WARTNETTO { get; set; }
        public decimal WARTBRUTTO { get; set; }
        public System.DateTime DATADOK { get; set; }
        public short CENAUSTALONA { get; set; }
        public string UWAGI { get; set; }
        public int INDEKS { get; set; }

 
    }
}
