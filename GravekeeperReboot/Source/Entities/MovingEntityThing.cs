using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
	class MovingEntityThing : Prefab {
		public override string Type => "MovingEntityThing";

		protected override void Instantiate(TileEntity entity, Scene scene, Point position) {
			entity.name = Type;
			entity.setTag((int) Tags.Soul);

			entity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.soul)) { color = Color.Red });

			entity.tilePosition = position;
			entity.movability = MovabilityFlags.Grabbable | MovabilityFlags.Pushable | MovabilityFlags.Pivotable;
		}
	}
}
