
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class SubjectSfera : SubjectSferaBasic
    {
  //      InsERT.Kontrahent oKh;
  //      InsERT.SuDokument oDokFA, oDokPAR;
  //      InsERT.SuPozycja pozycja = null;
  //      public SubjectSfera() { }

  //      private bool JestKOntrah(string Symbol)
  //      {
  //          return subiekt.Kontrahenci.Istnieje(Symbol);
  //      }

  //      public int DodajKontrahenta(IHP_KONTRAHENT _kontrah)
  //      {
  //          int idKh = -1;

  //          if (_kontrah.ID_KH_SUBJECT == 0) return -1;
  //          try
  //          {
  //              if (!subiekt.Kontrahenci.Istnieje(_kontrah.ID_KH_SUBJECT))
  //              {
  //                  oKh = subiekt.Kontrahenci.Dodaj();
  //                  oKh.Symbol = _kontrah.NRKONTRAH;
  //                  oKh.Nazwa = _kontrah.NAZWA;
  //                  oKh.NazwaPelna = _kontrah.NAZWAPELNA;
  //                  oKh.NIP = _kontrah.NIP;
  //                  oKh.Miejscowosc = _kontrah.MIEJSCOWOSC;
  //                  oKh.Ulica = _kontrah.ULICA;
  //                  oKh.NrDomu = _kontrah.NRDOMU;
  //                  oKh.NrLokalu = _kontrah.NRLOKALU;
  //                  oKh.KodPocztowy = _kontrah.KODPOCZTOWY;
  //                  oKh.Zapisz();
  //                  idKh = oKh.Identyfikator;
  //                  oKh.Zamknij();
  //              }
  //          }
  //          catch (Exception e)
  //          {
  //              //  throw e;
  //          }
  //          return idKh;
  //      }

  //      public void DodajFakture()
  //      {
  //          try
  //          {
  //              oDokFA = subiekt.SuDokumentyManager.DodajFS();
  //              oDokFA.KontrahentId = 1;
  //              oDokFA.Pozycje.Dodaj(1);
  //              oDokFA.Zapisz();
  //              //   oDokFA.Wyswietl();
  //              oDokFA.Zamknij();
  //          }
  //          catch (Exception ex)
  //          {
  //              throw ex;
  //          }
  //      }


  //      public void DodajParagon()
  //      {
  //          try
  //          {
  //              oDokFA = subiekt.SuDokumentyManager.DodajPA();
  //              oDokFA.KontrahentId = 1;
  //              pozycja = oDokFA.Pozycje.Dodaj(1);
  //              pozycja.IloscJm = 5;
  //              oDokFA.Zapisz();
  //              oDokFA.DrukujDoPliku("paragon.pdf", 0);
  //              //   oDokFA.Wyswietl();
  //              oDokFA.Zamknij();
  //          }

  //          catch (Exception ex)
  //          {
  //              throw ex;
  //          }
  //      }

  //      public void DrukujParagon()
  //      {

  //      }


  //      public static InsERT.KontrahenciLista przypiszListeKontrahentow(InsERT.Subiekt SGT)
  //      {
  //          return SGT.Kontrahenci.Wybierz();
  //      }


  //      private List<string> wczytajTelefonyKontrahenta(InsERT.Kontrahent kontrahent)
  //      {
  //          InsERT.KhTelefony telefony = (InsERT.KhTelefony)kontrahent.Telefony;

  //          if (telefony.Liczba == 0)
  //              return null;
  //          else
  //          {
  //              List<string> listaTelofonow = new List<string>();

  //              InsERT.KhTelefon telefon;

  //              for (int i = 1; i <= telefony.Liczba; i++)
  //              {
  //                  telefon = telefony.Wczytaj(i);
  //                  listaTelofonow.Add(telefon.Numer);
  //              }
  //              return listaTelofonow;
  //          }
  //      }

  //      public List<IHP_KONTRAHENT_ARCH> wyswietlListeKontrah()
  //      {
  //          List<IHP_KONTRAHENT_ARCH> LstKontrah = new List<IHP_KONTRAHENT_ARCH>();
  //          string tel1 = string.Empty;
  //          string tel2 = string.Empty;
  //          try
  //          {
  //              InsERT.KontrahenciLista listaKH = subiekt.Kontrahenci.Wybierz();
  //              listaKH.MultiSelekcja = true;
  //              listaKH.Wyswietl();

  //              foreach (InsERT.Kontrahent kh in listaKH.ZaznaczeniKontrahenci())
  //              {
  //                  List<string> lstTelefony = wczytajTelefonyKontrahenta(kh);
  //                  if (lstTelefony != null)
  //                  {
  //                      if (lstTelefony[0] != null)
  //                          tel1 = lstTelefony[0];
  //                      else
  //                          tel1 = string.Empty;

  //                      if (lstTelefony.Count > 1)
  //                      {
  //                          if (lstTelefony[1] != null)
  //                              tel2 = lstTelefony[1];
  //                          else
  //                              tel2 = string.Empty;
  //                      }
  //                  }
  //                  string wNazwisko = kh.NazwaPelna;
  //                  if (wNazwisko.Length > 60)
  //                      wNazwisko = wNazwisko.Substring(1, 60);
  //                  IHP_KONTRAHENT_ARCH KhLustro = new IHP_KONTRAHENT_ARCH()
  //                  {
  //                      ID_IHP_KONTRAHENT_ARCH = 0,
  //                      ID_IHP_DEFCENY = 1,
  //                      TYPKONTRAH = 0,
  //                      NAZWA = kh.Nazwa,
  //                      NRKONTRAH = 0,
  //                      TELEFON = tel1,
  //                      TELKOM = tel2,
  //                      EMAIL = kh.Email,
  //                      //       UWAGI = kh.Uwagi,
  //                      // NAZWA = wNazwisko,
  //                      ID_KH_SUBJECT = kh.Identyfikator,
  //                      NIP = kh.NIP,
  //                      MIEJSCOWOSC = kh.Miejscowosc,
  //                      KODPOCZTOWY = kh.KodPocztowy,
  //                      POCZTA = kh.Miejscowosc,
  //                      ULICA = kh.Ulica,
  //                      NRDOMU = kh.NrDomu,
  //                      NRLOKALU = kh.NrLokalu
  //                  };
  //                  LstKontrah.Add(KhLustro);
  //              }


  //              return LstKontrah;
  //          }
  //          catch (Exception ex)
  //          {
  //              //       MessageBox.Show(ex.Message + "\n\n Wybrany użytkownik nie znajduje się w bazie danych podmiotu.", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
  //              return null;
  //          }

  //      }

  //      public static InsERT.Kontrahent wyswietlListeKontrahentow(InsERT.KontrahenciLista listaKH)
  //      {
  //          try
  //          {
  //              listaKH.MultiSelekcja = false;
  //              listaKH.Wyswietl();
  //              if (listaKH.ZaznaczoneId().LiczbaElementow == 1)
  //              {
  //                  InsERT.Kontrahent KH = null;
  //                  foreach (InsERT.Kontrahent kh in listaKH.ZaznaczeniKontrahenci())
  //                  {
  //                      KH = kh;
  //                  }
  //                  return KH;
  //              }
  //              return null;
  //          }
  //          catch (Exception ex)
  //          {
  //              //       MessageBox.Show(ex.Message + "\n\n Wybrany użytkownik nie znajduje się w bazie danych podmiotu.", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Error);
  //              return null;
  //          }

  //      }

  //      public static List<string> wczytajEmaileKontrahenta(InsERT.Kontrahent kontrahent)
  //      {
  //          InsERT.KhEmaile emaile = (InsERT.KhEmaile)kontrahent.Emaile;

  //          if (emaile.Liczba == 0)
  //              return null;
  //          else
  //          {
  //              List<string> listaEmaili = new List<string>();

  //              InsERT.KhEmail email;

  //              for (int i = 1; i <= emaile.Liczba; i++)
  //              {
  //                  email = emaile.Wczytaj(i);
  //                  listaEmaili.Add(email.Email);
  //              }
  //              return listaEmaili;
  //          }
  //      }



  //      public static int wczytajTraseKontrahenta(InsERT.Kontrahent kontrahent)
  //      {
  //          object ob = kontrahent.get_PoleWlasne("Trasa-słownikowa");
  //          if (ob.GetType() == typeof(int))
  //              return kontrahent.get_PoleWlasne("Trasa-słownikowa");
  //          else
  //              return -1;
  //      }

  //      public static InsERT.Kontrahent zmianaKontrahenta(InsERT.Subiekt sgt)
  //      {
  //          InsERT.KontrahenciLista listaKh = przypiszListeKontrahentow(sgt);
  //          InsERT.Kontrahent kontrahent = wyswietlListeKontrahentow(listaKh);
  //          if (kontrahent != null)
  //              return kontrahent;
  //          return null;
  //      }

  //      public static InsERT.Kontrahent dodajKontrahenta(InsERT.Subiekt sgt)
  //      {
  //          InsERT.Kontrahent kontrahent = sgt.Kontrahenci.Dodaj();
  //          kontrahent.Wyswietl();
  //          if (kontrahent.Symbol != null && (DBNull)kontrahent.Nazwa != DBNull.Value)
  //          {
  //              //     if (MessageBox.Show("Czy wykonać wycene dla nowego kontrahenta?", "Uwaga", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
  //              //         return sgt.Kontrahenci.Wczytaj(kontrahent.Identyfikator);
  //              //     else
  //              //        return null;
  //          }
  //          return null;
  //      }
  //      public static InsERT.Kontrahent wczytajKontrahenta(InsERT.Subiekt sgt, int kh_id)
  //      {
  //          if (kh_id > 0)
  //          {
  //              InsERT.Kontrahent kh = null;
  //              if (sgt.Kontrahenci.Istnieje((int)kh_id))
  //              {
  //                  kh = sgt.Kontrahenci.Wczytaj((int)kh_id);
  //                  return kh;
  //              }
  //              else
  //                  return null;
  //          }
  //          else
  //          {
  //              return null;
  //          }
  //      }
  //      /// <summary>
  //      /// _____ szt. szer x wys mm
  //      /// </summary>
  //      /// <param name="ilosc"></param>
  //      /// <param name="szerokosc"></param>
  //      /// <param name="wysokosc"></param>
  //      /// <returns></returns>
  //      public static string generujOpis(int ilosc, decimal szerokosc, decimal wysokosc)
  //      {
  //          try
  //          {
  //              StringBuilder bld = new StringBuilder();

  //              bld.AppendFormat("{0,5}", ilosc);
  //              bld.Append(" szt. ");
  //              bld.Append(Math.Round(szerokosc * 10, 1) + " x " + Math.Round(wysokosc * 10, 1) + " mm");

  //              return bld.ToString();
  //          }
  //          catch
  //          {

  //              return string.Empty;
  //          }
  //      }
  //      public int DodajTowarPoz(IHP_POZDOK towarDto)
  //      {
  //          return -1;

  //      }
  //      public int DodajTowar(IHP_KARTOTEKA towarDto)
  //      {
  //          int retvalue = 1;
  //          int idVatu = 1;
  //          try
  //          {

  //              var towarSfera = subiekt.TowaryManager.DodajTowar();


  //              towarSfera.Symbol = towarDto.INDEKS;
  //              //    towarSfera.GrupaId = idGrupy;
  //              towarSfera.Nazwa = towarDto.NAZWASKR;
  //              towarSfera.NazwaDlaUF = towarDto.NAZWASKR;
  //              towarSfera.Opis = string.Empty;
  //              // towarSfera.SprzedazVatId = idVatu;
  //              //    towarSfera.ZakupVatId = idVatu;
  //              towarSfera.VatZakJakPrzySp = true;
  //              towarSfera.Miary.Podstawowa = towarDto.IHP_JM.JM;
  //              towarSfera.SprzedazJm = towarDto.IHP_JM.JM;
  //              towarSfera.SprzedazJmInna = false;
  //              towarSfera.ZakupJm = towarDto.IHP_JM.JM;
  //              towarSfera.ZakupJmInna = false;
  //              towarSfera.SWW = string.Empty;
  //              towarSfera.SprzedazPrzezWartosc = 0;
  //              towarSfera.CzasDostawy = 0;

  //              if (!string.IsNullOrEmpty(towarDto.KODEAN))
  //                  towarSfera.KodyKreskowe.Podstawowy = towarDto.KODEAN;
  //              //     towarSfera.Aktywny = "T";
  //              towarSfera.CenaKartotekowa = 0;
  //              towarSfera.CenaOtwarta = 0;
  //              towarSfera.KontrolaTerminu = false;
  //              towarSfera.DniWaznosci = 0;
  //              towarSfera.DoSklepuInternetowego = false;
  //              towarSfera.DoSerwisuAukcyjnego = false;
  //              towarSfera.DoSprzedazyMobilnej = false;
  //              towarSfera.ObrotMarza = 0;

  //              towarSfera.Zapisz();

  //              retvalue = towarSfera.Identyfikator;

  //              towarSfera.Zamknij();

  //              //    Marshal.ReleaseComObject(towarSfera);
  //          }

  //          catch (Exception e)
  //          {
  //              //   _logger.WriteError("Sfera :: Nie można dodać towaru [IdEvendo: {0}] [Nazwa {1}] :: wyjątek {2}.",
  //              //    towarDto.IdEvendo, towarDto.Nazwa, e.ToString());
  //          }
  //          return retvalue;
  //      }

  //      public int DodajUsluge(IHP_KARTOTEKA towarDto)
  //      {
  //          int retvalue = 1;
  //          int idVatu = 1;
  //          try
  //          {

  //              var towarSfera = subiekt.TowaryManager.DodajUsluge();


  //              towarSfera.Symbol = towarDto.INDEKS;
  //              //    towarSfera.GrupaId = idGrupy;
  //              towarSfera.Nazwa = towarDto.NAZWASKR;
  //              towarSfera.NazwaDlaUF = towarDto.NAZWASKR;
  //              towarSfera.Opis = string.Empty;
  //              //  towarSfera.SprzedazVatId = idVatu;
  //              //  towarSfera.ZakupVatId = idVatu;
  //              towarSfera.VatZakJakPrzySp = true;
  //              towarSfera.SprzedazJmInna = false;
  //              towarSfera.SWW = string.Empty;
  //              towarSfera.SprzedazPrzezWartosc = 0;
  //              towarSfera.CzasDostawy = 0;

  //              if (!string.IsNullOrEmpty(towarDto.KODEAN))
  //                  towarSfera.KodyKreskowe.Podstawowy = towarDto.KODEAN;
  //              //     towarSfera.Aktywny = "T";
  //              towarSfera.CenaKartotekowa = 0;
  //              towarSfera.CenaOtwarta = 0;
  //              towarSfera.KontrolaTerminu = false;
  //              towarSfera.DniWaznosci = 0;
  //              towarSfera.DoSklepuInternetowego = false;
  //              towarSfera.DoSerwisuAukcyjnego = false;
  //              towarSfera.DoSprzedazyMobilnej = false;
  //              towarSfera.ObrotMarza = 0;

  //              towarSfera.Zapisz();

  //              retvalue = towarSfera.Identyfikator;

  //              towarSfera.Zamknij();

  //              //    Marshal.ReleaseComObject(towarSfera);
  //          }

  //          catch (Exception e)
  //          {
  //              //   _logger.WriteError("Sfera :: Nie można dodać towaru [IdEvendo: {0}] [Nazwa {1}] :: wyjątek {2}.",
  //              //    towarDto.IdEvendo, towarDto.Nazwa, e.ToString());
  //          }
  //          return retvalue;
  //      }
  //      public int GenerujDokZamowienie(IHP_NAGLDOK nagl)
  //      {
  //          int idDok = 0;
  //          try
  //          {
  //              InsERT.SuDokument dokument = null;
  //              InsERT.SuPozycja pozycja = null;

  //              if (nagl.ID_RODZAJDOK == 1)
  //              {
  //                  dokument = subiekt.SuDokumentyManager.DodajFS();
  //                  dokument.KontrahentId = nagl.ID_KH_SUBJECT;
  //              }
  //              else if (nagl.ID_RODZAJDOK == 2)
  //              {
  //                  dokument = subiekt.SuDokumentyManager.DodajPA();
  //              }


  //              foreach (IHP_POZDOK item in nagl.IHP_POZDOK)
  //              {
  //                  //     var towarSfera = subiekt.TowaryManager.WczytajTowar(item.ID_TOWSUBJECT);
  //                  pozycja = dokument.Pozycje.Dodaj(item.ID_TOWSUBJECT);
  //                  pozycja.CenaNettoPrzedRabatem = item.CENANETTO;
  //                  pozycja.IloscJm = item.ILOSC;
  //                  //pozycja.Jm = item.KARTOTEKA.JM1;

  //              }
  //              //   dokument.Wyswietl(false);
  //              dokument.Zapisz();
  //              idDok = dokument.Identyfikator;
  //              dokument.Zamknij();

  //          }
  //          catch (Exception ex)
  //          {
  //              throw ex;
  //          }
  //          return idDok;

  //      }

     
  //      //public static int generujZamowienieKlienta(int idWyceny, InsERT.Subiekt SGT, ObiektyDataSet.DataSetSLOWNIKI dsSL,
  //      //    ObiektyDataSet.DataSetWYC_ZESTAW_ROLETY dsZestaw, string numerZamowienia, ref StringBuilder strBld)
  //      //{
  //      //    try
  //      //    {
  //      //        ObiektyDataSet.DataSetDOK_WYCENA dataset = new ObiektyDataSet.DataSetDOK_WYCENA();
  //      //        ObiektyDataSet.DataSetDOK_WYCENATableAdapters.TableAdapterManager mgr = new ObiektyDataSet.DataSetDOK_WYCENATableAdapters.TableAdapterManager();

  //      //        mgr.DOK_WYCENA_NAGLOWEKTableAdapter = new ObiektyDataSet.DataSetDOK_WYCENATableAdapters.DOK_WYCENA_NAGLOWEKTableAdapter();
  //      //        mgr.DOK_WYCENA_POZYCJATableAdapter = new ObiektyDataSet.DataSetDOK_WYCENATableAdapters.DOK_WYCENA_POZYCJATableAdapter();

  //      //        mgr.DOK_WYCENA_NAGLOWEKTableAdapter.Fill(dataset.DOK_WYCENA_NAGLOWEK);
  //      //        mgr.DOK_WYCENA_POZYCJATableAdapter.Fill(dataset.DOK_WYCENA_POZYCJA);

  //      //        var wycenaNag = dataset.DOK_WYCENA_NAGLOWEK.Where(x => x.ID == idWyceny).SingleOrDefault();
  //      //        var wycenaPoz = wycenaNag.GetDOK_WYCENA_POZYCJARows();

  //      //        OperacjePowiazania.operacjePOW_ZESTAW_ROLET powZest = new OperacjePowiazania.operacjePOW_ZESTAW_ROLET();
  //      //        OperacjePowiazania.operacjePowiazaniaTowarow powUsluga = new OperacjePowiazania.operacjePowiazaniaTowarow(dsSL);


  //      //        InsERT.SuDokument dokument = null;
  //      //        InsERT.SuPozycja pozycja = null;

  //      //        dokument = SGT.SuDokumentyManager.DodajZK();

  //      //        dokument.KontrahentId = wycenaNag.KONTRAHENT_ID;
  //      //        SGT.MagazynId = IdMagazynuDlaZamowienKlienta;
  //      //        dokument.NumerOryginalny = numerZamowienia;

  //      //        int idTW = 0;

  //      //        foreach (var item in wycenaPoz)
  //      //        {
  //      //            idTW = 0;
  //      //            if (item.IsSLOWNIK_IDNull() && item.IsSLOWNIK_TYPNull())
  //      //            {
  //      //                // z tabeli POW_ZESTAW_ROLET
  //      //                idTW = powZest.getIdTowarSubiekt(item.WYC_ZESTAW_ROLETY_ID);

  //      //                if (idTW == -1)
  //      //                {
  //      //                    strBld.AppendLine("Nie znaleziono odpowiednika zestawu: ");
  //      //                    strBld.AppendLine(OperacjeNaTabelach.operacjeWYC_ZESTAW_ROLETY.getOpisZestawu(
  //      //                        dsZestaw, dsSL, item.WYC_ZESTAW_ROLETY_ID));
  //      //                }
  //      //            }
  //      //            else
  //      //            {
  //      //                //z pol wlasnych subiekta
  //      //                int idKolor = 0;

  //      //                if ((Rolety.EnumDodatkiTabele)item.SLOWNIK_TYP == Rolety.EnumDodatkiTabele.SL_LISTWA_TYNKOWA)
  //      //                {
  //      //                    string[] dane = item.NAZWA.Split(',');

  //      //                    if (!string.IsNullOrWhiteSpace(dane[1]))
  //      //                    {
  //      //                        DataRow rowKolor = OperacjeNaTabelach.operacjeSL_KOLOR.getKolor(dsSL, dane[1]);

  //      //                        if (rowKolor != null)
  //      //                            idKolor = (int)rowKolor["ID"];
  //      //                    }
  //      //                }

  //      //                idTW = powUsluga.znajdzUsluge(item.SLOWNIK_ID, (Rolety.EnumDodatkiTabele)item.SLOWNIK_TYP, idKolor);

  //      //                if (idTW == -1)
  //      //                {
  //      //                    bladDopasowania(dsSL, strBld, (Rolety.EnumDodatkiTabele)item.SLOWNIK_TYP, item.SLOWNIK_ID, idKolor);
  //      //                }
  //      //            }

  //      //            if (idTW > 0)
  //      //            {
  //      //                pozycja = dokument.Pozycje.Dodaj(idTW);
  //      //                pozycja.CenaNettoPrzedRabatem = item.CENA;
  //      //                pozycja.IloscJm = item.ILOSC_RAZEM;
  //      //                pozycja.Jm = item.JEDNOSTKA;
  //      //                pozycja.RabatProcent = item.UPUST;
  //      //                try
  //      //                {
  //      //                    if (item.IsSLOWNIK_IDNull() && item.IsSLOWNIK_TYPNull())
  //      //                    {
  //      //                        decimal szer = 0;
  //      //                        decimal wys = 0;
  //      //                        dekodujSzerIWys(item.NAZWA, ref szer, ref wys);
  //      //                        pozycja.Opis = generujOpis(item.ILOSC_ROLET, szer, wys);
  //      //                    }
  //      //                }
  //      //                catch { }
  //      //            }
  //      //        }

  //      //        dokument.Wystawil = SGT.OperatorNazwa;
  //      //        dokument.Zapisz();
  //      //        int idDok = 0;
  //      //        idDok = dokument.Identyfikator;
  //      //        dokument.Zamknij();

  //      //        return idDok;
  //      //        //dokument.Wyswietl();

  //      //        //if (!string.IsNullOrWhiteSpace(dokument.Identyfikator.ToString()))
  //      //        //{
  //      //        //    return dokument.Identyfikator;
  //      //        //}
  //      //        //else
  //      //        //{
  //      //        //    return 0;
  //      //        //}

  //      //    }
  //      //    catch (Exception ex)
  //      //    {
  //      //        FunkcjePomocnicze.Funkcje.messageError(ex.Message);
  //      //        return 0;
  //      //    }
  //      //}

  //      //public static void pokazZamowienieKlienta(int idZK, InsERT.Subiekt SGT)
  //      //{
  //      //    try
  //      //    {
  //      //        InsERT.SuDokument dokument = null;

  //      //        dokument = SGT.SuDokumentyManager.Wczytaj(idZK);

  //      //        dokument.Wyswietl(true); //oznacza ze tylko do odczytu 

  //      //    }
  //      //    catch (Exception ex)
  //      //    {
  //      //        FunkcjePomocnicze.Funkcje.messageError(ex.Message);
  //      //    }
  //      //}

  //      //public static int generujRW(InsERT.Subiekt SGT, ObiektyDataSet.DataSetSLOWNIKI dsSL, List<ObiektySubiekt.ElementDoRW> listaELementow, ref StringBuilder strBld)
  //      //{
  //      //    try
  //      //    {
  //      //        OperacjePowiazania.operacjePowiazaniaTowarow powTowar = new OperacjePowiazania.operacjePowiazaniaTowarow(dsSL);

  //      //        InsERT.SuDokument dokument = null;
  //      //        InsERT.SuPozycja pozycja = null;

  //      //        dokument = SGT.SuDokumentyManager.DodajRW();

  //      //        SGT.MagazynId = idMagazynuDlaRW;

  //      //        foreach (var item in listaELementow)
  //      //        {
  //      //            //z pol wlasnych subiekta
  //      //            if (item.Kolor == null)
  //      //                item.Kolor = 0;

  //      //            List<int> idTW = powTowar.znajdzTowarLista(item.Element_id, (Rolety.EnumDodatkiTabele)item.Element_typ, (int)item.Kolor);

  //      //            if (idTW != null && idTW.Count > 0)
  //      //            {
  //      //                List<Dostawa> listaDost = znajdzWszystkieDostawyDlaTowarow(SGT, idTW);

  //      //                if (listaDost != null && listaDost.Count > 0)
  //      //                {
  //      //                    int idDoRW = listaDost.ElementAt(0).IdTw;

  //      //                    pozycja = dokument.Pozycje.Dodaj(idDoRW);
  //      //                    pozycja.IloscJm = item.Ilosc;
  //      //                    pozycja.Jm = item.Jednostka;
  //      //                }
  //      //            }
  //      //            else
  //      //            {
  //      //                bladDopasowania(dsSL, strBld, ((Rolety.EnumDodatkiTabele)item.Element_typ), item.Element_id, item.Kolor);
  //      //            }

  //      //        }

  //      //        dokument.StatusDokumentu = InsERT.SubiektDokumentStatusEnum.gtaSubiektDokumentStatusOdlozony;

  //      //        dokument.Wystawil = SGT.OperatorNazwa;
  //      //        dokument.Wyswietl(false);
  //      //        int idDok = 0;
  //      //        idDok = dokument.Identyfikator;
  //      //        dokument.Zamknij();

  //      //        return idDok;

  //      //    }
  //      //    catch (Exception ex)
  //      //    {
  //      //        FunkcjePomocnicze.Funkcje.messageError(ex.Message);
  //      //        return 0;
  //      //    }
  //      //}

  //      //private static void bladDopasowania(ObiektyDataSet.DataSetSLOWNIKI dsSL, StringBuilder strBld,
  //      //    Rolety.EnumDodatkiTabele typElementu, int idElementu, int? idKolor)
  //      //{
  //      //    strBld.AppendLine("Nie znaleziono dopasowania dla elementu: ");
  //      //    strBld.AppendLine("Typ:  " + typElementu.ToString());
  //      //    strBld.AppendLine("Element:  " + OperacjeNaTabelach.operacjeOgolneSlowniki.getDaneSlownikowe(
  //      //        dsSL, typElementu, idElementu)["KOD"] + " - " + OperacjeNaTabelach.operacjeOgolneSlowniki.getDaneSlownikowe(
  //      //        dsSL, typElementu, idElementu)["NAZWA"]);
  //      //    if (idKolor != null && (int)idKolor > 0)
  //      //        strBld.AppendLine("Kolor:  " + OperacjeNaTabelach.operacjeSL_KOLOR.getNazwa(
  //      //     dsSL.SL_KOLOR, (int)idKolor));
  //      //}

  //      //private static List<Dostawa> znajdzWszystkieDostawyDlaTowarow(InsERT.Subiekt SGT, List<int> idTW)
  //      //{
  //      //    try
  //      //    {
  //      //        List<Dostawa> listaDost = new List<Dostawa>();

  //      //        foreach (var itemTw in idTW)
  //      //        {
  //      //            List<Dostawa> listaDostTmp = podajDostawy(SGT, itemTw, idMagazynuDlaRW);

  //      //            if (listaDostTmp != null && listaDostTmp.Count > 0)
  //      //                listaDost.AddRange(listaDostTmp);
  //      //        }

  //      //        ///lista posortowana po dacie
  //      //        if (listaDost.Count > 0)
  //      //        {
  //      //            listaDost = listaDost.OrderBy(x => x.DataPrzyjecia).ToList();
  //      //            return listaDost;
  //      //        }
  //      //        else
  //      //            return null;
  //      //    }
  //      //    catch (Exception ex)
  //      //    {
  //      //        return null;
  //      //    }
  //      //}

  //      //public class Dostawa
  //      //{
  //      //    int idTw;

  //      //    public int IdTw
  //      //    {
  //      //        get { return idTw; }
  //      //        set { idTw = value; }
  //      //    }

  //      //    DateTime dataPrzyjecia;

  //      //    public DateTime DataPrzyjecia
  //      //    {
  //      //        get { return dataPrzyjecia; }
  //      //        set { dataPrzyjecia = value; }
  //      //    }

  //      //}

  //      //public static List<IHP_DOSTAWA> podajDostawy(InsERT.Subiekt SGT, int idTowaru, int idMag)
  //      //{
  //      //    try
  //      //    {
  //      //        if (SGT != null)
  //      //        {
  //      //            InsERT.SuDostawy oDostawy;

  //      //            oDostawy = SGT.SuDokumentyManager.PodajDostepneDostawy((int)idTowaru, (int)idMag, (DateTime)Convert.ToDateTime(DateTime.Now.ToLongDateString()));

  //      //            List<IHP_DOSTAWA> listaDostaw = new List<IHP_DOSTAWA>();

  //      //            foreach (InsERT.SuDostawa dost in oDostawy.Dostawy)
  //      //            {
  //      //                listaDostaw.Add(new IHP_DOSTAWA()
  //      //                {
  //      //                    DATADOSTAWY = dost.DataPrzyjecia,
  //      //                    IdTw = idTowaru
  //      //                });
  //      //            }

  //      //            return listaDostaw;
  //      //        }
  //      //        else
  //      //            return null;

  //      //    }
  //      //    catch (Exception ex)
  //      //    {
  //      //        return null;
  //      //    }
  //      //}
 
   }


}
