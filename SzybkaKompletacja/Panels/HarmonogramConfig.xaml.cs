using DevExpress.Mvvm;
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

namespace PlanowanieDostaw
{
    /// <summary>
    /// Interaction logic for HarmonogramConfig.xaml
    /// </summary>
    public partial class HarmonogramConfig : UserControl
    {
        ArItHarmonogramNagl row;
        List<DniTydgodnia> DniTygodnia;
        public HarmonogramConfig()
        {
            InitializeComponent();
            PokazHarmonogramNagl();
            WypelnijListe();
        }


        private void PokazHarmonogramNagl()
        {
            List<ArItHarmonogramNagl> lstHarNagl = HarmonogramDataAdapter.GetHarmonogramNagl();
            gridControl.ItemsSource = lstHarNagl;
        }

        private void PokazHarmonogram(int rok)
        {
            List<ArItHarmonogram> lstHarNagl = HarmonogramDataAdapter.GetHarmonogramRok(rok);
            gridHarmonogram.ItemsSource = lstHarNagl;
        }

        private void PokazDniWolne(int IdNagl)
        {
            List<ArItHarmonogramNaglDniWolne> lstHarNagl = HarmonogramDataAdapter.GetHarmonogramDniWolne(IdNagl);
            gridDniWolne.ItemsSource = lstHarNagl;
        }
        private void gridControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                row = (ArItHarmonogramNagl)gridControl.SelectedItem;
                if (row != null)
                {
                    PokazHarmonogram(row.Rok);
                    PokazDniWolne(row.IdArItHarmonogramNagl);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpiły błedy przy pobieraniu listy harmonogramu -sprawdz logi błedu");
                throw ex;
            }
        }
        private void deleteRowItem_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            if (row != null)
            {
                HarmonogramDataAdapter.DelHarmonogram(row.IdArItHarmonogramNagl);
                PokazHarmonogramNagl();
            }
        }
        private void updateSchedule_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            //
        }
        private void btnDodajRok_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                row = (ArItHarmonogramNagl)gridControl.SelectedItem;
                if (row != null)
                {
                    HarmonogramDataAdapter hda = new HarmonogramDataAdapter();
                    hda.GenHarmonogram();
                    PokazHarmonogramNagl();
                    MessageBox.Show("Wygenerowane");
                }
                else
                {
                    MessageBox.Show("Wybierz Rok");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpiły błedy przy pobieraniu listy harmonogramu -sprawdz logi błedu:" + ex.Message);
                throw ex;
            }
        }
        private void btnDniWolne_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                row = (ArItHarmonogramNagl)gridControl.SelectedItem;
                if (row != null)
                {
                    HarmonogramDataAdapter hda = new HarmonogramDataAdapter();
                    hda.GenDniWolne(row.IdArItHarmonogramNagl);
                    PokazDniWolne(row.IdArItHarmonogramNagl);
                    Messenger.Default.Send<int>(1);
                }
                else
                {
                    MessageBox.Show("Wybierz Rok");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpiły błedy przy pobieraniu listy harmonogramu -sprawdz logi błedu:" + ex.Message);
                throw ex;
            }
        }

       private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            //
        }


        void WypelnijListe()
        {
            DniTygodnia = new List<DniTydgodnia>();
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "PONIEDZIAŁEK", Id = 1 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "WTOREK", Id = 2 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "ŚRODA", Id = 3 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "CZWARTEK", Id = 4 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "PIĄTEK", Id = 5 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "SOBOTA", Id = 6 });
            DniTygodnia.Add(new DniTydgodnia() { Nazwa = "NIEDZIELA", Id = 0 });
            ComboBoxDzienTyg.ItemsSource = DniTygodnia;
        }
        private void DXTabControl_SelectionChanged(object sender, DevExpress.Xpf.Core.TabControlSelectionChangedEventArgs e)
        {

        }
        private void btnDodajRok_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            try
            {
                row = (ArItHarmonogramNagl)gridControl.SelectedItem;
                if (row != null)
                {
                    HarmonogramDataAdapter hda = new HarmonogramDataAdapter();
                    hda.GenHarmonogram();
                    PokazHarmonogramNagl();
                    MessageBox.Show("Wygenerowane");
                }
                else
                {
                    MessageBox.Show("Wybierz Rok");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpiły błedy przy pobieraniu listy harmonogramu -sprawdz logi błedu:" + ex.Message);
                throw ex;
            }

        }


        private void DodajDzien_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ArItHarmonogramNaglDniWolne ith = new ArItHarmonogramNaglDniWolne();
    
           if (row == null)
                {
                MessageBox.Show("Wybierz Rok z Listy");
                return;
            }
            DateTime data = dtData.DateTime;


            if (row != null)
            {
               if(row.Rok !=  data.Year)
                {
                    MessageBox.Show("Wybierz właściwy okres (Rok)");
                    return;
                }
                 HarmonogramDataAdapter hDa = new HarmonogramDataAdapter();
                ith.IdArItHarmonogramNagl = row.IdArItHarmonogramNagl;
                hDa.DodajDzienWolny(ith, data, mUwagi.Text);
                PokazHarmonogram(row.Rok);
                PokazDniWolne(row.IdArItHarmonogramNagl);
                Messenger.Default.Send<int>(1);
            }
        }

        private void DelDzien_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ArItHarmonogramNaglDniWolne ith = new ArItHarmonogramNaglDniWolne();

            if (row == null)
            {
                MessageBox.Show("Wybierz Rok z Listy");
                return;
            }
            DateTime data = dtData.DateTime;


            if (row != null)
            {
                if (row.Rok != data.Year)
                {
                    MessageBox.Show("Wybierz właściwy okres (Rok)");
                    return;
                }
                HarmonogramDataAdapter hDa = new HarmonogramDataAdapter();
                ith.IdArItHarmonogramNagl = row.IdArItHarmonogramNagl;
                hDa.DelDzienWolny(ith, data, mUwagi.Text);
                PokazHarmonogram(row.Rok);
                PokazDniWolne(row.IdArItHarmonogramNagl);
                Messenger.Default.Send<int>(1);
            }
        }




        private void DodajDzienTyg_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ArItHarmonogramNaglDniWolne ith = new ArItHarmonogramNaglDniWolne();
            if (row == null)
            {
                MessageBox.Show("Wybierz Rok z Listy");
                return;
            }


            if (ComboBoxDzienTyg.Text == String.Empty)
            {
                MessageBox.Show("Wybierz Dzień Tygodnia");
                return;
            }


            int numerDnia = (int)ComboBoxDzienTyg.EditValue;

            if (row != null)
            {
                HarmonogramDataAdapter hDa = new HarmonogramDataAdapter();
                int IdArItHarmonogramNagl = row.IdArItHarmonogramNagl;
                hDa.GenDniWolneDlaDnia(IdArItHarmonogramNagl, numerDnia, mUwagi.Text);
                PokazHarmonogram(row.Rok);
                PokazDniWolne(row.IdArItHarmonogramNagl);
                Messenger.Default.Send<int>(1);
            }
        }



        private void DodajWolnezProc_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
//
        }

        private void DeleteDzienTyg_Click_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ArItHarmonogramNaglDniWolne ith = new ArItHarmonogramNaglDniWolne();
            if (row == null)
            {
                MessageBox.Show("Wybierz Rok z Listy");
                return;
            }


            if (ComboBoxDzienTyg.Text == String.Empty)
            {
                MessageBox.Show("Wybierz Dzień Tygodnia");
                return;
            }


            int numerDnia = (int)ComboBoxDzienTyg.EditValue;

            if (row != null)
            {
                HarmonogramDataAdapter hDa = new HarmonogramDataAdapter();
                int IdArItHarmonogramNagl = row.IdArItHarmonogramNagl;
                hDa.DelDniWolneDlaDnia(IdArItHarmonogramNagl, numerDnia, mUwagi.Text);
                PokazHarmonogram(row.Rok);
                PokazDniWolne(row.IdArItHarmonogramNagl);
                Messenger.Default.Send<int>(1);
            }
        }

  
    }
    class DniTydgodnia
    {
        public int Id { get; set; }
        public string Nazwa { get; set; }
    }
}
 
