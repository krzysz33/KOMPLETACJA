using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace KpInfohelp.Repository
{

    public interface IJCennikiRepository : IGenericRepository2E<IHP_CENNIK,IHP_DEFCENY>
    {

      
        List<IHP_CENNIK> GetAll();
        IHP_CENNIK CennikById(int ID_IHP_CENNIK);
        List<IHP_CENNIK> CheckExistsByIdDefCeny(int IHP_DEFCENY);
        List<IHP_CENNIK> GetByIdKartoteka(int ID_IHP_KARTOTEKA);
        bool CheckExistsBySkrot(string shortcutname);
        bool CheckExistsByID(int ID_IHP_CENNIK);
        bool CheckExists(IHP_CENNIK Cennik);
        
    }
    
    public  class CennikiRepository : GenericRepository<KOMPLETACJAEntities, IHP_CENNIK>, IJCennikiRepository, INotifyPropertyChanged
    {
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
 
        public CennikiRepository()
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

       public List<IHP_CENNIK> GetAll()
        {
            return Context.IHP_CENNIK.ToList();
        }

        public IHP_CENNIK FindByIdKart(int IdKart,int idDefCeny)
        {
            IHP_CENNIK res = null;
            try
            {
              res = Context.IHP_CENNIK?.FirstOrDefault(x => x.ID_IHP_DEFCENY == idDefCeny && x.ID_IHP_KARTOTEKA == IdKart);
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

        private List<IHP_CENNIK> GetByIdDefDeny(int idDefCeny)
        {
            return Context.IHP_CENNIK.Where(x => x.ID_IHP_DEFCENY == idDefCeny).ToList();
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

        public IHP_CENNIK CennikById(int ID_IHP_CENNIK)
        {
            return Context.IHP_CENNIK?.FirstOrDefault(x => x.ID_IHP_CENNIK == ID_IHP_CENNIK) ?? null;
        }

        public bool CheckExistsBySkrot(string shortcutname)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistsByID(int ID_IHP_CENNIK)
        {
            return Context.IHP_CENNIK?.Any(x => x.ID_IHP_CENNIK == ID_IHP_CENNIK) ?? false;
        }

        public bool CheckExists(IHP_CENNIK Cennik)
        {
            return Context.IHP_CENNIK?.Any(x => x.ID_IHP_CENNIK == Cennik.ID_IHP_CENNIK) ?? false;
        }
    
        public List<IHP_CENNIK> CheckExistsByIdDefCeny(int IHP_DEFCENY)
        {
            return Context.IHP_CENNIK?.Where(x => x.ID_IHP_DEFCENY == IHP_DEFCENY).ToList() ?? null;
        }

        public List<IHP_DEFCENY> GetAll2()
        {
            return Context.IHP_DEFCENY.ToList();
        }

        public IQueryable<IHP_DEFCENY> FindBy2(Expression<Func<IHP_DEFCENY, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add2(IHP_DEFCENY entity)
        {
            throw new NotImplementedException();
        }

        public void Delete2(IHP_DEFCENY entity)
        {
        }
      

        public void Edit2(IHP_DEFCENY entity)
        {
            throw new NotImplementedException();
        }

        public List<IHP_CENNIK> GetByIdKartoteka(int ID_IHP_KARTOTEKA)
        {
            return Context.IHP_CENNIK?.Where(x => x.ID_IHP_KARTOTEKA == ID_IHP_KARTOTEKA).ToList() ?? null;
        }

        public void Update2(IHP_DEFCENY entity)
        {
            throw new NotImplementedException();
        }
    }
}
