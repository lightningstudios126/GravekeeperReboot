using GravekeeperReboot.Source.Components;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class RotateSystem : EntitySystem {
		public RotateSystem(Matcher matcher) : base(matcher) { }

		protected override void process(List<Entity> entities) {
			base.process(entities);
			foreach (Entity entity in entities) {
				RotateComponent component = entity.getComponent<RotateComponent>();
				if (entity.rotationDegrees != Utilities.Direction.DirectionDegrees(component.direction)) {
					entity.setRotationDegrees(Utilities.Direction.DirectionDegrees(component.direction));
					
					//if (entity.getComponent<GrabComponent>() != null && entity.getComponent<GrabComponent>().isGrabbing) {
					//	GrabComponent grabComp = entity.getComponent<GrabComponent>();
					//
					//
					//	Vector2 offset = Vector2.Normalize(entity.position - grabComp.target.position);
					//	Vector2 finalOffset = offset * TiledMapConstants.TileSize + component.Direction * TiledMapConstants.TileSize * ;
					//
					//
					//	Console.WriteLine("RotationOffset: " + component.Direction);
					//
					//	CommandSystem commandSystem = scene.getEntityProcessor<CommandSystem>();
					//	commandSystem.QueueCommand(new MoveCommand(grabComp.target, finalOffset));
					//}
				}
			}
		}
	}
}
