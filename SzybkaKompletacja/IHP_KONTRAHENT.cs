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
    
    public partial class IHP_KONTRAHENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IHP_KONTRAHENT()
        {
            this.IHP_KIEROWCA = new HashSet<IHP_KIEROWCA>();
            this.IHP_KONTRAHENT_ARCH = new HashSet<IHP_KONTRAHENT_ARCH>();
            this.IHP_WYSTTRASAKONTRAH = new HashSet<IHP_WYSTTRASAKONTRAH>();
            this.IHP_NAGLDOK = new HashSet<IHP_NAGLDOK>();
        }
    
        public int ID_IHP_KONTRAHENT { get; set; }
        public Nullable<int> NRKONTRAH { get; set; }
        public string NIP { get; set; }
        public string NAZWA { get; set; }
        public string MIEJSCOWOSC { get; set; }
        public string KODPOCZTOWY { get; set; }
        public string POCZTA { get; set; }
        public string ULICA { get; set; }
        public string NRDOMU { get; set; }
        public string NRLOKALU { get; set; }
        public string EMAIL { get; set; }
        public string TELEFON { get; set; }
        public int ID_IHP_DEFCENY { get; set; }
        public int ID_KH_SUBJECT { get; set; }
        public string TELKOM { get; set; }
        public Nullable<short> TYPKONTRAH { get; set; }
        public string NAZWAPELNA { get; set; }
        public Nullable<int> ID_IHP_RODZAJDOKDEF { get; set; }
    
        public virtual IHP_DEFCENY IHP_DEFCENY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_KIEROWCA> IHP_KIEROWCA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_KONTRAHENT_ARCH> IHP_KONTRAHENT_ARCH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_WYSTTRASAKONTRAH> IHP_WYSTTRASAKONTRAH { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_NAGLDOK> IHP_NAGLDOK { get; set; }
    }
}
