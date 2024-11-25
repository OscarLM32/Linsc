#pragma once
#include "ComponentsCommon.h"

namespace linsc::trasnform
{
	DEFINE_TYPED_ID(transform_id);

	struct init_info
	{
		f32 position[3]{};
		f32 rotation[4]{};
		f32 scale[3]{1, 1, 1} ;
	};

	transform_id create_transform(const init_info& info, game_entity::entity_id entity);
	void remove_transform(transform_id id);
}