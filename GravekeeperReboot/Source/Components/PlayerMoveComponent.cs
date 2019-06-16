using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Systems;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;
using System;

namespace GravekeeperReboot.Source.Components {
	class PlayerMoveComponent : Component {
		new TileEntity entity;

		GrabComponent playerGrab;
		GameBoard gameBoard;
		CommandSystem commandSystem;

		public event Action OnPlayerAction;

		public override void initialize() {
			base.initialize();
			this.entity = base.entity as TileEntity;
		}

		public override void onAddedToEntity() {
			base.onAddedToEntity();
			playerGrab = entity.getComponent<GrabComponent>();
			gameBoard = entity.scene.getSceneComponent<GameBoard>();
			commandSystem = entity.scene.getEntityProcessor<CommandSystem>();
		}

		public void OnPressUp() {
			OnPlayerAction();

			TileDirection direction = entity.tileDirection;
			if (!playerGrab.isGrabbing) {
				TileEntity entityAhead = gameBoard.FindAtLocation(entity.tilePosition + Directions.Offset(direction));

				if (entityAhead == null || entityAhead.CanPush(direction)) {
					MoveEntity(entity, Directions.Offset(direction));

					if (entityAhead != null)
						MoveEntity(entityAhead, Directions.Offset(direction));
				}
			} else {
				TileEntity entityAhead = gameBoard.FindAtLocation(playerGrab.target.tilePosition + Directions.Offset(direction));
				if (entityAhead == null || entityAhead.CanPush(direction)) {
					// Move the player as well as it's target
					MoveEntity(entity, Directions.Offset(direction));
					MoveEntity(playerGrab.target, Directions.Offset(direction));
					if (entityAhead != null) MoveEntity(entityAhead, Directions.Offset(direction));
				}
			}
		}

		public void OnPressDown() {
			OnPlayerAction();

			TileDirection direction = Directions.DirAdd(entity.tileDirection, TileDirection.DOWN);
			TileEntity entityAhead = gameBoard.FindAtLocation(entity.tilePosition + Directions.Offset(direction));

			if (entityAhead == null || entityAhead.CanPush(direction)) {
				MoveEntity(entity, Directions.Offset(direction));

				if (playerGrab.isGrabbing) MoveEntity(playerGrab.target, Directions.Offset(direction));
				if (entityAhead != null) MoveEntity(entityAhead, Directions.Offset(direction));
			}
		}

		public void OnPressLeft() {
			OnPlayerAction();

			var newDirection = Directions.DirAdd(entity.tileDirection, TileDirection.LEFT);

			if (!playerGrab.isGrabbing) {
				RotateEntity(entity, TileDirection.LEFT);
			} else if (playerGrab.target.CanPivot(entity.tilePosition, TileDirection.LEFT)) {
				RotateEntity(entity, TileDirection.LEFT);
				PivotEntity(playerGrab.target, entity.tilePosition, TileDirection.LEFT);
			}
		}

		public void OnPressRight() {
			OnPlayerAction();

			var newDirection = Directions.DirAdd(entity.tileDirection, TileDirection.RIGHT);

			if (!playerGrab.isGrabbing) {
				RotateEntity(entity, TileDirection.RIGHT);
			} else if (playerGrab.target.CanPivot(entity.tilePosition, TileDirection.RIGHT)) {
				RotateEntity(entity, TileDirection.RIGHT);
				PivotEntity(playerGrab.target, entity.tilePosition, TileDirection.RIGHT);
			}
		}

		public void OnPressGrab() {
			OnPlayerAction();
			if (playerGrab.isGrabbing) commandSystem.QueueCommand(new DropCommand(entity));
			else commandSystem.QueueCommand(new GrabCommand(entity));
		}

		private void MoveEntity(TileEntity entity, Point offset) {
			commandSystem.QueueCommand(new MoveCommand(entity, offset));
		}

		private void RotateEntity(TileEntity entity, TileDirection offset) {
			commandSystem.QueueCommand(new RotateCommand(entity, offset));
		}

		private void PivotEntity(TileEntity entity, Point pivot, TileDirection offset) {
			commandSystem.QueueCommand(new PivotCommand(entity, pivot, offset));
		}
	}
}
