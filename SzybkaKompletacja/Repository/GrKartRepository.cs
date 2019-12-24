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

    public interface IGrKartRepository : IGenericRepository<IHP_GRUPAKART>
    {

           List<IHP_GRUPAKART> GetAllByIdRoz(int ID_IHP_RODZGRUPKART);
        bool CheckExistsByID(int ID_IHP_GRUPAKART);
        bool CheckExists(IHP_GRUPAKART GrKart);
        bool CheckWystGrKartExists(IHP_GRUPAKART GrKart);

    }
    
    public  class GrKartRepository : GenericRepository<KOMPLETACJAEntities, IHP_GRUPAKART>, IGrKartRepository, INotifyPropertyChanged
    {
        
        private IHP_GRUPAKART _grkart;
        public IHP_GRUPAKART GrKart
        {
            get
            {
                return _grkart;
            }
            set
            {
                _grkart = value;
                OnRisePropertyChanged("GrKart");
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

        public List<IHP_GRUPAKART> GetAllByIdRoz(int ID_IHP_RODZGRUPKART)
        {
            return Context.IHP_GRUPAKART.Where(x => x.ID_IHP_RODZGRUPKART == ID_IHP_RODZGRUPKART).ToList();
        }

        public bool CheckExistsByID(int ID_IHP_GRUPAKART)
        {
            throw new NotImplementedException();
        }

        public bool CheckExists(IHP_GRUPAKART GrKart)
        {
            return Context.IHP_GRUPAKART.Any(x => x == GrKart);
        }

        public bool CheckWystGrKartExists(IHP_GRUPAKART GrKart)
        {
            throw new NotImplementedException();
        }

        IQueryable<IHP_GRUPAKART> IGenericRepository<IHP_GRUPAKART>.GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<IHP_GRUPAKART> FindBy(Expression<Func<IHP_GRUPAKART, bool>> predicate)
        {
            throw new NotImplementedException();
        }

 

        public event PropertyChangedEventHandler PropertyChanged;

     public GrKartRepository() { }
   

   
    }
  }
 
