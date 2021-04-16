using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Helpers
{
    public class Helper
    {
        public static void ExceptionLog(string log)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "ExceptionLogs.txt");

            FileStream fileStream;
            if (File.Exists(path))
                fileStream = new FileStream(path, FileMode.Append);
            else
                fileStream = new FileStream(path, FileMode.OpenOrCreate);

            StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(DateTime.Now + " - " + log + "\n\n");

            streamWriter.Flush();
            streamWriter.Close();
            fileStream.Close();
        }
    }
}
