using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KpInfohelp
{
    public class RejestrRepository : CrudVMBase, INotifyPropertyChanged
    {
        private ObservableCollection<int> _selected;
        private int _idNagl;
        private int _statushistId = 0;
        private List<IHP_STATUSHISTORIA> Lista;
        public int IdNagl { get; set; }
        public ObservableCollection<ZamowieniaViewStatusNagl> ZamowieniaViewStatusLstNagl
        {
            get;
            set;
        }
        private ObservableCollection<ZamowieniaViewLista> _zamowieniaviewlistalst;
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public RejestrRepository(){
            _selected = new ObservableCollection<int>();
        }
        private int getLastStatus(int IdPoz)
        {
            int res = 0;
            List<IHP_STATUSHISTORIA> stathists = context.IHP_STATUSHISTORIA.Where(x => x.ID_IHP_POZ == IdPoz).ToList();
            if (stathists.Count > 0)
            {
                IHP_STATUSHISTORIA stathist = stathists.LastOrDefault();
                if (stathist != null)
                    res = stathist.ID_IHP_DEFSTATUS;
            }
            return res;
        }
        public RejestrRepository(int IdNagl)
        {
            _idNagl = IdNagl;
 
        }
        protected async override void GetData()
        {
            ThrobberVisible = Visibility.Visible;
            ZamowieniaViewStatusLstNagl = new ObservableCollection<ZamowieniaViewStatusNagl>();
            var zamowienia = context.Database.SqlQuery<ZamowieniaViewStatusNagl>(string.Format(@"select K.INDEKS, SH.ID_STATUSHISTORIA, SH.ID_DEFSTATUS, SH.DATAWPISU, DS.NAZWA, SH.OPIS, SH.ID_POZ, AZ.ID_ARIT_ZAM_USERS,
                  SH.id_defstatus, DS.nazwa as  NAZWAZ, AZ.login as UZYTKOWNIK  from NAGL N  join POZ P on P.id_nagl = n.id_nagl
                    join KARTOTEKA K on k.id_kartoteka = p.id_kartoteka  join statushistoria SH on SH.id_poz = P.id_poz
                    join DEFSTATUS DS on DS.id_defstatus = SH.id_defstatus
                    join arit_zam_users az on az.id_arit_zam_users = sh.id_arit_zam_users
                  where n.id_nagl ={0}", _idNagl.ToString())).ToList();

            ZamowieniaViewStatusLstNagl.Clear();
            foreach (var q in zamowienia)
            {
                ZamowieniaViewStatusNagl zv = new ZamowieniaViewStatusNagl();
                zv.INDEKS = q.INDEKS;
                zv.DATAWPISU = q.DATAWPISU;
                zv.NAZWA = q.NAZWA;
                zv.NAZWAZ = q.NAZWAZ;
                zv.UZYTKOWNIK = q.UZYTKOWNIK;
                ZamowieniaViewStatusLstNagl.Add(zv);
            }
        }
        private ObservableCollection<ViewStatusy> _listastatus;
        public ObservableCollection<ViewStatusy> ListaStatus
        {
            get
            {
                return _listastatus;
            }
            set
            {
                _listastatus = value;
                RisePropertyChanged("ListaStatus");
            }
        }
        private ObservableCollection<ViewStatusy> _selectedtllist;
        public ObservableCollection<ViewStatusy> SelectedTLList
        {
            get
            {
                return _selectedtllist;
            }
            set
            {
                _selectedtllist = value;
                RisePropertyChanged("SelectedTLList");
            }
        }
        private int GetIdStatusHist()
        {
            string LastMessage = string.Empty;
            try
            {
                IHP_NUMERACJA numerpoz = GetId(4);
                numerpoz.NUMER++;
                _statushistId = numerpoz.NUMER;
                context.IHP_NUMERACJA.Add(numerpoz);
                context.Entry(numerpoz).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (LastMessage == String.Empty)
                    LastMessage = ex.InnerException.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
            }
            return _statushistId;

        }
        public void SaveStatus(int IdPoz,int StatusNew)
        {
            int StatusOld = getLastStatus(IdPoz);
           // ViewModelKafelkiOkno
            string LastMessage = string.Empty;
            try
            {
                IHP_DEFSTATUS stat = context.IHP_DEFSTATUS.First(x => x.ID_IHP_DEFSTATUS == StatusNew);
          
                    IHP_POZDOK p = context.IHP_POZDOK.Find(IdPoz);
                    if (p != null)
                    {
                        p.ID_IHP_DEFSTATUS = stat.ID_IHP_DEFSTATUS;
                        context.IHP_POZDOK.Add(p);
                        context.Entry(p).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                IHP_STATUSHISTORIA stathist = new IHP_STATUSHISTORIA()
                    {
                    ID_IHP_STATUSHISTORIA = GetIdStatusHist(),
                    ID_IHP_DEFSTATUS = stat.ID_IHP_DEFSTATUS,
                    ID_IHP_DEFSTATUSZ = StatusOld,
                      //  DATAWPISU = DateTime.Now,
                        OPIS = stat.NAZWA,
                    ID_IHP_POZ = IdPoz,
                        ID_ARIT_ZAM_USERS = ProgramDataSotrage.User.ID_IHP_ZAM_USERS
                    };

                   context.IHP_STATUSHISTORIA.Add(stathist);
                    //ZamowieniaViewListaLst.FirstOrDefault(x => x.ID_POZ == _zamwoienieselected.ID_POZ).ID_DEFSTATUS = stat.ID_DEFSTATUS;
                    //ZamowieniaViewListaLst.Where(x => x.ID_POZ == _zamwoienieselected.ID_POZ).FirstOrDefault().STATUS = stat.NAZWA;
                  context.SaveChanges();
                 
                 
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                 throw ex;
            }
        }
        public bool CheckExistWyciete(int IdPoz)
        {
            return true;
        }
        public bool CheckExistOnlyWpisane(int IdPoz)
        {
            return true;
    //       return Lista.Any(x => x.ID_POZ == IdPoz && x.ID_DEFSTATUS== ProgramDataSotrage.xmlSqlConfig.IdentStatusWpisane) && !Lista.Any(x => x.ID_POZ == IdPoz && x.ID_DEFSTATUS == ProgramDataSotrage.xmlSqlConfig.IdentStatusWyciete);
        }
    }
}
