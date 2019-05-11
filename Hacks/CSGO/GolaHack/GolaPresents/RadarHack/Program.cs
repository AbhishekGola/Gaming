using MemoryProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RadarHack
{
    public class Program
    {
        static bool StealthActive = true;
        static ProcessMemory Client;
        static ProcessMemory localPlayer;

        static int m_iTeamNum = Offsets.Variables.m_iTeamNum;
        static int m_bSpotted = Offsets.Variables.m_bSpotted;

        // Radar
        static bool RadarHackActive = true;

        public static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSRH");
            }

            Client = processMem.GetModule("client_panorama.dll");
            int getMaxPlayers = 32;

            while (true)
            {
                localPlayer = Client.AtAddress(Client + Offsets.signatures.dwLocalPlayer);
                int MyTeamId = localPlayer.AtOffset(m_iTeamNum).AsInteger();

                // Rotate through all players in game.
                for (int i = 0; i < getMaxPlayers; i++)
                {
                    ProcessMemory currentPlayer = Client.AtAddress(Client + Offsets.signatures.dwEntityList + i * 0x10);

                    bool currentPlayerDormant = currentPlayer.AtOffset(Offsets.signatures.m_bDormant).AsBool();

                    // 1 GoTV; 2 T; 3 CT
                    int EntityBaseTeamID = currentPlayer.AtOffset(m_iTeamNum).AsInteger();

                    if (currentPlayerDormant || EntityBaseTeamID == 0)
                    {
                        continue;
                    }

                    if (RadarHackActive)
                    {
                        // Enable Glow
                        ShowCurrentPlayer(currentPlayer, EntityBaseTeamID);
                    }
                }

                Thread.Sleep(1);
            }
        }

        static void ShowCurrentPlayer(ProcessMemory currentPlayer, int EntityBaseTeamID)
        {
            ProcessMemory spotted = currentPlayer.AtOffset(m_bSpotted);
            bool isSpotted = spotted.AsBool();
            if (!isSpotted)
            {
                spotted.Set(true);
            }
        }
    }
}
