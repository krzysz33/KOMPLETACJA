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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    public class ViewModelBazyDanych : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
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
    
        const string FirstConnectionString = "metadata = res://*/WagaDuza.csdl|res://*/WagaDuza.ssdl|res://*/WagaDuza.msl;provider=System.Data.SqlClient;provider connection string=";
        private string _databasename, _databasepass,_databaseuser, _remoteadrr;
        private string connectionString;
        public string DatabaseName 
            {
               get
            {
                return _databasename;
            } 
            set
            {
                _databasename = value;
                RisePropertyChanged("DatabaseName");
            }
          }
        public string DatabaseUser
        {
            get
            {
                return _databaseuser;
            }
            set
            {
                _databaseuser = value;
                RisePropertyChanged("DatabaseUser");
            }
        }
        public string DatabasePass
        {
            get
            {
                return _databasepass;
            }
            set
            {
                _databasepass = value;
                RisePropertyChanged("DatabasePass");
            }
        }
        private string _databasecatalog;
        public string DatabaseCatalog
        {
            get
            {
                return _databasecatalog;
            }
            set
            {
                _databasecatalog = value;
                RisePropertyChanged("DatabaseCatalog");
            }
        }
        public string RemoteAdrr
        {
            get
            {
                return _remoteadrr;
            }
            set
            {
                _remoteadrr = value;
                RisePropertyChanged("RemoteAdrr");
            }
        }
        private bool _integratedsecuritysql;
        public bool IntegratedSecuritySql
        {
            get
            {
                return _integratedsecuritysql;
            }
            set
            {
                _integratedsecuritysql = value;

                RisePropertyChanged("IntegratedSecuritySql");
            }
        }
        private bool _integratedsecuritywin;
        public bool IntegratedSecurityWin
        {
            get
            {
                return _integratedsecuritywin;
            }
            set
            {
                _integratedsecuritywin = value;
                  RisePropertyChanged("IntegratedSecurityWin");
            }
        }
        private bool _locaserwer;
        public bool LocalSerwer
        {
            get
            {
                return _locaserwer;
            }
            set
            {
                _locaserwer = value;
                if (_locaserwer)
                {
                    LayOutIsEnabledRemoteHost = false;
                }
                else
                {
                    LayOutIsEnabledRemoteHost = true;
                }
                RisePropertyChanged("LocalSerwer");
            }
        }
        private bool _layremotehost;
        public bool LayOutIsEnabledRemoteHost {
            get
            {
                return _layremotehost;
            }
               set
            {
                _layremotehost = value;
                RisePropertyChanged("LayOutIsEnabledRemoteHost");
            }
                
            }
        public ICommand ClearCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand TestCommand { get; private set; }
        AppConfig appconfig = AppConfig.GetInstance;
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
       public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelBazyDanych()
        {
        
           ClearCommand = new DelegateCommand(Clear);
           UpdateCommand = new DelegateCommand(Update);
           TestCommand = new DelegateCommand(Test);
            LoadCollection();
        }
        void Clear()
        {
            RemoteAdrr = "127.0.0.1";
            DatabaseCatalog = "INFOHELP";
            DatabaseName = @"INFOHELP\SQLEXPRESS";
            DatabaseUser = "SA";
            DatabasePass = String.Empty;
            LocalSerwer = true;
        }
        void Update()
        {
          Zapisz();
          ZapiszConfig();
        }
        void Test()
        {
            string provider = "System.Data.SqlClient";
            bool integratedSecurity = false;
            if(IntegratedSecuritySql)
            {
                integratedSecurity = false;
            }
            if(IntegratedSecurityWin)
            {
                integratedSecurity = true;
            }
                  if(TestConnection2(provider, _databasename, _databasecatalog, _databaseuser, _databasepass, integratedSecurity))
            {
                MessageBoxService.ShowMessage("Połaczenie Ustawione");
                ZapiszConfig();
                Zapisz(); 
            }
                  else
            {
                MessageBoxService.ShowMessage("Połaczenie nie powiodło się");
                connectionString = String.Empty;
            }
        }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public void LoadCollection()
        {
            int _integratedsecuritysqlint = 0;
            int _localserwerint = 0;
            int _kopisabazydanych = 0;
            int _wyslijprzyzamknieciu = 0;
            AppSettingsBazaDanych BazaDanych = appconfig.UstawieniaAplikacji.BazaDanych;

            _remoteadrr = BazaDanych.HostZdalny;
            _databasecatalog = BazaDanych.KatalogBazyDanych;
            _databasename = BazaDanych.NazwaBazyDanych;
            _databaseuser = BazaDanych.UzytkownikBazy;
            _databasepass = BazaDanych.Haslo;
            _integratedsecuritysqlint = BazaDanych.RodzajAutoryzacji;
            _localserwerint = BazaDanych.LokalnySerwerBazyDanych;
            _kopisabazydanych = BazaDanych.KopiaPrzyZamknieciu;
            _wyslijprzyzamknieciu = BazaDanych.WyslijFtp;
            ftpHostField = BazaDanych.FtpHost;
            ftpUserField = BazaDanych.FtpUser;
            ftpPassField = BazaDanych.FtpPass ;

            if (_kopisabazydanych == 1)
            {
                KopiaBazy = true;
            }
            if (_wyslijprzyzamknieciu == 1)
            {
                WysylkaFtp = true;
            }

            if (_integratedsecuritysqlint==0)
            {
                _integratedsecuritysql = true;
                _integratedsecuritywin = false;
            }

            if (_integratedsecuritysqlint == 1)
            {
                _integratedsecuritysql = false;
                _integratedsecuritywin = true;
            }
            if(_localserwerint==0)
            {
                LocalSerwer = false;
            }
            else if(_localserwerint==1)
            {
                LocalSerwer = true;
            }
       }
        public void Zapisz()
        {
              AppSettingsBazaDanych BazaDanych = appconfig.UstawieniaAplikacji.BazaDanych;

            appconfig.UstawieniaAplikacji.BazaDanych.HostZdalny =  _remoteadrr;
            appconfig.UstawieniaAplikacji.BazaDanych.KatalogBazyDanych  = _databasecatalog;
            appconfig.UstawieniaAplikacji.BazaDanych.NazwaBazyDanych  = _databasename ;
            appconfig.UstawieniaAplikacji.BazaDanych.UzytkownikBazy = _databaseuser;
            appconfig.UstawieniaAplikacji.BazaDanych.Haslo  = _databasepass;

           if (IntegratedSecuritySql)
            {
                appconfig.UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji = 0;
            }

            if (IntegratedSecurityWin)
            {
                appconfig.UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji = 1;
            }

            if (LocalSerwer)
            {
                appconfig.UstawieniaAplikacji.BazaDanych.LokalnySerwerBazyDanych = 1;
            }

            else
                appconfig.UstawieniaAplikacji.BazaDanych.LokalnySerwerBazyDanych = 0;

            if (KopiaBazy)
            {
                appconfig.UstawieniaAplikacji.BazaDanych.KopiaPrzyZamknieciu = 1;
            }

            else
                appconfig.UstawieniaAplikacji.BazaDanych.KopiaPrzyZamknieciu = 0;

            appconfig.UstawieniaAplikacji.BazaDanych.FtpHost = ftpHostField;
            appconfig.UstawieniaAplikacji.BazaDanych.FtpUser = ftpUserField;
            appconfig.UstawieniaAplikacji.BazaDanych.FtpPass  = ftpPassField;

            appconfig.Zapisz();
        }
        public void ZapiszConfig()
        {
            if (!String.IsNullOrEmpty(connectionString))
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                connectionString = FirstConnectionString + connectionString;
                config.ConnectionStrings.ConnectionStrings["WagaDuzaModel"].ConnectionString = connectionString;
                config.Save(ConfigurationSaveMode.Modified, true);
                ConfigurationManager.RefreshSection("connectionStrings");
            }
        }
        public bool TestConnection(string provider, string serverName, string initialCatalog, string userId, string password, bool integratedSecurity)
        {
           //   connectionString = integratedSecurity ? string.Format("data source={0};initial catalog={1};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework", serverName, initialCatalog, provider)
             //                                         : string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};", provider, serverName, initialCatalog, userId, password);

            connectionString = integratedSecurity ? string.Format("metadata = res://*/WagaDuza.csdl|res://*/WagaDuza.ssdl|res://*/WagaDuza.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source={1};initial catalog={2};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework&quot;", serverName, initialCatalog, provider)
                                                              : string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};", provider, serverName, initialCatalog, userId, password);

            

           using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
             }
        }
        public   bool TestConnection2(string provider, string serverName, string initialCatalog, string userId, string password, bool integratedSecurity)
        {
       //     string OldConnection = ConfigurationManager.ConnectionStrings["entWagaDuza"].ConnectionString;
            try
            {
                connectionString = integratedSecurity ? string.Format("data source={0};initial catalog={1};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework&quot;", serverName, initialCatalog, provider)
                                                                   : string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};", provider, serverName, initialCatalog, userId, password);
              //  ZapiszConfig();
              //  Zapisz();
                if (context.Database.Connection.State == ConnectionState.Open)
                {
              
                    context.Database.Connection.Close();
                    context.Database.Connection.ConnectionString = connectionString;
                    context.Database.Connection.Open();
                }
                else
                {
                    context.Database.Connection.ConnectionString = connectionString;
                    context.Database.Connection.Open();
                }
               
            }
            catch (SqlException)
            {
            //    connectionString =  OldConnection;
            //    ZapiszConfig();
           //     Zapisz();
                return false;
            }
            return true;
        }
        private  bool IsServerConnected(string connectionString, bool integratedSecurity)
        {
            return true;
            //var connectionstr = integratedSecurity ? String.format("provider={0};data source={1};initial catalog={2};integrated security=sspi;", provider, servername, initialcatalog)
            //                                  : String.format("provider={0};data source={1};initial catalog={2};user id={3};password={4};", provider, servername, initialcatalog, userid, password);

            //using (SqlConnection connection = new SqlConnection(connectionstr))
            //{
            //    try
            //    {
            //        connection.open();
            //        return true;
            //    }
            //    catch (SqlException)
            //    {
            //        return false;
            //    }
            //}
        }

        private bool _kopiabazy;
        public bool KopiaBazy
        {
            get
            {
                return _kopiabazy;

            }
            set
            {
                _kopiabazy = value;
                RisePropertyChanged("KopiaBazy");
            }
        }

        private bool _wysylkaftp;
        public bool WysylkaFtp
        {
            get
            {
                return _wysylkaftp;

            }
            set
            {
                _wysylkaftp = value;
                RisePropertyChanged("WysylkaFtp");
            }
        }

        private string ftpHostField;
        public string   FtpHost
        {
            get
            {
                return ftpHostField;
            }
            set
            {
                ftpHostField = value;
                RisePropertyChanged("FtpHost");
            }
        }
        private string ftpUserField;
        public string FtpUser
        {
            get
            {
                return ftpUserField;
            }
            set
            {
                ftpUserField = value;
                RisePropertyChanged("FtpUser");
            }
        }
        private string ftpPassField;
        public string FtpPass
        {
            get
            {
                return ftpPassField;
            }
            set
            {
                ftpPassField = value;
                RisePropertyChanged("FtpPass");
            }
        }


    

    }

}
 