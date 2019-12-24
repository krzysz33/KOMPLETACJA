using DevExpress.Xpf.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
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
using System.IO;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for OknoUzytkownikow.xaml
    /// </summary>
    public partial class OknoUzytkownikow : UserControl
    {
        private bool CanChangeGrid = false;
          string OknoUzytkownikowGridFlie =  "OknoUzytkownikowGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedOkonoUz = DependencyProperty.Register("IsLayoutSavedOknoUz", typeof(bool), typeof(OknoUzytkownikow), null);

        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedOkonoUz); }
            set { SetValue(IsLayoutSavedOkonoUz, value); }
        }

        List<IHP_ZAM_USERS> lstUsersSl;
        IHP_ZAM_USERS currentUser;
        ObservableCollection<IHP_ZAM_USERS> observlstUsersSl;
        ListCollectionView itemSourceCollectionView;
  
        bool Edycja = false;

        public OknoUzytkownikow()
        {
            InitializeComponent();
            RestoreView();
        }


        private void RestoreView()
        {
            if(File.Exists(Environment.CurrentDirectory + "\\"+ OknoUzytkownikowGridFlie))
            {
            var version = GetLayoutVersion(OknoUzytkownikowGridFlie);
            if (string.IsNullOrEmpty(version))
                DXSerializer.SetStoreLayoutMode(dgvTabelaUprawnien, StoreLayoutMode.UI);
            dgvTabelaUprawnien.RestoreLayoutFromXml(OknoUzytkownikowGridFlie);
            DXSerializer.SetStoreLayoutMode(dgvTabelaUprawnien, StoreLayoutMode.All);
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
        private void Clear()
        {
            txtUser.IsEnabled = true;
            txtNazwa.Text = string.Empty;
            txtUser.Text = string.Empty;
            txtPassword.Text = string.Empty;
            currentUser = null;
        }
       private void ButtonKHAdd_Click(object sender, RoutedEventArgs e)
        {

        }
        private void dgvTabelaUprawnien_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (IHP_ZAM_USERS)dgvTabelaUprawnien.SelectedItem;
            if (row != null)
            {
                currentUser = row;
                Edycja = true;
          
            }
        }
        private void txtUser_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (currentUser == null)
                currentUser = new IHP_ZAM_USERS();
            currentUser.LOGIN = txtUser.Text;
        }

        private void txtNazwa_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (currentUser == null)
                currentUser = new IHP_ZAM_USERS();
            currentUser.NAZWISKO_IMIE = txtNazwa.Text;
        }

        private void txtPassword_EditValueChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            if (currentUser == null)
                currentUser = new IHP_ZAM_USERS();
            currentUser.HASLO = PasswordManager.Encrypt(txtPassword.Text);
        }

        private void txtUser_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (Edycja) return;
            if (e.Value == null) return;
            if (lstUsersSl != null)
            {
                var spr = lstUsersSl.Where(x => x.LOGIN == e.Value.ToString()).ToList();
                if (spr.Count > 0)
                {
                    e.IsValid = false;
                    e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical;
                    e.ErrorContent = "Taki Login już istnieje";
                }
            }
        }

        private void txtNazwa_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (e.Value == null) return;
            if (e.Value.ToString().Length > 4) return;
            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            e.ErrorContent = "Nazwa musi mieć co najmniej 4 znaki.";
        }

        private void txtPassword_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e)
        {
            if (e.Value == null) return;
            if (e.Value.ToString().Length > 3) return;
            e.IsValid = false;
            e.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Information;
            e.ErrorContent = "Hasło musi mieć co najmniej 4 znaki.";
        }

        private void dgvTabelaUprawnien_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvTabelaUprawnien.SaveLayoutToXml(OknoUzytkownikowGridFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida");
            }
        }


       private void ButtonAdd_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }

        private void chkDodajStratus_Checked(object sender, RoutedEventArgs e)
        {
           // grDodajStatus.Visibility = Visibility.Visible;
        }

        private void chkDodajStratus_Unchecked(object sender, RoutedEventArgs e)
        {
            //grDodajStatus.Visibility = Visibility.Collapsed;
        }

        private void ButtonAddNew_Click(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {

        }
    }
  }
 

