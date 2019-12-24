using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Licencja;
using DevExpress.Mvvm;

namespace KpInfohelp
{
    public class LicConfig:CrudVMBase
    {
        public LicencjaClass UstawieniaLicencji;
        public bool LicOk { get; set; }
        public int IDDEFSTAWKAVAT;
        private static LicConfig m_oInstance = null;
        private int m_nCounter = 0;
        private static LicConfig _comconfig = null;
        public string NIP { get; set; }
        public string NAZWAFIRMY { get; set; }
        //public static LicConfig CreateComConfig()
        //{

        //    var doc = XDocument.Load(@"Lic.ihp");

        //    LicConfig _comconf = doc.Root
        //        .Descendants("WAGA1")
        //        .Select(node => new ComConfig
        //        {
        //         PORTNAME =(string)node.Element("PORTNAME"),
        //          BITDATA = (int)node.Element("BITDATA"),
        //            PARITY = (string)node.Element("PARITY"),
        //            BITSTOP = (int)node.Element("BITSTOP"),
        //            STERPRZEP = (string)node.Element("STERPRZEP")           
        //        }).FirstOrDefault();
        //    return _comconf;
        //}




        public static LicConfig GetInstance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new LicConfig();
                }
                return m_oInstance;
            }
        }
        public  LicConfig()
        {
          XmlSerializer serializerlic = new XmlSerializer(typeof(LicencjaClass));
             try
             {
                LicOk = true;
                StreamReader readerbaza = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Lic.ihp");
                string ToDecrypt = readerbaza.ReadToEnd();
                var TextBody = Krypto.Decrypt(ToDecrypt);
            
                var serializer = new XmlSerializer(typeof(LicencjaClass));
                using (var reader = new StringReader(TextBody))
                {
                    UstawieniaLicencji = (LicencjaClass)serializer.Deserialize(reader);
                    if (UstawieniaLicencji != null)
                        NIP = UstawieniaLicencji.Firma.NIP;
                        NAZWAFIRMY= UstawieniaLicencji.Firma.Nazwa;
                }
            //    UstawieniaLicencji = (LicencjaClass)serializerlic.Deserialize(readerbaza);
                 readerbaza.Close();
             }
             catch (Exception ex)
            {
                LicOk = false;
                //throw ex;
            }
           
        }
        public void ZaczytajNowa(string filename)
        {
            XmlSerializer serializerlic = new XmlSerializer(typeof(LicencjaClass));
            try
            {
                LicOk = true;
                StreamReader readerbaza = new StreamReader(filename);
                string ToDecrypt = readerbaza.ReadToEnd();
                var TextBody = Krypto.Decrypt(ToDecrypt);

                var serializer = new XmlSerializer(typeof(LicencjaClass));
                using (var reader = new StringReader(TextBody))
                {
                    UstawieniaLicencji = (LicencjaClass)serializer.Deserialize(reader);
                }
                //    UstawieniaLicencji = (LicencjaClass)serializerlic.Deserialize(readerbaza);
                readerbaza.Close();
            }
            catch (Exception ex)
            {
                LicOk = false;
                //throw ex;
            }
        }
        public void Zapisz()
        {
            string TextBody = string.Empty;
       // Create a new Serializer
        XmlSerializer serializer = new XmlSerializer(typeof(LicencjaClass));
           using (StringWriter textWriter = new StringWriter())
            {
                serializer.Serialize(textWriter, UstawieniaLicencji);
                  TextBody =  textWriter.ToString();
            }
        string cruptobody = Krypto.Encrypt(TextBody);
         System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "Lic.ihp", cruptobody);


         
        }


    }
}
 