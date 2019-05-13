using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Extensions {
	static class PointExtensions {
		public static Point scl(this Point point, int scalar) {
			return new Point(point.X * scalar, point.Y * scalar);
		}
	}
}
