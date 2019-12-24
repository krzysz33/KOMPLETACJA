using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using DevExpress.Mvvm;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading;
using DevExpress.Xpf.Printing;
using System.Windows;
using KpInfohelp.Repository;
using System.Globalization;
using DevExpress.Xpf.Docking;
using DevExpress.Xpf.Grid;
using System.IO;
using System.Windows.Controls;
using Microsoft.Win32;
using DevExpress.Xpf.Core;
using System.Windows.Markup;
using System.Windows.Data;

namespace KpInfohelp
{
     public class ViewModelJM : CrudVMBase, INotifyPropertyChanged, IMVVMDockingProperties, IDataErrorInfo
    {

        JMRepository jmr = new JMRepository();

        #region Property Pol

        private bool _isclosed = true;
        public bool IsClosed
        {
            get
            {
                return _isclosed;
            }

            set
            {
                _isclosed = value;
                RisePropertyChanged("IsClosed");
            }
        }
        public string TargetName
        {
            get { return GetProperty(() => TargetName); }
            set { SetProperty(() => TargetName, value); }
        }
        private IHP_JM _jedmiary;
        public IHP_JM JedMiary
        {
            get
            {
                return _jedmiary;
            }
            set
            {
                NazwaJednMiary = string.Empty;
                SkrotJednMiary = string.Empty;
                _jedmiary = value;
                RisePropertyChanged("JedMiary");
            }
        }

        private string _nazwajednmiary;
        public string NazwaJednMiary
        {

            get
            {
                return _nazwajednmiary;
            }
            set
            {
                _nazwajednmiary = value;
                RisePropertyChanged("NazwaJednMiary");
            }
        }

        private string _skrotjednmiary;
        public string SkrotJednMiary
        {

            get
            {
                return _skrotjednmiary;
            }
            set
            {
                _skrotjednmiary = value;
                RisePropertyChanged("SkrotJednMiary");
            }
        }
          private bool _isaktywny;
        public bool IsAktywny
        {
            get
            {
                return _isaktywny;
            }
            set
            {
                _isaktywny = value;
                RisePropertyChanged("IsAktywny");
            }
        }
        #endregion

        #region Listy

   

        public ObservableCollection<IHP_JM> lstJm { get; private set; }
      
        private ObservableCollection<IHP_JM> lstjednmiary;
        public ObservableCollection<IHP_JM> LstJednMiary
        {
            get
            {
                return lstjednmiary;
            }
            set
            {
                lstjednmiary = value;
                RisePropertyChanged("LstJednMiary");
            }
        }

 

        void InitLists()
        {
  
            LstJednMiary = new ObservableCollection<IHP_JM>(context.IHP_JM);
       
             
        }

        #endregion

        #region Interfejsy i commandy
        IMessageBoxService MessageBoxService { get { return GetService<IMessageBoxService>(ServiceSearchMode.PreferParents); } }
        protected ISaveFileDialogService SaveFileDialogService { get { return this.GetService<ISaveFileDialogService>(); } }
        protected IOpenFileDialgoService OpenFileService { get { return GetService<IOpenFileDialgoService>(); } }
           
        public ICommand _updateCommand
        {
            get;
            set;
        }
        public ICommand _deleteComand
        {
            get;
            set;
        }
        public ICommand SearchByKeyCommand;
        public ICommand  AddCommandJm { get; set; }
        public ICommand ClearCommandJm { get; set; }
        public ICommand UpdateCommandJm { get; set; }
        public ICommand EditCommandJm { get; set; }
        public ICommand DeleteCommandJm { get; set; }
        void InitCommands()
        {
         AddCommandJm = new DelegateCommand(DodajJednMiary);
         ClearCommandJm = new DelegateCommand(ClearJm);
         UpdateCommandJm = new DelegateCommand(UpdateJm);
         EditCommandJm = new DelegateCommand(EditJm);
        DeleteCommandJm = new DelegateCommand(DeleteJm,CanDelete);
    }

        private void UpdateJm()
        {
            throw new NotImplementedException();
        }
        private void EditJm()
        {
            if (JedMiary != null)
            {
                NazwaJednMiary = JedMiary.OPISJM;
                SkrotJednMiary = JedMiary.JM;
            }
         
        }
        private void DeleteJm()
        {
            if(_jedmiary!=null)  
            {

                jmr.DelJm(_jedmiary);
                LoadJEdnMiary();
            }
       }


        #endregion

        #region entities i zmienne
      
        public IHP_JM _jm;
        public IHP_JM Jm
        {
            get
            {
                return _jm;
            }
            set
            {
                _jm = value;
            }
        }
      
    
        #endregion

        void SeedData()
        {
            LoadJEdnMiary();
         }

        public int SelectedGrupaKartID { get; set; }
        public ViewModelJM()
        {
             InitLists();
              InitCommands();
              SeedData();
        }

     private void LoadJEdnMiary()
        {
            LstJednMiary.Clear();
             foreach (IHP_JM item in jmr.JmGetAll())
                                    LstJednMiary.Add(item);
        }
 
       void ClearJm()
        {
            NazwaJednMiary = string.Empty;
            SkrotJednMiary = string.Empty;
            IsAktywny = false;
            JedMiary = null;
        }
       void DodajJednMiary()
        {
            string error = EnableValidationAndGetError();
            if (error != null)
            {
                DXMessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK, MessageBoxImage.Error);
                //    MessageBox.Show("Błąd " + error, "Wypełnij poprawnie formularz", MessageBoxButton.OK);
                return;
            }
            IHP_JM jm = new IHP_JM();
            jm.ID_IHP_JM = GetNextNumer(20);
            jm.JM = _skrotjednmiary;
            jm.OPISJM = _nazwajednmiary;
            jmr.AddJm(jm);
            ClearJm();
            LoadJEdnMiary();
        }

      public event PropertyChangedEventHandler PropertyChanged;
 
      protected void RisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void SetFocus(int parameter)
        {

            switch (parameter)
            {
                case 1:
                //    NazwaJednDodat = string.Empty;
                    break;
                case 2:
                //    IloscDodat = _iloscdodat;
                    break;
            }
        }
        #region IDataErrorInfo & Validation Members
        string EnableValidationAndGetError()
        {

            string error = ((IDataErrorInfo)this).Error;
            if (!string.IsNullOrEmpty(error))
            {
                this.RaisePropertiesChanged();
                return error;
            }
            return null;
        }
        string IDataErrorInfo.Error
        {
            get
            {
                IDataErrorInfo me = (IDataErrorInfo)this;
                string error =
                    //  me[BindableBase.GetPropertyName(() => NazwaRodz)]
                    me[BindableBase.GetPropertyName(() => NazwaJednMiary)] + System.Environment.NewLine
                   + me[BindableBase.GetPropertyName(() => SkrotJednMiary)] + System.Environment.NewLine;

                error = error.Trim();
                if (!string.IsNullOrEmpty(error))
                    return error;
                return null;
            }
        }

         bool CanDelete()
        {
           if (JedMiary == null)
                return false;
            else if (JedMiary != null)
                return !jmr.CheckExists(JedMiary);
            else
                return true;
       }

        string IDataErrorInfo.this[string columnName]
        {
            get
            {

                string NazwaJednDodatProp = BindableBase.GetPropertyName(() => NazwaJednMiary);
                string IloscDodatProp = BindableBase.GetPropertyName(() => SkrotJednMiary);

                if (columnName == "NazwaJednDodat")
                    return RequiredValidationRuleJednMiary.GetErrorMessage(NazwaJednDodatProp, NazwaJednMiary);
                if (columnName == "IloscDodat")
                    return RequiredValidationRuleJednMiary.GetErrorMessage(IloscDodatProp, SkrotJednMiary);
                else
                    return null;
            }
        }
        #endregion // IDataErrorInfo & Validation Members
    }

    public class RequiredValidationRuleJednMiary : ValidationRule
    {
        public static string GetErrorMessage(string fieldName, object fieldValue, object nullValue = null)
        {
            string errorMessage = string.Empty;

            //            if (nullValue != null && nullValue.Equals(fieldValue))
            //              errorMessage = string.Format("Pole:  {0} jest puste.", fieldName);
            if (fieldName == "NazwaJednMairy")
            {
                if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
                {
                    errorMessage = string.Format("Pole: Nazwa jest puste.");
                }
            }

            if (fieldName == "SkrotJednMairy")
            {
                if (fieldValue == null || string.IsNullOrEmpty(fieldValue.ToString()))
                {
                    errorMessage = string.Format("Pole: skrot jest puste.");
                }
            }

            return errorMessage;
        }
        public string FieldName { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string error = GetErrorMessage(FieldName, value);
            if (!string.IsNullOrEmpty(error))
                return new ValidationResult(false, error);
            return ValidationResult.ValidResult;
            //           throw new NotImplementedException();
        }
    }

 
}


