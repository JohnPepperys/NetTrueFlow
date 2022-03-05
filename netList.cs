using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    internal static class netListTCP
    {
        static private int defaultOutputString = 15;
        
        
        static List<netRecord> denyTCPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string str)
        {
            if (string.IsNullOrEmpty(str)) { return; }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 3) { return; }

            // if find record in list
            foreach (var a in denyTCPIncomingAddress)
            {
                if(a.IPaddr == arr[1])
                {
                    a.count++;
                    return;
                }
            }

            // elst record not find
            denyTCPIncomingAddress.Add(new netRecord(arr[1]));
        }

        public static void outputList(int maxstring = 15)
        {
            // sortint by count before output records
            denyTCPIncomingAddress.Sort();
            
            if(maxstring == 0) { maxstring = denyTCPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyTCPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                k++;
            }
            Console.WriteLine();
        }
    }



    internal static class netListUDP
    {
        static List<netRecord> denyUDPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string str)
        {
            if (string.IsNullOrEmpty(str)) { return; }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 3) { return; }

            // if find record in list
            foreach (var a in denyUDPIncomingAddress)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    return;
                }
            }

            // elst record not find
            denyUDPIncomingAddress.Add(new netRecord(arr[1]));
        }

        public static void outputList(int maxstring = 15)
        {
            // sortint by count before output records
            denyUDPIncomingAddress.Sort();

            if (maxstring == 0) { maxstring = denyUDPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyUDPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                k++;
            }
            Console.WriteLine();
        }

    }




    internal static class netListICMP
    {
        static List<netRecord> denyICMPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string str)
        {
            if (string.IsNullOrEmpty(str)) { return; }

            string[] arr = str.Split(':');
            if (arr.Count() != 2) { return; }

            // if find record in list
            foreach (var a in denyICMPIncomingAddress)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    return;
                }
            }

            // elst record not find
            denyICMPIncomingAddress.Add(new netRecord(arr[1]));
        }

        public static void outputList(int maxstring = 15)
        {
            // sortint by count before output records
            denyICMPIncomingAddress.Sort();

            if (maxstring == 0) { maxstring = denyICMPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyICMPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                k++;
            }
            Console.WriteLine();
        }

    }
}
