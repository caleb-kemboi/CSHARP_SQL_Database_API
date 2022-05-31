using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConsole
{
    class GeneralSetups
    {
        public class Logger
        {
            public static void WriteLog(string strLog)
            {
                StreamWriter log;
                FileStream fileStream = null;
                DirectoryInfo logDirInfo = null;
                FileInfo logFileInfo;



                string logFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
                logFilePath = logFilePath + "Log - " + DateTime.Today.ToLongDateString() + "." + "txt";
                logFileInfo = new FileInfo(logFilePath);
                logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
                if (logDirInfo.Exists) logDirInfo.Create();
                if (!logFileInfo.Exists)
                {
                    fileStream = logFileInfo.Create();
                }
                else
                {
                    fileStream = new FileStream(logFilePath, FileMode.Append);
                }
                log = new StreamWriter(fileStream);
                log.WriteLine(strLog);
                log.Close();
            }

            public enum Uploading
            {
                uploaded,
            }




            //public static string BASE_API_URL = Utils.ReadAppConfigFile("BASE_API_URL", "https://localhost:44348/api/");
        }
    }
}
