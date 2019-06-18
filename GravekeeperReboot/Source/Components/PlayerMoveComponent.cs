using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
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
			gameBoard = entity.gameBoard;
			commandSystem = entity.scene.getEntityProcessor<CommandSystem>();
		}

		public void OnPressUp() {
			OnPlayerAction();

			TileDirection direction = entity.tileDirection;
			if (!gameBoard.GroundAtLocation(entity.tilePosition + direction.Offset())) return;

			if (!playerGrab.isGrabbing) {
				TileEntity entityAhead = gameBoard.FindAtLocation(entity.tilePosition + direction.Offset());

				if (entityAhead == null || entityAhead.CanPush(direction)) {
					MoveEntity(entity, direction.Offset());

					if (entityAhead != null)
						MoveEntity(entityAhead, direction.Offset());
				}
			} else {
				TileEntity entityAhead = gameBoard.FindAtLocation(playerGrab.target.tilePosition + direction.Offset());
				if (entityAhead == null || entityAhead.CanPush(direction)) {
					MoveEntity(entity, direction.Offset());
					MoveEntity(playerGrab.target, direction.Offset());
					if (entityAhead != null) MoveEntity(entityAhead, direction.Offset());
				}
			}
		}

		public void OnPressDown() {
			OnPlayerAction();

			TileDirection direction = entity.tileDirection.Add(TileDirection.DOWN);
			if (!gameBoard.GroundAtLocation(entity.tilePosition + direction.Offset())) return;

			TileEntity entityAhead = gameBoard.FindAtLocation(entity.tilePosition + direction.Offset());
			if (entityAhead == null || entityAhead.CanPush(direction)) {
				MoveEntity(entity, direction.Offset());

				if (playerGrab.isGrabbing) MoveEntity(playerGrab.target, direction.Offset());
				if (entityAhead != null) MoveEntity(entityAhead, direction.Offset());
			}
		}

		public void OnPressLeft() {
			OnPlayerAction();
			RotatePlayer(TileDirection.LEFT);
		}

		public void OnPressRight() {
			OnPlayerAction();
			RotatePlayer(TileDirection.RIGHT);
		}

		public void OnPressGrab() {
			OnPlayerAction();
			if (playerGrab.isGrabbing) commandSystem.QueueCommand(new DropCommand(entity));
			else commandSystem.QueueCommand(new GrabCommand(entity));
		}

		private void RotatePlayer(TileDirection direction) {
			if (direction != TileDirection.LEFT && direction != TileDirection.RIGHT)
				throw new ArgumentOutOfRangeException("Direction is not left or right");

			// Rotate the direction left or right
			var newDirection = entity.tileDirection.Add(direction);

			if (!playerGrab.isGrabbing) {
				// Rotate just the player
				RotateEntity(entity, direction);
			} else if (playerGrab.target.CanPivot(entity.tilePosition, direction)) {
				// Rotate the player and pivot the entity it's grabbing
				RotateEntity(entity, direction);
				PivotEntity(playerGrab.target, entity.tilePosition, direction);
			}
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
