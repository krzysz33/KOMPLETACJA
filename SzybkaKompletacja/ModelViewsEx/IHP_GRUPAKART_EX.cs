using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    using System.Collections.Generic;

    public partial class IHP_GRUPAKART_EX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IHP_GRUPAKART_EX()
        {
            this.IHP_WYSTGRKART = new HashSet<IHP_WYSTGRKART>();
        }

        public int ID_IHP_GRUPAKART { get; set; }
        public string NAZWAGRUPY { get; set; }
        public string KODGRUPY { get; set; }
        public string KODZLOZONY { get; set; }
        public string NAZWAZLOZONA { get; set; }
        public int ID_IHP_RODZGRUPKART { get; set; }
        public int ID_IHP_GRUPAKART_NADRZ { get; set; }

        public virtual IHP_RODZGRUPKART IHP_RODZGRUPKART { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IHP_WYSTGRKART> IHP_WYSTGRKART { get; set; }
    }
}
