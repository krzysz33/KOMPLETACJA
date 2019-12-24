using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class DragDropBehavior : Behavior<DXTabControl>
    {
        /// <summary>
        /// Interaction logic for Kartoteki.xaml
        /// </summary>
        ///     public class DragDropBehavior : Behavior<DXTabControl>{
        Point startPoint;
    public DXTabControl AssociatedDXTabControl { get { return (DXTabControl)this.AssociatedObject; } }
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.Loaded += AssociatedObject_Loaded;
    }
    void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {

        foreach (var item in AssociatedObject.Items)
        {
            var tabitem = AssociatedDXTabControl.ItemContainerGenerator.ContainerFromItem(item) as DXTabItem;
            tabitem.PreviewMouseDown += DragDropBehavior_PreviewMouseDown;
            tabitem.PreviewMouseMove += DragDropBehavior_PreviewMouseMove;
            tabitem.Drop += DragDropBehavior_Drop;
            tabitem.AllowDrop = true;
        }
    }
    void DragDropBehavior_PreviewMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            startPoint = e.GetPosition(null);
    }
    void DragDropBehavior_Drop(object sender, System.Windows.DragEventArgs e)
    {
        var tabItemTarget = sender as DXTabItem;

        var tabItemSource = e.Data.GetData(typeof(DXTabItem).FullName) as DXTabItem;

        if (!tabItemTarget.Equals(tabItemSource))
        {
            var tabControl = tabItemTarget.Owner as DXTabControl;
            int sourceIndex = tabControl.ItemContainerGenerator.IndexFromContainer(tabItemSource);
            int targetIndex = tabControl.ItemContainerGenerator.IndexFromContainer(tabItemTarget);

            var source = tabControl.ItemsSource as ObservableCollection<ExampleObject>;
            source.Move(sourceIndex, targetIndex);
            tabControl.SelectedItem = tabItemSource;
        }
    }
    void DragDropBehavior_PreviewMouseMove(object sender, MouseEventArgs e)
    {
        var tabItem = sender as DXTabItem;

        if (tabItem == null)
        {
            return;
        }
        var diff = startPoint - e.GetPosition(null);
        if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
        {
            DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.All);
        }

    }
}

    public partial class Poz: DXWindow
    {
      public Poz()
       {
            InitializeComponent();
       }
        private void simpleButton_Click(object sender, RoutedEventArgs e)
        {
            ///
        }
        private void Tile_Click(object sender, EventArgs e){ }
    }
   
}
