using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class VirtualTable
    {
        private int self;
        public VirtualTable(int self) { this.self = self; }
        public int address { get { return this.self; } }
        public int function(int index) { return engine.e_mem.read<int>(engine.e_mem.read<int>(self) + index * 4); }
    }

}
