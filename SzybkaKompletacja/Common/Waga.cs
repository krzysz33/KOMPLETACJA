using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace KpInfohelp
{
  public  class Waga : CrudVMBase, INotifyPropertyChanged
    {

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        CommunicationManager comm;
        public PortName wPortName { get; set; }
        public List<PortName> PortNameLst { get; set; }
        public BitData wBitData{ get; set; }
        public   List<BitData> BitDataLst { get; set; }
        public Miernik Miernik { get; set; }
        public List<Miernik> MiernikLst { get; set; }
        public Paritty wParity { get; set; }
       public List<Paritty> ParittyLst { get; set; }
        public BitStop wBitStop { get; set; }
        public   List<BitStop> BitStopLst { get; set; }
        public   SterPrzep wSterPrzep{get;set;}
        public   List<SterPrzep> SterPrzepLst { get; set; }
         public  Speed Speed { get; set; }
        public  List<Speed> SpeedLst { get; set; }
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
       AppConfig  app = null;
        private static  Waga m_oInstance = null;
        public static Waga tWaga
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new Waga();
                }
                return m_oInstance;
            }
        }
      System.Windows.Forms.Timer timerWaga = new System.Windows.Forms.Timer();
        private static readonly object syncLock = new object();
        Timer _timer;
        Random Random = new Random();
      
        static readonly ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random(DateTime.Now.Second));
        public  Waga()
        {
            timerWaga.Interval = 15000; // specify interval time as you want
            timerWaga.Tick += new EventHandler(tWaga_Tick);
            // timerWaga.Enabled = true;
            _ramka = new WagaRamka();
               app = AppConfig.GetInstance;

            MiernikLst = new List<Miernik>();
            foreach (IHP_MIERNIK item in context.IHP_MIERNIK.ToList())
                MiernikLst.Add(new Miernik { IdMiernik = item.ID_IHP_MIERNIK, Nazwa = item.NAZWA });

            SpeedLst = new List<Speed>();
            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 1).ToList())
                SpeedLst.Add(new Speed { ID = item.ID, Value = item.VAL });

            SterPrzepLst = new List<SterPrzep>();
            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 2).ToList())
                SterPrzepLst.Add(new SterPrzep { ID = item.ID, Value = item.VAL });

            BitStopLst = new List<BitStop>();
            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 3).ToList())
                BitStopLst.Add(new BitStop { ID = item.ID, Value = item.VAL });

            PortNameLst = new List<PortName>();
            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 4).ToList())
                PortNameLst.Add(new PortName { ID = item.ID, Value = item.VAL });


            BitDataLst = new List<BitData>();
            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 5).ToList())
                BitDataLst.Add(new BitData { ID = item.ID, Value = item.VAL });

            ParittyLst = new List<Paritty>();

            foreach (IHP_COMCONFIG item in context.IHP_COMCONFIG.Where(x => x.TYP == 6).ToList())
                ParittyLst.Add(new Paritty { ID = item.ID, Value = item.VAL });
            WypelnijListe();
        }
        private void PrepareRandomFrame()
        {
            _ramka.Waga = GetRandomNumber(1000, 10000);
            Messenger.Default.Send<WagaRamka>(_ramka);
        }

        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Value.Next(min, max);
            }
        }

        private void TimerCallback(object state)
        {
            try
            {
                PrepareRandomFrame();
            }
            finally
            {
                // _timer.Change(_random.Next(1000, 3000), Timeout.Infinite);
            }
        }


        public void StartWaga()
        {
          IHP_PARAMETRY param = GetParam(4);
          if(param.WARTOSC =="1")
            {
                if (comm != null)
                    comm.ClosePort(); //jezeli otwarty z poprzedniego testowania 

                comm = new CommunicationManager(Speed.Value,
                                                wSterPrzep.Value,
                                                wBitStop.Value,
                                                wBitData.Value,
                                                wPortName.Value, Miernik.Nazwa);
                    if (comm.OpenPort())
                     {
                          if (Miernik.Nazwa == "Radwag")
                                  timerWaga.Enabled = true;
                          if (Miernik.Nazwa == "Rinstrum")
                                 timerWaga.Enabled = true;
                          if (Miernik.Nazwa == "Rinstrum2")
                                 timerWaga.Enabled = true;
                }
            }

            if (param.WARTOSC == "0")
            {
                _timer = new Timer(TimerCallback, null, 1000, 1000);
           }
      }
        public static Waga GetInstance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new Waga();
                }
                return m_oInstance;
            }
        }

        public static Waga GetInstanceDispose
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new Waga();
                }
                else
                {
                     GC.SuppressFinalize(m_oInstance);
                    m_oInstance = new Waga();
                }
                return m_oInstance;
            }
        }
        private void tWaga_Tick(object sender, EventArgs e)
        {
            if (Miernik.Nazwa == "Radwag")
                comm.WriteData("SI" + Environment.NewLine);

            if (Miernik.Nazwa == "Rinstrum")
                comm.WriteData("20050026:;");
        }
        public void Zapisz()
        {

           if(app!=null)
            {
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Miernik = Miernik.Nazwa;
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.PortName = wPortName.Value;
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Speed = Speed.Value;
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.SterPrzep = wSterPrzep.Value;
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Paritty = wParity.Value;
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.BitData =  Convert.ToInt32(wBitData.Value);
                app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.BitStop = wBitStop.Value;
              app.Zapisz();
            }
   


        }
        public void WypelnijListe()
        {
            if (app != null)
            {
                Speed = SpeedLst.FirstOrDefault(x => x.Value == app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Speed);
                wSterPrzep = SterPrzepLst.FirstOrDefault(x => x.Value == app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.SterPrzep);
                wBitStop = BitStopLst.FirstOrDefault(x => x.Value == Convert.ToString(app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.BitStop));
                wBitData = BitDataLst.FirstOrDefault(x => x.Value == Convert.ToString(app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.BitData));
                wPortName = PortNameLst.FirstOrDefault(x => x.Value == app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.PortName);
                wParity = ParittyLst.FirstOrDefault(x => x.Value == app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Paritty);
                Miernik = MiernikLst.FirstOrDefault(x => x.Nazwa == app.UstawieniaAplikacji.UstawieniaPortuCom.WAGA1.Miernik);
            }
        }
    }
}
