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
using System.Windows.Media;
using System.Globalization;

namespace KpInfohelp
{
    class ViewModelKafelkiFiltr : CrudVMBase, INotifyPropertyChanged
    {
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool wybierzdzien, wybierztydzien, wybierzmiesiac;
        private Color _kolory;
        public Color Kolory
        {
            get
            {
                return _kolory;
            }
            set
            {
                _kolory = value;
                RisePropertyChangedRise("Kolory");
            }
        }
        private Color _kolordzien;
        public Color KolorDzien
        {
            get
            {
                return _kolordzien;
            }
            set
            {
                _kolordzien = value;
                RisePropertyChangedRise("KolorDzien");
            }
        }

        private Color _kolortydzien;
        public Color KolorTydzien
        {
            get
            {
                return _kolortydzien;
            }
            set
            {
                _kolortydzien = value;
                RisePropertyChangedRise("KolorTydzien");
            }
        }

        private Color _kolormiesiac;
        public Color KolorMiesiac
        {
            get
            {
                return _kolormiesiac;
            }
            set
            {
                _kolormiesiac = value;
                RisePropertyChangedRise("KolorMiesiac");
            }
        }

        private Color _kolorrok;
        public Color KolorRok
        {
            get
            {
                return _kolorrok;
            }
            set
            {
                _kolorrok = value;
                RisePropertyChangedRise("KolorRok");
            }
        }

        private Brush _kolor;
        public Brush Kolor
        {
            get
            {
                return _kolor;
            }
            set
            {
                _kolor = value;
                RisePropertyChangedRise("Kolor");
            }
        }

        public ObservableCollection<IHP_KONTRAHENT> Kontrahs { get;set;}
        public ICommand PrintCommand { get; private set; }
       public ICommand GenCommand { get; set; }
       public ICommand CommandSearchFiltr { get; set; }
       public ICommand CommandCzyscFiltruj { get; set; }

        public ICommand DayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand YearCommand { get; private set; }

        private IHP_KONTRAHENT _kontrah; 
        public IHP_KONTRAHENT Kontrah {
            get
            {
               return _kontrah;
            }
            set
            {
                _kontrah = value;
                RisePropertyChangedRise("Kontrah");
            }
       }
        public ViewModelKafelkiFiltr()
        {
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>(context.IHP_KONTRAHENT);
            DayCommand = new DelegateCommand(UstawDzien);
            WeekCommand = new DelegateCommand(UstawTydzien);
            MonthCommand = new DelegateCommand(UstawMiesiac);
            YearCommand = new DelegateCommand(UstawRok);
            DateDo = DateTime.Today;
            DateOd = DateTime.Today;
            WypelnijDane();
            CommandSearchFiltr = new DelegateCommand(Filtruj);
            CommandCzyscFiltruj = new DelegateCommand(CzyscFiltruj);
            UstawDzien();
        }

        private DateTime _dataod;
        public DateTime DateOd
        {
            get
            {
                return _dataod;
            }
            set
            {
                _dataod = value;
                RisePropertyChangedRise("DateOd");

            }
        }
        private DateTime _datado;
     public   DateTime DateDo

        {
            get
            {
                return _datado;
            }
            set
            {
                _datado = value;
                RisePropertyChangedRise("DateDo");


            }
        }

        private void KasujKolory()
        {
            KolorDzien = KolorTydzien = KolorMiesiac = KolorRok = Color.FromRgb(211, 211, 211);
            wybierzdzien = wybierztydzien = wybierzmiesiac  = false;
        }

        private void UstawDzien()
        {

            DateTime currentDateTime = DateTime.Now;
            DateOd = currentDateTime.Date;
            DateDo = currentDateTime.Date;

            KasujKolory();
            wybierzdzien = true;
            //KolorDzien  = Color.FromRgb(135, 206, 250);
            KolorDzien = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawTydzien()
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - DateTime.Today.DayOfWeek - 1;
            DateOd = DateTime.Today.AddDays(offset);
            DateDo = DateOd.AddDays(7);
    
            KasujKolory();
            wybierztydzien = true;
            //KolorTydzien  = Color.FromRgb(135, 206, 250);
            KolorTydzien = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawMiesiac()
        {
            DateOd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateDo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));


        
            KasujKolory();
            wybierzmiesiac = true;
            //KolorMiesiac  = Color.FromRgb(135, 206, 250);
            KolorMiesiac = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawRok()
        {
            DateOd = new DateTime(DateTime.Today.Year, 1, 1);
            DateDo = new DateTime(DateTime.Today.Year, 12, 31);
      
            KasujKolory();
            //KolorRok  = Color.FromRgb(135, 206, 250);
             KolorRok = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }

        public void Filtruj()
        {
            int idkontrah = 0;
            if (_kontrah != null)
                idkontrah = _kontrah.ID_IHP_KONTRAHENT;

            Messenger.Default.Send<KafelekFiltr>(new KafelekFiltr() { IdKontrah = idkontrah, DataOd = DateOd, DataDo = DateDo, TylkoOd =false,TypFiltra=2});
        }
        public void CzyscFiltruj()
        {
            int idkontrah = 0;
            Kontrah = null;

            Messenger.Default.Send<KafelekFiltr>(new KafelekFiltr() { IdKontrah = idkontrah, DataOd = DateOd, DataDo = DateDo, TylkoOd = false,TypFiltra=1 });
        }
        protected void RisePropertyChangedRise(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }




        private void WypelnijDane()
        {
       
        }
  

     
  
 


    }
 }
 


