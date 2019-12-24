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
    public partial class Import : UserControl
    {
        //AppConfig app = AppConfig.GetInstance;

 
        public static readonly DependencyProperty IsLayoutSavedOknoImport = DependencyProperty.Register("IsLayoutSavedOknoImport", typeof(bool), typeof(DaneFirmy), null);

        public Import()
        {
            InitializeComponent();
       //      RestoreView();
        }
        private void RestoreView()
        {
 
          
        }

        private void BarButtonItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }
    }
}
