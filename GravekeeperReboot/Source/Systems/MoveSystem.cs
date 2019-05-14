using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;
using System;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Systems {
	class MoveSystem : EntitySystem {
		GameBoard gameBoard;

        public MoveSystem(Matcher matcher) : base(matcher) {}

        protected override void process(List<Entity> entities) {
            base.process(entities);
			if (gameBoard == null) gameBoard = scene.getSceneComponent<GameBoard>();

            foreach(Entity entity in entities) {
                MoveComponent moveComponent = entity.getComponent<MoveComponent>();
				if (entity.position != gameBoard.TileToWorldPosition(moveComponent.position)) {
					//entity.position = component.targetPosition;
					entity.position = gameBoard.TileToWorldPosition(moveComponent.position);

					if (entity.HasComponent<GrabComponent>() && entity.getComponent<GrabComponent>().isGrabbing) {
						GrabComponent grabComponent = entity.getComponent<GrabComponent>();
						MoveComponent grabbedMoveComponent = grabComponent.target.getComponent<MoveComponent>();
					
						Point offset = moveComponent.position - grabbedMoveComponent.position;
						if (offset == Point.Zero) // Player overlaps with target when pushing forward
							offset = Utilities.Direction.DirectionPointOffset(entity.getComponent<RotateComponent>().direction);
						else
							offset = offset.Normalize();
						
						CommandSystem commandSystem = Core.scene.getEntityProcessor<CommandSystem>();
						Command command = new MoveCommand(grabComponent.target, offset) { playerInitiated = false };
						commandSystem.QueueCommand(command);
					}
				}
            }
        }
    }
}
