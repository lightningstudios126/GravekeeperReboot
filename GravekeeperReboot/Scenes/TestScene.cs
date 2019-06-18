using GravekeeperReboot.Source;
using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Systems;
using GravekeeperReboot.Source.Tiled;
using Nez;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;
		int world = 1;
		int level = 1;
		public TestScene() { }
		private TestScene(int world, int level) {
			this.world = world;
			this.level = level;
		}

		public override void initialize() {
			base.initialize();
			TiledMapRebuilder.Rebuild();

			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(new ArrowKeyBinding())); 
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(AnimationComponent))));
			gameBoard = addSceneComponent(new GameBoard());
		}

		public override void onStart() {
			base.onStart();
			gameBoard.LoadLevel(world, level);
			camera.setPosition(gameBoard.Center);
			camera.zoomIn(10);
			PlayerMoveComponent playerMove = findEntity(TiledMapConstants.TYPE_PLAYER).getComponent<PlayerMoveComponent>();
			CommandSystem command = getEntityProcessor<CommandSystem>();
			InputSystem input = getEntityProcessor<InputSystem>();
			MoveSystem move = getEntityProcessor<MoveSystem>();
			BindPlayerControls(input, playerMove);
			input.OnPressUndo += command.UndoTurn;
			playerMove.OnPlayerAction += command.StartNewTurn;
			input.OnInput += move.InterruptAnimation;
		}

		private void BindPlayerControls(InputSystem input, PlayerMoveComponent playerMove) {
			input.OnPressUp += playerMove.OnPressUp;
			input.OnPressDown += playerMove.OnPressDown;
			input.OnPressLeft += playerMove.OnPressLeft;
			input.OnPressRight += playerMove.OnPressRight;
			input.OnPressGrab += playerMove.OnPressGrab;
		}
	}
}
