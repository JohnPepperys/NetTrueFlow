using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    static class Constant
    {
        public const int DEFAULT_OUTPUT_STRING = 15;
    }


    internal static class netListTCP
    {
           
        static List<netRecord> denyTCPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string str, string destination)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(destination)) {
                throw new Exception("переданы пустые данные в поле source или destination!");
            }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 3) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyTCPIncomingAddress)
            {
                if(a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyTCPIncomingAddress.Add(new netRecord(arr[1]));
            var aaa = denyTCPIncomingAddress.Count();
            commonFunc.addDestinationAddr(denyTCPIncomingAddress[aaa-1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyTCPIncomingAddress.Sort();
            
            if(maxstring == 0) { maxstring = denyTCPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyTCPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                foreach (var b in a.listDest)
                {
                    Console.WriteLine("\t\t\t\t{0} - {1}", b.IPaddr, b.count);
                }
                k++;
            }
            Console.WriteLine();
        }
    }



    internal static class netListUDP
    {
        static List<netRecord> denyUDPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string source, string destination)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination)) { 
                throw new Exception("переданы пустые данные в поле source или destination!"); }

            string[] arr = source.Split(':', '/');
            if (arr.Count() != 3) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyUDPIncomingAddress)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyUDPIncomingAddress.Add(new netRecord(arr[1]));
            var maxelem = denyUDPIncomingAddress.Count;
            commonFunc.addDestinationAddr(denyUDPIncomingAddress[maxelem-1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyUDPIncomingAddress.Sort();

            if (maxstring == 0) { maxstring = denyUDPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyUDPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                a.listDest.Sort();
                foreach (var b in a.listDest)
                {
                    Console.WriteLine("\t\t\t\t{0} - {1}", b.IPaddr, b.count);
                }
                k++;
            }
            Console.WriteLine();
        }

    }




    internal static class netListICMP
    {
        static List<netRecord> denyICMPIncomingAddress = new List<netRecord> { };
        public static void addAddrInList(string str, string destination)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(destination)) {
                throw new Exception("переданы пустые данные в поле source или destination!");
            }

            string[] arr = str.Split(':');
            if (arr.Count() != 2) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyICMPIncomingAddress)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyICMPIncomingAddress.Add(new netRecord(arr[1]));
            var ss = denyICMPIncomingAddress.Count;
            commonFunc.addDestinationAddr(denyICMPIncomingAddress[ss-1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyICMPIncomingAddress.Sort();

            if (maxstring == 0) { maxstring = denyICMPIncomingAddress.Count; }
            int k = 0;
            foreach (var a in denyICMPIncomingAddress)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t\t\t{0}\t : {1}", a.IPaddr, a.count);
                foreach (var b in a.listDest)
                {
                    Console.WriteLine("\t\t\t\t{0} - {1}", b.IPaddr, b.count);
                }
                k++;
            }
            Console.WriteLine();
        }
    }


    // -----------------------------------------------------------------------------------------------------------------------------------------
    // -----------------------------------------------------------------------------------------------------------------------------------------
    static class commonFunc
    {
        public static void addDestinationAddr(netRecord obj, string dst)
        {
            if (string.IsNullOrEmpty(dst)) { 
                throw new Exception("переданы пустые данные в поле destination!"); }

            string[] arr = dst.Split(':', '/');
            if (arr.Count() < 2) { 
                throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in obj.listDest)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    return;
                }
            }

            // elst record not find
            obj.listDest.Add(new dest(arr[1]));

        }
    }

}
