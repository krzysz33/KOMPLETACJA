using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.ModelViewsEx
{
    public class IHP_ZAM_USERS_EX : IHP_ZAM_USERS, INotifyPropertyChanged
    {
        protected KOMPLETACJAEntities _context;
        public IHP_ZAM_USERS_EX(KOMPLETACJAEntities context, IHP_ZAM_USERS item)
        {
            _context = context;
            this.AKTYWNY = item.AKTYWNY;
            this.DANEFIRMY = item.DANEFIRMY;
            this.HASLO = item.HASLO;
            this.ID_IHP_ZAM_USERS = item.ID_IHP_ZAM_USERS;
            this.ID_UZYTKOWNIK = item.ID_UZYTKOWNIK;
            this.KARTOTEKI = item.KARTOTEKI;
            this.KIEROWCY = item.KIEROWCY;
            this.KONTRAHENT = item.KONTRAHENT;
            this.LOGIN = item.LOGIN;
            this.NAZWISKO_IMIE = item.NAZWISKO_IMIE;
            this.POJAZDY = item.POJAZDY;
            this.REJWAGA = item.REJWAGA;

            if (item.REJWAGA == 1)
                REJWAGAEX = true;
            if (item.REJWAGA == 0)
                REJWAGAEX = false;

            if (item.USLUGA == 1)
                USLUGAEX = true;
            if (item.USLUGA == 0)
                USLUGAEX = false;

            if (item.KIEROWCY == 1)
                KIEROWCYEX = true;
            if (item.KIEROWCY == 0)
                KIEROWCYEX = false;


            if (item.KARTOTEKI == 1)
                KARTOTEKIEX = true;
            if (item.KARTOTEKI == 0)
                KARTOTEKIEX = false;


            if (item.POJAZDY == 1)
                POJAZDYEX = true;
            if (item.POJAZDY == 0)
                POJAZDYEX = false;


            if (item.KONTRAHENT == 1)
                KONTRAHENTEX = true;
            if (item.KONTRAHENT == 0)
                KONTRAHENTEX = false;

            if (item.DANEFIRMY == 1)
                DANEFIRMYEX = true;
            if (item.DANEFIRMY == 0)
                DANEFIRMYEX = false;


            this.RESET_HASLA = item.RESET_HASLA;
            this.USLUGA = item.USLUGA;
         }
        private bool _rejwagaex;
    public bool  REJWAGAEX { get; set; }
        public bool USLUGAEX { get; set; }
        public bool  KIEROWCYEX { get; set; }
        public bool KARTOTEKIEX { get; set; }
        public bool  POJAZDYEX { get; set; }
        public bool KONTRAHENTEX { get; set; }
        public bool DANEFIRMYEX { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RisePropertyChanged2(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
