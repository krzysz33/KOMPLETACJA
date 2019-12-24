using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class IHP_WAZENIE_USLUGA_EX : IHP_WAZENIE_USLUGA
    {
        protected KOMPLETACJAEntities _context;
        public IHP_WAZENIE_USLUGA_EX(KOMPLETACJAEntities context, IHP_WAZENIE_USLUGA item)
        {
            this.ID_IHP_KIEROWCA = item.ID_IHP_KIEROWCA;
            this.ID_IHP_KONTRAHENT_ARCH = item.ID_IHP_KONTRAHENT_ARCH;
            this.ID_IHP_SAMOCHOD = item.ID_IHP_SAMOCHOD;
            this.ID_IHP_WAZENIE_USLUGA = item.ID_IHP_WAZENIE_USLUGA;
            this.KIEROWCA_NAZWA = item.KIEROWCA_NAZWA;
            this.KONTRAHENT_NAZWA = item.KONTRAHENT_NAZWA;
            this.NRKWIT = item.NRKWIT;
            this.NRKWITWEW = item.NRKWITWEW;
            this.NRREJ_NAZWA = item.NRREJ_NAZWA;
            this.STATUS = item.STATUS;
            this.UWAGI = item.UWAGI;
            this.WAGA = item.WAGA;
            this.DATACZAS = item.DATACZAS;
            _context = context;
            if (item.ID_IHP_KIEROWCA > 0)
                IHP_KIEROWCA = _context.IHP_KIEROWCA.FirstOrDefault(x => x.ID_IHP_KIEROWCA == item.ID_IHP_KIEROWCA);

            if (item.ID_IHP_SAMOCHOD > 0)
                IHP_SAMOCHOD = _context.IHP_SAMOCHOD.FirstOrDefault(x => x.ID_IHP_SAMOCHOD == item.ID_IHP_SAMOCHOD);
            if (item.ID_IHP_KONTRAHENT_ARCH > 0)
                IHP_KONTRAHENT_ARCH = _context.IHP_KONTRAHENT_ARCH.FirstOrDefault(x => x.ID_IHP_KONTRAHENT_ARCH == item.ID_IHP_KONTRAHENT_ARCH);

        }

        public bool IsChanged {get;set;}

        private IHP_KIEROWCA _ihpKierowca;
        public IHP_KIEROWCA IHP_KIEROWCA {
            get
            {
                return _ihpKierowca;
            }
            set
            {
                    _ihpKierowca = value;
            }
         }
        public IHP_SAMOCHOD IHP_SAMOCHOD { get; set; }
        public  IHP_KONTRAHENT_ARCH  IHP_KONTRAHENT_ARCH { get; set; }
    }
}
