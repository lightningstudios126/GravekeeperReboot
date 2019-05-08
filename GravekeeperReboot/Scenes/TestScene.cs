﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {

		TiledMap testmap;

		public override void initialize() {
			base.initialize();

			this.addRenderer(new DefaultRenderer());

			testmap = content.Load<TiledMap>(Content.Tilemaps.testmap);
		}

		public override void onStart() {
			base.onStart();

			Entity tileMapEntity = createEntity("tileMapEntity");
			TiledMapComponent comp = tileMapEntity.addComponent(new TiledMapComponent(testmap));
			tileMapEntity.position = new Vector2(100, 100);


		}

		public override void update() {
			base.update();
		}

	}
}
