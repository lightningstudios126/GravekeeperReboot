using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class RotateSystem : EntitySystem {
		public RotateSystem(Matcher matcher) : base(matcher) { }

		protected override void process(List<Entity> entities) {
			base.process(entities);
			foreach (Entity entity in entities) {
				TileComponent entityTile = entity.getComponent<TileComponent>();
				int targetRotation = Directions.DirectionDegrees(entityTile.tileDirection);

				if (entity.rotationDegrees != targetRotation) {
					entity.setRotationDegrees(targetRotation);

					GrabComponent grabComponent = entity.getComponent<GrabComponent>();
					if (grabComponent != null && grabComponent.isGrabbing && grabComponent.target != null)
						ManipulateEntity(entity);
				}
			}
		}

		/// <summary>
		/// Moves an entity along with the player when grabbing
		/// </summary>
		private void ManipulateEntity(Entity entity) {
			TileComponent entityTileComponent = entity.getComponent<TileComponent>();
			GrabComponent grabComponent = entity.getComponent<GrabComponent>();
			Entity target = grabComponent.target;
			
			if (!target.getComponent<ControlComponent>().isPivotable)
				return;

			// Moves target to grabber's position
			Point offset = Vector2.Normalize(entity.position - target.position).roundToPoint();
			// Pushes target out in the direction grabber is facing
			Point playerDirection = Directions.DirectionPointOffset(entityTileComponent.tileDirection);

			CommandSystem commandSystem = scene.getEntityProcessor<CommandSystem>();
			commandSystem.QueueCommand(
				new MoveCommand(grabComponent.target, offset) { playerInitiated = false },
				new MoveCommand(grabComponent.target, playerDirection) { playerInitiated = false }
			);
		}
	}
}
