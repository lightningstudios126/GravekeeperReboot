using GravekeeperReboot.Source.Utilities;
using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Extensions {
	public static class TileDirectionExtensions {
		/// <summary>
		/// Returns a tile coordinate representing one tile in the specified direction
		/// </summary>
		public static Point Offset(this TileDirection direction) {
			switch (direction) {
				case TileDirection.RIGHT:
				return new Point(1, 0);
				case TileDirection.UP:
				return new Point(0, -1);
				case TileDirection.LEFT:
				return new Point(-1, 0);
				case TileDirection.DOWN:
				return new Point(0, 1);
				default: return new Point(0, 0);
			}
		}

		/// <summary>
		/// Converts a <see cref="TileDirection" into degrees>
		/// </summary>
		/// <param name="direction">The direction to convert</param>
		/// <returns>An int representing rotation in degrees</returns>
		public static int DirectionDegrees(this TileDirection direction) {
			switch (direction) {
				case TileDirection.RIGHT:
				return 90;
				case TileDirection.UP:
				return 0;
				case TileDirection.LEFT:
				return -90;
				case TileDirection.DOWN:
				return 180;
				default: return 0;
			}
		}

		/// <summary>
		/// Adds a direction to this one
		/// </summary>
		/// <param name="b">The <see cref="TileDirection"> to add</param>
		/// <returns>The resultant <see cref="TileDirection"></returns>
		public static TileDirection Add(this TileDirection a, TileDirection b) {
			return a = (TileDirection)(((int)a + (int)b) % 4);
		}
	}
}
