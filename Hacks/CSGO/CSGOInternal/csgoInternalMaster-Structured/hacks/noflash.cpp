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
*/

#include "noflash.h"

#include "..\GUI\menu.h"
#include "..\globals.h"
#include "..\settings.h"
#include "..\Valve\definitions.h"

void NoFlash::FrameStageNotify()
{
	if (G::CurStage != ClientFrameStage_t::FRAME_NET_UPDATE_POSTDATAUPDATE_START)
		return;

	if (!Config::Visuals::OverrideView::NoFlash)
		return;

	if (!G::LocalPlayer || *G::LocalPlayer->GetFlashMaxAlpha() < Config::Visuals::OverrideView::FlashMaxAlpha)
		return;

	*G::LocalPlayer->GetFlashMaxAlpha() = Config::Visuals::OverrideView::FlashMaxAlpha;
}