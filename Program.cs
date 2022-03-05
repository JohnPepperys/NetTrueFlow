using System;
using System.IO;
using System.Text;

namespace NetTrueFlow
{
    internal class Program
    {
        // open log file
        static string path = @"E:\work\Programm\2022\C#\NetTrueFlow\log\cisco.log";


        static void Main(string[] args)
        {
            Console.WriteLine("Start app NetTrueFlow!");

            try
            {
                netData.parsingData(path);
                netData.outputResult();
            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Stop app NetTrueFlow");
        }
    }
}
