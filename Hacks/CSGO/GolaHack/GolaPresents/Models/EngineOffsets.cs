using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryProcessing
{
    public class EngineOffsets
    {
        private ProcessMemory engine;

        public EngineOffsets(ProcessMemory process)
        {
            engine = process.GetModule("Engine.dll");
        }

        public ProcessMemory Engine
        {
            get
            {
                return engine;
            }
        }

        public ProcessMemory Clientstate
        {
            get
            {
                return engine.AtOffset(Offsets.signatures.dwClientState);
            }
        }

        public float[] viewAngles
        {
            get
            {
                float x = Clientstate.AtOffset(Offsets.signatures.dwClientState_ViewAngles).AsFloat();
                float y = Clientstate.AtOffset(Offsets.signatures.dwClientState_ViewAngles + sizeof(float)).AsFloat();
                float z = Clientstate.AtOffset(Offsets.signatures.dwClientState_ViewAngles + 2 * sizeof(float)).AsFloat();

                return new float[] { x, y, z };
            }
        }
    }
}
