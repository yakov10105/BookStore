using BookStore.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BL.Services.LogService
{
    public class Logger : ILogger
    {
        private readonly string _dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AppDirectory");
        private readonly string _filePath = "LoggerData.txt";


        public Logger()
        {
            if (!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
        }
        public void Log(string message)
        {
            using (StreamWriter sw = DI.GetStreamWriter(Path.Combine(_dir, _filePath)))
            {
                sw.WriteLine($"========= {DateTime.Now} ========");
                sw.WriteLine(message);
                sw.WriteLine("=======================================");
                sw.Close();
            }
        }
    }
}
