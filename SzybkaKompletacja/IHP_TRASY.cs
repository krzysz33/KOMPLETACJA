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
    
    public partial class IHP_TRASY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IHP_TRASY()
        {
            this.IHP_WYSTTRASAKONTRAH = new HashSet<IHP_WYSTTRASAKONTRAH>();
        }
    
        public int ID_IHP_TRASY { get; set; }
        public string NAZWA { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_WYSTTRASAKONTRAH> IHP_WYSTTRASAKONTRAH { get; set; }
    }
}
