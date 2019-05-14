using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Utilities {
	public class Direction {
		public enum Directions {
			RIGHT, UP, LEFT, DOWN
		}

		/// <summary>
		/// Returns a tile coordinate representing one tile in the specified direction
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static Point DirectionPointOffset(Directions d) {
			switch (d) {
				case Directions.RIGHT:
					return new Point(1, 0);
				case Directions.UP:
					return new Point(0, -1);
				case Directions.LEFT:
					return new Point(-1, 0);
				case Directions.DOWN:
					return new Point(0, 1);
				default: return new Point(0, 0);
			}
		}

		public static int DirectionDegrees(Directions d) {
			switch (d) {
				case Directions.RIGHT:
					return 90;
				case Directions.UP:
					return 0;
				case Directions.LEFT:
					return -90;
				case Directions.DOWN:
					return 180;
				default: return 0;
			}
		}
		 
		public static Directions DirAdd(Directions a, Directions b) {
			return (Directions)(((int)a + (int)b+3) % 4);
		}
	}

}
