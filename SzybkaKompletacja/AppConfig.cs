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
 
namespace KpInfohelp
{
    public class AppConfig
    {
        public PANELSCOLLECTION PANELSCONFIG;
        public AppSettings UstawieniaAplikacji;
        public int IDDEFSTAWKAVAT;
        private static AppConfig m_oInstance = null;
        private int m_nCounter = 0;
        private static ComConfig _comconfig = null;
        public static ComConfig CreateComConfig()
        {
            var doc = XDocument.Load(@"AppSettings.xml");

            ComConfig _comconf = doc.Root
                .Descendants("WAGA1")
                .Select(node => new ComConfig
                {
                 PORTNAME =(string)node.Element("PORTNAME"),
                  BITDATA = (int)node.Element("BITDATA"),
                    PARITY = (string)node.Element("PARITY"),
                    BITSTOP = (int)node.Element("BITSTOP"),
                    STERPRZEP = (string)node.Element("STERPRZEP")           
                }).FirstOrDefault();
    

            return _comconf;
        }
        public static ComConfig GetComConfig
        {
            get
            {
                if (_comconfig == null)
                {
                    _comconfig = new ComConfig();
                     _comconfig = CreateComConfig();
                }
                return _comconfig;
            }
        }
        public static AppConfig GetInstance
        {
            get
            {
                if (m_oInstance == null)
                {
                    m_oInstance = new AppConfig();
                }
                return m_oInstance;
            }
        }
        public AppConfig()
        {
          XmlSerializer serializerapp = new XmlSerializer(typeof(AppSettings));
             try
             {
               StreamReader readerbaza = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "AppSettings.xml");
                 UstawieniaAplikacji = (AppSettings)serializerapp.Deserialize(readerbaza);
                 readerbaza.Close();
             }
             catch (Exception ex)
             {
                throw ex;
            }


        }
        public void Zapisz()
        {
            // Create a new Serializer
            XmlSerializer serializer = new XmlSerializer(typeof(AppSettings));
            TextWriter writer = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "AppSettings.xml");
           // Serialize the file
            serializer.Serialize(writer, UstawieniaAplikacji);
            // Close the writer
            writer.Close();
        }
        public string ConnectionString()
        {
            string connstr = String.Empty;
            string connectionString = string.Empty;
 

            if (UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji == 1)
                connectionString = string.Format("data source={0};initial catalog={1};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework",
                                                                              UstawieniaAplikacji.BazaDanych.NazwaBazyDanych,
                                                                             UstawieniaAplikacji.BazaDanych.KatalogBazyDanych,
                                                                             UstawieniaAplikacji.BazaDanych.provider);

            if (UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji == 0)
                connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};",
                                                                          UstawieniaAplikacji.BazaDanych.provider,
                                                                          UstawieniaAplikacji.BazaDanych.NazwaBazyDanych,
                                                                          UstawieniaAplikacji.BazaDanych.KatalogBazyDanych,
                                                                          UstawieniaAplikacji.BazaDanych.UzytkownikBazy,
                                                                          UstawieniaAplikacji.BazaDanych.Haslo);
            return connectionString;
        }
        public bool TestConnection()
        {
            string connectionString = String.Empty;
            if (UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji == 1)
                   connectionString = string.Format("data source={0};initial catalog={1};integrated security=True;multipleactiveresultsets=True;application name=EntityFramework",
                                                                                 UstawieniaAplikacji.BazaDanych.NazwaBazyDanych,
                                                                                UstawieniaAplikacji.BazaDanych.KatalogBazyDanych,
                                                                                UstawieniaAplikacji.BazaDanych.provider);

            if (UstawieniaAplikacji.BazaDanych.RodzajAutoryzacji == 0)
                  connectionString = string.Format("Provider={0};Data Source={1};Initial Catalog={2};User ID={3};Password={4};",
                                                                            UstawieniaAplikacji.BazaDanych.provider,
                                                                            UstawieniaAplikacji.BazaDanych.NazwaBazyDanych,
                                                                            UstawieniaAplikacji.BazaDanych.KatalogBazyDanych,
                                                                            UstawieniaAplikacji.BazaDanych.UzytkownikBazy,
                                                                            UstawieniaAplikacji.BazaDanych.Haslo);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
    }
}
 