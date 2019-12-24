using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace KpInfohelp
{
   public class ImageReadyEventArsgs:EventArgs
    {
        public BitmapImage Image { get; set; }
    }
}
