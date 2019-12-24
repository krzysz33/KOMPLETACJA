using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KpInfohelp.Reports;
namespace KpInfohelp
{
    public class ViewModelUsluga : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
        private string _bgcolor;
        public string BgColor
        {

            get
            {
                return _bgcolor;
            }
            set
            {
                _bgcolor = value;
                RisePropertyChanged("BgColor");
            }
        }
        private DateTime _dateod;
        public DateTime DateOd
        {
            get
            {
                return _dateod;
            }
            set
            {
                _dateod = value;
                RaisePropertyChanged("DateOd");
            }
        }
        private DateTime _datedo;
        public DateTime DateDo
        {
            get
            {
                return _datedo;
            }

            set
            {
                _datedo = value;
                RisePropertyChanged("DateDo");
            }
        }

        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }


        private bool _isclosed = true;
        public bool IsClosed
        {

            get
            {
                return _isclosed;
            }

            set
            {
                _isclosed = value;
                RisePropertyChanged("IsClosed");
            }
        }
        public ICommand ItemSelSamochodCommand { get; private set; }
        public ICommand AddWazenieCommand { get; private set; }
        public ICommand ClearFiltr { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand CommandFiltruj { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand DayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand YearCommand { get; private set; }
        public ICommand UpdateCommand { get; set; }

        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public ICommand FillDataCommand { get; private set; }
        public ICommand ClearCommand { get; set; }
        public ViewModelUsluga()
        {

            Samochody = new ObservableCollection<IHP_SAMOCHOD>(context.IHP_SAMOCHOD);
            Kierowcy = new ObservableCollection<IHP_KIEROWCA>(context.IHP_KIEROWCA);
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>(context.IHP_KONTRAHENT);
            KontrahsArch = new ObservableCollection<IHP_KONTRAHENT_ARCH>(context.IHP_KONTRAHENT_ARCH.Where(x => x.AKTYWNY == 1));
            ItemSelSamochodCommand = new DelegateCommand(SelectSamochod);
            FillDataCommand = new DelegateCommand(Popraw);
            AddWazenieCommand = new DelegateCommand(Save, CanSave);
            ClearFiltr = new DelegateCommand(Clear, CanClear);
            ClearCommand = new DelegateCommand(Clear, CanClear);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            CommandFiltruj = new DelegateCommand(Filtruj, CanFilrt);
            PrintCommand = new DelegateCommand(Print, CanPrint);
            Messenger.Default.Register<WagaRamka>(this, OnMessagewagaRamka);
            Uslugi = new ObservableCollection<IHP_WAZENIE_USLUGA_EX>();
            DayCommand = new DelegateCommand(UstawDzien);
            WeekCommand = new DelegateCommand(UstawTydzien);
            MonthCommand = new DelegateCommand(UstawMiesiac);
            YearCommand = new DelegateCommand(UstawRok);
            UpdateCommand = new DelegateCommand(Update);
            LoadCollection();
            DateDo = DateTime.Today;
            DateOd = DateTime.Today.AddDays(-7);

        }

        public void OnMessagewagaRamka(WagaRamka ramka)
        {
            _ramka = ramka;
            WagaKgLocal = ramka.Waga.ToString() + " kg";
        }
        private string _wagakglocal;
        public string WagaKgLocal
        {
            get
            {
                return _wagakglocal;
            }
            set
            {
                _wagakglocal = value;
                RisePropertyChanged("WagaKgLocal");
            }

        }
        private string _kierowcanazwa;
        public string KierowcaNazwa
        {
            get
            {
                return _kierowcanazwa;
            }
            set
            {
                _kierowcanazwa = value;
                RisePropertyChanged("KierowcaNazwa");

            }
        }
        private string _nrrejnazwa;
        public string NrRejNazwa
        {
            get
            {
                return _nrrejnazwa;
            }
            set
            {
                _nrrejnazwa = value;
                RisePropertyChanged("NrRejNazwa");
            }
        }
        private string _kontrahentnazwa;
        public string KontrahentNazwa
        {
            get
            {
                return _kontrahentnazwa;
            }
            set
            {
                _kontrahentnazwa = value;
                RisePropertyChanged("KontrahentNazwa");
            }
        }
        private string _nrrejnazwafiltr;
        public string NrRejNazwaFiltr
        {
            get
            {
                return _nrrejnazwafiltr;
            }

            set
            {
                _nrrejnazwafiltr = value;
                RisePropertyChanged("NrRejNazwaFiltr");
            }


        }
        private string _uwagi;
        public string Uwagi
        {
            get
            {
                return _uwagi;
            }
            set
            {
                _uwagi = value;
                RisePropertyChanged("Uwagi");
            }
        }
        private ObservableCollection<IHP_KONTRAHENT> _kontrahs;
        public ObservableCollection<IHP_KONTRAHENT> Kontrahs
        {
            get
            {
                return _kontrahs;
            }
            set
            {
                _kontrahs = value;
            }
        }
        private ObservableCollection<IHP_KONTRAHENT_ARCH> _kontrahsarch;
        public ObservableCollection<IHP_KONTRAHENT_ARCH> KontrahsArch
        {
            get
            {
                return _kontrahsarch;
            }
            set
            {
                _kontrahsarch = value;
                RisePropertyChanged("KontrahsArch");
            }
        }
        private IHP_KONTRAHENT_ARCH _kontraharch;
        public IHP_KONTRAHENT_ARCH KontrahArch
        {
            get
            {
                return _kontraharch;
            }
            set
            {
                _kontraharch = value;
                RisePropertyChanged("KontrahArch");
            }
        }
        private ObservableCollection<IHP_SAMOCHOD> _samochody;
        public ObservableCollection<IHP_SAMOCHOD> Samochody
        {
            get
            {
                return _samochody;
            }

            set
            {
                _samochody = value;
                RisePropertyChanged("Samochody");
            }
        }
        private ObservableCollection<IHP_KIEROWCA> _kierowcy;
        public ObservableCollection<IHP_KIEROWCA> Kierowcy
        {
            get
            {
                return _kierowcy;
            }
            set
            {
                _kierowcy = value;
            }
        }
        private ObservableCollection<IHP_RODZAJDOK> _rodzajedok;
        public ObservableCollection<IHP_RODZAJDOK> RodzajeDok
        {
            get
            {
                return _rodzajedok;
            }
            set
            {
                _rodzajedok = value;
                RisePropertyChanged("RodzajeDok");
            }
        }
        private List<RodzajFiltru> _rodzajfiltrulst;
        public List<RodzajFiltru> RodzajFiltruLst
        {
            get
            {
                return _rodzajfiltrulst;
            }
            set
            {
                _rodzajfiltrulst = value;
                RisePropertyChanged("RodzajFiltruLst");
            }

        }
        private IHP_KARTOTEKA _kartoteka;
        public IHP_KARTOTEKA Kartoteka
        {
            get
            {
                return _kartoteka;
            }
            set
            {
                _kartoteka = value;
                RisePropertyChanged("Kartoteka");
            }
        }
        private void SelectSamochod()
        {
           
            if (_samochod == null) return;

             NrRejNazwa = _samochod.NRREJ;

            if (_kierowca != null)
            {
                _kierowca = null;
                Kierowca = null;
            }
            Kierowca = Kierowcy.FirstOrDefault(x => x.ID_IHP_KIEROWCA == _samochod.ID_IHP_KIEROWCA);
            if ((Kierowca != null) && (!_ispopraw))
                KierowcaNazwa = Kierowca.NAZWA;

            if (_kontraharch != null)
            {
                _kontraharch = null;
                KontrahArch = null;
            }
            KontrahArch = KontrahsArch.FirstOrDefault(x => x.ID_IHP_KONTRAHENT == _kierowca.ID_IHP_KONTRAHENT && x.AKTYWNY == 1);
            if((KontrahArch!=null) && (!_ispopraw))
                KontrahentNazwa = KontrahArch.NAZWA;
        }
        private bool _ispopraw;
        private void Popraw()
        {
            _ispopraw = true;
            if (_wazenie == null) return;
            NrRejNazwa = _wazenie.IHP_SAMOCHOD.NRREJ;
            if (_kierowca != null)
            {
                _kierowca = null;
                Kierowca = null;
            }
            KierowcaNazwa = _wazenie.KIEROWCA_NAZWA;
            if (_kontraharch != null)
            {
                _kontraharch = null;
                KontrahArch = null;
            }
            KontrahentNazwa = _wazenie.KONTRAHENT_NAZWA;
            Uwagi = _wazenie.UWAGI;
            Samochod = _samochody.FirstOrDefault(x => x.ID_IHP_SAMOCHOD == _wazenie.ID_IHP_SAMOCHOD);

        }
        private void SelectKonrahent()
        {
            //
        }
        private IHP_KONTRAHENT _kontrah;
        public IHP_KONTRAHENT Kontrah
        {
            get
            {
                return _kontrah;
            }
            set
            {
                _kontrah = value;
                RisePropertyChanged("Kontrah");
            }
        }
        private IHP_SAMOCHOD _samochod;
        public IHP_SAMOCHOD Samochod
        {
            get
            {
                return _samochod;
            }
            set
            {
                _samochod = value;

                RisePropertyChanged("Samochod");
            }
        }
        private IHP_SAMOCHOD _samochodsel;
        public IHP_SAMOCHOD SamochodSel
        {
            get
            {
                return _samochodsel;
            }
            set
            {
                _samochodsel = value;
                RisePropertyChanged("SamochodSel");
            }
        }
        private IHP_KIEROWCA _kierowca;
        public IHP_KIEROWCA Kierowca
        {
            get
            {
                return _kierowca;
            }
            set
            {
                _kierowca = value;
                RisePropertyChanged("Kierowca");
            }
        }
        private IHP_RODZAJDOK _rodzajdok;
        private IHP_WAZENIE_USLUGA_EX _wazenie;
        public IHP_WAZENIE_USLUGA_EX Wazenie
        {
            get
            {
                return _wazenie;
            }
            set
            {
                _wazenie = value;
                RisePropertyChanged("Wazenie");
            }
        }
        private ObservableCollection<IHP_WAZENIE_USLUGA_EX> _uslugi;
        public ObservableCollection<IHP_WAZENIE_USLUGA_EX> Uslugi
        {
            get
            {
                return _uslugi;
            }

            set
            {
                _uslugi = value;
                RisePropertyChanged("Uslugi");
            }
        }
        private List<IHP_WAZENIE_USLUGA_EX> _lista;
        private string GetNrKwitWew(int numer)
        {
            string PREFIX = "KU";// docelowo z konfigu aplikacji

            return PREFIX + "/" + numer.ToString();
        }
        private bool CanClear()
        {
            return true;
        }
        private bool CanSave()
        {
            return true;
        }
        private void Save()
        {
            int idKierwoca = 0;
            int idSamochod = 0;
            int idKontrahArch = 0;
            IHP_NUMERACJA numerkr = GetId(11);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;

            if (_kierowca != null)
                idKierwoca = _kierowca.ID_IHP_KIEROWCA;

            if (_samochod != null)
                idSamochod = _samochod.ID_IHP_SAMOCHOD;

            if (_kontraharch != null)
                idKontrahArch = _kontraharch.ID_IHP_KONTRAHENT_ARCH;
            try
            {
                IHP_WAZENIE_USLUGA wazenie = new IHP_WAZENIE_USLUGA()
                {
                    ID_IHP_WAZENIE_USLUGA = numerkr.NUMER,
                    DATACZAS = DateTime.Now,
                    ID_IHP_KIEROWCA = idKierwoca,
                    ID_IHP_SAMOCHOD = idSamochod,
                    WAGA = _ramka.Waga,
                    ID_IHP_KONTRAHENT_ARCH = idKontrahArch,
                    STATUS = 0,
                    NRKWIT = numerkr.NUMER, //docelowo obsluga numeracji 
                    NRKWITWEW = GetNrKwitWew(numerkr.NUMER),
                    KIEROWCA_NAZWA = KierowcaNazwa,
                    KONTRAHENT_NAZWA = KontrahentNazwa,
                    NRREJ_NAZWA = _nrrejnazwa,
                    UWAGI = _uwagi
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_WAZENIE_USLUGA.Add(wazenie);
                context.SaveChanges();
                Clear();
                //SentSamochod();
                //SentWazenie();
            }
            catch (DbEntityValidationException ewx)
            {
                LastMessage = ewx.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ewx.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ewx;
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        private void Update()
        {
            int idKierwoca = 0;
            int idSamochod = 0;
            int idKontrahArch = 0;

        if (_kierowca != null)
                idKierwoca = _kierowca.ID_IHP_KIEROWCA;

            if (_samochod != null)
                idSamochod = _samochod.ID_IHP_SAMOCHOD;

            if (_kontraharch != null)
                idKontrahArch = _kontraharch.ID_IHP_KONTRAHENT_ARCH;
            string LastMessage;

            try
            {
              if(_wazenie!=null)
                {
                    IHP_WAZENIE_USLUGA wazenie = context.IHP_WAZENIE_USLUGA.FirstOrDefault(x => x.ID_IHP_WAZENIE_USLUGA == _wazenie.ID_IHP_WAZENIE_USLUGA);
                    if(wazenie!=null)
                    {
                        wazenie.DATACZAS = DateTime.Now;
                        wazenie.ID_IHP_KIEROWCA = idKierwoca;
                        wazenie.ID_IHP_SAMOCHOD = idSamochod;
                        wazenie.WAGA = _ramka.Waga;
                        wazenie.ID_IHP_KONTRAHENT_ARCH = idKontrahArch;
                        wazenie.STATUS = 0;
                        wazenie.KIEROWCA_NAZWA = KierowcaNazwa;
                        wazenie.KONTRAHENT_NAZWA = KontrahentNazwa;
                        wazenie.NRREJ_NAZWA = _nrrejnazwa;
                        wazenie.UWAGI = _uwagi;
                        context.IHP_WAZENIE_USLUGA.Attach(wazenie);
                        context.Entry(wazenie).State = EntityState.Modified;
                         context.SaveChanges();
                        Clear();
                        _ispopraw = false;
                    }
                    else
                    {
                        MessageBox.Show("Cos Poszło nie tak ! ");
                    }
               
                }

           
                //SentSamochod();
                //SentWazenie();
            }
            catch (DbEntityValidationException ewx)
            {
                LastMessage = ewx.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ewx.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ewx;
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        private bool CanDelete()
        {
            return true;
        }
        private void Delete()
        {
            try
            {
                if (_wazenie != null)
                {

                    MessageBoxResult result = MessageBox.Show("Czy napewno usunąć ważenie", "Potwierdź Usunięcie", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        IHP_WAZENIE_USLUGA wazeniebase = context.IHP_WAZENIE_USLUGA.FirstOrDefault(x => x.ID_IHP_WAZENIE_USLUGA == _wazenie.ID_IHP_WAZENIE_USLUGA);
                        context.Entry(wazeniebase).State = EntityState.Deleted;
                        context.IHP_WAZENIE_USLUGA.Remove(wazeniebase);
                        context.SaveChanges();
                        Clear();
                        LoadCollection();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw;
            }
        }
        private void Filtruj()
        {

            if (String.IsNullOrEmpty(NrRejNazwaFiltr)) return;

            List<IHP_WAZENIE_USLUGA_EX> filteredResults = new List<IHP_WAZENIE_USLUGA_EX>();
                 
                                   filteredResults = Uslugi.Where(x => x.KIEROWCA_NAZWA.Contains(NrRejNazwaFiltr)).ToList();
        
                Uslugi = new ObservableCollection<IHP_WAZENIE_USLUGA_EX>(filteredResults);
        }
        private bool CanFilrt()
        {
            return true;
        }
        private bool CanPrint()
        {
            return true;
        }
        private void Print()

        {
            //  rptKwitWagowy report = new rptKwitWagowy();
            KwitUsluga report = new KwitUsluga();
            foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            {
               p.Visible = false;
            }
        
            if (_wazenie != null)
                report.Parameters["WazenieIdUsuga"].Value = _wazenie.ID_IHP_WAZENIE_USLUGA;
            var window = new frmKiwtWagowy();
            window.PreviewControl.DocumentSource = report;

            report.CreateDocument();

            window.Show();
        }
        private void Clear()
        {
            if (_samochod != null)
            {
                Samochod = null;
                _samochod = null;
              
            }

            if (_kontrah != null)
            {
                _kontrah = null;
                Kontrah = null;
            
            }

            if (_kontraharch != null)
            {
                _kontraharch = null;
                KontrahArch = null;
             
            }

            if (_kierowca != null)
            {
                _kierowca = null;
                Kierowca = null;
            }

            if (_rodzajdok != null)
            {
                _rodzajdok = null;
           
            }
            if (_wazenie != null)
            {
                Wazenie = null;
                _wazenie = null;
            
            }
            KierowcaNazwa = string.Empty;
            KontrahentNazwa = string.Empty;
            NrRejNazwa = string.Empty;
            Uwagi = string.Empty;
            _ispopraw = false;
            LoadCollection();
        }
        public void LoadCollection()
        {
        List<IHP_WAZENIE_USLUGA> _uslugilst;
            _uslugilst =   context.IHP_WAZENIE_USLUGA.ToList();

            Uslugi.Clear();
            
              //Wazenia.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _uslugilst)
            {
                IHP_WAZENIE_USLUGA_EX it = new IHP_WAZENIE_USLUGA_EX(context, item);
                 Uslugi.Add(it);
            }
   
        }
        private void UstawDzien()
        {
            BgColor = "Red";
            DateTime currentDateTime = DateTime.Now;
            DateOd = currentDateTime.Date;
            DateDo = currentDateTime.Date.AddDays(1).AddTicks(-1);
           List<IHP_WAZENIE_USLUGA> _listaWwazen = context.IHP_WAZENIE_USLUGA.Where(x => x.DATACZAS >= DateOd && x.DATACZAS <= DateDo).ToList();

            Uslugi.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _listaWwazen)
            {
                IHP_WAZENIE_USLUGA_EX itemex = new IHP_WAZENIE_USLUGA_EX(context, item);
                Uslugi.Add(itemex);
            }

        }
        private void UstawTydzien()
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - DateTime.Today.DayOfWeek - 1;
            DateTime fdowDate = DateTime.Today.AddDays(offset);
            DateTime ldowDate = fdowDate.AddDays(7);
            List<IHP_WAZENIE_USLUGA> _listaWwazen = context.IHP_WAZENIE_USLUGA.Where(x => x.DATACZAS >= fdowDate && x.DATACZAS <= ldowDate).ToList();
            Uslugi.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _listaWwazen)
            {
                IHP_WAZENIE_USLUGA_EX itemex = new IHP_WAZENIE_USLUGA_EX(context, item);
                Uslugi.Add(itemex);
            }
        }
        private void UstawMiesiac()
        {
            DateTime dt = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime dt2 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            List<IHP_WAZENIE_USLUGA>  _listaWwazen = context.IHP_WAZENIE_USLUGA.Where(x => x.DATACZAS >= dt && x.DATACZAS <= dt2).ToList();

            Uslugi.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _listaWwazen)
            {
                IHP_WAZENIE_USLUGA_EX itemex = new IHP_WAZENIE_USLUGA_EX(context, item);
                Uslugi.Add(itemex);
            }
        }
        private void UstawRok()
        {
            DateTime dt = new DateTime(DateTime.Today.Year, 1, 1);
            DateTime dt2 = new DateTime(DateTime.Today.Year, 12, 31);
            List<IHP_WAZENIE_USLUGA>  _listaWwazen = context.IHP_WAZENIE_USLUGA.Where(x => x.DATACZAS >= dt && x.DATACZAS <= dt2).ToList();

            Uslugi.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _listaWwazen)
            {
                IHP_WAZENIE_USLUGA_EX itemex = new IHP_WAZENIE_USLUGA_EX(context, item);
                Uslugi.Add(itemex);
            }
        }
        private void UstawMiesiacFiltr(int NrMiesiac)
        {
            DateTime dt = new DateTime(DateTime.Today.Year, NrMiesiac, 1);
            DateTime dt2 = new DateTime(DateTime.Today.Year, NrMiesiac, DateTime.DaysInMonth(DateTime.Today.Year, NrMiesiac));
            List<IHP_WAZENIE_USLUGA> _listaWwazen = context.IHP_WAZENIE_USLUGA.Where(x => x.DATACZAS >= dt && x.DATACZAS <= dt2).ToList();

            Uslugi.Clear();
            foreach (IHP_WAZENIE_USLUGA item in _listaWwazen)
            {
                IHP_WAZENIE_USLUGA_EX itemex = new IHP_WAZENIE_USLUGA_EX(context, item);
                Uslugi.Add(itemex);
            }
        }
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
