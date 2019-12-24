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
    [POCOViewModel]
    class ViewModelProgramSettings : CrudVMBase,INotifyPropertyChanged, IMVVMDockingProperties
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
        private bool _isclosedbazadanych;
        public bool  IsClosedBazaDanych
        {
            get
            {
                return _isclosedbazadanych;
            }
            set
            {
                _isclosedbazadanych = value;
                RisePropertyChanged("IsClosedBazaDanych");
            }
        }
        private bool _isclosedkonfwazenia;
        public bool IsClosedKonfWazenia
        {
            get
            {
                return _isclosedkonfwazenia;
            }
            set
            {
                _isclosedkonfwazenia = value;
                RisePropertyChanged("IsClosedKonfWazenia");
            }
        }
        private bool _isoknouzytkownikow;
        public bool IsClosedOknoUzytkownikow
        {
            get
            {
                return _isoknouzytkownikow;
            }
            set
            {
                _isoknouzytkownikow = value;
                RisePropertyChanged("IsClosedOknoUzytkownikow");
            }
        }
        private bool _isclosedmierniki;
        public bool IsClosedMierniki
        {
            get
            {
                return _isclosedmierniki;
            }
            set
            {
                _isclosedmierniki = value;
                RisePropertyChanged("IsClosedMierniki");
            }
        }
        private bool _isclosedrestserwer;
        public bool IsClosedRestSerwer
        {
            get
            {
                return _isclosedrestserwer;
            }
            set
            {
                _isclosedrestserwer = value;
                RisePropertyChanged("IsClosedRestSerwer");
            }
        }
        private bool _isclosedsystemerp;
        public bool IsClosedSystemErp
        {
            get
            {
                return _isclosedsystemerp;
            }
            set
            {
                _isclosedsystemerp = value;
                  RisePropertyChanged("IsClosedSystemErp");

            }
        }
        private bool _isclosedKamera1Settings;
        public bool IsClosedKamera1Settings
        {
            get
            {
                return _isclosedKamera1Settings;
            }
            set
            {
                _isclosedKamera1Settings = value;
                RisePropertyChanged("IsClosedKamera1Settings");

            }
        }

        private bool _isclosedKamera2Settings;
        public bool IsClosedKamera2Settings
        {
            get
            {
                return _isclosedKamera2Settings;
            }
            set
            {
                _isclosedKamera2Settings = value;
                RisePropertyChanged("IsClosedKamera2Settings");

            }
        }
        private bool _isclosedKamera3Settings;
        public bool IsClosedKamera3Settings
        {
            get
            {
                return _isclosedKamera3Settings;
            }
            set
            {
                _isclosedKamera3Settings = value;
                RisePropertyChanged("IsClosedKamera3Settings");

            }
        }

        private bool _iscloseddanefirmy;
        public bool IsClosedDaneFirmy
        {
            get
            {
                return _iscloseddanefirmy;
            }
            set
            {
                _iscloseddanefirmy = value;
                RisePropertyChanged("IsClosedDaneFirmy");
            }
        }
        private bool _isclosedlicencja;
        public bool IsClosedLicencja
        {
            get
            {
                return _isclosedlicencja;
            }
            set
            {
                _isclosedlicencja = value;
                RisePropertyChanged("IsClosedLicencja");
            }
        }

        private bool _iswygladokien;
       public bool IsWygladOkien
        {
            get
            {
                return _iswygladokien;
            }
            set
            {
                _iswygladokien = value;
                        RisePropertyChanged("IsWygladOkien");
            }
        }


        public CommandHandler UstawPanel { get; private set; }
        public int SelectedGrupaKartID { get; set; }
        public virtual ViewModelLicencja OpenFileDialogViewModel { get; protected set; }

        public ViewModelProgramSettings()
        {
           UstawPanel = new CommandHandler(Ustaw,true);
           Messenger.Default.Register<IHP_ZAM_USERS>(this, OnMessageUser);
           ZamknijPanele();
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
        public void ZamknijPanele()
        {
            IsClosedSystemErp =
            IsClosedBazaDanych =
            IsClosedRestSerwer =
            IsClosedMierniki = 
            IsClosedDaneFirmy = 
            IsClosedKonfWazenia = 
            IsClosedKamera1Settings=
            IsClosedKamera2Settings =
            IsClosedKamera3Settings =
            IsClosedLicencja =
            IsWygladOkien=
            IsClosedOknoUzytkownikow = true;
            if (ProgramDataSotrage.User.ID_IHP_ZAM_USERS == 1)
                                           ShowPermmissions = true;
        }
        private void Ustaw(object value)
        {
            bool IsClosedO = false;
            ZamknijPanele();
            if (_user != null)
            {
                if (_user.ID_IHP_ZAM_USERS == 1)
                {
                    IsClosedO = false;
                }
                else
                {
                    IsClosedO = true;
                }
            }
            int IdPanel = System.Convert.ToInt32(value);
            switch (IdPanel)
            {
                case 1:
                    {
                        IsClosedBazaDanych = false;
                        break;
                    }
                case 2:
                    {
                        IsClosedRestSerwer = false;
                        break;
                    }
                case 3:
                    {
                        IsClosedKonfWazenia = false;
                        break;
                    }

                case 4:
                    {
                        IsWygladOkien = false;
                        break;
                    }
         case 5:
                    {
                        IsClosedOknoUzytkownikow = IsClosedO;
                        break;
                    }
                case 6:
                    {
                        IsClosedKamera1Settings = false;
                        break;
                    }
                case 7:
                    {
                        IsClosedDaneFirmy = false;
                        break;
                    }
                case 11:
                    {
                        IsClosedMierniki = false;
                        break;
                    }
                case 12:
                    {
                        IsClosedLicencja = false;
                        break;
                    }

                case 13:
                    {
                        IsClosedKamera2Settings = false;
                        break;
                    }

                case 14:
                    {
                        IsClosedKamera3Settings = false;
                        break;
                    }
            }
        }
        private  IHP_ZAM_USERS _user;
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
        public void OnMessageUser(IHP_ZAM_USERS user)
        {
            _user = user;

        }
        private bool CanUpdate()
        {
                return true;
        }
        void Update()
        {
   
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
                RisePropertyChanged("RodzajDok");
            }
        }
    
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _showpermmissions;
        public bool ShowPermmissions
        {
            get
            {
                return _showpermmissions;

            }
            set
            {
                _showpermmissions = value;
                RisePropertyChanged("ShowPermmissions");
            }
        }
    }

    public class KierunekMagConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
         {
            int input = System.Convert.ToInt32(value);
            switch (input)
            {
                case 1:
                    return "Przyjęcie";

                case -1:
                    return "Rozchód";

                default:
                    return "Przyjęcie";
            }
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}