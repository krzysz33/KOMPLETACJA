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
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    public class RequiredValidationRuleErpConn : ValidationRule
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
    public class YesNoToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (System.Convert.ToInt32(value) == 1)
                return true;
            else if (System.Convert.ToInt32(value) == 0)
                return false;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return 1;
                else
                    return 0;
            }
            return 0;
        }
    }

    class ViewModelERPConnector : CrudVMBase, INotifyPropertyChanged, IDataErrorInfo, IMVVMDockingProperties
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
                    me[BindableBase.GetPropertyName(() => Serwer)];
  
                if (!string.IsNullOrEmpty(error))
                    return "Sprawdz dane";
                return null;
            }
        }

        private string _idgrkart;
        public string IdGrKart
        {
          get
            {
                return  _idgrkart;
            }
            set
            {
                _idgrkart = value;
                OnRisePropertyChanged("IdGrKart");
            }

        }

        private string _idgrkartrodzaj;
        public string IdGrKartRodz
        {
            get
            {
                return _idgrkartrodzaj;
            }
            set
            {
                _idgrkartrodzaj = value;
                OnRisePropertyChanged("IdGrKartRodz");
            }
        }
        private bool _kartall;

        private string _idgrupakontrah;
        public string IdGrKontrah
        {
            get
            {
                return _idgrupakontrah;
            }
            set
            {
                _idgrupakontrah = value;
                OnRisePropertyChanged("IdGrKontrah");
            }
}

        public bool KartAll
        {
            get
            {
                return _kartall;
            }
            set
            {
                _kartall = value;
                OnRisePropertyChanged("KartAll");
            }
        }

        private bool _kartrodz;
        public bool KartRodz
        {
            get
            {
                return _kartrodz;
            }
         set
            {
                _kartrodz = value;
                OnRisePropertyChanged("KartRodz");
            }
        }
        private bool _kartgrupa;
        public bool KartGrupa
        {
            get
            {
                return _kartgrupa;
            }
            set
            {
                _kartgrupa = value;
                OnRisePropertyChanged("KartGrupa");
            }
        }
        private bool _kartcenniki;
        public bool KartCenniki
        {
            get
            {
                return _kartcenniki;
            }
            set
            {
                _kartcenniki = value;
                OnRisePropertyChanged("KartCenniki");
            }
        }
        private bool _kontrahall;
        public bool KontrahAll
        {
            get
            {
                return _kontrahall;
            }
            set
            {
                _kontrahall = value;
                OnRisePropertyChanged("KontrahAll");
            }
        }
        private bool _kontrahgrupa;
        public bool KontrahGrupa
        {
            get
            {
                return _kontrahgrupa;
            }
            set
            {
                _kontrahgrupa = value;
                OnRisePropertyChanged("KontrahGrupa");
            }
        }
        private bool _dockdirect;
        public bool DockDirect
        {
            get
            {
                return _dockdirect;
            }
            set
            {
                _dockdirect = value;
                OnRisePropertyChanged("DockDirect");
            }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string SerwerProp = BindableBase.GetPropertyName(() => Serwer);

                if (columnName == SerwerProp)
                    return RequiredValidationRule.GetErrorMessage(SerwerProp, Serwer);
                else
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
 
         public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddNewProgCommand { get; private set; }
        public ICommand  AddToSubjectCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand CreateDokCommand { get; private set; }
        public ICommand ExportSubjectCommand { get; private set; }
        public ICommand ImportSubjectCommand { get; private set; }
        public ICommand ItemSelERPCommand { get; set; }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public event PropertyChangedEventHandler PropertyChanged;

        private IHP_ERPCONNECTOR _erpconnect;
        public IHP_ERPCONNECTOR  ErpConnect
        {
            get
            {
               return  _erpconnect;
            }
            set
            {
                _erpconnect = value;
                if (_erpconnect != null)
                                FilData();
                   OnRisePropertyChanged("ErpConnect");
            }
        }

        private string _serwer;
        public string Serwer
        {
            get
            {
                return _serwer;
            }
            set
            {
                _serwer = value;
                RisePropertyChanged("Serwer");
            }

        }
        private string _katalogbazy;
        public string KatalogBazy
        {
            get
            {
                return _katalogbazy;
            }
            set
            {
                _katalogbazy = value;
                RisePropertyChanged("KatalogBazy");
            }
        }

        private string _haslo;
        public string Haslo
        {
            get
            {
                return _haslo;
            }
            set
            {
                _haslo = value;
                RisePropertyChanged("Haslo");
            }
        }

        private string _uzytkownik;
        public string Uzytkownik
        {
            get
            {
                return _uzytkownik;
            }
            set
            {
                _uzytkownik = value;
                RisePropertyChanged("Uzytkownik");
            }
        }

        private ObservableCollection<IHP_ERPCONNECTOR> _erpconnects;
        public ObservableCollection<IHP_ERPCONNECTOR> ErpConnects
        {
            get
            {
                return _erpconnects;
            }
            set
            {
                _erpconnects = value;
                RisePropertyChanged("ErpConnects");
            }
        }

        private List<DokDoWyst> _dokdowystlst;
        public List<DokDoWyst> DokDoWystLst
        {
            get
            {
                return _dokdowystlst;
            }
            set
            {
                _dokdowystlst = value;
                RisePropertyChanged("DokDoWystLst");
            }
        }
        private DokDoWyst _dokdowyst;
        public DokDoWyst DokDoWyst
        {
            get
            {
                return _dokdowyst;
            }

            set
            {
                _dokdowyst = value;
                OnRisePropertyChanged("DokDoWyst");
            }
        }


        public ViewModelERPConnector()
         {
             ErpConnects = new ObservableCollection<IHP_ERPCONNECTOR>(context.IHP_ERPCONNECTOR);
              ClearCommand = new DelegateCommand(Clear);
              UpdateCommand = new RelayCommand(Update, CanUpdate);
              ItemSelERPCommand = new DelegateCommand(Search); 
              DateOd = DateTime.Today;
            DokDoWystLst = new List<DokDoWyst>();
            DokDoWystLst.Add(new DokDoWyst() { Nazwa = "Faktura", Id = 1 });
            DokDoWystLst.Add(new DokDoWyst() { Nazwa = "Wydanie Magazynowe", Id = 2 });
            DokDoWystLst.Add(new DokDoWyst() { Nazwa = "Wg Listy Wyboru", Id = 3 });

        }
        private void Search()
        {
          if (_erpconnects == null) return;
            ErpConnect.SERWER = Serwer;
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
     
                return false;
         }
    private bool CanWystDok()
        {
 
                return false;
        }
    private bool CanUpdate()
        {
 
                return true;

        }
     private bool CanSave()
        {
            string error = EnableValidationAndGetError();

           if (error != null)
                return false;
               else 
                return true;
        }
     public void LoadCollection()
        {
       
       }
     private void FilData()
        {
            if (ErpConnect != null)
            {
                Serwer = ErpConnect.SERWER;

                if (ErpConnect.KARTWSZYSTKO == 1)
                    KartAll = true;
                else
                    KartAll = false;

                if (ErpConnect.KARTRODZ == 1)
                   KartRodz = true;
                    else
                      KartRodz = false;
       
                if (ErpConnect.KARTGRUPA == 1)
                    KartGrupa = true;
                     else     
                    KartGrupa = false;
                if (ErpConnect.KONTRAHWSZYSTKO == 1)
                    KontrahAll = true;
                else
                    KontrahAll = false;

                if (ErpConnect.KONTRAHGRUPA == 1)
                    KontrahGrupa = true;
                else
                    KontrahGrupa = false;

                if (ErpConnect.KARTCENNIKI == 1)
                    KartCenniki = true;
                 else
                    KartCenniki = false;

                if (ErpConnect.DOKBEZPOSR == 1)
                    DockDirect = true;
                else
                    DockDirect = false;

                IdGrKartRodz = ErpConnect.KARTIDRODZ;
                IdGrKart = ErpConnect.KARTIDGRUPA;
                IdGrKontrah = ErpConnect.KONTRAHIDGRUPA;

                if (_erpconnect.ID_DEFDOK > 0)
                          DokDoWyst = DokDoWystLst.FirstOrDefault(x => x.Id == _erpconnect.ID_DEFDOK);
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
 
        }
     private void UpdateAndSave()
        {
            string LastMessage;
            try
            {
                if (_erpconnect != null)
                {
                    _erpconnect.SERWER = _serwer;
                    _erpconnect.BAZA = _katalogbazy;
                    _erpconnect.UZYTKOWNIK = _uzytkownik;
                    _erpconnect.HASLO = _haslo;

                    _erpconnect.KARTIDGRUPA = IdGrKart;
                      _erpconnect.KARTIDRODZ = IdGrKartRodz;
                    _erpconnect.KONTRAHIDGRUPA = IdGrKontrah;


                    if (KartRodz)
                        _erpconnect.KARTRODZ = 1;
                    else
                        _erpconnect.KARTRODZ = 0;

                    if (KartGrupa)
                        _erpconnect.KARTGRUPA = 1;
                    else
                        _erpconnect.KARTGRUPA = 0;

                    if (KartAll)
                        _erpconnect.KARTWSZYSTKO = 1;
                    else
                        _erpconnect.KARTWSZYSTKO = 0;

                    if (KontrahAll)
                        _erpconnect.KONTRAHWSZYSTKO = 1;
                    else
                        _erpconnect.KONTRAHWSZYSTKO = 0;

                    if (KontrahGrupa)
                        _erpconnect.KONTRAHGRUPA = 1;
                    else
                        _erpconnect.KONTRAHGRUPA = 0;

                    if (KartCenniki)
                        _erpconnect.KARTCENNIKI = 1;
                    else
                        _erpconnect.KARTCENNIKI = 0;

                    if (DockDirect)
                        _erpconnect.DOKBEZPOSR = 1;
                    else
                        _erpconnect.DOKBEZPOSR = 0;
                    if (DokDoWyst != null)
                        _erpconnect.ID_DEFDOK = DokDoWyst.Id;

                    context.IHP_ERPCONNECTOR.Attach(_erpconnect);
                    context.Entry(_erpconnect).State = EntityState.Modified;
                    context.SaveChanges();
                    MessageBoxService.ShowMessage("Dane Zapisane");
                }
            }
             catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                MessageBox.Show("Wystąpiły błędy przy zapisaniu danych!!!" + System.Environment.NewLine + exceptionMessage);
                LogManager.WriteLogMessage(LogManager.LogType.Error, exceptionMessage.ToString());


                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
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
    public class ItemWidthConverter : MarkupExtension, IValueConverter
    {
        public ItemWidthConverter() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var intVal = 0;
            return Int32.TryParse(String.Format("{0}", parameter), out intVal) ? (double)value / intVal : (double)value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
 


