using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace KpInfohelp
{

    public class  ViewStatusy : INotifyPropertyChanged
    {
        private bool _zazn;

        public event PropertyChangedEventHandler PropertyChanged;
        
protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
 
        public int ID_DEFSTATUS { get; set; }

        private int _id_ihp_zam_users;
        public int ID_IHP_ZAM_USERS {
            get
            {
                return _id_ihp_zam_users;
            }
               set
            {
                _id_ihp_zam_users = value;
                if (_id_ihp_zam_users > 0)
                                  _zazn = true;
                RisePropertyChanged("ID_IHP_ZAM_USERS");
            }
                }
         public string NAZWA { get; set; }

        [DisplayName("Zaznaczenie")]
        public bool Zazn
        {
            get
            {
                return _zazn;
            }
            set
            {
                _zazn = value;
                RisePropertyChanged("Zazn");
            }
        }
    }

  

}
