using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogError
{
    public class LogError
    {
        public static void WriteError(Exception ex)
        {
            // this will be in a temp directory.
            string path = HttpRuntime.AppDomainAppPath;

            //once you have the path you get the directory with:
            var directory = System.IO.Path.GetDirectoryName(path);
            directory = directory + @"\Log Error\";
            Directory.CreateDirectory(directory);

            string filePath = directory + "Error.txt";

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("-----------------------------------------------------------------------------");
                writer.WriteLine("Date : " + DateTime.Now.ToString("yyyy-mmm-dd HH:MM:ss"));
                writer.WriteLine();

                while (ex != null)
                {
                    writer.WriteLine(ex.GetType().FullName);
                    writer.WriteLine("Message : " + ex.Message);
                    writer.WriteLine("StackTrace : " + ex.StackTrace);

                    ex = ex.InnerException;
                }
            }
        }
    }
}
