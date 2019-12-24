using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;

using DevExpress.Mvvm.UI.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using KpInfohelp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Validation;
//using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace KpInfohelp
{
    public class ViewModelOferta : DokumentyRepository, INotifyPropertyChanged, IMVVMDockingProperties
    {
        private decimal _wartbrutto, _wartvat,  _oldCenaNetto , _sumawartnetto, _sumawartbrutto, _sumawartvat, _sumailoscmat, _sumawartmat, _sumawartobr;

        private int _lppoz = 0;

        public virtual string FocusedElement { get; set; }

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public ViewModelOferta()
        {
            InitLists();
            InitCommands();
            SeedData();
            IsPopupOpenKontrah = true;
            Messenger.Default.Register<int>(this, OnMessage);
        }

        #region Listy
        public ObservableCollection<IHP_PRIORYTET> Priorytety { get; set; }
       public List<WystJednDodatView> JednZastSelAllLst { get; set; }
        public ObservableCollection<WystJednDodatView> JednZastSelAll { get; set; }

        private ObservableCollection<IHP_STAWKAVAT> _stawkavat;
        public ObservableCollection<IHP_STAWKAVAT> StawkaVat
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
        private ObservableCollection<IHP_RODZAJDOK> _rodzajdoklst;
        public ObservableCollection<IHP_RODZAJDOK> RodzajDokLst
        {
            get
            {
                return _rodzajdoklst;
            }
            set
            {
                _rodzajdoklst = value;
                RisePropertyChanged("RodzajDokLst");
            }
        }
        private ObservableCollection<IHP_KAROTEKA_EX> _kartoteki;
        public ObservableCollection<IHP_KAROTEKA_EX> Kartoteki
        {
            get
            {
                return _kartoteki;
            }
            set
            {
                _kartoteki = value;
                RisePropertyChanged("Kartoteki");
            }
        }
        public ObservableCollection<IHP_GRUPAKART> GrupyKart { get; private set; }
        public ObservableCollection<IHP_JM> lstJm { get; private set; }
        private ObservableCollection<IHP_POZDOK> _poz;
        public ObservableCollection<IHP_POZDOK> Poz
        {
            get
            {
                return _poz;
            }
            set
            {
                _poz = value;
                RisePropertyChanged("Poz");
            }
        }
        private List<IHP_POZDOK> _lstpoz;
        private List<IHP_CENNIK> _lstcennik;
        List<IHP_KARTOTEKA> _listakartoteka;
      
        private ObservableCollection<ZamowieniaViewListaExp> _listaexpzaz;
        public ObservableCollection<ZamowieniaViewListaExp> listaExpZazn {
            get
            {
                return _listaexpzaz;
            }
            set
            {
                _listaexpzaz = value;
                RisePropertyChanged("listaExpZazn");

            }
        }
        public   ObservableCollection<ZamowieniaViewListapozExpfrm> ListaZamowieniaViewListapozExpfrm {get;set;}
        #endregion
 
        #region Przyciski
        public ICommand EditPozCommand { get; set; }
        public ICommand FocusCommand
        {
            get; set;
        }
        public ICommand _updateCommand
        {
            get;
            set;
        }
        public ICommand _deleteComand
        {
            get;
            set;
        }
        public ICommand RefreshCommand { get; private set; }
        public ICommand CommandPrzyjete { get; private set; }
        public ICommand CommandwTrakcie { get; private set; }
        public ICommand CommandFiltruj { get; private set; }
        public ICommand CommandClearFiltr { get; set; }
        public ICommand CommandSearchFiltr { get; set; }
        public ICommand CommandPokazOkno { get; private set; }
        public ICommand DayCommand { get; private set; }
        public ICommand WeekCommand { get; private set; }
        public ICommand MonthCommand { get; private set; }
        public ICommand YearCommand { get; private set; }
        public ICommand ShowDokCommand { get; private set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand DeletePozCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand PanelExportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ZaznaczWszystkoCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand OpenCloseKontrahCommand { get; set; }
        public ICommand SetFocusCommand { get; set; }
        public ICommand CalcValWgPaczekCommand { get; set; }
        public ICommand ZapiszObr { get; set; }
        public ICommand Zatwierdz { get; set; }
        public ICommand FocusToSearch { get; set; }

        #endregion

        #region Repo

        KartotekaRepository KrRepo = new KartotekaRepository();

        #endregion

        #region Property pol
        private int NrDok;
        private string nrdokwew;
        public string NrDokWew
        {
            get
            {
                return nrdokwew;
            }
            set
            {
                nrdokwew = value;
                RisePropertyChanged("NrDokWew");
            }
        }
        private bool _panelexport;
        private bool pokazpanel;
        public bool PokazFiltr {
            get
            {
                return pokazpanel;
            }
                
                set
            {
                pokazpanel = value;
                RisePropertyChanged("PokazFiltr");
            }
          }
        private bool zaznodzn;
        private string ZaznaczOdznacz;
        public int SelectedGrupaKartID { get; set; }
        private decimal _wartnetto;
        public decimal  Wartnetto
        {
            get
            {
                return _wartnetto;
            }
            set
            {
                _wartnetto = value;
                RisePropertyChanged("Wartnetto");
            }
        }
        public decimal Wartvat
        {
            get
            {
                return _wartvat;
            }
            set
            {
                _wartvat = value;
                RisePropertyChanged("Wartvat");
            }
        }
        public decimal Wartbrutto
        {
            get
            {
                return _wartbrutto;
            }
            set
            {
                _wartbrutto = value;
                RisePropertyChanged("Wartbrutto");
            }
        }
        private decimal _wartoscrazem;
        public decimal WartoscRazem
        {
            get
            {
                return _wartoscrazem;
            }
            set
            {
                _wartoscrazem = value;
                RisePropertyChanged("WartoscRazem");
            }

        }
        public decimal SumaWartNetto
        {
            get
            {
                return _sumawartnetto;
            }
            set
            {
                _sumawartnetto = value;
                RisePropertyChanged("SumaWartNetto");
            }
        }
        public decimal SumaWartObr
        {
            get
            {
                return _sumawartobr;
            }
            set
            {
                _sumawartobr = value;
                RisePropertyChanged("SumaWartObr");
            }
        }
        public decimal SumaIloscMat
        {
            get
            {
                return _sumailoscmat;
            }
            set
            {
                _sumailoscmat = value;
                RisePropertyChanged("SumaIloscMat");
            }
        }
        public decimal SumaWartMat
        {
            get
            {
                return _sumawartmat;
            }
            set
            {
                _sumawartmat = value;
                RisePropertyChanged("SumaWartMat");
            }
        }
        public decimal SumaWartBrutto
        {
            get
            {
                return _sumawartbrutto;
            }
            set
            {
                _sumawartbrutto = value;
                RisePropertyChanged("SumaWartBrutto");
            }
        }
        public decimal SumaWartVat
        {
            get
            {
                return _sumawartvat;
            }
            set
            {
                _sumawartvat = value;
                RisePropertyChanged("SumaWartVat");
            }
        }

        private decimal _cenauzgnetto;
        public decimal CenaUzgNetto
        {
            get
            {
                return _cenauzgnetto;
            }
            set
            {
                _cenauzgnetto = value;
                RisePropertyChanged("CenaUzgNetto");
            }
        }
        private decimal _cenauzgbrutto;
        public decimal CenaUzgBrutto
        {
            get
            {
                return _cenauzgbrutto;
            }
            set
            {
                _cenauzgbrutto = value;
                RisePropertyChanged("CenaUzgBrutto");
            }
        }

        private bool _isuzgodnioneobr;
        public bool IsUzgodnioneObr
        {
            get
            {
                return _isuzgodnioneobr;
            }
            set
            {
                _isuzgodnioneobr = value;
                if (_isuzgodnioneobr == false)

                    RisePropertyChanged("IsUzgodnioneObr");
            }
        }

        private bool _txtsztil;
        public bool Txtsztil
        {
            get
            {
                return _txtsztil; 
            }
            set
            {
                _txtsztil = value;
                RisePropertyChanged("Txtsztil");
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

        private decimal _iloscwpaczce;
        public decimal IloscWpaczce
        {
            get
            {
                return _iloscwpaczce;
            }
            set
            {
                _iloscwpaczce = value;
                RisePropertyChanged("IloscWpaczce");
            }
        }

        private decimal _iloscrazem;
        public decimal  IloscRazem
        {
            get
            {
                return _iloscrazem;
            }
            set
            {
                _iloscrazem = value;
                RisePropertyChanged("IloscRazem");
            }
        }

        private decimal _iloscpaczek;
        public decimal IloscPaczek
        {
            get
            {
                return _iloscpaczek;
            }
            set
            {
                _iloscpaczek = value;
                RisePropertyChanged("IloscPaczek");
            }
        }

        public decimal _vat;
        public decimal VAT
        {
            get
            {
                return _vat;
            }
            set
            {
                _vat = value;
                RisePropertyChanged("VAT");
            }
        }

        private string _searchtext;
        public string SearchText
        {
            get
            {
                return _searchtext;
            }
            set
            {
                _searchtext = value;
                 RisePropertyChanged("SearchText");
            }
        }
        #endregion
        #region Metody 
        public void OnMessage(int NrMessage)
        {
            if (NrMessage==1)
                LoadCollectionKh();
        }
        void InitLists()
        {
            StawkaVat = new ObservableCollection<IHP_STAWKAVAT>(Context.IHP_STAWKAVAT);
            RodzajDokLst = new ObservableCollection<IHP_RODZAJDOK>(Context.IHP_RODZAJDOK);
        
            ZamowieniaViewStatusLstNagl = new ObservableCollection<ZamowieniaViewStatusNagl>();
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>(Context.IHP_KONTRAHENT);
            listaExpZazn = new ObservableCollection<ZamowieniaViewListaExp>();
            Kartoteki = new ObservableCollection<IHP_KAROTEKA_EX>();
            ZamowieniaViewStatusLst = new ObservableCollection<ZamowieniaViewStatus>();
            MenuItems = new ObservableCollection<ItemInfo>();
            Poz = new ObservableCollection<IHP_POZDOK>();
            JednZastSelAll  = new ObservableCollection<WystJednDodatView>();
            Priorytety = new ObservableCollection<IHP_PRIORYTET>(Context.IHP_PRIORYTET);
            Priorytet = Context.IHP_PRIORYTET?.FirstOrDefault(x => x.AKTYWNY == 1);
        }
        void InitCommands()
    {
        RefreshCommand = new DelegateCommand(Refresh);
        EditPozCommand = new DelegateCommand(EditPoz);
        DeleteCommand = new DelegateCommand(DeleteZam);
        DeletePozCommand = new DelegateCommand(DeletePozEdit); 
        OpenCloseKontrahCommand = new DelegateCommand(OpenCloseKontrah);
        SetFocusCommand = new DelegateCommand<int>(SetFocus);
        FocusCommand = new DelegateCommand(FocusCommandHandler);
        CalcValWgPaczekCommand = new DelegateCommand(PrzeliczPaczki);
        ZapiszObr = new DelegateCommand(SaveMatForEdit);
        Zatwierdz =  new DelegateCommand(Clear);
        FocusToSearch = new DelegateCommand(FocusToSearchTextKart);
        }
        void SeedData()
    {
        LoadCollectionKh();
        LoadCollectionKartoteka();
        SeedCennik();
        SearchText = " ";
        }

        private string GenNumerDokWew()
        {
            NrDok = GetNextNumer(5);
            return "ZAM/" + NrDok.ToString();
        }
        public void CreateOrder()
        {
            Nagl = new IHP_NAGLDOK();
            try
            {
                NrDokWew = GenNumerDokWew();
                int NrDok = GenNumerDok();
                Nagl.DATADOK = DateTime.Today;
                Nagl.ID_IHP_NAGLDOK = GetNextNumer(8);
                Nagl.ID_RODZAJDOK = 1;
                Nagl.ID_IHP_PRIORYTET = 1;
              
               Nagl.TERMINREALIZ = DateTime.Today;
                if (_kontrahnet != null)
                {
                    //     Nagl.KONTRAH = _kontrah;
                    //     Nagl.ID_KONTRAH = _kontrah.ID_KONTRAH;
                    Nagl.IHP_KONTRAHENT_ARCH = Context.IHP_KONTRAHENT_ARCH.FirstOrDefault(x => x.ID_IHP_KONTRAHENT == _kontrahnet.ID_IHP_KONTRAHENT);
                    Nagl.ID_IHP_KONTRAHENT = _kontrahnet.ID_IHP_KONTRAHENT;
                }
                Nagl.NRDOKWEW = NrDokWew;
                Nagl.NRDOK = NrDok;
        

                EdycjaZamowienia = true;
                Add(Nagl);
            }

            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
            }
    }
        private int GetLp()
        {
            _lppoz++;
            return _lppoz;
        }
        private void SelectedKontrah()
        {
            if (_kontrahnet == null) return;
            if (EdycjaZamowienia) //tylko zmieniamy kontrahenta 
            {
                if (Nagl != null)
                {
                    RewriteOrder();
                }
            }
            else
            {
                CreateOrder();
            }
        }
        private void RewriteOrder()
        {
            int oldkontarh = _kontrahnet.ID_IHP_KONTRAHENT;
            int rodzdok = 1;
            int priorytetlocal = 1;
            DateTime DataDok = _nagl.DATADOK;
            DateTime DataTermin = _nagl.TERMINREALIZ ?? DateTime.Today;
            string oldnrdokwew = _nagl.NRDOKWEW;
            if (Kontrahent != null) Kontrahent = null;
            if (_kontrahnet != null) _kontrahnet = null;
            if (RodzajDokSel != null) rodzdok = RodzajDokSel.ID_IHP_RODZAJDOK;
            if (Priorytet != null)
                priorytetlocal = Priorytet.ID_IHP_PRIORYTET;
            Nagl = new IHP_NAGLDOK();
            Nagl.IHP_KONTRAHENT_ARCH = Context.IHP_KONTRAHENT_ARCH.FirstOrDefault(x => x.ID_IHP_KONTRAHENT == oldkontarh);
            Nagl.ID_IHP_KONTRAHENT = oldkontarh;
 
           NrDokWew = oldnrdokwew;
            Nagl.NRDOKWEW = oldnrdokwew;
            Nagl.NRDOK = NrDok;
            Nagl.ID_RODZAJDOK = rodzdok;
            Nagl.DATADOK = DataDok;
            Nagl.TERMINREALIZ = DataTermin;

            RisePropertyChanged("Nagl");
            RisePropertyChanged("Kontrahent");
            RisePropertyChanged("NrDokWew");
            EdycjaZamowienia = true;

            SetFocus(7);

        }
        public void DetachAll()
        {
            //       context.Entry(Nagl).State = EntityState.Detached;
            try
            {
                var entries = ((DbContext)Context).ChangeTracker.Entries();
                foreach (var entry in entries)
                {
                    entry.State = EntityState.Detached;
                }
            }

            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }

        }
        private void Clear()
        {
            EdycjaZamowienia = false;
        
            Nagl = new IHP_NAGLDOK();
            Nagl.DATADOK = DateTime.Today;
            Nagl.BLOKADA = 1;
           Nagl.TERMINREALIZ = DateTime.Today;
             _lstpoz = new List<IHP_POZDOK>();
            IsPopupOpenKontrah = true;
            NrDokWew = string.Empty;
            Kontrahent = null;
            Poz.Clear();
            PozJ = null;
            _lppoz = 0;
            ClearPoz();


        }

        private void PopulatePozNagl()
        {
            Poz.Clear();
            _lstpoz.Clear();
            if (_nagl == null) return;
            if (_nagl.IHP_POZDOK.Count > 0)
            {
                foreach (IHP_POZDOK item in _nagl.IHP_POZDOK)
                {
                    Poz.Add(item);
                    _lstpoz.Add(item);
                }
            }
            SumaWartNetto = Poz.Sum(x => x.WARTNETTO);
            SumaWartVat = Poz.Sum(x => x.WARTVAT);
            SumaWartBrutto = Poz.Sum(x => x.WARTBRUTTO);
            WartoscRazem = SumaWartBrutto;
        }
        private void SeedCennik()
        {
            CennikiRepository cr = new CennikiRepository();
            _lstcennik = cr.GetAll();
        }
        void GetJednZast()
        {
            if (_kartotekasel != null)
                JednZastSelAllLst = KrRepo.GetWstyJednDodatAll(_kartotekasel.ID_IHP_KARTOTEKA);
            JednZastSelAll.Clear();
            foreach (WystJednDodatView item in JednZastSelAllLst)
                JednZastSelAll.Add(item);
            if (JednZastSelAll.Count > 0)
                JednZastSel = JednZastSelAll?.FirstOrDefault(x => x.AKTYWNA == 1);
            if (JednZastSel != null)
                IloscWpaczce = JednZastSel.WARTOSC;
        }
        private void DeletePozEdit()
        {
            DelPoz(_pozj);
            PopulatePozEdit();
            //wiocha !!! trzeba cos wymyslec na tego focusa
            FocusToSearchTextKart();
        }
        private void ClearPoz()
        {
            if (KartotekaSel != null) KartotekaSel = null;
            if (_kartotekasel != null) _kartotekasel = null;
         
            CenaUzgNetto = 0; _cenauzgnetto = 0; CenaUzgBrutto = 0;
            Wartnetto = 0; Wartvat  = 0; Wartbrutto = 0;Ilosc = 0;
            Ilosc = 0; IloscPaczek = 0; IloscRazem = 0;
            JednZastSelAll.Clear();
             SearchText = string.Empty;
             UstawFiltr();
           

        }
        private void DeleteZam()
        {
            string LastMessage;
            try
            {
                if (Nagl != null)
                {
                    MessageBoxResult result = MessageBox.Show("Czy Napewno Usunąć Dokument: " + Nagl.NRDOKWEW + " ??", "Potwierdź", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                         DelPozRange(Nagl.IHP_POZDOK.ToList());
                         Delete(Nagl);
                         Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                MessageBox.Show(LastMessage);
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
         public void LoadCollectionHistNagl(int IdNagl)
        {

            var zamowienia = Context.Database.SqlQuery<ZamowieniaViewStatusNagl>(string.Format(@"select K.INDEKS, SH.ID_STATUSHISTORIA, SH.ID_DEFSTATUS, SH.DATAWPISU, DS.NAZWA, SH.OPIS, SH.ID_POZ, AZ.ID_ARIT_ZAM_USERS,
                  SH.id_defstatus, DS.nazwa as  NAZWAZ, AZ.login as UZYTKOWNIK  from NAGL N  join POZ P on P.id_nagl = n.id_nagl
                    join KARTOTEKA K on k.id_kartoteka = p.id_kartoteka  join statushistoria SH on SH.id_poz = P.id_poz
                    join DEFSTATUS DS on DS.id_defstatus = SH.id_defstatus
                    join arit_zam_users az on az.id_arit_zam_users = sh.id_arit_zam_users
                  where n.id_nagl ={0}", IdNagl.ToString())).ToList();

            ZamowieniaViewStatusLstNagl.Clear();
            foreach (var q in zamowienia)
            {
                ZamowieniaViewStatusNagl zv = new ZamowieniaViewStatusNagl();
                zv.INDEKS = q.INDEKS;
                zv.DATAWPISU = q.DATAWPISU;
                zv.NAZWA = q.NAZWA;
                zv.NAZWAZ = q.NAZWAZ;
                zv.UZYTKOWNIK = q.UZYTKOWNIK;
                ZamowieniaViewStatusLstNagl.Add(zv);
            }
        }
        void Refresh()
        {
         
        }
        public void LoadCollectionKh()
        {
            Kontrahs.Clear();
            _listakontrah = new List<IHP_KONTRAHENT>();
            _listakontrah = Context.IHP_KONTRAHENT.ToList();

            foreach (IHP_KONTRAHENT item in _listakontrah)
            {
                Kontrahs.Add(item);
            }
        }
        private void SetFocus(int parameter)
        {
            switch (parameter)
            {
                case 1:
                    Ilosc = _ilosc;
                    break;
                case 2:
                    IloscRazem = Ilosc;
                    IloscPaczek = 0;
                    PrzeliczWartosci();
                    break;
                case 3:
                    FocusToSearchTextKart();
                     UstawFiltr();
                    break;
                case 4:
                    UstawFiltr();
                    break;
                case 5:
                    KartotekaSel = Kartoteki[0];
                    RisePropertyChanged("KartotekaSel");
                    break;
            }
        }

        private void FocusToSearchTextKart()
        {
            SearchText = " ";
            RisePropertyChanged("SearchText");
            SearchText = string.Empty;
        }
        private void FocusCommandHandler()
        {
            switch (FocusedElement)
            {
                case "One":
                    FocusedElement = "Two";
                    break;
                default:
                    FocusedElement = "One";
                    break;
            }
        }
        private void EditPoz()
        {
        
            if (_lstcennik == null) return;
            if (Nagl.IHP_KONTRAHENT_ARCH == null) return;

            Cennik = _lstcennik?.FirstOrDefault(x =>x.ID_IHP_KARTOTEKA == (_kartotekasel.ID_IHP_KARTOTEKA) && (x.ID_IHP_DEFCENY == Nagl.IHP_KONTRAHENT_ARCH.ID_IHP_DEFCENY));
             CenaUzgNetto = _lstcennik?.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == (_kartotekasel.ID_IHP_KARTOTEKA) && (x.ID_IHP_DEFCENY == Nagl.IHP_KONTRAHENT_ARCH.ID_IHP_DEFCENY))?.CENAN ?? 0;
             CenaUzgBrutto = _lstcennik?.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == (_kartotekasel.ID_IHP_KARTOTEKA) && (x.ID_IHP_DEFCENY == Nagl.IHP_KONTRAHENT_ARCH.ID_IHP_DEFCENY))?.CENAB ?? 0;
            VAT = _stawkavat?.FirstOrDefault(x => x.ID_IHP_STAWKAVAT == _kartotekasel.ID_IHP_STAWKAVAT).WARTOSC ?? 0;
            Ilosc = 1.00m;
            if(Cennik == null)
            {
                MessageBox.Show("Uwaga Brak Ceny w Cenniku !!!");
            }
            GetJednZast();
             SetFocus(1);
        }
        private void SaveMatForEdit()
        {
            if (_iloscrazem == 0) return;
            if (_kartotekasel == null) return;
            short cenauzgzam = 0;
            if (_isuzgodnione)
                cenauzgzam = 1;

            IHP_POZDOK poz = new IHP_POZDOK()
            {
                ID_IHP_POZDOK = GetNextNumer(2),
                ID_IHP_KARTOTEKA = _kartotekasel.ID_IHP_KARTOTEKA,
                NAZWASKRPOZ = _kartotekasel.NAZWASKR,
                ID_IHP_NAGLDOK = Nagl.ID_IHP_NAGLDOK,
                LP = GetLp(),
                ILOSC = _ilosc,
                ILOSCPACZKA =_iloscpaczek,
                ILOSCRAZEM = _iloscrazem,
                CENANETTO = _cenauzgnetto,
                CENABRUTTO = _cenauzgbrutto,
                WARTNETTO = _wartnetto,
                WARTBRUTTO = _wartbrutto,
                WARTVAT = _wartvat,
                CENAUSTALONA = cenauzgzam,

            };
            AddPoz(poz);
            //tutaj dodaj w repo
            //context.POZ.Add(poz);
            //context.SaveChanges();
            //PrzenumerujListeEdit();
            //PopulatePozNaglforEdit();
            PopulatePozEdit();
           ClearPoz(); 
         }
        private void PopulatePozEdit()
        {
            List<IHP_POZDOK> _poz = Context.IHP_POZDOK.Where(x => x.ID_IHP_NAGLDOK == _nagl.ID_IHP_NAGLDOK).OrderBy(x => x.LP).ToList();
            Poz.Clear();
            foreach (IHP_POZDOK item in _poz)
            {
                Poz.Add(item);
            }
            SumaWartNetto = Poz.Sum(x => x.WARTNETTO);
            SumaWartBrutto = Poz.Sum(x => x.WARTBRUTTO);
            WartoscRazem = SumaWartBrutto;
            SumaWartVat = Poz.Sum(x => x.WARTVAT);
            SumaIloscMat = Poz.Sum(x => x.ILOSC);
            SumaWartMat = Poz.Sum(x => x.WARTBRUTTO);
 
        //    PrzeliczWartoscWgCeny();
        }
        void PrzeliczPaczki()
        {
            if(JednZastSel!=null)
            {
                Ilosc = 0;
                IloscRazem = Math.Round(IloscPaczek * JednZastSel.WARTOSC, 2);
                PrzeliczWartosci();
            }
        }
        void  PrzeliczWartosci()
        {
            Wartnetto =   Math.Round(IloscRazem * CenaUzgNetto, 2 );
            Wartvat = (Wartnetto * _vat) / 100;
            Wartvat = Math.Round(Wartvat, 2);
            Wartbrutto = Wartnetto + Wartvat;
        }
        public void LoadCollectionKartoteka()
        {
            Kartoteki.Clear();
            _listakartoteka = new List<IHP_KARTOTEKA>();
            _listakartoteka = Context.IHP_KARTOTEKA.ToList();

            foreach (IHP_KARTOTEKA item in _listakartoteka)
            {
                Kartoteki.Add(new IHP_KAROTEKA_EX(Context, item));
            }
       
        }
        private void OpenCloseKontrah()
        {
            if(!IsPopupKontrah)
            IsPopupKontrah = true;
            else
                IsPopupKontrah =false;

        }
        private void UstawFiltr()
        {
            if (String.IsNullOrEmpty(SearchText))
                LoadCollectionKartoteka();
            else
            {
                Kartoteki.Clear();
            
                foreach (IHP_KARTOTEKA item in _listakartoteka.Where(x => x.NAZWADL.Contains(SearchText)
                                                                    || x.INDEKS.Contains(SearchText)
                                                                    || x.NAZWASKR.Contains(SearchText)))
                    
                       Kartoteki.Add(new IHP_KAROTEKA_EX(Context, item));
            }

            if (Kartoteki.Count() == 1)
            {
                _kartotekasel = Kartoteki.FirstOrDefault();
                EditPoz();
            }
            
        }
        #endregion
        
        #region FIltr Danych
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
                return _kolor;
            }
            set
            {
                _kolor = value;
                RisePropertyChanged("Kolor");
            }
        }
        #endregion
        
        #region Listy
    
        private ObservableCollection<ViewStatusy> _listastatus;
        public ObservableCollection<ViewStatusy> ListaStatus
        {
            get
            {
                return _listastatus;
            }
            set
            {
                _listastatus = value;
                RisePropertyChanged("ListaStatus");
            }
        }
        private ObservableCollection<ViewStatusy> _selectedtllist;
        public ObservableCollection<ViewStatusy> SelectedTLList
        {
            get
            {
                return _selectedtllist;
            }
            set
            {
                _selectedtllist = value;
                RisePropertyChanged("SelectedTLList");
            }
        }
        List<object> _selectedDeliveries;
        public List<object> SelectedDeliveries
        {
            get { return _selectedDeliveries; }
            set
            {
                _selectedDeliveries = value;
            }
        }
        private ObservableCollection<int> _selected;
       public ObservableCollection<ItemInfo> Items { get; set; }
        private ZamowieniaViewLista _zamwoienieselected;
        public ZamowieniaViewLista ZamwoienieSelected
        {
            get
            {
                return _zamwoienieselected;
            }
            set
            {
                _zamwoienieselected = value;
                   RisePropertyChanged("ZamwoienieSelected");
            }
        }
        public ObservableCollection<ItemInfo> _menuitems;
        public ObservableCollection<ItemInfo> MenuItems
        {
            get
            {
                return _menuitems;
            }
            set
            {
                _menuitems = value;
                RisePropertyChanged("MenuItems");
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
 
        private ObservableCollection<int> selectedItems;
        public ObservableCollection<int> SelectedItems
        {
            get
            {
                return selectedItems;
            }
            set
            {
                selectedItems = value;
                RaisePropertyChanged("SelectedItems");
            }
        }
        public ObservableCollection<ZamowieniaViewStatus> ZamowieniaViewStatusLst
        {
            get;
            set;
        }
        public ObservableCollection<ZamowieniaViewStatusNagl> ZamowieniaViewStatusLstNagl
        {
            get;
            set;
        }
        private List<IHP_KONTRAHENT> _listakontrah;
        #endregion

        #region Encje
        private bool _isActive = true;
        private bool EdycjaZamowienia = false;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }

            set
            {
                _isActive = value;
                RisePropertyChanged("IsActive");
            }
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
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        private bool ispopupkontrah;
        public bool IsPopupKontrah
        {
            get
            {
                return ispopupkontrah;
            }
            set
            {
                ispopupkontrah = value;
                RisePropertyChanged("IsPopupKontrah");
            }
        }

        private bool _isuzgodnione;
        public bool IsUzgodnione
        {
            get
            {
                return _isuzgodnione;
            }
            set
            {
                _isuzgodnione = value;
                RisePropertyChanged("IsUzgodnione");
            }
        }
       private bool _ispopupopenkontrah;
        public bool IsPopupOpenKontrah {
            get
            {
                return _ispopupopenkontrah;
            }

            set
            {
                _ispopupopenkontrah = value;
                RisePropertyChanged("IsPopupOpenKontrah");
            }
      }
        public IHP_PRIORYTET _priorytet { get; set; }
        public  IHP_PRIORYTET  Priorytet
        {
            get
            {
                return _priorytet;
            }
            set
            {
                _priorytet = value;
                RisePropertyChanged("Priorytet");
            }
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
        private IHP_KONTRAHENT _kontrahnet;
        public IHP_KONTRAHENT Kontrahent
        {
            get
            {
                return _kontrahnet;
            }
            set
            {
                _kontrahnet = value;
                if (_kontrahnet != null)
                {
                    SelectedKontrah(); //nowe zam 
                }
                RisePropertyChanged("Nagl");
                RisePropertyChanged("Kontrahent");
            }
        }

        public ViewStatusy SelStatus { get; private set; }


        private WystJednDodatView _jednzastsel;
        public WystJednDodatView JednZastSel
        {
            get
            {
                return _jednzastsel;
            }
            set
            {
                _jednzastsel = value;
                RisePropertyChanged("JednZastSel");
            }
        }

        private IHP_RODZAJDOK _rodzajdoksel;
        public IHP_RODZAJDOK RodzajDokSel
        {
            get
            {
                return _rodzajdoksel;
            }
            set
            {
                _rodzajdoksel = value;
                RisePropertyChanged("RodzajDokSel");

            }
        }
        private IHP_KAROTEKA_EX _kartoteka;
        public IHP_KAROTEKA_EX Kartoteka
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

        private IHP_KARTOTEKA _kartotekaseltmp;

        private IHP_KARTOTEKA _kartotekasel;
        public IHP_KARTOTEKA KartotekaSel
        {
            get
            {
                return _kartotekasel;
            }
            set
            {
                _kartotekasel = value;
                RisePropertyChanged("KartotekaSel");
            }
        }

        private IHP_NAGLDOK _nagl;
        public IHP_NAGLDOK Nagl
        {
            get
            {
                return _nagl;
            }
            set
            {
                _nagl = value;
                RisePropertyChanged("Nagl");
            }
        }

        private IHP_POZDOK _pozj; 
        public IHP_POZDOK PozJ
        {
            get
            {
                return _pozj;
            }
            set
            {
                _pozj = value;
                RisePropertyChanged("PozJ");
            }
        }

        private IHP_CENNIK _cennik;
        public IHP_CENNIK Cennik
        {
            get
            {
                return _cennik;
            }
            set
            {
                _cennik = value;
                if (_cennik != null)
                    _oldCenaNetto = _cennik.CENAN;
                RisePropertyChanged("Cennik");
            }
        }

        private DateTime _dataod;
        public DateTime DataOd
        {
            get
            {
                return _dataod;
            }
            set
            {
                _dataod = value;
                RisePropertyChanged("DataOd");

            }
        }
        private DateTime _datado;
        public DateTime DataDo
        {
            get
            {
                return _datado;
            }
            set
            {
                _datado = value;
                   RisePropertyChanged("DataDo");
            }
        }
        #endregion
      
      public event PropertyChangedEventHandler PropertyChanged;
    }
      public class ColorValueConverter : MarkupExtension, IValueConverter
    {
       #region IValueConverter Members
        public object Convert(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            /* your conditions here i.e Age<=0 */
            if ((Int64)value > 0)
                /*Return red color*/
                return System.Windows.Media.Brushes.Red;
            else
                /*Return the default color*/
                return System.Windows.Media.Brushes.White;
        }

        public object ConvertBack(object value, Type targetType,
        object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
    class MyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<object> updatedList = new List<object>();
            foreach (object obj in (ObservableCollection<int>)value)
            {
                updatedList.Add(obj);
            }
            return updatedList;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<Object> selection = (List<Object>)value;
            ObservableCollection<int> itemsSelection = new ObservableCollection<int>();
            if (selection != null)

                foreach (object item in selection)
            {
                itemsSelection.Add((int)item);
            }
            return itemsSelection;
        }
    }
    public class NaglSend
    {
        public int Numer { get; set; }
        public int IdNagl { get; set; }
    }
    public class StringFormatConverter : MarkupExtension, IValueConverter
    {
        public StringFormatConverter() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Format(culture, (string)parameter, value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NumToStringConverter : MarkupExtension, IValueConverter
    {

        #region IValueConverter Members
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is short)
            {
                var val = (short)value;
                return (val == 0) ? "Nie" : "Tak";
            }
            return null;

        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && value is string)
            {
                return ( value.ToString()  =="Tak") ? 1 : 0;
            }
            return null;
        }

        #endregion
    }
    public class IntToEnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                EditGridCellData CellValue = (EditGridCellData)value;
                if (Int32.Parse(CellValue.Value.ToString()) == 1)
                {
                    return "Altele";
                }
                else if (Int32.Parse(CellValue.Value.ToString()) == 0)
                {
                    return "Extruziuni";
                }
                else
                {
                    return "N/A";
                }
            }
            catch (Exception ex)
            {
                //    string x = ex.Message;
                //    MessageBox.Show("convert err : " + x);
                return "N/A";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}



