using MemoryProcessing;
using Models;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;


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
        static Process process;

        static Int32 m_iTeamNum = Offsets.Variables.m_iTeamNum;
        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;
        static Int32 m_iCrosshairId = Offsets.Variables.m_iCrosshairId;
        static Int32 dwForceAttack = Offsets.signatures.dwForceAttack;
        static Int32 dwClientState = Offsets.signatures.dwClientState;
        //static Int32 dwClientState = 75373728;
        static Int32 dwClientState_ViewAngles = Offsets.signatures.dwClientState_ViewAngles;

        // AIM bot Stuff
        static int current_Tick;
        static float sensitivity=1;
        static float flsensitivity=1;
        static int BoneIndex = 6;
        static int Smooth = 0;
        private static int current_tick, previous_tick;

        // TriggerBot
        static bool AimAssistActive = true;

        static void Main(string[] args)
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
                //m_viewPunchAngle
                int playerInCross = self.PlayeyInCrossHairId;
                if (AimAssistActive &&
                    playerInCross > 0 && playerInCross < 65)
                {
                    int index = playerInCross - 1;
                    target = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + index * 0x10));

                    if (target.IsDormant != true &&
                        target.Health > 0
                       //&& MyTeamId != target.TeamId    // not in my team
                       && target.TeamId != 1           // not a Spectator
                    )
                    {
                        Aim();
                    }
                }
                Thread.Sleep(10);
            }
        }

        static void Aim()
        {
            float[] viewAngle = self.ViewAngle;
            current_tick = self.TickCount;
            //flsensitivity = self.isScoped ?
            //    (self.FOV / 90.0f) * getSensitivity() :
            //    getSensitivity();
            flsensitivity = 1;
            target.BoneIndex = BoneIndex;
            float[] playerHeadPosition = target.PlayerBonePosition;

            aimAtTarget(viewAngle, getTargetAngle(self, target), 0.0111111111111111f, Smooth);

        }

        private static float getSensitivity()
        {
            return Engine.AtAddress(Engine + Offsets.signatures.dwSensitivity).AsFloat();
        }

        static float[] getTargetAngle(Player self, Player target)
        {
            target.BoneIndex = BoneIndex;
            float[] m = target.PlayerBonePosition;
            float[] c, p;

            c = self.Position_VecOrigin;
            p = self.VecView;
            c[0] += p[0]; c[1] += p[1]; c[2] += p[2];
            m[0] -= c[0]; m[1] -= c[1]; m[2] -= c[2];
            angleNormalize(ref m);
            vectorAngles(m, ref m);
            if (self.CountShotsFired > 1)
            {
                p = self.VecPunch;
                m[0] -= p[0] * 2f; m[1] -= p[1] * 2f; m[2] -= p[2] * 2f;
            }
            vectorClamp(ref m);
            return m;
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
                yaw = (float)(Math.Atan2(forward[1], forward[0]) * 180f / 3.14159265358979323846f);
                if (yaw < 0)
                {
                    yaw += 360f;
                }
                tmp = (float)Math.Sqrt(forward[0] * forward[0] + forward[1] * forward[1]);
                pitch = (float)(Math.Atan2(-forward[2], tmp) * 180 /Math.PI);
                if (pitch < 0)
                {
                    pitch += 360f;
                }
            }
            angles[0] = pitch;
            angles[1] = yaw;
            angles[2] = 0f;
        }

        private static void angleNormalize(ref float[] angle)
        {
            float radius = 1f / (float)(Math.Sqrt(angle[0] * angle[0] + angle[1] * angle[1] + angle[2] * angle[2]) + 1.192092896e-07f);
            angle[0] *= radius; angle[1] *= radius; angle[2] *= radius;
        }

        static void aimAtTarget2(float[] vangle, float[] angle, float fov, float smooth)
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
                //SetCursorPos((int)sx, (int)sy);
            }
        }
        static void aimAtTarget(float[] vangle, float[] angle, float fov, float smooth)
        {
            //self.ViewAngle = angle;
            int x= 1000;
            int y= 400;
            mouse_event(0x01, x, y, 0, 0);
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
            bool result =SetCursorPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
            return result;
        }
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
