using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalDriver.Internal.Windows
{
    internal static class Structures
    {
        public struct SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid ClassGuid;
            public int DevInst;
            public UIntPtr Reserved;

            public static readonly SP_DEVINFO_DATA Empty = default(SP_DEVINFO_DATA);
        }
        public struct SP_DEVICE_INTERFACE_DATA
        {
            public int cbSize;
            public Guid InterfaceClassGuid;
            public Constants.SPDeviceInterfaceDataFlags Flags;
            public UIntPtr Reserved;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        public struct SP_DEVICE_INTERFACE_DETAIL_DATA
        {
            public int cbSize;
            public short DevicePath;

            public static readonly SP_DEVICE_INTERFACE_DETAIL_DATA Empty = default(SP_DEVICE_INTERFACE_DETAIL_DATA);
        }
    }
}
