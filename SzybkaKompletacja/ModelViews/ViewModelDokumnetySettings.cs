using DevExpress.Mvvm;

using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.POCO;
using System.IO;
using DevExpress.Mvvm.DataAnnotations;
using System.Windows.Data;
using System.Windows.Markup;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
     class ViewModelDokumnetySettings :  CrudVMBase,INotifyPropertyChanged, IMVVMDockingProperties
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
        string _showlog;
        public bool NewRec=true;
    
        public ObservableCollection<IHP_JM> lstJm { get; private set; }

        public IHP_JM _jm;

            private List<IHP_RODZAJDOK> _listarodzdok;
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
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }

        public ICommand AddNewProgCommand { get; private set; }
        public ICommand DelNewProgCommand { get; private set; }
        public ICommand ClearNewProgCommand { get; private set; }
        public ICommand UpdNewProgCommand { get; private set; }
        public ICommand ShowDependencyCommand { get; private set; }
        public ICommand  ExportSubCommand { get; private set; }
        public int SelectedGrupaKartID { get; set; }
        public ViewModelDokumnetySettings()
        {
 
            RodzajeDok = new ObservableCollection<IHP_RODZAJDOK>(context.IHP_RODZAJDOK);
            RodzajDok = new IHP_RODZAJDOK();
            AddNewProgCommand = new RelayCommand(Save,CanSave);
            DelNewProgCommand = new DelegateCommand(Del);
            UpdNewProgCommand = new RelayCommand(Update,CanUpdate);
            ClearNewProgCommand = new DelegateCommand(Clear);
            NewRec = true;
        }

        public void SentRodzDok()
        {
            Messenger.Default.Send<List<IHP_RODZAJDOK>>(_listarodzdok);
        }
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
        private string _opis;
        public string Opis
        {
            get
            {
                return _opis;
            }
            set
            {
                _opis = value;
                RisePropertyChanged("Opis");
            }
        }
        private string _skrotdok;
        public string SkrotDok
        {
            get
            {
                return _skrotdok;
            }
            set
            {
                _skrotdok = value;
                RisePropertyChanged("SkrotDok");
            }
        }
     
        private void Clear()
        {
            if (_rodzajdok != null)
            {
                 RodzajDok = null;
                _rodzajdok = null;
                Opis = string.Empty;
            }
            NewRec = true;
        }
        private int _kierunekmag;
        //public bool IsKierunek
        //{
        //    get { return GetProperty(() => IsKierunek); }
        //    set { SetProperty(() => IsKierunek, value);   }
        //}
        private bool _iskierunek;
        public bool IsKierunek
        {
            get
            {
                return _iskierunek;
            }

            set
            {
                _iskierunek = value;
                RisePropertyChanged("IsKierunek");
            }
        }
        private bool CanUpdate()
        {
            if (!NewRec)
                return true;
            else
                return false;
            
        }
        void Update()
        {
            string LastMessage;
            try
            {
                //if (context.IHP_NAGLDOK.Any(x => x.ID_RODZAJDOK == _rodzajdok.ID_IHP_RODZAJDOK))
                //{
                //    MessageBoxService.Show("definicja wykorzystana w dokumencie nie można zmienić");
                //    return;
                //}

                if (_rodzajdok != null) 
                {

                    //if (IsKierunek)

                    //    _kierunekmag = 1;
                    //else
                    //    _kierunekmag = -1;

                    _rodzajdok.OPIS = _opis;
                    _rodzajdok.SKROTDOK = _skrotdok;
                  //  _rodzajdok.KIERUNEKMAG = _kierunekmag;
                    context.IHP_RODZAJDOK.Attach(_rodzajdok);
                    context.Entry(_rodzajdok).State = EntityState.Modified;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    NewRec = true;
                    SentRodzDok();
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
           NewRec = true;
        }
      
        private bool CanSave()
        {
 
            if ((_opis!= null) && (_opis.Length > 3) && (NewRec))
               return true;
            else
              return false;

        }
        private void Save()
          {
            string LastMessage;
            try
            {
                IHP_RODZAJDOK _rodzajdok = new IHP_RODZAJDOK();
                
                   IHP_NUMERACJA numerrodzdok = GetId(7);
                    if (numerrodzdok != null)
                        numerrodzdok.NUMER++;

                    if (IsKierunek)

                       _kierunekmag = 1;
                    else
                        _kierunekmag = -1;

                    int idrodzajdok = numerrodzdok.NUMER;

                    _rodzajdok.ID_IHP_RODZAJDOK = idrodzajdok;
                    _rodzajdok.OPIS = _opis;
                    _rodzajdok.SKROTDOK = _skrotdok;
                    _rodzajdok.KIERUNEKMAG = _kierunekmag;
           
                    context.IHP_RODZAJDOK.Add(_rodzajdok);
                    context.Entry(_rodzajdok).State = EntityState.Added;
                     context.IHP_NUMERACJA.Add(numerrodzdok);
                    context.Entry(numerrodzdok).State = EntityState.Modified;
                    context.SaveChanges();
                    Clear();
                    LoadCollection();
                    NewRec = true;
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
        private void Del()
        {
            string LastMessage;
            try
            {
                if (_rodzajdok != null)
                {
                    if(context.IHP_NAGLDOK.Any(x=>x.ID_RODZAJDOK == _rodzajdok.ID_IHP_RODZAJDOK))
                    {
                   MessageBoxService.Show("definicja wykorzystana w dkumencje nie można usunąć");
                        return;
               }
                   context.IHP_RODZAJDOK.Remove(_rodzajdok);
                    context.SaveChanges();
                    LoadCollection();
                    SentRodzDok();
                    NewRec = true;
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
        void LoadCollection()
        {
            RodzajeDok.Clear();
             _listarodzdok = new List<IHP_RODZAJDOK>();
             _listarodzdok = context.IHP_RODZAJDOK.ToList();

            foreach (IHP_RODZAJDOK item in _listarodzdok)
            {
                RodzajeDok.Add(item);
            }
            SentRodzDok();


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
                RisePropertyChanged("RodzajeDok");
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
                if(_rodzajdok!=null)
                {
                 if (_rodzajdok.KIERUNEKMAG == 1)
                        IsKierunek = true;
                 else if (_rodzajdok.KIERUNEKMAG == -1)
                        IsKierunek = false;
               }
                NewRec = false;
                RisePropertyChanged("RodzajDok");
            }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
    }

  
}


