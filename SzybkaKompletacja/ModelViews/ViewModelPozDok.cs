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
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    public class ViewModelPozDok : CrudVMBase, INotifyPropertyChanged ,IMVVMDockingProperties
    {
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
        static int seed = Environment.TickCount;
        Timer _timer;
         string WagaKg;

         private IHP_NAGLDOK _dokument;
        public IHP_NAGLDOK Dokument
        {
            get
            {
                return _dokument;
            }
            set
            {
                _dokument = value;
                  RisePropertyChanged("Dokument");
            }

        }
        private WagaRamka _ramka;
 
        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand AddToSubjectCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand UpdNewProgCommand { get; private set; }
        public ICommand ItemSelSamochodCommand { get; private set; }
        public ICommand ItemSelKontrahentCommand { get; private set; }
        public ICommand ItemSelKierowcaCommand { get; private set; }
       IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<IHP_SAMOCHOD> _samochody;
      
 
         public ViewModelPozDok()
        {
            PozycjeDok = new ObservableCollection<IHP_POZDOK>();
            Dokumenty = new ObservableCollection<IHP_NAGLDOK>(context.IHP_NAGLDOK);
            DeleteCommand = new RelayCommand(DeleteCurrent, CanDelete);
            EditCommand = new RelayCommand(ShowPoz, CanEdit);
            ClearNewProgCommand = new DelegateCommand(Clear);
            PrintCommand = new DelegateCommand(Print);
            UpdNewProgCommand = new RelayCommand(Update, CanUpdate);
            CloseCommand = new DelegateCommand<Window>(Closeform);

            Messenger.Default.Register<int>(this, OnMessageDokument);
            Messenger.Default.Register<WagaRamka>(this, OnMessagewagaRamka);

          _timer = new Timer(TimerCallback, null, 1000, 1000);
        }
     
        private ObservableCollection<IHP_NAGLDOK> _dokumenty;
        public ObservableCollection<IHP_NAGLDOK> Dokumenty
        {
            get
            {
                return _dokumenty;
            }
            set
            {
                _dokumenty = value;
                RisePropertyChanged("Dokumenty");
            }
        }
        private ObservableCollection<IHP_POZDOK> _pozycjedok;
        public ObservableCollection<IHP_POZDOK> PozycjeDok
        {
            get
            {
                return _pozycjedok;
            }
            set
            {
                _pozycjedok = value;
                RisePropertyChanged("PozycjeDok");
            }
        }

        private IHP_POZDOK _pozycjadok;
        public IHP_POZDOK PozycjaDok
        {
            get
            {
                return _pozycjadok;
            }
            set 
            {
                _pozycjadok = value;
                RisePropertyChanged("PozycjaDok");
            }
        }
        private void SelectKonrahent()
        {
            //
        }
        private void SelectKierowca()
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
        public IHP_RODZAJDOK RodzajDok
        {
            get
            {
                return _rodzajdok;
            }
            set
            {
                _rodzajdok = value;
                RisePropertyChanged("RodzajDok");
            }
        }
 
        private string _adres;
        public string Adres
        {
            get
            {
                return _adres;
            }
            set
            {
                _adres = value;
                RisePropertyChanged("Adres");
            }
        }

        private decimal _sumanagl;
        public decimal SumaNagl
        {
            get
            {
                return _sumanagl;
            }
            set
            {
                _sumanagl = value;
                RisePropertyChanged("SumaNagl");
            }
        }

        private string _numerdok;
        public string NumerDok
        {
            get
            {
                return _numerdok;
            }

            set
            {
                _numerdok = value;
                RisePropertyChanged("NumerDok");
            }
        }
        private bool CanDelete()
         {
            if (PozycjaDok != null)
                return true;
            else
                return false;
         }
        private bool CanUpdate()
        {
       if (_kierowca != null)
                  return true;
           else
                return false;
        }
        private bool CanSave()
        {
    //&& (_kontraharch != null)
                  //if ((_kierowca!=null) &&  (_samochod != null) && (_kartoteka != null))

                 return true;
            //else
            //    return false;
        }
        private IHP_NUMERACJA GetId(int dlatabeli)
        {
            return context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);
        }      
         private void WykonajWazenie()
        {
    //     if(_wazenie!=null)
    //        Save();

    //        LoadDOkData();
    ////  else
    // //    Update();
     }
      private bool  CanEdit()
        {
            if (PozycjaDok != null)
                return true;
            else
                return false;
        }
        public void ShowPoz()
        {
            if (Dokument != null)
            {

                var window = new Poz();
                Messenger.Default.Send<IHP_POZDOK>(PozycjaDok);
                window.Show();
            }
        }
        private string GetNumerDok(int nrdok)
        {
            string prefx = "W/";
            return prefx + nrdok.ToString();
        }
        protected override void DeleteCurrent()
        {
            string LastMessage;
            MessageBoxResult result = MessageBox.Show("Czy napewno usunąć pozycję ?", "Potwierdź Usunięcie", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
               try
                {
                    if (_pozycjadok != null)
                    {
                        context.Entry(_pozycjadok).State = EntityState.Deleted;
                        context.IHP_POZDOK.Remove(_pozycjadok);
                        context.SaveChanges();
                        Clear();

                    }
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
        }
        void DeleteCurrentPozDost(int ID_IHP_POZ)
        {
            string LastMessage;
            try
            {
                if (_dokument != null)
                {

                    context.Entry(_dokument).State = EntityState.Deleted;
                    context.IHP_NAGLDOK.Remove(_dokument);
                    context.SaveChanges();
                    Clear();
             
                }
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
        void DeleteCurrentPozRozch(int ID_IHP_POZ)
        {
            string LastMessage;
            try
            {
                if (_dokument != null)
                {

                    context.Entry(_dokument).State = EntityState.Deleted;
                    context.IHP_NAGLDOK.Remove(_dokument);
                    context.SaveChanges();
                    Clear();

                }
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
        void DeleteCurrentDostRozch(int ID_IHP_DOSTAWA)
        {
               string LastMessage;
            List<IHP_ROZCHDOST> rozchlst = context.IHP_ROZCHDOST.Where(x => x.ID_IHP_DOSTAWA == ID_IHP_DOSTAWA).ToList();

            try
            {
                    context.Entry(rozchlst).State = EntityState.Deleted;
                     context.IHP_ROZCHDOST.RemoveRange(rozchlst);
                    context.SaveChanges();
      
                 
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
        private void Save()
        {
            //_datawjazd = DateTime.Now;
            //_datawyjazd = DateTime.Now;
            //IHP_NUMERACJA numerdok = GetId(8);
            //if (numerdok != null)
            //    numerdok.NUMER++;
            //string LastMessage;
            //try
            //{
            //    IHP_NAGLDOK dokument = new IHP_NAGLDOK()
            //    {
            //        ID_IHP_NAGLDOK = numerdok.NUMER,
            //        DATADOK = DateTime.Today,
            //        NRDOK = numerdok.NUMER,
            //        NRDOKWEW = GetNumerDok(numerdok.NUMER),
            //        SUMAOG = Waga,
            //        ID_DEFDOK = _wazenie.IHP_RODZAJDOK.ID_IHP_RODZAJDOK,
            //        ID_IHP_KONTRAHENT_ARCH = _wazenie.IHP_KONTRAHENT_ARCH.ID_IHP_KONTRAHENT_ARCH,
            //        ID_RODZAJDOK = _wazenie.IHP_RODZAJDOK.ID_IHP_RODZAJDOK,
            //        ID_KH_SUBJECT = 0,
            //        IHP_KONTRAHENT_ARCH  = _wazenie.IHP_KONTRAHENT_ARCH,
            //        IHP_RODZAJDOK = _wazenie.IHP_RODZAJDOK,
            //        ID_IHP_WAZENIE = _wazenie.ID_IHP_WAZENIE
            //    };
            //    context.IHP_NUMERACJA.Add(numerdok);
            //    context.Entry(numerdok).State = EntityState.Modified;

            //    //   public virtual ICollection<IHP_POZDOK> IHP_POZDOK { get; set; }
            //    IHP_NUMERACJA numerpoz = GetId(9);
            //    if (numerpoz != null)
            //        numerpoz.NUMER++;
            //    short cenaust = 1;
            //    IHP_POZDOK poz = new IHP_POZDOK()
            //    {
            //        ID_IHP_POZDOK = numerpoz.NUMER,
            //        ID_IHP_KARTOTEKA = _wazenie.ID_IHP_KARTOTEKA ?? 0,
            //        ID_IHP_NAGLDOK = numerdok.NUMER,
            //        LP = 1,
            //        ILOSC = Waga,
            //        NAZWASKRPOZ = _wazenie.IHP_KARTOTEKA.NAZWASKR,
            //        CENANETTO = 1,
            //        CENABRUTTO = 1,
            //        WARTVAT = 1,
            //        WARTNETTO = 1,
            //        WARTBRUTTO = 1,
            //        DATADOK = DateTime.Today,
            //       CENAUSTALONA = cenaust,
            //       UWAGI =string.Empty,
            //      ID_TOWSUBJECT=0
            //    };
            //    dokument.IHP_POZDOK.Add(poz);

            //    context.IHP_NUMERACJA.Add(numerpoz);
            //    context.Entry(numerpoz).State = EntityState.Modified;
            
            //    context.IHP_NAGLDOK.Add(dokument);

            //    //status wazenia 
            //    _wazenie.STATUS = 3;
            //    context.IHP_WAZENIE.Add(_wazenie);
            //    context.Entry(_wazenie).State = EntityState.Modified;
            //   context.SaveChanges();

            //    LoadCollection();
            //    Messenger.Default.Send<int>(2);


            //}
            //catch (Exception ex)
            //{
            //    LastMessage = ex.ToString();
            //    if (LastMessage == String.Empty)
            //        LastMessage = ex.InnerException.ToString();
            //    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //    throw ex;
            //}
        }
        private void Delete()
        {
           // string LastMessage;
           // try
           // {
           //    if(_wazenie != null)
           //     {
                
           //         context.Entry(_wazenie).State = EntityState.Deleted;
           //         context.IHP_WAZENIE.Remove(_wazenie);
           //         context.SaveChanges();
           //         Clear();
           //         LoadCollection();
           //       } 
           //}
           // catch (Exception ex)
           // {
           //     LastMessage = ex.ToString();
           //     if (LastMessage == String.Empty)
           //         LastMessage = ex.InnerException.ToString();
           //     LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
           //     throw ex;
           // }
        }
        public void  SentSamochod()
        {
 
        }
        public void Closeform(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        private void Print()
        {
           rptKwitWagowy report = new rptKwitWagowy();
         var window = new frmKiwtWagowy();
           window.PreviewControl.DocumentSource = report;
          //  if(_wazenie!= null)
          //      report.Parameters["idWazenie"].Value = _wazenie.ID_IHP_WAZENIE;
         report.CreateDocument();
         window.Show();
        }
        private void Clear()
        {
        }
        private void Update()
        {
            //_datawyjazd = DateTime.Now;
            //try
            // {
            //  if (_wazenie != null)
            //    {
            //        _wazenie.IHP_KIEROWCA = _kierowca;
            //        _wazenie.IHP_SAMOCHOD = _samochod;
            //        _wazenie.IHP_KONTRAHENT_ARCH = _kontraharch;
            //        _wazenie.IHP_KARTOTEKA = _kartoteka;
            //        _wazenie.DATAWYJAZD = _datawyjazd;
            //       _wazenie.WAGAWYJAZD = _ramka.Waga;
            //        context.IHP_WAZENIE.Attach(_wazenie);
            //        context.Entry(_wazenie).State = EntityState.Modified;
            //        context.SaveChanges();
            //        Clear();
            //        LoadCollection();
            //      }
            //}
            //catch (DbEntityValidationException e)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" , eve.Entry.Entity.GetType().Name, eve.Entry.State));
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"",    ve.PropertyName, ve.ErrorMessage));
            //        }
            //    }
            //    throw ;
            //}
        }
       private void AddSubject()
        {
      //      SubjectSfera sb = new SubjectSfera();
     //        sb.DodajKontrahenta();
 
       }
       protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void OnMessageDokument(int ID_NAGL_DOK)
        {
            Dokument = context.IHP_NAGLDOK.FirstOrDefault(x=>x.ID_IHP_NAGLDOK== ID_NAGL_DOK);
            string nrdoklok = string.Empty;

            NumerDok = Dokument.NRDOKWEW;

            if (!string.IsNullOrEmpty(Dokument.IHP_KONTRAHENT_ARCH.NRLOKALU))
            {
                nrdoklok = Dokument.IHP_KONTRAHENT_ARCH.NRDOMU + " / " + Dokument.IHP_KONTRAHENT_ARCH.NRLOKALU;
            }
            else
            {
                nrdoklok = Dokument.IHP_KONTRAHENT_ARCH.NRDOMU;
            }
            Adres = Dokument.IHP_KONTRAHENT_ARCH.ULICA + " " + nrdoklok + " " + Dokument.IHP_KONTRAHENT_ARCH.KODPOCZTOWY + " " + Dokument.IHP_KONTRAHENT_ARCH.MIEJSCOWOSC;

            PozycjeDok.Clear();
            foreach (IHP_POZDOK item in Dokument.IHP_POZDOK)
            {
                PozycjeDok.Add(item);
            }
            SumaNagl = Dokument.IHP_POZDOK.Sum(x => x.CENABRUTTO);
        }
       public void OnMessagewagaRamka(WagaRamka ramka)
        {
            _ramka = ramka;
           WagaKg = ramka.WagaStr;
        }
     
        //Function to get random number
        private static readonly Random getrandom = new Random(DateTime.Now.Second);
        private static readonly object syncLock = new object();
        static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(DateTime.Now.Second));
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Value.Next(min, max);
            }
        }
        private void TimerCallback(object state)
        {
            try
            {
                OnMessagewagaRamkaTest();
            }
            finally
            {
               // _timer.Change(_random.Next(1000, 3000), Timeout.Infinite);
            }
        }
        public void OnMessagewagaRamkaTest()
        {
            WagaRamka ramka = new WagaRamka();
            ramka.Waga = GetRandomNumber(1000,10000);
             _ramka = ramka;
            WagaKg = _ramka.Waga.ToString() + " kg";
      
        }
         public WazenieKonfiguracja GetConfigForVerticle (int ID_IHP_SAMOCHOD)
        {
            WazenieKonfiguracja res = new WazenieKonfiguracja();
            //IHP_WAZENIE ostWaga = new IHP_WAZENIE();
            //ostWaga = context.IHP_WAZENIE.OrderByDescending(x => x.ID_IHP_SAMOCHOD == ID_IHP_SAMOCHOD).FirstOrDefault();
            //res.DataOstatniegoWazenia = ostWaga.DATAWJAZD ?? default(DateTime);
            //res.TaraPojazduOstWaga = ostWaga.WAGAWJAZD;
            return res;
        }
     }
}
 


