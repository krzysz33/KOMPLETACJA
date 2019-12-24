using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace KpInfohelp
{

    public class CennikViewHist
    {

        public int ID_CENNIKHISTORIA { get; set; }
        public int ID_CENNIK { get; set; }
        public Nullable<System.DateTime> DATAWPISU { get; set; }
        public decimal CENAZ { get; set; }
        public decimal CENANA { get; set; }
        public int ID_ARIT_ZAM_USERS { get; set; }
        public string UZYTKOWNIK { get; set; }
    }
    public class CennikViewKart : INotifyPropertyChanged
    {
        public decimal _cenan;
        private decimal _cenab;
        private int _vat;
        public Nullable<int> ID_IHP_CENNIK { get; set; }
        public Nullable<int> ID_IHP_DEFCENY { get; set; }
        public Nullable<int> ID_IHP_GRUPAKART { get; set; }


        [DisplayName("Nazwa Cennika")]
        public string Nazwa { get; set; }

        [DisplayName("Stawka VAT")]
        public int VAT
        {
            get
            {
                return _vat;
            }
            set
            {
                _vat = value;
                //     RisePropertyChanged("VAT");
            }
        }

        [DisplayName("Cena Netto")]
        public decimal CENAN
        {
            get
            {
                return _cenan;
            }
            set
            {
                _cenan = value;
                RisePropertyChanged("CENAN");
            }
        }
        [DisplayName("Cena brutto")]
        public decimal CENAB
        {
            get
            {
                return _cenab;
            }
            set
            {
                _cenab = value;
                RisePropertyChanged("CENAB");
            }
        }

        public Nullable<int> ID_IHP_KARTOTEKA { get; set; }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class CennikView : INotifyPropertyChanged
    {
        public decimal _cenan;
        private decimal _cenab;
        private int _vat;
        public Nullable<short> ODNETTO { get; set; }
        public Nullable<int> ID_IHP_CENNIK { get; set; }
        public Nullable<int> ID_IHP_DEFCENY { get; set; }
        public Nullable<int> ID_IHP_GRUPAKART { get; set; }

        [DisplayName("Nazwa Grupy")]
        public string NAZWAGRUPY { get; set; }

        [DisplayName("Nazwa Skr.")]
        public string NAZWASKR { get; set; }
        [DisplayName("Indeks")]
        public string INDEKS { get; set; }

        [DisplayName("Stawka VAT")]
        public int VAT
        {
            get
            {
                return _vat;
            }
            set
            {
                _vat = value;
           //     RisePropertyChanged("VAT");
            }
        }

        [DisplayName("Cena Netto")]
          public decimal CENAN {
            get
            {
                return _cenan;
            }
              set
            {
                _cenan = value;
                RisePropertyChanged("CENAN");
            }
       }
        [DisplayName("Cena brutto")]
        public decimal CENAB {
            get
            {
                return _cenab;
            }
              set
            {
               _cenab = value;
            RisePropertyChanged("CENAB");
            }
        }
  
        public Nullable<int> ID_IHP_KARTOTEKA { get; set; }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
