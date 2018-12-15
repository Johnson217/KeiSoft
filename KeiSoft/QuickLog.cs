using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeiSoft
{
   public  class QuickLog
     {

        public static void LogLine(string log)
        {
            try
            {
                string d = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log");
                if (!System.IO.Directory.Exists(d))
                {
                    System.IO.Directory.CreateDirectory(d);
                }
                string path = System.IO.Path.Combine(d, "Result.txt");
                using (StreamWriter file = new StreamWriter(path, true, Encoding.UTF8))
                {
                    file.WriteLine(DateTime.Now.ToString("F") + "==>" + log);
                }
            }
            catch { }
        }
    }
}
