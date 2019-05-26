using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Components {
	public class TileComponent : Component {
		public Point tilePosition;
		public TileDirection tileDirection;

		private GameBoard gameBoard;

		public void Initialize(GameBoard board, Point tilePosition) {
			Initialize(board, tilePosition, TileDirection.RIGHT);
		}

		public void Initialize(GameBoard board, Point tilePosition, TileDirection direction) {
			this.gameBoard = board;
			this.tilePosition = tilePosition;

			entity.position = board.TileToWorldPosition(tilePosition);
			entity.rotation = Directions.DirectionDegrees(direction);
		}
	}
}
