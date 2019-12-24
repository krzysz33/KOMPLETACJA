using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class AutomaticMultiPartReader
    {
        private MultiPartStream _mps;
        private bool _reading = false;
        private byte[] _currentPart;

        public AutomaticMultiPartReader(MultiPartStream stream)
        {
            _mps = stream;
        }

        public event EventHandler<PartReadyEventArgs> PartReady;

        protected virtual void OnPartReady()
        {
            if (PartReady != null)
            {
                PartReadyEventArgs args = new PartReadyEventArgs();
                args.Part = _currentPart;
                PartReady(this, args);
            }
        }
        public async void StartProcessing()
        {
            _reading = true;
            while (_reading)
            {
                _currentPart = await _mps.NextPartAsync().ConfigureAwait(false);
                OnPartReady();
            }
        }

        public void StopProcessing()
        {
            _mps.Close();
            _reading = false;
        }
    }
}
