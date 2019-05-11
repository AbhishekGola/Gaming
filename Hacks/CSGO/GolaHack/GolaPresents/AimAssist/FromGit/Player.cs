using MemoryProcessing;
using Models;
using Offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AimAssist
{
    public class Player
    {
        public int self;

        public Player(int self) { this.self = self; }
        public bool isScoped()
        {
            return engine.e_mem.read<bool>(self + engine.e_off.e_bIsScoped);
        }

        public int getTeam()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_iTeamNum);
        }

        public int getHealth()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_iHealth);
        }

        public int getLifeState()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_lifeState);
        }

        public int getTickCount()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_nTickBase);
        }

        public int getShotsFired()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_iShotsFired);
        }

        public int getFov()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_iFOV);
        }

        private int getActiveWeapon()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_hActiveWeapon);
        }

        public int getWeapon()
        {
            return engine.e_mem.read<int>(engine.e_off.entityList + ((getActiveWeapon() & 0xFFF) - 1) * 0x10);
        }

        public float[] getVecOrigin()
        {
            return engine.e_mem.read_array<float>(self + engine.e_off.e_vecOrigin, 3);
        }

        public float[] getVecView()
        {
            return engine.e_mem.read_array<float>(self + engine.e_off.e_vecViewOffset, 3);
        }

        public float[] getVecVelocity()
        {
            return engine.e_mem.read_array<float>(self + engine.e_off.e_vecVelocity, 3);
        }

        public float[] getVecPunch()
        {
            return engine.e_mem.read_array<float>(self + engine.e_off.e_vecPunch, 3);
        }

        public float[] getEyePos()
        {
            float[] v, o;
            v = getVecView();
            o = getVecOrigin();
            return new float[] { v[0] + o[0], v[1] + o[1], v[2] + o[2] };
        }

        public float[] getBonePos(int i)
        {
            return new float[] {
            engine.e_mem.read<float>(getBoneMatrix() + 0x30 * i + 0x0C),
            engine.e_mem.read<float>(getBoneMatrix() + 0x30 * i + 0x1C),
            engine.e_mem.read<float>(getBoneMatrix() + 0x30 * i + 0x2C)
                    };
        }

        private int getBoneMatrix()
        {
            return engine.e_mem.read<int>(self + engine.e_off.e_dwBoneMatrix);
        }
        
        public bool isValid()
        {
            int health = getHealth();
            return self != 0 && getLifeState() == 0 && health > 0 && health < 1337;
        }
        //public int XAngle
    }
}
