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
    
    public partial class IHP_CENNIK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IHP_CENNIK()
        {
            this.IHP_CENNIKHISTORIA = new HashSet<IHP_CENNIKHISTORIA>();
        }
    
        public int ID_IHP_CENNIK { get; set; }
        public int ID_IHP_KARTOTEKA { get; set; }
        public int ID_IHP_DEFCENY { get; set; }
        public decimal CENAN { get; set; }
        public decimal CENAB { get; set; }
    
        public virtual IHP_DEFCENY IHP_DEFCENY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_CENNIKHISTORIA> IHP_CENNIKHISTORIA { get; set; }
        public virtual IHP_KARTOTEKA IHP_KARTOTEKA { get; set; }
    }
}