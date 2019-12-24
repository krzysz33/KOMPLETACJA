using DevExpress.Xpf.Core;
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
    public partial class Trasy : UserControl
    {
        
          string OknoCechyGridFlie = ProgramDataSotrage.ProfilePath + "CechyGrid.xml";
      public Trasy()
        {
            InitializeComponent();
        //  RestoreView();
        }
        //private void RestoreView()
        //{
        //    if (File.Exists(Environment.CurrentDirectory + "\\" + OknoCechyGridFlie))
        //    {
        //        var version = GetLayoutVersion(OknoCechyGridFlie);
        //        if (string.IsNullOrEmpty(version))
        //            DXSerializer.SetStoreLayoutMode(dgvCechy, StoreLayoutMode.UI);
        //        dgvCechy.RestoreLayoutFromXml(OknoCechyGridFlie);
        //        DXSerializer.SetStoreLayoutMode(dgvCechy, StoreLayoutMode.All);
        //        CanChangeGrid = true;
        //    }
        //}

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
                dgvCechy.SaveLayoutToXml(OknoCechyGridFlie);
                MessageBox.Show("Zapisałem ustawienia grida Cennik");
            }
        }
    }
}
