using MemoryProcessing;
using Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AimAssistGola
{
    class Program
    {
        static bool StealthActive = true;
        static ProcessMemory Client;
        static ProcessMemory Engine;
        static ProcessMemory processMem;
        static Player self;
        static Player target;

        static Int32 m_iTeamNum = Offsets.Variables.m_iTeamNum;
        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;
        static Int32 m_iCrosshairId = Offsets.Variables.m_iCrosshairId;
        static Int32 dwForceAttack = Offsets.signatures.dwForceAttack;
        static Int32 dwClientState = Offsets.signatures.dwClientState;
        //static Int32 dwClientState = 75373728;
        static Int32 dwClientState_ViewAngles = Offsets.signatures.dwClientState_ViewAngles;

        // AIM bot Stuff
        static int current_Tick;
        static float sensitivity = 1;
        static float flsensitivity = 1;
        static int BoneIndex = 6;
        static int Smooth = 0;
        private static int current_tick, previous_tick;

        // TriggerBot
        static bool AimAssistActive = true;

        static void Main2(string[] args)
        {
            System.Diagnostics.Process process = System.Diagnostics.Process.GetProcessesByName("csgo")[0];
            processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSAA");
            }
            Client = processMem.GetModule("client_panorama.dll");
            Engine = processMem.GetModule("Engine.dll");

            while (true)
            {
                self = new Player(Client.AtAddress(Client + dwLocalPlayer));

                int MyTeamId = self.TeamId;
                //int playerInCross = self.PlayeyInCrossHairId;
                if (AimAssistActive
                    //&& playerInCross > 0 && playerInCross < 65
                    )
                {
                    //int index = playerInCross - 1;
                    //target = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + index * 0x10));

                    //if (target.IsDormant != true 
                    //    && target.Health > 0 
                    //    && MyTeamId != target.TeamId    // not in my team
                    //    && target.TeamId != 1           // not a Spectator
                    //    )
                    //{
                    aim();
                    //}
                }
                Thread.Sleep(10);
            }
        }

        static void aim()
        {
            float[] vangle;
            int FOV = self.FOV;
            vangle = self.ViewAngle;
            Int32 dwSensitivity = Offsets.signatures.dwSensitivity;
            Int32 dwSensitivityPtr = Offsets.signatures.dwSensitivityPtr;
            ProcessMemory sensitivityPtr = Client.AtOffset(Offsets.signatures.dwSensitivityPtr);
            sensitivity = Client.AtOffset(Offsets.signatures.dwSensitivity).AsFloat();
            sensitivity = 3;
            current_tick = self.TickCount;
            flsensitivity = self.isScoped ?
                (FOV / 90.0f) * sensitivity :
                sensitivity;
            if (GetAsyncKeyState(Keys.LShiftKey) != 0)
            {
                // Move code inside for buttom click check.

                if (getTarget(self, vangle) && target.isValid())
                {
                    aimAtTarget(vangle, getTargetAngle(self, target, BoneIndex), FOV, Smooth);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        static void aimAtTarget(float[] vangle, float[] angle, float fov, float smooth)
        {
            float x, y, sx, sy;


            y = vangle[0] - angle[0]; x = vangle[1] - angle[1];
            if (y > 89.0f) y = 89.0f; else if (y < -89.0f) y = -89.0f;
            if (x > 180.0f) x -= 360.0f; else if (x < -180.0f) x += 360.0f;

            //if (Math.Abs(x) / 180.0f >= fov)
            //    return;
            //if (Math.Abs(y) / 89.0f >= fov)
            //    return;
            x = ((x / flsensitivity) / 0.022f);
            y = ((y / flsensitivity) / -0.022f);
            if (smooth != 0)
            {
                sx = 0.0f; sy = 0.0f;
                if (sx < x)
                {
                    sx += 1.0f + (x / smooth);
                }
                else if (sx > x)
                {
                    sx -= 1.0f - (x / smooth);
                }
                if (sy < y)
                {
                    sy += 1.0f + (y / smooth);
                }
                else if (sy > y)
                {
                    sy -= 1.0f - (y / smooth);
                }
            }
            else
            {
                sx = x; sy = y;
            }

            if (current_tick - previous_tick > 0)
            {
                previous_tick = current_tick;
                mouse_event(0x0001, (int)sx, (int)sy, 0, 0);
            }
        }

        static bool getTarget(Player self, float[] vangle)
        {
            float best_fov;
            int i;
            Player e;
            float fov;
            int maxClients = Engine.AtAddress(Engine.Address + Offsets.signatures.dwClientState + Offsets.signatures.dwClientState_MaxPlayer).AsInteger();
            if (maxClients == 0)
            {
                maxClients = Engine.AtOffset(Offsets.signatures.dwClientState + Offsets.signatures.dwClientState_MaxPlayer).AsInteger();
            }
            if (maxClients == 0)
            {
                maxClients = 65;
            }

            best_fov = 9999f;
            for (i = 1; i < maxClients; i++)
            {
                e = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + i * 0x10));
                if (!e.isValid())
                    continue;
                if (self.TeamId == e.TeamId)
                    continue;
                fov = (float)angleBetween(vangle, getTargetAngle(self, e, BoneIndex));
                if (fov < best_fov)
                {
                    best_fov = fov;
                    target = e;
                }
            }
            return best_fov != 9999.0f;
        }

        static void sincos(float radians, ref float sine, ref float cosine)
        {
            sine = (float)Math.Sin(radians);
            cosine = (float)Math.Cos(radians);
        }

        static void angleVector(float[] angle, ref float[] forward)
        {
            float sp = 0, sy = 0, cp = 0, cy = 0;

            sincos((float)(angle[0]) * (float)(Math.PI / 180f), ref sp, ref cp);
            sincos((float)(angle[1]) * (float)(Math.PI / 180f), ref sy, ref cy);
            forward[0] = cp * cy;
            forward[1] = cp * sy;
            forward[2] = -sp;
        }

        static double dot(float[] v0, float[] v1)
        {
            return (v0[0] * v1[0] + v0[1] * v1[1] + v0[2] * v1[2]);
        }

        static float length(float[] v0)
        {
            return (v0[0] * v0[0] + v0[1] * v0[1] + v0[2] * v0[2]);
        }

        private static double angleBetween(float[] p0, float[] p1)
        {
            float[] a0 = new float[3], a1 = new float[3];
            angleVector(p0, ref a0);
            angleVector(p1, ref a1);
            return Math.Acos(dot(a0, a1) / length(a0)) * (float)(180f / Math.PI);
        }

        private static void angleNormalize(ref float[] angle)
        {
            float radius = 1f / (float)(Math.Sqrt(angle[0] * angle[0] + angle[1] * angle[1] + angle[2] * angle[2]) + 1.192092896e-07f);
            angle[0] *= radius; angle[1] *= radius; angle[2] *= radius;
        }

        static float[] getTargetAngle(Player self, Player target, int index)
        {
            target.BoneIndex = 6;
            float[] targetBonePosition = target.PlayerBonePosition;
            float[] selfVecOrigin, selfVecView;

            selfVecOrigin = self.Position_VecOrigin;
            selfVecView = self.VecView;
            selfVecOrigin[0] += selfVecView[0];
            selfVecOrigin[1] += selfVecView[1];
            selfVecOrigin[2] += selfVecView[2];

            targetBonePosition[0] -= selfVecOrigin[0];
            targetBonePosition[1] -= selfVecOrigin[1];
            targetBonePosition[2] -= selfVecOrigin[2];
            angleNormalize(ref targetBonePosition);
            vectorAngles(targetBonePosition, ref targetBonePosition);
            if (self.ShotsFiredCount > 1)
            {
                float[] selfVecPunch = self.VecPunch;
                targetBonePosition[0] -= selfVecPunch[0] * 2f;
                targetBonePosition[1] -= selfVecPunch[1] * 2f;
                targetBonePosition[2] -= selfVecPunch[2] * 2f;
            }
            vectorClamp(ref targetBonePosition);
            return targetBonePosition;
        }

        static void vectorClamp(ref float[] v)
        {
            if (v[0] > 89.0f && v[0] <= 180.0f)
                v[0] = 89.0f;
            else if (v[0] > 180.0f)
                v[0] = v[0] - 360.0f;
            else if (v[0] < -89.0f)
                v[0] = -89.0f;
            if (v[1] > 180.0f)
                v[1] -= 360.0f;
            else if (v[1] < -180.0f)
                v[1] += 360.0f;
            v[2] = 0;
        }

        static void vectorAngles(float[] forward, ref float[] angles)
        {
            float tmp, yaw, pitch;

            if (forward[1] == 0f && forward[0] == 0f)
            {
                yaw = 0;
                if (forward[2] > 0f)
                {
                    pitch = 270f;
                }
                else
                {
                    pitch = 90f;
                }
            }
            else
            {
                yaw = (float)(Math.Atan2(forward[1], forward[0]) * 180f / Math.PI);
                if (yaw < 0)
                {
                    yaw += 360f;
                }
                tmp = (float)Math.Sqrt(forward[0] * forward[0] + forward[1] * forward[1]);
                pitch = (float)(Math.Atan2(-forward[2], tmp) * 180 / Math.PI);
                if (pitch < 0)
                {
                    pitch += 360f;
                }
            }

            angles[0] = pitch;
            angles[1] = yaw;
            angles[2] = 0f;
        }

        //public Player GetClosestEnemyToCrossHair()
        //{
        //    Player enemyPlayer = null;
        //    for (int i = 0; i < 64; i++)
        //    {
        //        enemyPlayer = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + i * 0x10));
        //    }

        //    return enemyPlayer;
        //}

        //This is a replacement for Cursor.Position in WinForms
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;

        //This simulates a left mouse click
        public static bool LeftMouseClick(int xpos, int ypos)
        {
            bool result = SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            return result;
        }

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKeys);

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(MouseButtons mouseButton);
    }
}

/* DWORD cheat::Aimbot(LPVOID lpParam){
	HANDLE proc = Cheat.gameHandle;
 
	DWORD engine = RPM<DWORD>(proc, (Cheat.engineDll + Info::eng.base), sizeof(DWORD));
	DWORD myPlayer = RPM<DWORD>(proc, (Cheat.clientDll + Info::myPlayer.base), sizeof(DWORD));
 
	Vector angle;
	double Distance = 100;
	int targets = 0;
	int myTeam;
	int Team;
	int Health;
	bool dormant;
 
	while (true){
 
		if (GetAsyncKeyState(0x46)){
 
			//get targets
			for (int i = 0; i < 64; i++){
				DWORD entity = RPM<DWORD>(proc, (Cheat.clientDll + Info::ent.base + ((i - 1) * 0x10)), sizeof(DWORD));
 
				if (entity != NULL){
					myTeam = RPM<int>(proc, (myPlayer + Info::Comums.team), sizeof(int));
					Team = RPM<int>(proc, (entity + Info::Comums.team), sizeof(int));
					Health = RPM<int>(proc, (entity + Info::Comums.life), sizeof(int));
					dormant = RPM<bool>(proc, (entity + Info::ent.dormant), sizeof(bool));
				}
 
				if (dormant || Team == myTeam || Health == 0)
					continue;
 
				//a) my positon
				Vector myPos = RPM<Vector>(proc, (myPlayer + Info::Comums.origin), sizeof(Vector));
				//b) target head position
				Vector entityHeadPos = Cheat.BonePos(entity, Info::bone.boneMatrix, 10);
				//c) valid target found ++
				targets++;
 
				//d) check to find closest target and set angle to write
				if (Cheat.Distance(myPos, entityHeadPos, true) < Distance){
					Distance = Cheat.Distance(myPos, entityHeadPos, true);
					angle = Cheat.CalcAngle(myPos, entityHeadPos);
				}
			}
 
			//d) have target, write angle to target'head
			if (targets > 0){
				angle.z = 0.f;
				WPM<Vector>(proc, (engine + 0x4CE0), angle);
				LeftClick();
				Distance = 100;
				targets = 0;
				angle = { 0, 0, 0 };
			}
		}
		else{
			Distance = 100;
			targets = 0;
			angle = { 0, 0, 0 };
		}
 
		if (!Cheat.aim)
			break;
 
		Sleep(1);
	}
 
	return 0;
}
 */
