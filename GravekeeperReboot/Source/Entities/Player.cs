﻿using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Source.Entities {
    public sealed class Player : Prefab {
		public override string Type => "Player";

		public override Entity Instantiate (Scene scene, Vector2 position) {
			Entity player;
            player = scene.createEntity(Type, position);
            player.setTag((int)Tags.Player);

			player.addComponent(new Sprite(scene.content.Load<Texture2D>(Content.Sprites.Tiles.player)))
				.addComponent(new MoveComponent())
				.addComponent(new RotateComponent())
				.addComponent(new BoxCollider(new Rectangle(0, 0, TiledMapConstants.TileSize, TiledMapConstants.TileSize)));

            return player;
        }
    }
}
