using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class Process
    {
        private readonly long handle;
        private readonly bool wow64;
        private readonly long peb;

        private Process(long handle, bool wow64, long teb)
        {
            this.handle = handle;
            this.wow64 = wow64;
            this.peb = wow64 ?
                u.read<long>(handle, teb + 0x2030, 4) :
                u.read<long>(handle, teb + 0x0060, 8);
        }

        ~Process()
        {
            if (this.handle != 0)
            {
                NtClose(this.handle);
            }
        }

        public bool exists()
        {
            int code;
            GetExitCodeProcess(handle, out code);
            return code == 0x00000103;
        }

        public long getModule(crc32 crc)
        {
            int[] a0 = wow64 ?
                new int[5] { 0x04, 0x0C, 0x14, 0x28, 0x10 } :
                new int[5] { 0x08, 0x18, 0x20, 0x50, 0x20 };
            long a1 = read<long>(read<long>(this.peb + a0[1], a0[0]) + a0[2], a0[0]);
            long a2 = read<long>(a1 + a0[0], a0[0]);

            while (a1 != a2)
            {
                if (crc.compare(read_array<short>(read<long>(a1 + a0[3], a0[0]), 120)))
                    return read<long>(a1 + a0[4], a0[0]);
                a1 = read<long>(a1, a0[0]);
            }
            return 0;
        }

        public long getModule(string name)
        {
            return getModule(crc32.wcs_initialize(name));
        }

        public long getExport(long module, crc32 crc)
        {

            long a0 = read<int>(module + read<short>(module + 0x3C) + (this.wow64 ? 0x78 : 0x88)) + module;
            int[] a1 = read_array<int>(a0 + 0x18, 4);

            while (a1[0]-- > 0)
                if (crc.compare(read_array<char>(module + read<int>(module + a1[2] + (a1[0] * 4)), 120)))
                    return (module + read<int>(module + a1[1] + (read<short>(module + a1[3] + (a1[0] * 2)) * 4)));
            return 0;
        }

        public long getExport(long module, string name) { return getExport(module, crc32.str_initialize(name)); }

        private class u
        {
            public static T read<T>(long handle, long address, long size)
            {
                T[] v = new T[1];
                NtReadVirtualMemory(handle, address, v, size, 0);
                return v[0];
            }
            public static T[] read_array<T>(long handle, long address, long count)
            {
                T[] v = new T[count];
                NtReadVirtualMemory(handle, address, v, count * Marshal.SizeOf<T>(), 0);
                return v;
            }
        }

        public T[] read_array<T>(long address, long size)
        {
            return u.read_array<T>(this.handle, address, size);
        }

        public T read<T>(long address, long size)
        {
            return u.read<T>(this.handle, address, size);
        }

        public T read<T>(long address)
        {
            return u.read<T>(this.handle, address, Marshal.SizeOf<T>());
        }

        public static Process attach(crc32 crc)
        {
            Process process = null;
            long handle;
            for (var list = ProcessList.first; list != null; list = list.next)
            {
                handle = OpenProcess(0x1000 | 0x0010, 0, list.pid);
                if (crc.compare<short>(list.name))
                    process = new Process(handle, list.wow64, list.teb);
            }
            return process;
        }
        public static Process attach(string name)
        {
            return attach(crc32.wcs_initialize(name));
        }

        [DllImport("kernel32")]
        public static extern long OpenProcess(uint p0, uint p1, int p2);
        [DllImport("kernel32")]
        public static extern int GetExitCodeProcess(long p0, out int p1);
        [DllImport("ntdll")]
        public static extern int NtReadVirtualMemory(long p0, long p1, [Out, MarshalAs(UnmanagedType.AsAny)] object p2, long p3, long p4);
        [DllImport("ntdll")]
        public static extern int NtClose(long p0);
    }
}
