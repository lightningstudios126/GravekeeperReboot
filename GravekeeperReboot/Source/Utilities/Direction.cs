using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Source.Utilities {
	public class Direction {
		public enum Directions {
			RIGHT, UP, LEFT, DOWN
		}

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
