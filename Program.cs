using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace NetTrueFlow
{
    internal class Program
    {
        // open log file
        static string path = @"C:\programm\C#\NetTrueFlow\log\cisco.log";


        static void Main(string[] args)
        {
            Console.WriteLine("Start app NetTrueFlow!");

            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();

                netData.parsingData(path);
                
                watch.Stop();
                netData.outputResult();
                TimeSpan ts = watch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
                Console.WriteLine("Working time: {0}", elapsedTime);
            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Stop app NetTrueFlow");
        }
    }
}
