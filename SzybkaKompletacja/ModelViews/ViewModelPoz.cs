using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using DevExpress.Mvvm;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading;
using DevExpress.Xpf.Printing;
using System.Windows;

namespace KpInfohelp
{
    public class ExampleObject : BindableBase
    {
        private string _Header;
        public string Header
        {
            get
            {
                return _Header;
            }
            set
            {
                SetProperty(ref _Header, value, () => Header);
            }
        }
        public override string ToString()
        {
            return this.Header;
        }
    }

    public class ViewModelPoz : CrudVMBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool IsClosed
        {
            get { return GetProperty(() => IsClosed); }
            set { SetProperty(() => IsClosed, value); }
        }
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        private IHP_POZDOK Poz;
        private string _nazwatowaru;
        public string NazwaTowaru
        {
            get
            {
                return _nazwatowaru;

            }
            set
            {
                _nazwatowaru = value;
                RisePropertyChanged("NazwaTowaru");
            }
        }
        private string _jm;
        public string Jm
        {
            get
            {
                return _jm;
            }
            set
            {
                _jm = value;
                RisePropertyChanged("Jm");
            }
        }
        private string _stawkavat;
        public string StawkaVat
        {
            get
            {
                return _stawkavat;
            }
            set
            {
                _stawkavat = value;
                RisePropertyChanged("StawkaVat");
            }
}


        private decimal _ilosc;
        public decimal Ilosc
        {
            get
            {
                return _ilosc;
            }
            set
            {
                _ilosc = value;
                RisePropertyChanged("Ilosc");
            }
        }

        private decimal _cenajednnetto;
        public decimal CenaJedNetto
        {
            get
            {
                return _cenajednnetto;
            }

            set
            {
                _cenajednnetto = value;
                RisePropertyChanged("CenaJedNetto");
            }
        }

        private decimal _cenanetto;
        public decimal CenaNetto
        {
            get
            {
                return _cenanetto;
            }
            set
            {
                _cenanetto = value;
                RisePropertyChanged("CenaNetto");
            }
        }
        private decimal _cenabrutto;
        public decimal CenaBrutto
        {
            get
            {
                return _cenabrutto;
             }

            set
            {
                _cenabrutto = value;
                RisePropertyChanged("CenaBrutto");
            }
        }

        private string _datawazenia;
        public string DataWazenia
        {
            get
            {
                return _datawazenia;
            }
            set
            {
                _datawazenia = value;
                RisePropertyChanged("DataWazenia");
            }
        }

        private string _datawjazd;
        public string DataWjazd
        {
            get
            {
                return _datawjazd;
            }
            set
            {
                _datawjazd = value;
                RisePropertyChanged("DataWjazd");
            }
        }

        private string _datawyjazd;
        public string DataWyjazd
        {
            get
            {
                return _datawyjazd;
            }
            set
            {
                _datawyjazd = value;
                RisePropertyChanged("DataWyjazd");
            }
        }
        private string _username;
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RisePropertyChanged("UserName");
            }
        }
        private string _nrkwitu;
        public string NrKwitu
        {
            get
            {
                return _nrkwitu;
            }
            set
            {
                _nrkwitu = value;
                RisePropertyChanged("NrKwitu");
            }
        }

        private decimal _wartnetto;
        public decimal WartNetto
        {
            get
            {
                return _wartnetto;
            }
            set
            {
                _wartnetto = value;
                RisePropertyChanged("WartNetto");
            }
        }
        private decimal _wartbrutto;
        public decimal WartBrutto
        {
            get
            {
                return _wartbrutto;
            }
            set
            {
                _wartbrutto = value;
                RisePropertyChanged("WartBrutto");
            }
        }
        public ICommand CloseCommand { get; private set; }
        public ViewModelPoz()
        {
            CloseCommand = new DelegateCommand<Window>(Closeform);

            Source = new ObservableCollection<ExampleObject>() {
                new ExampleObject(){ Header = "Dane Podstawowe"},
                new ExampleObject(){ Header = "Dane Dodatkowe"},
                //new ExampleObject(){ Header = "item3"},
                //new ExampleObject(){ Header = "item4"},
            };
            Messenger.Default.Register<IHP_POZDOK>(this, OnMessagePoz);
        }

        public void OnMessagePoz(IHP_POZDOK poz)
        {
            Poz = poz;
            string nrdoklok = string.Empty;
            NazwaTowaru = Poz.IHP_KARTOTEKA.NAZWADL;
            Jm = Poz.IHP_KARTOTEKA.IHP_JM.JM;
            StawkaVat = Poz.IHP_KARTOTEKA.IHP_STAWKAVAT.NAZWA;
            Ilosc = Math.Round(Poz.ILOSC,2);
            CenaJedNetto = Math.Round(Poz.CENANETTO,2);
            CenaBrutto = Math.Round(Poz.CENABRUTTO,2);
            WartBrutto = Math.Round(Poz.WARTBRUTTO,2);
            WartNetto = Math.Round(Poz.WARTNETTO,2);
             }
        public void Closeform(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
  
        private ObservableCollection<ExampleObject> _Source;
        public ObservableCollection<ExampleObject> Source
        {
            get
            {
                return _Source;
            }
            set
            {
                SetProperty(ref _Source, value, () => Source);

            }
        }
     
     }
}
 


