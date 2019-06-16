using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
    public sealed class Player : Prefab {
		public override string Type => Tiled.TiledMapConstants.TYPE_PLAYER;

		protected override void Instantiate (TileEntity entity, Scene scene, Point position) {
			entity.name = Type;
            entity.setTag((int)Tags.Player);

			entity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.player)))
				.addComponent(new GrabComponent() { isGrabbing = false })
				.addComponent(new PlayerMoveComponent());

			entity.tilePosition = position;
			entity.movability = 0;
        }
    }
}
