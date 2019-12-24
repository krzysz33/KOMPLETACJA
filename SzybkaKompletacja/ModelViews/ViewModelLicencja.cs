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
using DevExpress.Mvvm.UI;
using Microsoft.Win32;
using System.Windows.Markup;
using System.Windows.Data;
using Licencja;
using System.Data.Entity;


namespace KpInfohelp
{
    [POCOViewModel]
    public class ViewModelLicencja : CrudVMBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public virtual string Filter { get; set; }
        public virtual int FilterIndex { get; set; }
        public virtual string Title { get; set; }
        public virtual bool DialogResult { get; protected set; }
        public virtual string ResultFileName { get; protected set; }
        public virtual string FileBody { get; set; }
        protected virtual IOpenFileDialogService OpenFileDialogService
        {
            get { return null; }
        }
         public ICommand OpenCommand { get; private set; }
        protected IOpenFileDialgoService OpenFileService { get { return GetService<IOpenFileDialgoService>(); } }
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        private string _nip;
        public string NIP
        {
            get
            {
                return _nip;
            }
            set
            {
                _nip = value;
                RisePropertyChanged("NIP");
            }
        }

        private string _nazwafirmy;
        public string NAZWAFIRMY
        {
            get
            {
                return _nazwafirmy;
            }
            set
            {
                _nazwafirmy = value;
                RisePropertyChanged("NAZWAFIRMY");
            }
        }

        private string _telefon;
        public string TELEFON
        {
            get
            {
                return _telefon;
            }
            set
            {
                _telefon = value;
                RisePropertyChanged("TELEFON");
            }
        }
        LicConfig lic;
        LicencjaClass li;
        public ViewModelLicencja()
        {
            //   Filter = "Lic  file (.ihp)";
            Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            FilterIndex = 1;
            Title = "Otwórz plik licencji";
            OpenCommand = new DelegateCommand(ZaczytajPlik);
            try
            {
                lic = LicConfig.GetInstance;
                if (lic.LicOk)
                {
                    li = lic.UstawieniaLicencji;
                    NAZWAFIRMY = li.Firma.Nazwa;
                    NIP = li.Firma.NIP;
                }
                else
                {
                    NAZWAFIRMY = "BŁAD !!!!";
                    NIP = "BŁĄD !!!!";
                }
            }

            catch (Exception ex)

            {
                //  MessageBoxService.ShowMessage("Problem z zaczytaniem pliku licencji - zgłoś do serwisu");
            }
        }
        public void ZaczytajPlik()
        {
            string fileName = OpenFileService.Show();
            if (string.IsNullOrEmpty(fileName)) return;
            LicConfig lic = LicConfig.GetInstance;
            lic.ZaczytajNowa(fileName);
            LicencjaClass licust = lic.UstawieniaLicencji;
            if(licust!=null)
            {
                NAZWAFIRMY = licust.Firma.Nazwa;
                NIP = licust.Firma.NIP;
                string krypto = Krypto.Encrypt(NAZWAFIRMY + NIP);

                IHP_PARAMETRY ihp = context.IHP_PARAMETRY?.FirstOrDefault(x => x.ID_IHP_PARAMETRY == 0);
                if (ihp != null)
                {
                    try
                    {

                        krypto = krypto.Substring(1, 35);
                        ihp.WARTOSC = krypto;
                        context.IHP_PARAMETRY.Attach(ihp);
                        context.Entry(ihp).State = EntityState.Modified;
                        context.SaveChanges();
                        lic.Zapisz();
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
                        //throw;
                    }

                }
            }
        }
    }
    public interface IOpenFileDialgoService
    {
        string Show();
        string ShowFoto();
        string ShowFotoEx();
        OpenFileDialog ShowFotoAll();

    }
    public class CustomService : ServiceBase, IOpenFileDialgoService
    {
        public string Show()
        {
            OpenFileDialog op = new OpenFileDialog();
      
            op.InitialDirectory = Environment.CurrentDirectory;
            op.Filter = "Lic Files (*.ihp)|*.ihp";
            op.ShowDialog();
            return op.FileName;
        }
        public string ShowFoto()
        {
            OpenFileDialog op = new OpenFileDialog();

            op.InitialDirectory = Environment.CurrentDirectory;
            op.Filter = " Pliki graficzne |*.png;*.jpg;*.jpeg";
            op.ShowDialog();
            return op.FileName;
        }
        public string ShowFotoEx()
        {
            OpenFileDialog op = new OpenFileDialog();

            op.InitialDirectory = Environment.CurrentDirectory;
            op.Filter = " Pliki graficzne |*.png;*.jpg;*.jpeg";
            op.ShowDialog();
            return op.SafeFileName;
        }
        public OpenFileDialog ShowFotoAll()
        {
            OpenFileDialog op = new OpenFileDialog();

            op.InitialDirectory = Environment.CurrentDirectory;
            op.Filter = " Pliki graficzne |*.png;*.jpg;*.jpeg";
            op.ShowDialog();
            return op;
        }
    }
    }
    public class StringFormatConverter : MarkupExtension, IValueConverter
    {
        public StringFormatConverter() { }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return String.Format(culture, (string)parameter, value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

 


