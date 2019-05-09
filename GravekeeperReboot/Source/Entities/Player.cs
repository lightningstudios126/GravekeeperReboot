using Components;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Entities {
    public class Player{
        public Entity PlayerEntity { get;  private set; }

        private Player(Entity playerEntity) {
			this.PlayerEntity = playerEntity;	
		}

        public static Player CreatePlayer (Scene scene) {
			Entity player;
            player = scene.createEntity("Player");
            player.setTag((int)Tags.Player);
			player.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.player)))
				.addComponent(new MoveComponent());

            return new Player(player);
        }
    }
}
