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
using DevExpress.Mvvm.DataAnnotations;
using System.Windows.Controls;
using System.Globalization;
using System.Windows;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    public class RequiredValidationRule : ValidationRule
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

    [POCOViewModel]
    class ViewModelDaneFirmy : CrudVMBase, INotifyPropertyChanged, IDataErrorInfo, IMVVMDockingProperties
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
                    me[BindableBase.GetPropertyName(() => NazwaPelna)] +
                    me[BindableBase.GetPropertyName(() => Nazwa)] +
                    me[BindableBase.GetPropertyName(() => Nip)] +
                    me[BindableBase.GetPropertyName(() => Regon)] +
                    me[BindableBase.GetPropertyName(() => Miejscowosc)] +
                    me[BindableBase.GetPropertyName(() => KodPoczta)] +
                    me[BindableBase.GetPropertyName(() => Poczta)]+
                    me[BindableBase.GetPropertyName(() => Ulica)] +
                    me[BindableBase.GetPropertyName(() => NrDomu)] +
                    me[BindableBase.GetPropertyName(() => NrLokalu)] +
                    me[BindableBase.GetPropertyName(() => Telkom)] +
                    me[BindableBase.GetPropertyName(() => Telefon)] +
                    me[BindableBase.GetPropertyName(() => Uwagi)] +
                    me[BindableBase.GetPropertyName(() => Email)];
                if (!string.IsNullOrEmpty(error))
                    return "Sprawdz dane";
                return null;
            }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string NazwaPelnaProp = BindableBase.GetPropertyName(() => NazwaPelna);
                string NazwaProp = BindableBase.GetPropertyName(() => Nazwa);
                string NipProp = BindableBase.GetPropertyName(() => Nip);
                string RegonProp = BindableBase.GetPropertyName(() => Regon);
                string MiejscowoscProp = BindableBase.GetPropertyName(() => Miejscowosc);
                string KodPocztaProp = BindableBase.GetPropertyName(() => KodPoczta);
                string PocztaProp = BindableBase.GetPropertyName(() => Poczta);

                string UlicaProp = BindableBase.GetPropertyName(() => Ulica);
                string NrDomuProp = BindableBase.GetPropertyName(() =>NrDomu);
                string NrLokaluProp = BindableBase.GetPropertyName(() => NrLokalu);
                string TelkomProp = BindableBase.GetPropertyName(() => Telkom);
                string TelefonProp = BindableBase.GetPropertyName(() => Telefon);
                string UwagiProp = BindableBase.GetPropertyName(() => Uwagi);
                string EmailProp = BindableBase.GetPropertyName(() => Email);
                if (columnName == NazwaPelnaProp)
                    return RequiredValidationRule.GetErrorMessage(NazwaPelnaProp, NazwaPelna);
                else if (columnName == NazwaProp)
                    return RequiredValidationRule.GetErrorMessage(NazwaProp, Nazwa);
                else if (columnName == NipProp)
                    return RequiredValidationRule.GetErrorMessage(NipProp, Nip);
                else if (columnName == RegonProp)
                    return RequiredValidationRule.GetErrorMessage(RegonProp, Regon);
                //else if (columnName == confirmPasswordProp)
                //{
                //    if (!string.IsNullOrEmpty(Password) && Password != ConfirmPassword)
                //        return "These passwords do not match. Please try again.";
                //}
                else if (columnName == MiejscowoscProp)
                    return RequiredValidationRule.GetErrorMessage(MiejscowoscProp, Miejscowosc);
                else if (columnName == KodPocztaProp)
                    return RequiredValidationRule.GetErrorMessage(KodPocztaProp, KodPoczta);
                else if (columnName == PocztaProp)
                    return RequiredValidationRule.GetErrorMessage(PocztaProp, Poczta);
                else if (columnName == UlicaProp)
                    return RequiredValidationRule.GetErrorMessage(UlicaProp, Ulica);
                else if (columnName == NrDomuProp)
                    return RequiredValidationRule.GetErrorMessage(NrDomuProp,NrDomu);
                else if (columnName == NrLokaluProp)
                    return RequiredValidationRule.GetErrorMessage(NrLokaluProp, NrLokalu);
                else if (columnName == EmailProp)
                    return RequiredValidationRule.GetErrorMessage(EmailProp, Email);
                else if (columnName == TelkomProp)
                    return RequiredValidationRule.GetErrorMessage(TelkomProp, Telkom);
                else if (columnName == TelefonProp)
                    return RequiredValidationRule.GetErrorMessage(TelefonProp, Telefon);
           //     else if (columnName == UwagiProp)
             //       return RequiredValidationRule.GetErrorMessage(UwagiProp, Kontrah.WWW);

                return null;
            }
        }
        protected void OnRisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private IHP_DANEFIRMY _kontrah;
        private List<IHP_DANEFIRMY> _listakontrah;
        private string  _nazwa, _nazwapelna, _email, _telkom, _telefon, _uwagi, _nip, _regon;
        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddNewProgCommand { get; private set; }
        public ICommand  AddToSubjectCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand CreateDokCommand { get; private set; }
        public ICommand ExportSubjectCommand { get; private set; }
        public ICommand ImportSubjectCommand { get; private set; }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<IHP_DANEFIRMY> _kontrahs;
        public ObservableCollection<IHP_DANEFIRMY> Kontrahs
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
        private ObservableCollection<IHP_DANEFIRMY> _kontrahsarch;
        public ObservableCollection<IHP_DANEFIRMY> Kontrahsarch
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
 
     public ViewModelDaneFirmy()
        {
               Kontrahs = new ObservableCollection<IHP_DANEFIRMY>(context.IHP_DANEFIRMY);
                DeleteCommand = new RelayCommand(Delete, CanDelete);
                AddNewProgCommand = new RelayCommand(ZapiszDane, CanSave);
                ClearCommand = new DelegateCommand(Clear);
                UpdateCommand = new RelayCommand(Update, CanUpdate);
                LoadCollection();
            DateOd = DateTime.Today;
           //SentKontrah();
       }
     private void ImportSubject()
         {
            //SubjectSfera sb = new SubjectSfera();
            //try
            //{
            //List<IHP_KONTRAHENT_ARCH> LstKontrah = sb.wyswietlListeKontrah();

            //    foreach(IHP_KONTRAHENT_ARCH kh in LstKontrah)
            //    {
            //        if(!context.IHP_KONTRAHENT_ARCH.Any(x=>x.ID_KH_SUBJECT == kh.ID_KH_SUBJECT))
            //        {
            //            context.IHP_KONTRAHENT_ARCH.Add(kh);
            //            context.SaveChanges();
            //           int id = kh.ID_IHP_KONTRAHENT;
            //                //             Clear();
            //              //               DetachAll();
            //          //        SentKontrah();
            //           // Kontrahs.Add(kh);
            //        }
            //    }
            // LoadCollection();
          //}
          //catch (DbEntityValidationException e)
          //  {
          //    foreach (var eve in e.EntityValidationErrors)
          //     {
          //       LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
          //         foreach (var ve in eve.ValidationErrors)
          //          {
          //             LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
          //         //    MessageBoxService.ShowMessage(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
          //          }
          //      }
          //   throw;
          //}
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
                OnRisePropertyChanged("Stawkavat");
            }
        }
     public IHP_DANEFIRMY Kontrah
        {
            get
            {
                return _kontrah;
            }
            set

            {
                _kontrah = value;
                if(_kontrah!=null)
                          FilData();
                OnRisePropertyChanged("Kontrah");
            }
        }
     public string NazwaPelna
        {
            get
            {
                return _nazwapelna;
            }
            set
            {
                _nazwapelna = value;
                OnRisePropertyChanged("NazwaPelna");
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
                OnRisePropertyChanged("Nazwa");
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
                OnRisePropertyChanged("Nip");
            }
        }
     public string Regon
        {
            get
            {
                return _regon;
            }
            set
            {
                _regon = value;
                OnRisePropertyChanged("Regon");
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
                OnRisePropertyChanged("Miejscowosc");
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
                OnRisePropertyChanged("KodPoczta");
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
                OnRisePropertyChanged("Poczta");
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
                OnRisePropertyChanged("Ulica");
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
                OnRisePropertyChanged("NrDomu");
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
                OnRisePropertyChanged("NrLokalu");
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
                OnRisePropertyChanged("Email");
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
                OnRisePropertyChanged("Telkom");
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
                OnRisePropertyChanged("Telefon");
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
                OnRisePropertyChanged("DateOd");
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

            string error = EnableValidationAndGetError();


            if (error != null)
                return false;
               else 
                return true;






            //    if (!string.IsNullOrEmpty(_nazwa)
            //   && !string.IsNullOrEmpty(_telefon)
            //   && !string.IsNullOrEmpty(_telkom)
            //   && !string.IsNullOrEmpty(_email)
            //   && !string.IsNullOrEmpty(_nip)
            //   && !string.IsNullOrEmpty(_miejscowosc)
            //   && !string.IsNullOrEmpty(_kodpocztowy)
            //   )
            //    return true;
            //else
            //    return false;
        }
     public void LoadCollection()
        {
            Kontrahs.Clear();
            _listakontrah = new List<IHP_DANEFIRMY>();
            _listakontrah = context.IHP_DANEFIRMY.ToList();
            foreach (IHP_DANEFIRMY item in _listakontrah)
            {
                Kontrahs.Add(item);
            }
       }
     private void FilData()
        {
            if (Kontrah != null)
            {
                DateOd = Kontrah.ODDATY;
                NazwaPelna = Kontrah.NAZWA_FIRMY;
                Nazwa = Kontrah.NAZWA_SKROCONA;
                Nip = Kontrah.NIP;
                Regon = Kontrah.REGON;
                Miejscowosc = Kontrah.MIEJSCOWOSC;
                KodPoczta = Kontrah.KODPOCZTOWY;
                Poczta = Kontrah.POCZTA;
                Ulica = Kontrah.ULICA;
                NrDomu = Kontrah.NRDOMU;
                NrLokalu = Kontrah.NRLOKALU;
                Email = Kontrah.EMAIL;
                Telkom = Kontrah.TELEFON;
                Telefon = Kontrah.TELEFON2;
            }
        }
     private void FilDataKontrah()
        {
            if (Kontrah != null)
            {
                Kontrah.NAZWA_FIRMY = NazwaPelna;
                Kontrah.NAZWA_SKROCONA = Nazwa;
                Kontrah.NIP = Nip;
                Kontrah.REGON = Regon;
                Kontrah.MIEJSCOWOSC = Miejscowosc;
                Kontrah.KODPOCZTOWY = KodPoczta;
                Kontrah.POCZTA = Poczta;
                Kontrah.ULICA = Ulica;
                Kontrah.NRDOMU = NrDomu;
                Kontrah.NRLOKALU = NrLokalu;
                Kontrah.EMAIL = Email;
                Kontrah.TELEFON = Telkom;
                Kontrah.TELEFON2 = Telefon;
            }
        }
        private void ZapiszDane()
        {
            if ((Kontrahs.Count > 0) && (Kontrah!= null))
                Update();
            else if ((Kontrahs.Count == 0) && (Kontrah == null))
                         Save();
        }
        private void Save()
        {
            string LastMessage;
            IHP_NUMERACJA numerkharch = GetId(10);
            if (numerkharch != null)
                numerkharch.NUMER++;
            try
            {
                try
                {
                    IHP_DANEFIRMY Kontrah = new IHP_DANEFIRMY();
                    Kontrah.ODDATY = DateOd;
                    Kontrah.NAZWA_FIRMY = NazwaPelna;
                    Kontrah.NAZWA_SKROCONA = Nazwa;
                    Kontrah.NIP = Nip;
                    Kontrah.REGON = Regon;
                    Kontrah.MIEJSCOWOSC = Miejscowosc;
                    Kontrah.KODPOCZTOWY = KodPoczta;
                    Kontrah.POCZTA = Poczta;
                    Kontrah.ULICA = Ulica;
                    Kontrah.NRDOMU = NrDomu;
                    Kontrah.NRLOKALU = NrLokalu;
                    Kontrah.EMAIL = Email;
                    Kontrah.TELEFON = Telkom;
                    Kontrah.TELEFON2 = Telefon;
                    Kontrah.ID_IHP_DANEFIRMY = numerkharch.NUMER;
                    context.IHP_DANEFIRMY.Add(Kontrah);
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                            //    MessageBoxService.ShowMessage(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                        }
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
     private void Delete()
        {
            string LastMessage;
            try
            {
                MessageBoxResult result = MessageBox.Show("Czy napewno usunąć dane firmy ?", "Potwierdź Usunięcie", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {

                    if (Kontrah != null)
                    {
                        context.IHP_DANEFIRMY.Remove(_kontrah);
                        context.SaveChanges();
                        Clear();
                        LoadCollection();
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
     public void DetachAll()
        {
            //       context.Entry(Nagl).State = EntityState.Detached;

            var entries = ((DbContext)context).ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                entry.State = EntityState.Detached;
            }
        }
     private void Clear()
        {
            if (_kontrah != null)
            {
                Kontrah = null;
                _kontrah = null;
            }
            NazwaPelna = String.Empty;
            Nazwa = String.Empty;
            Nip = String.Empty;
            Regon = String.Empty;
            Miejscowosc = String.Empty;
            KodPoczta = String.Empty;
            Poczta = String.Empty;
            Ulica = String.Empty;
            NrDomu = String.Empty;
            NrLokalu = String.Empty;
            Email = String.Empty;
            Telkom = String.Empty;
            Telefon = String.Empty;
        }
     private void UpdateAndSave()
        {
            string LastMessage;
            try
            {
                if (_kontrah != null)
                {
                    _kontrah.ODDATY = _dateod;
                    _kontrah.NAZWA_FIRMY = _nazwapelna;
                    _kontrah.NAZWA_SKROCONA = _nazwa;
                    _kontrah.TELEFON = _telefon;
                    _kontrah.TELEFON2 = _telkom;
                    _kontrah.EMAIL = _email;
                    _kontrah.NIP = _nip;
                    _kontrah.REGON = _regon;
                    _kontrah.MIEJSCOWOSC = _miejscowosc;
                    _kontrah.ULICA = _ulica;
                    _kontrah.KODPOCZTOWY = _kodpocztowy;
                    _kontrah.NRDOMU = _nrdomu;
                    _kontrah.NRLOKALU = _nrlokalu;
                    _kontrah.POCZTA = _poczta;
                };
                context.IHP_DANEFIRMY.Attach(_kontrah);
                context.Entry(_kontrah).State = EntityState.Modified;
                context.SaveChanges();
             
            }
            catch (Exception e)
            {
               LogManager.WriteLogMessage(LogManager.LogType.Error, e.Message.ToString());
                throw;
            }
        }
     private void Update()
        {
          UpdateAndSave();
          Clear();
          LoadCollection();
         }
      private void AddSubject()
        {
      //      SubjectSfera sb = new SubjectSfera();
     //        sb.DodajKontrahenta();
 
       }
   }
}
 


