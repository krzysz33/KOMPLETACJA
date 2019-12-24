using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using DevExpress.Mvvm;

namespace KpInfohelp
{
    public class ViewModel : ViewModelBase
    {
        ICommand onViewLoadedCommand;
        ICommand navigateCommand;

        public ICommand OnViewLoadedCommand
        {
            get
            {
                if (onViewLoadedCommand == null)
                    onViewLoadedCommand = new DelegateCommand(OnViewLoaded);
                return onViewLoadedCommand;
            }
        }
        public ICommand NavigateCommand
        {
            get
            {
                if (navigateCommand == null)
                    navigateCommand = new DelegateCommand<string>(Navigate);
                return navigateCommand;
            }
        }
        public void Navigate(string target)
        {
            NavigationService.Navigate(target, null, this);
        }
        protected INavigationService NavigationService { get { return GetService<INavigationService>(); } }
        protected virtual void OnViewLoaded() { }
    }
}
