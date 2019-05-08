using Components;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace Entities {
    public class Player{
        public Entity PlayerEntity { get;  private set; }

        private Player() {}

        public static Player CreatePlayer (Scene scene) {
            Player player = new Player();
            player.PlayerEntity = scene.createEntity("Player");
            player.PlayerEntity.setTag((int)Tags.Player);
            player.PlayerEntity.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.player)));
            player.PlayerEntity.addComponent(new MoveComponent());

            return player;
        }
    }
}
