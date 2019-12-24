using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Repository
{

    public interface IGenericRepository<T> where T : class
    {

        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void Save();
    }
    public abstract class GenericRepository<C, T> : ViewModelBase,
      IGenericRepository<T> where T : class where C : KOMPLETACJAEntities, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query;
        }
        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }
        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }
        public virtual void AddAndSave(T entity)
        {
            _entities.Set<T>().Add(entity);
            _entities.SaveChanges();
        }
        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            _entities.SaveChanges();
        }




        public virtual int GetNextNumer(int iddlatabeli)
        {
            int _statushistId = 0;
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = _entities.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == iddlatabeli);
                numerpoz.NUMER++;
                _statushistId = numerpoz.NUMER;
                //    _entities.IHP_NUMERACJA.Add(numerpoz);
                _entities.Entry(numerpoz).State = EntityState.Modified;
                _entities.SaveChanges();
            }

            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            return _statushistId;
        }


    }



    public interface IGenericRepository2E<T, K> where T : class where K : class
    {

        IQueryable<T> GetAll();
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);

        List<K> GetAll2();
        IQueryable<K> FindBy2(Expression<Func<K, bool>> predicate);
        void Add2(K entity);
        void Delete2(K entity);
        void Edit2(K entity);

        void Update2(K entity);

        void Save();
    }
    public abstract class GenericRepository2E<C, T, K> : ViewModelBase,
      IGenericRepository2E<T, K> where T : class where K : class where C : KOMPLETACJAEntities, new()
    {

        private C _entities = new C();
        public C Context
        {

            get { return _entities; }
            set { _entities = value; }
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = _entities.Set<T>();
            return query;
        }
        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = _entities.Set<T>().Where(predicate);
            return query;
        }
        public virtual void Add(T entity)
        {
            _entities.Set<T>().Add(entity);
        }
        public virtual void AddAndSave(T entity)
        {
            _entities.Set<T>().Add(entity);
            _entities.SaveChanges();
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(T entity)
        {
            _entities.Set<T>().Remove(entity);
        }
        public virtual List<K> GetAll2()
        {

            List<K> query = _entities.Set<K>().ToList();
            return query;
        }
        public IQueryable<K> FindBy2(System.Linq.Expressions.Expression<Func<K, bool>> predicate)
        {

            IQueryable<K> query = _entities.Set<K>().Where(predicate);
            return query;
        }
        public virtual void Add2(K entity)
        {
            _entities.Set<K>().Add(entity);
        }
        public virtual void AddAndSave2(K entity)
        {
            _entities.Set<K>().Add(entity);
            _entities.SaveChanges();
        }
        public virtual void Delete2(K entity)
        {
            _entities.Set<K>().Remove(entity);
        }
        public virtual void Edit2(K entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Save()
        {
            _entities.SaveChanges();
        }
        public virtual int GetNextNumer(int iddlatabeli)
        {
            int _statushistId = 0;
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = _entities.IHP_NUMERACJA.FirstOrDefault(x => x.ID_TABELA == iddlatabeli);
                numerpoz.NUMER++;
                _statushistId = numerpoz.NUMER;
                //    _entities.IHP_NUMERACJA.Add(numerpoz);
                _entities.Entry(numerpoz).State = EntityState.Modified;
                _entities.SaveChanges();
            }

            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            return _statushistId;
        }
        public void Update2(K entity)
        {
            _entities.Set<K>().Attach(entity);
            _entities.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}
  
