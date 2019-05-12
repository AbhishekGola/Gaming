#pragma once

#include "controls/Button.h"
#include "controls/Checkbox.h"
#include "controls/ComboBox.h"
#include "controls/SearchBox.h"
#include "controls/Slider.h"
#include "controls/Toggle.h"
#include "controls/KeyBind.h"
#include "controls/ColorPicker.h"

class Group
{
public:
	const char* Name;
	std::vector<IControl*> Controls;

	Group(const char* name)
	{
		Name = name;
	}

	Button* CreateButton(const char*, Event_t);
	Checkbox* CreateCheckbox(const char*, bool, bool*);
	Slider* CreateSlider(const char*, float, float, float*);
	Toggle* CreateToggle(const char*, bool, bool*);
	ComboBox* CreateComboBox(const char*, bool, int*, std::vector<std::string>);
	SearchBox* CreateSearchBox(const char*, std::vector<std::pair<int, std::string>>, int*);
	KeyBind* CreateKeyBind(const char*, int*);
	ColorPicker* CreateColorPicker(const char*, Color*);

	void Group::Update(POINT* location, POINT mouse, Menu*);
};