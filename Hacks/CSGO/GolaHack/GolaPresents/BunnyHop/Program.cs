using MemoryProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BunnyHop
{
    class Program
    {
        static bool StealthActive = true;
        static ProcessMemory Client;
        static ProcessMemory localPlayer;

        static Int32 m_iTeamNum = Offsets.Variables.m_iTeamNum;
        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;
        static Int32 m_iCrosshairId = Offsets.Variables.m_iCrosshairId;
        static Int32 m_fFlags = Offsets.Variables.m_fFlags;
        static Int32 dwForceJump = Offsets.signatures.dwForceJump;
        // BunnyHop
        static bool BunnyHopActive = true;

        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(Keys vKeys);

        static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSBH");
            }

            Client = processMem.GetModule("client_panorama.dll");

            while (true)
            {
                localPlayer = Client.AtAddress(Client + dwLocalPlayer);
                int MyTeamId = localPlayer.AtOffset(m_iTeamNum).AsInteger();
                while (GetAsyncKeyState(Keys.Space)!=0)
                {
                    ProcessMemory jumpFlagProcess = localPlayer.AtOffset(m_fFlags);
                    int jumpFlag = jumpFlagProcess.AsInteger();

                    if (jumpFlag == 257)
                    {
                        ProcessMemory ForceJump = Client.AtOffset(dwForceJump);
                        ForceJump.Set(5);
                        ForceJump.Set(4);
                    }
                }
                Thread.Sleep(1);
            }

        }
    }
}
