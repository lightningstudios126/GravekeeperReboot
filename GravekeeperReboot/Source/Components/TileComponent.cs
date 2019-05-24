using GravekeeperReboot.Source.Extensions;
using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class TileComponent : Component {
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
			entity.rotation = Directions.DirectionDegrees(direction);
		}

		public override void onEntityTransformChanged(Transform.Component comp) {
			base.onEntityTransformChanged(comp);

			tilePosition = gameBoard.WorldToTilePosition(entity.position + new Vector2(0, 16));
			tileDirection = Directions.DegreesDirection(Mathf.rad2Deg * entity.rotation);
		}
	}
}
