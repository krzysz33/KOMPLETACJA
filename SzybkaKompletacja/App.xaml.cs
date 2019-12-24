using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Waga _waga = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            Messenger.Default.Register<int>(this, OnMessageUser);
            _waga = Waga.GetInstance;
            if (_waga != null)
                _waga.StartWaga();
         }

        public void OnMessageUser(int reset)
        {
            if(reset==999)
            {
                // _waga = new Waga();
                _waga = Waga.GetInstanceDispose; 
                if (_waga != null)
                    _waga.StartWaga();

            }
 
        }

        private void OnAppStartup_UpdateThemeName(object sender, StartupEventArgs e)
        {
            DevExpress.Xpf.Core.ApplicationThemeHelper.UpdateApplicationThemeName();
        }
 
    }
}
