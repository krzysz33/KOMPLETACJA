using DevExpress.Mvvm;
using Licencja;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp.Kamery
{
    class KameryDAL : CrudVMBase
    {
        private string _filetext;
        private string _filename;

        private int nrkamery;


            private int _idwazenie;
        public bool InProcess { get; set; }
        private Bitmap loadedBitmap;
        private string cameraLogin { get; set; }
        private string cameraPassword { get; set; }

        private string AdressIP { get; set; }
        private int Port { get; set; }
        private string Producent { get; set; }
        private string Model { get; set; }
        private string FIleName { get; set; }
        private string Uri { get; set; }

        private AppConfig app;
        public KameryDAL(int IdWazenie)
        {
            _idwazenie = IdWazenie;
        }

        public void UpdateFoto(){}
        public void DeleteFoto(){}
        public void InsertFoto(){}

       
        private void GetSettings(int NrKamera)
        {
              app = AppConfig.GetInstance;

            if (NrKamera==1)
            {
                Producent = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Producent;
                Model = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Model;
                AdressIP = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.AdressIP;
                Port = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Port;
                Uri = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.URI;
                cameraLogin = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.User;
                cameraPassword = app.UstawieniaAplikacji.UstawieniaKamer.Kamera1.Pass;

            }

            if (NrKamera == 2)
            {
                Producent = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Producent;
                Model = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Model;
                AdressIP = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.AdressIP;
                Port = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Port;
                Uri = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.URI;
                cameraLogin = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.User;
                cameraPassword = app.UstawieniaAplikacji.UstawieniaKamer.Kamera2.Pass;
            }
            if (NrKamera == 3)
            {
                Producent = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Producent;
                Model = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Model;
                AdressIP = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.AdressIP;
                Port = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Port;
                Uri = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.URI;
                cameraLogin = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.User;
                cameraPassword = app.UstawieniaAplikacji.UstawieniaKamer.Kamera3.Pass;
            }
        }
        private void requestFrame()
        {
         try
            {
            InProcess = true;
            string cameraUrl = @"http://" + AdressIP + Uri;
            var request = System.Net.HttpWebRequest.Create(cameraUrl);
            request.Credentials = new NetworkCredential(cameraLogin, cameraPassword);
            request.Proxy = null;
            request.BeginGetResponse(new AsyncCallback(finishRequestFrame), request);
            }
            catch ( Exception e)
            {
                string lasterror = string.Empty;
                if (e.InnerException != null)
                    lasterror = e.InnerException.ToString();
                else
                    lasterror = e.Message.ToString();
                InProcess = false;
            }
        }
        void finishRequestFrame(IAsyncResult result)
        {
            try
            {
                HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                using (Bitmap frame = new Bitmap(responseStream))
                {
                    if (frame != null)
                    {
                        loadedBitmap = (Bitmap)frame.Clone();
                        SaveFile();
                        SavaToDatabase();
                    }
                }
            }
            catch (Exception e)
            {
                string lasterror = string.Empty;
                if (e.InnerException != null)
                    lasterror = e.InnerException.ToString();
                else
                    lasterror = e.Message.ToString();
                    InProcess = false;
            }
        }

      private void SaveFile( )
        {
           _filetext = DateTime.Now.ToString("yyyyMMddHHmmss");
             // _filename = Krypto.Encrypt(_filetext);
            _filename = _filetext + ".bmp";
            string filepath = Path.Combine(Environment.CurrentDirectory+ @"\img", _filename); 
            loadedBitmap.Save(filepath, ImageFormat.Bmp);
        }
        private void SavaToDatabase()
        {
            string LastMessage;
            try
            {
                IHP_NUMERACJA numerkr = GetId(15);
                if (numerkr != null)
                    numerkr.NUMER++;

                IHP_FOTO fototodel = context.IHP_FOTO?.FirstOrDefault(x => x.ID_IHP_KARTOTEKA == _idwazenie && x.LP == nrkamery);

                if(fototodel!=null)
                {
                    context.Entry(fototodel).State = EntityState.Deleted;
                    context.IHP_FOTO.Remove(fototodel);
                    context.SaveChanges();
                }

                IHP_FOTO foto = new IHP_FOTO()
                {
                    ID_IHP_FOTO = numerkr.NUMER,
                    ID_IHP_KARTOTEKA = _idwazenie,//  zmienione
                    LP = nrkamery, // zmienione
                    NAMEFILE = _filename
                };
                context.IHP_NUMERACJA.Add(numerkr);
                context.Entry(numerkr).State = EntityState.Modified;
                context.IHP_FOTO.Add(foto);
                context.SaveChanges();
                Messenger.Default.Send<int>(3);
                InProcess = false;
     
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        LogManager.WriteLogMessage(LogManager.LogType.Error, String.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                InProcess = false;
                throw;
            }
        }

        public void SaveCam1()
        {
         nrkamery = 1;
         GetSettings(nrkamery);
        requestFrame();
  
        }

        public void SaveCam2()
        {
            nrkamery = 2;
            GetSettings(nrkamery);
            requestFrame();
            // SaveFile();
            //  SavaToDatabase(); 
        }
        public void SaveCam3()
        {
            nrkamery = 3;
            GetSettings(nrkamery);
            requestFrame();
        }

 
    }
}
