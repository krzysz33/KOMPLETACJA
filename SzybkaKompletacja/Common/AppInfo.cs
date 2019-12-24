using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Common
{
  public   class AppInfo
    {
        private int idtrasa;
        private string origin;
        private int location;

        public  AppInfo(int idtrasa)
        {
            this.idtrasa = idtrasa;
        }

        public int IdTrasa
        {
            get { return this.idtrasa; }
        }
        
    }
}
