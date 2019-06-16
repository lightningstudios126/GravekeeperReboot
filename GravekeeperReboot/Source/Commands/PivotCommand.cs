using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Commands {
	public class PivotCommand : Command {
		private TileEntity entity;
		private Point initialPosition;
		private Point finalPosition;

		public PivotCommand(TileEntity entity, Point pivot, TileDirection direction) {
			this.entity = entity;

			var pivotTile = pivot;

			initialPosition = entity.tilePosition;

			var pivotOffset = pivot - entity.tilePosition;
			var pivotDirection = Directions.OffsetDirection(entity.tilePosition - pivot).Add(direction).Offset();

			finalPosition = initialPosition + pivotOffset + pivotDirection;
		}

		public override void Execute() {
			entity.tilePosition = finalPosition;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entity.tilePosition = initialPosition;
			entity.position -= (finalPosition - initialPosition).ToVector2() * 16;
		}

		private void Animation(float progress) {
			GameBoard gameBoard = entity.scene.getSceneComponent<GameBoard>();
			var initial = Tiled.TiledMapConstants.TileToWorldPosition(initialPosition) + Tiled.TiledMapConstants.ENTITY_OFFSET;
			var final = Tiled.TiledMapConstants.TileToWorldPosition(finalPosition) + Tiled.TiledMapConstants.ENTITY_OFFSET;

			var offset = progress * (final - initial);

			entity.position = initial + offset;
		}
	}
}
