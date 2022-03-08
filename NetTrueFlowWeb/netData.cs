using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    public static class netData
    {

        static public string inputFile { get; set; }

        // всего обработано строк в файле
        public static ulong allString = 0;
        // всего строк имеет отношение к сетевому журналу
        public static ulong allDataString = 0;
        // Count TCP block packet
        public static ulong countTCPblock = 0;
        // Count UDP block packet
        public static ulong countUDPblock = 0;
        // Count ICMP block packet
        public static ulong countICMPblock = 0;
        public static ulong blockConnection = 0;

        // count open connection
        public static ulong countOpenConnection = 0;
        // count open inbound connection
        public static ulong countInbountOpenConnection = 0;
        // count open outbound connection
        public static ulong countOutboundOpenConnection = 0;
        public static ulong countInOpenTCP = 0;
        public static ulong countInOpenUDP = 0;
        public static ulong countInOpenICMP = 0;

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

           /* Console.WriteLine("\tGroup DENY packets by Sources!!!");
            Console.WriteLine("\t\tBlock TCP connection: {0}", countTCPblock);
            netListTCP.outputList();
            Console.WriteLine("\t\tBlock UDP connection: {0}", countUDPblock);
            netListUDP.outputList();
            Console.WriteLine("\t\tBlock ICMP connection: {0}", countICMPblock);
            netListICMP.outputList();

            Console.WriteLine("\tGroup DENY packets by destintaion!!!");
            Console.WriteLine("\t\tBlock TCP connection: {0}", countTCPblock);
            netDenyDestinationTCP.outputList();
            Console.WriteLine("\t\tBlock UDP connection: {0}", countUDPblock);
            netDenyDestinationUDP.outputList();
            Console.WriteLine("\t\tBlock ICMP connection: {0}", countICMPblock);
            netDenyDestinationICMP.outputList();
           */

            Console.WriteLine("Open connection: {0}", countOpenConnection);
            Console.WriteLine("Open inbound connection: {0}", countInbountOpenConnection);
            Console.WriteLine("\tTCP connect: {0}", countInOpenTCP);
            Console.WriteLine("\tUDP connect: {0}", countInOpenUDP);
            Console.WriteLine("\tICMP connect: {0}", countInOpenICMP);
            Console.WriteLine("Open outbound connection: {0}", countOutboundOpenConnection);

            Console.WriteLine("List inbound TCP Connection sort by Sources");
            netOpenConnectTCP.printInList();
            netOpenConnectUDP.printInList();
            netOpenConnectICMP.printInList();
            Console.WriteLine("List inbound TCP Connection sort by Destination");
            netOpenDestConnectTCP.printInList();
            netOpenDestConnectUDP.printInList();
            netOpenDestConnectICMP.printInList();
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
                for(var i = 0; i < arr.Count(); i++)
                {
                    if(arr[i] == "Deny")
                    {
                        if (arr[i + 1] == "tcp")
                        {
                            countTCPblock++;
                            netListTCP.addAddrInList(arr[i + 3], arr[i + 5]);
                            netDenyDestinationTCP.addAddrInList(arr[i + 5], arr[i + 3]);
                        }
                        else
                        {
                            if (arr[i + 1] == "udp")
                            {
                                countUDPblock++;
                                netListUDP.addAddrInList(arr[i + 3], arr[i + 5]);
                                netDenyDestinationUDP.addAddrInList(arr[i + 5], arr[i + 3]);
                            }
                            else
                            {
                                if (arr[i + 1] == "icmp")
                                {
                                    countICMPblock++;
                                    netListICMP.addAddrInList(arr[i + 3], arr[i + 5]);
                                    netDenyDestinationICMP.addAddrInList(arr[i + 5], arr[i + 3]);
                                }
                            }
                        }
                        break;
                    }
                }
            }
            else {
                // Открываем входящие соединения
                if (line.Contains(" Built inbound "))
                {
                    countOpenConnection++;
                    countInbountOpenConnection++;
                    string[] arr = line.Split(' ');
                    for(var i = 0; i < arr.Count(); i++)
                    {
                        if(arr[i] == "inbound")
                        {
                            switch(arr[i+1])
                            {
                                case "TCP":
                                    {
                                        countInOpenTCP++;
                                        netOpenConnectTCP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        netOpenDestConnectTCP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        break;
                                    }
                                case "UDP":
                                    {
                                        countInOpenUDP++;
                                        netOpenConnectUDP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        netOpenDestConnectUDP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        break;
                                    }
                                case "ICMP":
                                    {
                                        countInOpenICMP++;
                                        netOpenConnectICMP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        netOpenDestConnectICMP.addOpenAddrInList(arr[i + 5], arr[i + 8]);
                                        break;
                                    }
                            }
                            break;
                        }
                    }
                    return;
                }
                else
                {

                    // Открываем исходящее соединение
                    if (line.Contains(" Built outbound "))
                    {
                        countOpenConnection++;
                        countOutboundOpenConnection++;
                        return;
                    }
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
