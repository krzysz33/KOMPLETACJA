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
    public class ViewModelImport : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
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
     
        private IHP_DEFCENY _defceny;
        private IHP_KIEROWCA _kierowca;
        private List<IHP_KIEROWCA> _listakierowcy;
        private string  _nazwa,   _telefon ;
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
        public ViewModelImport()
        {
 
 
             
        }
        private void OnMessageKontrahent(List<IHP_KONTRAHENT> name)
        {
            LoadCollection();
        }
     //  public object RodzajKontrahSel;
     private void WystaDok()
        {
       
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
     private bool CanDelete()
         {
            if (_kierowca != null)
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



        }
     private void Save()
        {

        }
     private void Delete()
        {
            string LastMessage;
            try
            {
               if(Kierowca != null)
                {
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
     protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
     
    }
}
 


