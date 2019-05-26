namespace GravekeeperReboot.Source.Tiled {
	static class TiledMapConstants {
		// floor tiles
		public const string LAYER_FLOOR = "Floor";
		// places where moving objects will spawn
		public const string LAYER_SPAWNS = "Entities";

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
		public const int TILESIZE = 16;
	}
}
