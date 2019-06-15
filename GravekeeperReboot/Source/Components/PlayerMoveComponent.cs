using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Systems;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace GravekeeperReboot.Source.Components {
	class PlayerMoveComponent : Component {
		GrabComponent playerGrab;
		TileComponent playerTile;
		GameBoard gameBoard;
		CommandSystem commandSystem;

		public event Action OnPlayerAction;

		public override void onAddedToEntity() {
			base.onAddedToEntity();
			playerGrab = entity.getComponent<GrabComponent>();
			playerTile = entity.getComponent<TileComponent>();
			gameBoard = entity.scene.getSceneComponent<GameBoard>();
			commandSystem = entity.scene.getEntityProcessor<CommandSystem>();
		}

		public void OnPressUp() {
			OnPlayerAction();

			TileDirection direction = playerTile.tileDirection;
			if (!playerGrab.isGrabbing) {
				Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Directions.Offset(direction));

				if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
					MoveEntity(entity, Directions.Offset(direction));

					if (entityAhead != null)
						MoveEntity(entityAhead, Directions.Offset(direction));
				}
			} else if (gameBoard.CanPush(playerGrab.target.getComponent<TileComponent>(), direction)) {
				// Move the player as well as it's target
				MoveEntity(entity, Directions.Offset(direction));
				MoveEntity(playerGrab.target, Directions.Offset(direction));
			}
		}

		public void OnPressDown() {
			OnPlayerAction();

			TileDirection direction = Directions.DirAdd(playerTile.tileDirection, TileDirection.DOWN);
			Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Directions.Offset(direction));

			if (entityAhead == null || gameBoard.CanPush(entityAhead.getComponent<TileComponent>(), direction)) {
				MoveEntity(entity, Directions.Offset(direction));

				if (playerGrab.isGrabbing) MoveEntity(playerGrab.target, Directions.Offset(direction));
				if (entityAhead != null) MoveEntity(entityAhead, Directions.Offset(direction));
			}
		}

		public void OnPressLeft() {
			OnPlayerAction();

			var newDirection = Directions.DirAdd(playerTile.tileDirection, TileDirection.LEFT);

			if (!playerGrab.isGrabbing) {
				RotateEntity(entity, TileDirection.LEFT);
			} else if(gameBoard.CanRotate(playerGrab.target.getComponent<TileComponent>(), playerTile, TileDirection.LEFT)) {
				RotateEntity(entity, TileDirection.LEFT);
				PivotEntity(playerGrab.target, entity, TileDirection.LEFT);
			}
		}

		public void OnPressRight() {
			OnPlayerAction();

			var newDirection = Directions.DirAdd(playerTile.tileDirection, TileDirection.RIGHT);

			if (!playerGrab.isGrabbing) {
				RotateEntity(entity, TileDirection.RIGHT);
			} else if (gameBoard.CanRotate(playerGrab.target.getComponent<TileComponent>(), playerTile, TileDirection.RIGHT)) {
				RotateEntity(entity, TileDirection.RIGHT);
				PivotEntity(playerGrab.target, entity, TileDirection.RIGHT);
			}
		}

		public void OnPressGrab() {
			OnPlayerAction();
			if (playerGrab.isGrabbing) commandSystem.QueueCommand(new DropCommand(entity));
			else commandSystem.QueueCommand(new GrabCommand(entity));
		}

		private void MoveEntity(Entity entity, Point offset) {
			commandSystem.QueueCommand(new MoveCommand(entity, offset));
		}

		private void RotateEntity(Entity entity, TileDirection offset) {
			commandSystem.QueueCommand(new RotateCommand(entity, offset));
		}

		private void PivotEntity(Entity entity, Entity pivot, TileDirection offset) {
			commandSystem.QueueCommand(new PivotCommand(entity, pivot, offset));
		}
	}
}
