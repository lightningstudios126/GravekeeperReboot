using GravekeeperReboot.Source.Commands;
using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Source.Components {
	class PlayerMoveComponent : Component {
		GrabComponent playerGrab;
		TileComponent playerTile;
		GameBoard gameBoard;
		Systems.CommandSystem commandSystem;

		public override void onAddedToEntity() {
			base.onAddedToEntity();
			playerGrab = entity.getComponent<GrabComponent>();
			playerTile = entity.getComponent<TileComponent>();
			gameBoard = entity.scene.getSceneComponent<GameBoard>();
			commandSystem = entity.scene.getEntityProcessor<Systems.CommandSystem>();
		}

		public void OnPressUp() {
			Utilities.TileDirection direction = playerTile.tileDirection;
			if (!playerGrab.isGrabbing) {
				Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
				if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
					commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
					if (entityAhead != null) {
						commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
					}
				}
			} else if (gameBoard.CanPush(playerGrab.target.getComponent<TileComponent>(), direction)) {
				commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
				commandSystem.QueueCommand(new MoveCommand(playerGrab.target, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
			}
		}

		public void OnPressDown() {
			Utilities.TileDirection direction = Utilities.Directions.DirAdd(playerTile.tileDirection, Utilities.TileDirection.DOWN);
			Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
			if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
				commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
				if (playerGrab.isGrabbing) {
					commandSystem.QueueCommand(new MoveCommand(playerGrab.target, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
				}
				if (entityAhead != null) {
					commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
				}
			}
		}

		public void OnPressLeft() {
			commandSystem.QueueCommand(new RotateCommand(entity, Utilities.TileDirection.LEFT));
		}

		public void OnPressRight() {
			commandSystem.QueueCommand(new RotateCommand(entity, Utilities.TileDirection.RIGHT));
		}

		public void OnPressGrab() {
			if (playerGrab.isGrabbing) commandSystem.QueueCommand(new DropCommand(entity));
			else commandSystem.QueueCommand(new GrabCommand(entity));
		}
	}
}
