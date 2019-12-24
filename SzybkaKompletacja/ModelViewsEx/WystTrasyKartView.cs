using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
 
    public class WystTrasyKartView : INotifyPropertyChanged
    {
        public Nullable<int> ID_IHP_WYSTTRASAKONTRAH { get; set; }
        public Nullable<int> ID_IHP_KONTRAHENT { get; set; }
        public Nullable<int> ID_IHP_TRASY { get; set; }
        public string NAZWA { get; set; }

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
