using DevExpress.Mvvm;

using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.CompilerServices;

using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.POCO;
using System.IO;
using DevExpress.Mvvm.DataAnnotations;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Globalization;
using System.Collections;
using System.Windows.Threading;
using System.Windows;
using System.Threading;
using DevExpress.Xpf.Docking;
 
 

namespace KpInfohelp
{
    //
    public class KafelekAkt
    {
        public int NrPanel { get; set; }
        public int IdNagl { get; set; }
        public int Status { get; set; }
    }
    public class KafelekFiltr
    {
        public int TypFiltra { get; set; }
        public int IdKontrah { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public bool TylkoOd { get; set; }
    }
    public class ItemKafelek :   INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {

                _isSelected = value;
                RisePropertyChanged("IsSelected");

            }
        }
        private int _idnagl;
        public int IdNagl {
            get
            {
                return _idnagl;
            }
               set
            {
                _idnagl = value;
                RisePropertyChanged("IdNagl");
            }
                
                }
        public int IdKontrah { get; set; }
        private int _statuszam;
        public int StatusZam
        {
            get
            {
                return _statuszam;
            }
            set
            {
                _statuszam = value;
                if (_statuszam == 1)
                        IsSelected = true;
                RisePropertyChanged("StatusZam");
            }
        }
        public string NrZam { get; set; }
        public string Termin { get; set; }
        public string Caption { get; set; }
        public string Caption2 { get; set; }
        public string Kontrah { get; set; }
        private bool _isflowbreak;
        public bool IsFlowBreak {
            get
            {
                return _isflowbreak;
            }
           set
            {
                _isflowbreak = value;
                RisePropertyChanged("IsFlowBreak");

            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void RisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
    public class ViewModelKafelki : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
       
     
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private int secondsLeft;
        private string _secondleft;
        public string SecondsLeft {
            get
            {
                return _secondleft;
            }
            set
            {
                _secondleft = value;
                RisePropertyChanged("SecondsLeft");
            }

        }
        protected ISplashScreenService SplashScreenService { get { return this.GetService<ISplashScreenService>(); } }
        public string Header { get; set; }
        public string DetailView { get; set; }
        public object ParentViewModel { get; set; }
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
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
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
        public ICommand ClickTileCommand { get; private set; }
        public ICommand FilterForPortfolioCmd { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ShowFiltrCommand { get; set; }
        public ICommand ShowOknoCommand { get; set; }


        string Kodkreskowy = string.Empty;

        KafelekFiltr kafelekfiltr;

        ///private DelegateCommand<object> _filterForPortfolioCmd;
        List<IHP_NAGLDOK> Nagls;

        //public DelegateCommand<object> FilterForPortfolioCmd
        //{
        //    get { return _filterForPortfolioCmd; }
        //}
        public ItemKafelek Kafelek { get; set; }
        private BackgroundWorker bgWorker;
        private BackgroundWorker bgWorkerFiltr;
        private DispatcherTimer timer;


        // https://blogs.msdn.microsoft.com/luc/2009/01/14/sorts-and-filters-on-observablecollection/

        ListCollectionView _cvItems;
        public CollectionView CVItems { get { return _cvItems; } }

        private ObservableCollection<ItemKafelek> _items;
        public ObservableCollection<ItemKafelek> Items {
            get
            {
                return _items;
            }
            set

            {
                _items = value;
                RisePropertyChanged("Items");


            }
        }
        private ObservableCollection<ZamowieniaViewLista> _zamowieniaviewlistalst;
        public ObservableCollection<ZamowieniaViewLista> ZamowieniaViewListaLst
        {
            get
            {
                return _zamowieniaviewlistalst;
            }
            set
            {
                _zamowieniaviewlistalst = value;
                RisePropertyChanged("ZamowieniaViewListaLst");
            }
        }

        private void OnMessageTrasy(IHP_TRASY item)
        {
            //if (item != null)
            //{
            //    IHP_TRASY wk1 = new IHP_TRASY();
         
       
            //    wk1. = GetNextNumer(19);
            //    wk1.ID_CECHAKART = item.ID_CECHAKART;
            //    wk1.ID_KARTOTEKA = _kartoteka.ID_KARTOTEKA;
            //    wk1.WARTOSC = string.Empty;
            //    context.WYSTCECHKART.Add(wk1);
            //    context.Entry(wk1).State = EntityState.Added;
            //    context.NUMERACJA.Add(numernagl);
            //    context.Entry(numernagl).State = EntityState.Modified;
            //    context.SaveChanges();
            //}

            //LoadColectionTrasy();
        }

        private void LoadZamowienia()
        {
            //     List<int> idList = new List<int> { 0, 1 };
            ////     Nagls = new List<IHP_NAGLDOK>(context.IHP_NAGLDOK.Where(e => idList.Contains(e.STATUSZAM )));

            //     Items.Clear();

            //     foreach (IHP_NAGLDOK item in Nagls.OrderByDescending(x => x.ID_IHP_NAGLDOK))
            //     {
            //         ItemKafelek ik = new ItemKafelek();
            //         StringBuilder captlocal = new StringBuilder();
            //         string sumalosc = string.Empty;
            //         string termin = string.Empty;
            //         decimal sumadec = 0;
            //         int linia = 1;
            //         string nazwakontrah = string.Empty;
            //         if (!string.IsNullOrEmpty(item.KONTRAH.IMIE))
            //         {
            //             nazwakontrah = item.KONTRAH.IMIE;
            //         }
            //         if (!string.IsNullOrEmpty(item.KONTRAH.NAZWISKO))
            //         {
            //             nazwakontrah = item.KONTRAH.NAZWISKO;
            //         }
            //         ik.IdKontrah = item.ID_KONTRAH;
            //         ik.IdNagl = item.ID_NAGL;
            //         ik.StatusZam = item.STATUSZAM ?? 0;
            //         ik.Kontrah = nazwakontrah;

            //         foreach (POZ itempoz in item.POZ.Where(x => x.KARTOTEKA.ID_GRUPAKART == 4))
            //         {
            //             captlocal.Append(itempoz.KARTOTEKA.NAZWASKR + "(" + itempoz.WYMIARX.ToString() + "X" + itempoz.WYMIARY.ToString() + ") " + System.Environment.NewLine);
            //             sumadec += itempoz.ILOSCRAZEM ?? 0;
            //             linia++;
            //             if (linia >= 4)
            //             {
            //                 captlocal.Append("...");
            //                 break;
            //             }
            //         }
            //         //uzupelnij puste 
            //         for (int i = 0; i < 4 - linia; i++)
            //         {
            //             captlocal.AppendLine("");
            //         }
            //         sumadec = Math.Round(sumadec, 2);
            //         ik.NrZam = item.NRDOKWEW;
            //         termin = "ter.: " + item.TERMINREALIZ.Value.ToShortDateString();
            //         ik.Termin = termin;
            //         ik.Caption = captlocal.ToString();
            //         Items.Add(ik);
            //         Thread.Sleep(50);
            //     }
            // UpdateCollectionViews();
            Items.Clear();
            ItemKafelek ik = new ItemKafelek();
            ik.IdKontrah = 2323;
              ik.IdNagl = 232;
              ik.StatusZam =1;
              ik.Kontrah = "Krzysztpf";
            Items.Add(ik);
        }
        public ViewModelKafelki(bool noload)
        {
        }
        public ViewModelKafelki()
        {
            Dokumenty = new ObservableCollection<IHP_NAGLDOK>(context.IHP_NAGLDOK);

            Items = new ObservableCollection<ItemKafelek>();

            RefreshCommand = new DelegateCommand(UpdateMagView);
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);

            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = false;
            bgWorker.WorkerSupportsCancellation = false;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;


            bgWorkerFiltr = new BackgroundWorker();
            bgWorkerFiltr.WorkerReportsProgress = false;
            bgWorkerFiltr.WorkerSupportsCancellation = false;
            bgWorkerFiltr.DoWork += BgWorker_DoWorkFiltr;
            bgWorkerFiltr.RunWorkerCompleted += BgWorker_RunWorkerCompletedFiltr;

           //ClickTileCommand = new DelegateCommand<string>(ShowPoz);
           // FilterForPortfolioCmd = new DelegateCommand<object>(FilterOrdersForPortfolio, true);
           // ShowFiltrCommand = new DelegateCommand(ShowFiltr);
            ShowOknoCommand = new DelegateCommand<KeyEventArgs>(ZnajdzKafelek);
            UpdateMagView();
            Messenger.Default.Register<KafelekAkt>(this, OnShowHidePanel);
            Messenger.Default.Register<KafelekFiltr>(this, OnShowFilter);

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


            return true;
        }
        private bool CanSave()
        {
            //&& (_kontraharch != null)
            //if ((_kierowca!=null) &&  (_samochod != null) && (_kartoteka != null))

            return true;
            //else
            //    return false;
        }
        private bool CanEdit()
        {
            if (PozycjaDok != null)
                return true;
            else
                return false;
        }
        private void OnShowHidePanel(KafelekAkt aktkafelek)
        {
            if (aktkafelek.NrPanel == 201)
            {
                if (Items.Any(x => x.IdNagl == aktkafelek.IdNagl))
                           Items.FirstOrDefault(x => x.IdNagl == aktkafelek.IdNagl).StatusZam = aktkafelek.Status;
            }
        }

    private void OnShowFilter(KafelekFiltr _kafelekfiltr)
        {
            kafelekfiltr = _kafelekfiltr;

            if (kafelekfiltr.TypFiltra == 1)
            {
                UpdateMagView();
                return;
            }

            if (bgWorkerFiltr.IsBusy)
                return;
          else if (bgWorker.IsBusy)
                return  ;
            else
             bgWorkerFiltr.RunWorkerAsync();
        }

        private void LoadZamowieniaFlitr(KafelekFiltr kafelekfiltr)
        {
            try
            {


                if (kafelekfiltr.TypFiltra == 2)
                {
                    List<IHP_NAGLDOK> n = new List<IHP_NAGLDOK>();
                    //var IHP_NAGLDOK = PredicateBuilder.True<IHP_NAGLDOK>();
                    ////if (kafelekfiltr.IdKontrah > 0)
                    ////      n = Nagls.Where((x => x.ID_KONTRAH == kafelekfiltr.IdKontrah &&  x.DATADOK>kafelekfiltr.DataOd  && x.DATADOK < kafelekfiltr.DataDo)).ToList();
                    ////else
                    ////      n = Nagls.Where((x =>x.DATADOK > kafelekfiltr.DataOd && x.DATADOK < kafelekfiltr.DataDo)).ToList();

                    //if (kafelekfiltr.IdKontrah > 0)
                    //{
                    //    predicate = predicate.And(x => x.ID_KONTRAH == kafelekfiltr.IdKontrah);
                    //}


                    //predicate = predicate.And(x => x.DATADOK >= kafelekfiltr.DataOd);
                    //predicate = predicate.And(x => x.DATADOK <= kafelekfiltr.DataDo);

                    //n = context.NAGL.Where(predicate).ToList();

                    Items.Clear();
                    foreach (IHP_NAGLDOK item in n.OrderByDescending(x => x.ID_IHP_NAGLDOK))
                    {
                        ItemKafelek ik = new ItemKafelek();
                        StringBuilder captlocal = new StringBuilder();
                        string sumalosc = string.Empty;
                        string termin = string.Empty;
                        decimal sumadec = 0;
                        int linia = 1;
                        string nazwakontrah = string.Empty;
                        if (!string.IsNullOrEmpty(item.IHP_KONTRAHENT_ARCH.NAZWA))
                        {
                            nazwakontrah = item.IHP_KONTRAHENT_ARCH.NAZWA;
                        }
                     
                        ik.IdKontrah = item.ID_IHP_KONTRAHENT_ARCH;
                        ik.IdNagl = item.ID_IHP_NAGLDOK;
                       // ik.StatusZam = item.IHP_STATUSZAM;
                        ik.Kontrah = nazwakontrah;

                        foreach (IHP_POZDOK itempoz in item.IHP_POZDOK.Where(x => x.ID_IHP_KARTOTEKA == 4))
                        {
                           // captlocal.Append(itempoz.KARTOTEKA.NAZWASKR + "(" + itempoz.WYMIARX.ToString() + "X" + itempoz.WYMIARY.ToString() + ") " + System.Environment.NewLine);
                           // sumadec += itempoz.ILOSCRAZEM ;
                            linia++;
                            if (linia >= 4)
                            {
                                captlocal.Append("...");
                                break;
                            }
                        }
                        //uzupelnij puste 
                        for (int i = 0; i < 4 - linia; i++)
                        {
                            captlocal.AppendLine("");
                        }
                        sumadec = Math.Round(sumadec, 2);
                        ik.NrZam = item.NRDOKWEW;
                        termin = "ter.: " + item.TERMINREALIZ.Value.ToShortDateString();
                        ik.Termin = termin;
                        ik.Caption = captlocal.ToString();
                        Items.Add(ik);
                        Thread.Sleep(100);
                    }
                }
                Thread.Sleep(500);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public void ShowPoz(string id)
        //{
        //  PozycjeDok = new ObservableCollection<POZ>(context.POZ.Where(x => x.ID_NAGL == 36));
        //    var window = new KafelkiOkno();
        //   window.Show();
  
        //}
        //public void ShowFiltr()
        //{
      
        //    var window = new KafelkiFiltr();
        //    window.Show();
        //}
        //void FilterOrdersForPortfolio(object param)
        //{
        //    //        MessageBoxService.ShowMessage( String.Format("[Kafelek] = '{0}'", (param as ItemKafelek).IdNagl));
        //     int _idnagl = (param as ItemKafelek).IdNagl;

        //    var window = new KafelkiOkno();
        //    NaglSend ns = new NaglSend() { Numer = 146, IdNagl = _idnagl };
        //    Messenger.Default.Send<NaglSend>(ns);
        //    window.Show();
        //}

      
        //public bool CanSearchByKey(KeyEventArgs args)
        //{
        //    return true;
        //  //  return (args.Key == Key.Enter) && !string.IsNullOrEmpty(Kodkreskowy);
        //}
        void ZnajdzKafelek(KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                MessageBox.Show(Kodkreskowy);
            }
            else
            {
                Kodkreskowy += Kodkreskowy;
            }
            
        }

        private void UpdateTime()
        {
            if (secondsLeft <= 0)
            {
                UpdateMagView();
            }
            TimeSpan t = TimeSpan.FromSeconds(secondsLeft);
          //  liOdswiezanie.Label = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            secondsLeft -= 1;
            SecondsLeft = secondsLeft.ToString();
        }
        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadZamowienia();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        //    SplashScreenService.HideSplashScreen();
            if (e.Error != null)
            {
                DXMessageBox.Show( "Błąd połączenia przetwarzania:" + Environment.NewLine + e.Error.ToString(), "ERROR",MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
     
            secondsLeft = 90;
            timer.Start();
        }
        private void BgWorker_DoWorkFiltr(object sender, DoWorkEventArgs e)
        {
            try
            {
                LoadZamowieniaFlitr(kafelekfiltr);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        private void BgWorker_RunWorkerCompletedFiltr(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error != null)
            {
                DXMessageBox.Show("Błąd połączenia przetwarzania:" + Environment.NewLine + e.Error.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }
        private void UpdateMagView()
        {
            if (bgWorker.IsBusy)
                return;

           if (bgWorkerFiltr.IsBusy)
                return;

            timer.Stop();

            bgWorker.RunWorkerAsync();
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
         private void Save()
        {
     
        }
        private void Delete()
        {
       
        }
        public void SentSamochod()
        {

        }
         private void Print()
        {
 
        }
        private void Clear()
        {
        }
        private void Update()
        {
 
        }
        private void AddSubject()
        {
            //      SubjectSfera sb = new SubjectSfera();
            //        sb.DodajKontrahenta();

        }
         private void OnMessageDokumentEx(int ID_NAGL_DOK)
        {
            // TODO :  tutaj jakis warunek na inta ... np:
            if (ID_NAGL_DOK > 250)
                OnMessageDokument(ID_NAGL_DOK);
        }
        public void OnMessageDokument(int ID_NAGL_DOK)
        {
            Dokument = context.IHP_NAGLDOK?.FirstOrDefault(x => x.ID_IHP_NAGLDOK == ID_NAGL_DOK);

            if (Dokument == null) return;

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
            StringBuilder AdresBullid = new StringBuilder();
            if (!string.IsNullOrEmpty(Dokument.IHP_KONTRAHENT_ARCH.ULICA))
                AdresBullid.Append(Dokument.IHP_KONTRAHENT_ARCH.ULICA + " ");
            if (!string.IsNullOrEmpty(Dokument.IHP_KONTRAHENT_ARCH.NRDOMU))
                AdresBullid.Append(Dokument.IHP_KONTRAHENT_ARCH.NRDOMU + " ");
            if (!string.IsNullOrEmpty(Dokument.IHP_KONTRAHENT_ARCH.NRLOKALU))
                AdresBullid.Append(Dokument.IHP_KONTRAHENT_ARCH.NRLOKALU + " ");
            if (!string.IsNullOrEmpty(Dokument.IHP_KONTRAHENT_ARCH.MIEJSCOWOSC))
                AdresBullid.Append(Dokument.IHP_KONTRAHENT_ARCH.MIEJSCOWOSC + " ");
            Adres = AdresBullid.ToString();

            PozycjeDok.Clear();
            foreach (IHP_POZDOK item in Dokument.IHP_POZDOK)
            {
                PozycjeDok.Add(item);
            }
            SumaNagl = Dokument.IHP_POZDOK.Sum(x => x.CENABRUTTO);
        }
    }
    public class TileFlowBreakConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (((IList)values[1]) != null)
            {
                var index = ((IList)values[1]).IndexOf(values[0]);
                if ((index) % 3 == 0)
                    return true;
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


