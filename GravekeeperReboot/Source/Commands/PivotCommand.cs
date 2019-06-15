using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class PivotCommand : Command {
		private Entity entity;
		private Point initialPosition;
		private Point finalPosition;

		private TileComponent entityTile;

		public PivotCommand(Entity entity, Entity pivot, TileDirection offset) {
			if (!entity.HasComponent<TileComponent>())
				throw new System.ArgumentException("Target does not have a TileComponent attached!");

			this.entity = entity;
			entityTile = entity.getComponent<TileComponent>();

			var pivotTile = pivot.getComponent<TileComponent>();

			initialPosition = entityTile.tilePosition;

			var pivotOffset = pivotTile.tilePosition - entityTile.tilePosition;
			var pivotDirection = Directions.Offset(Directions.DirAdd(pivotTile.tileDirection, offset));

			finalPosition = initialPosition + pivotOffset + pivotDirection;
		}

		public override void Execute() {
			entityTile.tilePosition = finalPosition;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entityTile.tilePosition = initialPosition;
			entity.position -= (finalPosition - initialPosition).ToVector2() * 16;
		}

		private void Animation(float progress) {
			GameBoard gameBoard = entity.scene.getSceneComponent<GameBoard>();
			var initial = gameBoard.TileToWorldPosition(initialPosition);
			var final = gameBoard.TileToWorldPosition(finalPosition);

			var offset = progress * (final - initial);

			entity.position = initial + offset;
		}
	}
}
