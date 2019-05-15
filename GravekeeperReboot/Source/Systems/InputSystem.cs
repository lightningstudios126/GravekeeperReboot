using GravekeeperReboot.Source.Commands;
using GravekeeperReboot.Source.Components;
using Microsoft.Xna.Framework.Input;
using Nez;
using System;

namespace GravekeeperReboot.Source.Systems {
	public class InputSystem : ProcessingSystem {
		private CommandSystem commandSystem;

		private Action 
			MoveUpAction, 
			MoveLeftAction, 
			MoveDownAction, 
			MoveRightAction, 

			RotateLeftAction, 
			RotateRightAction,

			GrabAction, 
			ReleaseAction, 
			ToggleGrabAction,

			UndoAction;

		private Entity player;
		private RotateComponent rotateComponent;
		public InputSystem() : base() {

			MoveUpAction = () => Move(Utilities.Directions.UP);
			MoveLeftAction = () => Move(Utilities.Directions.LEFT);
			MoveDownAction = () => Move(Utilities.Directions.DOWN);
			MoveRightAction = () => Move(Utilities.Directions.RIGHT);

			RotateLeftAction = () => Rotate(Utilities.Directions.LEFT);
			RotateRightAction = () => Rotate(Utilities.Directions.RIGHT);

			ToggleGrabAction = () => Grab(!player.getComponent<GrabComponent>().isGrabbing);
			GrabAction = () => Grab(true);
			ReleaseAction = () => Grab(false);

			UndoAction = Undo;
		}

		public override void process() {
			if (commandSystem == null) commandSystem = scene.getEntityProcessor<CommandSystem>();
			if (player == null) player = scene.findEntity("Player");
			if (rotateComponent == null) rotateComponent = player.getComponent<RotateComponent>();

			if (Input.isKeyPressed(Keys.W)) MoveUpAction();
			if (Input.isKeyPressed(Keys.A)) MoveLeftAction();
			if (Input.isKeyPressed(Keys.S)) MoveDownAction();
			if (Input.isKeyPressed(Keys.D)) MoveRightAction();

			//if (Input.isKeyPressed(Keys.Left)) RotateLeftAction();
			//if (Input.isKeyPressed(Keys.Right)) RotateRightAction();

			// "Toggle" grab
			//if (Input.isKeyPressed(Keys.LeftShift)) ToggleGrabAction();

			// "Hold" grab 
			if (Input.isKeyPressed(Keys.LeftShift)) GrabAction();
			if (Input.isKeyReleased(Keys.LeftShift)) ReleaseAction();

			if (Input.isKeyPressed(Keys.C)) UndoAction();			
		}


		// Main Game Actions
		public void Move(Utilities.Directions direction) {
			int dir = (int)direction;

			if (rotateComponent.direction == (Utilities.Directions)((dir + 1) % 4))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.Directions.RIGHT));
			else if (rotateComponent.direction == (Utilities.Directions)((dir + 3) % 4))
				commandSystem.QueueCommand(new RotateCommand(player, Utilities.Directions.LEFT));
			else
				commandSystem.QueueCommand(new MoveCommand(player, Utilities.Direction.DirectionPointOffset((Utilities.Directions)dir)));
		}
		
		public void Rotate(Utilities.Directions direction) {
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
