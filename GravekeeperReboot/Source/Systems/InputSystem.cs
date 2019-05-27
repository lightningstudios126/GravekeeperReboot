using GravekeeperReboot.Source.ActionMapping;
using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using Nez;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		KeyBinding inputMap;

		private Entity player;
		private TileComponent playerTile;
		private GrabComponent grabComponent;

		GameBoard gameBoard;

		public InputSystem(KeyBinding inputMap) {
			this.inputMap = inputMap;
		}

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();
			if (player == null) {
				player = scene.findEntity("Player");
				playerTile = player.getComponent<TileComponent>();
				grabComponent = player.getComponent<GrabComponent>();
				gameBoard = scene.getSceneComponent<GameBoard>();
			}

			if (Input.isKeyPressed(inputMap.UpButton)) {
				Utilities.TileDirection direction = playerTile.tileDirection;
				if (!grabComponent.isGrabbing) {
					Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
					if (entityAhead == null || gameBoard.CanPush(entityAhead, direction)) {
						commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset(direction)));
						if (entityAhead != null) {
							commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
						}
					}
				} else if (gameBoard.CanPush(grabComponent.target, direction)) {
					commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset(direction)));
					commandSystem.QueueCommand(new MoveCommand(grabComponent.target, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
				}
			}

			if (Input.isKeyPressed(inputMap.DownButton)) {
				Utilities.TileDirection direction = Utilities.Directions.DirAdd(playerTile.tileDirection, Utilities.TileDirection.DOWN);
				Entity entityAhead = gameBoard.FindAtLocation(playerTile.tilePosition + Utilities.Directions.DirectionPointOffset(direction));
				if (entityAhead == null || gameBoard.CanPush(entityAhead, direction)) {
					commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset(direction)));
					if (grabComponent.isGrabbing) {
						commandSystem.QueueCommand(new MoveCommand(grabComponent.target, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
					}
					if (entityAhead != null) {
						commandSystem.QueueCommand(new MoveCommand(entityAhead, Utilities.Directions.DirectionPointOffset(direction)) { playerInitiated = false });
					}
				}
			}
			if (Input.isKeyPressed(inputMap.LeftButton)) {
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.LEFT));
			}

			if (Input.isKeyPressed(inputMap.RightButton)) {
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.RIGHT));

			}

			// "Toggle" grab
			if (Input.isKeyPressed(inputMap.GrabButton)) Grab(grabComponent.isGrabbing);

			// "Hold" grab 
			//if (Input.isKeyPressed(inputMap.GrabButton)) Grab(true);
			//if (Input.isKeyReleased(inputMap.GrabButton)) Grab(false);

			if (Input.isKeyPressed(inputMap.UndoButton)) Undo();
		}

		public void Grab(bool currentlyGrabbing) {
			if (currentlyGrabbing)
				commandSystem.QueueCommand(new DropCommand(player));
			else
				commandSystem.QueueCommand(new GrabCommand(player));
		}

		public void Undo() {
			commandSystem.QueueCommand(new UndoCommand());
		}
	}
}
