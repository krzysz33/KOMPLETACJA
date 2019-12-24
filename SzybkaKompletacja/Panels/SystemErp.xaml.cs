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

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for SystemErp.xaml
    /// </summary>
    public partial class SystemErp : UserControl
    {
        public SystemErp()
        {
            InitializeComponent();
        }

        private void chkKartotekiAll_Checked(object sender, RoutedEventArgs e)
        {
            chkRodzKart.IsChecked = false;
            chkKartoteki.IsChecked = false;
        }

        private void chkRodzKart_Checked(object sender, RoutedEventArgs e)
        {
            chkKartotekiAll.IsChecked = false;
            chkKartoteki.IsChecked = false;

        }
 
        private void chkKartoteki_Checked(object sender, RoutedEventArgs e)
        {
            chkKartotekiAll.IsChecked = false;
            chkRodzKart.IsChecked = false;
        }

        private void chkCenniki_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void chkKontrahAll_Checked(object sender, RoutedEventArgs e)
        {
            chkKontrahGrupa.IsChecked = false;
        }

        private void chkKontrahGrupa_Checked(object sender, RoutedEventArgs e)
        {
            chkKontrahAll.IsChecked = false;
        }

        private void chkDokDirect_Checked(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}
