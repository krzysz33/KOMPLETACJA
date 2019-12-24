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
    public partial class Licencja : UserControl
    {
        private bool CanChangeGrid = false;
          string OknoLicencjaGridFlie =  "LicencjaGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedLicencja = DependencyProperty.Register("IsLayoutSavedLicencja", typeof(bool), typeof(Cennik), null);
       public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedLicencja); }
            set { SetValue(IsLayoutSavedLicencja, value); }
        }
        public Licencja()
        {
            InitializeComponent();
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
     
        }
    }
}
