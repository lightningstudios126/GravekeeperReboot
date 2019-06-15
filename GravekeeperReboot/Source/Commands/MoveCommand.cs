using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : Command {
		private Entity entity;
		private Point initialPosition;
		private Point finalPosition;

		private TileComponent entityTile;

		public MoveCommand(Entity entity, Point offset) {
			if (!entity.HasComponent<TileComponent>())
				throw new System.ArgumentException("Target does not have a TileComponent attached!");

			this.entity = entity;
			entityTile = entity.getComponent<TileComponent>();

			initialPosition = entityTile.tilePosition;
			finalPosition = entityTile.tilePosition + offset;
		}

		public override void Execute() {
			entityTile.tilePosition = finalPosition;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entityTile.tilePosition = initialPosition;
			entity.position -= (finalPosition - initialPosition).ToVector2() * Tiled.TiledMapConstants.TILESIZE;
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
