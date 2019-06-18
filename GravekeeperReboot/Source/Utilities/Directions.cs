using System;
using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Utilities {
	public static class Directions {
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

		/// <summary>
		/// Turns rotation into a <see cref="TileDirection">
		/// </summary>
		/// <param name="degrees">The rotation in degrees</param>
		/// <returns>A <see cref="TileDirection"> representing the rotation (snapped to 90 degrees) </returns>
		public static TileDirection DegreesDirection(float degrees) {
			return (TileDirection)(degrees % 360 / 90);
		}
	}
}
