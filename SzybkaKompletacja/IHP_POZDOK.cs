//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KpInfohelp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    public partial class IHP_POZDOK: INotifyPropertyChanged
    {
        public int ID_IHP_POZDOK { get; set; }
        public int ID_IHP_NAGLDOK { get; set; }
        public int ID_IHP_KARTOTEKA { get; set; }
        public int LP { get; set; }
        public decimal ILOSC { get; set; }
        public decimal ILOSCRAZEM { get; set; }
        public decimal ILOSCPACZKA { get; set; }
        private decimal _ilosczw;
        public Nullable<decimal> ILOSCZW {
            get
            {
                return _ilosczw;
            }
            set
            {
                _ilosczw = value ?? 0;
                RisePropertyChanged("ILOSCZW");
            }
                
                }
        public string NAZWASKRPOZ { get; set; }
        public decimal CENANETTO { get; set; }
        public decimal CENABRUTTO { get; set; }
        public decimal WARTVAT { get; set; }
        public decimal WARTNETTO { get; set; }
        public decimal WARTBRUTTO { get; set; }
        public System.DateTime DATADOK { get; set; }
        public short CENAUSTALONA { get; set; }
        public string UWAGI { get; set; }
        public int ID_TOWSUBJECT { get; set; }
        public Nullable<int> ID_IHP_DEFSTATUS { get; set; }
    
        public virtual IHP_KARTOTEKA IHP_KARTOTEKA { get; set; }
        public virtual IHP_NAGLDOK IHP_NAGLDOK { get; set; }
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}