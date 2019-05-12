/*
      ___           ___           ___           ___     
     /\  \         |\__\         /\  \         /\__\    
    /::\  \        |:|  |       /::\  \       /::|  |   
   /:/\:\  \       |:|  |      /:/\:\  \     /:|:|  |   
  /:/  \:\  \      |:|__|__   /::\~\:\  \   /:/|:|  |__ 
 /:/__/ \:\__\     /::::\__\ /:/\:\ \:\__\ /:/ |:| /\__\
 \:\  \  \/__/    /:/~~/~~   \/__\:\/:/  / \/__|:|/:/  /
  \:\  \         /:/  /           \::/  /      |:/:/  / 
   \:\  \        \/__/            /:/  /       |::/  /  
    \:\__\                       /:/  /        /:/  /   
     \/__/                       \/__/         \/__/    

revolt (4/2017)
credit: Synraw @ unknowncheats.me
*/

#pragma once

#ifndef INTERFACES_H
#define INTERFACES_H

#include "valve\IBaseClientDLL.h"
#include "valve\IEngineClient.h"
#include "valve\IVPanel.h"
#include "valve\IClientEntityList.h"
#include "valve\ISurface.h"
#include "valve\IVDebugOverlay.h"
#include "valve\CGlobalVars.h"
#include "valve\IPrediction.h"
#include "valve\IMaterialSystem.h"
#include "valve\CViewRender.h"
#include "valve\IVModelRender.h"
#include "valve\IVModelInfo.h"
#include "valve\IEngineTrace.h"
#include "valve\IPhysicsSurfaceProps.h"
#include "valve\ICvar.h"
#include "valve\IGameMovement.h"
#include "valve\IGameEvent.h"
#include "valve\IEngineSound.h"
#include "valve\CCSGameRules.h"
#include "valve\CEffects.h"
#include "valve\ILocalize.h"
#include "valve\CBaseClientState.h"
#include "valve\IInputSystem.h"
#include "valve\CPlayerResource.h"
#include "valve\CCSWeaponSystem.h"
#include "valve\CGlowObjectManager.h"
#include "valve\CInput.h"
#include "valve\CGameUI.h"

namespace I
{
	void Initialize();

	extern IBaseClientDLL* Client;
	extern IEngineClient* Engine;
	extern IVPanel* Panels;
	extern IClientEntityList* EntList;
	extern ISurface* Surface;
	extern IVDebugOverlay* DebugOverlay;
	extern DWORD *ClientMode;
	extern CGlobalVars *globalVars;
	extern IPrediction *Prediction;
	extern IMaterialSystem* MaterialSystem;
	extern CViewRender* RenderView;
	extern IVModelRender* ModelRender;
	extern IVModelInfo* ModelInfo;
	extern IEngineTrace* Trace;
	extern IPhysicsSurfaceProps* PhysProps;
	extern ICvar* CVar;
	extern IGameMovement *GameMovement;
	extern IGameEventManager2 *GameEventManager;
	extern IEngineSound *EngineSound;
	extern IMoveHelper* MoveHelper;
	extern C_CSGameRules* csGameRules;
	extern CEffects* Effects;
	extern ILocalize* Localize;
	extern CBaseClientState* ClientState;
	extern IInputSystem* InputSystem;
	extern C_CSPlayerResource** PlayerResourcePtr;
	extern IWeaponSystem* WeaponSystem;
	extern CGlowObjectManager* GlowManager;
	extern CInput* Input;
	extern CGameUI* GameUI;

	class InterfacesHead;
	class InterfaceNode;

	class InterfacesHead
	{
	public:
		InterfaceNode* HeadNode; //0x0000 
	};//Size=0x0040

	typedef int(*oCapture)(void);

	class InterfaceNode
	{
	public:
		oCapture fncGet; //0x0000 
		char* pName; //0x0004 
		InterfaceNode* pNext; //0x0008 
	};//Size=0x001C

	class InterfaceManager
	{
	public:
		InterfaceManager(std::string strModule) : ModuleBase(0), pIntHead(0) { Setup(strModule); }
		InterfaceManager() : ModuleBase(0), pIntHead(0) {}

		void Setup(std::string strModule);
		template <class T>
		T* GetInterface(std::string strName);
		void DumpAllInterfaces();

	private:
		std::string strModuleName;
		HMODULE ModuleBase;
		InterfacesHead* pIntHead;
	};
};

#endif