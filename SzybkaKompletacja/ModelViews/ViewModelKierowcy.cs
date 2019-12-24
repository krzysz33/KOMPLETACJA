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
using System.Data.Entity.Infrastructure;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    public class ViewModelKierowcy : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
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
        private IHP_DEFCENY _defceny;
        private IHP_KIEROWCA _kierowca;
        private IHP_KONTRAHENT _kontrah;
        private List<IHP_KONTRAHENT> _listakontrahentow;
        private List<IHP_KONTRAHENT> _listakontrah;
        private List<IHP_KIEROWCA> _listakierowcy;

        public int IdKontrahent { get; set; }


        private string _imie, _nazwa, _email, _telkom, _telefon, _uwagi, _nip;

        public ICommand RowDoubleClickCommand { get; private set; }
        public ICommand AddNewProgCommand { get; private set; }
        public ICommand  AddToSubjectCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand CreateDokCommand { get; private set; }
        public ICommand ExportSubjectCommand { get; private set; }
        public ICommand ImportSubjectCommand { get; private set; }
         public ICommand FillDataCommand { get; set; }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
       public event PropertyChangedEventHandler PropertyChanged;
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
        public ViewModelKierowcy()
        {
 
            Kontrahs = new ObservableCollection<IHP_KONTRAHENT>(context.IHP_KONTRAHENT);
            Kierowcy = new ObservableCollection<IHP_KIEROWCA>(context.IHP_KIEROWCA);
                  DeleteCommand = new RelayCommand(Delete, CanDelete);
            FillDataCommand = new DelegateCommand(FillData);
            AddNewProgCommand = new RelayCommand(Save, CanSave);
                      ClearCommand = new DelegateCommand(Clear);
                      UpdateCommand = new RelayCommand(Update, CanUpdate);
            Messenger.Default.Register<List<IHP_KONTRAHENT_ARCH>>(this, OnMessageKontrahent);
            LoadCollection();
            SentKierowcy();
             
        }
        private void OnMessageKontrahent(List<IHP_KONTRAHENT_ARCH> name)
        {
            LoadCollection();
        }
        private void FillData()
        {
            if(_kierowca!=null)
            {
                Nazwa = _kierowca.NAZWA;
                Telefon = _kierowca.TELEFON;
                Kontrah = _kierowca.IHP_KONTRAHENT;
                IdKontrahent = _kontrah.ID_IHP_KONTRAHENT;
            }
        }

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
                RisePropertyChanged("DefCeny");
            }
       }
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
     public IHP_KIEROWCA Kierowca
        {
            get
            {
               return  _kierowca;
            }
            set
            {
                _kierowca = value;
             RisePropertyChanged("Kierowca");
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
                RisePropertyChanged("Nazwa");
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
                RisePropertyChanged("Telefon");
            }
        }
     private bool CanDelete()
         {
            if (_kierowca != null)
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
            if(!string.IsNullOrEmpty(_nazwa)
               && !string.IsNullOrEmpty(_telefon))
           
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
            Kontrahs.Clear();
            _listakontrah = new List<IHP_KONTRAHENT>();
            _listakontrah = context.IHP_KONTRAHENT.ToList();

            foreach (IHP_KONTRAHENT item in _listakontrah)
            {
                Kontrahs.Add(item);
            }

            Kierowcy.Clear();
            _listakierowcy = new List<IHP_KIEROWCA>();
            _listakierowcy = context.IHP_KIEROWCA.ToList();

            foreach (IHP_KIEROWCA item in _listakierowcy)
            {
                Kierowcy.Add(item);
            }

        }
     private void Save()
        {
            int defcena = 1;
            if (_defceny != null)
                defcena = _defceny.ID_IHP_DEFCENY;

            IHP_NUMERACJA numerkr = GetId(3);
            if (numerkr != null)
                numerkr.NUMER++;

            string LastMessage;
            try
            {
                IHP_KIEROWCA kierowca = new IHP_KIEROWCA()
                {
                    ID_IHP_KIEROWCA = numerkr.NUMER,
                    NAZWA = _nazwa,
                    TELEFON = _telefon,
                    ID_IHP_KONTRAHENT = _kontrah.ID_IHP_KONTRAHENT,
                    IHP_KONTRAHENT = _kontrah
                };

                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_KIEROWCA.Add(kierowca);
                context.SaveChanges();
                LoadCollection();
                SentKierowcy();
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
               if(Kierowca != null)
                {
                    if(context.IHP_SAMOCHOD.Any(x=>x.ID_IHP_KIEROWCA == _kierowca.ID_IHP_KIEROWCA))
                    {
                        MessageBoxService.ShowMessage("Nie mozna usunąć kierowca przypisany do samochodu !");
                        return;
                    }

                   IHP_KIEROWCA dousuniecja = context.IHP_KIEROWCA.Find(_kierowca.ID_IHP_KIEROWCA);
                    context.Entry(dousuniecja).State = EntityState.Deleted;
                    context.IHP_KIEROWCA.Remove(dousuniecja);
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentKierowcy();
                } 
           }
            catch (DbUpdateException Ex)
            {
                LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("DbUpdateException \"{0}\"  :", Ex.InnerException.Message));
                throw Ex;
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
     public void  SentKierowcy()
        {
            Messenger.Default.Send<List<IHP_KIEROWCA>>(_listakierowcy);
        }
     private void Clear()
        {
            if (_kontrah != null)
            {
                Kontrah = null;
                _kontrah = null;
            }
            if (_kierowca != null)
            {
                Kierowca = null;
                _kierowca = null;
            }
             Nazwa = string.Empty;
             Telefon = string.Empty;
        } 
     private void Update()
        {
            string LastMessage;
            try
            {
                if (_kierowca != null)
                {
                    _kierowca.NAZWA = _nazwa;
                    _kierowca.TELEFON = _telefon;
                    _kierowca.IHP_KONTRAHENT = _kontrah;
                    //_kierowca.ID_IHP_KONTRAHENT = _kontrah.ID_IHP_KONTRAHENT;
                    context.IHP_KIEROWCA.Attach(_kierowca);
                    context.Entry(_kierowca).State = EntityState.Modified;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    SentKierowcy();
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
     private void AddSubject()
        {
      //      SubjectSfera sb = new SubjectSfera();
     //        sb.DodajKontrahenta();
 
       }
   
     
    }
}
 


