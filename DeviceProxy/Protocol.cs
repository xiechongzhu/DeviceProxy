using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeviceProxy
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    struct ProtocolHeader
    {
        public Int32 Channel;
        public Int32 DataLength;
    }
}
