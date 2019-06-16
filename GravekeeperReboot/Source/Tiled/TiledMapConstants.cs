using Microsoft.Xna.Framework;

namespace GravekeeperReboot.Source.Tiled {
	static class TiledMapConstants {
		// floor tiles
		public const string LAYER_FLOOR = "Floor";
		// places where moving objects will spawn
		public const string LAYER_SPAWNS = "Entities";

		// the name of the custom property
		public const string PROPERTY_TYPE = "TileName";

		public const string TYPE_EXIT = "Exit";
		public const string TYPE_GRAVESTONE_FULL = "Full Gravestone";
		public const string TYPE_GRAVESTONE_EMPTY = "Empty Gravestone";
		public const string TYPE_GROUND = "Ground";
		public const string TYPE_WALL = "Wall";
		public const string TYPE_BLOCK = "Block";
		public const string TYPE_SOUL = "Soul";
		public const string TYPE_PLAYER = "Player";

		// Dimensions of every tile
		public const int TILE_SIZE = 16;
		public static readonly Vector2 ENTITY_OFFSET = new Vector2(TILE_SIZE / 2);

		public static Vector2 TileToWorldPosition(Point tilePos) {
			return tilePos.ToVector2() * TILE_SIZE;
		}

		public static Point WorldToTilePosition(Vector2 worldPos) {
			return (worldPos/TILE_SIZE).ToPoint();
		}
	}
}
