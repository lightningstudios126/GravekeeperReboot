using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	class Soul : Prefab{
		public override string Type => "Soul";

		public override Entity Instantiate(Scene scene) {
			Entity soul;
			soul = scene.createEntity(Type);
			soul.setTag((int)Tags.Soul);

			soul.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.soul)))
				.addComponent(new MoveComponent())
				.addComponent(new BoxCollider(new Rectangle(0, 0, TiledMapConstants.TileSize, TiledMapConstants.TileSize)));

			return soul;
		}
	}
}
