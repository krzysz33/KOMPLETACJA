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
using System.Data.SqlClient;
using System.Data.Entity.Infrastructure;
using DevExpress.Xpf.Docking;
using System.Windows.Controls;
using System.Globalization;
using System.Windows;

namespace KpInfohelp
{

    public class RequiredValidationRuleKontrahnet : ValidationRule, IDataErrorInfo
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

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string error = GetErrorMessage(FieldName, value);
            if (!string.IsNullOrEmpty(error))
                return new ValidationResult(false, error);
            return ValidationResult.ValidResult;
            //           throw new NotImplementedException();
        }

    
    }

    public class ShowHidePanelMessage
    {

    }
    class ViewModelKontrahent : CrudVMBase, INotifyPropertyChanged,IMVVMDockingProperties, IDataErrorInfo
    {

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
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        private IHP_KONTRAHENT _kontrah;
        private IHP_KONTRAHENT_ARCH _kontraharch;
      
 
        private List<IHP_KONTRAHENT> _listakontrah;
        private List<IHP_KONTRAHENT_ARCH> _listakontraharch;
        private string _imie, _nazwa, _email, _telkom, _telefon, _uwagi, _nip;
        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand  AddToSubjectCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand CreateDokCommand { get; private set; }
        public ICommand ExportSubjectCommand { get; private set; }
        public ICommand ImportSubjectCommand { get; private set; }
        public ICommand  DelTrasaCommand { get; private set; }
        public ICommand DodajTraseCommand { get; private set; }
        public ICommand FillDataCommand { get; set; }

        

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public event PropertyChangedEventHandler PropertyChanged;
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
        public ObservableCollection<IHP_KONTRAHENT_ARCH> Kontrahsarch
        {
            get
            {
                return _kontrahsarch;
            }
            set
            {
                _kontrahsarch = value;
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
                RiseRisePropertyChanged("RodzajeDok");
            }
        }
        private ObservableCollection<WystTrasyKartView> _listawysttrasy;
        public ObservableCollection<WystTrasyKartView> ListaWystTrasy
        {
            get
            {
                return _listawysttrasy;
            }
            set
            {
                _listawysttrasy = value;
                RisePropertyChanged("ListaWystTrasy");
            }
        }
        private IHP_TRASY TrasaUpdate ; 

        private List<WystTrasyKartView> ListaWystTrasyL;

        private WystTrasyKartView _trasaselect; 
        public WystTrasyKartView TrasaSelect
        {
            get
            {
                return _trasaselect;
            }
            set
            {
                _trasaselect = value;
                RiseRisePropertyChanged("TrasaSelect");
            }
        }

        public ViewModelKontrahent()
        {
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>(context.IHP_KONTRAHENT);
            DefCenyLst = new ObservableCollection<IHP_DEFCENY>(context.IHP_DEFCENY);
            RodzajeDok = new ObservableCollection<IHP_RODZAJDOK>(context.IHP_RODZAJDOK);
            ListaWystTrasy = new ObservableCollection<WystTrasyKartView>();
            RodzajDok = new IHP_RODZAJDOK();
            DeleteCommand = new RelayCommand(Delete, CanDelete);
            CreateDokCommand = new RelayCommand(WystaDok,CanWystDok);
            AddCommand = new RelayCommand(Save, CanSave);
            ClearCommand = new DelegateCommand(Clear);
            DelTrasaCommand =  new RelayCommand(DeleteWysTrasaKart, CanDelTrasa);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            DodajTraseCommand = new DelegateCommand(pokazokno);
            FillDataCommand = new DelegateCommand(LoadWystColection);
            //  ExportSubjectCommand = new RelayCommand(ExportSubject, CanExportSubject);
            //  ImportSubjectCommand = new DelegateCommand(ImportSubject);

            Messenger.Default.Register<List<IHP_KIEROWCA>>(this, OnMessageKierowca);
            Messenger.Default.Register<IHP_TRASY>(this, OnMessageTrasy);
            LoadCollection();



        }

        public void OnMessageKierowca(List<IHP_KIEROWCA> Name)
        {
            LoadCollection();
        }
        public void OnMessageTrasy(IHP_TRASY Name)
        {
            TrasaUpdate = Name;
            SaveWystTrasa();
        }

        private void ExportSubject()
        {
            //int idKontrahSubject = -1;
            //if (_kontrah!= null)
            //{
            //  SubjectSfera sb = new SubjectSfera();
       
            //  idKontrahSubject = sb.DodajKontrahenta(_kontrah);

            //    if (idKontrahSubject > -1)
            //     {
            //        _kontrah.ID_KH_SUBJECT = idKontrahSubject;
            //        context.IHP_KONTRAHENT.Add(_kontrah);
            //        context.Entry(_kontrah).State = EntityState.Modified;
            //        context.SaveChanges();
            //        Kontrahs.FirstOrDefault(x => x.ID_IHP_KONTRAHENT == _kontrah.ID_IHP_KONTRAHENT).ID_KH_SUBJECT = idKontrahSubject;
            //    }
            //}
        }
       // private void ImportSubject()
       //  {
       //     SubjectSfera sb = new SubjectSfera();
       //     try
       //     {
       //     List<IHP_KONTRAHENT_ARCH> LstKontrah = sb.wyswietlListeKontrah();

       //         foreach(IHP_KONTRAHENT_ARCH kh in LstKontrah)
       //         {
       //             if(!context.IHP_KONTRAHENT_ARCH.Any(x=>x.ID_KH_SUBJECT == kh.ID_KH_SUBJECT))
       //             {
       //                 context.IHP_KONTRAHENT_ARCH.Add(kh);
       //                 context.SaveChanges();
       //                int id = kh.ID_IHP_KONTRAHENT;
       //                     //             Clear();
       //                   //               DetachAll();
       //               //        SentKontrah();
       //                // Kontrahs.Add(kh);
       //             }
       //         }
       //      LoadCollection();
       //   }
       //   catch (DbEntityValidationException e)
       //     {
       //       foreach (var eve in e.EntityValidationErrors)
       //        {
       //          LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
       //            foreach (var ve in eve.ValidationErrors)
       //             {
       //                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
       //            //    MessageBoxService.ShowMessage(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
       //             }
       //         }
       //      throw;
       //   }
       //}
      //private bool CanExportSubject()
      //  {
      //      if (_kontrah == null) return false;

      //      if (_kontrah.ID_KH_SUBJECT != null)
      //          return false;
      //      else
      //          return true;
      //  }
       //  public object RodzajKontrahSel;
        private void WystaDok()
        {
            Messenger.Default.Send<IHP_KONTRAHENT>(_kontrah);
            Messenger.Default.Send<bool>(true);
        }
        private ObservableCollection<IHP_DEFCENY> _defcenylst;
        public ObservableCollection<IHP_DEFCENY> DefCenyLst
        {
            get
            {
                return _defcenylst;
            }
            set
            {
                _defcenylst = value;
                RiseRisePropertyChanged("DefCeny");
            }
       }
        private IHP_DEFCENY _defceny;
        public IHP_DEFCENY Defceny
        {
            get
            {
                return _defceny;
            }
            set
            {
                _defceny = value;
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
                RiseRisePropertyChanged("RodzajDok");
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
                RiseRisePropertyChanged("Stawkavat");
            }
        }
        public IHP_KONTRAHENT Kontrah
        {
            get
            {
                return _kontrah;
            }
            set

            {
                _kontrah = value;
                LoadWystColection();
                RiseRisePropertyChanged("Kontrah");
            }
        }
        public string Nazwa
        {
            get
            {
                return _nazwa;
            }
            set
            {
                _nazwa = value;
           }
        }
        public string Nip
        {
            get
            {
                return _nip;
            }
            set
            {
                _nip = value;


            }
        }
        private string _miejscowosc;
        public string Miejscowosc
        {
            get
            {
                return _miejscowosc;
            }
                
            set
            {
                _miejscowosc = value;
            }
        }
        private string _kodpocztowy;
        public string KodPoczta
        {
            get
            {
                return _kodpocztowy;
            }

            set
            {
                _kodpocztowy = value;
            }
        }
        private string _poczta;
        public string Poczta
        {
            get
            {
                return _poczta;
            }

            set
            {
                _poczta = value;
            }
        }
        private string _ulica;
        public string Ulica
        {
            get
            {
                return _ulica;
            }

            set
            {
                _ulica = value;
            }
        }
        private string _nrdomu;
        public string NrDomu
        {
            get
            {
                return _nrdomu;
            }

            set
            {
                _nrdomu = value;
            }
        }
        private string _nrlokalu;
        public string NrLokalu
        {
            get
            {
                return _nrlokalu;
            }

            set
            {
                _nrlokalu = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;

            }
        }
        public string Telkom
        {
            get
            {
                return _telkom;
            }
            set
            {
                _telkom = value;

            }
        }
        public string Telefon
        {
            get
            {
                return _telefon;
            }

            set
            {
                _telefon = value;
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
                _uwagi = value;
            }
        }
      private bool CanDelete()
         {

            if (_kontrah!=null)
                  return true;
             else
                return false;
         }
     private bool CanWystDok()
        {
            if (_kontrah != null)
                return true;
            else
                return false;
        }
     private bool CanUpdate()
        {
       if (_kontrah!=null)
                  return true;
           else
                return false;

        }
     private bool CanSave()
        {
            //if(!string.IsNullOrEmpty(_nazwa)
            //   && !string.IsNullOrEmpty(_telefon)
            //   && !string.IsNullOrEmpty(_telkom)
            //   && !string.IsNullOrEmpty(_email)
            //   && !string.IsNullOrEmpty(_nip)
            //   && !string.IsNullOrEmpty(_miejscowosc)
            //   && !string.IsNullOrEmpty(_kodpocztowy)
            //  && (_defceny != null) )
            //    return true;
            //else
            //    return false;
            return true;
        }
     
     public void LoadCollection()
        {
            Kontrahs.Clear();
            _listakontrah = new List<IHP_KONTRAHENT>();
            _listakontrah = context.IHP_KONTRAHENT.ToList();

            foreach (IHP_KONTRAHENT item in _listakontrah)
            {
                Kontrahs.Add(item);
            }
       }
     private IHP_KONTRAHENT PreparateKontrah()
        {
            int defcena = 1;
            int defdokumnet = 1;
            string LastMessage;
            if (_defceny != null)
                defcena = _defceny.ID_IHP_DEFCENY;

            if(_rodzajdok!=null)
                  defdokumnet = _rodzajdok.ID_IHP_RODZAJDOK;
            try
            {
                IHP_NUMERACJA numerkh = GetId(2);
            if (numerkh != null)
                numerkh.NUMER++;
        
              IHP_KONTRAHENT kontrah = new IHP_KONTRAHENT()
                {
                    ID_IHP_KONTRAHENT = numerkh.NUMER,
                    ID_IHP_DEFCENY = defcena,
                    TYPKONTRAH = 1,
                    NAZWA = _nazwa,
                    NRKONTRAH = 0,
                    TELEFON = _telefon,
                    TELKOM = _telkom,
                    EMAIL = _email,
                    NIP = _nip,
                    MIEJSCOWOSC = _miejscowosc,
                    ULICA = _ulica,
                    KODPOCZTOWY = _kodpocztowy,
                    NRDOMU = _nrdomu,
                    NRLOKALU = _nrlokalu,
                    POCZTA = _poczta
                };
                context.IHP_NUMERACJA.Add(numerkh);
                context.Entry(numerkh).State = EntityState.Modified;
                return kontrah;
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
                    me[BindableBase.GetPropertyName(() => Nazwa)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => Nip)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => Email)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => Telkom)];
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

                string NazwaProp = BindableBase.GetPropertyName(() => Nazwa);
                string NipProp = BindableBase.GetPropertyName(() => Nip);
                string EmailProp = BindableBase.GetPropertyName(() => Email);
                string TelkomProp = BindableBase.GetPropertyName(() => Telkom);

                if (columnName == NazwaProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(NazwaProp, Nazwa);
                if (columnName == NipProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(NipProp, Nip);
                if (columnName == EmailProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(EmailProp, Email);
                if (columnName == TelkomProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(TelkomProp, Telkom);
                else
                    return null;
            }
        }



        private void Save()
        {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                MessageBox.Show("Błąd " + error, "Dodawanie grupy kartotekowej", MessageBoxButton.OK);
                return;
            }

            int defcena = 1;
            int defdokumnet = 1;
            string LastMessage;
            if (_defceny != null)
                defcena = _defceny.ID_IHP_DEFCENY;

            if (_rodzajdok != null)
                defdokumnet = _rodzajdok.ID_IHP_RODZAJDOK;

            IHP_NUMERACJA numerkharch = GetId(6);
             if (numerkharch != null)
                numerkharch.NUMER++;

          try
            {
                IHP_KONTRAHENT_ARCH kontraharch = new IHP_KONTRAHENT_ARCH()
                {
                    ID_IHP_KONTRAHENT_ARCH = numerkharch.NUMER,
                    ID_IHP_DEFCENY = defcena,
                    ID_IHP_RODZAJDOKDEF = defdokumnet,
                    TYPKONTRAH = 1,
                    NAZWA = _nazwa,
                    NRKONTRAH = 0,
                    TELEFON = _telefon,
                    TELKOM = _telkom,
                    EMAIL = _email,
                    NIP = _nip,
                    MIEJSCOWOSC = _miejscowosc,
                    ULICA = _ulica,
                    KODPOCZTOWY = _kodpocztowy,
                    NRDOMU = _nrdomu,
                    NRLOKALU = _nrlokalu,
                    POCZTA = _poczta,
                    AKTYWNY=1,
                    IHP_KONTRAHENT = PreparateKontrah()
                };
                context.IHP_KONTRAHENT_ARCH.Add(kontraharch);
                context.SaveChanges();
                Clear();
                LoadCollection();
                SentKontraharch();
         
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
     public void DetachAll()
        {
            //       context.Entry(Nagl).State = EntityState.Detached;

            var entries = ((DbContext)context).ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }

        }
     private void Delete()
        {
            string LastMessage;
            try
            {
               if(Kontrah != null)
                {
              
                  List<IHP_KONTRAHENT_ARCH> IHP_KONTRAHS_ARCH = context.IHP_KONTRAHENT_ARCH.Where(x => x.ID_IHP_KONTRAHENT.Equals(_kontrah.ID_IHP_KONTRAHENT)).ToList();


                    if (context.IHP_NAGLDOK.Any(o => o.ID_IHP_KONTRAHENT== Kontrah.ID_IHP_KONTRAHENT))

                    {

                        MessageBoxService.Show("Kontrahent wykorzystany w dokumencie!");
                        return;
                    }
                    //if (context.IHP_WAZENIE.Any(o => o.ID_IHP_KONTRAHENT == Kontrah.ID_IHP_KONTRAHENT))

                    //{

                    //    MessageBoxService.Show("Kontrahent wykorzystany w ważeniu!");
                    //    return;
                    //}
                    context.IHP_KONTRAHENT_ARCH.RemoveRange(IHP_KONTRAHS_ARCH);
                    context.IHP_KONTRAHENT.Remove(_kontrah);
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentKontraharch();

                } 
           }
            catch (DbUpdateException Ex)
            {
                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("DbUpdateException \"{0}\"  :", Ex.InnerException.Message));
                throw Ex;
            }
            catch (SqlException exc)
            {
                //here you might still get some exceptions but not about validation.

                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("SqlException \"{0}\"  :", exc.Message));


                //sometimes you may want to throw the exception to upper layers for handle it better over there!
                throw;
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
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }

        }
     public void  SentKontrah()
        {
            Messenger.Default.Send<List<IHP_KONTRAHENT>>(_listakontrah);
        }
     public void SentKontraharch()
        {
            Messenger.Default.Send<int>(1);
        }
     private void Clear()
        {
            if (_kontrah != null)
            {
                Kontrah = null;
                _kontrah = null;
            }

        }
     private void UpdateAndSave()
        {
            int defcena = 1;
            int defdokumnet = 1;
            string LastMessage;
            if (_defceny != null)
                defcena = _defceny.ID_IHP_DEFCENY;
            if (_rodzajdok != null)
                defdokumnet = _rodzajdok.ID_IHP_RODZAJDOK;
            IHP_NUMERACJA numerkharch = GetId(6);
            if (numerkharch != null)
                numerkharch.NUMER++;

            try
            {
                IHP_KONTRAHENT_ARCH kontraharch = new IHP_KONTRAHENT_ARCH()
                {
                    ID_IHP_KONTRAHENT_ARCH = numerkharch.NUMER,
                    ID_IHP_KONTRAHENT = _kontrah.ID_IHP_KONTRAHENT,
                    ID_IHP_DEFCENY = defcena,
                    ID_IHP_RODZAJDOKDEF = defdokumnet,
                    TYPKONTRAH = 1,
                    NAZWA = _nazwa,
                    NRKONTRAH = 0,
                    TELEFON = _telefon,
                    TELKOM = _telkom,
                    EMAIL = _email,
                    NIP = _nip,
                    MIEJSCOWOSC = _miejscowosc,
                    ULICA = _ulica,
                    KODPOCZTOWY = _kodpocztowy,
                    NRDOMU = _nrdomu,
                    NRLOKALU = _nrlokalu,
                    POCZTA = _poczta,
                    AKTYWNY = 1,
                    IHP_KONTRAHENT = _kontrah
                };
                context.IHP_KONTRAHENT_ARCH.Add(kontraharch);
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
     private void Update()
        {
            UpdateKh();
            UpdatekontrahArch();
             UpdateAndSave();
               Clear();
               LoadCollection();
            SentKontraharch();
        }
     private void UpdateKh()
        {
            string LastMessage;
            try
            {
                if (_kontrah != null)
                {
                    _kontrah.NAZWA = _nazwa;
                    _kontrah.TELEFON = _telefon;
                    _kontrah.TELKOM = _telkom;
                    _kontrah.EMAIL = _email;
                    _kontrah.NIP = _nip;
                    _kontrah.MIEJSCOWOSC = _miejscowosc;
                    _kontrah.ULICA = _ulica;
                    _kontrah.KODPOCZTOWY = _kodpocztowy;
                    _kontrah.NRDOMU = _nrdomu;
                    _kontrah.NRLOKALU = _nrlokalu;
                    _kontrah.POCZTA = _poczta;
                    context.IHP_KONTRAHENT.Attach(_kontrah);
                    context.Entry(_kontrah).State = EntityState.Modified;
                    context.SaveChanges();
                  SentKontrah();
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
     private void UpdatekontrahArch()
        {
            if (_kontrah != null)
            {
                IHP_KONTRAHENT_ARCH khtarch = context.IHP_KONTRAHENT_ARCH.FirstOrDefault(x => x.ID_IHP_KONTRAHENT.Equals(_kontrah.ID_IHP_KONTRAHENT) && (x.AKTYWNY == 1));

                if (khtarch!=null)
                {
                    khtarch.AKTYWNY = 0;
                    context.IHP_KONTRAHENT_ARCH.Attach(khtarch);
                    context.Entry(khtarch).State = EntityState.Modified;
                    context.SaveChanges();
                }


            }
        }

   private void AddSubject()
        {
      //      SubjectSfera sb = new SubjectSfera();
     //        sb.DodajKontrahenta();
 
       }
     protected void RiseRisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        void pokazokno()
        {
            var w = new TrasyPodpowiedz();
            w.ShowDialog();
        }

        private bool  CanDelTrasa()
        {
            return (TrasaSelect != null);
        }
        private void LoadWystColection()
        {
            if (Kontrah == null) return;
            ListaWystTrasy.Clear();
            var _listatrasy = context.Database.SqlQuery<WystTrasyKartView>(@" select WK.ID_IHP_WYSTTRASAKONTRAH , Wk.ID_IHP_KONTRAHENT, WK.ID_IHP_TRASY,T.NAZWA  from IHP_TRASY T
                                                                              join IHP_WYSTTRASAKONTRAH WK on T.ID_IHP_TRASY = WK.ID_IHP_TRASY AND  WK.ID_IHP_KONTRAHENT =" + Kontrah.ID_IHP_KONTRAHENT.ToString()).ToList();
            foreach (WystTrasyKartView item2 in _listatrasy)
            {
                ListaWystTrasy.Add(item2);
            }
        }


        private void LoadPoz()
        {

        }


        private void SaveWystTrasa()
        {
            string LastMessage;
            try
            {
                if (Kontrah != null && TrasaUpdate != null)
                {

                    IHP_WYSTTRASAKONTRAH wtk = new IHP_WYSTTRASAKONTRAH();
                    wtk.ID_IHP_WYSTTRASAKONTRAH = GetNextNumer(19);
                    wtk.ID_IHP_KONTRAHENT = Kontrah.ID_IHP_KONTRAHENT;
                    wtk.ID_IHP_TRASY = TrasaUpdate.ID_IHP_TRASY;
                    context.IHP_WYSTTRASAKONTRAH.Add(wtk);
                    context.SaveChanges();
                    LoadWystColection();
                    TrasaUpdate = null;
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

    
        private void DeleteWysTrasaKart()
        {
            string LastMessage;
            try
            {
                if (TrasaSelect != null)
                {

                    //    IHP_WYSTTRASAKART wtk = context.IHP_WYSTTRASAKART?.FirstOrDefault(x => x.ID_IHP_KONTRAHENT.Equals(TrasaSelect.ID_IHP_KONTRAHENT) &&  x.ID_IHP_TRASY.Equals(TrasaSelect.ID_IHP_TRASY));
                    //if(wtk!=null)
                    //    {
                    //       context.IHP_WYSTTRASAKART.Remove(wtk);
                    //       context.SaveChanges();
                    //        Clear();
                    //        LoadWystColection();
                    //        TrasaUpdate = null;
                    //    }
                    IHP_WYSTTRASAKONTRAH wtk =   context.IHP_WYSTTRASAKONTRAH.Find(TrasaSelect.ID_IHP_WYSTTRASAKONTRAH);
                    context.Entry(wtk).State = EntityState.Deleted;
                    context.IHP_WYSTTRASAKONTRAH.Remove(wtk);
                    context.SaveChanges();
                         LoadWystColection();
                        TrasaUpdate = null;
                }
            }
            catch (DbUpdateException Ex)
            {
                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("DbUpdateException \"{0}\"  :", Ex.InnerException.Message));
                throw Ex;
            }
            catch (SqlException exc)
            {
                //here you might still get some exceptions but not about validation.

                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("SqlException \"{0}\"  :", exc.Message));


                //sometimes you may want to throw the exception to upper layers for handle it better over there!
                throw;
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


}
 


