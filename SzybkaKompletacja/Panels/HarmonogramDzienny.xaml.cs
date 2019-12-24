using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using DevExpress.XtraScheduler;
using DevExpress.Xpf.Core.Serialization;
using System.Xml;
using System.IO;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for HarmonogramDzienny.xaml
    /// </summary>
    public partial class HarmonogramDzienny : UserControl
    {
     
          string HarmonogramDziennyGridFile = ProgramDataSotrage.ProfilePath + "HarmonogramDziennyGrid.xml";
        string HarmonogramDzScheuderGrid = ProgramDataSotrage.ProfilePath + "HarmonogramDzScheuderGrid.xml";
        string DzScheuderGrid = ProgramDataSotrage.ProfilePath + "ScheuderGrid.xml";

        public static readonly DependencyProperty IsLayoutSavedGrDzienny = DependencyProperty.Register("IsLayoutSavedGrDzienny", typeof(bool), typeof(HarmonogramDzienny), null);
        public static readonly DependencyProperty IsLayoutSavedGrSchr = DependencyProperty.Register("IsLayoutSavedGrSchr", typeof(bool), typeof(HarmonogramDzienny), null);

        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedGrDzienny); }
            set { SetValue(IsLayoutSavedGrDzienny, value); }
        }

        public bool IsLayoutSavedSch
        {
            get { return (bool)GetValue(IsLayoutSavedGrSchr); }
            set { SetValue(IsLayoutSavedGrSchr, value); }
        }

        public HarmonogramDzienny()
        {
            InitializeComponent();
           RestoreView();
            RestoreViewSch();
        }

        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e)
        {
       //     MessageBox.Show("zmieniam");
         //   context.SaveChanges();
        }
 
        private void RestoreView()
        {
            if( File.Exists(HarmonogramDziennyGridFile))
            {
            var version = GetLayoutVersion(HarmonogramDziennyGridFile);
            if (string.IsNullOrEmpty(version))
                DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.UI);
            dgvListaPoz.RestoreLayoutFromXml(HarmonogramDziennyGridFile);
            DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.All);
        
            }
        }

        private void RestoreViewSch()
        {
            if (File.Exists(HarmonogramDziennyGridFile))
            {
                var version = GetLayoutVersion(HarmonogramDzScheuderGrid);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(gridControl, StoreLayoutMode.UI);
                gridControl.RestoreLayoutFromXml(HarmonogramDzScheuderGrid);
                DXSerializer.SetStoreLayoutMode(gridControl, StoreLayoutMode.All);
       
            }
        }


        private string GetLayoutVersion(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("property");
                return reader.ReadElementContentAsString();
            }
        }
        private void dgvListaPoz_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaPoz.SaveLayoutToXml(HarmonogramDziennyGridFile);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }
        private void dgvListaPoz_CurrentColumnChanged(object sender, DevExpress.Xpf.Grid.CurrentColumnChangedEventArgs e)
        {
        }




        private void dgvListaPoz_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ButtonInfo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gridControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                gridControl.SaveLayoutToXml(HarmonogramDzScheuderGrid);
                IsLayoutSavedSch = true;
                MessageBox.Show("Zapisałem ustawienia grida GridScheuder");
            }
        }
    }
}
