using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class engine
    {
        public static Process e_mem;
        public static VirtualTables e_vt;
        public static Netvars e_off;

        public static bool isRunning()
        {
            return e_mem.exists();
        }

        public static int getLocalPlayer()
        {
            return e_mem.read<int>(e_off.clientState + e_off.c_getLocalPlayer);
        }

        public static float[] getViewAngles()
        {
            return e_mem.read_array<float>(e_off.clientState + e_off.c_getViewAngles, 3);
        }

        public static int getMaxClients()
        {
            return e_mem.read<int>(e_off.clientState + e_off.c_getMaxClients);
        }

        public static bool isInGame()
        {
            return e_mem.read<int>(e_off.clientState + e_off.c_getState) == 6;
        }

        static public bool initialize()
        {
            if ((e_mem = Process.attach(crc32.initialize(0x3B910704, 8, 0x7106C4BC))) == null)
            {
                Console.WriteLine("[!]process is not running");
                return false;
            }
            if ((e_vt = VirtualTables.initialize()) == null)
            {
                Console.WriteLine("[!]could not initialize virtualtables");
                return false;
            }
            if ((e_off = Netvars.initialize()) == null)
            {
                Console.WriteLine("[!]could not initialize netvars");
                return false;
            }
            Console.WriteLine(
                @"[*]vtables:
                VClient:                         0x{0:X}
                VClientEntityList:               0x{1:X}
                VEngineClient:                   0x{2:X}
                VEngineCvar:                     0x{3:X}
                InputSystemVersion:              0x{4:X}
                [*]offsets:
                entityList:                      0x{5:X}
                clientState:                     0x{6:X}
                c_getLocalPlayer:                0x{7:X}
                c_getViewAngles:                 0x{8:X}
                c_getMaxClients:                 0x{9:X}
                c_getState:                      0x{10:X}
                i_button:                        0x{11:X}
                i_analog:                        0x{12:X}
                i_analogDelta:                   0x{13:X}
                [*]netvars:
                DT_BasePlayer:                   m_iHealth:           0x{14:X}
                DT_BasePlayer:                   m_vecViewOffset:     0x{15:X}
                DT_BasePlayer:                   m_lifeState:         0x{16:X}
                DT_BasePlayer:                   m_nTickBase:         0x{17:X}
                DT_BasePlayer:                   m_vecVelocity:       0x{18:X}
                DT_BasePlayer:                   m_vecPunch:          0x{19:X}
                DT_BasePlayer:                   m_iFOV:              0x{20:X}
                DT_BaseEntity:                   m_iTeamNum:          0x{21:X}
                DT_BaseEntity:                   m_vecOrigin:         0x{22:X}
                DT_CSPlayer:                     m_hActiveWeapon:     0x{23:X}
                DT_CSPlayer:                     m_iShotsFired:       0x{24:X}
                DT_CSPlayer:                     m_bIsScoped:         0x{25:X}
                DT_BaseAnimating:                m_dwBoneMatrix:      0x{26:X}
            [*]convar demo
                sensitivity:                     {27}
                volume:                          {28}
                cl_crosshairsize:                {29}
            [*]sdk info:
                creator:                         ekknod
                repo:                            github.com/ekknod/csf",
                    engine.e_vt.client.address, 
                    engine.e_vt.entity.address, 
                    engine.e_vt.engine.address, 
                    engine.e_vt.cvar.address,
                    engine.e_vt.inputsystem.address, 
                    engine.e_off.entityList, 
                    engine.e_off.clientState, 
                    engine.e_off.c_getLocalPlayer,
                    engine.e_off.c_getViewAngles, 
                    engine.e_off.c_getMaxClients, 
                    engine.e_off.c_getMaxClients, 
                    engine.e_off.i_button,
                    engine.e_off.i_analog, 
                    engine.e_off.i_analogDelta, 
                    engine.e_off.e_iHealth, 
                    engine.e_off.e_vecViewOffset,
                    engine.e_off.e_lifeState, 
                    engine.e_off.e_nTickBase, 
                    engine.e_off.e_vecVelocity, 
                    engine.e_off.e_vecPunch,
                    engine.e_off.e_iFOV, 
                    engine.e_off.e_iTeamNum, 
                    engine.e_off.e_vecOrigin, 
                    engine.e_off.e_hActiveWeapon,
                    engine.e_off.e_iShotsFired, 
                    engine.e_off.e_bIsScoped, 
                    engine.e_off.e_dwBoneMatrix,
                    Convar.find(crc32.initialize(-889421740, 11, 1387158398)).getFloat(),
                    Convar.find(crc32.initialize(938228256, 6, 2002548591)).getFloat(),
                    Convar.find(crc32.initialize(143337850, 16, 1131805743)).getInt()
                );
            return true;
        }
    }

}
