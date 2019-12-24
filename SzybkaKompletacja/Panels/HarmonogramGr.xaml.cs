using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
using PlanowanieDostaw.ModelViews;
using DevExpress.Xpf.Grid.LookUp;
using System.Xml;
using DevExpress.Xpf.Core.Serialization;
using System.Collections.Specialized;
using System.ComponentModel;

namespace PlanowanieDostaw
{
    /// <summary>
    /// Interaction logic for HarmonogramGr.xaml
    /// </summary>

    public partial class HarmonogramGr : UserControl,INotifyPropertyChanged
    {
        private bool CanChangeGrid = false;
        private DateTime oldDate;
        const string HarmonogramGrFile = "HarmonogramGr.xml";
        public static readonly DependencyProperty IsLayoutSavedHarmonogramGrDzienny = DependencyProperty.Register("HarmonogramGrDzienny", typeof(bool), typeof(HarmonogramGr), null);

      public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedHarmonogramGrDzienny); }
            set { SetValue(IsLayoutSavedHarmonogramGrDzienny, value); }
        }
        private void RestoreView()
        {
            var version = GetLayoutVersion(HarmonogramGrFile);
            if (string.IsNullOrEmpty(version))
                DXSerializer.SetStoreLayoutMode(drvHarmonogram, StoreLayoutMode.UI);
            drvHarmonogram.RestoreLayoutFromXml(HarmonogramGrFile);
            DXSerializer.SetStoreLayoutMode(drvHarmonogram, StoreLayoutMode.All);
            CanChangeGrid = true;
        }
        DateTime _data;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
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
        public List<string> Items
        {
            get
            {
                return new List<string>() { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5", "Item 6", "Item 7", "Item 8", "Item 9" };
            }
        }
         KartModelGat wGatunek;
         KontrahModel wKontrah;
        SqlStatment config;
        ListCollectionView itemSourceCollectionView;
        ObservableCollection<ArItHarmonogram> obLstHarmonogram;
        List<ArItHarmonogram> lstHarmonogram;
        List<ArItHarmonogram> lstHarmonogramTemp;

        public event PropertyChangedEventHandler PropertyChanged;
 
        private void Item_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HarmonogramDataAdapter hda = new HarmonogramDataAdapter();
             ArItHarmonogram row = (ArItHarmonogram)sender;
              hda.PrzepiszDane(row);
         }
        public HarmonogramGr()
        {
            InitializeComponent();
        }
        public void RefreshAllData()
        {
         }

        public void GotToRec()
         {
            // if (ProgramDataSotrage.currentParametryChowuPoz.IdParametryChowuModelPoz > 0)
            //{
            //    int obj = drvHarmonogram.FindRowByValue("IdArItParametryChowuPoz", ProgramDataSotrage.currentParametryChowuPoz.IdParametryChowuModelPoz);
            //    if(obj>0)
            //             itemSourceCollectionView.MoveCurrentToPosition(obj);
            //}
        }

        public void RefreshData()
        {
   
    }
        private void drvHarmonogram_AutoGeneratingColumn(object sender, DevExpress.Xpf.Grid.AutoGeneratingColumnEventArgs e)
        {
            if(config==null)
             config = ProgramDataSotrage.xmlSqlConfig.sqlStatments.Where(x => x.Name == "HarmonogramGrid").SingleOrDefault();

            string prName = e.Column.ActualColumnChooserHeaderCaption.ToString().Replace(" ", "");
            Column colConfig = config.columns.Where(x => x.Binding == prName).FirstOrDefault();

            if (colConfig != null)
            {
                if (colConfig.Visable)
                {
                    e.Column.Visible = true;
                    e.Column.Width = colConfig.Width;
                    e.Column.Header = colConfig.Text;
                   return;
                }
                else
                  e.Column.Visible = false;
            }
        
        }
    
        private void tydzien_selected(object sender, RoutedEventArgs e)
        {
        }

        private void lupWybierzGatunek_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lookup = sender as LookUpEdit;

            if (lookup != null)
                lookup.EditValue = null;
        }
        private void lueKontrah_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lookup = sender as LookUpEdit;

            if (lookup != null)
               lookup.EditValue = null;

        }
  
        private void drvHarmonogram_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                drvHarmonogram.SaveLayoutToXml(HarmonogramGrFile);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }
 
    }
}

