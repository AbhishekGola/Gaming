using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class Convar
    {
        private int self;

        private Convar(int self)
        {
            this.self = self;
        }

        public char[] getName()
        {
            return engine.e_mem.read_array<char>(engine.e_mem.read<int>(self + 0xC), 120);
        }

        public char[] getString()
        {
            return engine.e_mem.read_array<char>(engine.e_mem.read<int>(self + 0x24), 120);
        }

        public int getInt()
        {
            return engine.e_mem.read<int>(self + 0x30) ^ self;
        }

        public float getFloat()
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(engine.e_mem.read<int>(self + 0x2C) ^ self), 0);
        }

        public static Convar find(crc32 crc)
        {
            var a1 = engine.e_mem.read<int>(engine.e_mem.read<int>(engine.e_vt.cvar.address + 0x34));
            while ((a1 = engine.e_mem.read<int>(a1 + 0x4)) != 0)
                if (crc.compare<char>(engine.e_mem.read_array<char>(engine.e_mem.read<int>(a1 + 0xc), 120)))
                    return new Convar(a1);
            return null;
        }

        public static Convar find(string name)
        {
            return find(crc32.str_initialize(name));
        }
    }

}
