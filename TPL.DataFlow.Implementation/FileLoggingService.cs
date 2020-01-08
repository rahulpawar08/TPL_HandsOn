using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace TPL.DataFlow.Implementation
{
    public class FileLoggingService : ILoggingService
    {
        string _filePath;
        public FileLoggingService(string filePath)
        {
            _filePath = filePath;
        }
        public FileLoggingService()
        {
            string binaryPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            _filePath = Path.Combine(binaryPath, "\\DownloaderLogs.txt");
        }
        public bool LogData(List<string> logs)
        {
            try
            {
                //using (FileStream stream = File.Open(_filePath, FileMode.Append))
                {
                    foreach(var log in logs)
                    {
                        string logData = "Log:" + DateTime.Now.ToLongTimeString()+" " + log;
                        //stream.WriteByte(Byte.Parse(logData));

                        Console.WriteLine(logData);
                    }
                   
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception Occured" +ex.Message);
                return false;
            }
        }

        public bool LogData(string name, List<string> logs)
        {
            List<string> logDataWithName = new List<string>();
            foreach (var log in logs)
            {
                logDataWithName.Add(name + ": "+ log);
            }
           return LogData(logDataWithName);
        }
    }
}
