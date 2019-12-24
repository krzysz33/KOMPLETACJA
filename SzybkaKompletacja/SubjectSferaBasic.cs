using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace KpInfohelp
{
 public   class SubjectSferaBasic
    {
       
        ////  // Internal members
        //protected InsERT.GT _gt = null;
        //protected InsERT.Subiekt subiekt = null;
        //protected InsERT.KontrahenciLista listaKH = null;
        //public SubjectSferaBasic()
        //{
        //    connect();
        //}


        //private void connect()
        //{
        //    try
        //    {
        //        // Utworzenie obiektu GT
        //        _gt = new InsERT.GT();
        //        _gt.Produkt = InsERT.ProduktEnum.gtaProduktSubiekt;
        //        _gt.Serwer = ConfigurationManager.AppSettings["Serwer"].ToString();
        //        _gt.Baza = ConfigurationManager.AppSettings["Baza"].ToString();
        //        _gt.Autentykacja = InsERT.AutentykacjaEnum.gtaAutentykacjaMieszana;
        //        _gt.Uzytkownik = ConfigurationManager.AppSettings["Uzytkownik"].ToString();
        //        _gt.UzytkownikHaslo = ConfigurationManager.AppSettings["UzytkownikHaslo"].ToString();
        //        _gt.Operator = ConfigurationManager.AppSettings["Operator"].ToString();
        //        _gt.OperatorHaslo = ConfigurationManager.AppSettings["OperatorHaslo"].ToString();
        //        // Uruchomienie Subiekta GT
        //        subiekt = (InsERT.Subiekt)_gt.Uruchom((Int32)InsERT.UruchomDopasujEnum.gtaUruchomDopasuj, (Int32)InsERT.UruchomEnum.gtaUruchomNieArchiwizujPrzyZamykaniu);
        //        subiekt.Okno.Widoczne = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        Console.ReadKey();
        //    }
        //}

    }
}
