using MemoryProcessing;
using Models;
using System;
using System.Diagnostics;
using System.Threading;

namespace TriggerBot
{
    class Program
    {
        static bool StealthActive = true;
        static ProcessMemory Client;
        static Player localPlayer;

        static Int32 m_iTeamNum = Offsets.Variables.m_iTeamNum;
        static Int32 dwLocalPlayer = Offsets.signatures.dwLocalPlayer;
        static Int32 m_iCrosshairId = Offsets.Variables.m_iCrosshairId;
        static Int32 dwForceAttack = Offsets.signatures.dwForceAttack;

        // TriggerBot
        static bool TriggerBotActive = true;

        static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSTB");
            }

            Client = processMem.GetModule("client_panorama.dll");

            while (true)
            {
                localPlayer = new Player(Client.AtAddress(Client + dwLocalPlayer));
                int MyTeamId = localPlayer.TeamId;

                int playerInCross = localPlayer.PlayeyInCrossHairId;
                if (TriggerBotActive &&
                    playerInCross > 0 && playerInCross < 65)
                {
                    int i = playerInCross - 1;
                    Player playerinCrossHair = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + i * 0x10));
                    int crossHairPlayerHealth = playerinCrossHair.Health;
                    // 1 GoTV; 2 T; 3 CT
                    int crossHairPlayerTeamID = playerinCrossHair.TeamId;

                    bool crossHairDormant = playerinCrossHair.IsDormant;

                    if (crossHairDormant != true &&
                        crossHairPlayerHealth > 0
                        && MyTeamId != crossHairPlayerTeamID    // not in my team
                       && crossHairPlayerTeamID != 1           // not a Spectator
                    )
                    {
                        ProcessMemory AttackAddress = Client.AtOffset(dwForceAttack);
                        AttackAddress.Set(1);
                        Thread.Sleep(200);
                        AttackAddress.Set(4);
                    }
                }
                Thread.Sleep(10);
            }
        }
    }
}
