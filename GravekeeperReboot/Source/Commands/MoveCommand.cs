using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : Command {
		private TileEntity entity;
		private Point initialPosition;
		private Point finalPosition;

		public MoveCommand(TileEntity entity, Point offset) {
			this.entity = entity;

			initialPosition = entity.tilePosition;
			finalPosition = entity.tilePosition + offset;
		}

		public override void Execute() {
			entity.tilePosition = finalPosition;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entity.tilePosition = initialPosition;
			entity.position -= (finalPosition - initialPosition).ToVector2() * Tiled.TiledMapConstants.TILE_SIZE;
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
