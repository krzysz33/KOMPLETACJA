using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.LayoutControl;
//using MiejscaSkladowania.DAO;
//using MiejscaSkladowania.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace KpInfohelp
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    /// 
    public partial class Kafelki : UserControl
    {
        ViewModelKafelki vmKafelki;
        public Kafelki()
        {
            InitializeComponent();
         }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //_waitIndicatorService.HideSplashScreen();
            //if (e.Error != null)
            //{
            //    DXMessageBox.Show(this, "Błąd połączenia przetwarzania:" + Environment.NewLine + e.Error.ToString(), "ERROR",
            //        MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            //titleLayoutControl.ItemsSource = ProgramDataStorage.LstMscSkl;
            //int ilosc = 0;
            //int suma = 0;
            //foreach (MiejsceSkladowaniaModel item in ProgramDataStorage.LstMscSkl)
            //{
            //    if (item.IsLabel)
            //        continue;
            //    ilosc += item.Ilosc;
            //    suma++;
            //}
            //txtZajetych.Text = ilosc.ToString();
            //txtWolnych.Text = ((16 * suma) - ilosc).ToString();
            //txtRazem.Text = (16 * suma).ToString();
            //txtZajetoscProc.Text = Math.Round(((double)ilosc / (16 * (double)suma)) * 100, 2).ToString();
            //secondsLeft = ProgramDataStorage.SecToRef;
            //timer.Start();
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //try
            //{
            //    MiejsceSkladowaniaDataAdapter miejsceSkladowaniaDA = new MiejsceSkladowaniaDataAdapter();
            //    ProgramDataStorage.LstMscSkl = miejsceSkladowaniaDA.GetAll(ProgramDataStorage.KodMag, first, skip);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void Tile_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ProgramDataStorage.MscSkl = ((Tile)sender).DataContext as MiejsceSkladowaniaModel;
            //if (ProgramDataStorage.MscSkl.IsLabel)
            //    e.Handled = true;
        }

        private void btnSzukaj_Click(object sender, RoutedEventArgs e)
        {
            //if (cmbSzukaj.SelectedItem == null || cmbSzukajParametr.SelectedItem == null || string.IsNullOrEmpty(txtSzukaj.Text))
            //    return;

            //switch ((SearchBy)cmbSzukaj.SelectedItem)
            //{
            //    case SearchBy.DATA_PRODUKCJI:
            //        foreach (MiejsceSkladowaniaModel item in (titleLayoutControl.ItemsSource as CompositeCollection))
            //        {
            //            if (item.IsLabel)
            //                continue;
            //            if (item.lstDostawa.Count == 0)
            //                item.IsVisible = false;
            //            DateTime data = Convert.ToDateTime(txtSzukaj.Text);
            //            foreach (DostawaModel itemDost in item.lstDostawa)
            //            {
            //                if ((SerchParametr)cmbSzukajParametr.SelectedItem == SerchParametr.ROWNE)
            //                {
            //                    if (itemDost.DataDostawy == data)
            //                        item.IsVisible = true;
            //                    else
            //                        item.IsVisible = false;
            //                }
            //                if ((SerchParametr)cmbSzukajParametr.SelectedItem == SerchParametr.OD)
            //                {
            //                    if (itemDost.DataDostawy > data)
            //                        item.IsVisible = true;
            //                    else
            //                        item.IsVisible = false;
            //                }
            //                if ((SerchParametr)cmbSzukajParametr.SelectedItem == SerchParametr.DO)
            //                {
            //                    if (itemDost.DataDostawy < data)
            //                        item.IsVisible = true;
            //                    else
            //                        item.IsVisible = false;
            //                }
            //            }
            //        }
            //        break;
            //    case SearchBy.INDEKS:
            //        foreach (MiejsceSkladowaniaModel item in (titleLayoutControl.ItemsSource as CompositeCollection))
            //        {
            //            if (item.IsLabel)
            //                continue;
            //            if (item.lstDostawa.Count == 0)
            //                item.IsVisible = false;
            //            foreach (DostawaModel itemDost in item.lstDostawa)
            //            {
            //                if (itemDost.Indeks == txtSzukaj.Text)
            //                    item.IsVisible = true;
            //                else
            //                    item.IsVisible = false;
            //            }
            //        }
            //        break;
            //    case SearchBy.NR_PARTII:
            //        foreach (MiejsceSkladowaniaModel item in (titleLayoutControl.ItemsSource as CompositeCollection))
            //        {
            //            if (item.IsLabel)
            //                continue;
            //            if (item.lstDostawa.Count == 0)
            //                item.IsVisible = false;
            //            foreach (DostawaModel itemDost in item.lstDostawa)
            //            {
            //                if (itemDost.NrDostawy == txtSzukaj.Text)
            //                    item.IsVisible = true;
            //                else
            //                    item.IsVisible = false;
            //            }
            //        }
            //        break;
            //}

            //titleLayoutControl.UpdateLayout();
            //titleLayoutControl.UpdateDefaultStyle();
        }

        private void btnWyszyscSzukaj_Click(object sender, RoutedEventArgs e)
        {
            //txtSzukaj.Text = "";
            //foreach (MiejsceSkladowaniaModel item in (titleLayoutControl.ItemsSource as CompositeCollection))
            //{
            //    item.IsVisible = true;
            //}
        }

        private void cmbSzukaj_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //txtSzukaj.Text = "";
            //if ((SearchBy)cmbSzukaj.SelectedItem == SearchBy.DATA_PRODUKCJI)
            //{
            //    txtSzukaj.MaskType = DevExpress.Xpf.Editors.MaskType.DateTime;
            //    cmbSzukajParametr.SelectedIndex = 0;
            //    cmbSzukajParametr.IsEnabled = true;
            //}
            //else
            //{
            //    txtSzukaj.MaskType = DevExpress.Xpf.Editors.MaskType.None;
            //    cmbSzukajParametr.SelectedIndex = 0;
            //    cmbSzukajParametr.IsEnabled = false;
            //}
        }

        private void cmbMagazyn_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateMagView();
        }

        private void UpdateTime()
        {
            //if(secondsLeft <= 0)
            //{
            //    UpdateMagView();
            //}
            //TimeSpan t = TimeSpan.FromSeconds(secondsLeft);
            //liOdswiezanie.Label = string.Format("{0:D2}:{1:D2}", t.Minutes, t.Seconds);
            //secondsLeft -= 1;
        }

        private void UpdateMagView()
        {
            //if (bgWorker.IsBusy)
            //    return;
            //timer.Stop();
            //ProgramDataStorage.KodMag = cmbMagazyn.SelectedValue.ToString();
            //_waitIndicatorService.ShowSplashScreen();
            //bgWorker.RunWorkerAsync();
        }

        private void btnOdsiwez_Click(object sender, RoutedEventArgs e)
        {
            UpdateMagView();
        }

        private void cmbRegal_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            //if (bgWorker.IsBusy)
            //    return;
            //switch ((Regal)cmbRegal.SelectedItem)
            //{
            //    case Regal.WSZYSTKO:
            //        first = 50;
            //        skip = 0;
            //        break;
            //    case Regal.A:
            //        first = 25;
            //        skip = 0;
            //        break;
            //    case Regal.B:
            //        first = 25;
            //        skip = 25;
            //        break;
            //}
            //UpdateMagView();
        }
    }
}
