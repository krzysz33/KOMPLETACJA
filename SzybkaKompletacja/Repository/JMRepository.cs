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

    public interface IJMRepository : IGenericRepository<IHP_JM>
    {

        IHP_JM GetSingle(int barId);
        List<IHP_JM> GetAll();
        IHP_JM JmById(int ID_IHP_JM);
          bool CheckExistsBySkrot(string shortcutname);
        bool CheckExistsByID(int ID_IHP_JM);
        bool CheckExists(IHP_JM Jm);
  
    }
    
    public  class JMRepository : GenericRepository<KOMPLETACJAEntities, IHP_JM>, IJMRepository, INotifyPropertyChanged
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
          try
            {
                if (_jm != null)
                {
                    Context.IHP_JM.Add(_jm);
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
        public void DelJm(IHP_JM _jm)
        {
            if (_jm != null)
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

        public JMRepository()
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
      
         public override  void Save()
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
  
   
        IHP_JM IJMRepository.GetSingle(int barId)
        {
            throw new NotImplementedException();
        }
        List<IHP_JM> IJMRepository.GetAll()
        {
            throw new NotImplementedException();
        }
        public bool CheckExistsBySkrot(string shortcutname)
        {
            throw new NotImplementedException();
        }

        public bool CheckExistsByID(int ID_IHP_JM)
        {
            return Context.IHP_JM.Any(x => x.ID_IHP_JM == ID_IHP_JM);
        }

        public bool CheckExists(IHP_JM Jm)
        {
            return Context.IHP_KARTOTEKA.Any(x => x.ID_IHP_JM == Jm.ID_IHP_JM);
        }
    }
    }
 
