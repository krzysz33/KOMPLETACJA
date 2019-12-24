using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanowanieDostaw.ModelViews
{
    class ViewModelHarmonogramSlownik
    {
        public ViewModelHarmonogramSlownik()
        {
            AritHarmonogramSl = SzablonChowuDataAdapter.GetSzablonSlownik("1");
        }
        public List<AritHarmonogramSlownik> AritHarmonogramSl { get; set; }
    }
}


