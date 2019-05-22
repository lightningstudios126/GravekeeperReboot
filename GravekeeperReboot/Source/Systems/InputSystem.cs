using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using GravekeeperReboot.Source.ActionMapping;
using Nez;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;
		KeyBinding inputMap;

		private Entity player;
		private RotateComponent rotateComponent;
		private GrabComponent grabComponent;

		public InputSystem(KeyBinding inputMap) {
			this.inputMap = inputMap;
		}

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();
			if (player == null) player = scene.findEntity("Player");
			if (rotateComponent == null) rotateComponent = player.getComponent<RotateComponent>();
			if (grabComponent == null) grabComponent = player.getComponent<GrabComponent>();

			if (Input.isKeyPressed(inputMap.UpButton)) Move(Utilities.TileDirection.UP);
			if (Input.isKeyPressed(inputMap.LeftButton)) Move(Utilities.TileDirection.LEFT);
			if (Input.isKeyPressed(inputMap.DownButton)) Move(Utilities.TileDirection.DOWN);
			if (Input.isKeyPressed(inputMap.RightButton)) Move(Utilities.TileDirection.RIGHT);

			//if (Input.isKeyPressed(inputMap.RotateLeftButton)) Rotate(Utilities.Directions.LEFT);
			//if (Input.isKeyPressed(inputMap.RotateRightButton)) Rotate(Utilities.Directions.RIGHT);

			// "Toggle" grab
			//if (Input.isKeyPressed(inputMap.GrabButton)) Grab(!grabComponent.isGrabbing);

			// "Hold" grab 
			if (Input.isKeyPressed(inputMap.GrabButton)) Grab(true);
			if (Input.isKeyReleased(inputMap.GrabButton)) Grab(false);

			if (Input.isKeyPressed(inputMap.UndoButton)) Undo();			
		}


		// Main Game Actions
		public void Move(Utilities.TileDirection direction) {
			int dir = (int)direction;

			if (rotateComponent.direction == (Utilities.TileDirection)((dir + 1) % 4))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.RIGHT));
			else if (rotateComponent.direction == (Utilities.TileDirection)((dir + 3) % 4))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.TileDirection.LEFT));
			else
				commandSystem.QueueCommand(new MoveCommand(player, Utilities.Directions.DirectionPointOffset((Utilities.TileDirection)dir)));
		}
		
		public void Rotate(Utilities.TileDirection direction) {
			commandSystem.QueueCommand(new RotateCommand(player, direction));
		}

		public void Grab(bool grabbing) {
			commandSystem.QueueCommand(new GrabCommand(player, grabbing));
		}

		public void Undo() {
			commandSystem.QueueCommand(new UndoCommand());
			Grab(false);
		}
	}
}
