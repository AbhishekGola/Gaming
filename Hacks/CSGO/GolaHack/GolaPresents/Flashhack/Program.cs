using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MemoryProcessing;
using System.Diagnostics;
using Stealth;

namespace Flashhack
{
    class Program
    {
        static bool StealthActive = true;
        static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSFH");
            }

            ProcessMemory proM = processMem.GetModule("client_panorama.dll");
            while (true)
            {
                ProcessMemory localPlayer = proM.AtAddress(proM + Offsets.signatures.dwLocalPlayer);
                int MyTeamId = localPlayer.AtOffset(Offsets.Variables.m_iTeamNum).AsInteger();

                int flashDuration = localPlayer.AtOffset(Offsets.Variables.m_flFlashDuration).AsInteger();
                //int flashduration = proM.
                float valueCompare = 1081339010;
                float value = 100;
                if (flashDuration > valueCompare)
                {
                    localPlayer.AtOffset(Offsets.Variables.m_flFlashDuration).Set(value);                    
                }
            }
        }
    }
}
