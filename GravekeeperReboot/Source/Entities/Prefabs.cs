using System.Collections.Generic;

namespace GravekeeperReboot.Source.Entities {
	class Prefabs {
		public static readonly Prefab Player = new Player();
		// Soul
		// Block
		// Wall
		// Gravestone

		public static readonly Dictionary<string, Prefab> prefabs;

		static Prefabs() {
			prefabs = new Dictionary<string, Prefab> {
				{ Player.Type, Player }
			};
		}
	}
}
