using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Repository
{ 

    public interface IDokumentyRepository : IGenericRepository2E<IHP_NAGLDOK,IHP_POZDOK>
    {
        void AddPoz(IHP_POZDOK entity);
        void DelPoz(IHP_POZDOK entity);
        void DelPozRange(List<IHP_POZDOK> entitys);

    }

    public class DokumentyRepository: GenericRepository2E<KOMPLETACJAEntities, IHP_NAGLDOK, IHP_POZDOK>, IDokumentyRepository, INotifyPropertyChanged
    {
        RejWagaMessage rejmsg;
 
        protected IHP_NAGLDOK _dokument;

      private string nrdokwew;
        private decimal _waga;
        private decimal _sumanagl;
        public Decimal SumaNagl
        {
            get
            {
                return _sumanagl;
            }
            set
            {
                _sumanagl = value;
                OnRisePropertyChanged("SumaNagl");
            }
        }
        protected void OnRisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public decimal Waga
        {
            get
            {
                return _waga;
            }
            set
            {
                _waga = value;
                OnRisePropertyChanged("Waga");
            }
        }
       public DokumentyRepository()
        {

        }
        public IEnumerable<IHP_NAGLDOK> List
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Add(IHP_NAGLDOK entity)
        {
            try
            {
                Context.IHP_NAGLDOK.Add(entity);
                Context.SaveChanges();
            }
    
           catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
            }
        }
        public void Delete(IHP_NAGLDOK entity)
        {
            Context.IHP_NAGLDOK.Remove(entity);
            Context.SaveChanges();
        } 
         public IHP_NAGLDOK FindById(int Id)
        {
            throw new NotImplementedException();
        }
        public void Update(IHP_NAGLDOK entity)
        {
            throw new NotImplementedException();
        }
 
        public void SaveAll()
        {
            //string LastMessage;
            //DateTime _datawjazd = DateTime.Now;
            //DateTime _datawyjazd = DateTime.Now;
            //decimal suma = 0;

            //var sums = _wazenia
            //            .GroupBy(x => new { x.ID_IHP_KONTRAHENT_ARCH, x.IHP_RODZAJDOK, x.ID_IHP_RODZAJDOK,x.ID_IHP_KARTOTEKA  })
            //            .Select(kkk => new { kkk.Key , kkk.Key.IHP_RODZAJDOK,  kkk.Key.ID_IHP_RODZAJDOK, kkk.Key.ID_IHP_KARTOTEKA, suma = kkk.Sum( a => a.WAGA) });
            //try
            //{
            //  foreach (var oo in sums)
            //  {
            //    int k = Convert.ToInt32(oo.Key.ID_IHP_KONTRAHENT_ARCH);
            //    int r = Convert.ToInt32(oo.ID_IHP_RODZAJDOK);
            //        int idKartoteka = Convert.ToInt32(oo.ID_IHP_KARTOTEKA);
            //        //int IdKontrahent = Convert.ToInt32(oo.ID_IHP_KONTRAHENT);
            //        int IdKontrahent = 1;
             
            //    IHP_NUMERACJA numerdok = GetId(8);
            //     if (numerdok != null)
            //         numerdok.NUMER++;

            //        nrdokwew = GetNumerDok(numerdok.NUMER);
           
            //        IHP_NAGLDOK dokument = new IHP_NAGLDOK()
            //        {
            //            ID_IHP_NAGLDOK = numerdok.NUMER,
            //            DATADOK = DateTime.Today,
            //            NRDOK = numerdok.NUMER,
            //            NRDOKWEW = nrdokwew,
            //            SUMAOG = suma,
            //            ID_DEFDOK = r,
            //            ID_IHP_KONTRAHENT_ARCH = k,
            //            ID_IHP_KONTRAHENT = IdKontrahent,
            //            ID_RODZAJDOK = r,
            //            ID_KH_SUBJECT = 0,
            //            //IHP_KONTRAHENT_ARCH = oo.IHP_KONTRAHENT_ARCH,
            //            //IHP_RODZAJDOK = oo.IHP_RODZAJDOK,
                        
            //            IHP_KONTRAHENT_ARCH = null,
            //            IHP_RODZAJDOK = null,
                   
            //        };


            //        context.IHP_NAGLDOK.Add(dokument);
            //        int _numerpoz = 0;
            //        IHP_NUMERACJA numerpoz = GetId(9);
            //        if (numerpoz != null)
            //            _numerpoz = numerpoz.NUMER;
            //        int _numerpowiazania = 0;
            //        IHP_NUMERACJA numerpowiazania = GetId(12);
            //        if (numerpowiazania != null)
            //            _numerpowiazania = numerpowiazania.NUMER;

            //        foreach (IHP_WAZENIE_EX item in _wazenia.Where(x=>x.ID_IHP_KONTRAHENT_ARCH == k  || x.ID_IHP_RODZAJDOK== r || x.ID_IHP_KARTOTEKA== idKartoteka))
            //         {
            //          short cenaust = 1;
            //             IHP_POZDOK poz = new IHP_POZDOK()
            //             {
            //                ID_IHP_POZDOK = _numerpoz,
            //                ID_IHP_KARTOTEKA = item.ID_IHP_KARTOTEKA,
            //                ID_IHP_NAGLDOK = numerdok.NUMER,
            //                LP = 1,
            //                ILOSC = item.WAGA,
            //                NAZWASKRPOZ = item.IHP_KARTOTEKA.NAZWASKR,
            //                CENANETTO = 1,
            //                CENABRUTTO = 1,
            //                WARTVAT = 1,
            //                WARTNETTO = 1,
            //                WARTBRUTTO = 1,
            //                DATADOK = DateTime.Today,
            //                CENAUSTALONA = cenaust,
            //                UWAGI = string.Empty,
            //                ID_TOWSUBJECT = 0
            //            };
            //            context.IHP_POZDOK.Add(poz);
            //            context.SaveChanges();
            //            //powiazanie 

            //            IHP_POWIAZANIE_WAGA powiazanie = new IHP_POWIAZANIE_WAGA()
            //            {
            //                ID_IHP_POWIAZANIE = _numerpowiazania,
            //                ID_IHP_WAZENIE = item.ID_IHP_WAZENIE,
            //                ID_IHP_POZDOK = _numerpoz,
            //                IHP_POZDOK = poz
            //            };

            //           IHP_WAZENIE wazenia = context.IHP_WAZENIE?.FirstOrDefault(x => x.ID_IHP_WAZENIE == item.ID_IHP_WAZENIE); 
            //                 if(wazenia!=null)
            //                    {
            //                         wazenia.STATUS = 3;
            //                         context.Entry(wazenia).State = EntityState.Modified;
            //                         context.SaveChanges();
            //                    }
               
            //            context.IHP_POWIAZANIE_WAGA.Add(powiazanie);
            //            context.SaveChanges();
            //            _numerpoz++;
            //            _numerpowiazania++;
            //        }

            //        if (numerpowiazania != null)
            //                  numerpowiazania.NUMER = _numerpowiazania;

            //        context.IHP_NUMERACJA.Add(numerdok);
            //        context.Entry(numerdok).State = EntityState.Modified;

            //        if (numerpoz != null)
            //             numerpoz.NUMER = _numerpoz;
            //        context.IHP_NUMERACJA.Add(numerpoz);

            //        context.Entry(numerpoz).State = EntityState.Modified;

            //        context.SaveChanges();

            //        Messenger.Default.Send<int>(3);
         
            //        }
            // }
            //catch (DbUpdateException ex)
            //{
            //    LastMessage = ex.ToString();
            //    if (LastMessage == String.Empty)
            //        LastMessage = ex.InnerException.ToString();
            //    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
           
            //}
            

            //catch (Exception ex)
            //{
            //    LastMessage = ex.ToString();
            //    if (LastMessage == String.Empty)
            //        LastMessage = ex.InnerException.ToString();
            //    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            //    throw ex;
            //}
        }
    
        public int GenNumerDok()
        {
            int nr = 0;
            //dla pierwszego 
            if (Context.IHP_NAGLDOK.Count() >0)
            {
                nr = Context.IHP_NAGLDOK.Max(x => x.NRDOK);
         
            }
            nr++;
           return nr;
        }
        public string GenNumerDokWew()
        {
            int nr = GetNextNumer(6);
            return "ZAM/" + nr.ToString();
        }

 
        public bool CheckExistsByID(int ID_IHP_GRUPAKART)
        {
            return false;
        }
        public bool CheckWystGrKartExists(IHP_GRUPAKART GrKart)
        {
            return false;
        }

        public void AddPoz(IHP_POZDOK entity)
        {
            Context.IHP_POZDOK.Add(entity);
            Context.SaveChanges();
        }

        public bool CheckExists(IHP_POZDOK poz)
        {
            return false;
        }

        public void DelPoz(IHP_POZDOK entity)
        {
            Context.IHP_POZDOK.Remove(entity);
            Context.SaveChanges();
        }

        public void DelPozRange(List<IHP_POZDOK> entitys)
        {
            Context.IHP_POZDOK.RemoveRange(entitys);
            Context.SaveChanges();
        }
    }
}
