using NetTrueFlow;
using System.Collections.Generic;

namespace NetTrueFlowWeb
{
    public class outputRecord
    {
        public string sourceS_IP { get; set; }
        public string sourceD_IP;
        public ulong sourcec;

        public string destS_IP;
        public string destD_IP;
        public ulong destc;
    }
    

    
    public class DenyOutput
    {
        public static List<outputRecord> outputTCP = new List<outputRecord> { };

        struct tmpdata { 
            public string src { get; set; } 
            public string dst { get; set; } 
            public ulong ct { get; set; } 
        }

        public void DenyTCPOutput() {
            
            // sorted get arrays
            netListTCP.denyTCPIncomingAddress.Sort();
            netDenyDestinationTCP.denyTCPgroupbyDestination.Sort();

            // defines variables
            var maxsource = netListTCP.denyTCPIncomingAddress.Count;
            var maxdest = netDenyDestinationTCP.denyTCPgroupbyDestination.Count;
            tmpdata[] srctmp = new tmpdata[Constant.DEFAULT_OUTPUT_STRING * Constant.DEFAULT_OUTPUT_STRING_2];
            tmpdata[] dsttmp = new tmpdata[Constant.DEFAULT_OUTPUT_STRING * Constant.DEFAULT_OUTPUT_STRING_2];

            for (int i = 0, k = 0; i < Constant.DEFAULT_OUTPUT_STRING; i++)
            {
                if (i < maxsource)
                {
                    var ddd = netListTCP.denyTCPIncomingAddress[i].listDest;
                    var dddmax = ddd.Count;
                    for (int j = 0; j < Constant.DEFAULT_OUTPUT_STRING_2; j++, k++)
                    {
                        srctmp[k].src = netListTCP.denyTCPIncomingAddress[i].IPaddr;
                        if (j < dddmax) {
                            srctmp[k].dst = ddd[j].IPaddr;
                            srctmp[k].ct = ddd[j].count;
                        }
                    }
                }
            }

            for (int i = 0, k = 0; i < Constant.DEFAULT_OUTPUT_STRING; i++)
            {
                if (i < maxdest)
                {
                    var ddd = netDenyDestinationTCP.denyTCPgroupbyDestination[i].listDest;
                    var dddmax = ddd.Count;
                    for (int j = 0; j < Constant.DEFAULT_OUTPUT_STRING_2; j++, k++)
                    {
                        dsttmp[k].src = netDenyDestinationTCP.denyTCPgroupbyDestination[i].IPaddr;
                        if (j < dddmax)
                        {
                            dsttmp[k].dst = ddd[j].IPaddr;
                            dsttmp[k].ct = ddd[j].count;
                        }
                    }
                }
            }

            for(int k = 0; k < Constant.DEFAULT_OUTPUT_STRING * Constant.DEFAULT_OUTPUT_STRING_2; k++)
            {
                outputTCP.Add(new outputRecord() { sourceS_IP = srctmp[k].src, sourceD_IP = srctmp[k].dst, sourcec = srctmp[k].ct,
                                                    destS_IP = dsttmp[k].src, destD_IP = dsttmp[k].dst, destc = dsttmp[k].ct });
            }
        }
    }
}
