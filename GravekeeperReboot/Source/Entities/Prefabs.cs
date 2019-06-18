using System.Collections.Generic;

namespace GravekeeperReboot.Source.Entities {
	static class Prefabs {
		public static readonly Prefab Player = new Player();
		public static readonly Prefab Soul = new Soul();
		public static readonly Prefab Block = new Block();
		public static readonly Prefab Wall = new Wall();
		// Gravestone
		public static readonly Prefab Thing = new MovingEntityThing();

		public static readonly Dictionary<string, Prefab> prefabs;

		static Prefabs() {
			prefabs = new Dictionary<string, Prefab> {
				{ Player.Type, Player },
				{ Soul.Type, Soul },
				{ Block.Type, Block },
				{ Wall.Type, Wall },
				{ Thing.Type, Thing }
			};
		}
	}
}
