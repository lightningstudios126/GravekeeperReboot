using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	class Wall : Prefab {
		public override string Type => "Wall";

		protected override void Instantiate(TileEntity entity, Scene scene, Point position) {
			entity.name = Type;
			entity.setTag((int)Tags.Wall);

			entity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.wall)));

			entity.tilePosition = position;
			entity.movability = MovabilityFlags.None;
		}
	}
}
