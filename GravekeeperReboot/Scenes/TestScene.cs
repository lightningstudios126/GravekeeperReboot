using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Systems;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		Player player;
		TiledMap testmap;

		public override void initialize() {
			base.initialize();
			this.addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(this));
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(MoveComponent))));

			player = Player.CreatePlayer(this);
			testmap = content.Load<TiledMap>(Content.Tilemaps.testmap);
		}

		public override void onStart() {
			base.onStart();

			Entity tileMapEntity = createEntity("tileMapEntity");
			TiledMapComponent comp = tileMapEntity.addComponent(new TiledMapComponent(testmap));

			//comp.tiledMap.getObjectGroup()

			tileMapEntity.position = new Vector2(0, 0);
			camera.position = comp.bounds.center;
			camera.zoom = 1f;
		}

		public override void update() {
			base.update();
		}

	}
}
