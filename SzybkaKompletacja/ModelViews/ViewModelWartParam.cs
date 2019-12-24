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
using DevExpress.Xpf.Docking;
using System.Windows;
using System.Data.Entity.Validation;

namespace KpInfohelp
{
    public class ParametryLista
    {
         public int ID { get; set; }
         public string Opis { get; set; }
         public string Wartosc { get; set; }
    }
    public  class ViewModelWartParam : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
        public int SzerokoscBool { get; set; }
        private string SendParam = string.Empty;
       public ICommand CloseCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
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

        private Visibility _pokazbool;
        public Visibility PokazBool
        {
         set
           {
            _pokazbool = value;
                RisePropertyChanged("PokazBool");
            }
         get
         {
          return _pokazbool;
          }
       }

        private Visibility _pokazliste;
        public Visibility PokazListe
        {
            set
            {
                _pokazliste = value;
                RisePropertyChanged("PokazListe");
            }
            get
            {
                return _pokazliste;
            }
        }
        AppConfig app;
        private ParametryLista _parametry;

        private bool _checkvalue;
        public bool CheckValue
        {
            get
            {
                return _checkvalue;
            }
            set
            {
                _checkvalue = value;
                RisePropertyChanged("CheckValue");

            }
        }
        public ParametryLista Parametr {
            get

            {
                return _parametry;
            }

             set
                {
                    _parametry = value;
                RisePropertyChanged("Parametr");
                }
            }

        private ObservableCollection<ParametryLista> _listadanych;
        public ObservableCollection<ParametryLista> ListaDanych {
            get
            {
                return _listadanych;
            }
            set
            {
                _listadanych = value;
                RisePropertyChanged("ListaDanych");

            }

        }
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        public ViewModelWartParam()
        {
            // NazwaParematru = "Parametr nazwa";
            PokazBool = Visibility.Visible;
            app = AppConfig.GetInstance;
            CloseCommand = new DelegateCommand<Window>(Closeform);
             SaveCommand = new DelegateCommand(Save);
            ListaDanych = new ObservableCollection<ParametryLista>();
          Messenger.Default.Register<IHP_PARAMETRY_EX>(this, OnMessageParam);
            SzerokoscBool = 160;


        }
        private IHP_PARAMETRY _param;
        private string _id;
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                RisePropertyChanged("ID");
            }
        }

        public void Save()
        {
            try
            {
                try
                {
                    if (_param != null)
                    {
                        if (_param.RODZAJ == 1)
                        {
                            if (CheckValue)
                                _param.WARTOSC = "0";
                            if (!CheckValue)
                                _param.WARTOSC = "1";
                        }
                        if (_param.RODZAJ == 2)
                        {
                            _param.WARTOSC = Parametr.Wartosc;
                        }
                        Messenger.Default.Send<IHP_PARAMETRY>(_param);

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
                    throw;
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                    MessageBox.Show(e.InnerException.ToString());
                else
                    MessageBox.Show(e.Message.ToString());
            }
        }

        public void OnMessageParam(IHP_PARAMETRY_EX param)
    {
            _param = context.IHP_PARAMETRY.FirstOrDefault(x=> x.ID_IHP_PARAMETRY == param.ID_IHP_PARAMETRY);


            ListaDanych.Clear();

            //AppSettingsUstawieniaAplikacjiParametrWartosci[]
            foreach (AppSettingsUstawieniaAplikacjiParametr item in app.UstawieniaAplikacji.UstawieniaAplikacji.Parametry.Where(x => x.IdIhpParametry == _param.ID_IHP_PARAMETRY))
            {
                foreach (AppSettingsUstawieniaAplikacjiParametrWartosci item2 in item.Wartosci)
                {
                    ParametryLista lst = new ParametryLista()
                    {
                        ID = item.IdIhpParametry,
                        Opis = item2.opis,
                        Wartosc = item2.Wartosc
                    };
                    ListaDanych.Add(lst);
                }

            }

            ID = _param.WARTOSC;                
            NazwaParematru = _param.PARAMETR;
            if(_param.RODZAJ==1)
            {
                
                PokazListe =   Visibility.Hidden;
                PokazBool = Visibility.Visible;
                if(_param.WARTOSC=="0")
                {
                 CheckValue = true;
                }
                if (_param.WARTOSC == "1")
                {
                    CheckValue = false;
                }
            }

            if(_param.RODZAJ == 2)
            {
                SzerokoscBool = 0;
                PokazListe = Visibility.Visible;
                PokazBool = Visibility.Hidden;
            }
  }
    public void Closeform(Window window)
        {
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

        private string _nazwaparametru;
        public string NazwaParematru
        {
            get
            {
                return _nazwaparametru;
            }
            set
            {
                _nazwaparametru = value;
                RisePropertyChanged("NazwaParematru");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
   }
}


