using System;
using System.Runtime.InteropServices;

namespace AimAssist
{
    public class crc32
    {
        public readonly int c, l, i;
        crc32(int crc, int length, int initial)
        {
            c = crc; l = length; i = initial;
        }

        public static crc32 initialize(int crc, int length, int initial)
        {
            return new crc32(crc, length, initial);
        }

        public static crc32 initialize<T>(T[] name)
        {
            var initial = new Random(Guid.NewGuid().GetHashCode()).Next();
            var length = name.Length;
            var crc = RtlCrc32(name, length, initial);
            return new crc32(crc, length, initial);
        }

        public static crc32 wcs_initialize(string name)
        {
            return initialize(strwcs(name));
        }

        public static crc32 str_initialize(string name)
        {
            return initialize(name.ToCharArray());
        }

        private static short[] strwcs(string n)
        {
            int a0 = n.Length; short[] a2 = new short[a0];
            while (--a0 > -1 && (a2[a0] = (short)(char)n[a0]) != (short)0) ;
            return a2;
        }

        public bool compare<T>(T[] v)
        {
            return RtlCrc32(v, l, i) == c;
        }

        public bool compare(char[] v, int extra_len)
        {
            return compare(v) && strlen(v) - l <= extra_len;
        }

        public bool compare(short[] v, int extra_len)
        {
            return compare(v) && wcslen(v) - l <= extra_len;
        }

        [DllImport("ntdll")]
        private static extern int RtlCrc32([In, MarshalAs(UnmanagedType.AsAny)] object p0, long p1, int p2);
        [DllImport("ntdll")]
        private static extern int strlen(char[] target);
        [DllImport("ntdll")]
        private static extern int wcslen(short[] target);
    }
}
