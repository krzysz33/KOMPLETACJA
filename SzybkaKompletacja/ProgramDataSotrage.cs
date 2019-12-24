using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace KpInfohelp
{
    public static class ProgramDataSotrage 
    {
        public static string ProfilePath;
        //   public static SqlStatmentsModel xmlSqlConfig;
        public static IHP_ZAM_USERS User;
        public static List<IHP_PARAMETRY> AppParams;
        public static int IDDEFSTAWKAVAT;
        public static SqlStatmentsModel xmlSqlConfig;
        static ProgramDataSotrage()
        {
            string LastMessage = string.Empty;
            XmlSerializer serializer = new XmlSerializer(typeof(SqlStatmentsModel));
            try
            {
                StreamReader reader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "SqlConfig.xml");
                xmlSqlConfig = (SqlStatmentsModel)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                LastMessage = ex.ToString();
                LogManager.WriteLogMessage(LogManager.LogType.Error, LastMessage);
                throw ex;
            }
        }

 
    }

}
