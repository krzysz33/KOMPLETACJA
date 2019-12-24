using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public interface IMainWindowService
    {
        void ShowMainWindow();
    }

    public class MainWindowService : ServiceBase, IMainWindowService
    {
        public void ShowMainWindow()
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
        }
    }



    public interface IMainLoginService
    {
        void ShowLoginWindow();
    }

    public class MainLoginService : ServiceBase, IMainLoginService
    {
        public void ShowLoginWindow()
        {
            LoginWindow wnd = new LoginWindow();
            wnd.Show();
        }
    }
}
