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
    
    public partial class IHP_WYSTTRASAKONTRAH
    {
        public int ID_IHP_WYSTTRASAKONTRAH { get; set; }
        public int ID_IHP_KONTRAHENT { get; set; }
        public int ID_IHP_TRASY { get; set; }
    
        public virtual IHP_KONTRAHENT IHP_KONTRAHENT { get; set; }
        public virtual IHP_TRASY IHP_TRASY { get; set; }
    }
}
