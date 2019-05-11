using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    class Netvars
    {
        public int entityList, clientState;
        public int c_getLocalPlayer, c_getViewAngles, c_getMaxClients, c_getState;
        public int i_button, i_analog, i_analogDelta, e_iHealth;
        public int e_vecViewOffset, e_lifeState, e_nTickBase, e_vecVelocity;
        public int e_vecPunch, e_iFOV, e_iTeamNum, e_vecOrigin;
        public int e_hActiveWeapon, e_iShotsFired, e_bIsScoped, e_dwBoneMatrix;

        public static Netvars initialize()
        {
            var nv = new Netvars();
            NetvarTable t;

            nv.entityList = engine.e_vt.entity.address - (engine.e_mem.read<int>(engine.e_vt.entity.function(5) + 0x22) - 0x38);
            nv.clientState = engine.e_mem.read<int>(engine.e_mem.read<int>(engine.e_vt.engine.function(18) + 0x16));
            nv.c_getLocalPlayer = engine.e_mem.read<int>(engine.e_vt.engine.function(12) + 0x16);
            nv.c_getViewAngles = engine.e_mem.read<int>(engine.e_vt.engine.function(19) + 0xB2);
            nv.c_getMaxClients = engine.e_mem.read<int>(engine.e_vt.engine.function(20) + 0x07);
            nv.c_getState = engine.e_mem.read<int>(engine.e_vt.engine.function(26) + 0x07);
            nv.i_button = engine.e_mem.read<int>(engine.e_vt.inputsystem.function(15) + 0x21D);
            nv.i_analog = engine.e_mem.read<int>(engine.e_vt.inputsystem.function(18) + 0x09);
            nv.i_analogDelta = engine.e_mem.read<int>(engine.e_vt.inputsystem.function(18) + 0x29);
            try
            {
                t = NetvarTable.open(crc32.initialize(-518714413, 13, 527684971));                       /* DT_BasePlayer */
                nv.e_iHealth = t.getOffset(crc32.initialize(1633193003, 9, 1145823205));       /* m_iHealth */
                nv.e_vecViewOffset = t.getOffset(crc32.initialize(1820487808, 18, 1153529757));      /* m_vecViewOffset[0] */
                nv.e_lifeState = t.getOffset(crc32.initialize(-274821372, 11, 746473911));       /* m_lifeState */
                nv.e_nTickBase = t.getOffset(crc32.initialize(-1409136347, 11, 1142360283));     /* m_nTickBase */
                nv.e_vecVelocity = t.getOffset(crc32.initialize(1830428536, 16, 1177502929));      /* m_vecVelocity[0] */
                nv.e_vecPunch = t.getOffset(crc32.initialize(1341626896, 7, 228412478)) + 0x70; /* m_Local */
                nv.e_iFOV = t.getOffset(crc32.initialize(1362244146, 6, 93949321));         /* m_iFOV */
                t = NetvarTable.open(crc32.initialize(66898466, 13, 949955796));                         /* DT_BaseEntity */
                nv.e_iTeamNum = t.getOffset(crc32.initialize(-670822643, 10, 1040583495));      /* m_iTeamNum */
                nv.e_vecOrigin = t.getOffset(crc32.initialize(1857655783, 11, 883947371));       /* m_vecOrigin */
                t = NetvarTable.open(crc32.initialize(-1331763933, 11, 866751482));                      /* DT_CSPlayer */
                nv.e_hActiveWeapon = t.getOffset(crc32.initialize(96205189, 15, 125279536));         /* m_hActiveWeapon */
                nv.e_iShotsFired = t.getOffset(crc32.initialize(1111863503, 13, 1803608319));      /* m_iShotsFired */
                nv.e_bIsScoped = t.getOffset(crc32.initialize(-2113269303, 11, 991466299));      /* m_bIsScoped */
                t = NetvarTable.open(crc32.initialize(243038784, 16, 895501995));                        /* DT_BaseAnimating */
                nv.e_dwBoneMatrix = t.getOffset(crc32.initialize(-960476223, 12, 235609196)) + 0x1C; /* m_nForceBone */
            }
            catch
            {
                return null;
            }
            return nv;
        }
    }

}
