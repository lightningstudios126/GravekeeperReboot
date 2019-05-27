using GravekeeperReboot.Source;
using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Systems;
using GravekeeperReboot.Source.Tiled;
using Nez;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;

		public override void initialize() {
			base.initialize();
			TiledMapRebuilder.Rebuild();

			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(new ArrowKeyBinding())); 
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(TileComponent))));
			addEntityProcessor(new RotateSystem(new Matcher().all(typeof(TileComponent))));
			gameBoard = addSceneComponent(new GameBoard());
		}

		public override void onStart() {
			base.onStart();
			gameBoard.LoadLevel(1, 1);
			camera.setPosition(gameBoard.Center);
			camera.zoomIn(10);
		}

		public override void update() {
			base.update();
		}
	}
}
