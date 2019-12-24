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

namespace KpInfohelp
{
    class ViewModelDefCeny : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
        public event PropertyChangedEventHandler PropertyChanged;
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }

        public ICommand AddDefCommand { get; set; }
        public ICommand FillDataCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand UpdateCommand { get; set; }

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

        IHP_DEFCENY _defceny;
        public IHP_DEFCENY DefCeny
        {
            get
            {
                return _defceny;
            }
            set
            {
                _defceny = value;
                RisePropertyChanged("DefCeny");
            }
        }
        private string _nazwa;
 
        private bool _odnetto;
        public bool OdNetto
        {
            get
            {
                return _odnetto;
            }
            set
            {
                _odnetto = value;
                RisePropertyChanged("OdNetto");
            }
        }

        private List<IHP_DEFCENY> _listadef;
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
                RisePropertyChanged("DefCenyLst");
            }
        }
        private  List<IHP_CENNIK> Cennik;
        private string _nazwdef;
        public string NazwaDef
        {
            get
            {
                return _nazwdef;
            }
            set
            {
                _nazwdef = value;
                RisePropertyChanged("NazwaDef");
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
        public ViewModelDefCeny()
        {
            Cennik = context.IHP_CENNIK.ToList();
            AddDefCommand = new DelegateCommand(Save);
            FillDataCommand = new DelegateCommand(FillData);
            UpdateCommand = new DelegateCommand(Update);
            ClearCommand = new DelegateCommand(Clear);
            DefCenyLst = new ObservableCollection<IHP_DEFCENY>();
            LoadCollection();
        }
         private void Save()
        {
            IHP_NUMERACJA numerkr = GetId(13);
            if (numerkr != null)
                numerkr.NUMER++;
            string LastMessage;
            try
            {
                _defceny = new IHP_DEFCENY()
                {
                    ID_IHP_DEFCENY = numerkr.NUMER,
                    NAZWACENY = _nazwdef,
                };

                if (IsAktywny)
                    _defceny.AKTYWNY = 1;

                if (!IsAktywny)
                    _defceny.AKTYWNY = 0;

                if (OdNetto)
                    _defceny.ODNETTO = 1;

                if (!OdNetto)
                    _defceny.ODNETTO = 0;

                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_DEFCENY.Add(_defceny);
                context.SaveChanges();
                LoadCollection();
                SentDefCeny();
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
        private void LoadCollection()
        {
            _listadef = context.IHP_DEFCENY.ToList();
       
            DefCenyLst.Clear();
            foreach (IHP_DEFCENY item in _listadef)
            {
                DefCenyLst.Add(item);
            }
        }
        public void SentDefCeny()
        {
            Messenger.Default.Send<IHP_DEFCENY>(DefCeny);
        }
        private void Delete()
        {
            string LastMessage;
            try
            {
                if (_defceny != null)
                {
                    if (Cennik.Any(x => x.ID_IHP_DEFCENY == _defceny.ID_IHP_DEFCENY))
                    {
                        MessageBoxService.ShowMessage("Pojazd wykorzystany przy ważeniu - Nie można skasować!!");
                        return;
                    }
                    IHP_DEFCENY dousuniecja = context.IHP_DEFCENY.Find(_defceny.ID_IHP_DEFCENY);
                    context.Entry(dousuniecja).State = EntityState.Deleted;
                    context.IHP_DEFCENY.Remove(dousuniecja);
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentDefCeny();
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
                  DefCeny = null;
                IsAktywny = false;
                OdNetto = false;
                NazwaDef = String.Empty;
   
        }
        private void FillData()
        {
          if (_defceny != null)
             {
              NazwaDef = _defceny.NAZWACENY;
               if (_defceny.AKTYWNY == 1)
                     IsAktywny = true;
               if (_defceny.AKTYWNY == 0)
                    IsAktywny = false;
               if (_defceny.ODNETTO == 1)
                     OdNetto = true;
               if (_defceny.ODNETTO == 0)
                     OdNetto = false;
            }
        }
        private void Update()
        {
           try
            {
                if (_defceny != null)
                {
                    _defceny.NAZWACENY = NazwaDef;

                    if (IsAktywny)
                        _defceny.AKTYWNY = 1;
                    if (!IsAktywny)
                        _defceny.AKTYWNY = 0;

                    if (OdNetto)
                        _defceny.ODNETTO = 1;
                    if (!OdNetto)
                        _defceny.ODNETTO = 0;

                    context.IHP_DEFCENY.Attach(_defceny);
                    context.Entry(_defceny).State = EntityState.Modified;
                    context.SaveChanges();
                    LoadCollection();
                    SentDefCeny();
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

    }
}

