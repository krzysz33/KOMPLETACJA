using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KpInfohelp
{
    public class ZamowieniaViewStatusNaglKafelek
    {
        public int LP { get; set; }
        public int WYMIARX { get; set; }
        public int WYMIARY { get; set; }
         public string INDEKS { get; set; }
        public int ID_STATUSHISTORIA { get; set; }
        public int ID_DEFSTATUS { get; set; }
        public DateTime DATAWPISU { get; set; }
        public string NAZWA { get; set; }
        public string OPIS { get; set; }
        public int ID_POZ { get; set; }
        public int ID_ARIT_ZAM_USERS { get; set; }
        public int ID_DEFSTRATUS { get; set; }
         public string NAZWAZ { get; set; }
        public string UZYTKOWNIK { get; set; }
    }
    public class ZamowieniaViewStatusNagl
    {
        public string INDEKS { get; set; }
        public int ID_STATUSHISTORIA { get; set; }
        public int ID_DEFSTATUS { get; set; }
        public DateTime DATAWPISU { get; set; }
        public string NAZWA { get; set; }
        public string OPIS { get; set; }
        public int ID_POZ { get; set; }
        public int ID_ARIT_ZAM_USERS { get; set; }
        public string NAZWAZ { get; set; }
        public string UZYTKOWNIK { get; set; }
    }
    public class ZamowieniaViewStatus
    {
        public int ID_STATUSHISTORIA { get; set; }
        public int ID_DEFSTATUS { get; set; }
        public DateTime DATAWPISU { get; set; }
        public string NAZWA { get; set; }
        public string OPIS { get; set; }
        public int ID_POZ { get; set; }
        public int ID_ARIT_ZAM_USERS { get; set; }
        public int ID_DEFSTRATUSZ { get; set; }
        public string NAZWAZ { get; set; }
        public string UZYTKOWNIK { get; set; }
    }
    public class ZamowieniaViewSearch
    {
        public int ID_NAGL { get; set; }
        public DateTime DATADOK { get; set; }
        public DateTime TERMINREALIZ { get; set; }
        public string NRDOKWEW { get; set; }
        public string NAZWISKO { get; set; }
        public string IMIE { get; set; }
        public string TELKOM { get; set; }
        public string TELEFON { get; set; }
        public string EMAIL { get; set; }

    }
    public class ZamowieniaView
    {
        public Nullable<int> ID_NAGL { get; set; }
        public Nullable<int> ID_KONTRAH { get; set; }
        public Nullable<int> ID_DANEKONTRAH { get; set; }
        public Nullable<System.DateTime> DATADOK { get; set; }
        public string NRDOKWEW { get; set; }
        public string NRDOKZEW { get; set; }
        public Nullable<int> ID_POZ { get; set; }
        public Nullable<int> LP { get; set; }
        public decimal ILOSC { get; set; }
        public decimal CENANETTO { get; set; }
        public Nullable<int> ID_KARTOTEKA { get; set; }
        public string INDEKS { get; set; }
    }
    public class ZamowieniaViewListaExp : INotifyPropertyChanged
    {
        [DisplayName("Data Dok.")]
        public Nullable<System.DateTime> DATADOK { get; set; }

        [DisplayName("Termin Re.")]
        public Nullable<System.DateTime> TERMINREALIZ { get; set; }

        [DisplayName("Numer Zam.")]
        public string NRDOKWEW { get; set; }

        [DisplayName("Ilość")]
        public decimal ILOSC { get; set; }

        [DisplayName("Ilość R.")]
        public decimal ILOSCREALIZ { get; set; }

        [DisplayName("Indeks")]
        public string INDEKS { get; set; }

         public event PropertyChangedEventHandler PropertyChanged;
         protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }

    public class ZamowieniaViewListapozExpfrm : INotifyPropertyChanged
    {

        [DisplayName("id_poz")]
        public int ID_IHP_POZDOK { get; set; }


        [DisplayName("Lp.")]
        public int LP { get; set; }

        [DisplayName("Indeks")]
        public string INDEKS { get; set; }

        [DisplayName("Ilość")]
        public decimal ILOSC { get; set; }


        [DisplayName("Cena")]
        public decimal CENA { get; set; }

        [DisplayName("Wartość")]
        public decimal WARTOSC { get; set; }


        [DisplayName("Uwagi")]
        public string UWAGI { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
    public class ZamowieniaViewLista
    {
        public Nullable<int> ID_NAGL { get; set; }
        public Nullable<int> ID_KONTRAH { get; set; }
        public Nullable<int> ID_DANEKONTRAH { get; set; }
        public Nullable<int> ID_GRUPAKART { get; set; }
        [DisplayName("Data Dok.")]
        public Nullable<System.DateTime> DATADOK { get; set; }

        [DisplayName("Termin Re.")]
        public Nullable<System.DateTime> TERMINREALIZ { get; set; }
        [DisplayName("Numer Zam.")]
        public string NRDOKWEW { get; set; }
        public Nullable<int> ID_POZ { get; set; }
        public Nullable<int> ID_POZNADRZ { get; set; }
        // public Nullable<int> ID_DEFSTATUS { get; set; }
        public int ID_DEFSTATUS { get; set; }
        [DisplayName("Lp")]
        public Nullable<int> LP { get; set; }
        [DisplayName("Ilość")]
        public decimal ILOSC { get; set; }
        [DisplayName("Ilość R.")]
        public decimal ILOSCRAZEM { get; set; }
        public decimal CENANETTO { get; set; }
        public Nullable<int> ID_KARTOTEKA { get; set; }
        [DisplayName("Nazw Pozycji")]
        public string INDEKS { get; set; }
        [DisplayName("Kontr.")]
        public string NAZWAKTH { get; set; }
        [DisplayName("X")]
        public int WYMIARX { get; set; }
        [DisplayName("Y")]
        public int WYMIARY { get; set; }
        [DisplayName("Stat.")]
        public string STATUS { get; set; }
        [DisplayName("Uw.")]
        public string UWAGI { get; set; }
        [DisplayName("Plik")]
        public string NAZWAPLIKU { get; set; }
    }
    public class ZamowieniaViewListaNagl:INotifyPropertyChanged
    {
        protected void RisePropertyChangedZazn(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private bool _zaznaczenie;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Zaznaczenie
        {
            get
            {
                return _zaznaczenie;
            }

            set

            {
                _zaznaczenie = value;
                RisePropertyChangedZazn("Zaznaczenie");
            }
        }

        public Nullable<int> ID_NAGL { get; set; }
        public Nullable<int> ID_KONTRAH { get; set; }
        [DisplayName("Data Dok.")]
        public Nullable<System.DateTime> DATADOK { get; set; }
        [DisplayName("Termin Re.")]
        public Nullable<System.DateTime> TERMINREALIZ { get; set; }
        [DisplayName("Numer Zam.")]
        public string NRDOKWEW { get; set; }
        public string IMIE { get; set; }
        public string NAZWISKO { get; set; }
        [DisplayName("Uw.")]
        public string UWAGI { get; set; }
        public int STATUSZAM { get; set; }
    }
  }
 
    
 
