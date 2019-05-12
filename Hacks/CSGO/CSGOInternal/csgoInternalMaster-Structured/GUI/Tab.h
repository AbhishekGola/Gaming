#pragma once

#include "Group.h"

#include "controls\Button.h"
#include "controls\Checkbox.h"
#include "controls\ComboBox.h"
#include "controls\SearchBox.h"
#include "controls\Slider.h"
#include "controls\Toggle.h"
#include "controls\KeyBind.h"

class Tab
{

public:
	Tab(const char* name) { Name = name; }
	//virtual ~Tab() {}

	Menu* Parent;
	
	const char* Name;

	std::vector<Group*> Groups;
	Group* CreateGroup(const char*);

	virtual void CustomRender() { }
	void Update(POINT* location, POINT mouse);
};

class ColorsTab : public Tab
{
public:
	ColorsTab(const char* name) : Tab(name) {}

	void CustomRender();
};

class SkinsTab : public Tab
{
public:
	SkinsTab(const char* name) : Tab(name) {}

	void CustomRender();
};