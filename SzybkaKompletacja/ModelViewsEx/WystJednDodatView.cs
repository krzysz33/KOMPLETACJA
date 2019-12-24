using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
 
    public class WystJednDodatView : INotifyPropertyChanged
    {
        public int  ID_IHP_WYST_JZ { get; set; }
        public int ID_IHP_JZ { get; set; }
        public int  ID_KARTOTEKA { get; set; }
        public string NAZWA { get; set; }
        public decimal WARTOSC { get; set; }
        public short AKTYWNA { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
