using GravekeeperReboot.Source.Components;
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

					//if (entity.getComponent<GrabComponent>() != null && entity.getComponent<GrabComponent>().isGrabbing) {
					//	GrabComponent grabComponent = entity.getComponent<GrabComponent>();
					//
					//	Vector2 offset = Vector2.Normalize(entity.position - grabComponent.target.position);
					//
					//	if (offset == Vector2.Zero)
					//		offset = entity.getComponent<RotateComponent>().Direction;
					//
					//	CommandSystem commandSystem = Core.scene.getEntityProcessor<CommandSystem>();
					//	commandSystem.QueueCommand(new MoveCommand(grabComponent.target, offset * TiledMapConstants.TileSize));
					//}
				}
            }
        }
    }
}
