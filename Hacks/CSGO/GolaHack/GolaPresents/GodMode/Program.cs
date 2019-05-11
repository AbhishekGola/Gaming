using MemoryProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GodMode
{
    class Program
    {
        static bool StealthActive = true;
        static ProcessMemory Client;
        static ProcessMemory localPlayer;

        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;

        // BunnyHop
        static bool GodModeActive = true;

        static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSGM");
            }

            Client = processMem.GetModule("client_panorama.dll");

            if (GodModeActive)
            {
                while (true)
                {
                    localPlayer = Client.AtAddress(Client + dwLocalPlayer);
                    int myHealth = localPlayer.AtOffset(Offsets.Variables.m_iHealth).AsInteger();
                    if (myHealth < 90)
                    {
                        myHealth = 90;
                        localPlayer.AtOffset(Offsets.Variables.m_iHealth).Set(myHealth);
                    }
                    Thread.Sleep(1);
                }
            }
        }
    }
}
