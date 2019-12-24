using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Repository
{
    public interface IZamowieniaListaRepository : IGenericRepository2E<IHP_NAGLDOK, IHP_POZDOK>
    {

        IHP_NAGLDOK GetSingle(int barId);
        List<IHP_NAGLDOK> GetZamByIdTrasa(int IdTrasa, DateTime Termin);
        List<IHP_POZDOK> GetPozByNagl(int idNagl);
        
      
    

    }
    public  class ZamowieniaListaRepository : GenericRepository2E<KOMPLETACJAEntities, IHP_NAGLDOK,IHP_POZDOK>, IZamowieniaListaRepository,INotifyPropertyChanged
    {
         private IHP_JM _jm;
        public IHP_JM JM
        {
            get
            {
                return _jm;
            }

            set
            {
                _jm = value;
                OnRisePropertyChanged("JM");
            }
        }

    
        private IHP_JZ _jz;
        public IHP_JZ Jz
        {
            get
            {
                return _jz;
            }
            set
            {
                _jz = value;
                OnRisePropertyChanged("Jz");
            }
        }

       string LastMessage;
        RejWagaMessage rejmsg;
      protected void OnRisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

      public event PropertyChangedEventHandler PropertyChanged;


        #region Jednosti miary
        public void AddJm(IHP_JM _jm)
        {
            IHP_JM wjz = new IHP_JM()
            {
                ID_IHP_JM = GetNextNumer(20),
                JM = _jm.JM,
                OPISJM = _jm.OPISJM   
          };

            try
            {
                if (_jz != null)
                {
                    Context.IHP_JM.Add(wjz);
                    Context.SaveChanges();
                }

            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }

            if (_jm!=null)
            {
                Context.IHP_JM.Add(JM);
                Context.SaveChanges();
            }
        }
        public void DelJm(IHP_JM _jm)
        {
            if (JM != null)
            {
                Context.IHP_JM.Remove(_jm);
                Context.SaveChanges();
            }
        }
        public void UpdateJm(IHP_JM _jm)
        {
            string LastMessage;
            try
            {
                if (_jz.AKTYWNA == 1)
                    ClearAktywny();
                IHP_JM _jzlocal = Context.IHP_JM.FirstOrDefault(x => x.ID_IHP_JM == _jm.ID_IHP_JM);
                _jzlocal.IHP_KARTOTEKA = _jm.IHP_KARTOTEKA;
                _jzlocal.JM = _jm.JM;
                _jzlocal.OPISJM = _jm.OPISJM;

                Context.IHP_JM.Attach(_jzlocal);
                Context.Entry(_jzlocal).State = EntityState.Modified;
                Context.SaveChanges();


            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public IEnumerable<IHP_JM> JmGetAll()
        {
           return Context.IHP_JM.ToList();
        }
        #endregion
        public IHP_JM JmById(int ID_IHP_JM)
        { 
            return Context.IHP_JM.FirstOrDefault(x => x.ID_IHP_JM == ID_IHP_JM);
        }
        public bool CheckExistsBySkrot(string shortcutname)
        {
            IHP_JM rr = Context.IHP_JM?.FirstOrDefault(x => x.JM == shortcutname);
            return (rr != null);
        }
        public ZamowieniaListaRepository()
        {
    
        }
        public IEnumerable<IHP_CENNIK> List
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public void Add()
        {
            throw new NotImplementedException();
        }
        public void Delete()
        {
            throw new NotImplementedException();
        }
        public List<WystJednDodatView> GetWstyJednDodatAll(int IdKartoteka)
        {
            List<WystJednDodatView> ret = new List<WystJednDodatView>();
            try
            { 
            var _listjedndodat = Context.Database.SqlQuery<WystJednDodatView>(String.Format("select  W.ID_IHP_WYST_JZ , W.ID_IHP_JZ  ,W.ID_KARTOTEKA, J.NAZWA, J.WARTOSC , W.AKTYWNA   from  IHP_WYST_JZ W  join IHP_JZ J on J.ID_IHP_JZ = W.ID_IHP_JZ  where W.ID_KARTOTEKA ={0}", IdKartoteka.ToString())).ToList();

            foreach (WystJednDodatView item in _listjedndodat)
                  {
                     ret.Add(item);
                 }
            }
            catch (DbUpdateException ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
            return ret;
        }
        public void SaveWyJz(WystJednDodatView _jz)
        {
            string LastMessage;

            if (_jz.AKTYWNA == 1)
                    ClearAktywnyWyst(_jz.ID_KARTOTEKA);

            IHP_WYST_JZ wjz = new IHP_WYST_JZ()
            {
                ID_IHP_WYST_JZ = GetNextNumer(1),
                ID_IHP_JZ = _jz.ID_IHP_JZ,
                ID_KARTOTEKA = _jz.ID_KARTOTEKA,
                AKTYWNA = _jz.AKTYWNA
                
            };

            try
            {
                if (_jz != null)
                {
                    Context.IHP_WYST_JZ.Add(wjz);
                    Context.SaveChanges();
                }

            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public void DelWyJz(int IDWystJz)
        {
            string LastMessage;

            IHP_WYST_JZ wjz = Context.IHP_WYST_JZ.FirstOrDefault(x => x.ID_IHP_WYST_JZ == IDWystJz);
        

            try
            {
                if (wjz != null)
                {
                    Context.IHP_WYST_JZ.Remove(wjz);
                    Context.SaveChanges();
                }

            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public List<IHP_JZ> GetAll()
        {
            List <IHP_JZ>  res = null;
            try
            {
                res = Context.IHP_JZ.ToList();
            }
            catch (DbUpdateException ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
            return res;
        }
        public IHP_JZ FindByJz(int IdKart,int idDefJz)
        {
            IHP_WYST_JZ wysz = null;
            IHP_JZ res = null;
            try
            {
                wysz = Context.IHP_WYST_JZ?.FirstOrDefault(x => x.ID_IHP_JZ == idDefJz && x.ID_KARTOTEKA == IdKart);
                if(wysz!=null)
                {
                    res = Context.IHP_JZ?.FirstOrDefault(x => x.ID_IHP_JZ == wysz.ID_IHP_JZ);
                }
            }
            catch (DbUpdateException ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
           catch (Exception ex)
             {
                 LastMessage = ex.ToString();
                 if (LastMessage == String.Empty)
                     LastMessage = ex.InnerException.ToString();
                 LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
            return res;
       }
        public void Save()
        {
          string LastMessage;
                try
                {
           

                }

              catch (Exception ex)
                {
                    LastMessage = ex.ToString();
                    if (LastMessage == String.Empty)
                        LastMessage = ex.InnerException.ToString();
                    LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                    throw ex;
                }
            }
         public void ClearAktywnyWyst(int IdKArtoetka)
        {
            string LastMessage;
            try
            {
                List<IHP_WYST_JZ> getAktywne = Context.IHP_WYST_JZ.Where(x => x.AKTYWNA == 1 && x.ID_KARTOTEKA == IdKArtoetka).ToList();

                foreach (IHP_WYST_JZ item in getAktywne)
                {
                    item.AKTYWNA = 0;
                    Context.IHP_WYST_JZ.Attach(item);
                    Context.Entry(item).State = EntityState.Modified;
                    Context.SaveChanges();
                }
            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public void ClearAktywny()
        {
            string LastMessage;
            try
            {
                List<IHP_JZ> getAktywne = Context.IHP_JZ.Where(x => x.AKTYWNA == 1).ToList();

                foreach(IHP_JZ item in getAktywne)
                {
                    item.AKTYWNA = 0;
                    Context.IHP_JZ.Attach(item);
                    Context.Entry(item).State = EntityState.Modified;
                    Context.SaveChanges();
                } 
           }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
         public void SaveWystGrKart(int  Idkart, int idgrkart)
        {
            IHP_GRUPAKART kk = Context.IHP_GRUPAKART?.FirstOrDefault(x => x.ID_IHP_GRUPAKART == idgrkart);
            IHP_WYSTGRKART newitem = new IHP_WYSTGRKART()
            {
                
                ID_IHP_WYSTGRKART = GetNextNumer(21),
                ID_IHP_GRUPAKART = idgrkart,
                ID_KARTOTEKA = Idkart,
                IHP_GRUPAKART = kk
            };
            Context.IHP_WYSTGRKART.Add(newitem);
            Context.SaveChanges();
        }
        public void DeleteWystGrKart(IHP_WYSTGRKART item )
        {
         string LastMessage;
            try
            {
                Context.IHP_WYSTGRKART.Remove(item);
                Context.SaveChanges();
            }

            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }
        public IEnumerable<IHP_WYSTGRKART> getwystgrkartall()
        {
            return Context.IHP_WYSTGRKART.ToList();
        }
        public List<IHP_WYSTGRKART> getwystgrkartallbyKart(int idKart)
        {
            return Context.IHP_WYSTGRKART.Where(x => x.ID_KARTOTEKA == idKart).ToList();
        }
         public void SaveRangeCennik(List<Cennik> items)
        {
            //string LastMessage;
            //DateTime _datawjazd = DateTime.Now;
            //DateTime _datawyjazd = DateTime.Now;
            //decimal suma = 0;
 
            //try
            //{
         
         
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
         public IHP_KARTOTEKA GetSingle(int kartId)
        {
            var query = Context.IHP_KARTOTEKA.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == kartId);
            return query;
        }
        public List<IHP_NAGLDOK> GetZamByIdTrasa(int IdTrasa,DateTime Termin)
        {
            List<IHP_NAGLDOK> res = new List<IHP_NAGLDOK>();
                   var lista = Context.IHP_NAGLDOK.Where(x=>x.TERMINREALIZ== Termin)
                                       .Join(Context.IHP_WYSTTRASAKONTRAH.Where(y=>y.ID_IHP_TRASY == IdTrasa), x => x.ID_IHP_KONTRAHENT, g => g.ID_IHP_KONTRAHENT,
                                          (x, g) => x);

                           foreach (IHP_NAGLDOK item in lista)
                                  res.Add(item);
            return res;
        }
        IHP_NAGLDOK IZamowieniaListaRepository.GetSingle(int barId)
        {
            throw new NotImplementedException();
        }

        public List<IHP_POZDOK> GetPozByNagl(int idNagl)
        {
            return Context.IHP_POZDOK.Where(x => x.ID_IHP_NAGLDOK == idNagl).ToList();
        }
    }
    }
 
