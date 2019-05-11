using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class InterfaceTable
    {
        private int self;
        private InterfaceTable(int self) { this.self = self; }
        public VirtualTable getInterface(crc32 crc)
        {
            var a0 = self;
            do
            {
                if (crc.compare(engine.e_mem.read_array<char>(engine.e_mem.read<int>(a0 + 0x4), 120), 4))
                {
                    return new VirtualTable(engine.e_mem.read<int>(engine.e_mem.read<int>(a0) + 1));
                }
            } while ((a0 = engine.e_mem.read<int>(a0 + 0x8)) != 0);
            throw new Exception("VirtualTable::getInterface");
        }

        public VirtualTable getInterface(string name)
        {
            return getInterface(crc32.str_initialize(name));
        }

        public static InterfaceTable open(crc32 crc)
        {
            var v = engine.e_mem.getExport(engine.e_mem.getModule(crc), crc32.initialize(0x1617BEAF, 15, 0x371FA0));
            return v != 0 ? new InterfaceTable(engine.e_mem.read<int>(engine.e_mem.read<int>(v - 0x6A))) : null;
        }

        public static InterfaceTable open(string dll_name)
        {
            return open(crc32.wcs_initialize(dll_name));
        }
    }

}
