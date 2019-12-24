using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Mvvm;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading;
using System.Data.SqlClient;

namespace KpInfohelp
{ 
    public class CrudVMBase : ViewModelBase, INotifyPropertyChanged
    {
        public WagaRamka _ramka;
       
        ICommand navigateCommand;
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
   
    
        public KOMPLETACJAEntities context;
        protected void HandleCommand(CommandMessage action) 
        { 
            if (isCurrentView) 
            { 
                switch (action.Command) 
                { 
                    case CommandType.Insert: 
                        break; 
                    case CommandType.Edit: 
                        break; 
                    case CommandType.Delete: 
                        DeleteCurrent(); 
                        break; 
                    case CommandType.Commit: 
                        CommitUpdates(); 
                        break; 
                    case CommandType.Refresh: 
                        RefreshData(); 
                        break; 
                    default: 
                        break; 
                } 
            } 
        } 
        private Visibility throbberVisible = Visibility.Visible; 
        public Visibility ThrobberVisible 
        { 
            get { return throbberVisible; } 
            set 
            { 
                throbberVisible = value; 
                RaisePropertyChanged(); 
            } 
        } 
        private string errorMessage; 
         public string ErrorMessage 
        { 
            get { return errorMessage; } 
            set  
            {  
                errorMessage = value; 
                RaisePropertyChanged(); 
            } 
        } 
        protected virtual void CommitUpdates(){} 
        protected virtual void DeleteCurrent(){} 
        protected virtual void RefreshData() 
        { 
            GetData(); 
            Messenger.Default.Send<UserMessage>(new UserMessage {Message="Data Refreshed" }); 
        } 
        protected virtual void GetData(){}
        public IHP_NUMERACJA GetId(int dlatabeli)
        {
            return context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);
        }
        public string GetNumerDok(int nrdok)
        {
            string prefx = "W/";
            return prefx + nrdok.ToString();
        }
        public int GetNextNumer(int iddlatabeli)
        {
            int _statushistId = 0;
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = GetId(iddlatabeli);
                numerpoz.NUMER++;
                _statushistId = numerpoz.NUMER;
                context.IHP_NUMERACJA.Add(numerpoz);
                context.Entry(numerpoz).State = EntityState.Modified;
                context.SaveChanges();
            }

            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            return _statushistId;
        }

        protected CrudVMBase() 
        {
            try
            {
                getConnection();
                //  CheckConnection();
                GetData();
                Messenger.Default.Register<CommandMessage>(this, (action) => HandleCommand(action));
                Messenger.Default.Register<NavigateMessage>(this, (action) => CurrentUserControl(action));
            }
            catch (Exception ex)
            {
                MessageBoxService.ShowMessage(ex.Message);
                throw ex;
             
            }
        }
        public  List<IHP_PARAMETRY> ParametryLista
        {
            get
            {
                return context.IHP_PARAMETRY.ToList();
            }
        }
        public IHP_PARAMETRY GetParam(int ID_IHP_PARAMETRY)
        {
            return context.IHP_PARAMETRY.FirstOrDefault(x => x.ID_IHP_PARAMETRY == ID_IHP_PARAMETRY);

        }
        public List<IHP_PARAMETRY> GetParams(int ID_GRU_PARAMETRY)
        {
            return context.IHP_PARAMETRY.Where(x => x.ID_GRU_PARAMETRY == ID_GRU_PARAMETRY).ToList();
        }
        public   bool CheckConnection()
        {
            try
            {
                 context = new KOMPLETACJAEntities();
                 context.Database.Connection.Open();
                 context.Database.Connection.Close();
            }
            catch (SqlException ex)
            {
                return false;
            }
            return true;
        }
        public void getConnection()
        {
            AppConfig aa = AppConfig.GetInstance;
            try
            {
                //string connection = aa.ConnectionString();
                //if (aa.TestConnection())
                //{
                    //string Connectt = aa.ConnectionString();
                 context = new KOMPLETACJAEntities();
                //}

                //else // brak polaczenia z baza pokazac tylo forme do polacznia
                //{
       
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }

    }
        protected bool isCurrentView = false;
        private void CurrentUserControl(NavigateMessage nm) 
        { 
            if (this.GetType() == nm.ViewModelType) 
            { 
                isCurrentView = true; 
            } 
            else 
            { 
                isCurrentView = false; 
            } 
        }
        protected virtual void OnViewLoaded() { }
  
        public ICommand NavigateCommand
        {
            get
            {
                if (navigateCommand == null)
                    navigateCommand = new DelegateCommand<string>(Navigate);
                return navigateCommand;
            }
        }
        public void Navigate(string target)
        {
            NavigationService.Navigate(target, null, this);
        }
        protected INavigationService NavigationService { get { return GetService<INavigationService>(); } }
    }
    public   class NavigateMessage
    {
        public Type ViewType { get; set; }
        public Type ViewModelType { get; set; }
        public UserControl View { get; set; }
    }
} 
 