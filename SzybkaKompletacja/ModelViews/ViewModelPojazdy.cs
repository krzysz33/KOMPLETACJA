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
using System.Windows.Controls;
using System.Globalization;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
 
    public class RequiredValidationRulePojazdy : ValidationRule
    {
        public static  List<IHP_SAMOCHOD> ListSam;
        public static string GetErrorMessage(string fieldName, object fieldValue, object nullValue = null)
        {

            string errorMessage = string.Empty;


            if (fieldValue == null) return errorMessage;

            if (nullValue != null && nullValue.Equals(fieldValue))
                errorMessage = string.Format("Pole:  {0} jest puste.", fieldName);
            if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
                errorMessage = string.Format("Pole: {0} jest puste.", fieldName);

     

                if ((fieldName=="NrRej") || (nullValue != null) ||   (!string.IsNullOrEmpty(fieldValue.ToString())))
               {
                    if (ListSam.Any(x => x.NRREJ == fieldValue.ToString()))
                            errorMessage = string.Format("Nr Rej. Jest Na Liście");
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

    public class ViewModelPojazdy : CrudVMBase, INotifyPropertyChanged, IDataErrorInfo, IMVVMDockingProperties
    {
        private bool _isreadonlynrrej = false;
        public bool IsReadOnlyNrRej
        {
            get
            {
                return _isreadonlynrrej;
            }
            set
            {
                _isreadonlynrrej = value;
                RisePropertyChanged("IsReadOnlyNrRej");
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
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        string IDataErrorInfo.Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string error =
                   me[BindableBase.GetPropertyName(() => NrRej)];
                if (Samochody.Any(x => x.NRREJ == NrRej))
                    return "Sprawdz dane";
                return null;
            }
        }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                string NrRejProp = BindableBase.GetPropertyName(() => NrRej);

                if (columnName == NrRejProp)
                    return RequiredValidationRulePojazdy.GetErrorMessage(NrRejProp, NrRej);


                return null;
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
        decimal tara = 0m;
        bool newrec = true;
        private WagaRamka _ramka;
        //private entWagaDuza context;
        private List<IHP_SAMOCHOD> _listasam;
        private List<IHP_KIEROWCA> _listakierowcy;

        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddPojazdCommand { get; private set; }
        public ICommand AddToSubjectCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DelNewProgCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand FillDataCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; set; }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<IHP_SAMOCHOD> _samochody;
        public ObservableCollection<IHP_SAMOCHOD> Samochody
        {
            get
            {
                return _samochody;
            }

            set
            {
                _samochody = value;
                RisePropertyChanged("Samochody");

            }
        }
        private ObservableCollection<IHP_KIEROWCA> _kierowcy;
        public ObservableCollection<IHP_KIEROWCA> Kierowcy
        {
            get
            {
                return _kierowcy;
            }
            set
            {
                _kierowcy = value;
            }
        }
        private bool istararecznietext;
        public bool IsTaraRecznieText
        {
            get
            {
                return istararecznietext;
            }
            set
            {
                istararecznietext = value;
                RisePropertyChanged("IsTaraRecznieText");
            }
        }
        private bool istararecznie;
        public bool IsTaraRecznie
        {
            get
            {
                return istararecznie;
            }
            set
            {

                istararecznie = value;

                if (istararecznie)
                    IsTaraRecznieText = false;
                else
                    IsTaraRecznieText = true;

                RisePropertyChanged("IsTaraRecznie");
            }
        }
        private string wagareczna;
        public string WagaReczna
        {
            get
            {
                return wagareczna;
            }
            set
            {
                wagareczna = value;
                if (!string.IsNullOrEmpty(wagareczna))
                    tara = Convert.ToDecimal(wagareczna);
                RisePropertyChangedMp("WagaReczna");
            }
        }
        private int _idkierowca;
        public int IdKierowca
        {
            get
            {
                return _idkierowca;
            }
            set
            {
                _idkierowca = value;
                RisePropertyChanged("IdKierowca");
            }
        }
        public ViewModelPojazdy()
        {
  
            Samochody = new ObservableCollection<IHP_SAMOCHOD>(context.IHP_SAMOCHOD);
            Kierowcy = new ObservableCollection<IHP_KIEROWCA>(context.IHP_KIEROWCA);
            DelNewProgCommand = new RelayCommand(Delete, CanDelete);
            AddPojazdCommand = new RelayCommand(Save, CanSave);
            ClearNewProgCommand = new DelegateCommand(Clear);
            FillDataCommand = new DelegateCommand(FillData);
            UpdateCommand = new RelayCommand(Update, CanUpdate);
            LoadCollection();

            Messenger.Default.Register<WagaRamka>(this, OnMessagewagaRamka);
            Messenger.Default.Register<List<IHP_KIEROWCA>>(this, OnMessageKierowca);
 
            IsTaraRecznie = true;
        }
        public void OnMessagewagaRamka(WagaRamka ramka)
        {
            _ramka = ramka;
            WagaKgLocal = ramka.Waga.ToString() + " kg";
        }
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
                RisePropertyChangedMp("WagaKgLocal");
            }

        }

        //  public object RodzajKontrahSel;
        private void WystaDok()
        {
            //Messenger.Default.Send<IHP_KONTRAHENT>(_kontrah);
            //Messenger.Default.Send<bool>(true);
        }
        private IHP_SAMOCHOD _samochod;
        public IHP_SAMOCHOD Samochod
        {
            get
            {
                return _samochod;
            }
            set
            {
                _samochod = value;
                if (_samochod != null)
                    newrec = false;
                RisePropertyChangedMp("Samochod");
            }
        }
        private IHP_KIEROWCA _kierowca;
        public IHP_KIEROWCA Kierowca
        {
            get
            {
                return _kierowca;
            }
            set
            {
                _kierowca = value;
                RisePropertyChangedMp("Kierowca");
            }
        }
        private string _nrrej;
        public string NrRej
        {
            get
            {
                return _nrrej;
            }
            set
            {
                _nrrej = value;
                RisePropertyChangedMp("NrRej");
            }
        }
        private string _nazwa;
        public string Nazwa
        {
            get
            {
                return _nazwa;
            }
            set
            {
                _nazwa = value;
                RisePropertyChangedMp("Nazwa");
            }
        }
        private bool CanDelete()
        {
            if (_samochod != null)
                return true;
            else
                return false;
        }
        private bool CanUpdate()
        {
            if (_kierowca != null)
                return true;
            else
                return false;
        }
        private bool CanSave()
        {
            if ((!string.IsNullOrEmpty(_nrrej))
                  && (!string.IsNullOrEmpty(_nazwa))
                  && ((_kierowca != null))
                   && (newrec))
                return true;
            else
                return false;
        }
        private IHP_NUMERACJA GetId(int dlatabeli)
        {
            return context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);
        }
        public void LoadCollection()
        {
            _listasam = context.IHP_SAMOCHOD.ToList();
            RequiredValidationRulePojazdy.ListSam = _listasam;
            Samochody.Clear();
            foreach (IHP_SAMOCHOD item in _listasam)
            {
                Samochody.Add(item);
            }
        }
        public void ReloadCollection()
        {
            _listasam = context.IHP_SAMOCHOD.ToList();
            Samochody.Clear();
            foreach (IHP_SAMOCHOD item in _listasam)
            {
                Samochody.Add(item);
            }
        }

        private void Save()
        {

            if (!IsTaraRecznie)
                tara = _ramka.Waga;


            IHP_NUMERACJA numerkr = GetId(4);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                IHP_SAMOCHOD samochod = new IHP_SAMOCHOD()
                {
                    ID_IHP_SAMOCHOD = numerkr.NUMER,
                    NAZWA = _nazwa,
                    NRREJ = _nrrej,
                    ID_IHP_KIEROWCA = _kierowca.ID_IHP_KIEROWCA,
                    IHP_KIEROWCA = _kierowca,
                    TARA = tara
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_SAMOCHOD.Add(samochod);
                context.SaveChanges();
                LoadCollection();
                SentSamochod();
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
        private void Delete()
        {
            string LastMessage;
            try
            {
                if (_samochod != null)
                {

                

                    IHP_SAMOCHOD dousuniecja = context.IHP_SAMOCHOD.Find(_samochod.ID_IHP_SAMOCHOD);
                    context.Entry(dousuniecja).State = EntityState.Deleted;
                    context.IHP_SAMOCHOD.Remove(dousuniecja);
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentSamochod();

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
        public void SentSamochod()
        {
            Messenger.Default.Send<int>(1);
        }
        private void Clear()
        {
            if (_samochod != null)
            {
                Samochod = null;
                _samochod = null;
                newrec = true;
            }
            if (_kierowca != null)
            {
                Kierowca = null;
                _kierowca = null;
            }

            NrRej = String.Empty;
            _isreadonlynrrej = false;
            Nazwa = String.Empty;
            WagaReczna = String.Empty;
        }
        private void FillData()
        {
            if (_samochod != null)
            {
                NrRej = _samochod.NRREJ;
                Nazwa = _samochod.NAZWA;
                IdKierowca = _samochod.ID_IHP_KIEROWCA ?? 0;
                Kierowca = _kierowcy.FirstOrDefault(x => x.ID_IHP_KIEROWCA == _samochod.ID_IHP_KIEROWCA);
                WagaReczna = _samochod.TARA.ToString();
            }
        }
        private void Update()
        {
            if (!IsTaraRecznie)
                tara = _ramka.Waga;
            try
            {
                if (_samochod != null)
                {
                    _samochod.NAZWA = _nazwa;
                    _samochod.NRREJ = _nrrej;
                    _samochod.IHP_KIEROWCA = _kierowca;
                    _samochod.TARA = tara;
                    context.IHP_SAMOCHOD.Attach(_samochod);
                    context.Entry(_samochod).State = EntityState.Modified;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentSamochod();
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
        private void AddSubject()
        {
            //  SubjectSfera sb = new SubjectSfera();
            //  sb.DodajKontrahenta();
        }
        protected void RisePropertyChangedMp(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void OnMessageKierowca(List<IHP_KIEROWCA> Name)
        {
            //this._listakierowcy = Name;
            _listakierowcy = context.IHP_KIEROWCA.ToList();
            Kierowcy.Clear();
            foreach (IHP_KIEROWCA item in _listakierowcy)
            {
                Kierowcy.Add(item);
            }
        }

    }

}
 


