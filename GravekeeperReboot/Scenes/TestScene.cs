using GravekeeperReboot.Source;
using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.Systems;
using GravekeeperReboot.Source.Tiled;
using Nez;
using Nez.Sprites;

namespace GravekeeperReboot.Scenes {
	class TestScene : Scene {
		GameBoard gameBoard;

		public override void initialize() {
			base.initialize();
			TiledMapRebuilder.Rebuild();

			addRenderer(new DefaultRenderer());
		
			addEntityProcessor(new CommandSystem());
			addEntityProcessor(new InputSystem(new ArrowKeyBinding())); 
			addEntityProcessor(new MoveSystem(new Matcher().all(typeof(AnimationComponent))));
			addEntityProcessor(new SoulSystem(new Matcher().all(typeof(Sprite))));

			gameBoard = addSceneComponent(new GameBoard());
		}

		public override void onStart() {
			base.onStart();
			gameBoard.LoadLevel(1, 1);
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
