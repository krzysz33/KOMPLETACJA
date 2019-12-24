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
using KpInfohelp.Repository;
using System.Globalization;
using DevExpress.Xpf.Docking;
using System.Windows.Markup;
using System.Windows.Data;
using System.Windows.Media;
 
namespace KpInfohelp
{
    public class ViewModelDokumenty : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
        private bool wybierzdzien, wybierztydzien ,wybierzmiesiac, wybierzrok;
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
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        static int seed = Environment.TickCount;
        Timer _timer;

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
                RisePropertyChanged("Kolory");
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
                RisePropertyChanged("KolorDzien");
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
                RisePropertyChanged("KolorTydzien");
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
                RisePropertyChanged("Kolormiesiac");
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
                RisePropertyChanged("KolorRok");
            }
        }

        private Brush _kolor;
        public Brush Kolor
        {
            get
            {
                return  _kolor;
            }
            set
            {
                _kolor = value;
                RisePropertyChanged("Kolor");
            }
        }
        private List<IHP_SAMOCHOD> _listasam;
        private List<IHP_KIEROWCA> _listakierowcy;
        private List<IHP_KONTRAHENT_ARCH> _listakontrah;
        private List<IHP_KARTOTEKA> _listakartoteka;
    
        private List<IHP_NAGLDOK> _listadokumentow;
        private WagaRamka _ramka;
        private DateTime _dateod;
        public  DateTime DateOd
        {
            get
            {
               return  _dateod;
            }
            set
            {
                _dateod = value;
                RisePropertyChanged("DateOd");
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

        private RejWagaMessage rejmsg;
        public ICommand Waga1 { get; private set; }
        public ICommand Waga2 { get; private set; }
        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddWazenieCommand { get; private set; }
        public ICommand AddToSubjectCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand UpdNewProgCommand { get; private set; }
        public ICommand ItemSelSamochodCommand { get; private set; }
        public ICommand ItemSelKontrahentCommand { get; private set; }
        public ICommand ItemSelKierowcaCommand { get; private set; }
        public ICommand ShowDokCommand { get; private set; }
        public ICommand CommandPanelFiltr { get; private set; }
        public ICommand CommandPanelListaWazen { get; private set; }
        public ICommand CommandPanelUtawienieDanych { get; private set; }
        public ICommand CommandFiltruj { get; private set; }
        public ICommand DayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand YearCommand { get; private set; }

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
          protected void OnRisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
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
                OnRisePropertyChanged("Samochody");

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
                OnRisePropertyChanged("RodzajeDok");
            }
        }
        public ViewModelDokumenty()
        {
            //https://www.rapidtables.com/web/color/blue-color.html
            Kolory = Color.FromRgb(255, 0, 0);
            Kolor = Brushes.Red;
            RisePropertyChanged("Kolor");

            Samochody = new ObservableCollection<IHP_SAMOCHOD>(context.IHP_SAMOCHOD);
            Kierowcy = new ObservableCollection<IHP_KIEROWCA>(context.IHP_KIEROWCA);
     
            KontrahsArch = new ObservableCollection<IHP_KONTRAHENT_ARCH>(context.IHP_KONTRAHENT_ARCH.Where(x=>x.AKTYWNY==1));
            Kartoteki = new ObservableCollection<IHP_KARTOTEKA>(context.IHP_KARTOTEKA);
            RodzajeDok = new ObservableCollection<IHP_RODZAJDOK>(context.IHP_RODZAJDOK);
     
            Dokumenty = new ObservableCollection<IHP_NAGLDOK>(context.IHP_NAGLDOK);
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            AddWazenieCommand = new RelayCommand(WykonajWazenie, CanSave);
            ClearNewProgCommand = new DelegateCommand(Clear);
            PrintCommand = new DelegateCommand(Print);
            UpdNewProgCommand = new RelayCommand(Update, CanUpdate);
            ItemSelSamochodCommand = new DelegateCommand(SelectSamochod);
            ItemSelKontrahentCommand = new DelegateCommand(SelectKonrahent);
            ItemSelKierowcaCommand = new DelegateCommand(SelectKierowca);
            CommandPanelFiltr = new DelegateCommand(PanelFiltr);
            CommandPanelListaWazen = new DelegateCommand(PanelListaWazen);
             CommandPanelUtawienieDanych = new DelegateCommand(UstawienieDanychVd);
            ShowDokCommand = new DelegateCommand(ShowDok);
            CommandFiltruj = new DelegateCommand(Flitr);
            DayCommand = new DelegateCommand(UstawDzien);
            WeekCommand = new DelegateCommand(UstawTydzien);
            MonthCommand = new DelegateCommand(UstawMiesiac);
            YearCommand = new DelegateCommand(UstawRok);

            rejmsg = new RejWagaMessage();
             RodzajDok = new IHP_RODZAJDOK();
      
            LoadCollection();
            UstawienieDanych = true;
            wybierzdzien = true;
            LoadDOkData();

            Messenger.Default.Register<List<IHP_KIEROWCA>>(this, OnMessageKierowca);
            Messenger.Default.Register<List<IHP_KONTRAHENT_ARCH>>(this, OnMessageKontrahent);
            Messenger.Default.Register<List<IHP_KARTOTEKA>>(this, OnMessageKartoteka);
    
            Messenger.Default.Register<int>(this, OnRefresh);
            Messenger.Default.Register<RejWagaMessage>(this, OnMessageDok);
            _timer = new Timer(TimerCallback, null, 1000, 1000);
      
            //  OnMessagewagaRamkaTest();
        }
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
                PrzeliczWage();
                OnRisePropertyChanged("Dokument");
            }
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
                OnRisePropertyChanged("Dokumenty");
            }
        }
        private ObservableCollection<IHP_KARTOTEKA> _kartoteki;
        public ObservableCollection<IHP_KARTOTEKA> Kartoteki
        {
            get
            {
                return _kartoteki;
            }
            set
            {
                _kartoteki = value;
                OnRisePropertyChanged("Kartoteki");
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
                OnRisePropertyChanged("Kartoteka");
            }
        }
        private void SelectSamochod()
          {
           if (_samochod == null) return;
           if(_kierowca != null)
            {
                _kierowca = null;
                Kierowca = null;
            }
            Kierowca = Kierowcy.FirstOrDefault(x => x.ID_IHP_KIEROWCA == _samochod.ID_IHP_KIEROWCA);

            if (_kontraharch != null)
            {
                _kontraharch = null;
                KontrahArch = null;
            }
            KontrahArch = KontrahsArch.FirstOrDefault(x => x.ID_IHP_KONTRAHENT == _kierowca.ID_IHP_KONTRAHENT && x.AKTYWNY==1);
            RodzajDok = RodzajeDok.FirstOrDefault(x => x.ID_IHP_RODZAJDOK == _kontraharch.ID_IHP_RODZAJDOKDEF);
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
                OnRisePropertyChanged("Kontrah");
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
            
                OnRisePropertyChanged("Samochod");
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
                OnRisePropertyChanged("Kierowca");
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
              
                OnRisePropertyChanged("RodzajDok");
            }
        }
        private ObservableCollection<IHP_KONTRAHENT_ARCH> _kontrahs;
        public ObservableCollection<IHP_KONTRAHENT_ARCH> Kontrahs
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
        public  ObservableCollection<IHP_KONTRAHENT_ARCH> KontrahsArch
        {
            get
            {
                return _kontrahsarch;
            }
            set
            {
                _kontrahsarch = value;
                OnRisePropertyChanged("KontrahsArch");
            }
        }
       private  IHP_KONTRAHENT_ARCH _kontraharch;
        public IHP_KONTRAHENT_ARCH KontrahArch
        {
            get
            {
                return _kontraharch;
            }
            set
            {
                _kontraharch = value;
                OnRisePropertyChanged("KontrahArch");
            }
        }
        private bool _filtrdanych;
        public bool FiltrDanych
        {
            get
            {
                return _filtrdanych;
            }
            set
            {
                _filtrdanych = value;
                OnRisePropertyChanged("FiltrDanych");
            }
        }
        private bool _listawazen;
        public bool ListaWazen
        {
            get
            {
                return _listawazen;
            }
            set
            {
                _listawazen = value;
                OnRisePropertyChanged("ListaWazen");
            }
        }
        private bool _ustawienie;
        public bool UstawienieDanych
        {
            get
            {
                return _ustawienie;
            }
            set
            {
                _ustawienie = value;
                OnRisePropertyChanged("UstawienieDanych");
            }
        }
        public void UstawienieDanychVd()
        {
            if (UstawienieDanych)
                UstawienieDanych = false;
            else
                if (UstawienieDanych == false)
                UstawienieDanych = true;
        }
        public void PanelFiltr()
        {
            if (FiltrDanych)
                FiltrDanych = false;
            else
                if (FiltrDanych == false)
                FiltrDanych = true;
        }
       private void Flitr()
        {
            if(KontrahArch!=null)
            _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo && x.ID_IHP_KONTRAHENT_ARCH.Equals(KontrahArch.ID_IHP_KONTRAHENT_ARCH)).OrderByDescending(x => x.DATAWPISU).ToList();
            else
                _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo).OrderByDescending(x => x.DATAWPISU).ToList();

            Dokumenty.Clear();
            foreach (IHP_NAGLDOK item in _listadokumentow)
            {
                Dokumenty.Add(item);
            }
        }
        private void KasujKolory()
        {
            KolorDzien  = KolorTydzien = KolorMiesiac = KolorRok =  Color.FromRgb(211, 211, 211);
            wybierzdzien = wybierztydzien = wybierzmiesiac = wybierzrok = false;
         
        }
        public void PanelListaWazen()
        {
            if (ListaWazen)
                ListaWazen = false;
            else
                if (ListaWazen == false)
                ListaWazen = true;
        }
        private string _nrrej;
        public string NrRej
        {
            get
            {
                return _nrrej;
            }
            set
            {
                _nrrej = value;
                OnRisePropertyChanged("NrRej");
           }
        }
        private string _nazwa;
        public string Nazwa
        {
            get
            {
                return _nazwa;
            }
            set
            {
                _nazwa = value;
                OnRisePropertyChanged("Nazwa");
            }
        }
        private string _wagakg;
        public string WagaKg
            {
            get
            {
                return _wagakg;
            }
            set
            {
                _wagakg = value;
                OnRisePropertyChanged("WagaKg");
            }

      }
        private DateTime _datawjazd;
        public DateTime DataWjazd
        {
            get
            {
                return _datawjazd;
            }
            set
            {
                _datawjazd = value;
                OnRisePropertyChanged("DataWjazd");
            }
        }
        private DateTime _datawyjazd;
        public DateTime DataWyjazd
        {
            get
            {
                return _datawyjazd;
            }
            set
            {
                _datawyjazd = value;
                OnRisePropertyChanged("DataWyjazd");
            }
        }
        private int _wagawjazd;
        public int WagaWjazd
        {
            get
            {
                return _wagawjazd;
            }
            set
            {
                _wagawjazd = value;
                OnRisePropertyChanged("WagaWjazd");
            }
        }
        private int _wagawyjazd;
        public int WagaWyjazd
        {
            get
            {
                return _wagawyjazd;
            }
            set
            {
                _wagawyjazd = value;
                OnRisePropertyChanged("WagaWyjazd");
            }
        }
        private decimal _waga;
        public decimal Waga
        {
            get
            {
                  return _waga;
            }
            set
            {
        
                _waga = value;
                OnRisePropertyChanged("Waga");
            }
        }
        private bool CanDelete()
         {
            if (_dokument != null)
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
       public void LoadCollection()
        {
 
        }
        public void ShowDok()
        {
            if(Dokument!=null)
            {
    
              var window = new PozDok();
                Messenger.Default.Send<int>(Dokument.ID_IHP_NAGLDOK);
                window.Show();
            }
        }
        public void LoadDOkData()
        {
            if (wybierzdzien)
                UstawDzien();
            if (wybierztydzien)
                UstawTydzien();
            if (wybierzmiesiac)
                UstawMiesiac();
            if (wybierzrok)
                UstawRok();

        }
        public void ReloadCollection()
        {
            _listasam = context.IHP_SAMOCHOD.ToList();
            Samochody.Clear();
            foreach (IHP_SAMOCHOD item in _listasam)
            {
                Samochody.Add(item);
            }
        }
        private void WykonajWazenie()
        {
 
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
                    LoadCollection();
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
                    LoadCollection();
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
            string LastMessage = string.Empty;
            if (_wazenie != null)

            {
                try
                {
                 //   DokumentyRepository DokRepo = new DokumentyRepository(_wazenie.ID_IHP_WAZENIE, 0);
                  //  DokRepo.Save();
                    LoadCollection();
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
        private void PrzeliczWage()
        {
         if(_wazenie!= null)
            {
                if ((_wazenie.WAGAWYJAZD > 0) && (_wazenie.WAGAWJAZD > 0))
                    Waga = Math.Abs(_wazenie.WAGAWJAZD - _wazenie.WAGAWYJAZD);
                else
                    Waga = 0;
            }

       }
        private void Delete()
        {
            string LastMessage;
            try
            {
               if(_dokument != null)
                {
                    MessageBoxResult result = MessageBox.Show("Czy Napewno Usunąć Dokument: " + _dokument.NRDOKWEW +  " ??", "Potwierdź", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                      //  DokumentyRepository DokRepo = new DokumentyRepository(_wazenie, _dokument);
                      //  DokRepo.DeleteDok();
                        Clear();
                        LoadCollection();
                        LoadDOkData();
                    }
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
        public void  SentSamochod()
        {
            Messenger.Default.Send<List<IHP_SAMOCHOD>>(_listasam);
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
            if (_samochod != null)
            {
                Samochod = null;
                _samochod = null;
            
            }

           if(_kontrah!= null)
            {
                _kontrah = null;
                Kontrah = null;
           
            }

            if (_kontraharch!= null)
            {
                _kontraharch = null;
                KontrahArch = null;
               
            }

           if( _kierowca != null)
            {
               _kierowca = null;
               Kierowca = null;
             }

            if( _rodzajdok != null )
            {
                _rodzajdok = null;
            }
          if(_wazenie!= null)
            {
                Wazenie = null;
               _wazenie = null;
             
            }             
             _nrrej = string.Empty;
              NrRej = string.Empty;
              _nazwa = string.Empty;
               Nazwa = string.Empty;
              _wagakg = string.Empty;
               WagaKg = string.Empty;
        }
        private void Update()
        {
            _datawyjazd = DateTime.Now;
            try
             {
              if (_wazenie != null)
                {
                    _wazenie.IHP_KIEROWCA = _kierowca;
                    _wazenie.IHP_SAMOCHOD = _samochod;
                    _wazenie.IHP_KONTRAHENT_ARCH = _kontraharch;
                    _wazenie.IHP_KARTOTEKA = _kartoteka;
                    _wazenie.DATAWYJAZD = _datawyjazd;
                   _wazenie.WAGAWYJAZD = _ramka.Waga;
                    context.IHP_WAZENIE.Attach(_wazenie);
                    context.Entry(_wazenie).State = EntityState.Modified;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                  }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:" , eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"",    ve.PropertyName, ve.ErrorMessage));
                    }
                }
                throw ;
            }
        }
        private void AddSubject()
        {
      //      SubjectSfera sb = new SubjectSfera();
     //        sb.DodajKontrahenta();
 
       }
        public void OnMessageKierowca(List<IHP_KIEROWCA> Name)
        {
            //this._listakierowcy = Name;
            _listakierowcy = context.IHP_KIEROWCA.ToList();
             Kierowcy.Clear();
            foreach (IHP_KIEROWCA item in _listakierowcy)
            {
                Kierowcy.Add(item);
            }
        }
        public void OnMessageKontrahent(List<IHP_KONTRAHENT_ARCH> Name)
        {
            //this._listakierowcy = Name;
            _listakontrah = context.IHP_KONTRAHENT_ARCH.ToList();
            if(Kontrahs!=null)
            {
                Kontrahs.Clear();
                foreach (IHP_KONTRAHENT_ARCH item in _listakontrah)
                {
                    Kontrahs.Add(item);
                }
            }
       
        }
        public void OnMessageKartoteka(List<IHP_KARTOTEKA> Name)
        {
            _listakartoteka = context.IHP_KARTOTEKA.ToList();
            Kartoteki.Clear();
            foreach (IHP_KARTOTEKA item in _listakartoteka)
            {
                Kartoteki.Add(item);
            }

        }
        public void OnMessageSamochod(List<IHP_SAMOCHOD> Name)
        {
            //this._listakierowcy = Name;
            _listasam = context.IHP_SAMOCHOD.ToList();
            Samochody.Clear();
            foreach (IHP_SAMOCHOD item in _listasam)
            {
                Samochody.Add(item);
            }
        }
        public void OnMessageDok(RejWagaMessage msg)
        {
            if(msg.RodzajMessage==2)
                         LoadDOkData();
        }
        public void OnRefresh(int Co)
        {
            if(Co==3)
                       LoadCollection();

        }
        private void UstawDzien()
        {
    
                  DateTime currentDateTime = DateTime.Now;
            DateOd = currentDateTime.Date;
            DateDo = currentDateTime.Date.AddDays(1).AddTicks(-1);
           _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo).OrderByDescending(x=>x.DATAWPISU).ToList();

            Dokumenty.Clear();

            foreach (IHP_NAGLDOK item in _listadokumentow)
            {
                Dokumenty.Add(item);
            }
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
            _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo).OrderByDescending(x => x.DATAWPISU).ToList();

            Dokumenty.Clear();

            foreach (IHP_NAGLDOK item in _listadokumentow)
            {
                Dokumenty.Add(item);
            }
            KasujKolory();
             wybierztydzien =  true;
            //KolorTydzien  = Color.FromRgb(135, 206, 250);
            KolorTydzien = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawMiesiac()
        {
              DateOd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
              DateDo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
            _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo).OrderByDescending(x => x.DATAWPISU).ToList();

            Dokumenty.Clear();

            foreach (IHP_NAGLDOK item in _listadokumentow)
            {
                Dokumenty.Add(item);
            }
             KasujKolory();
        wybierzmiesiac   = true;
            //KolorMiesiac  = Color.FromRgb(135, 206, 250);
            KolorMiesiac = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawRok()
        {
              DateOd = new DateTime(DateTime.Today.Year, 1, 1);
            DateDo = new DateTime(DateTime.Today.Year, 12, 31);
            _listadokumentow = context.IHP_NAGLDOK.Where(x => x.DATADOK >= DateOd && x.DATADOK <= DateDo).OrderByDescending(x => x.DATAWPISU).ToList();
            Dokumenty.Clear();
            foreach (IHP_NAGLDOK item in _listadokumentow)
            {
                Dokumenty.Add(item);
            }
            KasujKolory();
            //KolorRok  = Color.FromRgb(135, 206, 250);
           wybierzrok = true;
            KolorRok = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
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
            IHP_WAZENIE ostWaga = new IHP_WAZENIE();
            ostWaga = context.IHP_WAZENIE.OrderByDescending(x => x.ID_IHP_SAMOCHOD == ID_IHP_SAMOCHOD).FirstOrDefault();
            res.DataOstatniegoWazenia = ostWaga.DATAWJAZD ?? default(DateTime);
            res.TaraPojazduOstWaga = ostWaga.WAGAWJAZD;
            return res;
        }
 
        }
    public class ColorToBrushConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color)
                return new SolidColorBrush((Color)value);
            return new SolidColorBrush();
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }

}








