using DevExpress.Xpf.Core.Serialization;
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
    /// Interaction logic for GrupaKart.xaml
    /// </summary>
    public partial class GrupaKart : UserControl
    {
        private bool CanChangeGrid = false;
          string GrupaKartGridFlie = ProgramDataSotrage.ProfilePath +  "GrupaKartGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedOknoGrupaKart = DependencyProperty.Register("IsLayoutSavedOknoGrupaKart", typeof(bool), typeof(GrupaKart), null);
        public GrupaKart()
        {
            InitializeComponent();
            RestoreView();
        }
        private void RestoreView()
        {
            if(File.Exists(GrupaKartGridFlie))
            {
         
           //var version = GetLayoutVersion(GrupaKartGridFlie);
           // if (string.IsNullOrEmpty(version))
           //     DXSerializer.SetStoreLayoutMode(dgvGrupaKart, StoreLayoutMode.UI);
           // dgvGrupaKart.RestoreLayoutFromXml(GrupaKartGridFlie);
           // DXSerializer.SetStoreLayoutMode(dgvGrupaKart, StoreLayoutMode.All);
           // CanChangeGrid = true;
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
            get { return (bool)GetValue(IsLayoutSavedOknoGrupaKart); }
            set { SetValue(IsLayoutSavedOknoGrupaKart, value); }
        }

        private void dgvGrupaKart_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                //dgvGrupaKart.SaveLayoutToXml(GrupaKartGridFlie);
                //IsLayoutSaved = true;
                //MessageBox.Show("Zapisałem ustawienia grida");
            }
        }
    }
}
