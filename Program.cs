using System;
using System.IO;

namespace NetTrueFlow
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start app NetTrueFlow!");

            // open log file
            string path = @"E:\work\Programm\2022\C#\NetTrueFlow\log\cisco.log";
            try
            {
                using (FileStream fstream = new FileStream(path, FileMode.Open))
                {
                    byte[] buffer = new byte[fstream.Length];
                    fstream.Read(buffer, 0, buffer.Length);
                    Console.WriteLine(buffer);
                }
            } catch (Exception e) { 
                Console.WriteLine("Не могу прочитать указанный файл");
                Console.WriteLine(e.Message);
            }
            // parsing statistic
                // show results

            Console.WriteLine("Stop app NetTrueFlow");
        }
    }
}
