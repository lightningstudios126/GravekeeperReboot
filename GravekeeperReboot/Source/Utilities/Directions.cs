using Microsoft.Xna.Framework;
using System;

namespace GravekeeperReboot.Source.Utilities {
	public class Directions {
		/// <summary>
		/// Returns a tile coordinate representing one tile in the specified direction
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static Point DirectionPointOffset(TileDirection d) {
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
