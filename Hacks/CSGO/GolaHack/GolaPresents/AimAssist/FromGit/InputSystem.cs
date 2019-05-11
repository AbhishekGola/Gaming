using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class InputSystem
    {
        public static int isButtonDown(int button) { return (engine.e_mem.read<int>(engine.e_vt.inputsystem.address + (((button >> 5) * 4) + engine.e_off.i_button)) >> (button & 31)) & 1; }
        public static int[] getMouseAnalog() { return engine.e_mem.read_array<int>(engine.e_vt.inputsystem.address + engine.e_off.i_analog, 2); }
        public static int[] getMouseAnalogDelta() { return engine.e_mem.read_array<int>(engine.e_vt.inputsystem.address + engine.e_off.i_analogDelta, 2); }
    }
}
