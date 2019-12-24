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

namespace KpInfohelp
{
     class ViewModelKamerySettings : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
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
        public ViewModelKamerySettings()
        {
            UdpKamera1Command = new DelegateCommand(UdpKamera1);
            UdpKamera2Command = new DelegateCommand(UdpKamera2);
            UdpKamera3Command = new DelegateCommand(UdpKamera3);

            GetSettings();
        }
        public ICommand UdpKamera1Command { get; set; }
        public ICommand UdpKamera2Command { get; set; }
        public ICommand  UdpKamera3Command { get; set; }
        private void UdpKamera1()
        {
            SaveSettings(1);
        }
        private void UdpKamera2()
        {
            SaveSettings(2);
        }
        private void UdpKamera3()
        {
            SaveSettings(3);
        }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string _producent1;
        public string Producent1
        {
            get
            {
                return _producent1;
            }
            set
            {
                _producent1 = value;
                RisePropertyChanged("Producent1");
            }
        }
        private string _model1;
        public string Model1
        {
            get
            {
                return _model1;
            }
            set
            {
                _model1 = value;
                RisePropertyChanged("Model1");
            }
        }
        private string _adressip1;
        public string AdressIP1
        {
            get
            {
                return _adressip1;
            }
            set
            {
                _adressip1 = value;
                RisePropertyChanged("AdressIP1");
            }
        }
        private int _port1;
        public int Port1
        {
            get
            {
                return _port1;
            }
            set
            {
                _port1 = value;
                RisePropertyChanged("Port1");
            }
        }
        private string _uri1;
        public string Uri1
        {
            get
            {
                return _uri1;
            }
            set
            {
                _uri1 = value;
                RisePropertyChanged("Uri1");
            }
        }

        private string _urilive1;
        public string UriLive1
        {
            get
            {
                return _urilive1;
            }
            set
            {
                _urilive1 = value;
                RisePropertyChanged("UriLive1");
            }
        }


        private string _cameralogin1;
        public string  cameraLogin1
        {
            get
            {
                return _cameralogin1;
            }
            set
            {
                _cameralogin1 = value;
                RisePropertyChanged("cameraLogin1");

            }
        }
        private string _camerapassword1;
        public string cameraPassword1
        {
            get
            {
                return _camerapassword1;
            }
            set
            {
                _camerapassword1 = value;
                RisePropertyChanged("cameraPassword1");
            }
        }

        private string _producent2;
        public string Producent2
        {
            get
            {
                return _producent2;
            }
            set
            {
                _producent2 = value;
                RisePropertyChanged("Producent2");
            }
        }
        private string _model2;
        public string Model2
        {
            get
            {
                return _model2;
            }
            set
            {
                _model2 = value;
                RisePropertyChanged("Model2");
            }
        }
        private string _adressip2;
        public string AdressIP2
        {
            get
            {
                return _adressip2;
            }
            set
            {
                _adressip2 = value;
                RisePropertyChanged("AdressIP2");
            }
        }
        private int _port2;
        public int Port2
        {
            get
            {
                return _port2;
            }
            set
            {
                _port2 = value;
                RisePropertyChanged("Port2");
            }
        }
        private string _uri2;
        public string Uri2
        {
            get
            {
                return _uri2;
            }
            set
            {
                _uri2 = value;
                RisePropertyChanged("Uri2");
            }
        }


        private string _urilive2;
        public string UriLive2
        {
            get
            {
                return _urilive2;
            }
            set
            {
                _urilive2 = value;
                RisePropertyChanged("UriLive2");
            }
        }
        private string _cameralogin2;
        public string cameraLogin2
        {
            get
            {
                return _cameralogin2;
            }
            set
            {
                _cameralogin2 = value;
                RisePropertyChanged("cameraLogin2");

            }
        }
        private string _camerapassword2;
        public string cameraPassword2
        {
            get
            {
                return _camerapassword2;
            }
            set
            {
                _camerapassword2 = value;
                RisePropertyChanged("cameraPassword2");
            }
        }

        private string _producent3;
        public string Producent3
        {
            get
            {
                return _producent3;
            }
            set
            {
                _producent3 = value;
                RisePropertyChanged("Producent3");
            }
        }
        private string _model3;
        public string Model3
        {
            get
            {
                return _model3;
            }
            set
            {
                _model3 = value;
                RisePropertyChanged("Model3");
            }
        }
        private string _adressip3;
        public string AdressIP3
        {
            get
            {
                return _adressip3;
            }
            set
            {
                _adressip3 = value;
                RisePropertyChanged("AdressIP3");
            }
        }
        private int _port3;
        public int Port3
        {
            get
            {
                return _port3;
            }
            set
            {
                _port3 = value;
                RisePropertyChanged("Port3");
            }
        }
        private string _uri3;
        public string Uri3
        {
            get
            {
                return _uri3;
            }
            set
            {
                _uri3 = value;
                RisePropertyChanged("Uri3");
            }
        }

        private string _urilive3;
        public string UriLive3
        {
            get
            {
                return _urilive3;
            }
            set
            {
                _urilive3 = value;
                RisePropertyChanged("UriLive3");
            }
        }


        private string _cameralogin3;
        public string cameraLogin3
        {
            get
            {
                return _cameralogin3;
            }
            set
            {
                _cameralogin3 = value;
                RisePropertyChanged("cameraLogin3");

            }
        }
        private string _camerapassword3;
        public string cameraPassword3
        {
            get
            {
                return _camerapassword3;
            }
            set
            {
                _camerapassword3 = value;
                RisePropertyChanged("cameraPassword3");
            }
        }

        AppConfig app;

        private void GetSettings( )
        {
            app = AppConfig.GetInstance;

           
                Producent1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Producent;
                Model1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Model;
                AdressIP1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.AdressIP;
                Port1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Port;
                Uri1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URI;
                UriLive1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URILive;
               cameraLogin1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.User;
                cameraPassword1 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Pass;

                Producent2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Producent;
                Model2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Model;
                AdressIP2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.AdressIP;
                Port2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Port;
                Uri2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.URI;
                UriLive2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.URILive;
            cameraLogin2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.User;
                cameraPassword2 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Pass;
            
                Producent3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Producent;
                Model3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Model;
                AdressIP3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.AdressIP;
                Port3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Port;
                Uri3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.URI;
                UriLive3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.URILive;
                cameraLogin3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.User;
                cameraPassword3 = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Pass;
       
        }
        public void SaveSettings(int NrKamera)
        {
            app = AppConfig.GetInstance;

            if (NrKamera == 1)
            {
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Producent = Producent1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Model = Model1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.AdressIP = AdressIP1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Port = Port1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URI = Uri1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URILive = UriLive1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.User = cameraLogin1;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Pass = cameraPassword1;
                

            }
            if (NrKamera == 2)
            {
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Producent = Producent2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Model = Model2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.AdressIP = AdressIP2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Port = Port2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.URI = Uri2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.URILive = UriLive2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.User = cameraLogin2;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Pass = cameraPassword2;

            }
            if (NrKamera == 3)
            {
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Producent = Producent3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Model = Model3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.AdressIP = AdressIP3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Port = Port3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.URI = Uri3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.URILive = UriLive3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.User = cameraLogin3;
                app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Pass = cameraPassword3;

            }
            app.Zapisz();
            MessageBox.Show("Ustawienia zapisane");
        }
        }
  

}


