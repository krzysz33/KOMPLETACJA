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
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Core;
using System.Threading;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Docking;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DXWindow
    {
        string DockingLayout =   ProgramDataSotrage.ProfilePath + "DockingLayout.xml";
 
       private string LastMessage = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
             GridControlLocalizer.Active = new CustomDXGridLocalizer();
             DXMessageBoxLocalizer.Active = new CustomDXMessageBoxLocalizer();
              RestoreSettins();
        }
 

        private void DXRibbonWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F4)
            {
                SaveSettings();
                MessageBox.Show("Ustawienia Zapisane");
            }
        }


        public void SaveSettings()
        {
            docManager.SaveLayoutToXml(DockingLayout);
        }

        private void RestoreSettins()
        {
            if (System.IO.File.Exists(DockingLayout)) 
                docManager.RestoreLayoutFromXml(DockingLayout);
        }

        private void dockLayoutManager_DockItemEndDocking(object sender, DevExpress.Xpf.Docking.Base.DockItemDockingEventArgs e)
        {
            //Console.WriteLine("{0}, docktype {1}, item {2}, target {3}", "EndDocking", e.DockType, e.Item.Name, e.DockTarget.Name);
            //var items = (sender as DockLayoutManager).GetItems();
            //foreach (var item in items)
            //{
            //    Console.WriteLine("item {0}, Name \"{1}\",  parent {2}, parent Name \"{3}\"", item, item.Name ?? "", item.Parent == null ? "<null>" : item.Parent.ToString(), item.Parent == null ? "" : item.Parent.Name ?? "");
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //   dockLayoutManager.SaveLayoutToXml(layoutPath);
        }

        private void dockLayoutManager_DockItemStartDocking(object sender, DevExpress.Xpf.Docking.Base.ItemCancelEventArgs e)
        {
            Console.WriteLine("{0}, item {1}", "StartDocking", e.Item.Name);

        }
    }
 
}
