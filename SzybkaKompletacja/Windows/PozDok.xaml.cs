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
    public partial class PozDok : Window
    {
 
          
        public static readonly DependencyProperty IsLayoutDokumenty = DependencyProperty.Register("IsLayoutPozDok", typeof(bool), typeof(PozDok), null);
 
        public PozDok()
        {
            InitializeComponent();
       }
        private void luPojazdy_CustomDisplayText(object sender, DevExpress.Xpf.Editors.CustomDisplayTextEventArgs e)
        {

       
            }

        private void simpleButton_Click(object sender, RoutedEventArgs e)
        {
            ///
        }

        private void Tile_Click(object sender, EventArgs e)
        {

        }
    }
   
}
