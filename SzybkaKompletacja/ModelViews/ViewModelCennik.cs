using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Docking;
using KpInfohelp.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KpInfohelp
{
    class ViewModelCennik : CennikiRepository, INotifyPropertyChanged, IMVVMDockingProperties

    { 
 
     
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
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        string _nazwaceny;
        private decimal _oldprice;
        private ObservableCollection<IHP_DEFCENY> _lstdefceny;
        private ObservableCollection<CennikView> _lstcennik;
        private ObservableCollection<IHP_STAWKAVAT> _lststawkavat;
        private ObservableCollection<CennikViewHist> _lstcennikhist;
        public ObservableCollection<CennikViewHist> LstCennikHist
        {
            get
            {
                return _lstcennikhist;
            }
            set
            {
                _lstcennikhist = value;

            }

        }
        private List<IHP_GRUPAKART> LstDefGrupaKtL;
        private CennikView _selectedcennik;
        public CennikView SelectedCennik
        {
            get
            {
                return _selectedcennik;
            }
            set
            {
                _selectedcennik = value;
                if (_selectedcennik != null)
                {
                    _oldprice = _selectedcennik.CENAN;
                    LoadCollectionHist();
                }

                RisePropertyChanged("SelectedCennik");
            }
        }
        IHP_DEFCENY _defceny;
        List<IHP_CENNIK> _lstItem;
        private CennikView _oldrow;

        private bool _readonlynetto;
       public bool ReadonlyNetto
       {
            get 
            {
                return _readonlynetto;
                     }
            set
            {
                _readonlynetto = value;
                RisePropertyChanged("ReadonlyNetto");
            }
       }

        private bool _readonlybrutto;
        public bool ReadonlyBrutto
        {
            get
            {
                return _readonlybrutto;
            }
            set
            {
                _readonlybrutto = value;
                RisePropertyChanged("ReadonlyBrutto");
            }
        }
   
        public ViewModelCennik()
        {
            LstCennikHist = new ObservableCollection<CennikViewHist>();
            LstDefDeny = new ObservableCollection<IHP_DEFCENY>();
            LstDefGrupaKt = new ObservableCollection<IHP_GRUPAKART>();
            LstDefGrupaKtL = new List<IHP_GRUPAKART>();
            LstCennik = new ObservableCollection<CennikView>();
            LstCennikN = new ObservableCollection<IHP_CENNIK>();
            //LstStawkaVat =  new ObservableCollection<IHP_STAWKAVAT>(context.IHP_STAWKAVAT);
            _lstItem = new List<IHP_CENNIK>();
            LstCennik.CollectionChanged += LstCennik_CollectionChanged;
            //LstDefDeny.CollectionChanged += DefDeny_CollectionChanged;
            AddCommandCennik = new DelegateCommand(Zaczytaj);
            ClearCommand = new DelegateCommand(Clear);
            RefreshDef();
            LoadCollectionGrKontrah();
            MassagesReg();
        }

        private void MassagesReg()
        {
            Messenger.Default.Register<IHP_KAROTEKA_EX>(this, OnMessageKartotek);
        }

        public void OnMessageKartotek (IHP_KAROTEKA_EX item)
        {
            GetCenyKart(item.ID_IHP_KARTOTEKA);
        }


        public ICommand AddCommandCennik { get; private set; }
        public ICommand ClearCommand { get; set; }
        void RefreshDef()
        {
      

            foreach (IHP_DEFCENY item in GetAll2())
            {
                LstDefDeny.Add(item);
            }
        }
        private void Zaczytaj(int IdKart)
        {
            int autonumercennik = Context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == 3).NUMER;

            string LastMessage;
            try
            {
                var listKartotek = Context.IHP_KARTOTEKA.ToList();
                _lstItem.Clear();
                foreach (IHP_KARTOTEKA item in listKartotek)

                {
                    if (!LstCennik.Any(x => x.ID_IHP_KARTOTEKA == item.ID_IHP_KARTOTEKA))
                    {
                        autonumercennik++;
                        IHP_CENNIK cena = new IHP_CENNIK()
                        {
                            ID_IHP_CENNIK = autonumercennik,
                            ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA,
                            ID_IHP_DEFCENY = _defceny.ID_IHP_DEFCENY,
                            CENAN = 0,
                            CENAB = 0,
                            IHP_KARTOTEKA = item,
                            IHP_DEFCENY = _defceny
                        };
                        _lstItem.Add(cena);

                    }

                }
                Context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == 3).NUMER = autonumercennik;
                Context.IHP_CENNIK.AddRange(_lstItem);
                Context.SaveChanges();
                GetCeny(_defceny.ID_IHP_DEFCENY);
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

        private void Zaczytaj()
        {
            int autonumercennik = Context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == 3).NUMER;

            string LastMessage;
            try
            {
                var listKartotek = Context.IHP_KARTOTEKA.ToList();
                _lstItem.Clear();
                foreach (IHP_KARTOTEKA item in listKartotek)

                {
                    if (!LstCennik.Any(x => x.ID_IHP_KARTOTEKA == item.ID_IHP_KARTOTEKA && x.ID_IHP_DEFCENY == _defceny.ID_IHP_DEFCENY))
                    {
                        autonumercennik++;
                        IHP_CENNIK cena = new IHP_CENNIK()
                        {
                            ID_IHP_CENNIK = autonumercennik,
                            ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA,
                            ID_IHP_DEFCENY = _defceny.ID_IHP_DEFCENY,
                            CENAN = 0,
                            CENAB = 0,
                            IHP_KARTOTEKA = item,
                            IHP_DEFCENY = _defceny
                        };
                        _lstItem.Add(cena);

                    }

                }
                Context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == 3).NUMER = autonumercennik;
                Context.IHP_CENNIK.AddRange(_lstItem);
                Context.SaveChanges();
                GetCeny(_defceny.ID_IHP_DEFCENY);
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
        private void Del()
        {
            string LastMessage;
            try
            {
                if (_defceny != null)
                {
                    Context.IHP_DEFCENY.Remove(_defceny);
                    Context.SaveChanges();
                    RefreshDef();
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
        private void Clear()
        {
            GrupaKart = null;
            DefCeny = null;
            LstCennik.Clear();
            LstCennikHist.Clear();
        }
        public ObservableCollection<IHP_STAWKAVAT> LstStawkaVat
        {
            get
            {
                return _lststawkavat;
            }
            set
            {
                _lststawkavat = value;
                RisePropertyChanged("LstStawkaVat");
            }
        }
        public ObservableCollection<CennikView> LstCennik
        {
            get
            {
                return _lstcennik;
            }
            set
            {
                _lstcennik = value;
        //        RisePropertyChanged("LstCennik");
            }
        }

        private ObservableCollection<IHP_CENNIK> _lstcennikN;
        public ObservableCollection<IHP_CENNIK> LstCennikN
        {
            get
            {
                return _lstcennikN;
            }
            set
            {
                _lstcennikN = value;
                RisePropertyChanged("LstCennikN");
            }
        }


        public ObservableCollection<IHP_DEFCENY> LstDefDeny
        {
            get
            {
                return _lstdefceny;
            }
            set
            {
                _lstdefceny = value;
                RisePropertyChanged("DefDeny");
            }
        }
        private ObservableCollection<IHP_GRUPAKART> _lstgrupakt;
        public ObservableCollection<IHP_GRUPAKART> LstDefGrupaKt
        {
            get
            {
                return _lstgrupakt;
            }
            set
            {
                _lstgrupakt = value;
                RisePropertyChanged("LstDefGrupaKt");
            }
        }
        private IHP_GRUPAKART grupakart;
        public IHP_GRUPAKART GrupaKart
        {
            get
            {
                return grupakart;
            }
            set
            {
                grupakart = value;
                if((_defceny!=null) && (grupakart!= null))
                {
                    GetCenyGrKart(_defceny.ID_IHP_DEFCENY, grupakart.ID_IHP_GRUPAKART);
                }
               
                RisePropertyChanged("GrupaKart");
            }

        }
        private void GetCeny(int id_defceny)
        {
            if(_defceny.ODNETTO==1)
            {
                ReadonlyNetto = false;
                ReadonlyBrutto = true;
            }
            if(_defceny.ODNETTO == 0)
            {
                ReadonlyNetto = true;
                ReadonlyBrutto = false;
            }
                
            var query =
            from a in Context.IHP_CENNIK
            join k in Context.IHP_KARTOTEKA on a.ID_IHP_KARTOTEKA equals k.ID_IHP_KARTOTEKA
            join s in Context.IHP_STAWKAVAT on k.ID_IHP_STAWKAVAT equals s.ID_IHP_STAWKAVAT

            where (a.ID_IHP_DEFCENY == id_defceny)
            select new
            {
                a.ID_IHP_CENNIK,
                a.ID_IHP_DEFCENY,
                a.ID_IHP_KARTOTEKA,
                a.CENAN,
                a.CENAB,
                k.NAZWASKR,
                k.INDEKS,
                s.WARTOSC
   

            };
            LstCennik.Clear();
            _lstItem.Clear();
            _lstItem = Context.IHP_CENNIK.Where(x => x.ID_IHP_DEFCENY == id_defceny).ToList();

            foreach (var item in query)
            {
                CennikView cc = new CennikView()
                {
                    ID_IHP_CENNIK = item.ID_IHP_CENNIK,
                    NAZWASKR = item.NAZWASKR,
                    INDEKS = item.INDEKS,
                    CENAN = item.CENAN,
                    CENAB = item.CENAB,
                    ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA,
                    ID_IHP_DEFCENY = item.ID_IHP_DEFCENY,
                    VAT = item.WARTOSC,
                     };
                LstCennik.Add(cc);
            }
            RisePropertyChanged("LstCennik");
        }
        private void GetCenyKart(int id_kartoteka)
        {
            LstCennikN.Clear();
            foreach (IHP_CENNIK item in GetByIdKartoteka(id_kartoteka))
                LstCennikN.Add(item);
        }
         private void GetCenyGrKart(int id_defceny, int idGrKart)
        {
            if (_defceny.ODNETTO == 1)
            {
                ReadonlyNetto = false;
                ReadonlyBrutto = true;
            }
            if (_defceny.ODNETTO == 0)
            {
                ReadonlyNetto = true;
                ReadonlyBrutto = false;
            }

            var query =
            from a in Context.IHP_CENNIK
            join k in Context.IHP_KARTOTEKA on a.ID_IHP_KARTOTEKA equals k.ID_IHP_KARTOTEKA
            join s in Context.IHP_STAWKAVAT on k.ID_IHP_STAWKAVAT equals s.ID_IHP_STAWKAVAT
               where (a.ID_IHP_DEFCENY == id_defceny)
            select new
            {
                a.ID_IHP_CENNIK,
                a.ID_IHP_DEFCENY,
                a.ID_IHP_KARTOTEKA,
                a.CENAN,
                a.CENAB,
                k.NAZWASKR,
                k.INDEKS,
                s.WARTOSC

            };
            LstCennik.Clear();
            _lstItem.Clear();
            _lstItem = Context.IHP_CENNIK.Where(x => x.ID_IHP_DEFCENY == id_defceny).ToList();

            foreach (var item in query)
            {
         
                    CennikView cc = new CennikView()
                    {
                        ID_IHP_CENNIK = item.ID_IHP_CENNIK,
                        NAZWASKR = item.NAZWASKR,
                        INDEKS = item.INDEKS,
                        CENAN = item.CENAN,
                        CENAB = item.CENAB,
                        ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA,
                        ID_IHP_DEFCENY = item.ID_IHP_DEFCENY,
                        VAT = item.WARTOSC
                    };
                    LstCennik.Add(cc);
          
            }
     
            RisePropertyChanged("LstCennik");
        }
        private void LstCennik_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
       
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
              foreach (CennikView item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }

      

        }
        private void DefDeny_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            /*
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (DEFCENY item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
            */
        }
        private IHP_NUMERACJA GetId(int dlatabeli)
        {
            return Context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);

        }
        private int GetIdCennikHist()
        {
            int _cennikHist = 0;
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = GetId(7);
                numerpoz.NUMER++;
                _cennikHist = numerpoz.NUMER ;
                Context.IHP_NUMERACJA.Add(numerpoz);
                Context.Entry(numerpoz).State = EntityState.Modified;
                Context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //    MessageBoxService.ShowMessage(LastMessage);
            }
            return _cennikHist;

        }
        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           decimal cenaold = 0;
            string LastMessage;
            try
            {
             CennikView row = (CennikView)sender;
                if(_oldrow!=null)
                {
                    if (_oldrow.ID_IHP_CENNIK == row.ID_IHP_CENNIK)
                    {
                      {
                            //if (_defceny.ODNETTO == 1)
                            //{

                            //    if (_oldrow.CENAB == row.CENAB)&& (_oldrow.CENAN == row.CENAB))
                            //        return;

                            //}
                            //if (_defceny.ODNETTO == 0)
                            //{
                                if ((_oldrow.CENAN == row.CENAN) && (_oldrow.CENAB == row.CENAB))
                                            return;
                        }
                    }
                }
              decimal Cenanetto = row.CENAN;
              int  vat = row.VAT ;
              decimal wartvat = 0;
              decimal vatvalue = 0m;
              decimal Cenabrutto = row.CENAB;

                if (vat > 0)
                    {
                        if(_defceny.ODNETTO==1)
                        {
                            wartvat = (Cenanetto * vat) / 100;
                            Cenabrutto = Cenanetto + wartvat;
                            Cenabrutto = Math.Round(Cenabrutto, 2);
                        _oldrow = new CennikView()
                        {
                            ID_IHP_CENNIK = row.ID_IHP_CENNIK,
                            CENAB = Cenabrutto,
                            CENAN = Cenanetto
                        };
                            
                        
                        _lstcennik.FirstOrDefault(x => x.ID_IHP_CENNIK == row.ID_IHP_CENNIK).CENAB = Cenabrutto;
                    
                    }

                        if (_defceny.ODNETTO == 0)
                        {
                        vatvalue = (Convert.ToDecimal(vat) / 100);
                        vatvalue = 1 + vatvalue;
                        Cenanetto = Cenabrutto / vatvalue;
                            Cenanetto = Math.Round(Cenanetto, 2);
                             wartvat = Cenabrutto - Cenanetto;
                        _oldrow = new CennikView()
                        {
                            ID_IHP_CENNIK = row.ID_IHP_CENNIK,
                            CENAN = Cenanetto,
                            CENAB = Cenabrutto
                        };
                        _lstcennik.FirstOrDefault(x => x.ID_IHP_CENNIK == row.ID_IHP_CENNIK).CENAN = Cenanetto;
                        
                        }

                    }
                        else
                            Cenabrutto = Cenanetto; 

                  
                   IHP_CENNIK cena = Context.IHP_CENNIK.Where(x => x.ID_IHP_CENNIK == row.ID_IHP_CENNIK).SingleOrDefault();
                //wysylamy w eter
                if (cena != null)
                {
                     _lstItem.FirstOrDefault(x => x.ID_IHP_CENNIK == row.ID_IHP_CENNIK).CENAN = Cenanetto;
                     _lstItem.FirstOrDefault(x => x.ID_IHP_CENNIK == row.ID_IHP_CENNIK).CENAB = Cenabrutto;
                    Messenger.Default.Send<List<IHP_CENNIK>>(_lstItem);
                }
                //zapisuejmy do bazy
                if (cena!=null)
                {
                    cena.CENAB = row.CENAB;
                    cena.CENAN = row.CENAN;
                            DodajHistoria(row.CENAN);
                    Context.IHP_CENNIK.Attach(cena);
                    Context.Entry(cena).State = EntityState.Modified;
                    Context.SaveChanges();
                     LoadCollectionHist();
                 }
           }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public void LoadCollectionGrKontrah()
        {
            LstDefGrupaKtL = Context.IHP_GRUPAKART.ToList();
           LstDefGrupaKt.Clear();
             foreach (IHP_GRUPAKART item in LstDefGrupaKtL)
                                    LstDefGrupaKt.Add(item);
        }
        public void LoadCollectionHist()
        {
            if (_selectedcennik == null) return;
            var qu = from s in Context.IHP_CENNIKHISTORIA
                      join u in Context.IHP_ZAM_USERS on s.ID_IHP_ZAM_USERS equals u.ID_IHP_ZAM_USERS
                     where s.ID_IHP_CENNIK == _selectedcennik.ID_IHP_CENNIK
                     select new
                     {
                         s.DATAWPISU,
                          s.CENANA,
                         s.CENAZ,
                         UZYTKOWNIK = u.LOGIN
                     };
            LstCennikHist.Clear();
            foreach (var q in qu)
            {
                CennikViewHist zv = new CennikViewHist();
                zv.DATAWPISU = q.DATAWPISU  ;
                zv.CENANA = q.CENANA;
                zv.CENAZ = q.CENAZ;
                zv.UZYTKOWNIK = q.UZYTKOWNIK;
                LstCennikHist.Add(zv);
            }
        }
        private void DodajHistoria(decimal cena)
        {
            IHP_CENNIKHISTORIA cennikhist = new IHP_CENNIKHISTORIA()
            {
                ID_IHP_CENNIKHISTORIA = GetIdCennikHist(),
                ID_IHP_CENNIK = _selectedcennik.ID_IHP_CENNIK ?? 0,
                DATAWPISU = DateTime.Now,
                CENAZ = _oldprice,
                CENANA = cena,
                ID_IHP_ZAM_USERS = ProgramDataSotrage.User.ID_IHP_ZAM_USERS
            };
            Context.IHP_CENNIKHISTORIA.Add(cennikhist);

        }
        public string NazwaCeny
      {
        get
            {
                return _nazwaceny;
           }
        set
            {
                _nazwaceny = value;
                if (_defceny == null)
                    _defceny = new IHP_DEFCENY();
                 _defceny.NAZWACENY = value;
                RisePropertyChanged("NazwaCeny");
            }
      }
        public IHP_DEFCENY DefCeny
        {
            get
            {
                return _defceny;
            }
            set
            {
             _defceny = value;
                if(_defceny!=null)
                   GetCeny(_defceny.ID_IHP_DEFCENY);
             RisePropertyChanged("DefCeny");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}


