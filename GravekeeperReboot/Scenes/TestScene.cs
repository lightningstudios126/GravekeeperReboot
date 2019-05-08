using Components;
using Entities;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;
using Systems;

namespace Scenes {
	class TestScene : Scene {
		Player player;
		TiledMap testmap;

		public override void initialize() {
			base.initialize();
			this.addRenderer(new DefaultRenderer());
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(MoveComponent))));

			player = Player.CreatePlayer(this);
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
