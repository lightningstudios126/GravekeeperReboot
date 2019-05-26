using Microsoft.Xna.Framework;
using Nez;

namespace GravekeeperReboot.Source.Extensions {
	static class PointExtensions {
		public static Point Scl(this Point point, int scalar) {
			return new Point(point.X * scalar, point.Y * scalar);
		}

		public static Point Normalize(this Point point) {
			return Vector2.Normalize(new Vector2(point.X, point.Y)).roundToPoint();
		}
	}
}
