using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Components {
	class TileComponent : Component {
		public Point tilePosition;
		public TileDirection tileDirection;

		private GameBoard gameBoard;

		public void Initialize(GameBoard board, Point tilePosition) {
			Initialize(board, tilePosition, TileDirection.UP);
		}

		public void Initialize(GameBoard board, Point tilePosition, TileDirection direction) {
			this.gameBoard = board;
			this.tilePosition = tilePosition;

			entity.position = board.TileToWorldPosition(tilePosition);
			if (entity.HasComponent<MoveComponent>())
				entity.getComponent<MoveComponent>().position = tilePosition;

			entity.rotation = Directions.DirectionDegrees(direction);
			if(entity.HasComponent<RotateComponent>())
				entity.getComponent<RotateComponent>().direction = direction;
		}

		public override void onEntityTransformChanged(Transform.Component comp) {
			base.onEntityTransformChanged(comp);

			tilePosition = gameBoard.WorldToTilePosition(entity.position + new Vector2(0, 16));
			tileDirection = Directions.DegreesDirection(Mathf.rad2Deg * entity.rotation);
		}
	}
}
