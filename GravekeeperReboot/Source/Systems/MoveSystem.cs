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
					//if (grabComponent != null && grabComponent.isGrabbing && grabComponent.target != null)
					//	ManipulateEntity(entity);
				}
            }
        }
	}
}
