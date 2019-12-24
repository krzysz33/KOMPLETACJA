using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class CustomDXMessageBoxLocalizer : DXMessageBoxLocalizer
    {
        protected override void PopulateStringTable()
        {
            base.PopulateStringTable();
            AddString(DXMessageBoxStringId.Ok, "OK");
            AddString(DXMessageBoxStringId.Yes, "TAK");
            AddString(DXMessageBoxStringId.No, "NIE");
            AddString(DXMessageBoxStringId.Cancel, "ANULUJ");
        }
    }
}
