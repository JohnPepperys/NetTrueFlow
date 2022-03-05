using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetTrueFlow
{
    internal class netRecord : IComparable<netRecord>
    {
        public string IPaddr { get; }
        public int count { get; set; } = 1;
        public netRecord(string ipaddr) => IPaddr = ipaddr;

        public int CompareTo(netRecord? rec)
        {
            if (rec is null) throw new ArgumentException("Некорректное значение параметра");
            return rec.count - count;
        }
    }

}
