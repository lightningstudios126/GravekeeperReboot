using System.Collections.Generic;

namespace GravekeeperReboot.Source.Entities {
	class Prefabs {
		public static readonly Prefab Player = new Player();
		public static readonly Prefab Soul = new Soul();
		// Soul
		// Block
		// Wall
		// Gravestone

		public static readonly Dictionary<string, Prefab> prefabs;

		static Prefabs() {
			prefabs = new Dictionary<string, Prefab> {
				{ Player.Type, Player },
				{ Soul.Type, Soul }
			};
		}
	}
}
