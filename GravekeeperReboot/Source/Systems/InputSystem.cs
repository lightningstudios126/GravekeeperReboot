using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.ActionMapping;
using Nez;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		KeyBinding inputMap;

		private Entity player;
		private TileComponent tileComponent;
		private GrabComponent grabComponent;

		public InputSystem(KeyBinding inputMap) {
			this.inputMap = inputMap;
		}

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();
			if (player == null) {
				player = scene.findEntity("Player");
				tileComponent = player.getComponent<TileComponent>();
				grabComponent = player.getComponent<GrabComponent>();
			}

			if (Input.isKeyPressed(inputMap.UpButton))
				commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset(tileComponent.tileDirection)));
			if (Input.isKeyPressed(inputMap.LeftButton))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.LEFT));
			if (Input.isKeyPressed(inputMap.DownButton))
				commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset((Utilities.TileDirection)(((int)tileComponent.tileDirection + 2) % 4))));
			if (Input.isKeyPressed(inputMap.RightButton))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.RIGHT));

			// "Toggle" grab
			if (Input.isKeyPressed(inputMap.GrabButton)) Grab(!grabComponent.isGrabbing);

			// "Hold" grab 
			//if (Input.isKeyPressed(inputMap.GrabButton)) Grab(true);
			//if (Input.isKeyReleased(inputMap.GrabButton)) Grab(false);

			if (Input.isKeyPressed(inputMap.UndoButton)) Undo();
		}

		public void Grab(bool grabbing) {
			commandSystem.QueueCommand(new GrabCommand(player, grabbing));
		}

		public void Undo() {
			commandSystem.QueueCommand(new UndoCommand());
		}
	}
}
