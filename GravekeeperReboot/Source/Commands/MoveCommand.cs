using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Extensions;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Commands {
	public class MoveCommand : Command {
		private Entity entity;
		private Point offset;

		private TileComponent entityTile;

		public MoveCommand(Entity entity, Point offset) {
			if (!entity.HasComponent<TileComponent>())
				throw new System.ArgumentException("Target does not have a TileComponent attached!");

			this.entity = entity;
			this.offset = offset;
			entityTile = entity.getComponent<TileComponent>();
		}

		public override void Execute() {
			entityTile.tilePosition += offset;
			entity.addComponent<AnimationComponent>().animation = Animation;
		}

		public override void Undo() {
			entityTile.tilePosition -= offset;
			entity.position -= offset.ToVector2() * 16;
		}

		private void Animation(float progress) {
			GameBoard gameBoard = entity.scene.getSceneComponent<GameBoard>();
			entity.position = gameBoard.TileToWorldPosition(entityTile.tilePosition - offset) + progress * (gameBoard.TileToWorldPosition(offset) - new Vector2(8, 8));
		}
	}
}
