using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class RotateSystem : EntitySystem {
		public RotateSystem(Matcher matcher) : base(matcher) { }

		protected override void process(List<Entity> entities) {
			base.process(entities);
			foreach (Entity entity in entities) {
				RotateComponent rotateComponent = entity.getComponent<RotateComponent>();
				if (entity.rotationDegrees != Utilities.Direction.DirectionDegrees(rotateComponent.direction)) {
					entity.setRotationDegrees(Utilities.Direction.DirectionDegrees(rotateComponent.direction));
					
					if (entity.getComponent<GrabComponent>() != null && entity.getComponent<GrabComponent>().isGrabbing) {
						GrabComponent grabComponent = entity.getComponent<GrabComponent>();
					
						// Moves target to grabber's position
						Point offset = Vector2.Normalize(entity.position - grabComponent.target.position).roundToPoint();
						// Pushes target out in the direction grabber is facing
						Point playerDirection = Utilities.Direction.DirectionPointOffset(rotateComponent.direction);

						CommandSystem commandSystem = scene.getEntityProcessor<CommandSystem>();
						commandSystem.QueueCommand(
							new MoveCommand(grabComponent.target, offset) { playerInitiated = false },
							new MoveCommand(grabComponent.target, playerDirection) { playerInitiated = false }
						);
					}
				}
			}
		}
	}
}
