using DevExpress.XtraScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
// using DevExpress.XtraScheduler;

using System.Data;
using System.Data.SqlClient;

namespace PlanowanieDostaw
{
    /// <summary>
    /// Interaction logic for Harmonogram.xaml
    /// </summary>
    public partial class Harmonogram : UserControl
    {

    //    SchedulerTestDataSet dataSet;
     //   SchedulerTestDataSetTableAdapters.AppointmentsTableAdapter adapter;
    //    SchedulerTestDataSetTableAdapters.ResourcesTableAdapter resourcesAdapter;

        public Harmonogram()
        {
            InitializeComponent();
        
         this.Loaded += Window_Loaded;
         }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //scheduler.Start = new System.DateTime(2016, 12, 27);
            scheduler.Start = DateTime.Today;
            // context.MyAppointments.Load();
            //    context.MyResources.Load();
            //   scheduler.Storage.AppointmentStorage.DataSource =
         //   this.scheduler.Storage.AppointmentStorage.DataContext:= ;
            // this.scheduler.Storage.ResourceStorage.DataContext = context.MyResources.Local;
       //     this.scheduler.Storage.AppointmentsInserted +=
        //        new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            this.scheduler.Storage.AppointmentsChanged +=
               new PersistentObjectsEventHandler(Storage_AppointmentsModified);
        //    this.scheduler.Storage.AppointmentsDeleted +=
       //         new PersistentObjectsEventHandler(Storage_AppointmentsModified);


 
        }

        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
            
           // MessageBox.Show("zmieniam");
        }
       public void Refresh()
        {
             
           Dispatcher.BeginInvoke(new Action(() =>
            {
                scheduler.Storage.RefreshData();
                scheduler.UpdateLayout();
            }), System.Windows.Threading.DispatcherPriority.Render);

       //     scheduler.Dispatcher.Invoke(empty, System.Windows.Threading.DispatcherPriority.Render);
            //   scheduler.Storage.RefreshData();
        }

        private void scheduler_ActiveViewChanged(object sender, EventArgs e)
        {
    //        MessageBox.Show("zmieniam na aktywnym");
        }

        private void scheduler_ActiveViewChanging(object sender, DevExpress.Xpf.Scheduler.ActiveViewChangingEventArgs e)
        {
       //     MessageBox.Show("zmieniam na aktywnym");
        }

        private void scheduler_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void scheduler_PopupMenuShowing(object sender, DevExpress.Xpf.Scheduler.SchedulerMenuEventArgs e)
        {
                 //  e.Menu.Items.Clear();
        }
    }
}
