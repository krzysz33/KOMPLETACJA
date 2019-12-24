using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
 
using KpInfohelp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ItemInfo
    {
        public int IdDefStatus { get; set; }
        public string Caption { get; set; }
        public ICommand ItemCommand { get; set; }
    }
   public class ViewModelZamowienie : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
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

        private ZamowieniaViewListaNagl _zamowienieview;
        public ZamowieniaViewListaNagl ZamowienieView
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

         


        private ObservableCollection<ZamowieniaViewListaNagl> _zamowieniaviewlistanagllst;
        public ObservableCollection<ZamowieniaViewListaNagl> ZamowieniaViewListaNaglLst
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

        private ObservableCollection<ZamowieniaViewListaExp> _zamowieniaviewlistalstexp;
        public ObservableCollection<ZamowieniaViewListaExp> ZamowieniaViewListaLstExp
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

        private int _statushistId = 0;
        private List<IHP_NAGLDOK> _listnagl;
        private string _wymiarx;
        public string WymiarX
        {
            get
            {
                return _wymiarx;
            }
            set
            {
                _wymiarx = value;
                RisePropertyChanged("WymiarX");
            }
        }
        private string _wymiary;
        public string WymiarY
        {
            get
            {
                return _wymiary;
            }
            set
            {
                _wymiary = value;
                RisePropertyChanged("WymiarY");
            }
        }
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
        public ViewModelZamowienie(bool noload){}
        public ViewModelZamowienie()
        {
            _listnagl = new List<IHP_NAGLDOK>();
            ListaStatus = new ObservableCollection<ViewStatusy>();
         
            ZamowieniaViewListaNaglLst = new ObservableCollection<ZamowieniaViewListaNagl>();
            ZamowieniaViewListaLstExp = new  ObservableCollection<ZamowieniaViewListaExp>();
            ZamowieniaViewStatusLstNagl = new ObservableCollection<ZamowieniaViewStatusNagl>();
            listaExpZazn = new ObservableCollection<ZamowieniaViewListaExp>();
            LstTrasy = new ObservableCollection<IHP_TRASY>();
            SelectedItems = new ObservableCollection<int>() { _listastatus.Count };
            ZamowieniaViewListaLst = new ObservableCollection<ZamowieniaViewLista>();
            ZamowieniaViewStatusLst = new ObservableCollection<ZamowieniaViewStatus>();
            KartotekiMat = new ObservableCollection<IHP_KARTOTEKA>(context.IHP_KARTOTEKA);
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>();
            MenuItems = new ObservableCollection<ItemInfo>();
            _zamowieniaviewlista = new ObservableCollection<ZamowieniaViewLista>();
            _zamowieniaviewlistafilter = new ObservableCollection<ZamowieniaViewLista>();
            Poz = new ObservableCollection<IHP_POZDOK>();
      
 
            CommandPokazOkno = new DelegateCommand(pokazokno);
            DayCommand = new DelegateCommand(UstawDzien);
            WeekCommand = new DelegateCommand(UstawTydzien);
            MonthCommand = new DelegateCommand(UstawMiesiac);
            YearCommand = new DelegateCommand(UstawRok);
            ShowDokCommand = new DelegateCommand(ShowDok);
            DeleteCommand = new DelegateCommand(DeleteZam);
            EditCommand = new DelegateCommand(EditNagl);
            PanelExportCommand = new DelegateCommand(PanelExportPokaz);
            PrintCommand = new DelegateCommand(DrukujZaznaczone);
            ExportCommand = new DelegateCommand(ExportRun, CanExportRun);
 
            CutCommand = new DelegateCommand(UstawStatusWyciete);
            FiltrCommand = new DelegateCommand(UstawFiltr);
            DataOd = DateTime.Today;
            DataDo = DateTime.Today;
            LoadCollectionKh();
            wybierzdzien = true;
            zaznodzn = false;
            LoadCollectionNagl();
         
             Items = new ObservableCollection<ItemInfo>();
            _selected = new ObservableCollection<int>();
            PanelExport = true;
            Messenger.Default.Register<int>(this, OnShowHidePanel);
            LoadColectionTrasy();


        }
        private void LoadColectionTrasy()
        {
            if (LstTrasy == null) return;
            LstTrasy.Clear();
            List<IHP_TRASY> _listatrasy = new List<IHP_TRASY>();
            _listatrasy = context.IHP_TRASY.ToList();

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
          private void GenXmlFile(ZamowieniaViewListaExp item)
         {
            //XMLFileOperation gen = XMLFileOperation.GetInstance;
            //List<OptymalizacjaTafla> _tafla = new List<OptymalizacjaTafla>();
            //OptymalizacjaTafla optafla = new OptymalizacjaTafla();
 
            //gen.UstawieniaOptymalizacji.Numer = 1;
            //optafla.Operacje = "S+C";
            //OptymalizacjaTaflaSzklo _szklo = new OptymalizacjaTaflaSzklo();
            //_szklo.Nazwa = "NE4";
            //_szklo.G = 4;
            //_szklo.X = 6000;
            //_szklo.Y = 6000;
            //_szklo.M_XL = 1;
            //_szklo.M_XP = 2;
            //_szklo.M_YD = 3;
            //_szklo.M_YG = 4;
            // optafla.Szklo = _szklo;
            //_tafla.Add(optafla);
            //gen.UstawieniaOptymalizacji.Tafla = _tafla;
            //gen.Zapisz();
        }
        private void GenXmlFile1()
        {
            //XMLFileOperation gen = XMLFileOperation.GetInstance;
            //List<OptymalizacjaTafla> _tafla = new List<OptymalizacjaTafla>();
            //OptymalizacjaTafla optafla = new OptymalizacjaTafla();
            //Optymalizacja opt = new Optymalizacja();
            //opt.Numer = 1;
            //gen.UstawieniaOptymalizacji = opt;
            //optafla.Operacje = "S+C";
            //OptymalizacjaTaflaSzklo _szklo = new OptymalizacjaTaflaSzklo();
            //_szklo.Nazwa = "NE4";
            //_szklo.G = 4;
            //_szklo.X = 6000;
            //_szklo.Y = 6000;
            //_szklo.M_XL = 1;
            //_szklo.M_XP = 2;
            //_szklo.M_YD = 3;
            //_szklo.M_YG = 4;
            //optafla.Szklo = _szklo;
            //List<OptymalizacjaTaflaSzyba> _szyby = new List<OptymalizacjaTaflaSzyba>();
            //OptymalizacjaTaflaSzyba _szyba = new OptymalizacjaTaflaSzyba();
            //_szyba.Oznaczenie = "AKPOL-PLAST #86/5";
            //_szyba.Zlecenie = 270;
            //_szyba.Pozycja = 1;
            //_szyba.Element = 2;
            //_szyba.X0 = 15;
            //_szyba.Y0 = 15;
            //_szyba.X = 1922;
            //_szyba.Y = 829;
            //_szyby.Add(_szyba);
            //optafla.Szyby = _szyby;
            //_tafla.Add(optafla);
            //gen.UstawieniaOptymalizacji.Tafla = _tafla;
            //gen.Zapisz();
        }
        public void ExportRun()
        {
 
            //List < ZamowieniaViewListaExp > listaExp  = ZamowieniaViewListaLstExp.Where(x => x.Zaznaczenie == true).ToList();
            //XMLFileOperation gen = XMLFileOperation.GetInstance;
            //List<OptymalizacjaTafla> _tafla = new List<OptymalizacjaTafla>();
            //OptymalizacjaTafla optafla = new OptymalizacjaTafla();
            //Optymalizacja opt = new Optymalizacja();
            //opt.Numer = 1;
            //gen.UstawieniaOptymalizacji = opt;
            //optafla.Operacje = "S+C";

            //if (listaExp.Count > 0)
            //{
            //    foreach (ZamowieniaViewListaExp item in listaExp)
            //    {
             
            //        OptymalizacjaTaflaSzklo _szklo = new OptymalizacjaTaflaSzklo();
            //        _szklo.Nazwa = item.INDEKS;
            //        _szklo.G = 4;
            //        _szklo.X = 6000;
            //        _szklo.Y = 6000;
            //        _szklo.M_XL = 2;
            //        _szklo.M_XP = 2;
            //        _szklo.M_YD =4;
            //        _szklo.M_YG = 4;
            //        optafla.Szklo = _szklo;
            //        List<OptymalizacjaTaflaSzyba> _szyby = new List<OptymalizacjaTaflaSzyba>();
            //        OptymalizacjaTaflaSzyba _szyba = new OptymalizacjaTaflaSzyba();
            //        _szyba.Oznaczenie = item.INDEKS;
            //        _szyba.Zlecenie = 270;
            //        _szyba.Pozycja = 1;
            //        _szyba.Element = 2;
            //        _szyba.X0 = 15;
            //        _szyba.Y0 = 15;
            //        _szyba.X = item.WYMIARX;
            //        _szyba.Y = item.WYMIARY;
            //        _szyby.Add(_szyba);
            //        optafla.Szyby = _szyby;
            //        _tafla.Add(optafla);
            //        gen.UstawieniaOptymalizacji.Tafla = _tafla;
            //        gen.Zapisz();

            //    }
            //}

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
           if (wybierzdzien)
                UstawDzien();
            if (wybierztydzien)
                UstawTydzien();
            if (wybierzmiesiac)
                UstawMiesiac();
            if (wybierzrok)
                UstawRok();
        }
        private bool CheckObr(int idPoz)
        {
            return true;
        }
        public void LoadCollectionHistNagl(int IdNagl)
        {
            
        var zamowienia = context.Database.SqlQuery<ZamowieniaViewStatusNagl>(string.Format(@"select K.INDEKS, SH.ID_STATUSHISTORIA, SH.ID_DEFSTATUS, SH.DATAWPISU, DS.NAZWA, SH.OPIS, SH.ID_POZ, AZ.ID_ARIT_ZAM_USERS,
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
                if (_datado != null)
                    CanRemoveDataDoFilter = true;
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
            _listakontrah = context.IHP_KONTRAHENT.ToList();

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
                    IdNagl = ZamowienieView.ID_NAGL??0
                };
                Messenger.Default.Send<NaglSend>(ns);
            }
    

        }
        private void UstawDzien()
        {
            //DateTime currentDateTime = DateTime.Now;
            //DateOd = currentDateTime.Date;
            //DateDo = currentDateTime.Date.AddDays(1).AddTicks(-1);
            //var zamowienia = context.Database.SqlQuery<ZamowieniaViewListaNagl>(string.Format(@"select N.ID_NAGL,K.ID_KONTRAH,N.DATADOK,N.TERMINREALIZ,N.NRDOKWEW,K.IMIE,K.NAZWISKO ,N.UWAGI from NAGL
            //                                                                      N join KONTRAH K on K.id_kontrah = N.id_kontrah where N.DATADOK BETWEEN '{0}' and '{1}' order by TERMINREALIZ", DateOd.ToString("yyyy-MM-dd"), DateDo.ToString("yyyy-MM-dd"))).ToList();
            //ZamowieniaViewListaNaglLst.Clear();
            //foreach (ZamowieniaViewListaNagl item in zamowienia)
            //{
            //    ZamowieniaViewListaNaglLst.Add(item);
            //}
            //KasujKolory();
            //wybierzdzien = true;
            ////KolorDzien  = Color.FromRgb(135, 206, 250);
            //KolorDzien = (Color)ColorConverter.ConvertFromString("#FF0CADBF3");
        }
        private void UstawTydzien()
        {
            DayOfWeek fdow = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            int offset = fdow - DateTime.Today.DayOfWeek - 1;
            DateOd = DateTime.Today.AddDays(offset);
            DateDo = DateOd.AddDays(7);
            var zamowienia = context.Database.SqlQuery<ZamowieniaViewListaNagl>(string.Format(@"select N.ID_NAGL,K.ID_KONTRAH,N.DATADOK,N.TERMINREALIZ,N.NRDOKWEW,K.IMIE,K.NAZWISKO ,N.UWAGI from NAGL
                                                                                  N join KONTRAH K on K.id_kontrah = N.id_kontrah where N.DATADOK BETWEEN '{0}' and '{1}' order by TERMINREALIZ", DateOd.ToString("yyyy-MM-dd"), DateDo.ToString("yyyy-MM-dd"))).ToList();
            ZamowieniaViewListaNaglLst.Clear();

            foreach (ZamowieniaViewListaNagl item in zamowienia)
            {
                ZamowieniaViewListaNaglLst.Add(item);
            }
            KasujKolory();
            wybierztydzien = true;
            //KolorTydzien  = Color.FromRgb(135, 206, 250);
            KolorTydzien = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawMiesiac()
        {
            DateOd = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateDo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
 

            var zamowienia = context.Database.SqlQuery<ZamowieniaViewListaNagl>(string.Format(@"select N.ID_NAGL,K.ID_KONTRAH,N.DATADOK,N.TERMINREALIZ,N.NRDOKWEW,K.IMIE,K.NAZWISKO ,N.UWAGI from NAGL
                                                                                  N join KONTRAH K on K.id_kontrah = N.id_kontrah where N.DATADOK BETWEEN '{0}' and '{1}' order by TERMINREALIZ", DateOd.ToString("yyyy-MM-dd"), DateDo.ToString("yyyy-MM-dd"))).ToList();

            ZamowieniaViewListaNaglLst.Clear();

            foreach (ZamowieniaViewListaNagl item in zamowienia)
            {
                ZamowieniaViewListaNaglLst.Add(item);
            }
            KasujKolory();
            wybierzmiesiac = true;
            //KolorMiesiac  = Color.FromRgb(135, 206, 250);
            KolorMiesiac = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void UstawRok()
        {
            DateOd = new DateTime(DateTime.Today.Year, 1, 1);
            DateDo = new DateTime(DateTime.Today.Year, 12, 31);
            var zamowienia = context.Database.SqlQuery<ZamowieniaViewListaNagl>(string.Format(@"select N.ID_NAGL,K.ID_KONTRAH,N.DATADOK,N.TERMINREALIZ,N.NRDOKWEW,K.IMIE,K.NAZWISKO ,N.UWAGI from NAGL
                                                                                  N join KONTRAH K on K.id_kontrah = N.id_kontrah where N.DATADOK BETWEEN '{0}' and '{1}' order by TERMINREALIZ", DateOd.ToString("yyyy-MM-dd"), DateDo.ToString("yyyy-MM-dd"))).ToList();
            ZamowieniaViewListaNaglLst.Clear();
            foreach (ZamowieniaViewListaNagl item in zamowienia)
            {
                ZamowieniaViewListaNaglLst.Add(item);
            }
            KasujKolory();
            //KolorRok  = Color.FromRgb(135, 206, 250);
            wybierzrok = true;
            KolorRok = (Color)ColorConverter.ConvertFromString("#FFCADBF3");
        }
        private void KasujKolory()
        {
            KolorDzien = KolorTydzien = KolorMiesiac = KolorRok = Color.FromRgb(211, 211, 211);
            wybierzdzien = wybierztydzien = wybierzmiesiac = wybierzrok = false;
        }
    }
  
   
}


