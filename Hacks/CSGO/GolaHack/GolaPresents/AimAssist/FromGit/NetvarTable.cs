using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class NetvarTable
    {
        private int self;
        public NetvarTable(int self) { this.self = self; }
        public int getOffset(string name) { return getOffset(self, crc32.str_initialize(name)); }
        public int getOffset(crc32 crc) { return getOffset(self, crc); }
        private int getOffset(int address, crc32 crc)
        {
            int a0 = 0, a1, a2, a3, a4, a5;

            for (a1 = 0; a1 < engine.e_mem.read<int>(address + 0x4); a1++)
            {
                a2 = a1 * 60 + engine.e_mem.read<int>(address);
                a3 = engine.e_mem.read<int>(a2 + 0x2C);
                if ((a4 = engine.e_mem.read<int>(a2 + 0x28)) != 0 && engine.e_mem.read<int>(a4 + 0x4) != 0)
                    if ((a5 = getOffset(a4, crc)) != 0)
                        a0 += a3 + a5;
                if (crc.compare<char>(engine.e_mem.read_array<char>(engine.e_mem.read<int>(a2), 120)))
                    return a3 + a0;
            }
            return a0;
        }
        public static NetvarTable open(crc32 crc)
        {
            int a0, a1;

            a0 = engine.e_mem.read<int>(engine.e_mem.read<int>(engine.e_vt.client.function(8) + 1));
            do
            {
                a1 = engine.e_mem.read<int>(a0 + 0xC);
                if (crc.compare(engine.e_mem.read_array<char>(engine.e_mem.read<int>(a1 + 0xC), 120), 1))
                    return new NetvarTable(a1);
            } while ((a0 = engine.e_mem.read<int>(a0 + 0x10)) != 0);
            throw new Exception("NetvarTable::open");
        }
        public static NetvarTable open(string name)
        {
            return open(crc32.str_initialize(name));
        }
    }
}
