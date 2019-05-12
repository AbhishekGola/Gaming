#pragma once

// credit namazso @ unknowncheats.me

#include "..\Interfaces.h"

namespace Stickers
{
	enum class EStickerAttributeType
	{
		Index,
		Wear,
		Scale,
		Rotation
	};

	void ApplyStickerHooks(C_BaseAttributableItem* item);
}
