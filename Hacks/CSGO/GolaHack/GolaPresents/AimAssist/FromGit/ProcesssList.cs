using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class ProcessList
    {
        private byte[] snap;
        private int pos;

        private ProcessList()
        {
            uint len;
            if ((uint)NtQuerySystemInformation(57, new byte[8], 0x188, out len) != 0xC0000004
                || NtQuerySystemInformation(57, snap = new byte[len += 8192], len, out len) != 0)
                throw new Exception("[!]NtQuerySystemInformation");
            this.pos = 0;
        }

        private T copy<T>(byte[] buffer, int offset)
        {
            T[] t = new T[1];
            int size = Marshal.SizeOf<T>();
            Buffer.BlockCopy(buffer, offset, t, 0, size);
            return t[0];
        }

        public bool wow64
        {
            get { return start <= 0xffffffffU; }
        }

        public int pid
        {
            get
            {
                return copy<int>(snap, pos + 0x128);
            }
        }
        public long start
        {
            get
            {
                return copy<long>(snap, pos + 0x160);
            }
        }

        public long teb
        {
            get
            {
                return copy<long>(snap, pos + 0x168);
            }
        }

        public short[] name
        {
            get
            {
                var mem = new short[120];
                memcpy(mem, copy<long>(snap, pos + 0x40), 240);
                return mem;
            }
        }

        public ProcessList next
        {
            get
            {
                if (copy<int>(snap, pos) != 0)
                {
                    pos = copy<int>(snap, pos) + pos;
                    return this;
                }
                return null;
            }
        }

        public static ProcessList first { get { return new ProcessList().next; } }
        [DllImport("ntdll")]
        private static extern int NtQuerySystemInformation(uint p0, byte[] p1, uint p2, out uint p3);
        [DllImport("ntdll")]
        private static extern long memcpy(short[] p0, long p1, long p2);
    }
}
