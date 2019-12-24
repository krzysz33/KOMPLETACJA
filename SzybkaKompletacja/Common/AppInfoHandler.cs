using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Common
{
  public   class AppInfoHandler
    {
        private List<IObserver<AppInfo>> observers;
        private List<AppInfo> appconfigs;
        public AppInfoHandler()
        {
            observers = new List<IObserver<AppInfo>>();
            appconfigs = new List<AppInfo>();
        }
        public IDisposable Subscribe(IObserver<AppInfo> observer)
        {
            // Check whether observer is already registered. If not, add it
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                // Provide observer with existing data.
                foreach (var item in appconfigs)
                    observer.OnNext(item);
            }
            return new Unsubscriber<AppInfo>(observers, observer);
        }
    }

    internal class Unsubscriber<AppInfo> : IDisposable
    {
        private List<IObserver<AppInfo>> _observers;
        private IObserver<AppInfo> _observer;

        internal Unsubscriber(List<IObserver<AppInfo>> observers, IObserver<AppInfo> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}
