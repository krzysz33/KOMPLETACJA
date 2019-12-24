using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KpInfohelp
{
   
    public class ViewModelLogin : CrudVMBase, INotifyPropertyChanged
    {
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        IWindowService WindowService { get { return GetService<IWindowService>(); } }
       public virtual ICurrentWindowService CurrentWindowService { get { return null; } }
      public virtual IMainWindowService MainWindowService { get { return null; } }
       public ICommand OpenMainWindowCommand { get; private set; }

        string _username, _password;
  
     public ViewModelLogin()
      {
               _uzytkownik = new IHP_ZAM_USERS();
                OpenMainWindowCommand = new DelegateCommand(OpenWindowCommandExecute);
                Username = "admin";
                Password = "admin";
      }

        public IHP_ZAM_USERS _uzytkownik { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                RisePropertyChanged("Username");
             }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RisePropertyChanged("Password");
            }
        }
 
        public void SearchByKey(KeyEventArgs args)
        {
            OpenWindowCommandExecute();
        }
        public bool CanSearchByKey(KeyEventArgs args)
        {
            return (args.Key == Key.Enter) && !string.IsNullOrEmpty(Password);
        }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public IHP_ZAM_USERS DoLogin(string login, string password)
        {
            IHP_ZAM_USERS res;
            password = PasswordManager.Encrypt(password);
            try
            {
                  res = context.IHP_ZAM_USERS?.FirstOrDefault(x => x.LOGIN == login && x.HASLO == password);
                if (res != null)
                    CreateIfMissing(login);
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
            return res;
        }
        //public void CreateIfMissing(string login)
        //{
        //    FileInfo fileInfo = new FileInfo(Environment.CurrentDirectory);
        //    ProgramDataSotrage.ProfilePath = fileInfo.ToString() + "\\Profile\\" + login + "\\";
        //    string sourceDir = fileInfo.ToString() + "\\Profile\\defaultuser\\";


        //    bool folderExists = Directory.Exists(ProgramDataSotrage.ProfilePath);
        //    if (!folderExists)
        //    {
        //        Directory.CreateDirectory(sourceDir);
        //        foreach (var file in Directory.GetFiles(sourceDir))
        //            File.Copy(file, Path.Combine(sourceDir, Path.GetFileName(file)));
        //    }
        //}

        public void CreateIfMissing(string login)
        {
            FileInfo fileInfo = new FileInfo(Environment.CurrentDirectory);
            ProgramDataSotrage.ProfilePath = fileInfo.ToString() + "\\Profile\\" + login + "\\";

            string sourceDir = fileInfo.ToString() + "\\Profile\\defaultuser\\";

            bool folderExists = Directory.Exists(ProgramDataSotrage.ProfilePath);
            if (!folderExists)
            {
                Directory.CreateDirectory(ProgramDataSotrage.ProfilePath);
                foreach (var file in Directory.GetFiles(sourceDir))
                    File.Copy(file, Path.Combine(ProgramDataSotrage.ProfilePath, Path.GetFileName(file)));
            }
        }
        void OpenWindowCommandExecute()
        {
            if(Username==string.Empty)
            {
                MessageBoxService.ShowMessage("Wpisz Użytkownika!");
                return;
            }
            if (Password == string.Empty)
            {
                MessageBoxService.ShowMessage("Wpisz Hasło");
                return;
            }
            IHP_ZAM_USERS user = DoLogin(Username, Password);
              if(user!=null)
            {
                 ProgramDataSotrage.User = user;

                MainWindowService.ShowMainWindow();

                //var w = new JaednDodatPodpowiedz();
                //w.ShowDialog();

                Messenger.Default.Send<IHP_ZAM_USERS>(user);
                CurrentWindowService.Close();
            }
              else
            {
                MessageBoxService.ShowMessage("Niepoprawny Użytkownik lub Hasło!");
            }

        }
        public string Title { get { return "Login ViewModel"; } }

        public void Login(bool isCorrect)
        {
      //      if (isCorrect)
            //    MainWindowService.ShowMainWindow();
        //    CurrentWindowService.Close();
        }

    }
}
