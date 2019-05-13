using GravekeeperReboot.Source;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Entities;
using GravekeeperReboot.Source.Systems;
using Microsoft.Xna.Framework;
using Nez;
using Nez.Tiled;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;

		public override void initialize() {
			base.initialize();
			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(this));
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(MoveComponent))));

			Entity tileMapEntity = createEntity("tileMapEntity");
			gameBoard = addSceneComponent(new GameBoard(tileMapEntity));
		}

		public override void onStart() {
			base.onStart();
			getSceneComponent<GameBoard>().LoadLevel(Content.Tilemaps.testmap1);
		}

		public override void update() {
			base.update();
		}
	}
}
