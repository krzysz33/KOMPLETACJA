using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace KpInfohelp
{
    public class ViewModelTrasy : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
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

        public ICommand SaveCommand { get; private set; }
        public ICommand CleanCommand { get; private set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand CloseCommand { get; set; }
  

        private bool isupdate = false;
        private ObservableCollection<IHP_TRASY> _lsttrasy;
         public ObservableCollection<IHP_TRASY> LstTrasy
        {
            get
            {
                return _lsttrasy;
            }
            set
            {
                _lsttrasy = value;
                RisePropertyChanged("LstTrasy");
             
            }

        }
        private IHP_TRASY _trasaupdate;
        public IHP_TRASY TrasaUpdate
        {
            get
            {
                return _trasaupdate;
            }
            set
            {
                _trasaupdate = value;
                RisePropertyChanged("TrasaUpdate");
            }
        }
        private IHP_TRASY _trasa;
        public IHP_TRASY Trasa
        {
            get
            {
                return _trasa;
            }
            set
            {
                _trasa = value;
                RisePropertyChanged("Trasa");
            }
        }
        private string _nazwatrasy;
        public string NazwaTrasy
        {
            get
            {
                return _nazwatrasy;
            }
            set
            {
                _nazwatrasy = value;
                RisePropertyChanged("NazwaTrasy");
            }
}
        public ViewModelTrasy()
        {
            LstTrasy =  new ObservableCollection<IHP_TRASY>(context.IHP_TRASY);
            SaveCommand = new DelegateCommand(Save);
            CleanCommand = new DelegateCommand(Clear);
            UpdateCommand = new DelegateCommand(Update);
            CloseCommand = new DelegateCommand<Window>(DoubleClick);
  
            LoadColection();
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
        private void Clear()
        {
            _trasaupdate = null;
            NazwaTrasy = string.Empty;
            isupdate = false;
        }
        private void Save()
        {
            string LastMessage;
            try
            {
                if (_nazwatrasy.Length < 3) return;


                if(isupdate)
                {
                    _trasaupdate.NAZWA = _nazwatrasy;
                    context.IHP_TRASY.Attach(_trasaupdate);
                    context.Entry(_trasaupdate).State = EntityState.Modified;
                    context.SaveChanges();
                     LoadColection();
                    Clear();
                }
                else
                {
               
                _trasa = new IHP_TRASY();

                    _trasa.ID_IHP_TRASY = GetNextNumer(18);
                    _trasa.NAZWA = _nazwatrasy;
                     context.IHP_TRASY.Add(_trasa);
                    context.Entry(_trasa).State = EntityState.Added;
                    context.SaveChanges();
                    context.Entry(_trasa).Reload();
                     LoadColection();
                    Clear();
                }

                isupdate = false;
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
            if (_trasaupdate != null)
            {
                NazwaTrasy = _trasaupdate.NAZWA;
                isupdate = true;
            }
        }
        private void LoadColection()
        {
            if (LstTrasy == null) return;
            LstTrasy.Clear();
            List<IHP_TRASY> _listatrasy = new List<IHP_TRASY>();
            _listatrasy = context.IHP_TRASY.ToList();

            foreach (IHP_TRASY item2 in _listatrasy)
            {
                LstTrasy.Add(item2);
            }
        }

        private void DoubleClick(Window window)
        {
            if (TrasaUpdate != null)
                Messenger.Default.Send(TrasaUpdate);
            if (window != null)
            {
                window.Close();
            }
        }

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

     public event PropertyChangedEventHandler PropertyChanged;

    }
}


