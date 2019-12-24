using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace KpInfohelp
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<ViewModelMain>();
            SimpleIoc.Default.Register<ModuleSelectorViewModel>();
         


           


        }

        //public ViewModelRegWaga viewModelRegWaga
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<ViewModelRegWaga>();
        //    }
        //}

        //public Test2ViewModel Test2
        //{
        //    get
        //    {
        //        return ServiceLocator.Current.GetInstance<Test2ViewModel>();
        //    }
        //}
    }
}
