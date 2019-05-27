using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace GravekeeperReboot.Source.Tiled {
	static class TiledMapRebuilder {
		public static void Rebuild() {
			Directory.SetCurrentDirectory(@"..\..\..\..\Content\Tilemaps");
			List<string> files = Directory.GetFiles(Directory.GetCurrentDirectory()).ToList();

			XmlNode tilesetNode = GetTileset(files.Find(f => Path.GetExtension(f) == ".tsx"));

			foreach (string filePath in files){
				if (Path.GetExtension(filePath) != ".tmx")
					continue;

				XmlDocument doc = new XmlDocument();
				doc.Load(filePath);

				XmlNode root = doc.DocumentElement;
				XmlNode embeddedTileset = root.SelectSingleNode("tileset");
				XmlNode newTileset = doc.ImportNode(tilesetNode, true);

				root.RemoveChild(embeddedTileset);
				root.PrependChild(newTileset);

				doc.Save(filePath);
			}
		}

		private static XmlNode GetTileset(string filePath) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filePath);
			XmlNode tileset = doc.DocumentElement;

			XmlAttribute firstgidAttribute = doc.CreateAttribute("firstgid");
			firstgidAttribute.Value = "1";
			tileset.Attributes.SetNamedItem(firstgidAttribute);

			tileset.Attributes.Remove(tileset.Attributes["tiledversion"]);
			tileset.Attributes.Remove(tileset.Attributes["version"]);

			return tileset;
		}
	}
}
