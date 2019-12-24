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
    /// Interaction logic for Kartoteki.xaml
    /// </summary>
    public partial class DefCeny : UserControl
    {
        private bool CanChangeGrid = false;
          string DefCenyGridFlie = "DefCenyGridFlie.xml";
        public static readonly DependencyProperty IsLayoutSavedDefCeny = DependencyProperty.Register("IsLayoutSavedDefCeny", typeof(bool), typeof(DefCeny), null);
 
        public DefCeny()
        {
            InitializeComponent();
         //    RestoreView();
        }
        private void RestoreView()
        {
            if (File.Exists(Environment.CurrentDirectory + "\\" + DefCenyGridFlie))
            {
                var version = GetLayoutVersion(DefCenyGridFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvListaDef, StoreLayoutMode.UI);
                dgvListaDef.RestoreLayoutFromXml(DefCenyGridFlie);
                DXSerializer.SetStoreLayoutMode(dgvListaDef, StoreLayoutMode.All);
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
            get { return (bool)GetValue(IsLayoutSavedDefCeny); }
            set { SetValue(IsLayoutSavedDefCeny, value); }
        }

        private void dgvListaPoz_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaDef.SaveLayoutToXml(DefCenyGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }

        private void dgvListaPoz_CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            //if (e.IsGetData)
            //{
            //    int odnetto = Convert.ToInt32(e.GetListSourceFieldValue("ODNETTO"));
            //    if (odnetto == 1)
            //        e.Value = true;
            //    if (odnetto == 0)
            //        e.Value = false; 
            //}
        }
    }
}
