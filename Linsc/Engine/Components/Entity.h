#pragma once

#include "ComponentsCommon.h"

namespace linsc::game_entity
{
	struct entity_info
	{
		
	};

	entity_id create_game_entity(const entity_info& info);
	void remove_game_entity(entity_id id);
	bool is_alive(entity_id id);
}

