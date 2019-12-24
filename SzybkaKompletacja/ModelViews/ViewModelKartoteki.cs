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
using DevExpress.Xpf.Grid;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;
using DevExpress.Xpf.Core;
using System.Windows.Markup;
using System.Windows.Data;

namespace KpInfohelp
{
    public class RequiredValidationRulePhoto : ValidationRule
    {

        public static string GetErrorMessage(string fieldName, object fieldValue, object nullValue = null)
        {
            string errorMessage = string.Empty;
            if (nullValue != null && nullValue.Equals(fieldValue))
                errorMessage = string.Format("Pole:  {0} jest puste.", fieldName);
            if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
                errorMessage = string.Format("Pole: {0} jest puste.", fieldName);
            return errorMessage;
        }
        public string FieldName { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string error = GetErrorMessage(FieldName, value);
            if (!string.IsNullOrEmpty(error))
                return new ValidationResult(false, error);
            return ValidationResult.ValidResult;
            //           throw new NotImplementedException();
        }
    }
    public class ViewModelKartoteki : KartotekaRepository, INotifyPropertyChanged, IMVVMDockingProperties, IDataErrorInfo
    {

      //  KartotekaRepository kr = null;

        #region Property Pol

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
        string _showlog;
        public bool NewRec = true;
        private string _indeks, _nazwaskr, _nazwadl, _sww, _pkwiu, _kodean, _uwagi, _filename, _nazwafoto, _filefotofullpath;
        public string Filename
        {
            get
            {
                return _filename;
            }
            set
            {
                _filename = value;
                RisePropertyChanged("Filename");
            }
        }
        public string NazwaFoto
        {
            get
            {
                return _nazwafoto;
            }
            set
            {
                _nazwafoto = value;
                RisePropertyChanged("NazwaFoto");
            }
        }
        private string _nazwajedndodat;
        public string NazwaJednDodat
        {
            get
            {
                return _nazwajedndodat;
            }
            set
            {
                _nazwajedndodat = value;
                RisePropertyChanged("NazwaJednDodat");
                
            }
        }
        private string _nazwajednmiary;
        public string NazwaJednMairy
        {

            get
            {
                return _nazwajednmiary;
            }
            set
            {
                _nazwajednmiary = value;
                RisePropertyChanged("NazwaJednMairy");
            }
        }
        private string _skrotjednmiary;
        public string SkrotJednMairy
        {

            get
            {
                return _skrotjednmiary;
            }
            set
            {
                _skrotjednmiary = value;
                RisePropertyChanged("SkrotJednMairy");
            }
        }
        private decimal _iloscdodat;
        public decimal IloscDodat
        {
            get
            {
                return _iloscdodat;
            }
            set
            {
                _iloscdodat = value;
                RisePropertyChanged("IloscDodat");
            }
        }
        private bool _isaktywny;
        public bool IsAktywny
        {
            get
            {
                return _isaktywny;
            }
            set
            {
                _isaktywny = value;
                RisePropertyChanged("IsAktywny");
            }
        }
        public virtual int IdKartoteka { get; set; }
        #endregion
        #region Listy

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


        private ObservableCollection<IHP_WYSTGRKART> _lstwystgrkart;
        public ObservableCollection<IHP_WYSTGRKART> LstWystGrKart
        {
            get
            {
                return _lstwystgrkart;
            }
            set
            {
                _lstwystgrkart = value;
                RisePropertyChanged("LstWystGrKart");
            }
        }


        public ObservableCollection<IHP_GRUPAKART> GrupyKart { get; private set; }
        public ObservableCollection<IHP_RODZAJKART> RodzajeKart { get; private set; }
        public ObservableCollection<IHP_JM> lstJm { get; private set; }
        private List<IHP_KARTOTEKA> _listakartoteka;
        private List<IHP_GRUPAKART> _listgrupakart;
        private ObservableCollection<IHP_STAWKAVAT> _stawkavatlst;
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
        private ObservableCollection<IHP_RODZGRUPKART> _rodzkartlst;
        public ObservableCollection<IHP_RODZGRUPKART> RodzKartLst
        {
            get
            {
                return _rodzkartlst;
            }
            set
            {
                _rodzkartlst = value;
                RisePropertyChanged("RodzKartLst");
            }
        }
  
        private ObservableCollection<IHP_GRUPAKART> _grupakartlst;
        public ObservableCollection<IHP_GRUPAKART> GrupaKartLst
        {
            get
            {
                return _grupakartlst;
            }
            set
            {
                _grupakartlst = value;
                RisePropertyChanged("GrupaKartLst");
            }
        }

        private List<IHP_GRUPAKART> GrupaKartL;
        private List<IHP_RODZGRUPKART> GrupaGrKart;
        private List<IHP_GRUPAKART> _listagrkt;
        private ObservableCollection<IHP_FOTO> _fotos;
        public ObservableCollection<IHP_FOTO> Fotos
        {
            get
            {
                return _fotos;
            }
            set
            {
                _fotos = value;
                RisePropertyChanged("Fotos");

            }
        }
        public ObservableCollection<IHP_STAWKAVAT> StawkaVatLst
        {
            get
            {
                return _stawkavatlst;
            }
            set
            {
                _stawkavatlst = value;
                RisePropertyChanged("StawkaVatLst");
            }
        }

        private ObservableCollection<IHP_JZ> lstjedndodat;
        public ObservableCollection<IHP_JZ> LstJednDodat
        {
            get
            {
                return lstjedndodat;
            }
            set
            {
                lstjedndodat = value;
                RisePropertyChanged("LstJednDodat");
            }
        }

        private ObservableCollection<IHP_JM> lstjednmiary;
        public ObservableCollection<IHP_JM> LstJednMiary
        {
            get
            {
                return lstjednmiary;
            }
            set
            {
                lstjednmiary = value;
                RisePropertyChanged("LstJednMiary");
            }
        }

        private ObservableCollection<WystJednDodatView> _lstwystjedndodat;
        public ObservableCollection<WystJednDodatView> LstWystJednDodat
        {
            get
            {
                return _lstwystjedndodat;
            }
            set
            {
                _lstwystjedndodat = value;
                RisePropertyChanged("LstWystJednDodat");
            }
        }
        private WystJednDodatView _wystjedndodat;
        public WystJednDodatView WystJednDodat
        {

            get
            {
                return _wystjedndodat;
            }
            set
            {
                _wystjedndodat = value;
                RisePropertyChanged("WystJednDodat");
            }
        }

        void InitLists()
        {
            GrupaKartLst = new ObservableCollection<IHP_GRUPAKART>();
            RodzKartLst = new ObservableCollection<IHP_RODZGRUPKART>(context.IHP_RODZGRUPKART);
            RowUpdatedCommand = new DelegateCommand<KeyEventArgs>(AddNewItem);
            Kartoteki = new ObservableCollection<IHP_KAROTEKA_EX>();
            GrupyKart = new ObservableCollection<IHP_GRUPAKART>();
            GrupaKart = new IHP_GRUPAKART();
            Fotos = new ObservableCollection<IHP_FOTO>();
            lstJm = new ObservableCollection<IHP_JM>(context.IHP_JM);
            RodzajeKart = new ObservableCollection<IHP_RODZAJKART>(context.IHP_RODZAJKART);
            StawkaVatLst = new ObservableCollection<IHP_STAWKAVAT>(context.IHP_STAWKAVAT);
            LstJednDodat = new ObservableCollection<IHP_JZ>(context.IHP_JZ);
            LstWystJednDodat = new ObservableCollection<WystJednDodatView>();
            LstWystGrKart = new ObservableCollection<IHP_WYSTGRKART>();


        }

        #endregion
        #region Interfejsy i commandy
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        protected ISaveFileDialogService SaveFileDialogService { get { return this.GetService<ISaveFileDialogService>(); } }
        protected IOpenFileDialgoService OpenFileService { get { return GetService<IOpenFileDialgoService>(); } }

        public ICommand AddNewProgCommand { get; private set; }
        public ICommand DelNewProgCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; private set; }
        public ICommand UpdNewProgCommand { get; private set; }
        public ICommand ShowDependencyCommand { get; private set; }
        public ICommand ExportSubCommand { get; private set; }
        public ICommand ItemSelRodzCommand { get; set; }
        public ICommand RowUpdatedCommand { get; private set; }
        public ICommand ShowJednostkiListCommand { get; set; }
        public ICommand SaveFile { get; set; }
        public ICommand EditFile { get; set; }
        public ICommand SelectFile { get; set; }
        public ICommand DelFile { get; set; }
        public ICommand ClearFile { get; set; }
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
        public ICommand SearchByKeyCommand;
        public ICommand AddCommandJz
        {
            get; set;
        }
        public ICommand EditCommandJz { get; set; }
        public ICommand ClearCommandJz { get; set; }
        public ICommand UpdateCommandJz { get; set; }
        public ICommand DodajJednosteWystCommand { get; set; }
        public ICommand ClearJednosteWystCommand { get; set; }
        public ICommand UpdateJednosteWystCommand { get; set; }
        public ICommand EditJednosteWystCommand { get; set; } 
        public ICommand DelJednostkeWystCommand { get; set; }

        public ICommand  AddCommandJm { get; set; }
        public ICommand ClearCommandJm { get; set; }
        public ICommand UpdateCommandJm { get; set; }
        public ICommand EditCommandJm { get; set; }
        public ICommand DeleteCommandJm { get; set; }
        public ICommand AddNewGrupaKartCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand DeleteWystGrKart { get; set; }
        

        void InitCommands()
        {
            AddNewProgCommand = new RelayCommand(Save, CanSave);
            DelNewProgCommand = new DelegateCommand(Del);
            UpdNewProgCommand = new RelayCommand(Update, CanUpdate);
            ClearNewProgCommand = new DelegateCommand(Clear);
            SearchByKeyCommand = new DelegateCommand(SelStawkaVat);
            ShowDependencyCommand = new RelayCommand(pokazokno, CanOpen);
            ExportSubCommand = new RelayCommand(ExpoertSubject, CanExportSuject);
            ItemSelRodzCommand = new DelegateCommand(LoadCollectionGrKart);
            SelectFile = new RelayCommand(ZaczytajPlik, CanZaczytajPlik);
            SaveFile = new RelayCommand(SavePhoto, CanSavePhoto);
            DelFile = new RelayCommand(DelPhoto, CanDelPhoto);
            ClearFile = new DelegateCommand(ClearF);
            ShowJednostkiListCommand = new DelegateCommand(PokazJednostkiSlownik);
            AddCommandJz = new DelegateCommand(DodajJednDodat);
   
            EditCommandJz = new DelegateCommand(EditForms);
            ClearCommandJz = new DelegateCommand(ClearJz);
            UpdateCommandJz = new DelegateCommand(UpdateJz);
            DodajJednosteWystCommand = new DelegateCommand(SaveJzW);
            DelJednostkeWystCommand = new DelegateCommand(DelJzw);

            AddCommandJm = new DelegateCommand(DodajJednMiary);
            ClearCommandJm = new DelegateCommand(ClearJm);
             UpdateCommandJm = new DelegateCommand(UpdateJm);
            EditCommandJm = new DelegateCommand(EditJm);
             DeleteCommandJm = new DelegateCommand(DeleteJm);

            AddNewGrupaKartCommand = new DelegateCommand(PokazGrupeKart);
            DeleteWystGrKart = new DelegateCommand(DelWystGrK);


        }

        private void PokazGrupeKart()
        {
            var aaa = new GrupyKartPodpowiedz();
            aaa.Show();
        }

        private void UpdateJm()
        {
            throw new NotImplementedException();
        }

        private void EditJm()
        {
            throw new NotImplementedException();
        }

        private void DeleteJm()
        {
            throw new NotImplementedException();
        }


        #endregion
        #region entities i zmienne
        private string  jednmiary;
        public string JedMiary
        {
            get
            {
                return jednmiary;
            }
            set
            {
                jednmiary = value;
                RisePropertyChanged("JedMiary");
            }
        }
        private IHP_WYSTGRKART _wystgrkart;
        public  IHP_WYSTGRKART WystGrKart
        {
            get
            {
                return _wystgrkart;
            }
            set
            {
                _wystgrkart = value;
                RisePropertyChanged("WystGrKart");
            }
        }
        private IHP_JZ jedndodat;
        public IHP_JZ JednDodat
        {
            get
            {
                return jedndodat;
            }
            set
            {
                jedndodat = value;
                RisePropertyChanged("JednDodat");
            }
        }
   
        public IHP_JM _jm;
        private IHP_GRUPAKART _grupakart;
        public IHP_GRUPAKART GrupaKart
        {
            get
            {
                return _grupakart;
            }
            set
            {
                _grupakart = value;
                RisePropertyChanged("GrupaKart");
            }
        }
        private IHP_RODZAJKART _rodzajkart;
        public IHP_RODZAJKART RodzajKart
        {
            get
            {
                return _rodzajkart;

            }

            set
            {
                _rodzajkart = value;
                RisePropertyChanged("RodzajKart");
            }
        }
        private IHP_RODZGRUPKART _rodzgrkart;
        public IHP_RODZGRUPKART RodzGrKart
        {
            get
            {
                return _rodzgrkart;
            }
            set
            {
                _rodzgrkart = value;

                if (_rodzgrkart != null)
                    LoadCollection();
                RisePropertyChanged("RodzGrKart");
            }
        }
        public IHP_JM Jm
        {
            get
            {
                return _jm;
            }
            set
            {
                _jm = value;
            }
        }
        private IHP_STAWKAVAT _stawkavat;
        public IHP_STAWKAVAT Stawkavat
        {
            get
            {
                return _stawkavat;
            }
            set
            {
                _stawkavat = value;
                RisePropertyChanged("Stawkavat");
            }
        }
        private IHP_FOTO _foto;
        public IHP_FOTO Foto
        {
            get
            {
                return _foto;
            }
            set
            {
                _foto = value;
                RisePropertyChanged("Foto");
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
                NewRec = false;
                if (_kartoteka != null)
                {
                    _idkartotekaedit = _kartoteka.ID_IHP_KARTOTEKA;
                    Messenger.Default.Send<IHP_KAROTEKA_EX>(Kartoteka);
                     LoadFotos(_idkartotekaedit);
                    LoadWystJednDodat();
                    LoadWGrKart();
                }
                  RisePropertyChanged("Kartoteka");
            }
        }

        private IHP_KARTOTEKA _kart;
        public IHP_KARTOTEKA Kart
        {
            get
            {
                return _kart;
            }
            set
            {
                _kart = value;
                RisePropertyChanged("Kart");
            }
        }

        #endregion

        void SeedData()
        {
            LoadCollection();
            LoadRodzKartLst();
            LoadWystJednDodat();
        }

        public int SelectedGrupaKartID { get; set; }
        public ViewModelKartoteki()
        {
             InitLists();
              InitCommands();
                MassagesReg();
                    SeedData();
        }


        private void MassagesReg()
        {
            Messenger.Default.Register<List<IHP_GRUPAKART>>(this, OnMessageUpdate);
            Messenger.Default.Register<IHP_GRUPAKART>(this, OnMessageGrupyKart);
      
        }

        void FocusFirst()
        {
            SetFocus(1);
         }
        public bool CanZaczytajPlik()
        {
                    return true;
        }
        public void ZaczytajPlik()
        {
            if (!Directory.Exists(ProgramDataSotrage.xmlSqlConfig.PicturePath))
            {
                MessageBox.Show(string.Format("Nie mogę odczytać folderu docelowego ({0}) z pliku konfiguracyjnego!", ProgramDataSotrage.xmlSqlConfig.PicturePath));
                return;
            }
            OpenFileDialog op = OpenFileService.ShowFotoAll();
             Filename = op.SafeFileName;
            _filefotofullpath = op.FileName;
            if (string.IsNullOrEmpty(Filename)) return;
      
        }
        void AddNewItem( KeyEventArgs e)
        {
       
            if(e.Key==Key.Insert)
            {
                if (Kartoteka.Zaznaczenie)
                    Kartoteka.Zaznaczenie = false;
                else
                    Kartoteka.Zaznaczenie = true;
            }
       
        }

        private void LoadJEdnMiary()
        {
            LstJednMiary.Clear();
            foreach (IHP_JM item in JmGetAll())
                LstJednMiary.Add(item);
        }

        private void LoadJEdnDodat()
        {
            LstJednDodat.Clear();
            foreach (IHP_JZ item in GetAll())
                                LstJednDodat.Add(item);
        }

        private void LoadWystJednDodat()
        {
          if(Kartoteka!=null)
            {
                LstWystJednDodat.Clear();
                foreach (WystJednDodatView item in GetWstyJednDodatAll(Kartoteka.ID_IHP_KARTOTEKA))
                    LstWystJednDodat.Add(item);
            }
       
        }
        private void LoadRodzKartLst()
        {
            GrupaGrKart = context.IHP_RODZGRUPKART.ToList();
            RodzKartLst.Clear();

            foreach (IHP_RODZGRUPKART item in GrupaGrKart)
                RodzKartLst.Add(item);
        }
        private void LoadCollectionGrKart()
        {
            if (_rodzgrkart == null) return;
            _listagrkt = context.IHP_GRUPAKART.Where(x => x.ID_IHP_RODZGRUPKART == _rodzgrkart.ID_IHP_RODZGRUPKART).ToList();
            GrupaKartLst.Clear();

            foreach (IHP_GRUPAKART item in _listagrkt)
            {
                GrupaKartLst.Add(item);

            }
            RisePropertyChanged("GrupaKartLst");
        }
        private bool CanExportSuject()
        {
            if (_kartoteka != null)
            {
                if(_kartoteka.ID_TOWSUBJECT != null)
                          return false;
            }
             return true;
           
        }
        void ExpoertSubject()
        {
           // int idTowarSubject=-1;

           // if (_kartoteka == null) return;

           //try
           // {
           //     SubjectSfera sf = new SubjectSfera();
           //     if (_kartoteka.ID_IHP_GRUPAKART == 4)
           //     {
           //         idTowarSubject = sf.DodajTowar(_kartoteka);
           //     }
           //     else if (_kartoteka.ID_IHP_GRUPAKART == 1)
           //     {
           //         idTowarSubject = sf.DodajUsluge(_kartoteka);
           //     }
                  

           //     if (idTowarSubject > -1)
           //     {
           //         _kartoteka.ID_TOWSUBJECT = idTowarSubject;
           //         context.IHP_KARTOTEKA.Add(_kartoteka);
           //         context.Entry(_kartoteka).State = EntityState.Modified;
           //         context.SaveChanges();
           //         Kartoteki.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA).ID_TOWSUBJECT = idTowarSubject;
           //     }

           // }
           //catch(Exception ex)
           // {
                 
           //}
     }
        void pokazokno()
        {
            //if (_kartoteka != null)
            //    AppConfig.KartotekaMat = _kartoteka;
            // var w = new KartotekiObrobka(1);
            //   w.ShowDialog();
            //SentKartoteka();
        }
        void PokazJednostkiSlownik()
        {
  
            var w = new JaednDodatPodpowiedz();
               w.ShowDialog();
              LoadJEdnDodat(); 
        }
        public void OnMessageGrupyKart(IHP_GRUPAKART Name)
        {
            if (Kartoteka != null)
            {
                  SaveWystGrKart(Kartoteka.ID_IHP_KARTOTEKA, Name.ID_IHP_GRUPAKART);
                LoadWGrKart();
            }
            RisePropertyChanged("LstWystGrKart");

        }
        private void LoadWGrKart()
        {
            LstWystGrKart.Clear();
            foreach (IHP_WYSTGRKART item in getwystgrkartallbyKart(Kartoteka.ID_IHP_KARTOTEKA))
            {
                if (!LstWystGrKart.Any(x => x.ID_KARTOTEKA == Kartoteka.ID_IHP_KARTOTEKA &&  x.ID_IHP_GRUPAKART  == item.ID_IHP_GRUPAKART))
                                                                     LstWystGrKart.Add(item);
            }
        }
        void DelWystGrK()
        {
            if(WystGrKart!=null)
            {
                DeleteWystGrKart(WystGrKart);
                LoadWGrKart();
            }
       }

       void SaveJzW()
        {
            if(JednDodat!= null)
            {
                WystJednDodatView wn = new WystJednDodatView();

                wn.ID_IHP_JZ = JednDodat.ID_IHP_JZ;
                wn.NAZWA = JednDodat.NAZWA;

                if (IsAktywny)
                    wn.AKTYWNA = 1;
                else
                   wn.AKTYWNA = 0;

                 wn.ID_KARTOTEKA = Kartoteka.ID_IHP_KARTOTEKA;
                SaveWyJz(wn);
                LoadWystJednDodat();
         }

     
    }

        void DelJzw()
        {
            if (WystJednDodat != null)
            {
               DelWyJz(WystJednDodat.ID_IHP_WYST_JZ);
                LoadWystJednDodat();
            }
        }
        void DodajJednDodat()
        {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                DXMessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz",   MessageBoxButton.OK, MessageBoxImage.Error);
                //    MessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK);
                return;
            }
                IHP_JZ jz = new IHP_JZ();
                jz.ID_IHP_JZ = GetNextNumer(1);
                jz.WARTOSC = _iloscdodat;
                     jz.NAZWA = _nazwajedndodat;
                SaveJz(jz);
            ClearJz();
            LoadJEdnDodat();
        }
        void EditForms()
        {
            if (JednDodat != null)
            {
                NazwaJednDodat = JednDodat.NAZWA;
                IloscDodat = JednDodat.WARTOSC;
                IsAktywny = (JednDodat.AKTYWNA==1);
            }
       }
        void UpdateJz()
         {
                string error = EnableValidationAndGetError();
                if (error != null)
                {
                    DXMessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK, MessageBoxImage.Error);
                    //    MessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK);
                    return;
                }
            IHP_JZ jz = new IHP_JZ();
            jz.ID_IHP_JZ = JednDodat.ID_IHP_JZ;
            jz.WARTOSC = _iloscdodat;
            jz.NAZWA = _nazwajedndodat;
            if (IsAktywny)
            {
              jz.AKTYWNA = Convert.ToInt16(1);
            }
             else
               jz.AKTYWNA = 0;

              UpdateJz(jz);
              LoadJEdnDodat();
              ClearJz();
     }
        void SaveJz()
        {
            if (JednDodat != null)
            {
                IHP_JZ wn = new IHP_JZ();

                wn.ID_IHP_JZ = JednDodat.ID_IHP_JZ;
                wn.NAZWA = JednDodat.NAZWA;

                if (IsAktywny)
                    wn.AKTYWNA = 1;
                else
                    wn.AKTYWNA = 0;
                  SaveJz(wn);
                LoadWystJednDodat();
            }


        }
        void DelJz()
        {
            if (WystJednDodat != null)
            {
                 DelWyJz(WystJednDodat.ID_IHP_WYST_JZ);
                LoadWystJednDodat();
            }
        }
        void DodajJz()
        {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                DXMessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK, MessageBoxImage.Error);
                //    MessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK);
                return;
            }
            IHP_JZ jz = new IHP_JZ();
            jz.ID_IHP_JZ = GetNextNumer(1);
            jz.WARTOSC = _iloscdodat;
            jz.NAZWA = _nazwajedndodat;
             SaveJz(jz);
            ClearJz();
            LoadJEdnDodat();
        }
        void EditJz()
        {
            if (JednDodat != null)
            {
                NazwaJednDodat = JednDodat.NAZWA;
                IloscDodat = JednDodat.WARTOSC;
                IsAktywny = (JednDodat.AKTYWNA == 1);
            }
        }
        void ClearJz()
        {
            NazwaJednDodat = string.Empty;
            IloscDodat = 0;
            IsAktywny = false;
            JednDodat = null;
        }

        void ClearJm()
        {
            NazwaJednMairy = string.Empty;
            SkrotJednMairy = string.Empty;
            IsAktywny = false;
            JedMiary = null;
        }

        void DodajJednMiary()
        {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                DXMessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK, MessageBoxImage.Error);
                //    MessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK);
                return;
            }
            IHP_JM jm = new IHP_JM();
            jm.ID_IHP_JM = GetNextNumer(20);
            jm.JM = _skrotjednmiary;
            jm.OPISJM = _nazwajednmiary;
            AddJm(jm);
            ClearJm();
        }

        private void SelStawkaVat()
        {
            if((Kartoteka!=null) && (_stawkavat!=null))
              Kartoteka.ID_IHP_STAWKAVAT = _stawkavat.ID_IHP_STAWKAVAT;
        }
        private bool CanOpen()
        {
            if (Kartoteka != null)
                return true;
            else
                return false;
        }
        public void SentKartoteka()
        {
            Messenger.Default.Send<IHP_KARTOTEKA>(_kartoteka);
        }
        public void SentKartoteki()
        {
            Messenger.Default.Send<List<IHP_KARTOTEKA>>(_listakartoteka);
        }
        public void OnMessageUpdate(List<IHP_GRUPAKART> Name)
        {
            this._listgrupakart = Name;
            GrupyKart.Clear();
            foreach (IHP_GRUPAKART item in _listgrupakart)
            {
                GrupyKart.Add(item);
            }
        }
   
        
        public string Indeks
        {
            get
            {
                return _indeks;
            }
            set
            {
                _indeks = value;
                if (_kartoteka != null)
                     _kartoteka.INDEKS = value;
                else
                  {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.INDEKS = value;
                }
                RisePropertyChanged("Indeks");
            }
       }
        public string ShowLog

        {
            get
            {
                return _showlog;
            }
            set
            {
                _showlog = value;
            }
        }
        public string NazwaSkr
        {
            get
            {
                return _nazwaskr;
            }
            set
              {
                _nazwaskr = value;
                if (_kartoteka != null)
                    _kartoteka.NAZWASKR = value;
                else
                {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.NAZWASKR = value;
                }
                RisePropertyChanged("NazwaSkr");
            }
        }
        public string NazwaDl
        {
            get
            {
                return _nazwadl;
            }
            set
            {
                _nazwadl = value;
                if (_kartoteka != null)
                    _kartoteka.NAZWADL = value;
                else
                {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.NAZWADL = value;
                }
                RisePropertyChanged("NazwaDl");
            }
        }
        public string Sww
        {
            get
            {
                return _sww;
            }
            set
            {
                _sww = value;
                if (_kartoteka != null)
                    _kartoteka.SWW = value;
                else
                {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.SWW = value;
                }
                RisePropertyChanged("Sww");
            }
        }
        public string Pkwiu
        {
            get
            {
                return _pkwiu;
            }
            set
            {
             _pkwiu = value;
               if (_kartoteka != null)
                  _kartoteka.PKWIU = value;
               else
               {
                   _kartoteka = new IHP_KAROTEKA_EX();
                   _kartoteka.PKWIU = value;
                }
                RisePropertyChanged("Pkwiu");
           }
        }
        public string Uwagi
        {
            get
            {
                return _uwagi;
            }
            set
            {
                _pkwiu = value;
                if (_kartoteka != null)
                    _kartoteka.UWAGI = value;
                else
                {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.UWAGI = value;
                }
                RisePropertyChanged("Uwagi");
            }
        }
        public string KodEan
        {
            get
            {
                return _kodean;
            }
            set
            {
                _kodean = value;
               if (_kartoteka != null)
                    _kartoteka.KODEAN = value;
                else
                {
                    _kartoteka = new IHP_KAROTEKA_EX();
                    _kartoteka.KODEAN = value;
                }
                RisePropertyChanged("KodEan");
            }
        }
        private void Cl()
        {
            _kartoteka.NAZWADL = string.Empty;
            _kartoteka.KODEAN = string.Empty;
            _kartoteka.NAZWASKR = string.Empty;
            _kartoteka.SWW = string.Empty;
            _kartoteka.UWAGI= string.Empty;
            Kartoteka = null;
        }
        private void Clear()
        {
            if (_kartoteka != null)
            {
                 Kartoteka = null;
                _kartoteka = null;
            }
            NewRec = true;
        }
        private void ClearF()
        {
            Foto = null;
            Filename = string.Empty;
            NazwaFoto = string.Empty;
        }
        private bool CanUpdate()
        {
            if (!NewRec)
                return true;
            else
                return false;
            
        }
        private void Zaloguj(string log)
        {
            ShowLog = log;
        }
        void Update()
        {
            string LastMessage;
            try
            {
               if ((_kartoteka != null) && (_grupakart != null)) 
                {
                    IHP_KARTOTEKA kartlocal = context.IHP_KARTOTEKA?.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA);
                    context.Database.Log = Zaloguj;
                    kartlocal.NAZWADL = NazwaDl;
                    kartlocal.NAZWASKR = NazwaSkr;
                    kartlocal.IHP_JM = _jm;
                    kartlocal.IHP_STAWKAVAT = _stawkavat;
                 kartlocal.KODEAN = _kodean;
                  context.IHP_KARTOTEKA.Attach(kartlocal);
                    context.Entry(kartlocal).State = EntityState.Modified;
                    context.SaveChanges();
                    SentKartoteki();
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
          
            LoadCollection();
            NewRec = true;

        }
        public void LoadCollection()
        {
          Kartoteki.Clear();
            _listakartoteka = new List<IHP_KARTOTEKA>();
            _listakartoteka = context.IHP_KARTOTEKA.ToList();

            foreach(IHP_KARTOTEKA item in _listakartoteka)
            {
                Kartoteki.Add( new IHP_KAROTEKA_EX(context,item));
            }
         //   SentKartoteki();
        }
        private IHP_NUMERACJA GetId(int dlatabeli)
        {
            return context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);
        }
        public bool CanSave()
        {
            if (NewRec)
                return true;
            else
                return false;

        }
        private void Save()
          {
            string LastMessage;
            try
            {
              if ((_kartoteka != null) && (_grupakart!= null)) 
                {

                    IHP_KARTOTEKA kartlocal = new IHP_KARTOTEKA();
                    kartlocal.ID_IHP_KARTOTEKA =  GetNextNumer(1);
                    context.Database.Log = Zaloguj;
                    kartlocal.NAZWADL = NazwaDl;
                    kartlocal.NAZWASKR = NazwaSkr;
                    kartlocal.IHP_JM = _jm;
                    kartlocal.IHP_STAWKAVAT = _stawkavat;
                    kartlocal.KODEAN = _kodean;
                    kartlocal.INDEKS = _indeks;
                    kartlocal.ID_IHP_RODZAJKART = 1;
                    context.IHP_KARTOTEKA.Add(kartlocal);
                    context.Entry(kartlocal).State = EntityState.Added;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    NewRec = true;
                    SentKartoteki();
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
        private bool CanSavePhoto()
        {
            if (!String.IsNullOrEmpty(_filename) && !String.IsNullOrEmpty(_nazwafoto))
                return true;
            else
                return false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void Del()
        {
            string LastMessage;
            try
            {
                if (_kartoteka != null)
                {
                    if(context.IHP_POZDOK.Any(x=>x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA))
                    {
                   MessageBoxService.Show("Kartoteka wykorzystana w zamówieniu");
                        return;
                    }
                   if(context.IHP_CENNIK.Any(x=>x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA))  //usuwany ceny z cennika
                    {
                        List<IHP_CENNIK> ceny = context.IHP_CENNIK.Where(x => x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA).ToList();
                        context.IHP_CENNIK.RemoveRange(ceny);
                    }
                    IHP_KARTOTEKA _kartotekalocal = context.IHP_KARTOTEKA.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == _kartoteka.ID_IHP_KARTOTEKA);
                    context.IHP_KARTOTEKA.Remove(_kartotekalocal);
                    context.SaveChanges();
                    LoadCollection();
                    SentKartoteki();
                }
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
               MessageBoxService.Show(LastMessage);
              LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }

       }
 
        private int _idkartotekaedit;
        public void  LoadFotos(int IdKart)
        {
            List<IHP_FOTO> lista = context.IHP_FOTO.Where(x => x.ID_IHP_KARTOTEKA == IdKart).ToList();
            Fotos.Clear();
            foreach (IHP_FOTO item in lista)
            {
           
           if (item.NAMEFILE.IndexOf(@"\")==-1 )
                        item.NAMEFILE = ProgramDataSotrage.xmlSqlConfig.PicturePath + item.NAMEFILE;
                Fotos.Add(item);
            }
       }
         private void SavePhoto()
        {
            string LastMessage;
            int MaxLp = 0;

            try
            {

                if (Kartoteka == null) return;
                int idKartoeka = Kartoteka.ID_IHP_KARTOTEKA;

                if (Kartoteka.ID_IHP_KARTOTEKA == 0)
                {
                    MessageBox.Show("wybierz Kartoteke!!");
                    return;
                }
                if (!File.Exists(_filefotofullpath))
                {
                    MessageBox.Show("Wybierz ścieżkę");
                    return;
                }

                IHP_FOTO _foto = new IHP_FOTO();

                string newFileName = ProgramDataSotrage.xmlSqlConfig.PicturePath + _filename;
                if (!Directory.Exists(ProgramDataSotrage.xmlSqlConfig.PicturePath))
                {
                    MessageBox.Show("Nie mogę odczytać katalogu docelowego z pliku configuracyjnego !!");
                    return;
                }

                if (context.IHP_FOTO.Any(x => x.ID_IHP_KARTOTEKA == Kartoteka.ID_IHP_KARTOTEKA))
                {
                    MaxLp = Maxihp(context.IHP_FOTO.Where(x => x.ID_IHP_KARTOTEKA == Kartoteka.ID_IHP_KARTOTEKA).ToList());
                    MaxLp++;
                }
                    else
                      MaxLp = 1;


                File.Copy(_filefotofullpath, newFileName, true);
                IHP_NUMERACJA numerfoto = GetId(17);
                if (numerfoto != null)
                    numerfoto.NUMER++;
                int idihpfoto = numerfoto.NUMER;
                _foto.ID_IHP_FOTO = idihpfoto;
                _foto.ID_IHP_KARTOTEKA = idKartoeka;
                _foto.NAMEFILE = _filename;
                _foto.LP = MaxLp;
                _foto.NAZWA = _nazwafoto;
                context.IHP_FOTO.Add(_foto);
                context.Entry(_foto).State = EntityState.Added;
                context.IHP_NUMERACJA.Add(numerfoto);
                context.Entry(numerfoto).State = EntityState.Modified;
                context.SaveChanges();
                ClearF();
                LoadFotos(idKartoeka);
                NewRec = true;


            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                MessageBox.Show(LastMessage);
            }
        }
        private int Maxihp(  List<IHP_FOTO> source)
        {
            if (source == null)  return 0;
            int value = 0;
            foreach (IHP_FOTO item in source)
            {
                if (value == 0 || item.LP > value) value = item.LP ?? 0;
            }
            return value;
        }
        private bool CanDelPhoto()
        {
            if (Foto == null)
                        return false;
             int MaxLp = 0;
            if (context.IHP_FOTO.Any(x => x.ID_IHP_KARTOTEKA == _idkartotekaedit))
            {
               MaxLp = Maxihp(context.IHP_FOTO.Where(x => x.ID_IHP_KARTOTEKA == _idkartotekaedit).ToList());
            }
            if (MaxLp == Foto.LP)
                   return true;
                else
                    return false; 
           }
        private void DelPhoto()
        {
            string LastMessage;
            try
            {
                if (Foto != null)
                {
                    IHP_FOTO dousuniecja = context.IHP_FOTO.Find(Foto.ID_IHP_FOTO);
                    context.Entry(dousuniecja).State = EntityState.Deleted;
                    context.IHP_FOTO.Remove(Foto);
                    context.SaveChanges();
                    LoadFotos(Kartoteka.ID_IHP_KARTOTEKA);
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
                MessageBoxService.ShowMessage("Wystąpił błąd w zapisie do bazy sprawdz Log !!");
                throw;
            }

        }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void SetFocus(int parameter)
        {

            switch (parameter)
            {
                case 1:
                    NazwaJednDodat = string.Empty;
                    break;
                case 2:
                    IloscDodat = _iloscdodat;
                    break;
            }
        }
        #region IDataErrorInfo & Validation Members
        string EnableValidationAndGetError()
        {

            string error = ((IDataErrorInfo)this).Error;
            if (!string.IsNullOrEmpty(error))
            {
                this.RaisePropertiesChanged();
                return error;
            }
            return null;
        }
        string IDataErrorInfo.Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string error =
                    //  me[BindableBase.GetPropertyName(() => NazwaRodz)]
                    me[BindableBase.GetPropertyName(() => NazwaJednDodat)] + System.Environment.NewLine
                   + me[BindableBase.GetPropertyName(() => IloscDodat)] + System.Environment.NewLine;
 
                error = error.Trim();
                if (!string.IsNullOrEmpty(error))
                    return error;
                return null;
            }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {

                string NazwaJednDodatProp = BindableBase.GetPropertyName(() => NazwaJednDodat);
                string IloscDodatProp = BindableBase.GetPropertyName(() => IloscDodat);

                if (columnName == "NazwaJednDodat")
                    return RequiredValidationRuleJednDodat.GetErrorMessage(NazwaJednDodatProp, NazwaJednDodat);
                if (columnName == "IloscDodat")
                    return RequiredValidationRuleJednDodat.GetErrorMessage(IloscDodatProp, IloscDodat);
                else
                    return null;
            }
        }
        #endregion // IDataErrorInfo & Validation Members
    }

    public class RequiredValidationRuleJednDodat : ValidationRule
    {
        public static string GetErrorMessage(string fieldName, object fieldValue, object nullValue = null)
        {
            string errorMessage = string.Empty;

            //            if (nullValue != null && nullValue.Equals(fieldValue))
            //              errorMessage = string.Format("Pole:  {0} jest puste.", fieldName);
            if (fieldName == "NazwaJednDodat")
            {
                if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
                {
                    errorMessage = string.Format("Pole: Nazwa jest puste.");
                }
            }

            if (fieldName == "IloscDodat")
            {
                decimal aa = DataTypeConvert.ToDecimal(fieldValue);
                if (aa < 0.01m)
                {
                    errorMessage = string.Format("Pole: Ilość jest zero.");
                }
            }
           
            return errorMessage;
        }
        public string FieldName { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string error = GetErrorMessage(FieldName, value);
            if (!string.IsNullOrEmpty(error))
                return new ValidationResult(false, error);
            return ValidationResult.ValidResult;
            //           throw new NotImplementedException();
        }
    }

    public class Converter : MarkupExtension, IValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (int)value == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? 1 : 0;
        }
    }
}


