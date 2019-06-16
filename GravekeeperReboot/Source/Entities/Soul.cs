using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	public sealed class Soul : Prefab{
		public override string Type => Tiled.TiledMapConstants.TYPE_SOUL;

		protected override void Instantiate(TileEntity entity, Scene scene, Point position) {
			entity.name = Type;
			entity.setTag((int)Tags.Soul);

			entity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.soul)));

			entity.tilePosition = position;
			entity.movability = TileEntity.MovabilityFlags.Grabbable | TileEntity.MovabilityFlags.Pushable | TileEntity.MovabilityFlags.Pivotable;
		}
	}
}
