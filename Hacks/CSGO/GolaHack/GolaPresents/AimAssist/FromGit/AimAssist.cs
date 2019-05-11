using MemoryProcessing;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace AimAssist
{
    public class AimAssist
    {
        private static Player target = new Player(0);
        private static Convar sensitivity;
        private static Convar mp_teammates_are_enemies;
        private static int current_tick, previous_tick;
        private static float flsensitivity;

        private static Models.Player localPlayer;
        static ProcessMemory Client;
        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;

        private static int BoneIndex = 8;
        private static float Smooth = 8f;

        public static void Main2()
        {
            if (!engine.initialize())
            {
                return;
            }

            Stealth.Stealth.Hide("CSAA");

            sensitivity = Convar.find(crc32.initialize(-889421740, 11, 1387158398));
            mp_teammates_are_enemies = Convar.find(crc32.initialize(1235347760, 24, 304350720));

            while (engine.isRunning())
            {
                if (engine.isInGame())
                {
                    aim();
                }
            }
        }

        static void aim()
        {
            Player self;
            float[] vangle;

            self = entity.getClientEntity(engine.getLocalPlayer());
            vangle = engine.getViewAngles();
            current_tick = self.getTickCount();
            flsensitivity = self.isScoped() ?
                (self.getFov() / 90.0f) * sensitivity.getFloat() :
                sensitivity.getFloat();
            if (InputSystem.isButtonDown(107) == 1 || InputSystem.isButtonDown(111) == 1)
            {
                //localPlayer = new Models.Player(Client.AtAddress(Client + dwLocalPlayer));
                if (!getTarget(self, vangle) && !target.isValid() 
                    //&& localPlayer.PlayeyInCrossHairId > 0 && localPlayer.PlayeyInCrossHairId < 65
                    )
                    return;
                aimAtTarget(vangle, getTargetAngle(self, target, BoneIndex), 0.0111111111111111f, Smooth);
                if (!target.isValid())
                {
                    Thread.Sleep(1000);
                }
            }
            else
            {
                target.self = 0;
            }
        }

        static void aimAtTarget(float[] vangle, float[] angle, float fov, float smooth)
        {
            float x, y, sx, sy;


            y = vangle[0] - angle[0]; x = vangle[1] - angle[1];
            if (y > 89.0f) y = 89.0f; else if (y < -89.0f) y = -89.0f;
            if (x > 180.0f) x -= 360.0f; else if (x < -180.0f) x += 360.0f;

            //if (fabs(x) / 180.0f >= fov)
            //    return;
            //if (fabs(y) / 89.0f >= fov)
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

            best_fov = 9999f;
            for (i = 1; i < engine.getMaxClients(); i++)
            {
                e = entity.getClientEntity(i);
                if (!e.isValid())
                    continue;
                if (mp_teammates_are_enemies.getInt() == 0 && self.getTeam() == e.getTeam())
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
            sine = (float)sin(radians);
            cosine = (float)cos(radians);
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
            float radius = 1f / (float)(sqrt(angle[0] * angle[0] + angle[1] * angle[1] + angle[2] * angle[2]) + 1.192092896e-07f);
            angle[0] *= radius; angle[1] *= radius; angle[2] *= radius;
        }

        static float[] getTargetAngle(Player self, Player target, int index)
        {
            float[] m = target.getBonePos(index);
            float[] c, p;

            c = self.getVecOrigin();
            p = self.getVecView();
            c[0] += p[0]; c[1] += p[1]; c[2] += p[2];
            m[0] -= c[0]; m[1] -= c[1]; m[2] -= c[2];
            angleNormalize(ref m);
            vectorAngles(m, ref m);
            if (self.getShotsFired() > 1)
            {
                p = self.getVecPunch();
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
                yaw = (float)(atan2(forward[1], forward[0]) * 180f / 3.14159265358979323846f);
                if (yaw < 0)
                {
                    yaw += 360f;
                }
                tmp = (float)sqrt(forward[0] * forward[0] + forward[1] * forward[1]);
                pitch = (float)(atan2(-forward[2], tmp) * 180 / 3.14159265358979323846f);
                if (pitch < 0)
                {
                    pitch += 360f;
                }
            }
            angles[0] = pitch;
            angles[1] = yaw;
            angles[2] = 0f;
        }

        [DllImport("ntdll")]
        public static extern double atan2(double x, double y);
        [DllImport("ntdll")]
        public static extern double sqrt(double p0);
        [DllImport("ntdll")]
        public static extern double fabs(double p0);
        [DllImport("ntdll")]
        public static extern double sin(double p0);
        [DllImport("ntdll")]
        public static extern double cos(double p0);
        [DllImport("user32")]
        public static extern int mouse_event(int p0, int p1, int p2, int p3, long p4);
    }
}
