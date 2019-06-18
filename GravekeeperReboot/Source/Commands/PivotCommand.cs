using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Tiled;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class PivotCommand : Command {
		private TileEntity entity;
		private Point initialPosition;
		private Point finalPosition;
		private Point pivot;
		private TileDirection turnDirection;

		public PivotCommand(TileEntity entity, Point pivot, TileDirection direction) {
			this.entity = entity;

			this.pivot = pivot;
			this.turnDirection = direction;
			initialPosition = entity.tilePosition;

			var pivotOffset = pivot - entity.tilePosition;
			var pivotDirection = Directions.OffsetDirection(entity.tilePosition - pivot).Add(direction).Offset();

			finalPosition = initialPosition + pivotOffset + pivotDirection;
		}

		public override void Execute() {
			entity.tilePosition = finalPosition;
			var animComponent = entity.addComponent<AnimationComponent>();
			animComponent.animation = Animation;
			animComponent.animationFinish = () => { };
		}

		public override void Undo() {
			entity.tilePosition = initialPosition;
			entity.position -= (finalPosition - initialPosition).ToVector2() * 16;
		}

		private void Animation(float progress) {
			GameBoard gameBoard = entity.scene.getSceneComponent<GameBoard>();
			var initial = TiledMapConstants.TileToWorldPosition(initialPosition) + TiledMapConstants.ENTITY_OFFSET;
			var center = TiledMapConstants.TileToWorldPosition(pivot) + TiledMapConstants.ENTITY_OFFSET;
			var a = progress * 90 * (turnDirection == TileDirection.LEFT ? -1 : 1) * Mathf.deg2Rad;

			var rotatedX = Mathf.cos(a) * (initial.X - center.X) - Mathf.sin(a) * (initial.Y - center.Y) + center.X;
			var rotatedY = Mathf.sin(a) * (initial.X - center.X) + Mathf.cos(a) * (initial.Y - center.Y) + center.Y;

			entity.position = new Vector2(rotatedX, rotatedY);
		}
	}
}
