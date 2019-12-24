using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public static class LogManager
    {
        public enum LogType
        {
            Info,
            Warning,
            Error
        };

        public static void WriteLogMessage(LogType logType, string description)
        {
            FileInfo fileInfo = new FileInfo(Environment.CurrentDirectory);
            string path = fileInfo.ToString() + "\\Logs\\" + DateTime.Now.ToShortDateString().Replace('-', '_') + ".txt";
            using (StreamWriter SW = new StreamWriter(path, true))
            {
                string message = logType.ToString() + " '" + DateTime.Now.ToShortTimeString() + "': " + description;
                SW.WriteLine(message);
            }
        }
    }
}
