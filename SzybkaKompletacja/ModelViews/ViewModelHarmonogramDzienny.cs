using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using System.Windows.Input;
using System.Collections.Specialized;
//using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Scheduler;
using System.Windows;
using System.Data.Entity.Validation;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{

    public class ViewModelHarmonogramDzienny  : CrudVMBase,INotifyPropertyChanged, IMVVMDockingProperties
    {
        private DateTime _submissiondate;
        private TimeInterval lastFetchedInterval;
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

        #region Property
        private IHP_HARMONOGRAM_DZIENNY currentModelAppointment;
        public IHP_HARMONOGRAM_DZIENNY CurrentModelAppointment
        {
            get { return currentModelAppointment; }
            set
            {
                if (value != currentModelAppointment)
                {
                    currentModelAppointment = value;
           //         ScrollToAppointment();
                }
            }
        }
        public SchedulerControl Scheduler { get; set; }
        #endregion
        private PersistentObjectsEventHandler _poehandler;

        #region Interfejsy Command
        private ICommand _addnewroutecommand, _refreshcommand;
         public ICommand AddNewAppointmentCommand { get; private set; }
        public ICommand GetSourceObjectCommand { get; private set; }
        public ICommand RemoveRouteCommand { get; private set; }
        public ICommand AddRouteCommand { get; private set; }
        public ICommand ZamDostCommand { get; private set; }
        public ICommand ZamDostDel { get; private set; }
        public ICommand SchedulerLoadedCommand { get; private set; }
        #endregion Interfejsy Command
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(); } }
        public event PropertyChangedEventHandler PropertyChanged;
        #region Listy
  

        private ObservableCollection<IHP_HARMONOGRAM_DZIENNY> appointments;
        private ObservableCollection<IHP_HARMONOGRAM_DZIENNY> _selectedmodelappointments;
        public virtual ObservableCollection<IHP_HARMONOGRAM_DZIENNY> SelectedModelAppointments
        {
            get
            {
                return _selectedmodelappointments;
            }
            set
            {
                _selectedmodelappointments = value;
                RisePropertyChanged("SelectedModelAppointments");
            }
        }
      
       public ObservableCollection<IHP_MASZYNA> Machines { get; private set; }
        public ObservableCollection<IHP_MASZYNA> MachinesLst { get; private set; }
        public ObservableCollection<IHP_HARMONOGRAM_DZIENNY> Appointments
        {
            get
            {
                return appointments;
            }
            private set
            {
                appointments = value;
                RisePropertyChanged("Appointments");
            }
        }
        #endregion
        private decimal _ilosczazn;
         public decimal IloscZazn
        {
            get
            {
                return _ilosczazn;
            }
            set
            {
                _ilosczazn = value;
                RisePropertyChanged("IloscZazn");

            }

        }
         private decimal _wagazazn;
        public decimal WagaZazn
        {
            get
            {
               return _wagazazn;
            }
            set
            {
                _wagazazn = value;
                RisePropertyChanged("WagaZazn");
            }
        }
        private decimal _wagawyk;
        public decimal WagaWyk
        {
            get
            {
                return _wagawyk;
            }
            set
            {
                _wagawyk = value;
                RisePropertyChanged("WagaWyk");
            }
        }
        private decimal _wagapoz;
        public decimal WagaPoz
        {
            get
            {
                return _wagapoz;
            }
            set
            {
                _wagapoz = value;
                RisePropertyChanged("WagaPoz");
            }
        }
        private string _hodowca;
        public string Hodowca
        {
            get
            {
                return _hodowca;
            }

            set
            {
                _hodowca = value;
                RisePropertyChanged("Hodowca");
            }
        }
        private string _miejscowosc;
        public string Miejscowosc
        {
            get
            {
                return _miejscowosc;
            }
            set
            {
                _miejscowosc = value;
                RisePropertyChanged("Miejscowosc");
            }
        }
        void Podsumuj(int IdHarmonogram,int ilosczazn,decimal wagazazn)
        {
             IloscZazn = ilosczazn;
               WagaZazn = wagazazn;
               WagaWyk = context.IHP_HARMONOGRAM_DZIENNY.Where(x => x.ID_IHP_HARMONOGRAM_DZIENNY == IdHarmonogram).Sum(x => x.WAGA) ?? 0;
               WagaPoz = WagaZazn - WagaWyk;
        }
        void SelectedListHarmonogram_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //      if (e.Action == NotifyCollectionChangedAction.Reset)
            //    {
            //   OnPropertyChanged("SelectedListHarmonogram");
            //    Podsumuj();
                //    RaisePropertyChanged("SelectedListHarmonogram");
            //     }
      
        }
        public  List<IHP_MASZYNA> lstMachines { get; private set; }
        public  ViewModelHarmonogramDzienny()
        { 

            lastFetchedInterval = new TimeInterval();

            _poehandler = new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            _addnewroutecommand = new RelayCommand(SaveExecute_Execute, CanExecuteTrasa);
            _refreshcommand = new RelayCommand(Refresh, CanRefrest);
           
            //SelectedListHarmonogram = new ObservableCollection<ARIT_HARMONOGRAM>();
            //SelectedListHarmonogram.CollectionChanged += SelectedListHarmonogram_CollectionChanged;
            //_selectedAppointments = new ObservableCollection<ModelAppointment>();
 
            AppointmentsModifiedCommand = new DelegateCommand<DevExpress.XtraScheduler.PersistentObjectsEventArgs>(AppointmentsModifiedExecute);
      //      AppointmentsDeletedCommand = new DelegateCommand<DevExpress.XtraScheduler.PersistentObjectsEventArgs>(AppointmentsDeletedExecute);
            AppointmentsDeletedCommand = new DelegateCommand<DevExpress.XtraScheduler.PersistentObjectCancelEventArgs>(AppointmentsDeletedExecute2);
            FetchAppointmentsCommand = new DelegateCommand<DevExpress.XtraScheduler.FetchAppointmentsEventArgs>(FetchAppointmentsExecute);
            SchedulerLoadedCommand = new DevExpress.Mvvm.DelegateCommand<RoutedEventArgs>(SchedulerLoadedCommandExecute);
            RemoveRouteCommand = new DelegateCommand(RemoveMachine);
            AddRouteCommand = new DelegateCommand(AddMachine);
       //     ZamDostCommand = new RelayCommand(GenZam,CanGenZam);
        //    ZamDostDel = new RelayCommand(DelZam, CanDelZam);
        //    ListHarmonogramDzienny = new ObservableCollection<ARIT_HARMONOGRAM>(); 
            Appointments = new ObservableCollection<IHP_HARMONOGRAM_DZIENNY>();
            SelectedAppointments = new ObservableCollection<Appointment>();
            SelectedModelAppointments = new ObservableCollection<IHP_HARMONOGRAM_DZIENNY>();

            SelectedAppointments.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SelectedAppointments_CollectionChanged);
            SelectedModelAppointments.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(SelectedModelAppointment_CollectionChanged);

            ///    Resources = new ObservableCollection<ModelResource>();
            Machines = new ObservableCollection<IHP_MASZYNA>();
            MachinesLst = new ObservableCollection<IHP_MASZYNA>();
            Refresh();
            GetMachines();
            Messenger.Default.Register<int>(this, OnMessageUpdate);
        }
        /*
        protected void ScrollToAppointment()
        {
            if (Scheduler != null)
            {
                Scheduler.ActiveView.GotoTimeInterval(new TimeInterval(CurrentModelAppointment.CzasStart??default(DateTime), CurrentModelAppointment.CzasStop ?? default(DateTime)));
                if (Scheduler.ActiveViewType == SchedulerViewType.Day)
                    (Scheduler.ActiveView as DayView).TopRowTime = (CurrentModelAppointment.CzasStop ?? default(DateTime)).TimeOfDay;
            }
        }
        */
        public ICommand AppointmentsDeletedCommand
        {
            get;
            private set;
        }
        public ICommand AppointmentsModifiedCommand
        {
            get;
            private set;
        }
        public ICommand FetchAppointmentsCommand
        {
            get;
            private set;
        }
        public PersistentObjectsEventHandler PoeHandler
        {
            get { return _poehandler; }
            set { _poehandler = value; }
        }
        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
        }
        void SelectedAppointments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedModelAppointments.CollectionChanged -= SelectedModelAppointment_CollectionChanged;
            SelectGridRows();
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                CurrentModelAppointment = (e.NewItems[0] as Appointment).GetSourceObject(Scheduler.Storage.GetCoreStorage()) as IHP_HARMONOGRAM_DZIENNY;
                RaisePropertyChanged("CurrentModelAppointment");
            }
            SelectedModelAppointments.CollectionChanged += SelectedModelAppointment_CollectionChanged;
            if(CurrentModelAppointment!= null)
            {
           ///     CurrentSelHarmonogram = ListHarmonogramDzienny.FirstOrDefault(x => x.ID_HARMONOGRAM == CurrentModelAppointment.ID_ARIT_HARMONOGRAM);
                RaisePropertyChanged("CurrentSelHarmonogram");
            }
     
        }
        void SelectGridRows()
        {
            SelectedModelAppointments.Clear();
            foreach (Appointment apt in SelectedAppointments)
            {
                if (apt.IsBase || apt.IsException)
                    SelectedModelAppointments.Add(apt.GetSourceObject(Scheduler.Storage.GetCoreStorage()) as IHP_HARMONOGRAM_DZIENNY);
                if (apt.IsOccurrence && !apt.IsException)
                    SelectedModelAppointments.Add(apt.RecurrencePattern.GetSourceObject(Scheduler.Storage.GetCoreStorage()) as IHP_HARMONOGRAM_DZIENNY);
            }
        }
        /*
             public ObservableCollection<ModelAppointment> SelectedAppointments
              {
                 get { return _selectedAppointments; }
                 set { _selectedAppointments = value; }
              }
        */
        public void OnMessageUpdate(int Name)
        {
            if (Name == 1)
                RefreshHarmonogram();
        }
        public virtual ObservableCollection<Appointment> SelectedAppointments { get; set; }
        public ICommand AddNewRouteCommand
        {
            get { return _addnewroutecommand; }
        }
        public ICommand RefreshCommand
        {
            get { return _refreshcommand; }
        }
       public static ViewModelHarmonogramDzienny Create()
        {
            return ViewModelSource.Create(() => new ViewModelHarmonogramDzienny());
        }
        IHP_MASZYNA _machine;
        public IHP_MASZYNA Machine
        {
            get { return _machine; }
            set
            {
                if (_machine != value)
                {
                    _machine = value;
                    RisePropertyChanged("Machine");
                }
            }
        }
        public DateTime SubmissionDate
        {
            get { return _submissiondate; }
            set
            {
                if (_submissiondate != value)
                {
                    _submissiondate = value;
                    RisePropertyChanged("SubmissionDate");
                    Refresh();
                }
            }
        }
   
        public void SaveExecute_Execute()
        {
            AddTestData();
        }
        public void Refresh_Execute()
        {
           //
        }
        public bool CanRefrest()
        {
            return true;
        }
        public bool CanGenZam()
        {
            if (CurrentModelAppointment != null)
                if (CurrentModelAppointment.IHP_NAGLDOK > 0)
                    return false;
                else
                    return true;
            else
                return
                    false;
        }
        public bool CanDelZam()
        {
            if (CurrentModelAppointment != null)
                if (CurrentModelAppointment.IHP_NAGLDOK == null)
                    return false;
                else
                    return true;
            else
                return
                    false;
        }
        public bool CanExecuteTrasa()
        {
            bool res = false;
            if ((_machine != null)  )
            {
                res =  true;
            }
            return res;
      }
        private void RaisePropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private void SelectedModelAppointment_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SelectedAppointments.CollectionChanged -= SelectedAppointments_CollectionChanged;
            SelectAppointments();
            SelectedAppointments.CollectionChanged += SelectedAppointments_CollectionChanged;
        }
        void SelectAppointments()
        {
            SelectedAppointments.Clear();
            foreach (IHP_HARMONOGRAM_DZIENNY modelApt in SelectedModelAppointments)
            {
                Appointment apt = Scheduler.Storage.AppointmentStorage.GetAppointmentById(modelApt.ID_IHP_HARMONOGRAM_DZIENNY);
                if (apt != null)
                    SelectedAppointments.Add(apt);
            }
        }
        //   https://www.devexpress.com/Support/Center/Example/Details/E4670
        private void AppointmentsModifiedExecute(DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            string LastMessage;
           try
            {
                context.SaveChanges();
                //      Refresh();
                RefreshCalendar();
            }
   
           catch (Exception ex)
            {
    
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
        }
        private void SchedulerLoadedCommandExecute(RoutedEventArgs args)
        {
            Scheduler = args.Source as SchedulerControl;
            Scheduler.Start = DateTime.Today;
            Scheduler.Storage.AppointmentInserting += Storage_AppointmentInserting;
        }
        void Storage_AppointmentInserting(object sender, PersistentObjectCancelEventArgs e)
        {
            Scheduler.Storage.SetAppointmentId((Appointment)e.Object, Guid.NewGuid());
        }
        private void AppointmentsDeletedExecute(DevExpress.XtraScheduler.PersistentObjectsEventArgs e)
        {
            string LastMessage;
            for (int i = 0; i < e.Objects.Count; i++)
            {
                Appointment deletedAppt = e.Objects[i] as Appointment;
                IHP_HARMONOGRAM_DZIENNY item = context.IHP_HARMONOGRAM_DZIENNY.Local.FirstOrDefault(x => x.ID_IHP_HARMONOGRAM_DZIENNY == Convert.ToInt32(deletedAppt.Id));


                if (item != null)
                {
                    if (item.IHP_NAGLDOK > 0)
                    {
                        MessageBoxService.ShowMessage("WYSTAWIONO ZAMÓWIENIE - NIE MOŻNA USUNĄĆ");

                        return;
                    }

                    try
                    {
                        context.IHP_HARMONOGRAM_DZIENNY.Remove(item);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                        LastMessage = ex.ToString();
                        LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                    }
                }
            }
        }
          
        private void AppointmentsDeletedExecute2(DevExpress.XtraScheduler.PersistentObjectCancelEventArgs e)
        {
            string LastMessage;

            Appointment deletedAppt = e.Object as Appointment;
            IHP_HARMONOGRAM_DZIENNY item = context.IHP_HARMONOGRAM_DZIENNY.Local.FirstOrDefault(x => x.ID_IHP_HARMONOGRAM_DZIENNY == Convert.ToInt32(deletedAppt.Id));


            if (item != null)
            {
                if (item.IHP_NAGLDOK > 0)
                {
                    MessageBoxService.ShowMessage("WYSTAWIONO ZAMÓWIENIE - NIE MOŻNA USUNĄĆ");
                    e.Cancel = true;
                    return;
                }
                try
                {
                    context.IHP_HARMONOGRAM_DZIENNY.Remove(item);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    LastMessage = ex.ToString();
                    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                }
            }
        }

        private void FetchAppointmentsExecute(DevExpress.XtraScheduler.FetchAppointmentsEventArgs e)
        {
            TimeInterval currentInterval = e.Interval as TimeInterval;

            if (lastFetchedInterval.Contains(currentInterval))
                return;
            lastFetchedInterval = currentInterval;
            //
            var entities = from entity in context.IHP_HARMONOGRAM_DZIENNY
                           where (entity.CZASSTART < lastFetchedInterval.End && entity.CZASSTOP > lastFetchedInterval.Start)
                           select entity;

            entities.Load();
            var Appo = context.IHP_HARMONOGRAM_DZIENNY.Local.ToList();
            Appointments.Clear();
            foreach (IHP_HARMONOGRAM_DZIENNY item in Appo)
            {
                //   item.TEMAT = item.ARIT_HARMONOGRAM.HODOWCA ;
                //  item.LABELID = 2;
                //  item.WagaSr = 22;
                Appointments.Add(item);
            }
        }

        private int WyliczIlosc()
        {
            //  int ladDopuszczalna = WyliczIlosc()
         //   if (_arItharmonogramSel == null) return 0;
 
            return 444;
        }
        private decimal WyliczWage()
        {
           // decimal SumaZdiecia = _arItharmonogramSel.WAGAKG??0;
           // decimal ladDopuszczalna = Convert.ToDecimal(_route.DOPUSZCZALNALADOWNISC ?? 0);
           // decimal ladWykorzystana = planowanieEntity.ARIT_HARMONOGRAM_DZIENNY
           //                                                                 .Where(x => x.ID_ARIT_HARMONOGRAM.Equals(_arItharmonogramSel.ID_HARMONOGRAM))
           //                                                                   .Sum(x => x.WAGA) ?? 0;
           // decimal Pozostalo = SumaZdiecia - ladWykorzystana;

           // if (Pozostalo > ladDopuszczalna)
           //                 return ladDopuszczalna;

           // if(Pozostalo < ladDopuszczalna)
           // {
           //     if ((Pozostalo - ladDopuszczalna) > 0)
           //          return Pozostalo - ladDopuszczalna;
           //     else if (Pozostalo > 0)
           //     {
           //         return Pozostalo;
           //     }
           //     else
           //         return 0;

           // }
                            
           //if (_arItharmonogramSel == null) return 0;

            return 444;
        }
        private int przeliczCzasStart()
        {
            return 10;
        }

        private int przeliczCzasStop(int idKontrah)
        {
            int odlg = 100;
            //if (context.WYSTCECHYKONTRAH.Any(x => x.ID_KONTRAH.Equals(idKontrah) && x.ID_CECHA.Equals(10022)))
            //{

            //    if (int.TryParse(planowanieEntity.WYSTCECHYKONTRAH.FirstOrDefault(x => x.ID_KONTRAH.Equals(idKontrah) && x.ID_CECHA.Equals(10022)).WARTOSC, out odlg))
            //    {
            //        odlg = Convert.ToInt32(planowanieEntity.WYSTCECHYKONTRAH.FirstOrDefault(x => x.ID_KONTRAH.Equals(idKontrah) && x.ID_CECHA.Equals(10022)).WARTOSC);
            //    }
            // }
            int godz = odlg / 40;
            godz = godz + 2;
            return godz;
        }
        private void AddTestData()
        {
   //         planowanieEntity.ARIT_HARMONOGRAM_DZIENNY.Local.Clear();
            int godzstart = 6;
            int godzstop = godzstart + 5; // przeliczCzasStop(_arItharmonogramSel.ID_KONTRAH ?? 0);
            string LastMessage;
            decimal _waga = WyliczWage();
            try
            {
                IHP_HARMONOGRAM_DZIENNY apt = new IHP_HARMONOGRAM_DZIENNY()
                 {
                    ID_IHP_HARMONOGRAM_DZIENNY = GetNextNumer(1),
                    DATA = _submissiondate,
                    //  TEMAT = _arItharmonogramSel.HODOWCA,
                      TEMAT = "MASZYNA1",
                    CZASSTART = _submissiondate.AddHours(godzstart),
                    CZASSTOP = _submissiondate.AddHours(godzstop),
                 //   ID_ARIT_HARMONOGRAM = _arItharmonogramSel.ID_HARMONOGRAM,
                    ID_IHP_MASZYNA = _machine.ID_IHP_MASZYNA,
                    LABELID = 2,
                    ILOSC = 3,
                    WAGA = _waga,
                    ID_KARTOTEKA = 1,
                    ID_KONTRAH = 1
                 
                };

                Appointments.Add(apt);
                context.IHP_HARMONOGRAM_DZIENNY.Add(apt);
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
         
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LastMessage = ve.PropertyName +": " + ve.ErrorMessage;
                          LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);

                 //       Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                   //         ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            /*
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == string.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            */
         }
        private int ToRgb(System.Drawing.Color color)
        {
            return color.B << 16 | color.G << 8 | color.R;
        }
        private void RefreshCalendar()
        {
            DateTime endDate = _submissiondate.AddDays(7);
            var entities = from entity in context.IHP_HARMONOGRAM_DZIENNY
                           where (entity.CZASSTART > _submissiondate && entity.CZASSTART < endDate)
                           select entity;

            entities.Load();
            var Appo = context.IHP_HARMONOGRAM_DZIENNY.Local.ToList();
            Appointments.Clear();
            foreach (IHP_HARMONOGRAM_DZIENNY item in Appo)
            {
                //   item.TEMAT = item.ARIT_HARMONOGRAM.HODOWCA ;
                //  item.LABELID = 2;
                //  item.WagaSr = 22;
                Appointments.Add(item);
            }

        }
        private void RefreshHarmonogram()
        {
            //string LastMessage = string.Empty;
            //DateTime endDate = _submissiondate.AddDays(7);
            //PlanowanieEntity pl = new PlanowanieEntity();
            //try
            //{
            //    lstHarDzienny = pl.ARIT_HARMONOGRAM.Where(x => x.DATA >= _submissiondate && x.DATA <= endDate && x.ID_KONTRAH > 0).ToList();

            //    ListHarmonogramDzienny.Clear();

            //    foreach (ARIT_HARMONOGRAM Item in lstHarDzienny)
            //    {
            //        ListHarmonogramDzienny.Add(Item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //   LastMessage = ex.ToString();
            //    if (LastMessage == string.Empty)
            //        LastMessage = ex.InnerException.ToString();
            //    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //}
        }
        private void Refresh()
        {
            //string LastMessage;
            //DateTime endDate = _submissiondate.AddDays(7);
            //SelectedModelAppointments.Clear();
           
            //PlanowanieEntity pl = new PlanowanieEntity();
            //try
            //{
            //    lstHarDzienny = pl.ARIT_HARMONOGRAM.Where(x => x.DATA >= _submissiondate && x.DATA <= endDate && x.ID_KONTRAH >0).ToList();
                             
            //    ListHarmonogramDzienny.Clear();

            //    foreach (ARIT_HARMONOGRAM Item in lstHarDzienny)
            //    {
            //        ListHarmonogramDzienny.Add(Item);
            //    }

            //    var entities = from entity in planowanieEntity.ARIT_HARMONOGRAM_DZIENNY
            //                   where (entity.CZASSTART > _submissiondate  && entity.CZASSTART < endDate)
            //                   select entity;

            //    entities.Load();
            //    var Appo = planowanieEntity.ARIT_HARMONOGRAM_DZIENNY.Local.ToList();
            //    Appointments.Clear();
            //    foreach (ArItHarmonogramDzienny item in Appo)
            //    {
            //        //   item.TEMAT = item.ARIT_HARMONOGRAM.HODOWCA ;
            //        //  item.LABELID = 2;
            //        //  item.WagaSr = 22;
            //        Appointments.Add(item);
            //    }
            //}      
            //catch (Exception ex)
            //{
            // LastMessage = ex.ToString();
            // if (LastMessage == string.Empty)
            //   LastMessage = ex.InnerException.ToString();
            //   LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //}
        }
        private void GetHarmonogramDnia(DateTime data)
        {
        
        }

        private void AddMachine()
        {
            string oldNrRej = string.Empty;

            if (Machine == null) return;
            try
            {
                Machine.AKTYWNY = 1;
                context.IHP_MASZYNA.Add(Machine);
                context.Entry(Machine).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join(";", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, "Wystąpiły Błędy: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }
        //private void GetRoutesFilter()
        //{
        //    int[] listaId = { 1, 2, 3 };

        //    lstRoutes = planowanieEntity.ARIT_TRANSPORT.Where(x => listaId.Contains(x.ID_ARIT_TRANSPORT)).ToList();
        //    Routes.Clear();
        //    foreach (ARIT_TRANSPORT Item in lstRoutes)
        //    {
        //        Routes.Add(Item);
        //    }

        //}
        //public void OnMouseRightButtonUp(ARIT_TRANSPORT item)
        //{
        //    ARIT_TRANSPORT it = new ARIT_TRANSPORT();
        //}

        private void GetMachines()
        {

            lstMachines = context.IHP_MASZYNA.ToList();
            lstMachines.Clear();
            foreach (IHP_MASZYNA Item in lstMachines)
            {
                //     if (Item.WIDOCZNY == 1)
                //       {
                //        Item.NAZWA = Item.NRREJ + " (" + Item.RODZPOJAZDUSTR + ") \n" + Item.ARIT_KIEROWCY.IMIE + " " + Item.ARIT_KIEROWCY.NAZWISKO + "\n tel.: " + Item.ARIT_KIEROWCY.TELEFON + "\n Ład. Dop.: "+ Item.DOPUSZCZALNALADOWNISC.ToString();
                Machines.Add(Item);
                //       }
                //    }
                //RoutesLst.Clear();
                //foreach (ARIT_TRANSPORT Item2 in lstRoutes)
                //{
                //  Item2.NAZWARODZP = Item2.NRREJ + " (" + Item2.RODZPOJAZDUSTR + ") \n" + Item2.ARIT_KIEROWCY.IMIE + " " + Item2.ARIT_KIEROWCY.NAZWISKO + "\n tel.: " + Item2.ARIT_KIEROWCY.TELEFON + "\n Ład. Dop.: " + Item2.DOPUSZCZALNALADOWNISC.ToString();
                //    RoutesLst.Add(Item2);
                //}
            }
        }

        private void RemoveMachine()
        {
            string LastMessage;
            try
            { 
            Machines.Remove(Machine);
                Machine.AKTYWNY = 0;
                context.IHP_MASZYNA.Add(Machine);
                context.Entry(Machine).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LastMessage = ve.PropertyName + ": " + ve.ErrorMessage;
                        LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                    }
                }
                throw;
            }
        }


        

   //private void DelZam()
   //     {
   //         try
   //         {
   //             if (currentModelAppointment != null)
   //             {
   //                 int IdNagl = 0;
   //                 string NrdokZew = string.Empty;
   //                 UrzZewDataAdapter uzGenZam = new UrzZewDataAdapter("ZADOST");
   //                 SelectedModelAppointments.FirstOrDefault(x => x.ID_ARIT_HARMONOGRAM_DZIENNY == currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY).ID_NAGLZAMDOST = null;
   //                 SelectedModelAppointments.FirstOrDefault(x => x.ID_ARIT_HARMONOGRAM_DZIENNY == currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY).NRDOKZA = string.Empty;
   //                 OnPropertyChanged("SelectedModelAppointments");
   //                 uzGenZam.AktHarmonogramNull(currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY);
   //                 Refresh();
   //             }
   //         }
   //         catch (Exception ex)
   //         {
   //             MessageBox.Show(ex.Message);
   //         }
   //     }


     //   private void GenZam()
     //{
     //  try
     //  {
     //     if (currentModelAppointment != null)
     //      {
     //             int IdNagl = 0;
     //              string NrdokZew = string.Empty;
     //             UrzZewDataAdapter uzGenZam = new UrzZewDataAdapter("ZADOST");
     //               //    uzGenZam.GenZamDost2(currentModelAppointment.ID_KONTRAH, currentModelAppointment.ID_KARTOTEKA, currentModelAppointment.WAGA ?? 0, out IdNagl, out NrdokZew, currentModelAppointment.ILOSC.ToString(), currentModelAppointment.ARIT_TRANSPORT.NRREJ, currentModelAppointment.CZASSTOP?? default(DateTime));
     //               uzGenZam.GenZamDost3(currentModelAppointment.ID_KONTRAH, currentModelAppointment.ID_KARTOTEKA, currentModelAppointment.WAGA ?? 0, out IdNagl, out NrdokZew, currentModelAppointment.ILOSC.ToString(), currentModelAppointment.ARIT_TRANSPORT.NRREJ, currentModelAppointment.RODZZYWCA, currentModelAppointment.CZASSTOP ?? default(DateTime));
     //               SelectedModelAppointments.FirstOrDefault(x => x.ID_ARIT_HARMONOGRAM_DZIENNY == currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY).ID_NAGLZAMDOST = IdNagl;
     //               SelectedModelAppointments.FirstOrDefault(x => x.ID_ARIT_HARMONOGRAM_DZIENNY == currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY).NRDOKZA = NrdokZew;
     //               OnPropertyChanged("SelectedModelAppointments");
     //               //    currentModelAppointment.ID_NAGLZAMDOST = IdNagl;
     //               //currentModelAppointment.NRDOKZA = NrdokZew;
     //               uzGenZam.AktHarmonogram(NrdokZew, IdNagl, currentModelAppointment.ID_ARIT_HARMONOGRAM_DZIENNY);
     //               MessageBoxService.ShowMessage("ZAMÓWIENIE WYGENEROWANE : " + NrdokZew);
     //               Refresh();
     //         }
     //      }
     //       catch (Exception ex)
     //       {
     //           MessageBox.Show(ex.Message);
     //       }
     //   }
 
        }
  }
    /*
    public class MouseEventConverter : EventArgsConverterBase<MouseEventArgs>
    {
        protected override object Convert(object sender, MouseEventArgs args)
        {
            var grid = sender as GridControl;

            return args.GetPosition(grid);
        }
    }
    */
 
