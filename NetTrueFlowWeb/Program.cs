using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetTrueFlow;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetTrueFlowWeb
{
    public class Program
    {
        static public string elapsedTime { get; set; }
        public static void Main(string[] args)
        {
            string FilePath = @"E:\program\C#\NetTrueFlow\log\cisco.log";
            try
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                netData.parsingData(FilePath);
                watch.Stop();
                TimeSpan ts = watch.Elapsed;
                elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
