using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    public class netOpenConnect
    {
        public void addOpenAddrInList(List<netRecord> list, string src, string dest)
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


        public void printInList(List<netRecord> list, int maxstring = 15)
        {
            list.Sort();

            if (maxstring == 0) { maxstring = list.Count; }
            int k = 0;
            foreach (var a in list)
            {
                if (k == maxstring) { break; }
                Console.WriteLine("\t{0}\t : {1}", a.IPaddr, a.count);
                foreach (var b in a.listDest)
                {
                    Console.WriteLine("\t\t{0} - {1}", b.IPaddr, b.count);
                }
                k++;
            }
            Console.WriteLine();
        }
    }


    public class netOpenConnectTCP : netOpenConnect
    {
        static List<netRecord> openTCPIncomingAddress = new List<netRecord> { };

        public void addOpenAddrInList(string src, string dst)
        {
            addOpenAddrInList(openTCPIncomingAddress, src, dst);
        }

        public void printInList() { printInList(openTCPIncomingAddress); }
    }

    public class netOpenConnectUDP : netOpenConnect
    {
        static List<netRecord> openUPDIncomingAddress = new List<netRecord> { };

        public void addOpenAddrInList(string src, string dst)
        {
            addOpenAddrInList(openUPDIncomingAddress, src, dst);
        }

        public void printInList() { printInList(openUPDIncomingAddress); }
    }

    public class netOpenConnectICMP : netOpenConnect
    {
        static List<netRecord> openICMPIncomingAddress = new List<netRecord> { };

        public void addOpenAddrInList(string src, string dst)
        {
            addOpenAddrInList(openICMPIncomingAddress, src, dst);
        }

        public void printInList() { printInList(openICMPIncomingAddress); }
    }

}
