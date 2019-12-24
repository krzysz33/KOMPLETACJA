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
using System.Xml.Linq;

namespace KpInfohelp
{
   public  class ViewModelMiernik : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties
    {
 
        AppConfig app = null;
        Waga _waga = null;
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
        private PortName _portname;
        public PortName wPortName
        {
            get
            {
                return _portname;
            }
            set
            {
                _portname = value;

                if (_portname != null)
                        _waga.wPortName = _portname;

                RisePropertyChanged("wPortName");
            }
        }
        public   List<PortName> PortNameLst { get; set; }
        private BitData _bitdata;
        public BitData wBitData
        {
            get
            {
                return _bitdata;
            }
            set
            {
                _bitdata = value;
                 if (_bitdata != null)
                    _waga.wBitData = _bitdata;
                RisePropertyChanged("wBitData");
            }

        }
        public   List<BitData> BitDataLst { get; set; }
   
        private Miernik _miernik;
        public Miernik Miernik
        {
            get
            {
                return _miernik;
            }
            set
            {
                _miernik = value;
                if (_miernik != null)
                    _waga.Miernik = _miernik;
                RisePropertyChanged("Miernik");
            }
        }
        public   List<Miernik> MiernikLst { get; set; }

        private Paritty _parity;
        public Paritty wParity
        {
            get
            {
                return _parity;
            }
            set
            {
                _parity = value;
                if (_parity != null)
                    _waga.wParity = _parity;
                RisePropertyChanged("wParity");
           }
        }
        public   List<Paritty> ParittyLst { get; set; }
        private BitStop _bitstop;
        public BitStop wBitStop
        {
            get
            {
                return _bitstop;
            }
            set
            {
                _bitstop = value;
                    _waga.wBitStop = _bitstop;
                RisePropertyChanged("wBitStop");
            }
        }
        public   List<BitStop> BitStopLst { get; set; }
        private SterPrzep _sterprzep;
        public   SterPrzep wSterPrzep
        {
            get
            {
                return _sterprzep;
            }

            set
            {
                _sterprzep = value;
                if (_sterprzep != null)
                    _waga.wSterPrzep = _sterprzep;
                RisePropertyChanged("wSterPrzep");
            }
        }
        public   List<SterPrzep> SterPrzepLst { get; set; }
        private Speed _speed;
        public   Speed wSpeed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
                if(_speed!=null)
                        _waga.Speed = _speed;
                RisePropertyChanged("wSpeed");
            }
        }
        public   List<Speed> SpeedLst { get; set; }
        private string _wagalocal;
        public string WagaKgLocal
        {
            get
            {
                return _wagalocal;
            }
            set
            {
                _wagalocal = value;
                RisePropertyChanged("WagaKgLocal");
            }
        }
        public static void CreateItemsList(ref List<Speed> SpeedList)
        {
            var doc = XDocument.Load(@"..\..\ItemsXML.xml");

            SpeedList = doc.Root
                .Descendants("SPEED")
                .Select(node => new Speed
                {
                    ID = (int)node.Element("ID"),
                    Value = (string)node.Element("VALUE")
                })
                .ToList();

            Console.WriteLine(string.Join(",", SpeedList.Select(x => x.ToString())));

        }

        public ICommand SaveCommand { get; set; }
        public ICommand TestCommand { get; set; }
       private void TestWagi()
        {
            if (_waga != null)
                _waga.StartWaga();
        }
       private void ZapiszUstawienia()
        {
        if(_waga!=null)
            {
                _waga.Zapisz();
                MessageBox.Show("Ustawienia Wagi Zapisane!");
            }
        else
            {
                MessageBox.Show("Wystąpiły błędy przy zapisie!");
            }
        }
       public void WypelnijCombo()
        {
            SpeedLst = _waga.SpeedLst;
            wSpeed = _waga.Speed;

            SterPrzepLst = _waga.SterPrzepLst;
            wSterPrzep = _waga.wSterPrzep;

            BitStopLst = _waga.BitStopLst;
            wBitStop = _waga.wBitStop;

            BitDataLst = _waga.BitDataLst;
            wBitData = _waga.wBitData;

            PortNameLst = _waga.PortNameLst;
            wPortName = _waga.wPortName;

      
            ParittyLst = _waga.ParittyLst;
            wParity = _waga.wParity;

            MiernikLst = _waga.MiernikLst;
            Miernik = _waga.Miernik;
        }
       public ViewModelMiernik()
        {
            WagaKgLocal = "BRAK POŁĄCZENIA";
            SaveCommand = new  DelegateCommand(ZapiszUstawienia);
            TestCommand = new DelegateCommand(TestWagi);
            app = AppConfig.GetInstance;
            _waga = Waga.GetInstance;
            Messenger.Default.Register<WagaRamka>(this, OnMessagewagaRamka);
            WypelnijCombo();
        }
       public void OnMessagewagaRamka(WagaRamka ramka)
        {
            _ramka = ramka;
            if (String.IsNullOrEmpty(_ramka.ErrMsg))
                WagaKgLocal = _ramka.Waga.ToString() + " kg";
            else
                WagaKgLocal = _ramka.ErrMsg;
        }
       protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
      public event PropertyChangedEventHandler PropertyChanged;
    }
   
}


