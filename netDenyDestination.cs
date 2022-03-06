using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    static class netDenyDestinationTCP
    {
        static List<netRecord> denyTCPgroupbyDestination = new List<netRecord> { };
        public static void addAddrInList(string str, string destination)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(destination)) { 
                throw new Exception("переданы пустые данные в поле source или destination!"); }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 3) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyTCPgroupbyDestination)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyTCPgroupbyDestination.Add(new netRecord(arr[1]));
            var aaa = denyTCPgroupbyDestination.Count();
            commonFunc.addDestinationAddr(denyTCPgroupbyDestination[aaa - 1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyTCPgroupbyDestination.Sort();

            if (maxstring == 0) { maxstring = denyTCPgroupbyDestination.Count; }
            int k = 0;
            foreach (var a in denyTCPgroupbyDestination)
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



    static class netDenyDestinationUDP
    {
        static List<netRecord> denyUDPgroupbyDestination = new List<netRecord> { };
        public static void addAddrInList(string str, string destination)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(destination)) { 
                throw new Exception("переданы пустые данные в поле source или destination!"); }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 3) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyUDPgroupbyDestination)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyUDPgroupbyDestination.Add(new netRecord(arr[1]));
            var aaa = denyUDPgroupbyDestination.Count();
            commonFunc.addDestinationAddr(denyUDPgroupbyDestination[aaa - 1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyUDPgroupbyDestination.Sort();

            if (maxstring == 0) { maxstring = denyUDPgroupbyDestination.Count; }
            int k = 0;
            foreach (var a in denyUDPgroupbyDestination)
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



    static class netDenyDestinationICMP
    {
        static List<netRecord> denyICMPgroupbyDestination = new List<netRecord> { };
        public static void addAddrInList(string str, string destination)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(destination)) { 
                throw new Exception("переданы пустые данные в поле source или destination!"); }

            string[] arr = str.Split(':', '/');
            if (arr.Count() != 2) { throw new Exception("Строка содержит неверный формат!"); }

            // if find record in list
            foreach (var a in denyICMPgroupbyDestination)
            {
                if (a.IPaddr == arr[1])
                {
                    a.count++;
                    commonFunc.addDestinationAddr(a, destination);
                    return;
                }
            }

            // elst record not find
            denyICMPgroupbyDestination.Add(new netRecord(arr[1]));
            var aaa = denyICMPgroupbyDestination.Count();
            commonFunc.addDestinationAddr(denyICMPgroupbyDestination[aaa - 1], destination);
        }

        public static void outputList(int maxstring = Constant.DEFAULT_OUTPUT_STRING)
        {
            // sortint by count before output records
            denyICMPgroupbyDestination.Sort();

            if (maxstring == 0) { maxstring = denyICMPgroupbyDestination.Count; }
            int k = 0;
            foreach (var a in denyICMPgroupbyDestination)
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
}
