using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class IHP_PARAMETRY_EX : IHP_PARAMETRY
    {
       AppConfig app;
        public IHP_PARAMETRY_EX(IHP_PARAMETRY item)
         {
             this.ID_GRUPAPARAMETRY = item.ID_GRUPAPARAMETRY;
             this.ID_IHP_PARAMETRY = item.ID_IHP_PARAMETRY;
             this.PARAMETR = item.PARAMETR;
             this.RODZAJ = item.RODZAJ;
             this.WARTOSC = item.WARTOSC;
             this.ID_GRU_PARAMETRY = item.ID_GRU_PARAMETRY;

             app = AppConfig.GetInstance;
             SetWArtocEx();
             SetLpMain();
        }
        private string getparamname()
        {
         string res = "brak";
            foreach (AppSettingsUstawieniaAplikacjiParametr item in app.UstawieniaAplikacji.UstawieniaAplikacji.Parametry.Where(x => x.IdIhpParametry == ID_IHP_PARAMETRY))
            {
                foreach (AppSettingsUstawieniaAplikacjiParametrWartosci item2 in item.Wartosci)
                {
                    if (item2.Wartosc == WARTOSC)
                        res =  item2.opis;
               }
            }
            return res;
            //return item.Where(x=>x.)
        }

        private void SetWArtocEx()
        {
            if (RODZAJ == 1)
            {
                if (WARTOSC=="0")
                    WARTOSCEX = "TAK";
                if (WARTOSC == "1")
                    WARTOSCEX = "NIE";
            }
            if (RODZAJ == 2)
                WARTOSCEX = getparamname();
   }


        private string _wartoscex;
        public string WARTOSCEX
        {
            get
            {
                return _wartoscex;
            }
            set
            {
                 _wartoscex = value;
            }
        }

        private void SetLpMain()
        {
             if ((ID_IHP_PARAMETRY == ID_GRU_PARAMETRY) && (ID_GRUPAPARAMETRY==2))
            {
                LpMain = 1;
                WARTOSCEX = String.Empty;
            }                
            else
                LpMain = 0;
        }

        private int _lpmain;

        public int LpMain
        {
            get
            {
                return _lpmain;
            }

            set
            {
                _lpmain = value;
                
            }
        }

    }
}
