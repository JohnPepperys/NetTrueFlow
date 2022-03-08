using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    public static class netOpenConnect
    {
        public static void addOpenAddrInList(List<netRecord> list, string src, string dest)
        {
            if(string.IsNullOrEmpty(src) || string.IsNullOrEmpty(dest))
            {
                throw new Exception("Переданные пустые source или destination данные!!!");
            }

            string[] arr = src.Split(':', '/');
            if (arr.Count() < 2) { throw new Exception("Строка содержит неверный формат!"); }

            foreach (var aa in list)
            {
                if(aa.IPaddr == arr[1])
                {
                    aa.count++;
                    commonFunc.addDestinationAddr(aa, dest);
                    return;
                }
            }
            list.Add(new netRecord(arr[1]));
            var aaa = list.Count();
            commonFunc.addDestinationAddr(list[aaa - 1], dest);
        }


        public static void printInList(List<netRecord> list, int maxstring = 15)
        {
            list.Sort();

            if (maxstring == 0) { maxstring = list.Count; }
            int k = 0;
            foreach (var a in list)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t{0}\t : {1}", a.IPaddr, a.count);
                uint i = 0;
                foreach (var b in a.listDest)
                {
                    if(i == maxstring)
                    {
                        break;
                    }
                    Console.WriteLine("\t\t{0} - {1}", b.IPaddr, b.count);
                    i++;
                }
                k++;
            }
            Console.WriteLine();
        }
    }


    public static class netOpenConnectTCP 
    {
        static List<netRecord> openTCPIncomingAddress = new List<netRecord> { };

        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openTCPIncomingAddress, src, dst);
        }

        public static void printInList() { netOpenConnect.printInList(openTCPIncomingAddress); }
    }

    public static class netOpenConnectUDP
    {
        static List<netRecord> openUPDIncomingAddress = new List<netRecord> { };

        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openUPDIncomingAddress, src, dst);
        }

        public static void printInList() { netOpenConnect.printInList(openUPDIncomingAddress); }
    }

    public static class netOpenConnectICMP
    {
        static List<netRecord> openICMPIncomingAddress = new List<netRecord> { };

        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openICMPIncomingAddress, src, dst);
        }

        public static void printInList() { netOpenConnect.printInList(openICMPIncomingAddress); }
    }


    // -------------------------------------------------------------------------------------------------------------------------
    public static class netOpenDestConnectTCP
    {
        static List<netRecord> openTCPIncomeDestAddress = new List<netRecord> { };
        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openTCPIncomeDestAddress, dst, src);
        }
        public static void printInList() { netOpenConnect.printInList(openTCPIncomeDestAddress); }
    }

    public static class netOpenDestConnectUDP
    {
        static List<netRecord> openUPDIncomeDestAddress = new List<netRecord> { };
        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openUPDIncomeDestAddress, dst, src);
        }
        public static void printInList() { netOpenConnect.printInList(openUPDIncomeDestAddress); }
    }

    public static class netOpenDestConnectICMP
    {
        static List<netRecord> openICMPIncomeDestAddress = new List<netRecord> { };
        public static void addOpenAddrInList(string src, string dst)
        {
            netOpenConnect.addOpenAddrInList(openICMPIncomeDestAddress, dst, src);
        }
        public static void printInList() { netOpenConnect.printInList(openICMPIncomeDestAddress); }
    }

}
