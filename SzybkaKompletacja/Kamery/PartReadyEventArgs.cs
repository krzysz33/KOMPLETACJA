using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KpInfohelp
{
   public class PartReadyEventArgs:EventArgs
    {
        public byte[] Part { get; set; }
    }
}
