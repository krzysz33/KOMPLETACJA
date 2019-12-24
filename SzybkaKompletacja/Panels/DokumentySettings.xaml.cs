using DevExpress.Xpf.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for Kartoteki.xaml
    /// </summary>
    public partial class DokumentySettings : UserControl
    {
        private bool CanChangeGrid = false;
          string KarotekiGridFlie = "DokumentySettings.xml";
        public static readonly DependencyProperty IsLayoutSaveDokumentSetting = DependencyProperty.Register("IsLayoutSaveDokumentSetting", typeof(bool), typeof(Kartoteki), null);
         public DokumentySettings()
        {
            InitializeComponent();
         //    RestoreView();
        }
        private void RestoreView()
        {
            var version = GetLayoutVersion(KarotekiGridFlie);
            if (string.IsNullOrEmpty(version))
                DXSerializer.SetStoreLayoutMode(dgvListaRodzDok, StoreLayoutMode.UI);
            dgvListaRodzDok.RestoreLayoutFromXml(KarotekiGridFlie);
            DXSerializer.SetStoreLayoutMode(dgvListaRodzDok, StoreLayoutMode.All);
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
            get { return (bool)GetValue(IsLayoutSaveDokumentSetting); }
            set { SetValue(IsLayoutSaveDokumentSetting, value); }
        }
        private void dgvListaPoz_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaRodzDok.SaveLayoutToXml(KarotekiGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }

        private void dgvListaRodzDok_CurrentColumnChanged(object sender, DevExpress.Xpf.Grid.CurrentColumnChangedEventArgs e)
        {
  
        }
    }
}
