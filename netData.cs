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
        List<string> denyIncomingAddress = new List<string> { };
        List<string> sourceOutgoingAddress = new List<string> { };
         
        public netData(string s)
        {
            inputFile = s;
        }

        public void parsingData()
        {

        }

        public void outputResult()
        {

        }
    }
}
