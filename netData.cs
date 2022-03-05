using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    internal static class netData
    {

        static public string inputFile { get; set; }

        // всего обработано строк в файле
        static int allString = 0;
        // всего строк имеет отношение к сетевому журналу
        static int allDataString = 0;
        // Count TCP block packet
        static int countTCPblock = 0;
        // Count UDP block packet
        static int countUDPblock = 0;
        // Count ICMP block packet
        static int countICMPblock = 0;
        static int blockConnection = 0;
        

        public static void parsingData(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            inputFile = fileName;

            try
            {
                foreach (string line in File.ReadLines(inputFile))
                {
                    allString++;
                    if (line.Contains("%ASA-"))
                    {
                        allDataString++;
                        parseOneString(line);
                    }
                }
            } catch (Exception e) { 
                Console.WriteLine(e.Message);
            }
        }

        public static void outputResult()
        {
            Console.WriteLine("Parsing all lines: {0}", allString);
            Console.WriteLine("Lines from cisco FPR log: {0}", allDataString);
            Console.WriteLine("Block connection: {0}", blockConnection);
            Console.WriteLine("\tBlock TCP connection: {0}", countTCPblock);
            netListTCP.outputList();
            Console.WriteLine("\tBlock UDP connection: {0}", countUDPblock);
            netListUDP.outputList();
            Console.WriteLine("\tBlock ICMP connection: {0}", countICMPblock);
            netListICMP.outputList();
        }

    // ------------- work function -------------------------------------------------------------------
        private static void parseOneString(string line)
        {
            var eventTime = getTimeFromString(line);

            // Deny TCP, UDP: source, port - distenation, port
            if (line.Contains(" Deny "))
            {
                blockConnection++;
                string[] arr = line.Split(' ');
                for(var i = 0; i < arr.Length; i++)
                {
                    if(arr[i] == "Deny")
                    {
                       /* Console.WriteLine("proto: {0}", arr[i+1]);
                        Console.WriteLine("src: {0}", arr[i + 3]);
                        Console.WriteLine("dst: {0}", arr[i + 5]); */

                        if (arr[i + 1] == "tcp") { 
                            countTCPblock++;
                            netListTCP.addAddrInList(arr[i + 3]);
                        }
                        if (arr[i + 1] == "udp") {
                            countUDPblock++;
                            netListUDP.addAddrInList(arr[i + 3]);
                        }
                        if (arr[i + 1] == "icmp") { 
                            countICMPblock++;
                            netListICMP.addAddrInList(arr[i + 3]);
                        }
                    }
                }
            }
            else {
                // Build dynamic
                if (line.Contains(" Build "))
                {
                    return;
                }
            }
        }

        private static string getTimeFromString(string line)
        {
            string[] arr = line.Split(' ');
            return arr[0] + " " +  arr[1] + " " + arr[2];
        }

        
        // -------------------------------------------------------------------------------------------------------------------------
    }
}
