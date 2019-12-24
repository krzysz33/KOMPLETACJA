using DevExpress.Xpf.Core.Serialization;
using System;
using System.Collections.Generic;
using DevExpress.Mvvm.UI.Interactivity;
using System.IO;
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
using DevExpress.Xpf.Core.Native;
using System.Windows.Threading;

namespace KpInfohelp
{
    /// <summary>
    /// Interaction logic for Kontrahent.xaml
    /// </summary>
    public partial class Oferta : UserControl
    {
 
        public ICommand FocusCommand { get; private set; }

        private bool CanChangeGrid = false;
        string ZamowieniaListaGridFlie = ProgramDataSotrage.ProfilePath + "ZamowieniaListaGridFlie.xml";
        string ZamowieniaListaExpFlie = ProgramDataSotrage.ProfilePath + "ZamowieniaListaExpFlie.xml";
        public static readonly DependencyProperty IsLayoutSavedListaZamowien = DependencyProperty.Register("IsLayoutSavedListaZamowien", typeof(bool), typeof(ZamowieniaLista), null);
 
        public Oferta()
        {
            InitializeComponent();
             RestoreView();
             RestoreViewExp();
        }
        private void RestoreView()
        {
            //if (File.Exists(ZamowieniaListaGridFlie))
            //{
            //    var version = GetLayoutVersion(ZamowieniaListaGridFlie);
            //    if (string.IsNullOrEmpty(version))
            //        DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.UI);
            //    dgvKontrah.RestoreLayoutFromXml(ZamowieniaListaGridFlie);
            //    DXSerializer.SetStoreLayoutMode(dgvKontrah, StoreLayoutMode.All);
            //    CanChangeGrid = true;
            //}
        }

        private void RestoreViewExp()
        {
            if (File.Exists(ZamowieniaListaExpFlie))
            {
                var version = GetLayoutVersion(ZamowieniaListaExpFlie);
                if (string.IsNullOrEmpty(version))
                    DXSerializer.SetStoreLayoutMode(dgvListaLxp, StoreLayoutMode.UI);
                dgvListaLxp.RestoreLayoutFromXml(ZamowieniaListaExpFlie);
                DXSerializer.SetStoreLayoutMode(dgvListaLxp, StoreLayoutMode.All);
                CanChangeGrid = true;
            }
        }
        private void view_Loaded(object sender, RoutedEventArgs e)
        {
            //if (view.SearchControl != null)
            //{
            //    view.SearchControl.Focus();
            //    view.SearchControl.SearchText = string.Empty;
            //}
        }
        private string GetLayoutVersion(string fileName)
        {
            using (var reader = XmlReader.Create(fileName))
            {
                reader.ReadToFollowing("property");
                return reader.ReadElementContentAsString();
            }
        }
        public bool IsLayoutSaved
        {
            get { return (bool)GetValue(IsLayoutSavedListaZamowien); }
            set { SetValue(IsLayoutSavedListaZamowien, value); }
        }
  
         private void dgvKontrah_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                //dgvKontrah.SaveLayoutToXml(ZamowieniaListaGridFlie);
                //IsLayoutSaved = true;
                //MessageBox.Show("Zapisałem ustawienia grida zamowienia");
            }
        }

        private void dgvKontrah_MouseDoubleClick(object sender, MouseButtonEventArgs e){}

        private void drvZamowieniaHist_PreviewKeyDown(object sender, KeyEventArgs e){}
 
        private void dgvListaLxp_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F2)
            {
                dgvListaLxp.SaveLayoutToXml(ZamowieniaListaExpFlie);
                IsLayoutSaved = true;
                MessageBox.Show("Zapisałem ustawienia grida Exp");
            }
        }
    }

    public class FocusBehaviorEx : EventTriggerBase<Control>
    {
#if !SILVERLIGHT
        public readonly static TimeSpan DefaultFocusDelay = TimeSpan.FromMilliseconds(0);
#else
		public readonly static TimeSpan DefaultFocusDelay = TimeSpan.FromMilliseconds(500);
#endif
        public static readonly DependencyProperty FocusDelayProperty =
            DependencyProperty.Register("FocusDelay", typeof(TimeSpan?), typeof(FocusBehaviorEx),
            new PropertyMetadata(null));
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.Register("PropertyName", typeof(string), typeof(FocusBehaviorEx),
            new PropertyMetadata(string.Empty));

        [IgnoreDependencyPropertiesConsistencyChecker]
        static readonly DependencyProperty PropertyValueProperty =
            DependencyProperty.Register("PropertyValue", typeof(object), typeof(FocusBehaviorEx),
            new PropertyMetadata(null, (d, e) => ((FocusBehaviorEx)d).OnPropertyValueChanged(e.NewValue)));
        public TimeSpan? FocusDelay
        {
            get { return (TimeSpan?)GetValue(FocusDelayProperty); }
            set { SetValue(FocusDelayProperty, value); }
        }
        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        public object TriggerValue { get; set; }

        bool lockPropertyValueChanged;
        protected override void OnEvent(object sender, object eventArgs)
        {
            base.OnEvent(sender, eventArgs);

            if (!string.IsNullOrEmpty(PropertyName)) return;
            DoFocus();
        }
        protected override void OnSourceChanged(object oldSource, object newSource)
        {
            base.OnSourceChanged(oldSource, newSource);
            lockPropertyValueChanged = true;
            ClearValue(PropertyValueProperty);
            lockPropertyValueChanged = false;
            if (!string.IsNullOrEmpty(PropertyName) && newSource != null)
            {
                lockPropertyValueChanged = true;
                BindingOperations.SetBinding(this, PropertyValueProperty,
                    new Binding(PropertyName) { Source = newSource, Mode = BindingMode.OneWay });
                lockPropertyValueChanged = false;
            }
        }
        protected virtual void OnPropertyValueChanged(object value)
        {
            if (lockPropertyValueChanged) return;
            if (Object.Equals(value, TriggerValue))
                DoFocus();
        }
        internal TimeSpan GetFocusDelay()
        {
            if (EventName == "Loaded")
                return FocusDelay ?? DefaultFocusDelay;
            else
                return FocusDelay ?? TimeSpan.FromMilliseconds(0);
        }
        void DoFocus()
        {
            if (!IsAttached) return;
            var focusDelay = GetFocusDelay();
            if (focusDelay == TimeSpan.FromMilliseconds(0))
            {
                AssociatedObject.Focus();
                return;
            }
            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = focusDelay,
            };
            timer.Tick += OnTimerTick;
            timer.Start();
        }
        void OnTimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Tick -= OnTimerTick;
            timer.Stop();
            AssociatedObject.Focus();
        }
    }

}
