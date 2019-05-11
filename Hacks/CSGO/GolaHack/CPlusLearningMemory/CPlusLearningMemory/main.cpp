#include <Windows.h>
#include <iostream> 
#include <future>
#include "stdafx.h"
#include "ProcMem.h"
#include ".\Offsets\Offsets.h"

#pragma region Settings
//Stealth
bool StealthActive = true;

//Glow
bool GlowActive = true;
bool GlowTeamCheck = true; 
float GlowTerroristRed = 1.5f;
float GlowTerroristGreen =0.f;
float GlowTerroristBlue = 0.f;
float GlowTerroristAlpha = 0.50f; 

float GlowCounterTerroristRed = 0.5f;
float GlowCounterTerroristGreen = 0.5f;
float GlowCounterTerroristBlue = 2.f;
float GlowCounterTerroristAlpha = 0.50f;

// Radar
bool RadarHackActive = false;

// Trigger bot
bool TriggerBotActive = true;

// Bunny Hop
bool BunnyHopActive = false;

// Bunny Hop
bool FlashHack = true;

#pragma endregion

ProcMem proM;
DWORD Client;
DWORD LocalPlayerAddress;
int MyTeamID;

int main(); 
void Hack();
void Stealth();

// CS GO hacks
void RadarEnableCurrentPlayer(DWORD currentPlayer);
void GlowEspCurrentPlayer(DWORD currentPlayer);
void GetCurrentPlayer(int index);
void GlowESPCurrentPlayer(DWORD currentPlayer);
void TriggerBot(int playerInCross);
void BunnyHop();
void AntiFlash();


int main()
{
	printf("Getting CSGO process id");
	proM.Process("csgo.exe");
	Sleep(200);
	printf("Getting client_panorama module.");
	Client = proM.Module("client_panorama.dll");
	if (StealthActive)
		Stealth(); 

	LocalPlayerAddress = proM.Read<DWORD>(Client + dwLocalPlayer);
	MyTeamID = proM.Read<int>(LocalPlayerAddress + m_iTeamNum);

	while (true)
	{
		Hack();
	}
}

void Stealth()
{
	HWND Stealth;
	AllocConsole();
	Stealth = FindWindowA("ConsoleWindowClass", NULL);
	ShowWindow(Stealth, 0);
}


void Hack()
{
	DWORD LocalPlayerAddress = proM.Read<DWORD>(Client + dwLocalPlayer);
	int MyTeamID = proM.Read<int>(LocalPlayerAddress + m_iTeamNum);

	int getMaxPlayers = 32;
	/*DWORD maxPlayerCountAdr=proM.Read<DWORD>(Client + dwClientState_MaxPlayer);
	getMaxPlayers = proM.Read<int>(maxPlayerCountAdr);*/

	// Rotate through all players in game.
	for (int i = 0; i < getMaxPlayers; i++)
	{
		DWORD currentPlayerbaseAddress= Client + dwEntityList + i * 0x10;
		DWORD currentPlayer = proM.Read<DWORD>(currentPlayerbaseAddress);
		bool currentPlayerDormant = proM.Read<bool>(currentPlayer + m_bDormant);
		DWORD EntityBaseTeamID = proM.Read<DWORD>(currentPlayer + m_iTeamNum);

		if (currentPlayerDormant == 1 || EntityBaseTeamID == 0)
		{
			continue;
		}

		if(GlowActive)
		{
			// Enable Glow
			GlowESPCurrentPlayer(currentPlayer);
		}

		if(RadarHackActive)
		{
			// Radar Enable
			RadarEnableCurrentPlayer(currentPlayer);
		}
	}

	//Trigger Bot
	//m_iCrosshairId
	int playerInCross = proM.Read<DWORD>(LocalPlayerAddress + m_iCrosshairId);

	// 1 GoTV; 2 T; 3 CT
	if(TriggerBotActive 
		&& playerInCross > 0 && playerInCross < 65)
	{
		// call TriggerBot(playerInCross, MyTeamID) asynchronously:
		TriggerBot(playerInCross);
		// TriggerBot(playerInCross, MyTeamID);
	}

	if(BunnyHopActive)
	{
		BunnyHop();
		// BunnyHop();
	}

	if(FlashHack)
	{
		AntiFlash();
	}
}

void TriggerBot(int playerInCross)
{
	DWORD AttackAddress = proM.Read<DWORD>(Client + dwForceAttack);
	DWORD playerinCrossAddress =Client + dwEntityList + (playerInCross-1) * 0x10;
	DWORD playerinCrossAd = proM.Read<DWORD>(playerinCrossAddress);
	int currentPlayerHealth = proM.Read<int>(playerinCrossAd + m_iHealth);
	DWORD EntityBaseTeamID = proM.Read<DWORD>(playerinCrossAd + m_iTeamNum);

	if(currentPlayerHealth > 0 		
		&& EntityBaseTeamID != MyTeamID // not in my team
		&& EntityBaseTeamID != 1) // not a Spectator
	{
		proM.Write<int>(Client + dwForceAttack,1);
		proM.Write<int>(Client + dwForceAttack,4);
	}
}

void GetCurrentPlayer(int index)
{
}

void GlowESPCurrentPlayer(DWORD currentPlayer)
{ 
	DWORD glow_Pointer = proM.Read<DWORD>(Client + dwGlowObjectManager);
	int currentPlayerGlowIndex = proM.Read<DWORD>(currentPlayer + m_iGlowIndex);
	DWORD EntityBaseTeamID = proM.Read<DWORD>(currentPlayer + m_iTeamNum);if(GlowActive)
	{
		switch (EntityBaseTeamID)	// 1 GoTV; 2 T; 3 CT
		{
		case 2:  
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x4)), GlowTerroristRed);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x8)), GlowTerroristGreen);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0xC)), GlowTerroristBlue);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x10)), GlowTerroristAlpha);
			proM.Write<BOOL>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x24)), true);
			proM.Write<BOOL>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x25)), false);
			break;
		case 3:
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x4)), GlowCounterTerroristRed);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x8)), GlowCounterTerroristGreen);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0xC)), GlowCounterTerroristBlue);
			proM.Write<float>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x10)), GlowCounterTerroristAlpha);
			proM.Write<BOOL>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x24)), true);
			proM.Write<BOOL>((glow_Pointer + ((currentPlayerGlowIndex * 0x38) + 0x25)), false);
			break;
		}
	}
}

void RadarEnableCurrentPlayer(DWORD currentPlayer)
{
	if(RadarHackActive)
	{
		bool isSpotted = proM.Read<bool>(currentPlayer + m_bSpotted);
		if(!isSpotted)
		{
			proM.Write<BOOL>((currentPlayer + m_bSpotted), true);
		}
	}
}

void BunnyHop()
{
	while(GetAsyncKeyState(32))
	{
		int jumpFlag =  proM.Read<DWORD>(LocalPlayerAddress + m_fFlags);

		if(jumpFlag == 257)
		{
			proM.Write<int>(Client + dwForceJump,5);	
			proM.Write<int>(Client + dwForceJump,4);	
		}
	}
}

void AntiFlash()
{
	int flashduration = proM.Read<int>(LocalPlayerAddress + m_flFlashDuration);
	if(flashduration >0)
	{
		proM.Write<int>((LocalPlayerAddress + m_flFlashDuration), 0);
	}
}