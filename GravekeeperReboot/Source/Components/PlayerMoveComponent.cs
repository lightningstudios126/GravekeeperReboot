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

		public event Action OnPlayerAction;

		public override void onAddedToEntity() {
			base.onAddedToEntity();
			playerGrab = entity.getComponent<GrabComponent>();
			playerTile = entity.getComponent<TileComponent>();
			gameBoard = entity.scene.getSceneComponent<GameBoard>();
			commandSystem = entity.scene.getEntityProcessor<Systems.CommandSystem>();
		}

		public void OnPressUp() {
			OnPlayerAction();
			Utilities.TileDirection direction = playerTile.tileDirection;
			if (!playerGrab.isGrabbing) {
				Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
				if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
					commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
					if (entityAhead != null) {
						commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)));
					}
				}
			} else if (gameBoard.CanPush(playerGrab.target.getComponent<TileComponent>(), direction)) {
				commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
				commandSystem.QueueCommand(new MoveCommand(playerGrab.target, Utilities.Directions.DirectionPointOffset(direction)));
			}
		}

		public void OnPressDown() {
			OnPlayerAction();
			Utilities.TileDirection direction = Utilities.Directions.DirAdd(playerTile.tileDirection, Utilities.TileDirection.DOWN);
			Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
			if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
				commandSystem.QueueCommand(new MoveCommand(entity, Utilities.Directions.DirectionPointOffset(direction)));
				if (playerGrab.isGrabbing) {
					commandSystem.QueueCommand(new MoveCommand(playerGrab.target, Utilities.Directions.DirectionPointOffset(direction)));
				}
				if (entityAhead != null) {
					commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)));
				}
			}
		}

		public void OnPressLeft() {
			OnPlayerAction();
			commandSystem.QueueCommand(new RotateCommand(entity, Utilities.TileDirection.LEFT));
		}

		public void OnPressRight() {
			OnPlayerAction();
			commandSystem.QueueCommand(new RotateCommand(entity, Utilities.TileDirection.RIGHT));
		}

		public void OnPressGrab() {
			OnPlayerAction();
			if (playerGrab.isGrabbing) commandSystem.QueueCommand(new DropCommand(entity));
			else commandSystem.QueueCommand(new GrabCommand(entity));
		}
	}
}
