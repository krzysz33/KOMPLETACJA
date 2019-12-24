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
    public partial class Kamera1 : UserControl
    {
        private bool CanChangeGrid = false;
          string KarotekiGridFlie =   "KarottekiGrid.xml";
        public static readonly DependencyProperty IsLayoutSavedKart = DependencyProperty.Register("IsLayoutSavedKamera1", typeof(bool), typeof(Kamera1), null);
        private string user, password, urilive;
        private AppConfig app;
        private IpCamController _controller;
        public Kamera1()
        {
            InitializeComponent();
            app = AppConfig.GetInstance;
            user = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.User;
            password = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Pass;
            urilive = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URILive;
            _urlTextBox.Text =   urilive.Replace("{user}", user).Replace("{password}", password);
        }

        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //_controller.StopProcessing();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //_controller = new IpCamController("http://192.168.0.20:80", "admin", "1qaz@WSX");
            //_controller.ImageReady += dec_FrameReady;
            //_controller.StartProcessing();

        }

        void dec_FrameReady(object sender, ImageReadyEventArsgs args)
        {
     
        }


        private void btnRight_Click_1(object sender, RoutedEventArgs e){}

        private void btnLeft_Click_1(object sender, RoutedEventArgs e)
        {
            _controller.PanLeft();
        }

        private void btnDown_Click_1(object sender, RoutedEventArgs e)
        {
            _controller.PanDown();
        }

        private void btnUp_Click_1(object sender, RoutedEventArgs e)
        {
            _controller.PanUp();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //_controller = new IpCamController("http://192.168.0.20:80", "admin", "1qaz@WSX");
            //_controller.ImageReady += dec_FrameReady;
            //_controller.StartProcessing();
        }

        private void HandlePlayerEvent(object sender, WebEye.Controls.Wpf.StreamPlayerControl.StreamFailedEventArgs e)
        {

        }
        private void HandlePlayerEvent(object sender, RoutedEventArgs e)
        {
        

            if (e.RoutedEvent.Name == "StreamStarted")
            {
                _statusLabel.Text = "Playing";
            }
            else if (e.RoutedEvent.Name == "StreamFailed")
            {
                _statusLabel.Text = "Failed";

                MessageBox.Show(
                    ((WebEye.Controls.Wpf.StreamPlayerControl.StreamFailedEventArgs)e).Error,
                    "Stream Player Demo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            else if (e.RoutedEvent.Name == "StreamStopped")
            {
                _statusLabel.Text = "Stopped";
            }
        }

        private void HandlePlayButtonClick(object sender, RoutedEventArgs e)
        {
 
            string uriloclal = urilive.Replace("{user}", user).Replace("{password}", password);
           // var uri = new Uri("rtsp://admin:krzysz33123@192.168.0.20");
            var uri = new Uri(uriloclal);
            _streamPlayerControl.StartPlay(uri);
               _statusLabel.Text = "Connecting...";
        }

        private void HandleStopButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void HandleImageButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
