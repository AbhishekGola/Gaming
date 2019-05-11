using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class entity
    {
        public static Player getClientEntity(int index)
        {
            return new Player(engine.e_mem.read<int>(engine.e_off.entityList + index * 0x10));
        }
    }
}
