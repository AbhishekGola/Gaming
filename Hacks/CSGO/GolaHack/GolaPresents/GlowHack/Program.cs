using MemoryProcessing;
using Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GlowHack
{
    public class Program
    {
        static bool StealthActive = true;
        static bool GlowActive = true;
        static ProcessMemory Client;
        static Player localPlayer;
        static ProcessMemory glow_Pointer;

        static int dwGlowObjectManager = Offsets.signatures.dwGlowObjectManager;

        //Glow
        static float GlowTerroristRed = 1.5f;
        static float GlowTerroristGreen = 0.0f;
        static float GlowTerroristBlue = 0.0f;
        static float GlowTerroristAlpha = 0.50f;

        static float GlowCounterTerroristRed = 0.5f;
        static float GlowCounterTerroristGreen = 0.5f;
        static float GlowCounterTerroristBlue = 2.0f;
        static float GlowCounterTerroristAlpha = 0.50f;

        public static void Main(string[] args)
        {
            Process process = Process.GetProcessesByName("csgo")[0];
            ProcessMemory processMem = ProcessMemory.ForProcess(process);

            if (StealthActive)
            {
                Stealth.Stealth.Hide("CSGH");
            }

            Client = processMem.GetModule("client_panorama.dll");
            int getMaxPlayers = 20;

            while (true)
            {
                glow_Pointer = Client.AtAddress(Client + dwGlowObjectManager);
                localPlayer = new Player(Client.AtAddress(Client + Offsets.signatures.dwLocalPlayer));

                int MyTeamId = localPlayer.TeamId;

                // Rotate through all players in game.
                for (int i = 0; i < getMaxPlayers; i++)
                {
                    Player currentPlayer = new Player(Client.AtAddress(Client + Offsets.signatures.dwEntityList + i * 0x10));

                    bool currentPlayerDormant = currentPlayer.IsDormant;

                    // 1 GoTV; 2 T; 3 CT
                    int EntityBaseTeamID = currentPlayer.TeamId;

                    if (currentPlayerDormant || EntityBaseTeamID == 0 || EntityBaseTeamID == MyTeamId)
                    {
                        continue;
                    }

                    if (GlowActive)
                    {
                        // Enable Glow
                        GlowESPCurrentPlayer(currentPlayer, EntityBaseTeamID);
                    }
                }
            }
        }


        static void GlowESPCurrentPlayer(Player currentPlayer, int EntityBaseTeamID)
        {
            int currentPlayerGlowIndex = currentPlayer.GlowIndex;

            switch (EntityBaseTeamID)	// 1 GoTV; 2 T; 3 CT
            {
                case 2:
                    {
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x4).Set(GlowTerroristRed);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x8).Set(GlowTerroristGreen);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0xC).Set(GlowTerroristBlue);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x10).Set(GlowTerroristAlpha);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x24).Set(true);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x25).Set(false);
                    }
                    break;
                case 3:
                    {
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x4).Set(GlowCounterTerroristRed);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x8).Set(GlowCounterTerroristGreen);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0xC).Set(GlowCounterTerroristBlue);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x10).Set(GlowCounterTerroristAlpha);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x24).Set(true);
                        glow_Pointer.AtOffset((currentPlayerGlowIndex * 0x38) + 0x25).Set(false);
                    }
                    break;
            }
        }
    }
}
