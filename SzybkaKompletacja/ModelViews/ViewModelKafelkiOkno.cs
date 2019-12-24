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
using System.Windows;
using System.Windows.Media;
namespace KpInfohelp
{
    //
 public class PozOknoKafelki: INotifyPropertyChanged
    {
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public int IdPoz { get; set;}

        public int IdDefStatus { get; set; }

        public int Lp { get; set; }
        public string NazwaMat { get; set; }
        private bool _zazn;
        public bool Zazn {
            get
            {
                return _zazn;
            }
             set
            {
                _zazn = value;
                RisePropertyChanged("Zazn");
            }
                }

        private int _statuspoz;
    
        public event PropertyChangedEventHandler PropertyChanged;
    }

   public class ViewModelKafelkiOkno : CrudVMBase, INotifyPropertyChanged
    {
        public Action CloseAction { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Color _kolorstart;
        public Color KolorStart
        {
            get
            {
                return _kolorstart;
            }
            set
            {
                _kolorstart = value;
                RisePropertyChanged("KolorStart");
            }
        }

        private Color _kolorstatus;
        public Color KolorStatus
        {
            get
            {
                return _kolorstatus;
            }
            set
            {
                _kolorstatus = value;
                RisePropertyChanged("KolorStatus");
            }
        }

        private Color _kolorclose;
        public Color KolorClose
        {
            get
            {
                return _kolorclose;
            }
            set
            {
                _kolorclose = value;
                RisePropertyChanged("KolorClose");
            }
        }
        bool wybierzstart,wybierzstatus ;
        string BgColor = string.Empty;
        protected virtual INavigationService NavigationService { get { return null; } }
        public string Header { get; set; }
        private string _nrzam;
        private int _statushistId = 0;
        public string NrZam {
            get
            {
                return _nrzam;
            }
           set
            {
                _nrzam = value;
                RisePropertyChanged("NrZam");
            }
                
                }
        public List<IHP_DEFSTATUS> ListaStatusow;
        IHP_DEFSTATUS SelDefStatus;

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
        public ICommand SelectAllCommand { get; private set; }
        public ICommand AddToSubjectCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public ICommand PrintCommand { get; private set; }
        public ICommand UpdNewProgCommand { get; private set; }
        public ICommand ItemSelSamochodCommand { get; private set; }
        public ICommand ItemSelKontrahentCommand { get; private set; }
        public ICommand ClickTileCommand { get; private set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ShowOknoCommand { get; set; }
        public ICommand StartCommand { get; set; }
        public ICommand StatusCommand { get; set; }
        private string Kodkreskowy = string.Empty;
        private readonly IDictionary<Key, int> NumericKeys =
                         new Dictionary<Key, int> {{ Key.D0, 0 },{ Key.D1, 1 },{ Key.D2, 2 },{ Key.D3, 3 },{ Key.D4, 4 },{ Key.D5, 5 },{ Key.D6, 6 },{ Key.D7, 7 },{ Key.D8, 8 },{ Key.D9, 9 },
                             { Key.NumPad0, 0 },{ Key.NumPad1, 1 },{ Key.NumPad2, 2 },{ Key.NumPad3, 3 },{ Key.NumPad4, 4 },{ Key.NumPad5, 5 },{ Key.NumPad6, 6 },{ Key.NumPad7, 7 },{ Key.NumPad8, 8 },{ Key.NumPad9, 9 }};

        private readonly IDictionary<Key, string> StringKeys =
                       new Dictionary<Key, string> { { Key.S, "S" }, };
        private int? GetKeyNumericValue(KeyEventArgs e)
        {
            if (NumericKeys.ContainsKey(e.Key)) return NumericKeys[e.Key];
            else return null;
        }
   
        void ZnajdzKafelek(KeyEventArgs e)
        {
            if (e.Key == Key.S)
                {
                   Kodkreskowy += "S";
                }
                else if (e.Key == Key.E)
                {
                    Kodkreskowy += "E";
                }
               else if(e.Key >= Key.D0 && e.Key <= Key.D9)
                 {
                    int num = GetKeyNumericValue(e) ?? -1;
                    Kodkreskowy += num.ToString();
                }

            if (e.Key == Key.Enter)
            {
                if (String.IsNullOrEmpty(Kodkreskowy)) return;
                if(Kodkreskowy=="EEEEE")
                {
                    CloseAction();
                    return;
                }
                if (Kodkreskowy.Substring(0, 1) == "S")
                {
                    SelDefStatus = ListaStatusow?.FirstOrDefault(x => x.KODSKANER == Kodkreskowy);
                    if(SelDefStatus!=null)
                    {
                        ZaznaczStatus(SelDefStatus.ID_IHP_DEFSTATUS);
                        Save(null);
                        PozycjeDok.ToList().ForEach(z => z.Zazn = false);

                    }
                }
                else
                {
                    int idpoz = Convert.ToInt32(Kodkreskowy);
                    ZaznaczPoz(idpoz);
                }                
               Kodkreskowy = string.Empty;
            }
        }
        private ViewStatusy _status;
        public ViewStatusy Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                RisePropertyChanged("Status");
            }
        }
 
        private void ZaznaczPoz(int IdPoz)
        {
            if (PozycjeDok.Any(x => x.IdPoz == IdPoz))
            {
                //PozycjeDok.FirstOrDefault(x => x.IdPoz == IdPoz).Zazn = true;
                PozycjaDok = PozycjeDok.FirstOrDefault(x => x.IdPoz == IdPoz);
                PozycjaDok.Zazn = true;
                RisePropertyChanged("PozycjaDok");
                UstawStatus();
           }
          else
                MessageBox.Show("Nie Znaleziono pozycji");
        }
        private void ZaznaczStatus(int IdDefStatus)
        {
      
                //PozycjeDok.FirstOrDefault(x => x.IdPoz == IdPoz).Zazn = true;
                Status = ListaStatus.FirstOrDefault(x => x.ID_DEFSTATUS == IdDefStatus);
           
                RisePropertyChanged("Status");
                UstawClose();
        

      
        }
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
        public void LoadMenuItem(int IdUser)
        {
            //ListaStatus.Clear();
 
            //var qu = from K in context.IHP_DEFSTATUS
            //         join N in context.IHP_WYSTSTATUS on K.ID_DEFSTATUS equals N.ID_DEFSTATUS
            //         where N.ID_ARIT_ZAM_USERS == IdUser
            //         select new
            //         {
            //             ID_ARIT_ZAM_USERS = N.ID_ARIT_ZAM_USERS,
            //             ID_DEFSTATUS = K.ID_DEFSTATUS,
            //             K.NAZWA

            //         };

            //foreach (var q in qu)
            //{
            //    ViewStatusy zvl = new ViewStatusy();
            //    zvl.ID_ARIT_ZAM_USERS = q.ID_ARIT_ZAM_USERS;
            //    zvl.ID_DEFSTATUS = q.ID_DEFSTATUS;
            //    zvl.NAZWA = q.NAZWA;
            //    ListaStatus.Add(zvl);
 
            //}

        }
        private int getLastStatus(int IdPoz)
        {
            //int res = 0;
            //List<STATUSHISTORIA> stathists = context.STATUSHISTORIA.Where(x => x.ID_POZ == IdPoz).ToList();
            //if(stathists.Count>0)
            //{
            //    STATUSHISTORIA stathist = stathists.LastOrDefault();
            //    if (stathist != null)
            //        res = stathist.ID_DEFSTATUS;
            //}
            //return res;
            return 1;
        }
        private void SaveStatus(int IdPoz)
        {
      
            //POZ p = context.POZ.Find(IdPoz);
            //int defstatusz = getLastStatus(IdPoz);
            //if (p != null)
            //{
            //    p.ID_DEFSTATUS = Status.ID_DEFSTATUS;
            //    context.POZ.Add(p);
            //    context.Entry(p).State = EntityState.Modified;
            //    context.SaveChanges();
            //}
            //STATUSHISTORIA stathist = new STATUSHISTORIA()
            //{
            //    ID_STATUSHISTORIA = GetIdStatusHist(),
            //    ID_DEFSTATUS = Status.ID_DEFSTATUS,
            //    ID_DEFSTATUSZ = defstatusz,
            //    DATAWPISU = DateTime.Now,
            //    OPIS = Status.NAZWA,
            //    ID_POZ = IdPoz,
            //    ID_ARIT_ZAM_USERS = ProgramDataSotrage.User.ID_ARIT_ZAM_USERS
            //};
            //context.STATUSHISTORIA.Add(stathist);
            //context.SaveChanges();
        }
        private int GetIdStatusHist()
        {
            string LastMessage = string.Empty;
            try
            {
                //NUMERACJA numerpoz = GetId(4);
                //numerpoz.NUMER++;
                //_statushistId = numerpoz.NUMER ?? 0;
                //context.NUMERACJA.Add(numerpoz);
                //context.Entry(numerpoz).State = EntityState.Modified;
                //context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                MessageBoxService.ShowMessage(LastMessage);
            }
            return _statushistId;
        }
          public ItemKafelek Kafelek { get; set; }

        public ObservableCollection<ItemKafelek> Items { get; set; }
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
        public ObservableCollection<ZamowieniaViewStatusNaglKafelek> ZamowieniaViewStatusLstNagl {get;set;}
        public ObservableCollection<ZamowieniaViewLista> ZamowieniaObrobkaLst
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
         private bool CheckKomplet()
        {
            bool res = true;

            foreach (PozOknoKafelki item in PozycjeDok)
            {
                if (!ZamowieniaViewStatusLstNagl.Any((x => x.ID_POZ == item.IdPoz && x.ID_DEFSTATUS == 3)))
                                       return false;
            }


            return res;
        }
        public void LoadCollectionHistNagl(int IdNagl)
        {
            var zamowienia = context.Database.SqlQuery<ZamowieniaViewStatusNaglKafelek>(string.Format(@"select P.ID_POZ, P.LP,P.WYMIARX,P.WYMIARY, K.INDEKS, SH.ID_STATUSHISTORIA, SH.ID_DEFSTATUS, SH.DATAWPISU, DS.NAZWA, SH.OPIS, SH.ID_POZ, AZ.ID_ARIT_ZAM_USERS,
                  SH.id_defstatus, DSZ.nazwa as  NAZWAZ, AZ.login as UZYTKOWNIK  from NAGL N  join POZ P on P.id_nagl = n.id_nagl
                    join KARTOTEKA K on k.id_kartoteka = p.id_kartoteka  join statushistoria SH on SH.id_poz = P.id_poz and K.ID_GRUPAKART=4
                    join DEFSTATUS DS on DS.id_defstatus = SH.id_defstatus
                    join DEFSTATUS DSZ on DSZ.id_defstatus = SH.id_defstatusz
                    join arit_zam_users az on az.id_arit_zam_users = sh.id_arit_zam_users
                  where n.id_nagl ={0} order by P.LP", IdNagl.ToString())).ToList();
            ZamowieniaViewStatusLstNagl.Clear();
            foreach (var q in zamowienia)
            {
                ZamowieniaViewStatusNaglKafelek zv = new ZamowieniaViewStatusNaglKafelek();
                zv.ID_POZ = q.ID_POZ;
                zv.INDEKS = q.INDEKS;
                zv.ID_DEFSTATUS = q.ID_DEFSTATUS;
                zv.WYMIARY = q.WYMIARY;
                zv.WYMIARX = q.WYMIARX;
                zv.LP = q.LP;
                zv.DATAWPISU = q.DATAWPISU;
                zv.NAZWA = q.NAZWA;
                zv.NAZWAZ = q.NAZWAZ;
                zv.UZYTKOWNIK = q.UZYTKOWNIK;
                ZamowieniaViewStatusLstNagl.Add(zv);
            }
        }
         public ViewModelKafelkiOkno()
        {
           Messenger.Default.Register<NaglSend>(this, OnMessageNumber);
            ZamowieniaViewStatusLstNagl = new ObservableCollection<ZamowieniaViewStatusNaglKafelek>();
            PozycjeDok = new ObservableCollection<PozOknoKafelki>();
            PozycjeDokObrobka = new ObservableCollection<PozOknoKafelki>();
            ShowOknoCommand = new DelegateCommand<KeyEventArgs>(ZnajdzKafelek);
            ListaStatus = new ObservableCollection<ViewStatusy>();
            CloseCommand = new DelegateCommand<Window>(Closeform);
            SelectAllCommand = new DelegateCommand(SelectAll);
            SaveCommand = new DelegateCommand<Window>(Save);
            LoadMenuItem(ProgramDataSotrage.User.ID_IHP_ZAM_USERS);
            StartCommand = new DelegateCommand(UstawStart);
            StatusCommand = new DelegateCommand(UstawStatus);
            ListaStatusow = context.IHP_DEFSTATUS.ToList();
            UstawStart();
        }
        private void UstawStart()
        {
            BgColor = "Red";
            KasujKolory();
            wybierzstart = true;
            KolorStart = (Color)ColorConverter.ConvertFromString("#FF0000");
        }
        private void UstawStatus()
        {
            BgColor = "Blue";
            KasujKolory();
            wybierzstatus = true;
             KolorStatus = (Color)ColorConverter.ConvertFromString("#0000FF");
        }
        private void UstawClose()
        {
            BgColor = "Green";
            KasujKolory();
     
            KolorClose = (Color)ColorConverter.ConvertFromString("#00FF00");
        }
        private void KasujKolory()
        {
            KolorStart = KolorStatus = KolorClose = Color.FromRgb(211, 211, 211);
            wybierzstart = wybierzstatus = false;
        }
        private ObservableCollection<IHP_NAGLDOK> _dokumenty;
        private ObservableCollection<PozOknoKafelki> _pozycjedok;
        public ObservableCollection<PozOknoKafelki> PozycjeDok
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

        private ObservableCollection<PozOknoKafelki> _pozycjedokobr;
        public ObservableCollection<PozOknoKafelki> PozycjeDokObrobka
        {
            get
            {
                return _pozycjedokobr;
            }
            set
            {
                _pozycjedokobr = value;
                RisePropertyChanged("PozycjeDokObrobka");
            }
        }
        private PozOknoKafelki _pozycjadok;
        public PozOknoKafelki PozycjaDok
        {
            get
            {
                return _pozycjadok;
            }
            set
            {
                _pozycjadok = value;
                if(_pozycjadok!=null)
                
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
        private void SelectAll()
        {
            if (PozycjeDok.Where(x => x.Zazn == true).Count() > 1)
                PozycjeDok.ToList().ForEach(x => x.Zazn = false);
            else
                PozycjeDok.ToList().ForEach(x => x.Zazn = true);

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
            return true;
       }
        private bool CanEdit()
        {
            if (PozycjaDok != null)
                return true;
            else
                return false;
        }
    
        private void Save(Window window)
        {
            if (Status == null)
            {
                MessageBox.Show("Wybierz Status !!!");
                return;
            }

           List<PozOknoKafelki> zaznaczone = PozycjeDok.Where(x => x.Zazn == true).ToList();

            IHP_NAGLDOK naglLocal = context.IHP_NAGLDOK?.FirstOrDefault(x => x.ID_IHP_NAGLDOK == Dokument.ID_IHP_NAGLDOK);
            if (naglLocal != null)
            {

                foreach (PozOknoKafelki item in zaznaczone)
                {
                    SaveStatus(item.IdPoz);
                }
                LoadCollectionHistNagl(Dokument.ID_IHP_NAGLDOK);
                OnMessageDokument(Dokument.ID_IHP_NAGLDOK);
                Status = null;
                if (CheckKomplet()) // zamowienie kompletne 
                {
               //     naglLocal.STATUSZAM = 3;
                    context.Entry(naglLocal).State = EntityState.Modified;
                    context.SaveChanges();
                    Messenger.Default.Send<KafelekAkt>(new KafelekAkt() { IdNagl = naglLocal.ID_IHP_NAGLDOK, NrPanel = 201, Status = 3 });
                }
                else
                {
               //     naglLocal.STATUSZAM = 1;
                    context.Entry(naglLocal).State = EntityState.Modified;
                    context.SaveChanges();
                    Messenger.Default.Send<KafelekAkt>(new KafelekAkt() { IdNagl = naglLocal.ID_IHP_NAGLDOK, NrPanel = 201, Status = 1 });
                }
            }
         }
        private void Delete(){}
        public void SentSamochod(){}
         private void Print(){}
        private void Clear(){}
        private void Update(){}
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

            NrZam = Dokument.NRDOKWEW;

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
               PozOknoKafelki pd = new PozOknoKafelki()
                    {
                        IdPoz = item.ID_IHP_POZDOK,
                        Lp = item.LP,

                        Zazn = false,
                        IdDefStatus = item.ID_IHP_DEFSTATUS??0
                    };
                      PozycjeDok.Add(pd);
               
            }
            LoadCollectionHistNagl(ID_NAGL_DOK);

        }
     
   
        public void Closeform(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
        public void OnMessageNumber(NaglSend number)
        {
            switch (number.Numer)
            {
               case 146:
                    OnMessageDokument(number.IdNagl);
                    break;
            }
        }

    }

}


