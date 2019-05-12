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

        public const Int32 m_aimPunchAngle = Offsets.Variables.m_aimPunchAngle;
        public const Int32 m_aimPunchAngleVel = Offsets.Variables.m_aimPunchAngleVel;
        public const Int32 m_angEyeAnglesX = Offsets.Variables.m_angEyeAnglesX;
        public const Int32 m_angEyeAnglesY = Offsets.Variables.m_angEyeAnglesY;
        public const Int32 m_viewPunchAngle = Offsets.Variables.m_viewPunchAngle;
        public const Int32 set_abs_angles = 0x1C9770;
        public const Int32 set_abs_origin = 0x1C95B0;
        public const Int32 dwViewMatrix = 0x4CEEF94;
        public const Int32 dwClientState_State = 0x108;
        public const Int32 dwClientState = 0x58BCFC;

        private ProcessMemory playerMemory;
        private ProcessMemory Engine;
        private ProcessMemory Client;

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
                float x = PlayerBoneMatrix.AtOffset((0x30 * BoneIndex) + 0x0c).AsFloat();
                float y = PlayerBoneMatrix.AtOffset((0x30 * BoneIndex) + 0x1c).AsFloat();
                float z = PlayerBoneMatrix.AtOffset((0x30 * BoneIndex) + 0x2c).AsFloat();
                return new float[] { x, y, z };
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

        public Team Team
        {
            get
            {
                Team result = Team.GoTV;
                Team.TryParse(TeamId.ToString(), out result);
                return result;
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

        public int LifeState
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_lifeState).AsInteger();
            }
        }

        public bool isValid()
        {
            return (!IsDormant && Health > 0);
        }

        //public int XAngle

        public float[] Position_VecOrigin
        {
            get
            {
                int floatSize = sizeof(float);
                Int32 vecOrigin = Offsets.Variables.m_vecOrigin;
                float x = PlayerMemory.AtOffset(vecOrigin).AsFloat();
                float y = PlayerMemory.AtOffset(vecOrigin + floatSize).AsFloat();
                float z = PlayerMemory.AtOffset(vecOrigin + (2 * floatSize)).AsFloat();
                return new float[] { x, y, z };
            }
        }

        public float[] VecView
        {
            get
            {
                int floatSize = sizeof(float);
                Int32 viewoffset = Offsets.Variables.m_vecViewOffset;
                float x = PlayerMemory.AtOffset(viewoffset).AsFloat();
                float y = PlayerMemory.AtOffset(viewoffset + floatSize).AsFloat();
                float z = PlayerMemory.AtOffset(viewoffset + (2 * floatSize)).AsFloat();
                return new float[] { x, y, z };
            }
        }
        public float[] ViewAngle
        {
            get
            {
                float x = PlayerMemory.AtOffset(m_angEyeAnglesX).AsFloat();
                float y = PlayerMemory.AtOffset(m_angEyeAnglesY).AsFloat();
                float z = 0;
                if (x > 180)
                {
                    x -= 360;
                }
                if (x < -180)
                {
                    x += 360;
                }

                if (y > 180)
                {
                    y -= 360;
                }
                if (y < -180)
                {
                    y += 360;
                }
                return new float[] { x, y, z };
            }
            set
            {
                float x = value[0];
                float y = value[1];
                PlayerMemory.AtOffset(m_angEyeAnglesX).Set(x);
                PlayerMemory.AtOffset(m_angEyeAnglesY).Set(y);
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

        public int ShotsFiredCount
        {
            get
            {
                return playerMemory.AtOffset(Offsets.Variables.m_iShotsFired).AsInteger();
            }
        }
    }
}
