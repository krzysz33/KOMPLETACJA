using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace KpInfohelp
{

    public class RequiredValidationRuleRodzGrKart : ValidationRule
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

    public class RequiredValidationRuleGrKart : ValidationRule
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

    class ViewModelGrupaKart : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties, IDataErrorInfo
    {
        private bool isedit = false;
        private bool isadding = true;
   
    
        public event PropertyChangedEventHandler PropertyChanged;
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }

        public ICommand AddDefCommand { get; set; }
        public ICommand AddCommandGrKar { get; set; }
        public ICommand AddCommandNad { get; set; }
        public ICommand AddCommandPod { get; set; }
        public ICommand  DelRodzCommand { get; set; }
        public ICommand FillDataCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand  ClearRodzCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
       public ICommand AddRodzCommand { get; set; }
        public ICommand UpdateRodzCommand { get; set; }
        public ICommand EditRodzCommand { get; set; }
        public ICommand DelCommand { get; set; }

        public ICommand OnViewLoadedCommand { get; set; }
        public ICommand  CloseCommand { get; set; }

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
                    me[BindableBase.GetPropertyName(() => KodGrupy)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => NazwaGrupy)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => KodZlozony)] + System.Environment.NewLine
                    + me[BindableBase.GetPropertyName(() => NazwaZlozona)];
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
             
                string KodGrupyProp = BindableBase.GetPropertyName(() => KodGrupy);
                string NazwaGrupyProp = BindableBase.GetPropertyName(() => NazwaGrupy);
                string KodZlozonyProp = BindableBase.GetPropertyName(() => KodZlozony);
                string NazwaZlozonaProp = BindableBase.GetPropertyName(() => NazwaZlozona);

                if (columnName == KodGrupyProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(KodGrupyProp, KodGrupy);
                if (columnName == NazwaGrupyProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(NazwaGrupyProp, NazwaGrupy);
                if (columnName == KodZlozonyProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(KodZlozonyProp, KodZlozony);
                if (columnName == NazwaZlozonaProp)
                    return RequiredValidationRuleGrKart.GetErrorMessage(NazwaZlozonaProp, NazwaZlozona);
                else
                return null;
            }
        }


        private IHP_GRUPAKART _grupakartupdate;
        public IHP_GRUPAKART GrupaKartUpdate
        {
            get
            {
                return _grupakartupdate;
            }

            set
            {
                _grupakartupdate = value;
                RisePropertyChanged("GrupaKartUpdate");
            }
        }
        IHP_GRUPAKART _grupakart;
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
        private string _nazwagrupy;
        public string NazwaGrupy
         {
            get
            {
                return _nazwagrupy;

            }
            set
            {
                _nazwagrupy = value;
                RisePropertyChanged("NazwaGrupy");
            }
}
        private string _kodgrupy;
        public string KodGrupy
        {
            get
            {
                return _kodgrupy;

            }
            set
            {
                _kodgrupy = value;
                RisePropertyChanged("KodGrupy");
            }
        }
        private string _kodzlozony;
        public string KodZlozony
        {
            get
            {
                return _kodzlozony;

            }
            set
            {
                _kodzlozony = value;
                RisePropertyChanged("KodZlozony");
            }
        }
        private string _nazwazlozona;
        public string NazwaZlozona
        {
            get
            {
                return _nazwazlozona;
            }
            set
            {
                _nazwazlozona = value;
                RisePropertyChanged("NazwaZlozona");
            }
        }
        private string _nazwarodz;
        public string NazwaRodz
        {
            get
            {
                return _nazwarodz;
            }
            set
            {
                _nazwarodz = value;
                RisePropertyChanged("NazwaRodz");
            }
        }
        private List<IHP_GRUPAKART> _listagrkt;
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
        private  List<IHP_GRUPAKART> GrupaKartL;
        private List<IHP_RODZGRUPKART> GrupaGrKart;
        public ViewModelGrupaKart()
        {
            GrupaKartLst = new ObservableCollection<IHP_GRUPAKART>();
            RodzKartLst = new ObservableCollection<IHP_RODZGRUPKART>(context.IHP_RODZGRUPKART);
            AddRodzCommand = new DelegateCommand(SaveRodzaj, CanAdd);
            UpdateRodzCommand = new DelegateCommand(UpdateRodz, CanAdd);
            DelRodzCommand = new DelegateCommand(DeleteRodz);
            ClearRodzCommand = new DelegateCommand(ClearRodz);
            AddCommandGrKar = new DelegateCommand(Save, CanAddGrKart);
            AddCommandNad = new DelegateCommand(SaveNad, CanAdd);
            AddCommandPod = new DelegateCommand(SavePod, CanAddGrKart);
            FillDataCommand = new DelegateCommand(FillData);
            UpdateCommand = new DelegateCommand(Update, CanUpdateGrKart);
            ClearCommand = new DelegateCommand(Clear);
            EditRodzCommand = new DelegateCommand(FillRodzData);
            DelCommand = new DelegateCommand(Delete);
            OnViewLoadedCommand = new DelegateCommand(LoadCollectionGrKartAll);
            CloseCommand = new DelegateCommand<Window>(DoubleClick);
            LoadRodzKartLst();
      }

        private void LoadCollectionGrKartAll()
        {

            _listagrkt = context.IHP_GRUPAKART.ToList();
            GrupaKartLst.Clear();

            foreach (IHP_GRUPAKART item in _listagrkt)
            {
                GrupaKartLst.Add(item);

            }
            RisePropertyChanged("GrupaKartLst");
        }

        private void SavePod()
         {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                MessageBox.Show("Błąd " + error, "Dodawanie grupy kartotekowej", MessageBoxButton.OK);
                return;
            }

            if (_grupakart == null)
            {
                MessageBox.Show("Wybierz grupę nadrzędną !!!");
                return;
            }

            if (_rodzgrkart == null)
            {
                MessageBox.Show("Wybierz rodzaj grupy!!!");
                return;
            }
            IHP_NUMERACJA numerkr = GetId(14);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                IHP_GRUPAKART grupakart = new IHP_GRUPAKART()
                {
                    ID_IHP_GRUPAKART = numerkr.NUMER,
                    ID_IHP_GRUPAKART_NADRZ = _grupakart.ID_IHP_GRUPAKART,
                    ID_IHP_RODZGRUPKART = _rodzgrkart.ID_IHP_RODZGRUPKART,
                    NAZWAGRUPY = NazwaGrupy,
                    KODGRUPY = KodGrupy,
                    KODZLOZONY = KodZlozony,
                    NAZWAZLOZONA = NazwaZlozona
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_GRUPAKART.Add(grupakart);
                context.SaveChanges();
                LoadCollection();
                SentGrupaKart();
                Clear();
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
        private void SaveNad()
        {
            IHP_NUMERACJA numerkr = GetId(14);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                _grupakart = new IHP_GRUPAKART()
                {
                    ID_IHP_GRUPAKART = numerkr.NUMER,
                    NAZWAGRUPY = NazwaGrupy,
                    KODGRUPY = KodGrupy,
                    KODZLOZONY = KodZlozony,
                    NAZWAZLOZONA = NazwaZlozona
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_GRUPAKART.Add(_grupakart);
                context.SaveChanges();
                LoadCollection();
                SentGrupaKart();
                Clear();
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
        private void SaveRodzaj()
        {
            IHP_NUMERACJA numerkr = GetId(16);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                _rodzgrkart = new IHP_RODZGRUPKART()
                {
                    ID_IHP_RODZGRUPKART = numerkr.NUMER,
                    NAZWA = _nazwarodz
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_RODZGRUPKART.Add(_rodzgrkart);
                context.SaveChanges();
                LoadRodzKartLst();
                SentGrupaKart();
                Clear();
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

            string error = EnableValidationAndGetError();
            if (error != null)
            {
                MessageBox.Show("Błąd " + error, "Dodawanie grupy kartotekowej", MessageBoxButton.OK);
                return;
            }


            IHP_NUMERACJA numerkr = GetId(14);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                _grupakart = new IHP_GRUPAKART()
                {
                    ID_IHP_GRUPAKART = numerkr.NUMER,
                    ID_IHP_GRUPAKART_NADRZ= numerkr.NUMER,
                    IHP_RODZGRUPKART = _rodzgrkart,
                    NAZWAGRUPY = NazwaGrupy,
                    KODGRUPY  = KodGrupy,
                    KODZLOZONY   = KodZlozony,
                    NAZWAZLOZONA  = NazwaZlozona
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_GRUPAKART.Add(_grupakart);
                context.SaveChanges();
                LoadCollection();
                SentGrupaKart();
                Clear();
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
        private void SentGrupaKart()
        {
            Messenger.Default.Send<IHP_GRUPAKART>(_grupakart);
        }
        private void LoadRodzKartLst()
        {
            GrupaGrKart = context.IHP_RODZGRUPKART.ToList();
            RodzKartLst.Clear();

            foreach (IHP_RODZGRUPKART item in GrupaGrKart)
                                          RodzKartLst.Add(item);
        }
        private void LoadCollection()
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
        public void SentGrupaKartCeny()
        {
            Messenger.Default.Send<IHP_GRUPAKART>(_grupakart);
        }
        private void Delete()
        {
            string LastMessage;
            try
            {
                if (_grupakart != null)
                {
                    if (context.IHP_GRUPAKART.Any(x => x.ID_IHP_GRUPAKART_NADRZ == _grupakart.ID_IHP_GRUPAKART && x.ID_IHP_GRUPAKART != _grupakart.ID_IHP_GRUPAKART))
                    {
                        MessageBox.Show("Nie można usunąć grupy która posiada grupe podrzędną !!");
                        return;
                    }

                    MessageBoxResult result = MessageBox.Show("Czy Napewno Usunąć Grupę " + _grupakart.NAZWAGRUPY + " ??", "Potwierdź", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                       IHP_GRUPAKART dousuniecja = context.IHP_GRUPAKART.Find(_grupakart.ID_IHP_GRUPAKART);
                        context.Entry(dousuniecja).State = EntityState.Deleted;
                        context.IHP_GRUPAKART.Remove(dousuniecja);
                        context.SaveChanges();
                        Clear();
                        LoadCollection();
                        SentGrupaKartCeny();
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
        private bool CanAdd()
        {
            if (_rodzgrkart == null)
                return false;
            else
                return true;
        }
        private bool CanAddGrKart()
        {
            return isadding;
        }
        private bool CanUpdateGrKart()
        {
            return isedit;
        }
        private void Clear()
        {
            _grupakart = null;
            NazwaGrupy = String.Empty;
            KodGrupy = String.Empty;
            KodZlozony = String.Empty;
            NazwaZlozona = String.Empty;
            isedit = false;
            isadding = true;
        }
        private void ClearRodz()
        {
 
             NazwaRodz = String.Empty;
      
        }
        private void FillRodzData()
        {
            if(_rodzgrkart==null )
            {
                MessageBox.Show("Wybierz Rodzaj Grupy !");
                return;

            }
            else 
                NazwaRodz = _rodzgrkart.NAZWA;  
        }
        private void FillData()
        {
          if (_grupakart != null)
             {
                NazwaGrupy  = _grupakart.NAZWAGRUPY;
                KodGrupy = _grupakart.KODGRUPY;
                KodZlozony = _grupakart.KODZLOZONY;
                NazwaZlozona = _grupakart.NAZWAZLOZONA;
                isedit = true;
                isadding = false;
            }
        }
        private void Update()
        {
           try
            {
                if (_grupakart != null)
                {
                    _grupakart.KODGRUPY = KodGrupy;
                    _grupakart.NAZWAGRUPY = NazwaGrupy;
                    _grupakart.KODZLOZONY = KodZlozony;
                    _grupakart.NAZWAZLOZONA = NazwaZlozona;
                    context.IHP_GRUPAKART.Attach(_grupakart);
                    context.Entry(_grupakart).State = EntityState.Modified;
                    context.SaveChanges();
                    LoadCollection();
                    Clear();
             
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
        private void UpdateRodz()
        {
            try
            {
                if (_rodzgrkart != null)
                {
                    _rodzgrkart.NAZWA = NazwaRodz;
                     context.IHP_RODZGRUPKART.Attach(_rodzgrkart);
                    context.Entry(_rodzgrkart).State = EntityState.Modified;
                    context.SaveChanges();
                    LoadRodzKartLst();
                    ClearRodz();
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
        private void DeleteRodz()
        {
            string LastMessage;
            try
            {
                if (_rodzgrkart != null)
                {
                    if (context.IHP_GRUPAKART.Any(x => x.ID_IHP_RODZGRUPKART == _rodzgrkart.ID_IHP_RODZGRUPKART))
                    {
                        MessageBox.Show("Nie można usunąć rodzaju grupy  który posiada przypisaną grupe!!");
                        return;
                    }

                    MessageBoxResult result = MessageBox.Show("Czy Napewno Usunąć Rodzaj grupy  " + _rodzgrkart.NAZWA + " ??", "Potwierdź", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {

                        IHP_RODZGRUPKART dousuniecja = context.IHP_RODZGRUPKART.Find(_rodzgrkart.ID_IHP_RODZGRUPKART);
                        context.Entry(dousuniecja).State = EntityState.Deleted;
                        context.IHP_RODZGRUPKART.Remove(dousuniecja);
                        context.SaveChanges();
                        LoadRodzKartLst();
                        ClearRodz();

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
        private void DoubleClick(Window window)
        {
            if (GrupaKartUpdate != null)
                Messenger.Default.Send(GrupaKartUpdate);
            if (window != null)
            {
                window.Close();
            }
        }

    }
}

