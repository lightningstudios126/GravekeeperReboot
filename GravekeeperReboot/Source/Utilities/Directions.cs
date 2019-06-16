using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Utilities {
	public static class Directions {
		/// <summary>
		/// Returns a tile coordinate representing one tile in the specified direction
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static Point Offset(TileDirection d) {
			switch (d) {
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
		/// Converts an integer offset with a magnitude of 1 to a <see cref="TileDirection"/>.
		/// </summary>
		/// <param name="o">integer offset. Must have a magnitude of 1</param>
		/// <returns>Returns a <see cref="TileDirection"/> that corresponds to the offset</returns>
		public static TileDirection OffsetDirection(Point o) {
			if (o == new Point(1, 0)) return TileDirection.RIGHT;
			if (o == new Point(0, 1)) return TileDirection.DOWN;
			if (o == new Point(-1, 0)) return TileDirection.LEFT;
			if (o == new Point(0, -1)) return TileDirection.UP;
			else throw new System.ArgumentOutOfRangeException("offset must have a magnitude of 1");
		}

		public static int DirectionDegrees(TileDirection direction) {
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

		public static TileDirection DegreesDirection(float degrees) {
			return (TileDirection)(degrees % 360 / 90);
		}

		public static TileDirection DirAdd(TileDirection a, TileDirection b) {
			return (TileDirection)(((int)a + (int)b) % 4);
		}
	}
}
