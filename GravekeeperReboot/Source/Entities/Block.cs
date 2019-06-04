using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	public class Block : Prefab {
		public override string Type => Tiled.TiledMapConstants.TYPE_BLOCK;

		public override Entity Instantiate(Scene scene, Vector2 position) {
			Entity block = scene.createEntity(Type, position);
			block.setTag((int)Tags.Block);

			block.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.movableWall)))
				.addComponent(new TileComponent())
				.addComponent(new ControlComponent(true, false));

			return block;
		}
	}
}
