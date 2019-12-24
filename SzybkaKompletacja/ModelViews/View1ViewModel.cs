using DevExpress.Mvvm;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
      class View1ViewModel : ViewModelBase, IMVVMDockingProperties
    {
        //public string TargetName
        //{
        //    get { return "PanelGroup"; }
        //    set { throw new NotImplementedException(); }
        //}
        public View1ViewModel()
        {

        }
        public bool IsClosed
        {
            get { return GetProperty(() => IsClosed); }
            set { SetProperty(() => IsClosed, value); }
        }
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
    }
}
