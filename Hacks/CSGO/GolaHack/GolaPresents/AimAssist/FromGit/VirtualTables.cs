using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class VirtualTables
    {
        public VirtualTable client;
        public VirtualTable entity;
        public VirtualTable engine;
        public VirtualTable cvar;
        public VirtualTable inputsystem;

        public static VirtualTables initialize()
        {
            VirtualTables vt = new VirtualTables();
            InterfaceTable t;

            try
            {
                t = InterfaceTable.open(crc32.initialize(322218740, 19, 52673840));                 /* client_panorama.dll */
                vt.client = t.getInterface(crc32.initialize(-2117240988, 7, 517220328));   /* VClient  */
                vt.entity = t.getInterface(crc32.initialize(988898746, 17, 584443012));    /* VClientEntityList */
                t = InterfaceTable.open(crc32.initialize(1830937564, 10, 164455238));               /* engine.dll */
                vt.engine = t.getInterface(crc32.initialize(1246076804, 13, 1237105580));  /* VEngineClient */
                t = InterfaceTable.open(crc32.initialize(-1266026105, 11, 1598327702));             /* vstdlib.dll */
                vt.cvar = t.getInterface(crc32.initialize(-1014352715, 11, 1570200836)); /* VEngineCvar */
                t = InterfaceTable.open(crc32.initialize(-288952189, 15, 1365201083));              /* inputsystem.dll */
                vt.inputsystem = t.getInterface(crc32.initialize(-1692259331, 18, 1628494329)); /* InputSystemVersion */
            }
            catch
            {
                return null;
            }
            return vt;
        }
    }

}
