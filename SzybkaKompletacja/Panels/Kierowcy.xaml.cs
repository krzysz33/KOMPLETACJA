using DevExpress.Xpf.Core.Serialization;
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
using System.Xml;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for Kontrahent.xaml
    /// </summary>
    public partial class Kierowcy : UserControl
    {
  
        public Kierowcy()
        {
            InitializeComponent();
            // RestoreView();
        }
        /*
        private void RestoreView()
        {
            var version = GetLayoutVersion(KontrahGridFlie);
            if (string.IsNullOrEmpty(version))
                DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.UI);
            dgvKontrah.RestoreLayoutFromXml(KontrahGridFlie);
            DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.All);
            CanChangeGrid = true;
        }
        private string GetLayoutVersion(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("property");
                return reader.ReadElementContentAsString();
            }
        }
        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedOknoKontrah); }
            set { SetValue(IsLayoutSavedOknoKontrah, value); }
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
                dgvKontrah.SaveLayoutToXml(KontrahGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida Kontrahnet");
            }
        }

        private void dgvKontrah_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FormKh.IsCollapsed)
                 FormKh.IsCollapsed = false;
            else
                FormKh.IsCollapsed = true;
        }
        */
    }
}
