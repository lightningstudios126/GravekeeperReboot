using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace GravekeeperReboot.Source.Tiled {
	static class TiledMapRebuilder {
		private const string contentPath = @"..\..\..\..\Content\Tilemaps";

		/// <summary>
		/// Updates each tilemap's tileset with the external tileset in the content folder
		/// </summary>
		public static void Rebuild() {
			List<string> files = Directory.GetFiles(contentPath).ToList();

			XmlNode tilesetNode = GetTileset(files.Find(f => Path.GetExtension(f) == ".tsx"));
			if (tilesetNode != null)
				UpdateTilemaps(files, tilesetNode);
		}

		/// <summary>
		/// Retrieves the tileset XmlNode 
		/// </summary>
		/// <param name="filePath">The path of the tileset to extract</param>
		/// <returns>The XmlNode representing the Tileset</returns>
		private static XmlNode GetTileset(string filePath) {
			if (!File.Exists(filePath))
				return null;

			try {
				XmlDocument doc = new XmlDocument();
				doc.Load(filePath);
				XmlNode tileset = doc.DocumentElement;

				if (tileset != null) {
					XmlAttribute firstgidAttribute = doc.CreateAttribute("firstgid");
					firstgidAttribute.Value = "1";
					tileset.Attributes.Prepend(firstgidAttribute);

					tileset.Attributes.Remove(tileset.Attributes["tiledversion"]);
					tileset.Attributes.Remove(tileset.Attributes["version"]);

					return tileset;
				} else {
					Console.WriteLine($"Could not find a tileset in {filePath}");
				}
			}
			catch (XmlException e) {
				Console.WriteLine("XmlException occurred reading the tileset", e.Message);
			}
			return null;
		}

		/// <summary>
		/// Update every tilemap with the given tileset
		/// </summary>
		/// <param name="files">The list of files to update</param>
		/// <param name="tilesetNode">The new tileset</param>
		private static void UpdateTilemaps(List<string> files, XmlNode tilesetNode) {
			foreach (string filePath in files) {
				if (!File.Exists(filePath) || Path.GetExtension(filePath) != ".tmx")
					continue;

				try {
					XmlDocument doc = new XmlDocument();
					doc.Load(filePath);

					XmlNode root = doc.DocumentElement;
					if (root != null) {
						XmlNode embeddedTileset = root.SelectSingleNode("tileset");
						XmlNode newTileset = doc.ImportNode(tilesetNode, true);

						if (embeddedTileset.Equals(newTileset))
							continue;

						if (embeddedTileset != null)
							root.RemoveChild(embeddedTileset);
						root.PrependChild(newTileset);
					} else {
						Console.WriteLine($"Could not find a tilemap in {filePath}");
					}

					doc.Save(filePath);
				}
				catch (XmlException e) {
					Console.WriteLine("XmlException occurred reading the tilemap", e.Message);
				}
			}
		}
	}
}
