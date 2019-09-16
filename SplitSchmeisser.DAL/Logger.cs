using System.Threading;
using System.IO;
using System;

namespace SplitSchmeisser.DAL
{
    public static class Logger
    {
        private static readonly string path = @"log.txt";
        private static object locker = new object();

        public static void Write(string str, bool delay = false)
        {
            lock (locker)
            {
                using (StreamWriter sw = new StreamWriter(File.Open(path, FileMode.Append)))
                {
                    sw.WriteLineAsync($"{DateTime.Now.ToLongTimeString()} --> {str}");
                    if(delay) Thread.Sleep(10000);
                }
            }
        }
    }
}
