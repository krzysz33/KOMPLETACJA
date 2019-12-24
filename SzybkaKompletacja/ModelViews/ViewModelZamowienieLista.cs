using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;

using KpInfohelp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
//using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace KpInfohelp
{
    public interface IZamowienieLista: IGenericRepository<IHP_NAGLDOK>
    {
   
    }
   public class ViewModelZamowienieLista : ZamowieniaListaRepository, INotifyPropertyChanged, IMVVMDockingProperties, IZamowienieLista
    {
        public WagaRamka _ramka;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isActive = true;
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
        private bool _iswyciete;
        public bool IsWyciete
        {
            get
            {
                return _iswyciete;
            }
            set
            {
                _iswyciete = value;
                if(_iswyciete)
                {
                    IsWpisane = false;
                }
                RisePropertyChanged("IsWyciete");
            }
        }
        private bool _iswpisane;

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

        public bool IsWpisane
        {
            get
            {
                return _iswpisane;
            }
            set
            {
                _iswpisane = value;
                if (_iswpisane)
                    IsWyciete = false;
                RisePropertyChanged("IsWpisane");
            }
                
        }
        private IHP_TRASY _trasa;
        public IHP_TRASY Trasa
        {
            get
            {
                return _trasa;
            }
            set
            {
                _trasa = value;
                RisePropertyChanged("Trasa");
            }
        }
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        private bool wybierzdzien, wybierztydzien, wybierzmiesiac, wybierzrok;
        private ObservableCollection<IHP_TRASY> _lsttrasy;
        public ObservableCollection<IHP_TRASY> LstTrasy
        {
            get
            {
                return _lsttrasy;
            }
            set
            {
                _lsttrasy = value;
                RisePropertyChanged("LstTrasy");

            }

        }
        private IHP_NAGLDOK _zamowienieview;
        public IHP_NAGLDOK ZamowienieView
        {
            get
            {
                return _zamowienieview;
            }
            set
            {
                _zamowienieview = value;

                RisePropertyChanged("ZamowienieView");
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
        private   ObservableCollection<ZamowieniaViewListaExp> _listaexpzaz;
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
        public ObservableCollection<ZamowieniaViewLista> ZamowieniaViewListaLst { get; set; }
        private ObservableCollection<IHP_NAGLDOK> _zamowieniaviewlistanagllst;
        public ObservableCollection<IHP_NAGLDOK> ZamowieniaViewListaNaglLst
        {
            get
            {
                return _zamowieniaviewlistanagllst;
            }
            set
            {
                _zamowieniaviewlistanagllst = value;
                RisePropertyChanged("ZamowieniaViewListaNaglLst");
            }
        }
        private ObservableCollection<IHP_POZDOK> _zamowieniaviewlistalstexp;
        public ObservableCollection<IHP_POZDOK> ZamowieniaViewListaLstExp
        {
            get
            {
                return _zamowieniaviewlistalstexp;
            }
            set
            {
                _zamowieniaviewlistalstexp = value;
                RisePropertyChanged("ZamowieniaViewListaLstExp");
            }
        }

        private IHP_POZDOK _selpoz;
         public IHP_POZDOK SelPoz
        {
            get
            {
                return _selpoz;
            }
            set
            {
                _selpoz = value;
                OnRisePropertyChanged("SelPoz");
            }

        }

        private int _statushistId = 0;
        private List<IHP_NAGLDOK> _listnagl;
        public ViewStatusy SelStatus { get; private set; }
        #region Przyciski
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
        public ICommand EditCommand { get; set; }
        public ICommand PanelExportCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        public ICommand ZaznaczWszystkoCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand FiltrCommand { get; set; }
        public ICommand CutCommand { get; set; }
        public ICommand SetFocusCommand { get; set; }
        public ICommand EditPozCommand { get; set; }
        public ICommand WagaCommand { get; set; }

        #endregion
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
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        void pokazokno()
        {
            //if (_zamwoienieselected != null)
            //{
            //    var w = new PictureOpen(_zamwoienieselected.NAZWAPLIKU);
            //    w.ShowDialog();
            //}
        }
        public ViewModelZamowienieLista(bool noload){}
        public ViewModelZamowienieLista()
        {
            DateDo = DateTime.Today;
            wybierzdzien = true;
            zaznodzn = false;
            PanelExport = true;

            initcommands();
            intimessages();
            initlist();
            seed();

            Messenger.Default.Register<int>(this, OnShowHidePanel);
     
         }
        #region Metody
        void initcommands()
        {
            CommandPokazOkno = new DelegateCommand(pokazokno);
            ShowDokCommand = new DelegateCommand(ShowDok);
            DeleteCommand = new DelegateCommand(DeleteZam);
            EditCommand = new DelegateCommand(EditNagl);
            PanelExportCommand = new DelegateCommand(PanelExportPokaz);
            PrintCommand = new DelegateCommand(DrukujZaznaczone);
            SetFocusCommand = new DelegateCommand<int>(SetFocus);
            CutCommand = new DelegateCommand(UstawStatusWyciete);
            FiltrCommand = new DelegateCommand(UstawFiltr);
            EditPozCommand =  new DelegateCommand(UstawPozCommand);
            WagaCommand = new DelegateCommand(warzenie);


        }
        void intimessages()
        {
            Messenger.Default.Register<WagaRamka>(this, OnMessagewagaRamka);
        }
        void initlist()
        {
            _listnagl = new List<IHP_NAGLDOK>();
            ListaStatus = new ObservableCollection<ViewStatusy>();
            ZamowieniaViewListaNaglLst = new ObservableCollection<IHP_NAGLDOK>();
            ZamowieniaViewStatusLstNagl = new ObservableCollection<ZamowieniaViewStatusNagl>();
            listaExpZazn = new ObservableCollection<ZamowieniaViewListaExp>();
            LstTrasy = new ObservableCollection<IHP_TRASY>();
            SelectedItems = new ObservableCollection<int>() { _listastatus.Count };
            ZamowieniaViewListaLst = new ObservableCollection<ZamowieniaViewLista>();
            ZamowieniaViewStatusLst = new ObservableCollection<ZamowieniaViewStatus>();
            KartotekiMat = new ObservableCollection<IHP_KARTOTEKA>(Context.IHP_KARTOTEKA);
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>();
            MenuItems = new ObservableCollection<ItemInfo>();
            _zamowieniaviewlista = new ObservableCollection<ZamowieniaViewLista>();
            _zamowieniaviewlistafilter = new ObservableCollection<ZamowieniaViewLista>();
            Poz = new ObservableCollection<IHP_POZDOK>();
            Poz.CollectionChanged += Poz_CollectionChanged;
            Items = new ObservableCollection<ItemInfo>();
            _selected = new ObservableCollection<int>();
        }
        void seed()
        {
            LoadCollectionKh();
            LoadCollectionNagl();
            LoadColectionTrasy();
        }
        public void OnMessagewagaRamka(WagaRamka ramka)
        {
            _ramka = ramka;
            WagaKgLocal = _ramka.Waga.ToString() + " kg";
        }
        void UstawPozCommand()
        {
            if (ZamowienieView == null) return;
            Poz.Clear();
            foreach (IHP_POZDOK item in GetPozByNagl(ZamowienieView.ID_IHP_NAGLDOK))
                Poz.Add(item);
        }
        void LoadZamWgTrasy()
        {
            ZamowieniaViewListaNaglLst.Clear();
            if (Trasa !=null)
            {
                foreach (IHP_NAGLDOK item in GetZamByIdTrasa(Trasa.ID_IHP_TRASY, _datedo))
                    ZamowieniaViewListaNaglLst.Add(item);
            }
        }
        #endregion
        private void LoadColectionTrasy()
        {
            if (LstTrasy == null) return;
            LstTrasy.Clear();
            List<IHP_TRASY> _listatrasy = new List<IHP_TRASY>();
            _listatrasy = Context.IHP_TRASY.ToList();

            foreach (IHP_TRASY item2 in _listatrasy)
            {
                LstTrasy.Add(item2);
            }
        }
        private void UstawFiltr()
        {
            if (PokazFiltr)
                PokazFiltr =false;
            else if (!PokazFiltr)
                PokazFiltr = true;
        }
        private void UstawStatusWyciete()
        {
          
            //RejestrRepository rRepo = new RejestrRepository();
            //try
            //{
            //    foreach (ZamowieniaViewListaExp item in ZamowieniaViewListaLstExp.Where(x => x.Zaznaczenie == true).ToList())
            //    {
            //        rRepo.IdNagl = item.ID_NAGL ?? 0;
            //        rRepo.SaveStatus(item.ID_POZ, ProgramDataSotrage.xmlSqlConfig.IdentStatusWyciete);
            //    }
            //    MessageBox.Show("Ustawiono Statusy Wyciete !");
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
      }
        private void DrukujZaznaczone()
        {
            //listaExpZazn.Clear();
            //  //listaExpZazn =  ZamowieniaViewListaLstExp.Where(x => x.Zaznaczenie == true).ToList());
            ////listaExpZazn = new ObservableCollection<ZamowieniaViewListaExp>();
            //foreach (ZamowieniaViewListaExp item in ZamowieniaViewListaLstExp.Where(x => x.Zaznaczenie == true).ToList())
            //{
            //    if(item.PROCKSZTALTU>0)
            //    {
            //        MessageBox.Show(string.Format("dla zlecenia: {0} sprawdz rysunek !!", item.NRDOKWEW));
            //    }

            //    listaExpZazn.Add(item);
            //}
                
            //RisePropertyChanged("listaExpZazn");
            //if (listaExpZazn.Count == 0)
            //    return;
            ////  rptKwitWagowy report = new rptKwitWagowy();
            //rptLabel report = new rptLabel();
            //foreach (DevExpress.XtraReports.Parameters.Parameter p in report.Parameters)
            //{
            //    p.Visible = false;
            //}
            //report.DataSource = listaExpZazn;
            //          //     report.Parameters["WazenieId"].Value = 1;

            //          var window = new frmKiwtWagowy();
            //window.PreviewControl.DocumentSource = report;
            //report.CreateDocument();
            //window.Show();
        }
        private bool CanExportRun()
        {
            return !PanelExport;
        }
        public void PanelExportPokaz()
        {

            if(PanelExport)
            {
                PanelExport = false;
                PanelZamowienia = true;
            }
            else if(!PanelExport)
            {
                PanelExport = true;
                PanelZamowienia = false;
            }
        }
        private bool _panelexport;
        public bool PanelExport {
            get
            {
                return _panelexport;
            }
            set
            {
                _panelexport = value;
                RisePropertyChanged("PanelExport");
            }
                
        }
        private bool _panelzamowienia;
        public bool PanelZamowienia
        {
            get
            {
                return _panelzamowienia;
            }
            set
            {
                _panelzamowienia = value;
                RisePropertyChanged("PanelZamowienia");
            }

        }
        public void OnMessageUpdate(List<IHP_GRUPAKART> Name)
        {
            //
        }
        
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
                if (_datedo != null && Trasa != null)
                                       SetFocus(1);
                RisePropertyChanged("DateDo");
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
        private ObservableCollection<ZamowieniaViewLista> _zamowieniaviewlistalst;
        private ObservableCollection<ZamowieniaViewLista> _zamowieniaviewlista;
        private ObservableCollection<ZamowieniaViewLista> _zamowieniaviewlistafilter;
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
        private ObservableCollection<IHP_KARTOTEKA> _kartoteki;
        public ObservableCollection<IHP_KARTOTEKA> KartotekiMat
        {
            get
            {
                return _kartoteki;
            }
            set
            {
                _kartoteki = value;
                RisePropertyChanged("KartotekiMat");
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
       
        #region Metody
        private void DeleteZam()
        {
            string LastMessage;
            try
            {
                //if ZamowienieView != null)
                //{
                //    MessageBoxResult result = MessageBox.Show("Czy Napewno Usunąć Dokument: " + ZamowienieView.NRDOKWEW + " ??", "Potwierdź", MessageBoxButton.YesNo);
                //    if (result == MessageBoxResult.Yes)
                //    {
                //        IHP_NAGLDOK nagltodelete = context.IHP_NAGLDOK.FirstOrDefault(x => x.ID_IHP_NAGLDOK == ZamowienieView.ID_NAGL);
                //        context.Entry(nagltodelete).State = EntityState.Deleted;
                //        context.IHP_NAGLDOK.Remove(nagltodelete);
                //        context.SaveChanges();

                //        NaglSend ns = new NaglSend()
                //        {
                //            Numer = 143,
                //            IdNagl = ZamowienieView.ID_NAGL ?? 0
                //        };

                //        Messenger.Default.Send<NaglSend>(ns);
                      
                //        LoadCollectionNagl();
                //    }
                //}
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
   
        public void LoadCollectionNagl()
        {
 
        }
    
        private bool CheckObr(int idPoz)
        {
            return true;
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
         public void ShowDok()
        {
            //if (ZamowienieView != null)
            //{

            //    //var window = new PozDok();
            //    //Messenger.Default.Send<int>(ZamowienieView.ID_NAGL ?? 0);
            //    //window.Show();
            //    int _idnagl = ZamowienieView.ID_NAGL ?? 0;

            //    var window = new KafelkiOkno();
            //    NaglSend ns = new NaglSend() { Numer = 146, IdNagl = _idnagl };
            //    Messenger.Default.Send<NaglSend>(ns);
            //    window.Show();
            //}
        }
        private void SetFocus(int parameter)
        {

            switch (parameter)
            {
                case 1:
                    LoadZamWgTrasy();
                    break;
            }
        }

        #endregion

        #region Encje
        private IHP_KARTOTEKA _kartotekamat;
        public IHP_KARTOTEKA KartotekaMat
        {
            get
            {
                return _kartotekamat;
            }
            set
            {
                _kartotekamat = value;
                if (_kartotekamat != null)
                {
                    CanRemoveMaterialFilter = true;

                }
                RisePropertyChanged("KartotekaMat");
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
 
        #region Filtry
        private bool _canremovematerialfilter;
        public bool CanRemoveMaterialFilter
        {
            get { return _canremovematerialfilter; }
            set
            {
                _canremovematerialfilter = value;
                RisePropertyChanged("CanRemoveMaterialFilter");
            }
        }

        private bool _canremovestatusfilter;
        public bool CanRemoveStatusFilter
        {
            get { return _canremovestatusfilter; }
            set
            {
                _canremovestatusfilter = value;
                RisePropertyChanged("CanRemoveStatusFilter");
            }
        }

        private bool _canremovedataodfilter;
        public bool CanRemoveDataOdFilter
        {
            get { return _canremovedataodfilter; }
            set
            {
                _canremovedataodfilter = value;
                RaisePropertyChanged("CanRemoveDataOdFilter");
            }
        }
        private bool _canremovedatadofilter;
        public bool CanRemoveDataDoFilter
        {
            get { return _canremovedatadofilter; }
            set
            {
                _canremovedatadofilter = value;
                RaisePropertyChanged("CanRemoveDataDoFilter");
            }
        }
        public void ResetFilters()
        {
            _zamowieniaviewlistafilter.Clear();
            foreach (ZamowieniaViewLista item in _zamowieniaviewlista)
                _zamowieniaviewlistafilter.Add(item);
        }
        public void Filtr()
        {
            if (CanRemoveMaterialFilter)
                AddMaterialFilter();
            AddStatusFilter();
            if (CanRemoveDataDoFilter)
                AddDatyFilter();
        }

        public void RemoveMaterialFilter()
        {
            _kartotekamat = null;
            CanRemoveMaterialFilter = false;
        }
        private void FilterByMaterial(object sender, FilterEventArgs e)
        {
            // see Notes on Filter Methods:
            var src = e.Item as ZamowieniaViewLista;
            if (src == null)
                e.Accepted = false;
            //   else if (_kartotekamat.ID_KARTOTEKA != src.ID_KARTOTEKA??0)
            //     e.Accepted = false;
        }
        /*
                public void RemoveDataOdFilter()
                {
                    CVS.Filter -= new FilterEventHandler(FilterByAuthor);
                    //      SelectedAuthor = null;
                    //      CanRemoveAuthorFilter = false;
                }
                public void RemoveDataDoFilter()
                {
                    CVS.Filter -= new FilterEventHandler(FilterByYear);
                    SelectedYear = null;
                    CanRemoveYearFilter = false;
                }



                private void FilterByYear(object sender, FilterEventArgs e)
                {
                    // see Notes on Filter Methods:
                    var src = e.Item as Thing;
                    if (src == null)
                        e.Accepted = false;
                    else if (SelectedYear != src.Year)
                        e.Accepted = false;
                }
                private void FilterByCountry(object sender, FilterEventArgs e)
                {
                    // see Notes on Filter Methods:
                    var src = e.Item as Thing;
                    if (src == null)
                        e.Accepted = false;
                    else if (string.Compare(SelectedCountry, src.Country) != 0)
                        e.Accepted = false;
                }
                */
        public void AddMaterialFilter()
        {
            // see Notes on Adding Filters:
            if (CanRemoveMaterialFilter)
            {
                ZamowieniaViewListaLst.Clear();
                _zamowieniaviewlistafilter.Clear();

                foreach (ZamowieniaViewLista item in _zamowieniaviewlista.Where(x => x.ID_KARTOTEKA == _kartotekamat.ID_IHP_KARTOTEKA))
                    _zamowieniaviewlistafilter.Add(item);

                foreach (ZamowieniaViewLista item in _zamowieniaviewlistafilter)
                    ZamowieniaViewListaLst.Add(item);
            }
            else
            {
                CanRemoveMaterialFilter = true;
            }
        }
        public void AddStatusFilter()
        {
            if (SelectedDeliveries == null) return;
            if (SelectedDeliveries.Count == 0) return;
            _selected.Clear();
            foreach (ViewStatusy i in SelectedDeliveries)
            {
                _selected.Add(i.ID_DEFSTATUS);
            }
            // see Notes on Adding Filters:
            if (CanRemoveMaterialFilter)
            {
                ZamowieniaViewListaLst.Clear();
             //   foreach (ZamowieniaViewLista item in _zamowieniaviewlistafilter.Where(x => selectedItems.Contains(x.ID_DEFSTATUS)))
           
              foreach (ZamowieniaViewLista item in _zamowieniaviewlistafilter.Where(x => _selected.Contains(x.ID_DEFSTATUS)))
                {
                    ZamowieniaViewListaLst.Add(item);
                }
            }
            else
            {
                ZamowieniaViewListaLst.Clear();
              foreach (ZamowieniaViewLista item in _zamowieniaviewlista.Where(x => _selected.Contains(x.ID_DEFSTATUS)))
       //        foreach (ZamowieniaViewLista item in _zamowieniaviewlista.Where(x => selectedItems.Contains(x.ID_DEFSTATUS)))
                {
                    _zamowieniaviewlistafilter.Add(item);
                    ZamowieniaViewListaLst.Add(item);
                }

            }
            CanRemoveStatusFilter = true;
        }
        public void AddDatyFilter()
        {
            if (SelectedDeliveries == null) return;
            if (SelectedDeliveries.Count == 0) return;
            _selected.Clear();
            foreach (ViewStatusy i in SelectedDeliveries)
            {
                _selected.Add(i.ID_DEFSTATUS);
            }
            // see Notes on Adding Filters:
            if ((CanRemoveMaterialFilter)   || (CanRemoveStatusFilter))
            {
                ZamowieniaViewListaLst.Clear();
                foreach (ZamowieniaViewLista item in _zamowieniaviewlistafilter.Where(x => _selected.Contains(x.ID_DEFSTATUS) && x.DATADOK >= DataOd && x.DATADOK <= DataDo))
                {
                     if(!ZamowieniaViewListaLst.Any(x=>x.ID_POZ ==item.ID_POZ))
                                               ZamowieniaViewListaLst.Add(item);
                }
            }
            else
            {
                ZamowieniaViewListaLst.Clear();
                foreach (ZamowieniaViewLista item in _zamowieniaviewlistafilter.Where(x => x.DATADOK >= DataOd && x.DATADOK <= DataDo))
                {
                    _zamowieniaviewlistafilter.Add(item);
                    ZamowieniaViewListaLst.Add(item);
                }

            }
        }
        #endregion
        private void OnShowHidePanel(int nrpanel)
        {
            if (nrpanel == 146)
                LoadCollectionNagl();
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
        private void EditNagl()
        {
            if (ZamowienieView != null)
            {
               NaglSend ns = new NaglSend()
                {
                    Numer = 144,
                    IdNagl = ZamowienieView.ID_IHP_NAGLDOK
                };
                Messenger.Default.Send<NaglSend>(ns);
            }
    

        }
        
        private void warzenie()
        {
            SelPoz.ILOSCZW = _ramka.Waga;
            //OnRisePropertyChanged("SelPoz");
            //OnRisePropertyChanged("Poz");
            Update2(SelPoz);
        }
        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
          //  if(SelPoz!=null)
                 //         _poz.FirstOrDefault(x => x.ID_IHP_POZDOK == SelPoz.ID_IHP_POZDOK).ILOSCZW = _ramka.Waga;
        }

        private void Poz_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (IHP_POZDOK item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }



        }

    }
  
   
}


