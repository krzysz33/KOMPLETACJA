using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KpInfohelp.ModelViewsEx;

namespace KpInfohelp
{
 

    public class ViewModelOknoUzytkownikow : CrudVMBase, INotifyPropertyChanged
    {

       protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private IHP_ZAM_USERS_EX _currentuser;
        public IHP_ZAM_USERS_EX CurrentUser
        {
            get
            {
                return _currentuser;
            }
            set
            {
                _currentuser = value;
                RisePropertyChanged("CurrentUser");
            }

        }
        List<ViewStatusy> lstUsersSl;
        List<IHP_ZAM_USERS> lstUsers;
        private ObservableCollection<IHP_ZAM_USERS_EX> _observlstuserssl;
        public ObservableCollection<IHP_ZAM_USERS_EX> ObservlstUsersSl
        {
            get
            {
                return _observlstuserssl;
            }
            set
            {
                _observlstuserssl = value;
                RisePropertyChanged("ObservlstUsersSl");
            }
        }
        private bool _ischangepass;
        public bool IsChangePass
        {
            get
            {
                return _ischangepass;
            }
            set
            {
                _ischangepass = value;
                RisePropertyChanged("IsChangePass");
            }
        }
       private ObservableCollection<ViewStatusy> _obslststatusy;
        public ObservableCollection<ViewStatusy> ObslstStatusy
        {
            get
            {
                return _obslststatusy;
            }
            set
            {
                _obslststatusy = value;
                RisePropertyChanged("ObslstStatusy");
            }
        }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public ICommand AddUserCommand { get; private set; }
        public ICommand UpdateUserCommand { get; private set; }
        public ICommand DelUserCommand { get; private set; }
        public ICommand  DodajStatCommand { get; private set; }
        public ICommand UsunStatCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }
        IHP_ZAM_USERS _loginuser;

        public ViewModelOknoUzytkownikow()
        {
       
           ObservlstUsersSl = new ObservableCollection<IHP_ZAM_USERS_EX>();
          //  ObservlstUsersSl.CollectionChanged += ObservlstUsersSl_CollectionChanged;
            ObslstStatusy = new ObservableCollection<ViewStatusy>();
            ObslstStatusy.CollectionChanged += ObslstStatusy_CollectionChanged;
            
             AddUserCommand = new DelegateCommand(AddUser);
            //   AddUserCommand = new DelegateCommand(Del);
            DodajStatCommand = new DelegateCommand(DodajStat);
            UsunStatCommand = new DelegateCommand(UsunStat);
             UpdateUserCommand = new DelegateCommand(UpdateUser);
           ClearCommand = new DelegateCommand(Clear);
            Messenger.Default.Register<IHP_ZAM_USERS>(this, OnMessageUser);
            Refresh();
        }
        public void OnMessageUser(IHP_ZAM_USERS user)
        {
            _loginuser = user;

        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _nazwauser;
        public String NazwaUser
        {
            get
            {
                return _nazwauser;
            }
            set
            {
                _nazwauser = value;
                RisePropertyChanged("NazwaUser");
            }
        }

        private string _user;
        public String User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                RisePropertyChanged("User");
            }
        }

        private string _statusnazwa;
        public String StatusNazwa
        {
            get
            {
                return _statusnazwa;
            }
            set
            {
              _statusnazwa = value;
             RisePropertyChanged("StatusNazwa");
          }
        }

        private string _password;
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
        private void refreshstatus(int IdUser)
        {
            //ObslstStatusy.Clear();

            //var qu = from K in context.DEFSTATUS
            //         from N in context.WYSTSTATUS
            //            .Where(o => K.ID_DEFSTATUS == o.ID_DEFSTATUS).Where(x => x.ID_ARIT_ZAM_USERS == IdUser)
            //               .DefaultIfEmpty()

            //         select new
            //         {
            //             ID_ARIT_ZAM_USERS = (int?) N.ID_ARIT_ZAM_USERS ?? 0,
            //               ID_DEFSTATUS = K.ID_DEFSTATUS,
            //                K.NAZWA
            //         };

            //foreach (var q in qu)
            //{
            //    ViewStatusy zvl = new ViewStatusy();
            //    zvl.ID_IHP_ZAM_USERS = q.ID_IHP_ZAM_USERS;
            //    zvl.ID_DEFSTATUS = q.ID_DEFSTATUS;
            //    zvl.NAZWA = q.NAZWA;
            //    ObslstStatusy.Add(zvl);
            //}

        }
        private void ObslstStatusy_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ViewStatusy item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }
        }
        private void ObservlstUsersSl_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (IHP_ZAM_USERS_EX item in e.NewItems)
                {
                    item.PropertyChanged += Item_PropertyChangedUsers;
                }
            }
        }

        private void Refresh()
        {
            lstUsers = context.IHP_ZAM_USERS.ToList();
            ObservlstUsersSl.Clear();
          foreach (IHP_ZAM_USERS item in lstUsers)
            {
                IHP_ZAM_USERS_EX itemex = new IHP_ZAM_USERS_EX(context, item);
                ObservlstUsersSl.Add(itemex);
            }
        }
        private void DodajStat()
        {
         //   string LastMessage = string.Empty;
         //if (!string.IsNullOrEmpty(_statusnazwa))
         //   {
         //       try
         //       {
         //           DEFSTATUS ds = new DEFSTATUS();
         //       ds.ID_DEFSTATUS=  GetNextNumer(8);
         //       ds.NAZWA = _statusnazwa;
         //       Context.DEFSTATUS.Add(ds);
         //       Context.SaveChanges();
         //       }
         //       catch (Exception ex)
         //       {
         //           LastMessage = ex.ToString();
         //           LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
         //           throw ex;
         //       }
         //   }
         //   refreshstatus(_currentuser.ID_ARIT_ZAM_USERS);
        }
        private void UsunStat()
        {
            //string LastMessage = string.Empty;
            //try
            //{
            //    if (Context.WYSTSTATUS.Any(x => x.ID_DEFSTATUS == _currenstatus.ID_DEFSTATUS))
            //    {
            //        MessageBoxService.ShowMessage("Status wykorzystany u innego uzytkownika");
            //        return;
            //    }
            //    if (Context.POZ.Any(x => x.ID_DEFSTATUS == _currenstatus.ID_DEFSTATUS))
            //    {
            //        MessageBoxService.ShowMessage("Status na pozycji dokumentu");
            //        return;
            //    }
            //     DEFSTATUS ds = Context.DEFSTATUS.FirstOrDefault(x=>x.ID_DEFSTATUS == _currenstatus.ID_DEFSTATUS);
            //        Context.DEFSTATUS.Remove(ds);
            //        Context.SaveChanges();
            //    }
            //    catch (Exception ex)
            //    {
            //        LastMessage = ex.ToString();
            //        LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //        throw ex;
            //    }
            //  refreshstatus(_currentuser.ID_ARIT_ZAM_USERS);
        }

        private void DelWystStatus(int ID_ARIT_ZAM_USERS, int ID_DEFSTRATUS)
        {
            //WYSTSTATUS rw = Context.WYSTSTATUS.FirstOrDefault(f => f.ID_ARIT_ZAM_USERS == ID_ARIT_ZAM_USERS || f.ID_DEFSTATUS== ID_DEFSTRATUS);

            //if (rw != null)
            //{
            //    Context.WYSTSTATUS.Remove(rw);
            //    Context.SaveChanges();
            //}
        }
        private void Item_PropertyChangedUsers(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string LastMessage;
            try
            {
                IHP_ZAM_USERS_EX row = (IHP_ZAM_USERS_EX)sender;
             
                    AktRec(row);           
       
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        private  void  AktRec(IHP_ZAM_USERS_EX Item )
        {
            IHP_ZAM_USERS it = context.IHP_ZAM_USERS?.FirstOrDefault(w => w.ID_IHP_ZAM_USERS == Item.ID_IHP_ZAM_USERS);
            if(it!=null)
            {
                if (Item.REJWAGAEX == false)
                    Item.REJWAGA = 0;
                if (Item.REJWAGAEX == true)
                    Item.REJWAGA = 1;

                if (Item.USLUGAEX == false)
                    Item.USLUGA = 0;
                if(Item.USLUGAEX == true)
                       Item.USLUGA = 1;

                if (Item.KIEROWCYEX == false)
                    Item.KIEROWCY = 0;
                if (Item.KIEROWCYEX == true)
                    Item.KIEROWCY = 1;

                if (Item.KARTOTEKIEX == false)
                    Item.KARTOTEKI = 0;
                if (Item.KARTOTEKIEX == true)
                    Item.KARTOTEKI = 1;

                if (Item.POJAZDYEX == false)
                    Item.POJAZDY = 0; 
                if (Item.POJAZDYEX == true)
                    Item.POJAZDY = 1;

                if (Item.KONTRAHENTEX == false)
                    Item.KONTRAHENT = 0;
                if (Item.KONTRAHENTEX == true)
                    Item.KONTRAHENT = 1;

                if (Item.DANEFIRMYEX == false)
                    Item.DANEFIRMY = 0;
                if (Item.DANEFIRMYEX == true)
                    Item.DANEFIRMY = 1;
            if(_ischangepass)
                it.HASLO = PasswordManager.Encrypt(Password);
              //   it.LOGIN = Item.LOGIN;
              //    it.ID_UZYTKOWNIK = Item.ID_UZYTKOWNIK;
                it.KARTOTEKI = Item.KARTOTEKI;
                it.KIEROWCY = Item.KIEROWCY;
                it.KONTRAHENT = Item.KONTRAHENT;
                it.POJAZDY = Item.POJAZDY;
                it.REJWAGA = Item.REJWAGA;
                it.RESET_HASLA = Item.RESET_HASLA;
                it.USLUGA = Item.USLUGA;
                it.DANEFIRMY = Item.DANEFIRMY;
                it.AKTYWNY = Item.AKTYWNY;
                context.SaveChanges();
            }
        }
        private   int GetNextNumer(int iddlatabeli)
        {
            int _statushistId = 0;
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = GetId(8);
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
        private   IHP_NUMERACJA GetId(int dlatabeli)
        {
            return context.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == dlatabeli);
       }

        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //string LastMessage;
            //try
            //{
            //    ViewStatusy row = (ViewStatusy)sender;

            //    if (!row.Zazn)
            //        DelWystStatus(_currentuser.ID_ARIT_ZAM_USERS, row.ID_DEFSTATUS);
            //    else
            //    {
            //        WYSTSTATUS rw = new WYSTSTATUS();
            //        {
            //            rw.ID_WYSTSTATUS = GetNextNumer(8);
            //            rw.ID_ARIT_ZAM_USERS = _currentuser.ID_ARIT_ZAM_USERS;
            //            rw.ID_DEFSTATUS = row.ID_DEFSTATUS;
            //        }

            //        Context.WYSTSTATUS.Add(rw);
            //        Context.SaveChanges();
                }


        private bool checkData(IHP_ZAM_USERS Item)
        {
            if (string.IsNullOrEmpty(Item.NAZWISKO_IMIE))
            {
                MessageBoxService.ShowMessage("Nazwa pusta");

                return false;
            }

            if (string.IsNullOrEmpty(Item.LOGIN))
            {
                MessageBoxService.ShowMessage("Login pusty");
                return false;
            }

            if (string.IsNullOrEmpty(Item.HASLO))
            {
                MessageBoxService.ShowMessage("Hasło puste");
                return false;
            }
            return true;
        }
       private void AddUser()
     {
       string LastMessage;
         try
        {
            IHP_ZAM_USERS addtuser = new IHP_ZAM_USERS()
            {
                ID_IHP_ZAM_USERS = GetNextNumer(10),
                ID_UZYTKOWNIK = 0,
                LOGIN = _user,
                HASLO = PasswordManager.Encrypt(_password),
                NAZWISKO_IMIE = _nazwauser,
                AKTYWNY = 0,
                RESET_HASLA = 0
            };
            if (checkData(addtuser))
            {
                context.IHP_ZAM_USERS.Add(addtuser);
                context.SaveChanges();
            }
 
        //    ObservlstUsersSl.Add(addtuser);
            Clear();
                Refresh();
            }

        catch (Exception ex)
        {
            LastMessage = ex.ToString();
            LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            throw ex;
        }
    }
       private void UpdateUser()
        {
            string LastMessage;


            if(CurrentUser != null)
            {
            if(CurrentUser.ID_IHP_ZAM_USERS==1)
                {
                    MessageBoxService.ShowMessage("Administrator nie można zmieniać!");
                    return;
                }
           }
         try
            {
                if (_currentuser != null)
                {
                    AktRec(_currentuser);
                    Clear();
                    Refresh();
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
                        //    MessageBoxService.ShowMessage(String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        void Clear()
        {
            NazwaUser = 
            User =  
            Password = string.Empty;
            CurrentUser = null;
            IsChangePass = false;
        }
        private void Del()
        {
            string LastMessage;
            try
            {
                if (_currentuser != null)
                {
                    if (_currentuser.LOGIN == "admin")
                    {
                        MessageBoxService.Show("Administratora nie można skasować");
                        return;
                    }

                    var local = context.IHP_ZAM_USERS.FirstOrDefault(f => f.ID_IHP_ZAM_USERS == _currentuser.ID_IHP_ZAM_USERS);

                    if (local != null) //It’s in the local!
                    {
                        context.Entry(local).State = EntityState.Detached;
                        if (context.Entry(local).State == EntityState.Detached)
                        {
                            context.IHP_ZAM_USERS.Attach(local);
                            context.IHP_ZAM_USERS.Remove(local);
                            context.SaveChanges();
                            //    refresh();
                            //   Clear();
                        }
                    }
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

        //  private void WypelnijDane()
        //   {
        //if (_currentuser != null)
        //{
        //    NazwaUser = _currentuser.NAZWISKO_IMIE;
        //    User = _currentuser.LOGIN;
        //    Password = _currentuser.HASLO;
        //}

        //  }

         }
    }
