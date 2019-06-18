using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	public sealed class Block : Prefab {
		public override string Type => Tiled.TiledMapConstants.TYPE_BLOCK;

		protected override void Instantiate(TileEntity entity, Scene scene, Point position) {
			entity.name = Type;
			entity.setTag((int)Tags.Block);

			entity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.block)));

			entity.tilePosition = position;
			entity.movability = MovabilityFlags.Grabbable | MovabilityFlags.Pushable;
		}
	}
}
