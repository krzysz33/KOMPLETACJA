using DevExpress.Xpf.Core.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for Kontrahent.xaml
    /// </summary>
    public partial class ZamowieniaLista : UserControl
    {
        private bool CanChangeGrid = false;
        string ZamowieniaListaGridFlie = ProgramDataSotrage.ProfilePath + "ZamowieniaListaGridFlie.xml";
        string ZamowieniaListaExpFlie = ProgramDataSotrage.ProfilePath + "ZamowieniaListaExpFlie.xml";
 

        public ZamowieniaLista()
        {
            InitializeComponent();
             RestoreView();
             RestoreViewExp();
        }
        private void RestoreView()
        {
            if (File.Exists(ZamowieniaListaGridFlie))
            {
                var version = GetLayoutVersion(ZamowieniaListaGridFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.UI);
                dgvKontrah.RestoreLayoutFromXml(ZamowieniaListaGridFlie);
                DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.All);
                CanChangeGrid = true;
            }
        }

        private void RestoreViewExp()
        {
            if (File.Exists(ZamowieniaListaExpFlie))
            {
                var version = GetLayoutVersion(ZamowieniaListaExpFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvListaLxp, StoreLayoutMode.UI);
                dgvListaLxp.RestoreLayoutFromXml(ZamowieniaListaExpFlie);
                DXSerializer.SetStoreLayoutMode(dgvListaLxp, StoreLayoutMode.All);
                CanChangeGrid = true;
            }
        }
        private void view_Loaded(object sender, RoutedEventArgs e)
        {
            //if (view.SearchControl != null)
            //{
            //    view.SearchControl.Focus();
            //    view.SearchControl.SearchText = string.Empty;
            //}
        }
        private string GetLayoutVersion(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("property");
                return reader.ReadElementContentAsString();
            }
        }
      
        private void detaliczny_selected(object sender, RoutedEventArgs e)
        {

        }

        private void hurtowy_selected(object sender, RoutedEventArgs e)
        {

        }

        private void specjalny_selected(object sender, RoutedEventArgs e)
        {

        }

        private void dgvKontrah_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvKontrah.SaveLayoutToXml(ZamowieniaListaGridFlie);
         
                MessageBox.Show("Zapisałem ustawienia grida zamowienia");
            }
        }

        private void dgvKontrah_MouseDoubleClick(object sender, MouseButtonEventArgs e){}

        private void drvZamowieniaHist_PreviewKeyDown(object sender, KeyEventArgs e){}
 
        private void dgvListaLxp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaLxp.SaveLayoutToXml(ZamowieniaListaExpFlie);
       
                MessageBox.Show("Zapisałem ustawienia grida Exp");
            }
        }
    }
}
