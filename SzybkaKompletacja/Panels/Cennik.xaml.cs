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
    /// Interaction logic for Cennik.xaml
    /// </summary>
    public partial class Cennik : UserControl
    {
        private bool CanChangeGrid = false;
          string OknoCennikGridFlie =  "CennikGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedCennik = DependencyProperty.Register("IsLayoutSavedCennik", typeof(bool), typeof(Cennik), null);

        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedCennik); }
            set { SetValue(IsLayoutSavedCennik, value); }
        }
        public Cennik()
        {
            InitializeComponent();
      //   RestoreView();
        }
        private void RestoreView()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + OknoCennikGridFlie))
            {
                var version = GetLayoutVersion(OknoCennikGridFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvCennik, StoreLayoutMode.UI);
                dgvCennik.RestoreLayoutFromXml(OknoCennikGridFlie);
                DXSerializer.SetStoreLayoutMode(dgvCennik, StoreLayoutMode.All);
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
        private void dgvCennik_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvCennik.SaveLayoutToXml(OknoCennikGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida Cennik");
            }
        }
    }
}
