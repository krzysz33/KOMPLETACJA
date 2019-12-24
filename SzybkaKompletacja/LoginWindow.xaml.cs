using DevExpress.Xpf.Core;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : System.Windows.Window
    {
   //     private ViewModelLogin viewModel;

        public LoginWindow()

        {
            InitializeComponent();
         //   viewModel = new ViewModelLogin();
         //   this.DataContext = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string LastMessage;
            try
            {

           
            CultureInfo culture = CultureInfo.CreateSpecificCulture("pl-PL");
            CultureInfo uiCulture = CultureInfo.CreateSpecificCulture("pl");

            Thread.CurrentThread.CurrentCulture = uiCulture;
            Thread.CurrentThread.CurrentUICulture = uiCulture;

            CultureInfo.DefaultThreadCurrentCulture = uiCulture;
            CultureInfo.DefaultThreadCurrentUICulture = uiCulture;

            ////if (!databasemanager.checkandsaveconnection())
            ////{
            ////    dxmessagebox.show(this, "błąd połączenia z bazą danych", "błąd!", messageboxbutton.ok, messageboximage.error);
            ////    application.current.shutdown();
            ////}
        //    ProgramDataSotrage.RefreshData();
    //    ThemeManager.SetThemeName(this, ProgramDataSotrage.xmlSqlConfig.defaultThemeName);
            txtLogin.Focus();
            }
            catch (Exception ex)
            {

                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;

            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void txtLogin_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Login();
        }

        private void Login()
        {
        
            string LastMessage;
            try {
                if (!string.IsNullOrEmpty(txtHaslo.Password) && !string.IsNullOrEmpty(txtLogin.Text))
                {

                    //ARIT_ZAM_USERS userModel = viewModel.DoLogin(txtLogin.Text, txtHaslo.Password);

                    //if (userModel == null)
                    //{
                    //    DXMessageBox.Show("Niepoprawne dane logowania.");
                    //    return;
                    //}
                //    else
                  //      ProgramDataSotrage.User = userModel;
                     /*
                     if (userModel.ResetHasla)
                     {
                         PasswordChangeWindow passwordChangeWindow = new PasswordChangeWindow(userModel.IdAritZamUsers);
                         passwordChangeWindow.Owner = this;
                         passwordChangeWindow.ShowDialog();
                         if (!passwordChangeWindow.isPasswordChanged)
                             return;
                     }
                     */

                     MainWindow mainWindow = new MainWindow();
                 //   mainWindow.menuLoginLabel.Content = "Zalogowany jako:    " + userModel.NAZWISKO_IMIE;
                    mainWindow.Show();
                    Close();
                }
            }
            catch (Exception ex)
            {
               
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
      //      Login();
        }
        private void txtUser_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e) { }

        private void txtPassword_Validate(object sender, DevExpress.Xpf.Editors.ValidationEventArgs e){ }

        private void txtHaslo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Key == Key.Enter)
            //    Login();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

     
    }
}
 