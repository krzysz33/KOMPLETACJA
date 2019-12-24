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
    public partial class Usluga : UserControl
    {
       
          string OknoUslugaGridFlie =  "UslugaGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedCennik = DependencyProperty.Register("IsLayoutSavedCennik", typeof(bool), typeof(Usluga), null);

        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedCennik); }
            set { SetValue(IsLayoutSavedCennik, value); }
        }

        public Usluga()
        {
            InitializeComponent();
      //    RestoreView();
        }
        private void RestoreView()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + OknoUslugaGridFlie))
            {
                var version = GetLayoutVersion(OknoUslugaGridFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.UI);
                dgvListaPoz.RestoreLayoutFromXml(OknoUslugaGridFlie);
                DXSerializer.SetStoreLayoutMode(dgvListaPoz, StoreLayoutMode.All);
       
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
                dgvListaPoz.SaveLayoutToXml(OknoUslugaGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida Cennik");
            }
        }

        private void luPojazdySearch_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
