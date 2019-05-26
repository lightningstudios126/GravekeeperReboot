using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class MoveSystem : EntitySystem {
		GameBoard gameBoard;

        public MoveSystem(Matcher matcher) : base(matcher) {}

        protected override void process(List<Entity> entities) {
            base.process(entities);
			if (gameBoard == null) gameBoard = scene.getSceneComponent<GameBoard>();

            foreach(Entity entity in entities) {
                TileComponent tileComponent = entity.getComponent<TileComponent>();
				Vector2 targetPosition = gameBoard.TileToWorldPosition(tileComponent.tilePosition);
				if (entity.position != targetPosition) {
					entity.position = targetPosition;

					GrabComponent grabComponent = entity.getComponent<GrabComponent>();
					if (grabComponent != null && grabComponent.isGrabbing && grabComponent.target != null)
						ManipulateEntity(entity);
				}
            }
        }

		/// <summary>
		/// Pushes / Pulls an entity with the player when grabbing.
		/// </summary>
		/// <param name="grabber">The entity that is grabbing</param>
		private void ManipulateEntity(Entity grabber) {
			TileComponent grabberTile = grabber.getComponent<TileComponent>();
			GrabComponent grabComponent = grabber.getComponent<GrabComponent>();
			Entity target = grabComponent.target;

			if (!target.getComponent<ControlComponent>().isPushable)
				return;

			TileComponent targetTile = grabComponent.target.getComponent<TileComponent>();

			Point offset = grabberTile.tilePosition - targetTile.tilePosition;
			// if the grabber is overlapping the target, it should be pushed
			if (offset == Point.Zero)
			offset = Utilities.Directions.DirectionPointOffset(grabber.getComponent<TileComponent>().tileDirection);
			else 
				offset = offset.Normalize();

			CommandSystem commandSystem = scene.getEntityProcessor<CommandSystem>();
			Command command = new MoveCommand(target, offset) { playerInitiated = false };
			commandSystem.QueueCommand(command);
		}
	}
}
