using DevExpress.Mvvm;

using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.POCO;
using System.IO;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Ribbon;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Docking.Base;                                                                                              
using System.Windows.Markup;
using System.Windows;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace KpInfohelp
{
    
    //
    public class ViewModelMain : CrudVMBase, INotifyPropertyChanged
    {
        AppConfig app = new AppConfig();
         IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        public virtual IDialogService LoginDialogService { get { return null; } }
        public virtual ICurrentWindowService CurrentWindowService { get { return null; } }
        public virtual IMainWindowService MainWindowService { get { return null; } }
        public virtual IMainLoginService MainLoginService { get { return null; } }
        public virtual IDockLayoutService LayoutService { get { return null; } }
        private string _panelnavicaption;
        public string PanelNaviCaption
        {
            get
            {
                return _panelnavicaption;
            }
            set
            {
                _panelnavicaption = value;
                RisePropertyChanged("PanelNaviCaption");
            }
        }
        private bool _panelnavi;
        public bool PanelNavi
        {
            get
            {
                return _panelnavi;
            }
            set
            {
                _panelnavi = value;
                RisePropertyChanged("PanelNavi");
            }
        }
        private bool _isPanelKierowcy;
        public bool PanelKierowcy
        {
            get
            {
                return _isPanelKierowcy;
            }
            set
            {
                _isPanelKierowcy = value;
                RisePropertyChanged("PanelKierowcy");
            }
        }
        private bool _isPozDok;
        public bool PozDok
        {
            get
            {
                return _isPozDok;
            }
            set
            {
                _isPozDok = value;
                RisePropertyChanged("PozDok");
            }
        }
        private bool _isDokSettings;
        public bool DokSettings
        {
            get
            {
                return _isDokSettings;
            }
            set
            {
                _isDokSettings = value;
                RisePropertyChanged("DokSettings");
            }
        }
        private bool _isPanelImport;
        public bool PanelImport
        {
            get
            {
                return _isPanelImport;
            }
            set
            {
                _isPanelImport = value;
                RisePropertyChanged("PanelImport");
            }
        }
        private bool _iskontrahmenu;
        public bool IsKontrahMenu
        {
            get
            {
                return _iskontrahmenu;
            }
            set
            {
                _iskontrahmenu = value;
                RisePropertyChanged("IsKontrahMenu");
            }
        }
        private bool _istrasamenu;
        public bool IsTrasaMenu
        {
            get
            {
                return _istrasamenu;
            }
            set
            {
                _istrasamenu = value;
                RisePropertyChanged("IsTrasaMenu");
            }
        }
        private bool _iskartotekimenu;
        public bool IsKartotekiMenu
        {
            get
            {
                return _iskartotekimenu;
            }
            set
            {
                _iskartotekimenu = value;
                RisePropertyChanged("IsKartotekiMenu");
            }
        }
        private bool _isPanelKontrah;
        public bool PanelKontrah
        {
            get
            {
                return _isPanelKontrah;
            }
            set
            {
                _isPanelKontrah = value;
                RisePropertyChanged("PanelKontrah");
            }
        }
        private bool _isPanelKartoteki;
        private bool _isPanelDefCeny;
        public bool PanelDefCeny
        {
            get
            {
                return _isPanelDefCeny;
            }
            set
            {
                _isPanelDefCeny = value;
                RisePropertyChanged("PanelDefCeny");

            }
        }
        private bool _isPanelCennik;
        public bool PanelCennik
        {
            get
            {
                return _isPanelCennik;
            }
            set
            {
                _isPanelCennik = value;
                RisePropertyChanged("PanelCennik");
            }
        }
        public bool PanelKartoteki
        {
            get
            {
                return _isPanelKartoteki;
            }
            set
            {
                _isPanelKartoteki = value;
                RisePropertyChanged("PanelKartoteki");
            }
        }
        private bool _isPanelKamera1;
        public bool PanelKamera1
        {
            get
            {
                return _isPanelKamera1;
            }
            set
            {
                _isPanelKamera1 = value;
                RisePropertyChanged("PanelKamera1");
            }
        }
        private bool _isPanelKamera2;
        public bool PanelKamera2
        {
            get
            {
                return _isPanelKamera2;
            }
            set
            {
                _isPanelKamera2 = value;
                RisePropertyChanged("PanelKamera2");
            }
        }
        private bool _isPanelKamera3;
        public bool PanelKamera3
        {
            get
            {
                return _isPanelKamera3;
            }
            set
            {
                _isPanelKamera3 = value;
                RisePropertyChanged("PanelKamera3");
            }
        }
        private bool _panelwaga;
        public bool PanelWaga
        {
            get
            {
                return _panelwaga;
            }
            set
            {
                _panelwaga = value;
                RisePropertyChanged("PanelWaga");
            }
        }
        private bool _panelDokumenty;
        public bool PanelDokumenty
        {
            get
            {
                return _panelDokumenty;
            }
            set
            {
                _panelDokumenty = value;
                RisePropertyChanged("PanelDokumenty");
            }
        }
        private bool _panelDokumentySettings;
        public bool PanelDokumentySettings
        {
            get
            {
                return _panelDokumentySettings;
            }
            set
            {
                _panelDokumentySettings = value;
                RisePropertyChanged("PanelDokumentySettings");
            }
        }
        private bool _panelUsluga;
        public bool PanelUsluga
        {
            get
            {
                return _panelUsluga;
            }
            set
            {
                _panelUsluga = value;
                RisePropertyChanged("PanelUsluga");
            }
        }
        private bool _panelPojazdy;
        public bool PanelPojazdy
        {
            get
            {
                return _panelPojazdy;
            }
            set
            {
                _panelPojazdy = value;
                RisePropertyChanged("PanelPojazdy");
            }
        }
        private bool _panelprogramsetings;
        public bool PanelProgramSettings
        {
            get
            {
                return _panelprogramsetings;
            }
            set
            {
                _panelprogramsetings = value;
                RisePropertyChanged("PanelProgramSettings");
            }
        }
        private bool _panelerpconnector;
        public bool PanelErpConnector
        {
            get
            {
                return _panelerpconnector;
            }
            set
            {
                _panelerpconnector = value;
                RisePropertyChanged("PanelErpConnector");
            }
        }
        private bool _panelbaza;
        public bool PanelBaza
        {
            get
            {
                return _panelbaza;
            }
            set
            {
                _panelbaza = value;
                RaisePropertiesChanged("PanelBaza");
            }
        }
        public bool IsZamowienieLista { get; set; }
        private List<IHP_PARAMETRY> _parametry;
        public List<IHP_PARAMETRY> Parametry
        {
            get
            {
              return   _parametry;
            }
            set
            {
                _parametry = value;
                RisePropertyChanged("Parametry");
            }

        }
        private bool _isPanelGrupaKart;
        private bool _isPanelHarmonogramDzienny;


        private bool _panelsprzedaz;
        public bool PanelSprzedaz
        {
            get
            {
                return _panelsprzedaz;
            }
            set
            {
                _panelsprzedaz = value;
                RisePropertyChanged("PanelSprzedaz");
            }
        }

        #region Przyciski
        public ICommand ICommandNavi { get; private set; }
        public ICommand ICommandNoweWazenie { get; private set; }
        public ICommand ICommandKamera1 { get; private set; }
        public ICommand ICommandKamera2 { get; private set; }
        public ICommand ICommandKamera3 { get; private set; }
        public ICommand ICommandDokumenty { get; private set; }
        public ICommand ICommandUsluga { get; private set; }
        public ICommand ICommandPojazdy { get; private set; }
        public ICommand ICommandKartoteki { get; private set; }
        public ICommand ICommandGrupaKart { get; private set; }
        public ICommand ICommandImport { get; private set; }
        public ICommand ICommandKontrah { get; private set; }
        public ICommand ICommandTrasa { get; private set; }
        public ICommand ICommandKierowcy { get; private set; }
        public ICommand ICommandDokumentySetting { get; set; }
        public ICommand ICommandUstawienia { get; set; }
        public ICommand ICommandConnector { get; set; }
        public ICommand WindowClosing { get; set; }
        public ICommand ICommandLogout { get; set; }
        public ICommand ICommandCennik { get; set; }
        public ICommand ICommandDefCeny { get; set; }
        public ICommand ICommandZamowienieLista { get; set; }
        public ICommand ICommandZamowieniePanel { get; set; }
        public ICommand ICommandZamowienieSprzedaz { get; set; }

        public ICommand ICommandJednostkiDodat { get; set; }
        public ICommand ICommandJednostkiMiary { get; set; }

      


        #endregion

        #region Ustawiwianie WIdoków

        public void UstawZamowienieLista()
        {
            CloseAllViews();
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelZamowienie)))
                {
                    ViewModelZamowienie = new ViewModelZamowienie() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelZamowienie);
                }
                ViewModelZamowienie.IsClosed =false;
        }

        public void ustawK1()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKamera1)))
            {
                ViewModelKamera1 = new ViewModelKamera1() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelKamera1);
                _isPanelKamera1 = true;
            }
          if (_isPanelKamera1)
            {
                PanelKamera1 = false;
                ViewModelKamera1.IsClosed = false;
            }
            else if (!_isPanelKamera1)
            {
                PanelKamera1 = true;
                ViewModelKamera1.IsClosed = true;
            }
        }
        public void ustawK2()
        {
          if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKamera2)))
            {
                ViewModelKamera2 = new ViewModelKamera2() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelKamera2);
                _isPanelKamera2 = true;
            }
                if (_isPanelKamera2)
                {
                     PanelKamera2 = false;
                    ViewModelKamera2.IsClosed = false;
                }
                else if (!_isPanelKamera2)
                {
                 PanelKamera2 = true;
                 ViewModelKamera2.IsClosed = true;
            }
        }
        public void ustawK3()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKamera3)))
            {
                ViewModelKamera3 = new ViewModelKamera3() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelKamera3);
                _isPanelKamera3 = true;
            }
              if (_isPanelKamera3)
                {
                PanelKamera3 = false;
                ViewModelKamera3.IsClosed = false;
                }
                else if (!_isPanelKamera3)
                {
                    PanelKamera3 = true;
                    ViewModelKamera3.IsClosed = true;
            }
        }
        public void ustawPanelKierowcy()
        {
            if (_user.KIEROWCY == 1)
            {
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKierowcy)))
                {
                    ViewModelKierowcy = new ViewModelKierowcy() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelKierowcy);
                    _isPanelKierowcy = true;
                }
                if (_isPanelKierowcy)
                {
                    PanelKierowcy = false;
                    ViewModelKierowcy.IsClosed = false;
                }
                else if (!_isPanelKierowcy)
                {
                    PanelKierowcy = true;
                    ViewModelKierowcy.IsClosed = true;
                }
            }
         
        }
        public void ustawDokumenty()
        {
              //  if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelDokumenty)))
              //  {
              //      ViewModelDokumenty = new ViewModelDokumenty() { TargetName = "LayoutGroupLeft", IsClosed = false };
              //      ChildViews.Add(ViewModelDokumenty);
              //      _panelDokumenty = true;
              //  }
              //if (_panelDokumenty)
              //  {
              //      PanelDokumenty = false;
              //      ViewModelDokumenty.IsClosed = false;
              //  }
              //  else if (!_panelDokumenty)
              //  {
              //      PanelDokumenty = true;
              //      ViewModelDokumenty.IsClosed = true;
              //  }
     }
        public void ustawDokumentySettings()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelDokumnetySettings)))
            {
                ViewModelDokumnetySettings = new ViewModelDokumnetySettings() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelDokumnetySettings);
            }

              if (_panelDokumentySettings)
                {
         
                PanelDokumentySettings = false;
                ViewModelDokumnetySettings.IsClosed = false;
                }
                else if (!_panelDokumentySettings)
                {
                      PanelDokumentySettings = true;
                    ViewModelDokumnetySettings.IsClosed = true;
                }
        }
        public void ustawUsluga()
        {
            if (_user.USLUGA == 1)
            {
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelUsluga)))
                {
                    ViewModelUsluga = new ViewModelUsluga() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelUsluga);
                    _panelUsluga = true;
                }
                if (_panelUsluga)
                {
                    PanelUsluga = false;
                    ViewModelUsluga.IsClosed = false;
                }
                else if (!_panelUsluga)
                {
                    PanelUsluga = true;
                    ViewModelUsluga.IsClosed = true;
                }
            }
        }

        public void ustawPojzady()
        {
            if (_user.POJAZDY == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelPojazdy)))
                {
                    ViewModelPojazdy = new ViewModelPojazdy() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelPojazdy);
                }
                ViewModelPojazdy.IsClosed = true;
               
            } 
        }

        public void  UstawCennik ()
        {
            if (_user.KARTOTEKI == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelCennik)))
                {
                    ViewModelCennik = new ViewModelCennik() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelCennik);
                    _isPanelCennik = true;
                }
                ViewModelCennik.IsClosed = false;
            }
        }

        public void UstawDefCeny()
        {
            if (_user.KARTOTEKI == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelDefCeny)))
                {
                    ViewModelDefCeny = new ViewModelDefCeny() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelDefCeny);
                    _isPanelDefCeny = true;
                }

                ViewModelDefCeny.IsClosed = false;
            }
        }

        public void UstawKartoteki()
        {
            if (_user.KARTOTEKI == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKartoteki)))
                {
                    ViewModelKartoteki = new ViewModelKartoteki() { TargetName = "LayoutGroupLeft", IsClosed = false };
                     ChildViews.Add(ViewModelKartoteki);
                    _isPanelKartoteki = true;
                }
              ViewModelKartoteki.IsClosed = false;
            }
        }

        public void UstawKontrah()
        {
            if (_user.KONTRAHENT == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKontrahent)))
                {
                    ViewModelKontrahent = new ViewModelKontrahent() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelKontrahent);
                }
           //     ViewModelKontrahent.IsActive = true;
                ViewModelKontrahent.IsClosed = false;
            }
        }
        public void UstawTrasa()
        {
            if (_user.KONTRAHENT == 1)
            {
                CloseAllViews();
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelTrasy)))
                {
                    ViewModelTrasy = new ViewModelTrasy() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelTrasy);
               }
                ViewModelTrasy.IsClosed = false;
            }
        }
        public void UstawImport()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelImport)))
            {
                ViewModelImport = new ViewModelImport() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelImport);
            }
            if (!_panelnavi)
            {
                PanelNaviCaption = "Panel Nawigacyjny - Kontrahenci";
                Navigate("Kontrahent");
            }
            else
            {
                if (_isPanelImport)
                {
                    PanelImport = false;
                }
                else if (!_isPanelImport)
                {
                    PanelImport = true;
                }
            }
        }
        public void UstawProgram()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelProgramSettings)))
            {
                ViewModelProgramSettings = new ViewModelProgramSettings() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelProgramSettings);
             }
          if (!_panelnavi)
            {
                PanelNaviCaption = "Panel Navigacyjny - Ustawienie Programu";
                Navigate("ProgramSettings");
            }
            else
            {
                if (_panelprogramsetings)
                {
                //    Messenger.Default.Send<IHP_ZAM_USERS>(_user);
                    ViewModelProgramSettings.IsClosed = false;
                    PanelProgramSettings = false;
                }
                else if (!_panelprogramsetings)
                {
                  //  Messenger.Default.Send<IHP_ZAM_USERS>(_user);
                    ViewModelProgramSettings.IsClosed = true;
                    PanelProgramSettings = true;
                }
            }

        }
        public void CloseAllViews()
        {
           foreach (var obj in ChildViews)
            {
                var property = obj.GetType().GetProperty("IsClosed");
                    property.SetValue(obj, true, null);
            }
        }
        public void UstawErpConnector()
        {
           if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelERPConnector)))
            {
                ViewModelERPConnector = new ViewModelERPConnector() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelERPConnector);
            }


            if (!_panelnavi)
            {
                PanelNaviCaption = "Panel Nawigacyjny - ErpConnector";
                Navigate("SystemErp");
            }
            else
            {
                if (_panelerpconnector)
                {
                    PanelErpConnector = false;
                }
                else if (!_panelerpconnector)
                {
                    PanelErpConnector = true;
                }
            }
        }
        private bool CanPanelWaga()
        {
            return true;
        }
        private void UstawPanelWaga()
        {

            ////IsOpenRegWaga = IsOpenRegWaga ? false : true;
            //if (!_panelnavi)
            //{
            //    PanelNaviCaption = "Panel Nawigacyjny - Rejestracja Ważeń";
            //    Navigate("RejWaga");
            //    return;
            //}
            //if (_user.REJWAGA == 1)
            //{
            //    if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelRegWaga)))
            //    {
            //        ViewModelRegWaga = new ViewModelRegWaga() { TargetName = "LayoutGroupLeft", IsClosed = false };
            //        ChildViews.Add(ViewModelRegWaga);
            //    }
            //    else
            //    {
            //        if (_panelwaga)
            //        {
            //            PanelWaga = false;
            //            ViewModelRegWaga.IsClosed = false;

            //        }
            //        else if (!_panelwaga)
            //        {
            //            PanelWaga = true;
            //            ViewModelRegWaga.IsClosed = true;

            //        }
            //    }
            //}
        }
        private void UstawPanelNavi()
        {
            if (_panelnavi)
            {
                PanelNavi = false;
            }
            else if (!_panelnavi)
            {
                PanelNavi = true;
            }
        }
        private void UstawRejZam()
        {

            ////IsOpenRegWaga = IsOpenRegWaga ? false : true;
            //if (!_panelnavi)
            //{
            //    PanelNaviCaption = "Panel Nawigacyjny - Rejestracja Ważeń";
            //    Navigate("RejWaga");
            //    return;
            //}
            //if (_user.REJZAM == 1)
            //{
            //    if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelRejZam)))
            //    {
            //        ViewModelRejZam = new ViewModelRejZam() { TargetName = "LayoutGroupLeft", IsClosed = false };
            //        ChildViews.Add(ViewModelRejZam);
            //    }
            //    else
            //    {
            //        if (_panelwaga)
            //        {
            //            PanelWaga = false;
            //            ViewModelRejZam.IsClosed = false;

            //        }
            //        else if (!_panelwaga)
            //        {
            //            PanelWaga = true;
            //            ViewModelRejZam.IsClosed = true;

            //        }
            //    }
            //}
        }
        private void UstawZamPanelSprzedaz()
        {
            CloseAllViews();
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelOferta)))
                {
                    ViewModelOferta = new ViewModelOferta() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelOferta);
              
            }
            ViewModelOferta.IsClosed = false;
        }
        

        private void UstawZamPanelDotyk()
        {

            //IsOpenRegWaga = IsOpenRegWaga ? false : true;
            if (!_panelnavi)
            {
                PanelNaviCaption = "Panel Nawigacyjny - Rejestracja Ważeń";
                Navigate("RejWaga");
                return;
            }
            if (_user.REJZAM == 1)
            {
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelKafelki)))
                {
                    ViewModelKafelki = new ViewModelKafelki() { TargetName = "LayoutGroupLeft", IsClosed = false };
                    ChildViews.Add(ViewModelKafelki);
                }
                else
                {
                    if (_panelwaga)
                    {
                        PanelWaga = false;
                        ViewModelKafelki.IsClosed = false;

                    }
                    else if (!_panelwaga)
                    {
                        PanelWaga = true;
                        ViewModelKafelki.IsClosed = true;

                    }
                }
            }
        }
        private void UstawPanelBazaDanych()
        {
           
                if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelBazyDanych)))
            {
                ViewModelBazyDanych = new ViewModelBazyDanych() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelBazyDanych);
                _panelbaza = true;
            }
            if (_panelbaza)
            {
                PanelBaza = false;
                ViewModelBazyDanych.IsClosed = false;
            }
            else if (!_panelbaza)
            {
                PanelBaza = true;
                ViewModelBazyDanych.IsClosed = true;
            }

        }

        public void UstawHarmonogram()
        {
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelHarmonogramDzienny)))
            {
                ViewModelHarmonogramDzienny = new ViewModelHarmonogramDzienny() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelHarmonogramDzienny);
                _isPanelHarmonogramDzienny = true;
            }
            if (_isPanelHarmonogramDzienny)
            {
                _isPanelHarmonogramDzienny = true;
                ViewModelHarmonogramDzienny.IsClosed = false;
            }
            else if (!_isPanelHarmonogramDzienny)
            {
                _isPanelHarmonogramDzienny = false;
                ViewModelHarmonogramDzienny.IsClosed = true;
            }
        }
        public void UstawGrupaKart()
        {
            CloseAllViews();
            if (!ChildViews.Any(p => p.GetType() == typeof(ViewModelGrupaKart)))
            {
                ViewModelGrupaKart = new ViewModelGrupaKart() { TargetName = "LayoutGroupLeft", IsClosed = false };
                ChildViews.Add(ViewModelGrupaKart);
                _isPanelGrupaKart = true;
            }
            ViewModelGrupaKart.IsClosed = false;
        }
        #endregion
 
        private bool _isvisiblekamera;
        public bool IsVisibleKamera
        {
            get
            {
                return _isvisiblekamera;
            }
            set
            {
                _isvisiblekamera = value;
                RisePropertyChanged("IsVisibleKamera"); ;
            }
        }
        private bool _isvisiblesynchro;                 
        public bool IsVisibleSynchro
        {
            get
            {
                return _isvisiblesynchro;
            }
            set
            {
                _isvisiblesynchro = value;
                RisePropertyChanged("IsVisibleSynchro");
            }
        }
        private bool _isvisiblesynchrolic;
        public bool IsVisibleSynchroLic
        {
            get
            {
                return _isvisiblesynchrolic;
            }
            set
            {
                _isvisiblesynchrolic = value;
                RisePropertyChanged("IsVisibleSynchroLic");
            }
        }
        private bool _isvisiblewazenie;
        public bool IsVisibleWazenie
        {
            get
            {
                return _isvisiblewazenie;
            }
            set
            {
                _isvisiblewazenie = value;
                RisePropertyChanged("IsVisibleWazenie");
            }
        }

        private bool _isvisiblerejzam;
        public bool IsVisibleRejZam
        {
            get
            {
                return _isvisiblerejzam;
            }
            set
            {
                _isvisiblerejzam = value;
                RisePropertyChanged("IsVisibleRejZam");
            }
        }

        private bool _isvisiblewazenieusluga;
        public bool IsVisibleWazenieUsluga
        {
            get
            {
                return _isvisiblewazenieusluga;
            }
            set
            {
                _isvisiblewazenieusluga = value;
                RisePropertyChanged("IsVisibleWazenieUsluga");
            }
        }
        private bool _isvisibledokumenty;
        public bool IsVisibleDokumenty
        {
            get
            {
                return _isvisibledokumenty;
            }
            set
            {
                _isvisibledokumenty = value;
                RisePropertyChanged("IsVisibleDokumenty");
            }
        }
        private bool _isvisibleIntegracja;
        public bool IsVisibleIntegracja
        {
            get
            {
                return _isvisibleIntegracja;
            }
            set
            {
                _isvisibleIntegracja = value;
                RisePropertyChanged("IsVisibleIntegracja");
            }
        }
        private bool _isvisibleKameralic;
        public bool IsVisibleKameraLic
        {
            get
            {
                return _isvisibleKameralic;
            }
            set
            {
                _isvisibleKameralic = value;
                RisePropertyChanged("IsVisibleKameraLic");
            }
        }
        private bool _isvisiblecennik;
        public bool IsVisibleCennik
        {
            get
            {
                return _isvisiblecennik;
            }
            set
            {
                _isvisiblecennik = value;
                RisePropertyChanged("IsVisibleCennik");

            }
        }
        private bool logoout = false;
        IHP_ZAM_USERS _user;
        private ObservableCollection<object> _childview;
        public ObservableCollection<object> ChildViews {
           get
            {
                return _childview;
            }
           set
            {
                _childview = value;
                RisePropertyChanged("ChildViews");
            }
          }

        ViewModelBazyDanych ViewModelBazyDanych = null;
        ViewModelUsluga ViewModelUsluga = null;    ViewModelKamera1 ViewModelKamera1 = null;     ViewModelKamera2 ViewModelKamera2 = null;
        ViewModelKamera3 ViewModelKamera3 = null;  ViewModelProgramSettings ViewModelProgramSettings = null; ViewModelPojazdy ViewModelPojazdy = null;
        ViewModelKontrahent ViewModelKontrahent = null;   ViewModelKierowcy ViewModelKierowcy = null;        ViewModelKartoteki ViewModelKartoteki = null;
        ViewModelImport ViewModelImport = null;           ViewModelERPConnector ViewModelERPConnector = null;ViewModelDokumnetySettings ViewModelDokumnetySettings = null;
            ViewModelDaneFirmy ViewModelDaneFirmy = null; ViewModelCennik ViewModelCennik = null; ViewModelDefCeny ViewModelDefCeny = null;
          ViewModelZamowienie ViewModelZamowienie = null; ViewModelGrupaKart ViewModelGrupaKart = null; ViewModelKafelki ViewModelKafelki = null;
        ViewModelTrasy ViewModelTrasy = null; ViewModelOferta ViewModelOferta = null; ViewModelHarmonogramDzienny ViewModelHarmonogramDzienny = null;
         LicConfig lic;
        LicencjaClass licust;
        public IHP_ZAM_USERS Uzytkownik { get; set; }
        string MachineName = Environment.MachineName;

        public ViewModelMain()
        {
          _ramka = new WagaRamka();

            
            _panelnavi = true;
            Messenger.Default.Register<IHP_ZAM_USERS>(this, OnMessageUser);
            InitCommands();
            InitLists();
            UstawUprawnieniaLic();
                   UstawPrawaMenu();
         }


        void   InitCommands()
        {
            ICommandNavi = new RelayCommand(UstawPanelNavi); ICommandNoweWazenie = new RelayCommand(UstawPanelWaga); ICommandKamera1 = new RelayCommand(ustawK1);
            ICommandKamera2 = new RelayCommand(ustawK2); ICommandKamera3 = new RelayCommand(ustawK3); ICommandDokumenty = new RelayCommand(ustawDokumenty);
            ICommandUsluga = new RelayCommand(ustawUsluga); ICommandPojazdy = new RelayCommand(ustawPojzady);
            ICommandKartoteki = new RelayCommand(UstawKartoteki); ICommandGrupaKart = new DelegateCommand(UstawGrupaKart);
            ICommandKontrah = new RelayCommand(UstawKontrah); ICommandTrasa = new RelayCommand(UstawTrasa);
            ICommandKierowcy = new RelayCommand(ustawPanelKierowcy); ICommandDokumentySetting = new RelayCommand(ustawDokumentySettings);
            ICommandUstawienia = new RelayCommand(UstawProgram); ICommandConnector = new RelayCommand(UstawErpConnector);
            ICommandCennik = new RelayCommand(UstawCennik);
            WindowClosing = new DelegateCommand<object>(this.OnWindowClosing);
            ICommandImport = new RelayCommand(UstawImport);
            ICommandLogout = new DelegateCommand(LogOut);
            ICommandDefCeny = new DelegateCommand(UstawDefCeny);
            ICommandZamowienieLista = new DelegateCommand(UstawZamowienieLista);
            ICommandZamowieniePanel = new DelegateCommand(UstawZamPanelDotyk);
            ICommandZamowienieSprzedaz = new DelegateCommand(UstawZamPanelSprzedaz);
             ICommandJednostkiDodat = new DelegateCommand(PokazSlownikJz);
            ICommandJednostkiMiary = new DelegateCommand(PokazSlownikJm);
        }

        private void PokazSlownikJm()
        {
            var w = new JaednMiary();
            w.ShowDialog();
            
        }

        private void PokazSlownikJz()
        {
            var w = new JaednDodatPodpowiedz();
            w.ShowDialog();
          
        }

        void InitLists()
        {
            ChildViews = new ObservableCollection<object>() { }; ;
        }
        private void LogOut()
        {
            MainLoginService.ShowLoginWindow();
            logoout = true;
            CurrentWindowService.Close();
        }
        private void OnWindowClosing(object obj)
        {
            if ((app.UstawieniaAplikacji.BazaDanych.KopiaPrzyZamknieciu == 1) && (!logoout))
            {
               DXSplashScreen.Show<SplashScreenView>();
                // https://www.youtube.com/watch?v=npMnqcz63Cg - sampple
                try
                {
                    SqlConnection myConnection = new SqlConnection();
                    SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder();
                    //    myBuilder.UserID = "sa";
                    //   myBuilder.Password = "admin@123";
                    myBuilder.InitialCatalog = "INFOHELP";
                    myBuilder.DataSource = MachineName + @"\INFOHELP";
                    myBuilder.ConnectTimeout = 30;
                    myBuilder.IntegratedSecurity = true;
                    myConnection.ConnectionString = myBuilder.ConnectionString;
                     Server dbServer = new Server(new ServerConnection(myConnection));
                    Backup dbBackup = new Backup() { Action = BackupActionType.Database, Database = "INFOHELP" };
                    dbBackup.Devices.AddDevice(AppDomain.CurrentDomain.BaseDirectory + @"\Backup\" + DateTime.Now.ToShortDateString().Replace('-', '_') + "_infohelp.bak", DeviceType.File);
                    dbBackup.Initialize = true;
                    dbBackup.PercentComplete += DbBackup_PercentComplete;
                    dbBackup.Complete += DbBackup_Complete;
                    dbBackup.SqlBackupAsync(dbServer);
                    
                  DXSplashScreen.Close();

                }
                catch (Exception ex)                {
                    DXSplashScreen.Close();
                    MessageBox.Show("Blad podczas wykonywania kopi zapasowej :" + ex.InnerException);
                    for (int i = 0; i < 100; i++)
                    {
                        DXSplashScreen.Progress(i);
                        DXSplashScreen.SetState(string.Format("{0} %", (i + 1)));
                        System.Threading.Thread.Sleep(40);
                    }
                }
            }
        }
        private void DbBackup_Complete(object sender, Microsoft.SqlServer.Management.Common.ServerMessageEventArgs e)
        {
              DXSplashScreen.Close();
        }
        private void DbBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            DXSplashScreen.Progress(e.Percent);
            DXSplashScreen.SetState(string.Format("{0} %", (e.Percent + 1)));
        }
        private void UstawUprawnieniaLic()
       {
          try
              {
                        lic = LicConfig.GetInstance;
                        licust = lic.UstawieniaLicencji;

          if (!lic.LicOk)
                       {
                        IsVisibleRejZam = 
                        IsVisibleWazenie =
                        IsVisibleWazenieUsluga =
                        IsVisibleKamera =
                        IsVisibleKameraLic=
                        IsVisibleDokumenty =
                        IsVisibleSynchro =
                        IsVisibleSynchroLic=
                        IsVisibleCennik = 
                        IsVisibleIntegracja = false;
                        MessageBox.Show("Niepoprawny plik Licencji");
                        }
                       else
                      {
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 1).Wartosci == "1") // wazenie 
                                IsVisibleWazenie = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 2).Wartosci == "1")  //wazenie uslugowe
                                IsVisibleWazenieUsluga = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 3).Wartosci == "1")// obsluga dokumentow
                                IsVisibleDokumenty = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 4).Wartosci == "1")     //synchronizacja - wersja serwerowa ... 
                                IsVisibleSynchroLic = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 5).Wartosci == "1")     //Integracja ERP
                                IsVisibleIntegracja = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 6).Wartosci == "1")     //Kamery
                                IsVisibleKameraLic = true;
                            if (licust.Moduly.FirstOrDefault(x => x.IdIhpModul == 7).Wartosci == "1")     //RejZam
                                        {
                                            IsZamowienieLista = true;
                                            IsVisibleRejZam = true;
                                        }
      
                    IHP_PARAMETRY param = GetParam(2);
                            if ((param.WARTOSC == "0") & (IsVisibleKameraLic))
                               IsVisibleKamera = true;
                                  param = GetParam(3);
                            if ((param.WARTOSC == "0") & (IsVisibleSynchroLic))
                                IsVisibleSynchro = true;
                  }
              
            }
                        catch (Exception ex)
                        {
                            MessageBoxService.ShowMessage("Problem z zaczytaniem pliku licencji - zgłoś do serwisu");
                            return;
                        }
        }
        public void UstawPrawaMenu()
        {
                      IsTrasaMenu = true;
         //  if (_user.KONTRAHENT == 1)
                      IsKontrahMenu = true;
         //   if (_user.KARTOTEKI == 1)
                    IsKartotekiMenu = true;
        }
        public void OnMessageUser(IHP_ZAM_USERS user)
        {
          _user = user;
        //    if (lic.LicOk)
               // UstawGrupaKart();
            //UstawRejZam();
            //UstawPanelWaga();
          //  UstawPrawaMenu();
        }

        protected override void OnViewLoaded()
        {
            base.OnViewLoaded();
            Navigate("RejZam");
        }
 
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    
        //bool  TestDatabaseConn()
        //{
        //    AppSettingsBazaDanych BazaDanych = app.UstawieniaAplikacji.BazaDanych;

        //    string provider = "System.Data.SqlClient";
        //    bool integratedSecurity = false;
        //    if (IntegratedSecuritySql)
        //    {
        //        integratedSecurity = false;
        //    }
        //    if (IntegratedSecurityWin)
        //    {
        //        integratedSecurity = true;
        //    }
        //    if (TestConnection2(provider, _databasename, _databasecatalog, _databaseuser, _databasepass, integratedSecurity))
        //    {
        //        MessageBoxService.ShowMessage("Połaczenie Ustawione");
        //        ZapiszConfig();
        //        Zapisz();
        //    }
        //    else
        //    {
        //        MessageBoxService.ShowMessage("Połaczenie nie powiodło się");
        //        connectionString = String.Empty;
        //    }

        //}
 
        public bool TestConnection(string provider, string serverName, string initialCatalog, string userId, string password, bool integratedSecurity)
        {
            var connectionString = integratedSecurity ? string.Format("data source={0};initial catalog={1};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework", serverName, initialCatalog, provider)
                                                      : string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};", provider, serverName, initialCatalog, userId, password);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    UstawPanelBazaDanych();
                    return false;

                }
            }
        }
    }
   public interface IDockLayoutService
        {
            void Save(string path);
            void Restore(string path);
        }
   public class DockLayoutService : ServiceBase, IDockLayoutService
        {
            public void Save(string path)
            {
                ((DockLayoutManager)AssociatedObject).SaveLayoutToXml(path);
            }

            public void Restore(string path)
            {
                if (File.Exists(path))
                    ((DockLayoutManager)AssociatedObject).RestoreLayoutFromXml(path);
            }
        }
   class TemplateSelector : DataTemplateSelector
        {
            public DataTemplate MainDocumentTemplate { get; set; }
            public DataTemplate RejWagaTemplate { get; set; }
            public DataTemplate UslugaTemplate { get; set; }
            public DataTemplate BazaDanychTemplate { get; set; }
            public DataTemplate Kamera1Template { get; set; }
            public DataTemplate Kamera2Template { get; set; }
            public DataTemplate Kamera3Template { get; set; }
            public DataTemplate KontrahentTemplate { get; set; }
            public DataTemplate DokumentyTemplate { get; set; }
            public DataTemplate PojazdyTemplate { get; set; }
            public DataTemplate KartotekiTemplate { get; set; }
            public DataTemplate DokumentySettingsTemplate { get; set; }
            public DataTemplate KierowcyTemplate { get; set; }
            public DataTemplate ProgramSettingsTemplate { get; set; }
            public DataTemplate SystemErpTemplate { get; set; }
            public DataTemplate View1Template { get; set; }
            public DataTemplate ImportTemplate { get; set; }
            public DataTemplate CennikTemplate { get; set; }
           public DataTemplate DefCenyTemplate { get; set; }
           public DataTemplate RejZamTemplate { get; set; }
           public DataTemplate ZamowienieListaTemplate { get; set; }
           public DataTemplate GrupaKartTemplate { get; set; }
        public DataTemplate ZamowieniaDotykTemplate { get; set; }
        public DataTemplate TrasyTemplate { get; set; }
        public DataTemplate OfertaTemplate { get; set; }
        public DataTemplate HarmonogramDziennyTemplate { get; set; }
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
            {
                if (item is View1ViewModel)
                    return View1Template;
           
                if (item is ViewModelBazyDanych)
                    return BazaDanychTemplate;
                if (item is ViewModelUsluga)
                    return UslugaTemplate;
                if (item is ViewModelKamera1)
                    return Kamera1Template;
                if (item is ViewModelKamera2)
                    return Kamera2Template;
                if (item is ViewModelKamera3)
                    return Kamera3Template;
                if (item is ViewModelKontrahent)
                    return KontrahentTemplate;
 
                if (item is ViewModelKierowcy)
                    return KierowcyTemplate;
                if (item is ViewModelKartoteki)
                        return KartotekiTemplate;
                if (item is ViewModelDokumnetySettings)
                       return DokumentySettingsTemplate;
                if (item is ViewModelProgramSettings)
                    return ProgramSettingsTemplate;
                if (item is ViewModelPojazdy)
                      return PojazdyTemplate;
                if (item is ViewModelImport)
                         return ImportTemplate;
                if (item is ViewModelCennik)
                      return CennikTemplate;
                if (item is ViewModelDefCeny)
                return DefCenyTemplate;
         
               if (item is ViewModelZamowienie)
                return ZamowienieListaTemplate;
               if (item is ViewModelGrupaKart)
                    return GrupaKartTemplate;
            if (item is ViewModelKafelki)
                return ZamowieniaDotykTemplate;
            if (item is ViewModelTrasy)
                     return TrasyTemplate;
            if (item is ViewModelHarmonogramDzienny)
                return HarmonogramDziennyTemplate;
            if (item is ViewModelOferta)
                return OfertaTemplate;
            
            return null;

            
            }
        }
}  



