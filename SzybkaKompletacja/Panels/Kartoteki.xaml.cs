using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class Kartoteki : UserControl
    {
        private bool CanChangeGrid = false;
        string KarotekiGridFlie = ProgramDataSotrage.ProfilePath + "KarotekiGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedKart = DependencyProperty.Register("IsLayoutSavedOknoUz", typeof(bool), typeof(Kartoteki), null);

        public Kartoteki()
        {
            InitializeComponent();
            RestoreView();
        }
        private void RestoreView()
        {
            if (File.Exists(KarotekiGridFlie))
            {
                var version = GetLayoutVersion(KarotekiGridFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.UI);
                dgvListaPoz.RestoreLayoutFromXml(KarotekiGridFlie);
                DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.All);
                CanChangeGrid = true;
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
        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedKart); }
            set { SetValue(IsLayoutSavedKart, value); }
        }

        private void dgvListaPoz_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaPoz.SaveLayoutToXml(KarotekiGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }

        private void TableView_PreviewKeyDown(object sender, KeyEventArgs e)
        {

       
        }
    }
}
