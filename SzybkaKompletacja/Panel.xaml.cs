using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for PokazDaneHarmonogram.xaml
    /// </summary>
    public partial class Panel : DXWindow
    {
        public Panel()
        {
            ThemeManager.SetThemeName(this, "TouchlineDark");
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
             
        }

        private void Ilosc_LostFocus(object sender, RoutedEventArgs e)
        {
         //   Waga.Focus();
        }

        private void srWaga_LostFocus(object sender, RoutedEventArgs e)
        {
         //   Waga.Focus();
        }

        private void srWaga_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            //if (srWaga.Text == string.Empty) return;
            //if (Ilosc.Text == string.Empty) return;
            //decimal wagadec = 0;
          
            //decimal iloscdec = decimal.Parse(Ilosc.Text, CultureInfo.InvariantCulture);
            //decimal wagasrdec = decimal.Parse(srWaga.Text, CultureInfo.InvariantCulture);
         
            //wagadec = Math.Round(wagasrdec * iloscdec);
            //Waga.Text = wagadec.ToString();
        }

        private void Ilosc_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            //if (srWaga.Text == string.Empty) return;
            //if (Ilosc.Text == string.Empty) return;
            //decimal wagadec = 0;
            //decimal iloscdec = decimal.Parse(Ilosc.Text, CultureInfo.InvariantCulture);
            //decimal wagasrdec = decimal.Parse(srWaga.Text, CultureInfo.InvariantCulture);
            //wagadec = Math.Round(wagasrdec * iloscdec);
            //Waga.Text = wagadec.ToString();
        }

        private void Waga_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            //if (srWaga.Text == string.Empty) return;
            //if (Ilosc.Text == string.Empty) return;
            //decimal wagadec = 0;
            //decimal iloscdec = decimal.Parse(Ilosc.Text, CultureInfo.InvariantCulture);
            //decimal wagasrdec = decimal.Parse(srWaga.Text, CultureInfo.InvariantCulture);
            //iloscdec  = Math.Round(wagadec / wagasrdec);
            //Ilosc.Text = iloscdec.ToString();
        }

        private void selekcja_selected(object sender, RoutedEventArgs e)
        {

        }

        private void reprodukcja_selected(object sender, RoutedEventArgs e)
        {

        }

        private void brojler_selected(object sender, RoutedEventArgs e)
        {

        }

        private void mindyk_selected(object sender, RoutedEventArgs e)
        {

        }
    }
}
