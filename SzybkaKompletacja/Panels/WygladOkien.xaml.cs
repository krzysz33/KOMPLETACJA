using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Grid;
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
    public partial class WygladOkien : UserControl
    {
        public WygladOkien()
        {
            InitializeComponent();
            var viewmodel = new ViewModelWygladOkien();
            this.Loaded += (s, e) =>
            {
                this.DataContext = viewmodel;
            };
        }
        private void tree_ItemsSourceChanged(object sender, ItemsSourceChangedEventArgs e)
        {
          //   Dispatcher.BeginInvoke(new Action(() => { ((TreeListControl)sender).View.ExpandAllNodes(); }));
        }

    }
}
