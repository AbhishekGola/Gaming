using MemoryProcessing;
using Offsets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Player
    {
        private ProcessMemory playerMemory;

        public Player(ProcessMemory playerMemory)
        {
            PlayerMemory = playerMemory;
        }

        public ProcessMemory PlayerMemory
        {
            get { return playerMemory; }
            set { playerMemory = value; }
        }

        public int BoneIndex
        {
            get;
            set;
        }

        public int TickCount
        {
            get
            {
                return PlayerMemory.AtOffset(Offsets.Variables.m_nTickBase);
            }
        }

        public int PlayeyInCrossHairId
        {
            get
            {
                return PlayerMemory.AtOffset(Offsets.Variables.m_iCrosshairId).AsInteger();
            }
        }

        //b_headLocation[0] = ((bPointer + 0x30) * (6 +0x0c));
        //b_headLocation[1] = ((bPointer + 0x30) * (6 +0x1c));
        //b_headLocation[2] = ((bPointer + 0x30) * (6 +0x2c));
        ProcessMemory PlayerBoneMatrix
        {
            get
            {
                return PlayerMemory.AtOffset(Variables.m_dwBoneMatrix);
            }
        }

        private float[] playerBonePosition;
        public float[] PlayerBonePosition
        {
            get
            {
                return new float[]
                {
                    PlayerBoneMatrix.AtOffset(0x30 * (BoneIndex + 0x0c)).AsFloat(),
                    PlayerBoneMatrix.AtOffset(0x30 * (BoneIndex + 0x1c)).AsFloat(),
                    PlayerBoneMatrix.AtOffset(0x30 * (BoneIndex + 0x2c)).AsFloat()
                };
            }
        }

        public int Health
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iHealth).AsInteger();
            }
            set
            {
                playerMemory.AtOffset(Offsets.Variables.m_iHealth).Set(value);
            }
        }

        public int TeamId
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iTeamNum).AsInteger();
            }
            set
            {
                playerMemory.AtOffset(Offsets.Variables.m_iTeamNum).Set(value);
            }
        }

        public bool IsDormant
        {
            get
            {
                return playerMemory.AtOffset(Offsets.signatures.m_bDormant).AsBool();
            }
            set
            {
                playerMemory.AtOffset(Offsets.Variables.m_iHealth).Set(value);
            }
        }
        //public int XAngle

        public float[] VecOrigin
        {
            get
            {
                return new float[]
                {
                   PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecOrigin+ 0x0c)).AsFloat(),
                   PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecOrigin+ 0x1c)).AsFloat(),
                   PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecOrigin+ 0x2c)).AsFloat()
                };
            }
        }

        public float[] VecView
        {
            get
            {
                return new float[]
                {
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecViewOffset+ 0x0c)).AsFloat(),
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecViewOffset+ 0x1c)).AsFloat(),
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_vecViewOffset+ 0x2c)).AsFloat()
                };
            }
        }

        public float[] VecPunch
        {
            get
            {
                return new float[]
                {
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_Local+ 0x0c)).AsFloat(),
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_Local+ 0x1c)).AsFloat(),
                    PlayerMemory.AtOffset(0x30 * (Offsets.Variables.m_Local+ 0x2c)).AsFloat()
                };

            }
        }

        public int CountShotsFired
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iShotsFired).AsInteger();
            }
        }

        public int FOV
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iFOV).AsInteger();
            }
        }

        public bool isScoped
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_bIsScoped).AsBool();
            }
        }

        public int GlowIndex
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iGlowIndex).AsInteger();
            }
        }
    }
}
