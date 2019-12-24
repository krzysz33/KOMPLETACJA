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
   public  class ViewModelWygladOkien : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
        private bool CheckValue;
        private string WartoscListy;
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
        private ObservableCollection<IHP_PARAMETRY> _parametrywazenie;
        public ObservableCollection<IHP_PARAMETRY> ParametryWazenie
        {
            get
            {
                return _parametrywazenie;
            }
            set
            {
                _parametrywazenie = value;
                RisePropertyChanged("ParametryWazenie");
            }
        }
        private ObservableCollection<IHP_PARAMETRY_EX> _parametrywyglad;
        public ObservableCollection<IHP_PARAMETRY_EX> lstParametryWyglad
        {
            get
            {
                return _parametrywyglad;
            }
            set
            {
                _parametrywyglad = value;
                RisePropertyChanged("lstParametryWyglad");
            }
        }

        private bool _isexpanded;
        public bool IsExpanded {
            get
            {
                return _isexpanded;
            }
                    
                set

            {
                _isexpanded = value;
                RisePropertyChanged("IsExpanded");

            }
        }

        private IHP_PARAMETRY _parametrwazenie;
        public   IHP_PARAMETRY ParametrWazenie {
            get
            {
                return _parametrwazenie;
            }
            set

            {
                _parametrwazenie = value;
                OnWazenieSet();

                RisePropertyChanged("ParametrWazenie"); 
            }
      }
        private IHP_PARAMETRY_EX _parametrwazeniex;
        public IHP_PARAMETRY_EX ParametrWazenieEx
        {
          get
           {
              return _parametrwazeniex;
           }
          set
            {
                _parametrwazeniex = value;
                    RisePropertyChanged("ParametrWazenieEx");
            }
        }
        private void  OnWazenieSet()
        {
             if(_parametrwazenie!=null)
            {
              // _paramex = new IHP_PARAMETRY_EX(context, _parametrwazenie);
            }
}
        public ViewModelWygladOkien()
        {
            Messenger.Default.Register<IHP_PARAMETRY>(this, OnMessageSave);
            CommandGridDoubleClick = new DelegateCommand(PokazOknParam);
            lstParametryWyglad = new ObservableCollection<IHP_PARAMETRY_EX>();
            LoadCollection();
        }
        public void OnMessageSave(IHP_PARAMETRY item)
        {
            ParametrWazenie = context.IHP_PARAMETRY.FirstOrDefault(x => x.ID_IHP_PARAMETRY == item.ID_IHP_PARAMETRY);
            ParametrWazenie.WARTOSC = item.WARTOSC;
            // context.IHP_PARAMETRY.Attach(item);
            context.Entry(ParametrWazenie).State = EntityState.Modified;
            context.SaveChanges();
            LoadCollection();
            //    RisePropertyChanged("ParametryWazenieEx");
            ParametrWazenieEx = lstParametryWyglad.FirstOrDefault(x => x.ID_IHP_PARAMETRY == item.ID_IHP_PARAMETRY);
       //     RisePropertyChanged("ParametrWazenieEx");


        }
        void LoadCollection()
        {
            lstParametryWyglad.Clear();
           List<IHP_PARAMETRY> _parwazenia = new ObservableCollection<IHP_PARAMETRY>(context.IHP_PARAMETRY.Where(x => x.ID_GRUPAPARAMETRY == 2)).ToList();
            foreach(IHP_PARAMETRY item in _parwazenia)
            {
              IHP_PARAMETRY_EX _paramex = new IHP_PARAMETRY_EX(item);
                lstParametryWyglad.Add(_paramex);
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
        public ICommand CommandGridDoubleClick { get; private set; }
        private void PokazOknParam()
        {
            if(_parametrwazeniex != null)
            {
                if(_parametrwazeniex.LpMain==0)
                {
                    WartParam frm = new WartParam();
                    Messenger.Default.Send<IHP_PARAMETRY_EX>(_parametrwazeniex);
                    frm.ShowDialog();
                     IsExpanded = true;
                }
            }
        }
        public void Save()
        {
            try
            {
                try
                {
                    if (ParametrWazenie != null)
                    {
                        if (ParametrWazenie.RODZAJ == 1)
                        {

                            if (CheckValue)
                                ParametrWazenie.WARTOSC = "0";
                            if (!CheckValue)
                                ParametrWazenie.WARTOSC = "1";
                        }

                        if (ParametrWazenie.RODZAJ == 2)
                        {
                            ParametrWazenie.WARTOSC = WartoscListy;
                        }
                       context.Entry(ParametrWazenie).State = EntityState.Modified;
                        context.SaveChanges();
                        LoadCollection();

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

    }

}


