using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class DokumentMessage
    {
        public int IdMessage { get; set; }
        public int IdWazenie { get; set; }
        public int IdNaglDok { get; set; }
        public int StatusWazenia { get; set; }
        public string NrDokWew { get; set; }
        public List<IHP_NAGLDOK> Dokumenty { get; set; }
     }
}
