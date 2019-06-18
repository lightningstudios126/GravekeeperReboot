using System.Collections.Generic;

namespace GravekeeperReboot.Source.Entities {
	static class Prefabs {
		public static readonly Prefab Player = new Player();
		public static readonly Prefab Soul = new Soul();
		public static readonly Prefab Block = new Block();
		// Wall
		// Gravestone

		public static readonly Dictionary<string, Prefab> prefabs;

		static Prefabs() {
			prefabs = new Dictionary<string, Prefab> {
				{ Player.Type, Player },
				{ Soul.Type, Soul },
				{ Block.Type, Block }
			};
		}
	}
}
