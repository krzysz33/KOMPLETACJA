using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class IHP_KAROTEKA_EX : IHP_KARTOTEKA, INotifyPropertyChanged
    {
        protected KOMPLETACJAEntities _context;
        public IHP_KAROTEKA_EX()
        {
        }

        public IHP_KAROTEKA_EX(KOMPLETACJAEntities context, IHP_KARTOTEKA item)
        {
            _context = context;
            this.ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA;
            this.ID_IHP_STAWKAVAT = item.ID_IHP_STAWKAVAT;
            this.ID_IHP_RODZAJKART = item.ID_IHP_RODZAJKART;
            this.ID_IHP_JM = item.ID_IHP_JM;
            this.INDEKS = item.INDEKS;
            this.NAZWASKR = item.NAZWASKR;
            this.NAZWADL = item.NAZWADL;
            this.SWW = item.SWW;
            this.PKWIU = item.PKWIU;
            this.UWAGI = item.UWAGI;
            this.KODEAN = item.KODEAN;
            this.ID_TOWSUBJECT = item.ID_TOWSUBJECT;
            this.IHP_CENNIK = item.IHP_CENNIK;
            this.IHP_JM = item.IHP_JM;
            this.IHP_RODZAJKART = item.IHP_RODZAJKART;
            this.IHP_STAWKAVAT = item.IHP_STAWKAVAT;
     
            this.IHP_POZDOK = item.IHP_POZDOK;
  
    
        }
        public bool CenaUzgodniona{ get; set; }
        private string _nrdokpow;
        public string NrDokPow
        {
            get
            {
                return _nrdokpow;
            }
            set
            {
                _nrdokpow = value;
                
            }
        }
 
    
        private bool _zaznaczenie;
        public event PropertyChangedEventHandler PropertyChanged;
        private string _pokazdane;
 
        public bool Zaznaczenie
        {
            get
            {
                return _zaznaczenie;
            }
            set
            {
                _zaznaczenie = value;
               RisePropertyChanged("Zaznaczenie");
            }
        }
      


        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
