using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    internal class netData
    {
        public string inputFile { get; set; }
        List<string> denyTCPIncomingAddress = new List<string> { };
        List<string> denyUDPIncomingAddress = new List<string> { };
        List<string> denyICMPIncomingAddress = new List<string> { };
        
        List<string> sourceOutgoingAddress = new List<string> { };

        // всего обработано строк в файле
        int allString = 0;
        // всего строк имеет отношение к сетевому журналу
        int allDataString = 0;
        // Count TCP block packet
        int countTCPblock = 0;
        // Count UDP block packet
        int countUDPblock = 0;
        // Count ICMP block packet
        int countICMPblock = 0;
        int blockConnection = 0;
        
        public netData(string s)
        {
            inputFile = s;
        }

        public void parsingData()
        {
            if (string.IsNullOrEmpty(inputFile))
            {
                return;
            }

            string[] newText = inputFile.Split('\n');
            foreach (string line in newText)
            {
                allString++;
                if (line.Contains("%ASA-"))
                {
                    allDataString++;
                    parseOneString(line);
                }
            }
        }

        public void outputResult()
        {
            Console.WriteLine("Parsing all lines: {0}", allString);
            Console.WriteLine("Lines from cisco FPR log: {0}", allDataString);
            Console.WriteLine("Block connection: {0}", blockConnection);
            Console.WriteLine("\tBlock TCP connection: {0}", countTCPblock);
            Console.WriteLine("\tBlock UDP connection: {0}", countUDPblock);
            Console.WriteLine("\tBlock ICMP connection: {0}", countICMPblock);
        }

    // ------------- work function -------------------------------------------------------------------
        private void parseOneString(string line)
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
                        /*Console.WriteLine("proto: {0}", arr[i+1]);
                        Console.WriteLine("src: {0}", arr[i + 3]);
                        Console.WriteLine("dst: {0}", arr[i + 5]);*/

                        if (arr[i + 1] == "tcp") { 
                            countTCPblock++;
                            denyTCPIncomingAddress.Add(arr[i + 3]);
                        }
                        if (arr[i + 1] == "udp") {
                            denyUDPIncomingAddress.Add(arr[i + 3]);
                            countUDPblock++; 
                        }
                        if (arr[i + 1] == "icmp") { 
                            countICMPblock++;
                            denyICMPIncomingAddress.Add(arr[i + 3]);
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

        private string getTimeFromString(string line)
        {
            string[] arr = line.Split(' ');
            return arr[0] + " " +  arr[1] + " " + arr[2];
        }

    }
}
