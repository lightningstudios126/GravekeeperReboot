using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Tiled;
using System.Collections.Generic;

namespace GravekeeperReboot.Source.Tiled {
	static class GlobalTileset {
		private static Texture2D tilesetAlias;

		private static Dictionary<int, string> types = new Dictionary<int, string>() {
			{0, TiledMapConstants.TYPE_EXIT },
			{1, TiledMapConstants.TYPE_GRAVESTONE_FULL },
			{2, TiledMapConstants.TYPE_GRAVESTONE_EMPTY },
			{3, TiledMapConstants.TYPE_GROUND },
			{4, TiledMapConstants.TYPE_GROUND },
			{5, TiledMapConstants.TYPE_GROUND },
			{6, TiledMapConstants.TYPE_GROUND },
			{7, TiledMapConstants.TYPE_BLOCK },
			{8, TiledMapConstants.TYPE_WALL },
			{9, TiledMapConstants.TYPE_PLAYER },
			{10, TiledMapConstants.TYPE_SOUL },
		};

		public static TiledTileset Tileset {
			get {
				TiledTileset tileset = new TiledTileset(tilesetAlias, 0);
				tileset.tiles.ForEach(t => t.properties[TiledMapConstants.PROPERTY_TYPE] = types[t.id]);
				System.Console.WriteLine(tileset.tiles.Count);
				return tileset;
			}
		}

		static GlobalTileset() {
			tilesetAlias = Core.scene.content.Load<Texture2D>(Content.Tilemaps.testsetatlas);
		}
	}
}
