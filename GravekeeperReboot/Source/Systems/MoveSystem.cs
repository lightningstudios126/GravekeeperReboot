using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class MoveSystem : EntitySystem {
        public MoveSystem(Matcher matcher) : base(matcher) {}

        protected override void process(List<Entity> entities) {
            base.process(entities);
            foreach(Entity entity in entities) {
                MoveComponent component = entity.getComponent<MoveComponent>();
				if (entity.position != component.targetPosition) {
					entity.position = component.targetPosition;

					if (entity.getComponent<GrabComponent>() != null && entity.getComponent<GrabComponent>().isGrabbing) {
						GrabComponent grabComponent = entity.getComponent<GrabComponent>();
					
						Vector2 offset = entity.position - grabComponent.target.position;
						if (offset == Vector2.Zero) // Player overlaps with target when pushing forward
							offset = entity.getComponent<RotateComponent>().Direction;
						else
							Vector2.Normalize(offset);
					
						CommandSystem commandSystem = Core.scene.getEntityProcessor<CommandSystem>();
						Command command = new MoveCommand(grabComponent.target, offset * TiledMapConstants.TileSize) { playerInitiated = false };
						commandSystem.QueueCommand(command);
					}
				}
            }
        }
    }
}
